
using Chat.API.Config;
using Chat.API.Hubs;
using Chat.Application.Hubs;
using Chat.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Chat.Application.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

//var builder = WebApplication.CreateBuilder(args);
//用 WebApplicationOptions 代替args
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development,
    Args = args
});

// Add services to the container.

builder.Services.AddControllers();
//支持 HttpContextAccessor
builder.Services.AddHttpContextAccessor();
//获取IP
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//支持 SqlServer
builder.Services.AddDbContext<EFDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
//添加的singlr服务
builder.Services.AddSignalR();
builder.Services.AddControllers();


builder.Services.AddCors(cor =>
{
    cor.AddPolicy("Cors", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader()
               .WithOrigins("http://chat.yeyh.online")
               .WithOrigins("http://localhost:8080")
               .AllowCredentials();
        //不对的
        //policy.AllowAnyOrigin()
        //.AllowAnyHeader()
        //.AllowAnyMethod()
        //测试
        /*.AllowCredentials()*/;
         //policy.WithOrigins("http://localhost:8080")
         //.AllowCredentials() // 允许携带凭证
         //.WithHeaders("Authorization", "Content-Type") // 允许的请求头
         //.WithMethods("GET", "POST", "PUT", "DELETE"); // 允许的 HTTP 方法
    });
});
//支持 JWT
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));
var jwtSetting = new JwtSetting();
builder.Configuration.Bind("JwtSetting", jwtSetting);
builder.Services
   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,//是否验证Issuer
           ValidIssuer = jwtSetting.Issuer,//发行人Issuer
           ValidateAudience = true,//是否验证Audience
           ValidAudience = jwtSetting.Audience,//订阅人Audience

           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),//SecurityKey
           ValidateLifetime = true,//是否验证失效时间
           // 默认 300s
           ClockSkew = TimeSpan.Zero
       };
   });
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    ////添加依赖注入，AddModule是一个自定义的拓展方法，将依赖注入的方法单独提取出来，方便管理
    builder.Services.AddModule(container, builder.Configuration);
});
//添加AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReagentDetection.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer",
       new OpenApiSecurityScheme
       {
           In = ParameterLocation.Header,
           Description = "请输入OAuth接口返回的Token，前置Bearer。示例：Bearer {Roken}",
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey,
       });
    //声明一个Scheme，注意下面的Id要和上面AddSecurityDefinition中的参数name一致
    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    //注册全局认证（所有的接口都可以使用认证）
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            [scheme] = new string[0]
        });
});
var app = builder.Build();
app.UseStaticFiles();
//添加跨域设置
app.UseCors("Cors");
////调用中间件：UseAuthentication（认证），必须在所有需要身份认证的中间件前调用，比如 UseAuthorization（授权）。
app.UseAuthentication();
//添加的signalr
app.UseRouting();
////调用中间件：（授权）
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
    endpoints.MapHub<ChatHub>("/chatHub");
    endpoints.MapHub<ChatDemo>("/chatDemo");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();
