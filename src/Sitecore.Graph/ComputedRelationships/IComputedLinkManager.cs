using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.Graph.ComputedRelationships
{
    public interface IComputedLinkManager
    {
        List<ItemLink> GetComputedLinkItems(Item item);
    }
}
