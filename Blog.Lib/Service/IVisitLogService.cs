using Blog.Lib.Models;
using LanguageExt.Common;

namespace Blog.Lib.Service;

public interface IVisitLogService
{
    Task<Result<int>> AddAsync(VisitLogModel visitLogModelPo);
}