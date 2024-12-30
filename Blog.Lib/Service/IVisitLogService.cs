using Blog.Lib.Models;


namespace Blog.Lib.Service;

public interface IVisitLogService
{
    Task<int> AddAsync(VisitLogModel visitLogModelPo);
}