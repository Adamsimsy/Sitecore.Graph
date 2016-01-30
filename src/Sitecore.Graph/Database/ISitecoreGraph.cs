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
        SitecoreNode ReadNode(string uri);
        SitecoreNode CreateNode(SitecoreNode node);
        RelationshipReference CreateRelationship(SitecoreNode sourceNode, SitecoreNode targetNode);
        IEnumerable<SitecoreNode> GetReferences(string v);
    }
}
