using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class StudySchedulesController : BaseController
    {
        IStringLocalizer<SharedResource> _localizer;
        public StudySchedulesController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            SetBreadcrumb(
          mapPath: _localizer["StudentServices"],
          pageName: _localizer["ExamSchedules"],
          activePage: _localizer["ExamSchedules"]
 );
            return View();
        }
    }
}
