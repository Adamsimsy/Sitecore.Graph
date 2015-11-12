using Example.Library.Models;

namespace Example.Library.Services
{
    public interface IExampleService
    {
        PageModel GetPage();
        PageModel GetPage(string id);
    }
}
