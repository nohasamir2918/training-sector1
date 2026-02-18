using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
        private readonly IGenericService<EntityImage> _EntityImageService;
        private readonly ILoggerRepository _logger;
        IStringLocalizer<SharedResource> _localizer;

        public NewsDetailsController(IStringLocalizer<SharedResource> localizer, IGenericService<News> News,
            IGenericService<EducationalFacility> educationalFacilityService, IMapper mapper, ILoggerRepository logger, IGenericService<EntityImage> EntityImageService)
        {
            _localizer = localizer;
            _News = News;
            _educationalFacilityService = educationalFacilityService;
            _mapper = mapper;
            _logger = logger;
            _EntityImageService = EntityImageService;
        }
        public async Task<IActionResult> Index(int Id=0)
        {
            SetBreadcrumb(
         mapPath: _localizer["News"],
         pageName: _localizer["NewsDetails"],
         activePage: _localizer["NewsDetails"]
);
          

            var project = await _EntityImageService.GetByIdAsync(Id);

            if (project == null)
                return NotFound();

            var viewModelList = _mapper.Map<NewsVM>(project);

            var projectImagesList = await _EntityImageService.FindAsync(
        x => x.EntityImagesTableTypeId == 2 && x.IsDeleted != true);


            if (projectImagesList.Where(a => a.EntityId == viewModelList.Id).ToList().Count > 0)
            {

                viewModelList.NewsImages = projectImagesList.Where(a => a.EntityId == viewModelList.Id).ToList();
            }





            return View(viewModelList);
        }
        private readonly string _basePath = @"D:\SharedStorageTrainigSector"; // Change to your folder


        public IActionResult GetImage(string fileName)
        {
            var fullPath = Path.Combine(_basePath, fileName);

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fullPath, out string contentType))
            {
                contentType = "application/octet-stream"; // default لو مش معروف
            }

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, contentType);
        }

    }
}
