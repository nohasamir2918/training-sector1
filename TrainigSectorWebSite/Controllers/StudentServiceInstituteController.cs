using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;

namespace TrainigSectorWebSite.Controllers
{
    public class StudentServiceInstituteController : BaseController
    {
       

        IStringLocalizer<SharedResource> _localizer;
        public StudentServiceInstituteController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            
        }
        public IActionResult Index()
        {
            SetBreadcrumb(
         mapPath: _localizer["MainPage"],
         pageName: _localizer["StudentServices"],
         activePage: _localizer["StudentServices"]
);
            return View();
        }
    }
}
