using Microsoft.AspNetCore.Mvc;

namespace TrainigSectorWebSite.Controllers
{
    public class NewsDetailsController : Controller
    {
        public IActionResult Index(int Id=0)
        {
            return View();
        }
    }
}
