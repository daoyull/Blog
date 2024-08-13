using Common.Lib.Service;
using Common.Mvvm.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Blog.Lib.Models
{
    public class ConfigVo : ObservableObject
    {
        private int? _id;

        /// <summary>
        /// Id
        /// </summary>
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string? _configName;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string? ConfigName
        {
            get => _configName;
            set => SetProperty(ref _configName, value);
        }

        private string? _configValue;

        /// <summary>
        /// 配置Value
        /// </summary>
        public string? ConfigValue
        {
            get => _configValue;
            set => SetProperty(ref _configValue, value);
        }

        private int? _configType;

        /// <summary>
        /// 1 json
        /// </summary>
        public int? ConfigType
        {
            get => _configType;
            set => SetProperty(ref _configType, value);
        }
    }

    public class ConfigQueryDto : ObservableObject
    {
        private int? _id;

        /// <summary>
        /// Id
        /// </summary>
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string? _configName;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string? ConfigName
        {
            get => _configName;
            set => SetProperty(ref _configName, value);
        }
    }

    public class ConfigPageQueryDto : MvvmPageQuery
    {
        private int? _id;

        /// <summary>
        /// Id
        /// </summary>
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string? _configName;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string? ConfigName
        {
            get => _configName;
            set => SetProperty(ref _configName, value);
        }
    }

    public class ConfigAddDto : ObservableObject, IDataVerify
    {
        private int? _id;

        /// <summary>
        /// 主键
        /// </summary>
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }


        private string? _configName;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string? ConfigName
        {
            get => _configName;
            set => SetProperty(ref _configName, value);
        }

        private string? _configValue;

        /// <summary>
        /// 配置Value
        /// </summary>
        public string? ConfigValue
        {
            get => _configValue;
            set => SetProperty(ref _configValue, value);
        }

        private int? _configType;

        /// <summary>
        /// 1 json
        /// </summary>
        public int? ConfigType
        {
            get => _configType;
            set => SetProperty(ref _configType, value);
        }
    }

    public class ConfigEditDto : ObservableObject, IDataVerify
    {
        private int? _id;

        /// <summary>
        /// 主键
        /// </summary>
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }


        private string? _configName;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string? ConfigName
        {
            get => _configName;
            set => SetProperty(ref _configName, value);
        }

        private string? _configValue;

        /// <summary>
        /// 配置Value
        /// </summary>
        public string? ConfigValue
        {
            get => _configValue;
            set => SetProperty(ref _configValue, value);
        }

        private int? _configType;

        /// <summary>
        /// 1 json
        /// </summary>
        public int? ConfigType
        {
            get => _configType;
            set => SetProperty(ref _configType, value);
        }
    }
}
