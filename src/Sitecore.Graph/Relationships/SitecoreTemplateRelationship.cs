using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Graph.Relationships
{
    class SitecoreTemplateRelationship  : BaseRelationship
    {
        public string SubjectTemplateName { get; set; }
        public string ObjectTemplateName { get; set; }

        public override bool IsMatch(string subjectCompareValue, string objectCompareValue)
        {
            return subjectCompareValue.ToLower().Equals(SubjectTemplateName.ToLower())
                && (objectCompareValue.ToLower().Equals(ObjectTemplateName.ToLower()) || ObjectTemplateName.Equals("*"));
        }
    }
}
