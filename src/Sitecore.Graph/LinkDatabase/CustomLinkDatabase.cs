using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Diagnostics.PerformanceCounters;
using Sitecore.Globalization;
using Sitecore.Jobs;
using Sitecore.Links;
using Sitecore.SecurityModel;

namespace Sitecore.Graph.LinkDatabase
{
    public class CustomLinkDatabase : Links.LinkDatabase
    {
        private readonly ILinkDatabaseFactory _linkDatabaseFactory;

        public CustomLinkDatabase()
        {
            _linkDatabaseFactory = new LinkDatabaseFactory();
        }

        public override void Compact(Data.Database database)
        {
            _linkDatabaseFactory.GetContextLinkManager(database).Compact(database);
        }

        public override ItemLink[] GetBrokenLinks(Data.Database database)
        {
            return _linkDatabaseFactory.GetContextLinkManager(database).GetBrokenLinks(database);
        }

        public override int GetReferenceCount(Item item)
        {
            return _linkDatabaseFactory.GetContextLinkManager(item).GetReferenceCount(item);
        }

        public override ItemLink[] GetReferences(Item item)
        {
            return _linkDatabaseFactory.GetContextLinkManager(item).GetReferences(item);
        }

        public override int GetReferrerCount(Item item)
        {
            return _linkDatabaseFactory.GetContextLinkManager(item).GetReferrerCount(item);
        }

        public override ItemLink[] GetReferrers(Item item)
        {
            return _linkDatabaseFactory.GetContextLinkManager(item).GetReferrers(item);
        }

        public override ItemLink[] GetReferrers(Item item, bool deep)
        {
            return _linkDatabaseFactory.GetContextLinkManager(item).GetReferrers(item, deep);
        }

        public override void RemoveReferences(Item item)
        {
            _linkDatabaseFactory.GetContextLinkManager(item).RemoveReferences(item);
        }

        protected override void UpdateLinks(Item item, ItemLink[] links)
        {
            _linkDatabaseFactory.GetContextLinkManager(item).UpdateLinks(item, links);
        }
    }
}
