
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
//�� WebApplicationOptions ����args
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development,
    Args = args
});

// Add services to the container.

builder.Services.AddControllers();
//֧�� HttpContextAccessor
builder.Services.AddHttpContextAccessor();
//��ȡIP
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//֧�� SqlServer
builder.Services.AddDbContext<EFDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
//��ӵ�singlr����
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
        //���Ե�
        //policy.AllowAnyOrigin()
        //.AllowAnyHeader()
        //.AllowAnyMethod()
        //����
        /*.AllowCredentials()*/;
         //policy.WithOrigins("http://localhost:8080")
         //.AllowCredentials() // ����Я��ƾ֤
         //.WithHeaders("Authorization", "Content-Type") // ���������ͷ
         //.WithMethods("GET", "POST", "PUT", "DELETE"); // ����� HTTP ����
    });
});
//֧�� JWT
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));
var jwtSetting = new JwtSetting();
builder.Configuration.Bind("JwtSetting", jwtSetting);
builder.Services
   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,//�Ƿ���֤Issuer
           ValidIssuer = jwtSetting.Issuer,//������Issuer
           ValidateAudience = true,//�Ƿ���֤Audience
           ValidAudience = jwtSetting.Audience,//������Audience

           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),//SecurityKey
           ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
           // Ĭ�� 300s
           ClockSkew = TimeSpan.Zero
       };
   });
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    ////�������ע�룬AddModule��һ���Զ������չ������������ע��ķ���������ȡ�������������
    builder.Services.AddModule(container, builder.Configuration);
});
//���AutoMapper
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
           Description = "������OAuth�ӿڷ��ص�Token��ǰ��Bearer��ʾ����Bearer {Roken}",
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey,
       });
    //����һ��Scheme��ע�������IdҪ������AddSecurityDefinition�еĲ���nameһ��
    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    //ע��ȫ����֤�����еĽӿڶ�����ʹ����֤��
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            [scheme] = new string[0]
        });
});
var app = builder.Build();
app.UseStaticFiles();
//��ӿ�������
app.UseCors("Cors");
////�����м����UseAuthentication����֤����������������Ҫ�����֤���м��ǰ���ã����� UseAuthorization����Ȩ����
app.UseAuthentication();
//��ӵ�signalr
app.UseRouting();
////�����м��������Ȩ��
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
