using Example.Library.Models;
using Example.Library.Services;
using System.Web.Mvc;

namespace Example.Website.Controllers
{
    public class SitecoreExampleController : Controller
    {
        public SitecoreExampleController()
        {
        }

        // GET: Sitecore
        public ActionResult Index()
        {
            return View(new PageModel() { Title = "Hello from the controller" });
        }
    }
}