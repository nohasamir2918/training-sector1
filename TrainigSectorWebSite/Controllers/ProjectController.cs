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
        

        private readonly IGenericService<Project> _ProjectsService;
      

    
        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        public ProjectController(IStringLocalizer<SharedResource> localizer, IGenericService<Project> ProjectsService, IMapper mapper, ILoggerRepository logger)
        {
            _ProjectsService = ProjectsService;
           
            
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<IActionResult> Index(int Id=0,int page = 1, int pageSize = 9)
        {
            if (Id==1)
            {
                SetBreadcrumb(
                 mapPath: _localizer["LeadershipDevelopmentCenter"],
                 pageName: _localizer["Projects"],
                 activePage: _localizer["Projects"]);
            }
            else if (Id == 2)
            {
                SetBreadcrumb(
                mapPath: _localizer["AdvancedTechnicalInstituteForIndustries"],
                pageName: _localizer["Projects"],
                activePage: _localizer["Projects"]);
            }
            else if (Id==3)
            {
                SetBreadcrumb(
                mapPath: _localizer["ElSalamAppliedTechnologySecondarySchool"],
                pageName: _localizer["Projects"],
                activePage: _localizer["Projects"]);
                
            }
            else if (Id == 4)
            {
                SetBreadcrumb(
                mapPath: _localizer["HelwanSecondarySchoolForAppliedTechnology"],
                pageName: _localizer["Projects"],
                activePage: _localizer["Projects"]);
            }
            else
            {
                SetBreadcrumb(
                mapPath: _localizer["TechnologicalCollege"],
                pageName: _localizer["Projects"],
                activePage: _localizer["Projects"]);
            }

                var projectsList = await _ProjectsService.GetAllAsyncByEducationalFacilitiesId(
                      false,
                      Id,
                      x => x.ProjectImages
                  );


            var viewModelList = _mapper.Map<List<ProjectVM>>(projectsList);

        

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
