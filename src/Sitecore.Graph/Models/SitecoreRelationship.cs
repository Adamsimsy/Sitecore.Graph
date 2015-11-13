using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;

namespace Sitecore.Graph.Models
{
    public class SitecoreRelationship : Relationship, Neo4jClient.IRelationshipAllowingSourceNode<SitecoreNode>, Neo4jClient.IRelationshipAllowingTargetNode<SitecoreNode>
    {
        public SitecoreRelationship(NodeReference targetNode, string relationshipTypeKey) : base(targetNode)
        {
            TypeKey = relationshipTypeKey;
        }

        public static string TypeKey = "KNOWS";

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }
}
