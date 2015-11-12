using Example.Library.Services;
using System.Web.Mvc;

namespace Example.Library.Controllers.InjectionExample
{
    public class InjectionExampleController : Controller
    {
        private IExampleService _service;

        public InjectionExampleController(IExampleService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View("~/Views/InjectionExample/Index.cshtml", _service.GetPage());
        }
    }
}
