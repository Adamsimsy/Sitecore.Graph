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
        }

        private IGraphClient CreateGraphClient()
        {
            //Use an IoC container and register as a Singleton
            var url = "http://192.168.99.100:32769/db/data";
            var user = "neo4j";
            var password = "Password1!";

            return new GraphClient(new Uri(url), user, password);
        }

        public NodeReference<SitecoreNode> ReadNode(string uri)
        {
            var client = CreateGraphClient();

            client.Connect();

            return client.Cypher
                .Match("(m:Item)")
                .Where("m.Uri == {uri}")
                .WithParam("uri", uri)
                .Return<NodeReference<SitecoreNode>>("m")
                .Results.FirstOrDefault();
        }

        public NodeReference<SitecoreNode> CreateNode(SitecoreNode node)
        {
            var client = CreateGraphClient();

            client.Connect();

            return _graphClient.Create(node);
        }

        public RelationshipReference CreateRelationship(NodeReference<SitecoreNode> nodeReference, SitecoreRelationship relationship)
        {
            var client = CreateGraphClient();

            client.Connect();

            return _graphClient.CreateRelationship(nodeReference, relationship);
        }
    }
}
