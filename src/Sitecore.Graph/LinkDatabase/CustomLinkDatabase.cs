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
        private readonly LockSet locks = new LockSet();
        private readonly ILinkDatabaseFactory _linkDatabaseFactory;

        public CustomLinkDatabase(string connectionString)
        {
            _linkDatabaseFactory = new LinkDatabaseFactory();
        }

        public override void Compact(Data.Database database)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                _linkDatabaseFactory.GetContextLinkManager(database).Compact(database);
            }
        }

        public override ItemLink[] GetBrokenLinks(Data.Database database)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                return _linkDatabaseFactory.GetContextLinkManager(database).GetBrokenLinks(database);
            }
        }

        public override int GetReferenceCount(Item item)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                return _linkDatabaseFactory.GetContextLinkManager(item).GetReferenceCount(item);
            }
        }

        public override ItemLink[] GetReferences(Item item)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                return _linkDatabaseFactory.GetContextLinkManager(item).GetReferences(item);
            }
        }

        public override int GetReferrerCount(Item item)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                return _linkDatabaseFactory.GetContextLinkManager(item).GetReferrerCount(item);
            }
        }

        public override ItemLink[] GetReferrers(Item item)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                return _linkDatabaseFactory.GetContextLinkManager(item).GetReferrers(item);
            }
        }

        public override ItemLink[] GetReferrers(Item item, ID sourceFieldId)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                return _linkDatabaseFactory.GetContextLinkManager(item).GetReferrers(item, sourceFieldId);
            }
        }

        public override ItemLink[] GetItemVersionReferrers(Item version)
        {
            Assert.ArgumentNotNull((object)version, "version");

            lock (this.locks.GetLock((object)"rdflock"))
            {
                return _linkDatabaseFactory.GetContextLinkManager(version).GetItemVersionReferrers(version);
            }
        }

        public override ItemLink[] GetReferrers(Item item, bool deep)
        {
            return this.GetReferrers(item);
        }

        public override void RemoveReferences(Item item)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                _linkDatabaseFactory.GetContextLinkManager(item).RemoveReferences(item);
            }
        }

        public override void UpdateItemVersionReferences(Item item)
        {
            Assert.ArgumentNotNull((object)item, "item");
            var allLinks = item.Links.GetAllLinks(false);
            this.UpdateItemVersionLink(item, allLinks);
        }

        protected override void AddLink(Item item, ItemLink link)
        {
            Assert.ArgumentNotNull((object)item, "item");
            Assert.ArgumentNotNull((object)link, "link");

            lock (this.locks.GetLock((object)"rdflock"))
            {

            }
        }

        protected virtual void RemoveLinks(Item item)
        {
            Assert.ArgumentNotNull((object)item, "item");

            this.RemoveItemVersionLinks(item);
        }

        protected virtual void RemoveItemVersionLinks(Item item)
        {
            Assert.ArgumentNotNull((object)item, "item");

            lock (this.locks.GetLock((object)"rdflock"))
            {
                _linkDatabaseFactory.GetContextLinkManager(item).RemoveItemVersionLinks(item);
            }
        }

        protected override void RemoveItemVersionLink(ItemLink itemLink)
        {
            Assert.ArgumentNotNull((object)itemLink, "itemLink");
            lock (this.locks.GetLock((object)"rdflock"))
            {
                _linkDatabaseFactory.GetContextLinkManager(itemLink.GetSourceItem()).RemoveItemVersionLink(itemLink);
            }

            //this.DataApi.Execute(" DELETE\r\n                      FROM {0}Links{1}\r\n                      WHERE {0}SourceItemID{1} = {2}itemID{3} \r\n                      AND {0}SourceLanguage{1} = {2}sourceLanguage{3} \r\n                      AND {0}SourceVersion{1}  = {2}sourceVersion{3}\r\n                      AND {0}SourceFieldID{1}  = {2}sourceFieldID{3}\r\n                      AND {0}TargetItemID{1}  = {2}targetItemID{3}\r\n                      AND {0}SourceDatabase{1} = {2}sourceDatabase{3}\r\n                      AND {0}TargetDatabase{1} = {2}targetDatabase{3}\r\n                      AND {0}TargetLanguage{1} = {2}targetLanguage{3}\r\n                      AND {0}TargetVersion{1} = {2}targetVersion{3}", (object)"itemID", (object)itemLink.SourceItemID.ToGuid(), (object)"sourceLanguage", (object)itemLink.SourceItemLanguage.ToString(), (object)"sourceVersion", (object)itemLink.SourceItemVersion.Number, (object)"sourceFieldID", (object)itemLink.SourceFieldID, (object)"targetItemID", (object)itemLink.TargetItemID, (object)"sourceDatabase", (object)this.GetString(itemLink.SourceDatabaseName, 50), (object)"targetDatabase", (object)this.GetString(itemLink.TargetDatabaseName, 50), (object)"targetLanguage", (object)itemLink.TargetItemLanguage.ToString(), (object)"targetVersion", (object)itemLink.TargetItemVersion.Number);
        }

        protected override void UpdateLinks(Item item, ItemLink[] links)
        {
            lock (this.locks.GetLock((object)"rdflock"))
            {
                _linkDatabaseFactory.GetContextLinkManager(item).UpdateLinks(item, links);
            }
        }

        protected override void UpdateItemVersionLinks(Item item, ItemLink[] links)
        {
            Assert.ArgumentNotNull((object)item, "item");
            Assert.ArgumentNotNull((object)links, "links");

            lock (this.locks.GetLock((object)"rdflock"))
            {
                _linkDatabaseFactory.GetContextLinkManager(item).UpdateItemVersionLinks(item, links);
            }
        }

        protected virtual void UpdateItemVersionLink(Item item, ItemLink[] contextitemLinks)
        {
            Assert.ArgumentNotNull((object)item, "item");
            Assert.ArgumentNotNull((object)contextitemLinks, "contextitemLinks");

            lock (this.locks.GetLock((object)"rdflock"))
            {
                _linkDatabaseFactory.GetContextLinkManager(item).UpdateItemVersionLink(item, contextitemLinks);
            }
        }
    }
}
