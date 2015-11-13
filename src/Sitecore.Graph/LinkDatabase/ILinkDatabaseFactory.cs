using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.Graph.LinkDatabase
{
    public interface ILinkDatabaseFactory
    {
        ICustomLinkManager GetContextLinkManager();
        ICustomLinkManager GetContextLinkManager(Data.Database database);
        ICustomLinkManager GetContextLinkManager(Item item);
    }
}
