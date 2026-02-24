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
    public class CommunityContributionsController : BaseController
    {
        private readonly IGenericService<CommunityAndInternationalEngagement> _CommunityAndInternationalEngagement;
        private readonly IGenericService<EducationalFacility> _educationalFacilityService;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        IStringLocalizer<SharedResource> _localizer;

        public CommunityContributionsController(IStringLocalizer<SharedResource> localizer,IGenericService<CommunityAndInternationalEngagement> CommunityAndInternationalEngagement,
            IGenericService<EducationalFacility> educationalFacilityService, IMapper mapper, ILoggerRepository logger)
        {
            _localizer = localizer;
            _CommunityAndInternationalEngagement = CommunityAndInternationalEngagement;
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



            var services = await _CommunityAndInternationalEngagement.GetAllAsync(
                    false
                );
            //foreach (var item in viewModelList)
            //{
            //    var obj = await _educationalFacilityService.GetByIdAsync(item.EducationalFacilitiesId);

            //    item.EducationalFacilitiesNameAr = obj.NameAr;
            //    item.EducationalFacilitiesNameEn = obj.NameEn;

            //}
            var viewModelList = _mapper.Map<List<CommunityAndInternationalEngagementVm>>(services);
            return View(viewModelList);

           
        }

        private readonly string _basePath = @"D:\SharedStorageTrainigSector"; // Change to your folder

        public IActionResult GetImage(string fileName)
        {

            var fullPath = Path.Combine(_basePath, fileName).Replace("\\", "/");// @"D:\SharedStorageTrainigSector\" + fileName;

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            var contentType = "image/jpeg"; // Change if you have png/gif
            return File(fileBytes, contentType);
        }
    }
}
