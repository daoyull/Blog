﻿using System.ComponentModel.DataAnnotations;
using Blog.DbModule.Models;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.FreeSql;
using Common.Lib.Exceptions;
using Common.Lib.Models;
using FreeSql.Internal.Model;
using LanguageExt.Common;
using Mapster;

namespace Blog.DbModule.Service.Impl;

public class TagServiceImpl : ITagService
{
    private IFreeSql _db;

    public TagServiceImpl(FreeSqlResolver resolver)
    {
        _db = resolver(BlogDbModule.BlogDatabaseName);
    }

    public async Task<Result<int>> AddAsync(TagAddDto tag)
    {
        // 业务校验
        if (await _db.Select<TagPo>().AnyAsync(it => it.TagName == tag.TagName))
        {
            return new Result<int>(new BusinessException("标签名称重复"));
        }

        var tagPo = tag.Adapt<TagPo>();
        return await _db.Insert(tagPo).ExecuteAffrowsAsync();
    }

    public async Task<Result<int>> DeleteAsync(long tagId)
    {
        var anyAsync = await _db.Select<RelBlogTagPo>()
            .Where(it => tagId == it.TagId)
            .AnyAsync();
        if (anyAsync)
        {
            return new Result<int>(new BusinessException("存在已使用的标签，无法删除"));
        }

        return await _db.Delete<TagPo>()
            .Where(i => i.Id == tagId)
            .ExecuteAffrowsAsync();
    }

    public async Task<Result<int>> EditAsync(TagEditDto tag)
    {
        // 
        var anyAsync = await _db.Select<TagPo>()
            .Where(it => it.Id != tag.Id!.Value)
            .AnyAsync(it => it.TagName == tag.TagName);
        if (anyAsync)
        {
            return new Result<int>(new BusinessException("标签名称重复"));
        }

        var tagPo = tag.Adapt<TagPo>();

        return await _db.Update<TagPo>()
            .SetSource(tagPo)
            .ExecuteAffrowsAsync();
    }

    public async Task<Result<TagVo>> GetAsync(TagQueryDto query)
    {
        var tagPo = await _db.Select<TagPo>()
            .WhereIf(query.Id != null, it => it.Id == query.Id)
            .WhereIf(!string.IsNullOrEmpty(query.TagName), it => it.TagName.Contains(query.TagName!))
            .FirstAsync();
        var tagVo = tagPo.Adapt<TagVo>();
        return tagVo;
    }

    public async Task<Result<PageResult<TagVo>>> GetPageAsync(TagPageQueryDto query)
    {
        var pagingInfo = query.Adapt<BasePagingInfo>();
        var pageList = await _db.Select<TagPo>()
            .WhereIf(query.Id != null, it => it.Id == query.Id)
            .WhereIf(!string.IsNullOrEmpty(query.TagName), it => it.TagName.Contains(query.TagName!))
            .Page(pagingInfo)
            .ToListAsync()
            .MapperTo<List<TagPo>, List<TagVo>>();

        return new PageResult<TagVo>(pagingInfo.Count, pageList);
    }

    public async Task<Result<List<TagCacheVo>>> GetCacheListAsync()
    {
        return await _db.Select<TagPo>()
            .ToListAsync()
            .MapperTo<List<TagPo>, List<TagCacheVo>>();
    }

    public async ValueTask<Result<List<CountChart>>> GetTagBlogNum()
    {
        return await _db.Select<RelBlogTagPo, TagPo>()
            .InnerJoin((rel, tag) => rel.TagId == tag.Id)
            .GroupBy((rel, tag) => new { rel.TagId, tag.TagColor, tag.TagName })
            .ToListAsync(g => new CountChart
            {
                Name = g.Key.TagName,
                Count = g.Count(),
                Color = g.Key.TagColor ?? "#f0f0f0"
            });
    }

    public async Task<Result<long>> GetIdByNameAsync(string tagName)
    {
        var categoryPo = await _db.Select<TagPo>()
            .Where(it => it.TagName == tagName)
            .ToOneAsync();
        return categoryPo?.Id ?? 0;
    }

    public async Task<Result<int>> DeleteListAsync(List<long> toList)
    {
        var anyAsync = await _db.Select<RelBlogTagPo>()
            .Where(it => toList.Contains(it.TagId))
            .AnyAsync();
        if (anyAsync)
        {
            return new Result<int>(new BusinessException("存在已使用的标签，无法删除"));
        }

        using var uow = _db.CreateUnitOfWork();
        var rows = await uow.Orm.Delete<TagPo>()
            .Where(it => toList.Contains(it.Id))
            .ExecuteAffrowsAsync();
        uow.Commit();
        return rows;
    }
}