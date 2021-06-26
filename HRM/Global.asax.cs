using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using HRM.Domain.IOC;
using HRM.Domain.Mapping;
using HRM.IOC;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

namespace HRM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<AutoMapperConfig>();
                });
            
            configuration.CompileMappings();
                
            BundleConfig.RegisterBundles(BundleTable.Bundles);
 
            NinjectModule hrmDependencyModule = new HRMDependencyModule();
            NinjectModule domainDependencyModule = new DomainDependencyModule();
            var kernel = new StandardKernel(hrmDependencyModule, domainDependencyModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            
            
        }
    }
}
