using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class CoursesTrainingController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public CoursesTrainingController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            {
                ViewData["Breadcrumb_MapPath"] = "مركز إعداد القادة";
                ViewData["Breadcrumb_PageName"] = "الدورات التدريبية";
                ViewData["Breadcrumb_ActivePage"] = "الدورات التدريبية";
                return View();
            }
        }
    }
}
