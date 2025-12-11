using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class HistoeryMinisterController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public HistoeryMinisterController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Breadcrumb_MapPath"] = "مركز إعداد القادة";
            ViewData["Breadcrumb_PageName"] = "HistoeryMinister";
            ViewData["Breadcrumb_ActivePage"] = "نبذه تاريخية";
            //var title = _localizer["test"];
            return View();
        }
    }
}
