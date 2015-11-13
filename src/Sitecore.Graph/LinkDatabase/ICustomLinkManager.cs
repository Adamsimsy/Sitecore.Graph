using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.Graph.LinkDatabase
{
    public interface ICustomLinkManager
    {
        void Compact(Data.Database database);
        ItemLink[] GetBrokenLinks(Data.Database database);
        int GetReferenceCount(Item item);
        ItemLink[] GetReferences(Item item);
        int GetReferrerCount(Item item);
        ItemLink[] GetReferrers(Item item);
        ItemLink[] GetReferrers(Item item, bool deep);
        void RemoveReferences(Item item);
        void UpdateLinks(Item item, ItemLink[] links);
    }
}
