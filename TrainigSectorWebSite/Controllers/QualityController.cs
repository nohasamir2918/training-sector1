using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class QualityController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public QualityController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Breadcrumb_MapPath"] = "معامل";
            ViewData["Breadcrumb_PageName"] = "Quality";
            ViewData["Breadcrumb_ActivePage"] = "معامل هندسية";
            return View();
        }
    }
}
