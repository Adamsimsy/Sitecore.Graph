using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Graph.Relationships
{
    public class RelationshipManager : IRelationshipManager
    {
        private readonly List<BaseRelationship> _relationships;

        public RelationshipManager(IRelationshipProvider relationshipProvider)
        {
            _relationships = relationshipProvider.GetRelationships();
        }

        public string GetRelationship(string subUri, string objUri)
        {
            var sourceItem = ItemHelper.UriToItem(subUri);
            var targetItem = ItemHelper.UriToItem(objUri);

            if (sourceItem != null && targetItem != null)
            {
                var sourceTemplateName = sourceItem.TemplateName;
                var targetTemplateName = targetItem.TemplateName;

                foreach (var baseRelationship in _relationships)
                {
                    if (baseRelationship.IsMatch(sourceTemplateName, targetTemplateName))
                    {
                        return baseRelationship.Name;
                    }
                }
            }

            return "LINKED_TO";
        }
    }
}
