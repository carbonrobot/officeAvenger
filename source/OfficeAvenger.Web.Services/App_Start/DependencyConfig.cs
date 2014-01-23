namespace OfficeAvenger.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Dependencies;
    using Ninject;
    using Ninject.Modules;
    using OfficeAvenger.Domain.Data;
    using OfficeAvenger.Services;

    /// <summary>
    /// Its job is to Register Ninject Modules and Resolve Dependencies
    /// </summary>
    public class NinjectHttpContainer
    {
        //Register Ninject Modules
        public static void RegisterModules(NinjectModule[] modules)
        {
            _resolver = new NinjectHttpResolver(modules);
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        //Manually Resolve Dependencies
        public static T Resolve<T>()
        {
            return _resolver.Kernel.Get<T>();
        }

        private static NinjectHttpResolver _resolver;
    }

    // List and Describe Necessary HttpModules
    // This class is optional if you already Have NinjectMvc
    public class NinjectHttpModules
    {
        //Return Lists of Modules in the Application
        public static NinjectModule[] Modules
        {
            get
            {
                return new[] { new MainModule() };
            }
        }

        //Main Module For Application
        public class MainModule : NinjectModule
        {
            public override void Load()
            {
                Bind<IDataContext>().To<DataContext>();
            }
        }
    }

    /// <summary>
    /// Resolves Dependencies Using Ninject
    /// </summary>
    public class NinjectHttpResolver : IDependencyResolver, IDependencyScope
    {
        public NinjectHttpResolver(params NinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);
        }

        public IKernel Kernel { get; private set; }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            //Do Nothing
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }
}