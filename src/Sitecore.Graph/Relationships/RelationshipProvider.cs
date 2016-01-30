using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Graph.Relationships
{
    public class RelationshipProvider : IRelationshipProvider
    {
        private readonly List<BaseRelationship> _replationships;

        public RelationshipProvider()
        {
            _replationships = new List<BaseRelationship>();
        }

        public RelationshipProvider(List<BaseRelationship> relationships)
        {
            _replationships = relationships;
        }

        public List<BaseRelationship> GetRelationships()
        {

            return _replationships;
        }
    }
}
