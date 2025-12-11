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
    public class ProjectController : BaseController
    {
        

        private readonly IGenericService<ProjectVM> _ProjectsService;
        private readonly IGenericService<ProjectImageVM> _ProjectImagesService;

    
        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        public ProjectController(IStringLocalizer<SharedResource> localizer, IGenericService<ProjectVM> ProjectsService, IGenericService<ProjectImageVM> ProjectImagesService, IMapper mapper, ILoggerRepository logger)
        {
            _ProjectsService = ProjectsService;
            _ProjectImagesService = ProjectImagesService;
            
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<IActionResult> Index1(int page = 1, int pageSize = 9)
        {

            SetBreadcrumb(
          mapPath: _localizer["StudentServices"],
          pageName: _localizer["ExamSchedules"],
          activePage: _localizer["ExamSchedules"]);
            var ProjectsList = await _ProjectsService.GetAllAsync();
            var ProjectImagesList = await _ProjectImagesService.GetAllAsync();

  

            
            foreach (var item in ProjectsList)
            {
                if (ProjectImagesList.Where(a => a.ProjectsId == item.Id).ToList().Count > 0)
                {

                    item.ProjectImages = ProjectImagesList.Where(a => a.ProjectsId == item.Id).ToList();
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
