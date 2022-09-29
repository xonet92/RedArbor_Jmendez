using Ninject;
using Ninject.Extensions.ChildKernel;
using RedArbor_Jmendez_DAL.Implementation;
using RedArbor_Jmendez_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using WebApiContrib.IoC.Ninject;

namespace RedArbor_Jmendez.Infraestructure
{
    public class NinjectR : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectR() : this(new StandardKernel())
        {
        }
        public NinjectR(IKernel ninjectKernel, bool scope = false)
        {
            kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(kernel);
            }
        }
        public IDependencyScope BeginScope()
        {
            return new NinjectR(AddRequestBindings(new ChildKernel(kernel)), true);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {

        }

        private void AddBindings(IKernel kernel)
        {
            // singleton and transient bindings go here
        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>().InSingletonScope();
            kernel.Bind<ILogApiCallsRepository>().To<LogApiCallsRepository>().InSingletonScope();
            return kernel;
        }
    }
}