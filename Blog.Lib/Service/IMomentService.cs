using Blog.Lib.Models;
using Common.Lib.Models;


namespace Blog.Lib.Service;

public interface IMomentService
{
    Task<PageResult<MomentVo>> GetMomentPageList(MomentPageQueryDto query);
    Task<bool> LikeMoment(long id);
    Task<bool> Published(long id, bool published);
    Task<MomentVo> GetAsync(long id);
    Task<int> EditAsync(MomentEditDto moment);
    Task<int> AddAsync(MomentAddDto moment);
    Task<int> DeleteAsync(long id);
}
