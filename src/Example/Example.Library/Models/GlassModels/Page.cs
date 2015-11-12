using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Library.Models.GlassModels
{
    public class Page
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Text { get; set; }
    }
}
