using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class ServicesController : BaseController
    {
        private readonly IGenericService<Service> _Services;
        private readonly IGenericService<EducationalFacility> _educationalFacilityService;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        IStringLocalizer<SharedResource> _localizer;

        public ServicesController(IStringLocalizer<SharedResource> localizer,IGenericService<Service> Services,
            IGenericService<EducationalFacility> educationalFacilityService, IMapper mapper, ILoggerRepository logger)
        {
            _localizer = localizer;
            _Services = Services;
            _educationalFacilityService = educationalFacilityService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            SetBreadcrumb(
         mapPath: _localizer["MainPage"],
         pageName: _localizer["StudentServices"],
         activePage: _localizer["StudentServices"]
);
            //var services= await _Services.GetAllAsync();



            var services = await _Services.GetAllAsync(
                    false,
                    x => x.EducationalFacilities
                );
            //foreach (var item in viewModelList)
            //{
            //    var obj = await _educationalFacilityService.GetByIdAsync(item.EducationalFacilitiesId);

            //    item.EducationalFacilitiesNameAr = obj.NameAr;
            //    item.EducationalFacilitiesNameEn = obj.NameEn;

            //}
            var viewModelList = _mapper.Map<List<ServiceVM>>(services);
            return View(viewModelList);

           
        }

        private readonly string _basePath = @"D:\"; // Change to your folder

        public IActionResult GetImage(string fileName)
        {
            var fullPath = Path.Combine(_basePath, fileName);

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            var contentType = "image/jpeg"; // Change if you have png/gif
            return File(fileBytes, contentType);
        }
    }
}
