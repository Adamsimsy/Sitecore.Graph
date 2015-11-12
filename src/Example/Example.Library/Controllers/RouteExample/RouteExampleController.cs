using Example.Library.Services;
using System.Web.Mvc;

namespace Example.Library.Controllers.RouteExample
{
    public class RouteExampleController : Controller
    {
        private IExampleService _service;

        public RouteExampleController(IExampleService service)
        {
            _service = service;
        }

        public ActionResult Index(string id = "not specified")
        {
            return Json(_service.GetPage(id), JsonRequestBehavior.AllowGet);
        }
    }
}
