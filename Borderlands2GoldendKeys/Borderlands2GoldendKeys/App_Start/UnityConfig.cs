using Microsoft.Practices.Unity;
using Raven.Client;
using System.Web.Mvc;
using Unity.Mvc5;

namespace Borderlands2GoldendKeys
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            var documentStore = RavenDBConfig.InitializeDocumentStore();
            container.RegisterType<IDocumentSession>(new InjectionFactory(c => documentStore.OpenSession()));
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}