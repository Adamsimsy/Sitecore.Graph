using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Graph
{
    public static class ItemHelper
    {
        public static Uri BrokenLinkUri = new Uri("sitecore://broken-link");

        public static string ItemToUri(Item item)
        {
            if (item != null && item.Uri != null)
            {
                return item.Uri.ToString().Replace("{", "%7B").Replace("}", "%7D");
            }
            return string.Empty;
        }

        public static Item UriToItem(string uri)
        {
            if (uri != null && !string.IsNullOrEmpty(uri)
                && uri.StartsWith("sitecore:")
                && !new Uri(uri).Equals(BrokenLinkUri))
            {
                var itemUri = new ItemUri(uri.Replace("%7B", "{").Replace("%7D", "}"));

                var database = Sitecore.Configuration.Factory.GetDatabase(itemUri.DatabaseName);

                return database.GetItem(itemUri.ItemID, itemUri.Language, itemUri.Version);
            }
            return null;
        }
    }
}
