using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Sitecore.Data.Items;
using Sitecore.Graph.Database;
using Sitecore.Links;

namespace Sitecore.Graph.LinkDatabase
{
    public class LinkDatabaseFactory : ILinkDatabaseFactory
    {
        private readonly IEnumerable<ICustomLinkManager> _managers;

        public LinkDatabaseFactory()
        {
            //Use an IoC container and register as a Singleton
            var url = "http://192.168.99.100:32769/db/data";
            var user = "neo4j";
            var password = "Password1!";

            var client = new GraphClient(new Uri(url), user, password);

            var graph = new SitecoreGraph(client);

            _managers = new List<ICustomLinkManager>() { new GraphLinkManager(graph) };
        }

        public ICustomLinkManager GetContextLinkManager()
        {
            return _managers.FirstOrDefault();
        }

        public ICustomLinkManager GetContextLinkManager(Data.Database database)
        {
            return _managers.FirstOrDefault();
        }

        public ICustomLinkManager GetContextLinkManager(Item item)
        {
            return _managers.FirstOrDefault();
        }
    }
}
