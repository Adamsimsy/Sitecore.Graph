using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Graph.Relationships
{
    public abstract class BaseRelationship
    {
        public String Name { get; set; }
        abstract public bool IsMatch(string objectCompareValue, string subjectCompareValue);
    }
}
