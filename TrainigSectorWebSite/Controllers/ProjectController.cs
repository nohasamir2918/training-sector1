using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class ProjectController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public ProjectController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Breadcrumb_MapPath"] = "مركز إعداد القادة";
            ViewData["Breadcrumb_PageName"] = "project";
            ViewData["Breadcrumb_ActivePage"] = "المشروعات";
            return View();
        }
    }
}
