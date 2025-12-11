using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class TrainingHistoryController : BaseController
    {
        IStringLocalizer<SharedResource> _localizer;
        public TrainingHistoryController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
           SetBreadcrumb(
            mapPath: _localizer["MainPage"],
            pageName: _localizer["TrainingHistory"],
            activePage: _localizer["TrainingHistory"]
);
            return View();
        }
    }
}
