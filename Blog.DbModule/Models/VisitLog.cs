using Common.Lib.Attributes;
using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models
{
    [Table(Name = "visit_log")]
    public class VisitLogPo
    {
        [Snowflake] [Column(IsPrimary = true)] public long Id { get; set; }


        /// <summary>
        /// 访客标识码
        /// </summary>
        public string? VisitId { get; set; }

        /// <summary>
        /// 请求接口
        /// </summary>
        public string Path { get; set; } = null!;

        /// <summary>
        /// 请求方式
        /// </summary>
        public string Method { get; set; } = null!;

        /// <summary>
        /// 请求参数
        /// </summary>
        public string Param { get; set; } = null!;

        /// <summary>
        /// 访问行为
        /// </summary>
        public string? Behavior { get; set; }

        /// <summary>
        /// 访问内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// ip来源
        /// </summary>
        public string? IpSource { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        public string? Os { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public string? Browser { get; set; }

        /// <summary>
        /// 请求耗时（毫秒）
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// user-agent用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateBy { get; set; }
    }
}