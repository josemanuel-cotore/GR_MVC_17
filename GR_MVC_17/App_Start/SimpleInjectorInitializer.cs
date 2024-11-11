[assembly: WebActivator.PostApplicationStartMethod(typeof(GR_MVC_17.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace GR_MVC_17.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using GR_MVC_17.DAL;
    using GR_MVC_17.Servicios;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            container.Register<IUsuarioRepositiorio, UsuarioRepositiorio>(Lifestyle.Scoped);
            container.Register<IRutasRepositorio, RutasRepositorio>(Lifestyle.Scoped);
            container.Register<IRegistroRepositorio, RegistroRepositorio>(Lifestyle.Scoped);
            container.Register<IPerfilRepositorio, PerfilRepositorio>(Lifestyle.Scoped);
            container.Register<IInconvenienteRepositorio, InconvenienteRepositorio>(Lifestyle.Scoped);
            container.Register<IHerramientaRepositorio, HerramientaRepositorio>(Lifestyle.Scoped);
            //container.Register<>

        }
    }
}