using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Graph.Relationships
{
    public interface IRelationshipManager
    {
       string GetRelationship(string subUri, string objUri);
    }
}
