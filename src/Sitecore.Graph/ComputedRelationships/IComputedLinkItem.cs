using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.Graph.ComputedRelationships
{
    public interface IComputedLinkItem
    {
        IEnumerable<ItemLink> ComputeLinkItem(Item item);
    }
}
