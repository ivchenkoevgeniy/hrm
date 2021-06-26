using AutoMapper;
using HRM.Domain;
using HRM.Domain.Services;
using Ninject.Modules;

namespace HRM.IOC
{
    public class HRMDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEmployeeService>().To<EmployeeService>();
        }
    }
}