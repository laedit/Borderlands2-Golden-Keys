﻿using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                //UseEmbeddedHttpServer = true
#endif
            };
            documentStore.Conventions.IdentityPartsSeparator = "-";
            documentStore.Conventions.DefaultQueryingConsistency = Raven.Client.Document.ConsistencyOptions.AlwaysWaitForNonStaleResultsAsOfLastWrite;
            documentStore.Initialize();

            // if necessary
            //IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), documentStore);

            return documentStore;
        }
    }
}