using System.Drawing.Printing;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class NewsController : BaseController
    {
        private readonly IGenericService<News> _newsService;
        private readonly IGenericService<NewsImage> _newsImagesService;
        private readonly IGenericService<EntityImage> _EntityImageService;
        private readonly IGenericService<TrainingSector> _trainingSectorService;
        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        public NewsController(IStringLocalizer<SharedResource> localizer, IGenericService<News> newsService, IGenericService<NewsImage> newsImagesService, IGenericService<TrainingSector> trainingSectorService, IMapper mapper, 
            ILoggerRepository logger, IGenericService<EntityImage> EntityImageService)
        {
            _newsService = newsService;
            _newsImagesService = newsImagesService;
            _trainingSectorService = trainingSectorService;
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;
            _EntityImageService = EntityImageService;

        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 9)
        {
            SetBreadcrumb(
            mapPath: _localizer["MainPage"],
            pageName: _localizer["News"],
            activePage: _localizer["News"]
);
           
            var newsList = await _newsService.GetAllAsync(
            
         );
            var viewModelList = _mapper.Map<List<NewsVM>>(newsList);

            var projectImagesList = await _EntityImageService.FindAsync(
           x => x.EntityImagesTableTypeId == 2 && x.IsDeleted != true);

            foreach (var item in viewModelList)
            {
                if (projectImagesList.Where(a => a.EntityId == item.Id).ToList().Count > 0)
                {

                    item.NewsImages = projectImagesList.Where(a => a.EntityId == item.Id).ToList();
                }
            }


            // === PAGINATION ===
            int totalItems = viewModelList.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var pagedData = viewModelList
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(pagedData);

        }

        //public IActionResult Index()
        //{
        //    ViewData["Breadcrumb_MapPath"] = "معامل";
        //    ViewData["Breadcrumb_PageName"] = "News";
        //    ViewData["Breadcrumb_ActivePage"] = "معامل هندسية";
        //    return View();
        //}


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

