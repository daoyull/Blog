using Blog.DbModule.Models;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.Lib.Helpers;
using Common.Lib.Ioc;
using IP2Region.Net.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using UAParser;

namespace Blog.WebApi.Aspect;

/// <summary>
/// 流量统计
/// </summary>
public class VisitLog : ActionFilterAttribute
{
    /// <summary>
    /// 备注
    /// </summary>
    public string? Behavitor { get; set; }

    /// <summary>
    /// 方法执行前调用
    /// </summary>
    /// <param name="context">请求上下文</param>
    /// <param name="next">方法委托</param>
    /// <returns></returns>
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;
        var request = httpContext.Request;


        var getIpResult = request.Headers.TryGetValue("X-Forwarded-For", out var ipValue);
        var visitIdResult = request.Headers.TryGetValue("identification", out var visitId);
        var userAgent = request.Headers.UserAgent.ToString();
        var path = request.Path;
        var method = request.Method;
        var param = request.QueryString.ToString();

        var startTime = TimeHelper.Timestamp;
        var task = base.OnActionExecutionAsync(context, next);
        var totalTime = TimeHelper.Timestamp - startTime;

        async void CallBack(object? asyncCallback)
        {
            try
            {
                var service = Ioc.ResolveOptional<IVisitLogService>();
                var visitLog = new VisitLogModel();
                visitLog.Id = IdHelper.SnowId;
                visitLog.VisitId = visitIdResult ? visitId : "";
                visitLog.Path = path;
                visitLog.Method = method;
                visitLog.Param = param;
                visitLog.Behavior = Behavitor ?? "";

                visitLog.Times = (int)totalTime;
                visitLog.CreateTime = TimeHelper.NowCst;
                visitLog.UserAgent = userAgent;
                visitLog.CreateBy = "system";
                if (getIpResult)
                {
                    var searcher = Ioc.Resolve<ISearcher>();

                    visitLog.Ip = ipValue.ToString();
                    var search = searcher.Search(visitLog.Ip);
                    visitLog.IpSource = search;
                }

                var clientInfo = Parser.GetDefault().Parse(userAgent);
                visitLog.Os = clientInfo.OS.ToString();
                visitLog.Browser = clientInfo.UA.ToString();
                await service!.AddAsync(visitLog);
            }
            catch (Exception e)
            {
            }
        }

        ThreadPool.QueueUserWorkItem(CallBack);

        return task;
    }
}