using System.Drawing.Printing;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.Services;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class RoomStageController : BaseController
    {
        private readonly IGenericService<StagesAndHall> _StagesAndHall;
        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger; 
        private readonly IGenericService<EntityImage> _EntityImageService;
        public RoomStageController(IStringLocalizer<SharedResource> localizer, IGenericService<StagesAndHall> StagesAndHall, IMapper mapper, ILoggerRepository logger, IGenericService<EntityImage> EntityImageService)
        {
            _localizer = localizer;
            _StagesAndHall = StagesAndHall;
            _mapper = mapper;
            _logger = logger;
            _EntityImageService = EntityImageService;
        }
        public async Task<IActionResult> Index(int Id=0,int page = 1, int pageSize = 9)
        {
  
            if (Id==1)
            {
                SetBreadcrumb(
         mapPath: _localizer["OurServices"],
         pageName: _localizer["Theater"],
         activePage: _localizer["Theater"]
);

                var StagesList = await _StagesAndHall.GetAllAsyncByEducationalFacilitiesId(false);
                var viewModelList = _mapper.Map<List<StagesAndHallVM>>(StagesList.Where(a=>a.ISStage==false));
                var projectImagesList = await _EntityImageService.FindAsync(
          x => x.EntityImagesTableTypeId == 3 && x.IsDeleted != true);

                foreach (var item in viewModelList)
                {
                    if (projectImagesList.Where(a => a.EntityId == item.Id).ToList().Count > 0)
                    {

                        item.HallsImages = projectImagesList.Where(a => a.EntityId == item.Id).ToList();
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
                ViewBag.title = _localizer["Theater"];
                return View(pagedData);
            }
            else if (Id == 2)
            {
                SetBreadcrumb(
 mapPath: _localizer["OurServices"],
 pageName: _localizer["Halls"],
 activePage: _localizer["Halls"]
);

                var StagesList = await _StagesAndHall.GetAllAsyncByEducationalFacilitiesId(false);
                var viewModelList = _mapper.Map<List<StagesAndHallVM>>(StagesList.Where(a => a.ISStage == true));
                var projectImagesList = await _EntityImageService.FindAsync(
          x => x.EntityImagesTableTypeId == 3 && x.IsDeleted != true);

                foreach (var item in viewModelList)
                {
                    if (projectImagesList.Where(a => a.EntityId == item.Id).ToList().Count > 0)
                    {

                        item.HallsImages = projectImagesList.Where(a => a.EntityId == item.Id).ToList();
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
                ViewBag.title = _localizer["Halls"];
                return View(pagedData);
            }



            return View();


        }

        private readonly string _basePath = @"D:\SharedStorageTrainigSector"; // Change to your folder


        public IActionResult GetImage(string fileName)
        {
            var fullPath = Path.Combine(_basePath, fileName).Replace("\\", "/");

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
