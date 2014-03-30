using Borderlands2GoldenKeys.Helpers;
using Borderlands2GoldenKeys.Models;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using System.Linq;

namespace Borderlands2GoldenKeys
{
    public static class RavenDBConfig
    {
        public static IDocumentStore InitializeDocumentStore()
        {

#if DEBUG
            Raven.Database.Server.NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
#endif
            var documentStore = new EmbeddableDocumentStore
            {
                DataDirectory = @"~\App_Data\Database",
#if DEBUG
                UseEmbeddedHttpServer = true,
                //RunInMemory = true,
#endif
            };
            documentStore.Conventions.IdentityPartsSeparator = "-";
            documentStore.Conventions.DefaultQueryingConsistency = Raven.Client.Document.ConsistencyOptions.AlwaysWaitForNonStaleResultsAsOfLastWrite;
            documentStore.Conventions.RegisterIdConvention<Settings>((dbname, commands, user) => Settings.UniqueId);
            documentStore.Initialize();
            
            IndexCreation.CreateIndexes(typeof(ShiftCodesIndex).Assembly, documentStore);

            return documentStore;
        }
    }
}