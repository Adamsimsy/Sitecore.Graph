using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Newtonsoft.Json.Serialization;
using Sitecore.Graph.Database;
using Sitecore.Graph.Models;

namespace Sitecore.Graph.Database
{
    public class SitecoreGraph : ISitecoreGraph
    {
        private readonly IGraphClient _graphClient;

        public SitecoreGraph()
        {
            //var graph = new SitecoreGraph(client);

            //_graphClient = client;
            _graphClient =
                Sitecore.DependencyInjection.ContainerContexts.WindsorContainerContext.Instance.Resolve<IGraphClient>();
        }

        private IGraphClient CreateGraphClient()
        {
            //Use an IoC container and register as a Singleton
            var url = "http://192.168.99.100:32768/db/data";
            var user = "neo4j";
            var password = "Password1!";

            return new GraphClient(new Uri(url), user, password);
        }

        public SitecoreNode ReadNode(string uri)
        {
            //var client = CreateGraphClient();

            _graphClient.Connect();

            return _graphClient.Cypher
                .Match("(item:Item)")
                .Where("item.Uri = {uri}")
                .WithParam("uri", uri)
                .Return<SitecoreNode>("item")
                .Results.FirstOrDefault();
        }

        public SitecoreNode CreateNode(SitecoreNode node)
        {
            _graphClient.Connect();

            var newNode = _graphClient.Cypher
                .Create("(item:Item {node})")
                .WithParam("node", node)
                .Return<SitecoreNode>("item")
                .Results.FirstOrDefault();

            return newNode;
        }

        public RelationshipReference CreateRelationship(SitecoreNode sourceNode, SitecoreNode targetNode)
        {
            //var client = CreateGraphClient();

            _graphClient.Connect();

            _graphClient.Cypher
                .Match("(item1:Item)", "(item2:Item)")
                .Where((SitecoreNode item1) => item1.Uri == sourceNode.Uri)
                .AndWhere((SitecoreNode item2) => item2.Uri == targetNode.Uri)
                .Merge("item1-[:LINKED_TO]->item2")
                .ExecuteWithoutResults();


            return null;
        }
    }
}
