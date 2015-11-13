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

        public SitecoreGraph(IGraphClient graphClient)
        {
            _graphClient = graphClient;

            _graphClient.Connect();

        }

        public NodeReference<SitecoreNode> CreateNode(SitecoreNode node)
        {
            return _graphClient.Create(node);
        }

        public RelationshipReference CreateRelationship(NodeReference<SitecoreNode> nodeReference, SitecoreRelationship relationship)
        {
            return _graphClient.CreateRelationship(nodeReference, relationship);
        }
    }
}
