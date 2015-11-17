using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Sitecore.Graph.Models;

namespace Sitecore.Graph.Database
{
    public interface ISitecoreGraph
    {
        NodeReference<SitecoreNode> ReadNode(string uri);
        NodeReference<SitecoreNode> CreateNode(SitecoreNode node);
        RelationshipReference CreateRelationship(NodeReference<SitecoreNode> nodeReference, SitecoreRelationship relationship);
    }
}
