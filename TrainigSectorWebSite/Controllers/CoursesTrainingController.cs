using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class CoursesTrainingController : BaseController
    {
        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly IGenericService<TrainingCourse> _TrainingCourse;
        private readonly IGenericService<TrainingCoursesType> _TrainingCoursesType;
        public CoursesTrainingController(IStringLocalizer<SharedResource> localizer, IGenericService<TrainingCoursesType> trainingCoursesType, IGenericService<TrainingCourse> TrainingCourse, IMapper mapper)
        {
            _localizer = localizer;
            _TrainingCoursesType = trainingCoursesType;
            _TrainingCourse = TrainingCourse;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            SetBreadcrumb(
           mapPath: _localizer["MainPage"],
           pageName: _localizer["دورات تدريبية"],
           activePage: _localizer["دورات تدريبية"]
);



            var TrainingCoursesTypeResult = await _TrainingCoursesType.GetAllAsync();
            var viewModelList = _mapper.Map<List<TrainingCoursesTypeVM>>(TrainingCoursesTypeResult);
         
            ViewData["Breadcrumb_MapPath"] = "مركز إعداد القادة";
                ViewData["Breadcrumb_PageName"] = "الدورات التدريبية";
                ViewData["Breadcrumb_ActivePage"] = "الدورات التدريبية";



            return View(viewModelList);

        }
        [HttpGet]
        public async Task<IActionResult> GetCoursesByType(int typeId)
        {
            var courses = await _TrainingCourse.GetAllAsync();

            var result = courses
                .Where(x => x.TrainigCoursesTypesId == typeId )
                .Select((x, index) => new
                {
                    Index = index + 1,
                    Name = x.NameAr,
                    FilePath = x.FilePathAr
                });

            return Json(result);
        }


    }
}
