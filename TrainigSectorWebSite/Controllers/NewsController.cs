using System.Drawing.Printing;
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
    public class NewsController : BaseController
    {
        private readonly IGenericService<News> _newsService;
        private readonly IGenericService<NewsImage> _newsImagesService;

        private readonly IGenericService<TrainingSector> _trainingSectorService;
        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        public NewsController(IStringLocalizer<SharedResource> localizer, IGenericService<News> newsService, IGenericService<NewsImage> newsImagesService, IGenericService<TrainingSector> trainingSectorService, IMapper mapper, ILoggerRepository logger)
        {
            _newsService = newsService;
            _newsImagesService = newsImagesService;
            _trainingSectorService = trainingSectorService;
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 9)
        {
            SetBreadcrumb(
            mapPath: _localizer["MainPage"],
            pageName: _localizer["News"],
            activePage: _localizer["News"]
);
            var newsList = await _newsService.GetAllAsync();
            var newsImagesList = await _newsImagesService.GetAllAsync();

            var sectors = await _trainingSectorService.GetDropdownListAsync();

            ViewBag.TrainingSectorList = new SelectList(sectors, "Id", "NameAr");
            foreach (var item in newsList)
            {
                if (newsImagesList.Where(a => a.NewsId == item.Id).ToList().Count > 0)
                {

                    item.NewsImages = newsImagesList.Where(a => a.NewsId == item.Id).ToList();
                }
            }
            var viewModelList = _mapper.Map<List<NewsVM>>(newsList);

           // return View(viewModelList);


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

