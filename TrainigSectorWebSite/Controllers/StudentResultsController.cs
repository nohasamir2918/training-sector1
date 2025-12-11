using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class StudentResultsController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public StudentResultsController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Breadcrumb_MapPath"] = "معامل";
            ViewData["Breadcrumb_PageName"] = "WorkShopLap";
            ViewData["Breadcrumb_ActivePage"] = "معامل هندسية";
            return View();
        }
    }
}
