using System.Web.Mvc;

namespace Northwind.UI.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}