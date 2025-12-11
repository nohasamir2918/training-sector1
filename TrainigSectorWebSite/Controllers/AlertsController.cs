using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class AlertsController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public AlertsController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Breadcrumb_MapPath"] = "معامل";
            ViewData["Breadcrumb_PageName"] = "Alerts";
            ViewData["Breadcrumb_ActivePage"] = "معامل هندسية";
            return View();
        }
    }
}
