using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.DbModule;
using Common.FreeSql;
using Common.FreeSql.Models;
using Common.Lib.Ioc;
using Common.Redis;
using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Config

builder.Services.AddOptions<FreeSqlOptions>("blog").BindConfiguration("blog_db");
builder.Services.AddOptions<RedisOptions>("blog").BindConfiguration("blog_redis");

#endregion

#region Json

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // 设置全局的日期时间格式
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

#endregion

#region AutoFac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
{
    containerBuilder.RegisterModule<BlogDbModule>();
    containerBuilder.AddFreeSql();
    containerBuilder.AddRedis();
    var searcher = new Searcher(CachePolicy.File, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ip.merge.txt"));
    containerBuilder.RegisterInstance(searcher).As<ISearcher>().SingleInstance();
}));

#endregion

#region cors

const string cors = "Cors";
builder.Services.AddCors(policy => policy.AddPolicy(cors,
    opt =>
    {
        opt
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    }
));

#endregion

var app = builder.Build();
Ioc.SetRootContainer(app.Services.GetAutofacRoot());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(cors);
app.MapControllers();

app.Run();