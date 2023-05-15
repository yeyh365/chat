
using Chat.API.Hubs;
using Chat.Application.Hubs;
using Chat.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//֧�� HttpContextAccessor
builder.Services.AddHttpContextAccessor();
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
var app = builder.Build();
app.UseStaticFiles();
//��ӿ�������
app.UseCors("Cors");
//��ӵ�signalr
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
