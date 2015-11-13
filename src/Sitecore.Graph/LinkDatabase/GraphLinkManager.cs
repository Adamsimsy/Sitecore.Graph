using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Graph.Database;
using Sitecore.Links;

namespace Sitecore.Graph.LinkDatabase
{
    internal class GraphLinkManager : ICustomLinkManager
    {
        private readonly ISitecoreGraph _graph;

        public GraphLinkManager(ISitecoreGraph graph)
        {
            _graph = graph;
        }

        public void Compact(Data.Database database)
        {
            throw new NotImplementedException();
        }

        public ItemLink[] GetBrokenLinks(Data.Database database)
        {
            throw new NotImplementedException();
        }

        public int GetReferenceCount(Item item)
        {
            throw new NotImplementedException();
        }

        public ItemLink[] GetReferences(Item item)
        {
            throw new NotImplementedException();
        }

        public int GetReferrerCount(Item item)
        {
            throw new NotImplementedException();
        }

        public ItemLink[] GetReferrers(Item item)
        {
            throw new NotImplementedException();
        }

        public ItemLink[] GetReferrers(Item item, bool deep)
        {
            throw new NotImplementedException();
        }

        public void RemoveReferences(Item item)
        {
            throw new NotImplementedException();
        }

        public void UpdateLinks(Item item, ItemLink[] links)
        {
            throw new NotImplementedException();
        }
    }
}