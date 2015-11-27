using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Graph.Database;
using Sitecore.Graph.Models;
using Sitecore.Links;

namespace Sitecore.Graph.LinkDatabase
{
    internal class GraphLinkManager : ICustomLinkManager
    {
        private readonly ISitecoreGraph _graph;

        public string Context { get; }

        public GraphLinkManager(string context)
        {
            _graph = new SitecoreGraph();
            Context = context;
        }

        public void Compact(Data.Database database)
        {
            //Do nothing as Graph database doesn't need compacting link MSSQL
        }

        public ItemLink[] GetBrokenLinks(Data.Database database)
        {
            return new ItemLink[] {};
        }

        public ItemLink[] GetItemVersionReferrers(Item version)
        {
            return new ItemLink[] {};
        }

        public int GetReferenceCount(Item item)
        {
            Assert.ArgumentNotNull((object)item, "item");

            //var items = _factory.GetContextLinkDatabaseDataManager(item).GetItemTriplesBySubject(item);

            //if (items != null)
            //{
            //    return items.Count();
            //}
            return 0;
        }

        public ItemLink[] GetReferences(Item item)
        {
            Assert.ArgumentNotNull((object)item, "item");
            List<ItemLink> list = new List<ItemLink>();


                //var items = _factory.GetContextLinkDatabaseDataManager(item).GetItemTriplesBySubject(item);

                //list = SitecoreTripleHelper.TriplesToItemLinks(items);

            return list.ToArray();
        }

        public int GetReferrerCount(Item item)
        {
            Assert.ArgumentNotNull((object)item, "item");

            //var items = _factory.GetContextLinkDatabaseDataManager(item).GetItemTriplesByObject(item);

            //if (items != null)
            //{
            //    return items.Count();
            //}
            return 0;
        }

        public ItemLink[] GetReferrers(Item item)
        {
            Assert.ArgumentNotNull((object)item, "item");
            List<ItemLink> list = new List<ItemLink>();

                //var items = _factory.GetContextLinkDatabaseDataManager(item).GetItemTriplesByObject(item);

                //list = SitecoreTripleHelper.TriplesToItemLinks(items);

            return list.ToArray();
        }

        public ItemLink[] GetReferrers(Item item, ID sourceFieldId)
        {
            return new ItemLink[] { };
        }

        public ItemLink[] GetReferrers(Item item, bool deep)
        {
            Assert.ArgumentNotNull((object)item, "item");
            //if (!deep)
            //    return this.GetReferrers(item);
            //else
            //    return this.GetHTMLReferersDeep(item);

            return new ItemLink[] {};
        }

        public void RemoveItemVersionLink(ItemLink itemLink)
        {
        }

        public void RemoveItemVersionLinks(Item item)
        {
        }

        public void RemoveReferences(Item item)
        {
            Assert.ArgumentNotNull((object)item, "item");

            //foreach (var manager in _factory.GetContextSitecoreLinkedDataManagers(item))
            //{
            //    var items = manager.GetItemTriplesByObject(item);

            //    manager.DeleteTriples(items);
            //}
        }

        public void UpdateItemVersionLink(Item item, ItemLink[] contextitemLinks)
        {
            contextitemLinks.ToList().ForEach(x => this.Update(item, x));
        }

        public void UpdateItemVersionLinks(Item item, ItemLink[] links)
        {
            links.ToList().ForEach(x => this.Update(item, x));
        }

        public void UpdateLinks(Item item, ItemLink[] links)
        {
            Assert.ArgumentNotNull((object)item, "item");
            Assert.ArgumentNotNull((object)links, "links");

            links.ToList().ForEach(x => this.Update(item, x));

            //var allLinks = _computedLinkManager.GetComputedLinkItems(item);

            //allLinks.AddRange(links);

            //foreach (var manager in _factory.GetContextSitecoreLinkedDataManagers(item))
            //{
            //    allLinks.ToList().ForEach(computedLink => manager.AddLink(item, computedLink));

            //    //Now remove removed links
            //    var oldLinks = GetReferences(item);
            //    var removeLinks = new List<ItemLink>();

            //    foreach (var link in oldLinks)
            //    {
            //        if (!allLinks.Where(x => x.TargetItemID.Guid == link.TargetItemID.Guid).Any())
            //        {
            //            removeLinks.Add(link);
            //        }
            //    }

            //    foreach (var removeLink in removeLinks)
            //    {
            //        manager.RemoveLinksForItem(item, removeLink);
            //    }
            //}
        }

        public void Update(Item item, ItemLink link)
        {
            var sourceNode = _graph.ReadNode(ItemHelper.ItemToUri(item));

            if (sourceNode == null)
            {
                sourceNode = _graph.CreateNode(new SitecoreNode() { Uri = ItemHelper.ItemToUri(item), Name = item.Name });
            }

            var targetItem = link.GetTargetItem();

            if (targetItem != null)
            {
                var targetNode = _graph.ReadNode(ItemHelper.ItemToUri(link.GetTargetItem()));

                if (targetNode == null)
                {
                    targetNode = _graph.CreateNode(new SitecoreNode() { Uri = ItemHelper.ItemToUri(targetItem), Name = targetItem.Name });
                }

                _graph.CreateRelationship(sourceNode, targetNode);
            }

            
        }
    }
}