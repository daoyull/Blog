using Blog.Lib.Models;
using Common.Lib.Models;
using LanguageExt.Common;

namespace Blog.Lib.Service;

public interface IMomentService
{
    Task<Result<PageResult<MomentVo>>> GetMomentPageList(MomentPageQueryDto query);
    Task<Result<bool>> LikeMoment(long id);
    Task<Result<bool>> Published(long id, bool published);
    Task<Result<MomentVo>> GetAsync(long id);
    Task<Result<int>> EditAsync(MomentEditDto moment);
    Task<Result<int>> AddAsync(MomentAddDto moment);
    Task<Result<int>> DeleteAsync(long id);
}
