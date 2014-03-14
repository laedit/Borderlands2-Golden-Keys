using Borderlands2GoldendKeys.Helpers;
using Borderlands2GoldendKeys.Models;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
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
            
            IndexCreation.CreateIndexes(typeof(ShiftCodesIndex).Assembly, documentStore);

            return documentStore;
        }
    }
}