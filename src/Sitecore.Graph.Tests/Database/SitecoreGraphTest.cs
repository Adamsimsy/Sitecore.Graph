using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Sitecore.Graph.Database;
using Sitecore.Graph.Models;
using Xunit;

namespace Sitecore.Graph.Tests.Database
{
    public class SitecoreGraphTest
    {
        private ISitecoreGraph _graph;

        public SitecoreGraphTest()
        {
            //Use an IoC container and register as a Singleton
            var url = "http://192.168.99.100:32769/db/data";
            var user = "neo4j";
            var password = "Password1!";

            var client = new GraphClient(new Uri(url), user, password);

            _graph = new SitecoreGraph();
        }

        [Fact]
        public void Can_add_new_item_to_graph()
        {
            var teamNode = _graph.CreateNode(new SitecoreNode() { Uri = "1", Name = "Arsenal"});
            var playerNode = _graph.CreateNode(new SitecoreNode() { Uri = "2", Name = "Walcott" });

            var agentNode = _graph.CreateNode(new SitecoreNode() { Uri = "3", Name = "Bloggs" });

            //var teamToPlayerRelationship = new SitecoreRelationship(new NodeReference(playerNode.Id), "TeamToPlayer");
            //_graph.CreateRelationship(teamNode, teamToPlayerRelationship);

            //var agentToPlayerRelationship = new SitecoreRelationship(new NodeReference(playerNode.Id), "AgentFor");
            //_graph.CreateRelationship(agentNode, agentToPlayerRelationship);

        }
    }
}
