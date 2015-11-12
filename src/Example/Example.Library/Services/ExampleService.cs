using Example.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Library.Services
{
    public class ExampleService : IExampleService
    {
        public PageModel GetPage()
        {
            return new PageModel() { Title = "Hello from the service" };
        }

        public PageModel GetPage(string id)
        {
            return new PageModel() { Title = "Hello from the service using id: " + id };
        }
    }
}
