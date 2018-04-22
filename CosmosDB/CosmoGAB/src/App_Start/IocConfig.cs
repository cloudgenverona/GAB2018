namespace CosmoGab
{
    using System.Reflection;
    using System.Web.Mvc;

    using CosmoGab.Models;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;

    public class IocConfig
    {
        public static void RegisterComponents()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<IDocumentDBRepository<Item>, DocumentDBRepository<Item>>();
            container.Register<IDocumentDBRepository<Contact>, DocumentDBRepository<Contact>>();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}