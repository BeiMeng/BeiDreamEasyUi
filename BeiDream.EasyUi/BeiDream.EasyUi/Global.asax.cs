using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BeiDream.Common;
using BeiDream.Services.CalibrationManagement;
using BeiDream.Services.CalibrationManagement.IServices;
using BeiDream.Services.CalibrationManagement.PetaPoco.Service;
using BeiDream.Services.Systems;
using BeiDream.Services.Systems.Commom;
using BeiDream.Services.Systems.Configs;
using BeiDream.Services.Systems.IServices;
using BeiDream.Services.Systems.PetaPoco.Service;
using Util.Files;

namespace BeiDream.EasyUi
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);//错误拦截过滤器注册
            RouteConfig.RegisterRoutes(RouteTable.Routes);//路由器注册
            BundleConfig.RegisterBundles(BundleTable.Bundles);//打包压缩文件注册

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .AsImplementedInterfaces();
            builder.RegisterType<SysPetaPocoUnitOfWork>().As<ISysPetaPocoUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<PetaPocoMenuRepository>().As<IMenuRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PetaPocoIconRepository>().As<IIconRepositiory>().InstancePerLifetimeScope();

            builder.RegisterType<ClmPetaPocoUnitOfWork>().As<IClmPetaPocoUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<PetaPocoParameterRepository>().As<IParameterRepository>().InstancePerLifetimeScope();
            builder.RegisterType<IconManager>().As<IIconManager>().InstancePerLifetimeScope();
            builder.RegisterType<TenantUploadPathStrategy>().As<IUploadPathStrategy>().SingleInstance();
            builder.RegisterType<FileUpload>().As<IFileUpload>().InstancePerLifetimeScope();
            builder.RegisterType<FileManager>().As<IFileManager>().InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}