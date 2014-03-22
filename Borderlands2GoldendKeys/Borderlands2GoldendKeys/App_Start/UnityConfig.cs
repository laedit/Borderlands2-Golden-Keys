using Borderlands2GoldendKeys.Helpers;
using Microsoft.Practices.Unity;
using PoliteCaptcha;
using Raven.Client;
using System.Web.Mvc;
using Unity.Mvc5;

namespace Borderlands2GoldendKeys
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
			var container = new UnityContainer();

            var documentStore = RavenDBConfig.InitializeDocumentStore();
            container.RegisterInstance<IDocumentStore>(documentStore);
            container.RegisterType<IDocumentSession>(new InjectionFactory(c => documentStore.OpenSession()));

            container.RegisterType<ShiftCodeUpdateProcess>();

            container.RegisterType<IConfigurationSource, RavenReCaptchaConfigurationSource>();

            container.RegisterType<ICaptchaGenerator, ReCaptchaGenerator>();
            container.RegisterType<ICaptchaValidator, ReCaptchaValidator>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }
    }
}