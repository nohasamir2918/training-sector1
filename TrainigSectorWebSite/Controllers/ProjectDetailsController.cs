using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class ProjectDetailsController : BaseController
    {

        private readonly IGenericService<Project> _ProjectsService;

        private readonly IGenericService<EntityImage> _EntityImageService;

        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        public ProjectDetailsController(IStringLocalizer<SharedResource> localizer, IGenericService<Project> ProjectsService, IMapper mapper, ILoggerRepository logger, IGenericService<EntityImage> EntityImageService)
        {
            _ProjectsService = ProjectsService;

            _EntityImageService = EntityImageService;
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<IActionResult> Index(int Id = 0)
        {
            if (Id == 1)
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
            else if (Id == 3)
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

            var projectsList = await _ProjectsService.GetByIdAsync(
                  
                  Id

              );

            var projectImagesList = await _EntityImageService.FindAsync(
          x => x.EntityImagesTableTypeId == 1 && x.IsDeleted != true);


            var project = await _ProjectsService.GetByIdAsync(Id);

            if (project == null)
                return NotFound();

            var viewModelList = _mapper.Map<ProjectVM>(project);


            
                if (projectImagesList.Where(a => a.EntityId == viewModelList.Id).ToList().Count > 0)
                {

                viewModelList.ProjectImages = projectImagesList.Where(a => a.EntityId == viewModelList.Id).ToList();
                }



           


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
