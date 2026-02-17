using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    
    public class NewsDetailsController : BaseController
    {
        private readonly IGenericService<News> _News;
        private readonly IGenericService<EducationalFacility> _educationalFacilityService;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        IStringLocalizer<SharedResource> _localizer;

        public NewsDetailsController(IStringLocalizer<SharedResource> localizer, IGenericService<News> News,
            IGenericService<EducationalFacility> educationalFacilityService, IMapper mapper, ILoggerRepository logger)
        {
            _localizer = localizer;
            _News = News;
            _educationalFacilityService = educationalFacilityService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int Id=0)
        {
            SetBreadcrumb(
         mapPath: _localizer["News"],
         pageName: _localizer["NewsDetails"],
         activePage: _localizer["NewsDetails"]
);
            var NewsImages = await _News.GetByIdAsync(
                  
                    Id,
                    x => x.NewsImages
                );
            var viewModelList = _mapper.Map<NewsVM>(NewsImages);

            return View(viewModelList);
        }
        private readonly string _basePath = @"D:\"; // Change to your folder

        public IActionResult GetImage(string fileName)
        {
            var fullPath = @"D:\SharedStorageTrainigSector\" + fileName;

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            var contentType = "image/jpeg"; // Change if you have png/gif
            return File(fileBytes, contentType);
        }
    }
}
