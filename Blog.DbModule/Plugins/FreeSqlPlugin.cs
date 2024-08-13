using System.Reflection;
using Blog.DbModule.Models;
using Common.Lib.Attributes;
using Common.Lib.Helpers;
using FreeSql.Aop;

namespace Blog.DbModule.Plugins;

public static class FreeSqlPlugin
{
    public static void Aop(this IFreeSql<BlogDbFlag> freeSql)
    {
        freeSql.Aop.AuditValue += (s, e) =>
        {
            // 雪花Id
            if (e.Column.CsType == typeof(long) &&
                e.Property.GetCustomAttribute<SnowflakeAttribute>(false) != null &&
                e.Value?.ToString() == "0")
                e.Value = IdHelper.SnowId;

            // Guid
            if (e.Column.CsType == typeof(string) &&
                e.Property.GetCustomAttribute<GuidAttribute>(false) != null &&
                string.IsNullOrEmpty(e.Value?.ToString()))
                e.Value = IdHelper.Guid;

            // SimpleGuid
            if (e.Column.CsType == typeof(string) &&
                e.Property.GetCustomAttribute<SimpleGuidAttribute>(false) != null &&
                string.IsNullOrEmpty(e.Value?.ToString()))
                e.Value = IdHelper.SimpleGuid;
        };
    }
}