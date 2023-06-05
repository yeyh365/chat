using Autofac;
using Chat.Application.Services;
using Chat.Domain.Repositories;
using Chat.EntityFrameworkCore.Entities;
using Chat.EntityFrameworkCore.Repositories;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;

namespace Chat.API.Config
{
    public static class AutoFac
    {
        public static IServiceCollection AddModule(this IServiceCollection services, ContainerBuilder builder, IConfiguration configuration)
        {
            //注册EF
            builder.RegisterType<EFRepositoryProvider>().As<IRepositoryProvider>().InstancePerLifetimeScope();// 等价于自带Ioc的Scoped
            builder.RegisterType<EFDBContext>().As<IDBContext>().InstancePerLifetimeScope();// 等价于自带Ioc的Scoped
            //builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();
            //初始化配置文件

            Type baseType = typeof(ApplicationService);//自定义的空类
            //动态运行项目的类库
            var compilationLibrary = DependencyContext.Default
                .RuntimeLibraries
                .Where(x => !x.Serviceable
                && x.Type == "project")
                .ToList();
            var count1 = compilationLibrary.Count;
            List<Assembly> assemblyList = new List<Assembly>();

            foreach (var _compilation in compilationLibrary)
            {
                try
                {
                    assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(_compilation.Name)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(_compilation.Name + ex.Message);
                }
            }
            //注册程序集的方式添加依赖
            builder.RegisterAssemblyTypes(assemblyList.ToArray())
             .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)//过滤非抽象类，继承了IDependency接口的类
             .AsSelf().AsImplementedInterfaces()
             .InstancePerLifetimeScope();//实例的生命周期

            //单独注册依赖服务
            //builder.RegisterType<MemoryCacheService>().As<ICacheService>().SingleInstance();

            return services;
        }
    }
}
