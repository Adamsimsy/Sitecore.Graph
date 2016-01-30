using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Graph.ComputedRelationships;
using Sitecore.Graph.Database;
using Sitecore.Graph.Models;
using Sitecore.Graph.Relationships;
using Sitecore.Links;

namespace Sitecore.Graph.LinkDatabase
{
    internal class GraphLinkManager : ICustomLinkManager
    {
        private readonly ISitecoreGraph _graph;
        private readonly IRelationshipManager _relationshipManager;
        private readonly IComputedLinkManager _computedLinkManager;

        public string Context { get; }

        public GraphLinkManager(string context)
        {
            _graph = new SitecoreGraph();

            var computedLinkItems = new List<IComputedLinkItem>();

            computedLinkItems.Add(new AncestorComputedLinkItem("team", "ground"));
            computedLinkItems.Add(new DescendantComputedLinkItem("staff", "team"));

            _computedLinkManager = new SitecoreComputedLinkManager(computedLinkItems);

            List<BaseRelationship> relationships = new List<BaseRelationship>();

            relationships.Add(new SitecoreTemplateRelationship() { SubjectTemplateName = "league", Name = "league_to_team", ObjectTemplateName = "team" });
            relationships.Add(new SitecoreTemplateRelationship() { SubjectTemplateName = "team", Name = "team_to_player", ObjectTemplateName = "player" });
            relationships.Add(new SitecoreTemplateRelationship() { SubjectTemplateName = "newsstory", Name = "news_to_item", ObjectTemplateName = "*" });
            relationships.Add(new SitecoreTemplateRelationship() { SubjectTemplateName = "ground", Name = "home_of_team", ObjectTemplateName = "team" });
            relationships.Add(new SitecoreTemplateRelationship() { SubjectTemplateName = "team", Name = "team_staff", ObjectTemplateName = "staff" });
            
            IRelationshipProvider relationshipProvider = new RelationshipProvider(relationships);

            _relationshipManager = new RelationshipManager(relationshipProvider);

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

            IEnumerable<SitecoreNode> references = _graph.GetReferences(ItemHelper.ItemToUri(item));
            
            //list.Add(new ItemLink(item, ));

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
            this.UpdateLinks(item, contextitemLinks);
        }

        public void UpdateItemVersionLinks(Item item, ItemLink[] links)
        {
            this.UpdateLinks(item, links);
        }

        public void UpdateLinks(Item item, ItemLink[] links)
        {
            Assert.ArgumentNotNull((object)item, "item");
            Assert.ArgumentNotNull((object)links, "links");

            var oldLinks = GetReferences(item);

            var removeLinks = new List<ItemLink>();

            foreach (var link in oldLinks)
            {
                if (!links.Any(x => x.TargetItemID.Guid == link.TargetItemID.Guid))
                {
                    removeLinks.Add(link);
                }
            }

            removeLinks.ForEach(x => RemoveItemVersionLink(x));

            var allLinks = _computedLinkManager.GetComputedLinkItems(item);

            allLinks.AddRange(links);

            allLinks.ToList().ForEach(x => this.Update(item, x));

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
            if (FilterItem(item))
            {
                return;
            }

            var targetItem = link.GetTargetItem();

            if (targetItem != null)
            {
                if (FilterItem(targetItem))
                {
                    return;
                }

                var sourceNode = _graph.ReadNode(ItemHelper.ItemToUri(item));

                if (sourceNode == null)
                {
                    sourceNode = _graph.CreateNode(new SitecoreNode() { Uri = ItemHelper.ItemToUri(item), Name = item.Name });
                }

                var targetNode = _graph.ReadNode(ItemHelper.ItemToUri(link.GetTargetItem()));

                if (targetNode == null)
                {
                    targetNode = _graph.CreateNode(new SitecoreNode() { Uri = ItemHelper.ItemToUri(targetItem), Name = targetItem.Name });
                }

                var relationship = _relationshipManager.GetRelationship(sourceNode.Uri, targetNode.Uri);

                _graph.CreateRelationship(sourceNode, relationship, targetNode);
            }

            
        }

        private bool FilterItem(Item item)
        {
            //Should add functionality to add configure filters. For now hard code so only index stuff under content.
            return !item.Paths.FullPath.ToLower().StartsWith("/sitecore/content/home");
        }
    }
}