using System.Web.Mvc;

namespace ApiSite.Controllers
{
    public class HomeController : Controller
    {
        [Route()]
        public ViewResult index()
        {
            return View();
        }
    }
}