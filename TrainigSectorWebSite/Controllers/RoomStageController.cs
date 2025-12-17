using System.Drawing.Printing;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public RoomStageController(IStringLocalizer<SharedResource> localizer, IGenericService<StagesAndHall> StagesAndHall, IMapper mapper, ILoggerRepository logger)
        {
            _localizer = localizer;
            _StagesAndHall = StagesAndHall;
            _mapper = mapper;
            _logger = logger;
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

                var StagesList = await _StagesAndHall.GetAllAsync(
            false,
            x => x.Equals(null)
        );
                var viewModelList = _mapper.Map<List<StagesAndHallVM>>(StagesList.Where(a=>a.ISStage==false));

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
                ViewBag.title = _localizer["Threater"];
                return View(pagedData);
            }
            else if (Id == 2)
            {
                SetBreadcrumb(
 mapPath: _localizer["OurServices"],
 pageName: _localizer["Halls"],
 activePage: _localizer["Halls"]
);

                var StagesList = await _StagesAndHall.GetAllAsync(
            false,
            x => x.Equals(null)
        );
                var viewModelList = _mapper.Map<List<StagesAndHallVM>>(StagesList.Where(a => a.ISStage == true));

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
