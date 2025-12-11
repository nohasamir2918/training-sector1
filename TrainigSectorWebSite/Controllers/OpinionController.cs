using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class OpinionController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public OpinionController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Breadcrumb_MapPath"] = "معامل";
            ViewData["Breadcrumb_PageName"] = "Opinion";
            ViewData["Breadcrumb_ActivePage"] = "معامل هندسية";
            return View();
        }
    }
}
