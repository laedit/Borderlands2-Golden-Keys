using Borderlands2GoldendKeys.Models;
using Raven.Client;
using Raven.Client.Embedded;
using System.Linq;

namespace Borderlands2GoldendKeys
{
    public static class RavenDBConfig
    {
        public static IDocumentStore InitializeDocumentStore()
        {

#if DEBUG
            //NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
#endif
            var documentStore = new EmbeddableDocumentStore
            {
                DataDirectory = @"~\App_Data\Database",
#if DEBUG
                //UseEmbeddedHttpServer = true,
                //RunInMemory = true,
#endif
            };
            documentStore.Conventions.IdentityPartsSeparator = "-";
            documentStore.Conventions.DefaultQueryingConsistency = Raven.Client.Document.ConsistencyOptions.AlwaysWaitForNonStaleResultsAsOfLastWrite;
            documentStore.Initialize();

            // if necessary
            //IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), documentStore);

            using (IDocumentSession documentSession = documentStore.OpenSession())
            {
                // Store some ClapTrap's quotes if needed
                if (!documentSession.Query<ClapTrapQuote>().Any())
                {
                    ClapTrapQuote.GetBaseQuotes().ForEach(q => documentSession.Store(q));
                }

                documentSession.SaveChanges();
            }

            return documentStore;
        }
    }
}