using Blog.DbModule.Models;
using Blog.Lib.Entity;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.FreeSql;
using Common.Lib.Helpers;
using Common.Lib.Models;
using FreeSql.Internal.Model;
using LanguageExt.Common;
using Mapster;

namespace Blog.DbModule.Service.Impl;

public class BlogServiceImpl : IBlogService
{
    private readonly IFreeSql _db;

    public BlogServiceImpl(FreeSqlResolver resolver)
    {
        _db = resolver(BlogDbModule.BlogDatabaseName);
    }

    public async Task<Result<PageResult<BlogVo>>> GetPageAsync(BlogPageQueryDto query, bool showPass = false)
    {
        var pageInfo = query.Adapt<BasePagingInfo>();
        var list = await _db.Select<BlogPo>()
            .Where(a => a.Type == 1)
            .WhereIf(query.CategoryId != null && query.CategoryId != 1, it => it.CategoryId == query.CategoryId)
            .WhereIf(!string.IsNullOrEmpty(query.Title), it => it.Title.Contains(query.Title!))
            .LeftJoin(a => a.Category.Id == a.CategoryId)
            .IncludeMany(a => a.Tags)
            .Page(pageInfo)
            .ToListAsync()
            .MapperTo<List<BlogPo>, List<BlogVo>>();
        if (!showPass)
        {
            list.ForEach(it => it.Password = "");
        }

        return new PageResult<BlogVo>(pageInfo.Count, list);
    }

    public async Task<Result<PageResult<BlogCardVo>>> QueryCardPageList(BlogPageQueryDto query)
    {
        var pageInfo = query.Adapt<BasePagingInfo>();
        var list = await _db.Select<BlogPo>()
            .Where(a => a.Type == 1)
            .LeftJoin(a => a.Category.Id == a.CategoryId)
            .IncludeMany(a => a.Tags)
            .Page(pageInfo)
            .ToListAsync()
            .MapperTo<List<BlogPo>, List<BlogCardVo>>();
        return new PageResult<BlogCardVo>(pageInfo.Count, list);
    }

    public async Task<Result<List<Archive>>> QueryArchiveList()
    {
        var pos = await _db.Select<BlogPo>()
            .Where(blog => blog.Type == 1)
            .OrderByDescending(it => it.CreateTime)
            .ToListAsync(it => new BlogPo()
            {
                Id = it.Id,
                Title = it.Title,
                CreateTime = it.CreateTime
            });

        var dict = new Dictionary<string, List<BlogPo>>();
        foreach (var blogPo in pos)
        {
            if (blogPo.CreateTime == null)
            {
                continue;
            }

            var time = blogPo.CreateTime.Value;
            var yearMonth = time.ToString("yyyy年MM月");
            if (!dict.ContainsKey(yearMonth))
            {
                dict.Add(yearMonth, new List<BlogPo>());
            }

            dict[yearMonth].Add(blogPo);
        }

        var res = new List<Archive>();
        foreach (var keyValuePair in dict)
        {
            var item = new Archive();
            item.Time = keyValuePair.Key;
            var archiveBlogItems = keyValuePair.Value.Select(it => new ArchiveBlogItem
            {
                Day = it.CreateTime!.Value.ToString("dd日"),
                Title = it.Title,
                Id = it.Id
            });
            item.Blogs = new(archiveBlogItems);
            res.Add(item);
        }

        return res;
    }

    public async Task<Result<BlogCardVo>> GetBlogContentAsync(long blogId, bool showPass = false)
    {
        var blogCardVo = await _db.Select<BlogContentPo>()
            .Where(a => a.Type == 1 && a.Id == blogId)
            .LeftJoin(a => a.Category.Id == a.CategoryId)
            .IncludeMany(a => a.Tags)
            .ToOneAsync()
            .MapperTo<BlogContentPo, BlogCardVo>();
        if (!showPass)
        {
            blogCardVo.Password = "";
        }

        return blogCardVo;
    }

    public async Task<Result<PageResult<BlogCardVo>>> GetBlogPageByTagId(long tagId, int pageNum, int pageSize)
    {
        var pageInfo = new BasePagingInfo()
        {
            PageNumber = pageNum,
            PageSize = pageSize
        };
        var blogIds = await _db.Select<RelBlogTagPo>()
            .Where(it => it.TagId == tagId)
            .ToListAsync(it => it.BlogId);
        var list = await _db.Select<BlogPo>()
            .Where(a => a.Type == 1 && blogIds.Contains(a.Id))
            .LeftJoin(a => a.Category.Id == a.CategoryId)
            .IncludeMany(a => a.Tags)
            .Page(pageInfo)
            .ToListAsync();
        return new PageResult<BlogCardVo>(pageInfo.Count, list.Adapt<List<BlogCardVo>>());
    }

    public async Task<Result<PageResult<BlogCardVo>>> GetBlogPageByCategoryId(long categoryId, int pageNum,
        int pageSize)
    {
        var pageInfo = new BasePagingInfo()
        {
            PageNumber = pageNum,
            PageSize = pageSize
        };
        var list = await _db.Select<BlogPo>()
            .Where(a => a.Type == 1 && a.CategoryId == categoryId)
            .LeftJoin(a => a.Category.Id == a.CategoryId)
            .IncludeMany(a => a.Tags)
            .Page(pageInfo)
            .ToListAsync();
        return new PageResult<BlogCardVo>(pageInfo.Count, list.Adapt<List<BlogCardVo>>());
    }

    public async Task<Result<List<NewBlog>>> QueryNewBlog(int num)
    {
        return await _db.Select<BlogPo>()
            .Where(blog => blog.Type == 1)
            .OrderByDescending(it => it.CreateTime)
            .Take(num)
            .ToListAsync(it => new NewBlog()
            {
                BlogId = it.Id,
                Title = it.Title,
            });
    }

    public async Task<Result<List<RandomBlog>>> QueryRandomBlogs(int num)
    {
        return await _db.Select<BlogPo>()
            .Where(blog => blog.Type == 1)
            .OrderByRandom()
            .Take(num)
            .ToListAsync(it => new RandomBlog()
            {
                Id = it.Id,
                Title = it.Title,
                PreviewImg = it.PreviewImg,
                CreateTime = it.CreateTime
            });
    }

    public async Task<Result<int>> AddAsync(BlogAddDto model)
    {
        var po = model.Adapt<BlogContentPo>();
        var id = IdHelper.SnowId;
        var rel = model.TagIds.Select(it => new RelBlogTagPo
        {
            TagId = it,
            BlogId = id,
        }).ToList();
        po.Id = id;
        using var uow = _db.CreateUnitOfWork();
        // Blog
        var rows = await uow.Orm.Insert(po).ExecuteAffrowsAsync();
        await uow.Orm.Insert(rel).ExecuteAffrowsAsync();
        uow.Commit();
        return rows;
    }

    public async Task<Result<int>> EditAsync(BlogEditDto model)
    {
        var po = model.Adapt<BlogContentPo>();
        var rel = model.TagIds.Select(it => new RelBlogTagPo
        {
            TagId = it,
            BlogId = model.Id!.Value,
        }).ToList();
        using var uow = _db.CreateUnitOfWork();
        var rows = await uow.Orm.Update<BlogContentPo>().SetSource(po).ExecuteAffrowsAsync();
        await uow.Orm.Delete<RelBlogTagPo>().Where(it => it.BlogId == model.Id).ExecuteAffrowsAsync();
        await uow.Orm.Insert(rel).ExecuteAffrowsAsync();
        uow.Commit();
        return rows;
    }

    public async Task<Result<int>> DeleteAsync(long argId)
    {
        using var uow = _db.CreateUnitOfWork();
        var rows = await uow.Orm.Delete<BlogContentPo>().Where(it => it.Id == argId).ExecuteAffrowsAsync();
        await uow.Orm.Delete<RelBlogTagPo>().Where(it => it.BlogId == argId).ExecuteAffrowsAsync();
        uow.Commit();
        return rows;
    }

    public async Task<Result<int>> DeleteListAsync(List<long> toList)
    {
        using var uow = _db.CreateUnitOfWork();
        var rows = await uow.Orm.Delete<BlogContentPo>().Where(it => toList.Contains(it.Id)).ExecuteAffrowsAsync();
        await uow.Orm.Delete<RelBlogTagPo>().Where(it => toList.Contains(it.BlogId)).ExecuteAffrowsAsync();
        uow.Commit();
        return rows;
    }

    public async Task<Result<List<SearchBlogResult>>> GetSearchBlog(string query)
    {
        var list = await _db.Select<BlogContentPo>()
            .Where(it => it.Title.Contains(query) || (it.Content ?? "").Contains(query))
            .ToListAsync();
        Func<string, string> subContent = (content) =>
        {
            if (string.IsNullOrEmpty(content))
            {
                return "";
            }

            var indexOf = content.IndexOf(query, StringComparison.OrdinalIgnoreCase);
            return indexOf == -1
                ? ""
                : content.Substring(Math.Max(0, indexOf - 10), Math.Min(query.Length + 20, content.Length));
        };
        return list.Select(it => new SearchBlogResult()
        {
            Title = it.Title,
            Id = it.Id,
            Content = subContent(it.Content ?? "")
        }).ToList();
    }

    public async Task<Result<bool>> TopAsync(long id, bool top)
    {
        var rows = await _db.Update<BlogPo>()
            .Set(it => it.IsTop, top)
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
        return rows == 1;
    }

    public async Task<Result<bool>> RecommendAsync(long id, bool recommend)
    {
        var rows = await _db.Update<BlogPo>()
            .Set(it => it.IsRecommend, recommend)
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
        return rows == 1;
    }

    public async Task<Result<bool>> EditVisibility(long id, BlogEditVisibilityDto blog)
    {
        var rows = await _db.Update<BlogPo>()
            .Set(it => it.IsPublished, blog.IsPublished)
            .Set(it => it.IsAppreciation, blog.IsAppreciation)
            .Set(it => it.IsComment, blog.IsComment)
            .Set(it => it.IsRecommend, blog.IsRecommend)
            .Set(it => it.IsTop, blog.IsTop)
            .Set(it => it.Password, blog.Password)
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
        return rows == 1;
    }
}