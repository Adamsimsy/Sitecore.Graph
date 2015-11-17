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


            _managers = new List<ICustomLinkManager>() { new GraphLinkManager() };
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
