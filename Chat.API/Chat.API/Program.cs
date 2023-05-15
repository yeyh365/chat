
using Chat.API.Hubs;
using Chat.Application.Hubs;
using Chat.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//支持 HttpContextAccessor
builder.Services.AddHttpContextAccessor();
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
var app = builder.Build();
app.UseStaticFiles();
//添加跨域设置
app.UseCors("Cors");
//添加的signalr
app.UseRouting();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
