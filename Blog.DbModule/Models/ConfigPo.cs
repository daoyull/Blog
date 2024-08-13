using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models
{
    [Table(Name = "config")]
    public class ConfigPo
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; } = null!;

        /// <summary>
        /// 配置Value
        /// </summary>
        public string? ConfigValue { get; set; }

        /// <summary>
        /// 1 json
        /// </summary>
        public int ConfigType { get; set; }
        
    }
}
