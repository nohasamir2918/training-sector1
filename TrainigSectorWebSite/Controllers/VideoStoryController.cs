using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class VideoStoryController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public VideoStoryController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Breadcrumb_MapPath"] = "معامل";
            ViewData["Breadcrumb_PageName"] = "VideoStory";
            ViewData["Breadcrumb_ActivePage"] = "معامل هندسية";
            return View();
        }
    }
}
