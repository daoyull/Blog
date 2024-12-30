using Blog.DbModule.Constants;
using Blog.DbModule.Helper;
using Blog.DbModule.Models;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.FreeSql;
using Common.Lib.Exceptions;
using Common.Lib.Models;
using Common.Redis;
using FreeSql.Internal.Model;

using Mapster;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Blog.DbModule.Service.Impl;

public class CategoryServiceImpl : ICategoryService
{
    private readonly IFreeSql  _db;
    private IDatabase? _redis;

    public CategoryServiceImpl(FreeSqlResolver resolver, RedisResolver redisResolver)
    {
        _db = resolver.GetDatabase();
        _redis = redisResolver(BlogDbModule.BlogDatabaseName);
    }

    public async Task<CategoryVo> GetAsync(CategoryQueryDto query)
    {
        return await _db.Select<CategoryPo>()
            .WhereIf(query.Id != null, it => it.Id == query.Id)
            .WhereIf(!string.IsNullOrEmpty(query.CategoryName), it => it.CategoryName.Contains(query.CategoryName!))
            .ToOneAsync()
            .MapperTo<CategoryPo, CategoryVo>();
    }

    public async Task<PageResult<CategoryVo>> GetPageAsync(CategoryPageQueryDto query)
    {
        var pageInfo = query.Adapt<BasePagingInfo>();
        var list = await _db.Select<CategoryPo>()
            .WhereIf(query.Id != null, it => it.Id == query.Id)
            .WhereIf(!string.IsNullOrEmpty(query.CategoryName), it => it.CategoryName.Contains(query.CategoryName!))
            .Page(pageInfo)
            .ToListAsync()
            .MapperTo<List<CategoryPo>, List<CategoryVo>>();
        return new PageResult<CategoryVo>(pageInfo.Count, list);
    }

    public async Task<int> AddAsync(CategoryAddDto category)
    {
        var any = await _db.Select<CategoryPo>()
            .AnyAsync(it => it.CategoryName == category.CategoryName);
        if (any)
        {
          throw  new BusinessException("分类名称重复");
        }

        var po = category.Adapt<CategoryPo>();
        return await _db.Insert(po).ExecuteAffrowsAsync();
    }

    public async Task<int> EditAsync(CategoryEditDto category)
    {
        var any = await _db.Select<CategoryPo>()
            .WhereCascade(it => it.Id != category.Id!.Value)
            .AnyAsync(it => it.CategoryName == category.CategoryName);
        if (any)
        {
            throw new BusinessException("分类名称重复");
        }

        var po = category.Adapt<CategoryPo>();
        return await _db.Update<CategoryPo>()
            .SetSource(po)
            .ExecuteAffrowsAsync();
    }

    public async Task<int> DeleteAsync(long id)
    {
        var any = await _db.Select<BlogPo>()
            .AnyAsync(it => it.CategoryId == id);
        if (any)
        {
            throw new BusinessException("该分类下有博客，不能删除");
        }

        return await _db.Delete<CategoryPo>()
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
    }

    public async Task<int> DeleteListAsync(List<long> ids)
    {
        var any = await _db.Select<BlogPo>()
            .AnyAsync(it => ids.Contains(it.CategoryId));
        if (any)
        {
            throw new BusinessException("该分类下有博客，不能删除");
        }

        return await _db.Delete<CategoryPo>()
            .Where(it => ids.Contains(it.Id))
            .ExecuteAffrowsAsync();
    }

    public async Task<List<CategoryCacheVo>> GetCacheListAsync()
    {
        if (_redis != null)
        {
            var redisValue = await _redis.StringGetAsync(BlogConstant.CategoryCache);
            if (!redisValue.HasValue)
            {
                var list = await GetAllCacheList();
                await _redis.StringSetAsync(BlogConstant.CategoryCache, JsonConvert.SerializeObject(list));
                return list;
            }

            return JsonConvert.DeserializeObject<List<CategoryCacheVo>>(redisValue.ToString())!;
        }

        return await GetAllCacheList();
    }

    private async Task<List<CategoryCacheVo>> GetAllCacheList()
    {
        return await _db.Select<CategoryPo>()
            .ToListAsync()
            .MapperTo<List<CategoryPo>, List<CategoryCacheVo>>();
    }

    public async Task<List<CountChart>> GetTagBlogNum()
    {
        return await _db.Select<CategoryPo, BlogPo>()
            .InnerJoin((cat, blog) => cat.Id == blog.CategoryId)
            .GroupBy((cat, blog) => new { cat.Id, cat.CategoryName, cat.CategoryColor })
            .ToListAsync(g => new CountChart
            {
                Name = g.Key.CategoryName,
                Count = g.Count(),
                Color = g.Key.CategoryColor ?? "#f0f0f0"
            });
    }

    public async Task<long> GetIdByNameAsync(string categoryName)
    {
        var categoryPo = await _db.Select<CategoryPo>()
            .Where(it => it.CategoryName == categoryName)
            .ToOneAsync();
        return categoryPo?.Id ?? 0;
    }
}