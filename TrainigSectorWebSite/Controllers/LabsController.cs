using System.Drawing.Printing;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.DataContext;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class LabsController : BaseController
    {
        private readonly IGenericService<Departmentsandbranch> _Labs;

        private readonly TrainingSectorDbContext _context;

        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;

        public LabsController(TrainingSectorDbContext context, IStringLocalizer<SharedResource> localizer, IGenericService<Departmentsandbranch> Labs, IMapper mapper, ILoggerRepository logger)
        {
            _Labs = Labs;

            _context = context;
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int EducationFacility=0 ,int Id = 0, int page = 1, int pageSize = 9)
        {

           
            var departments = await _context.Departmentsandbranches
                .Include(x => x.DepartmentsandBranchesImages)
                .Where(x => x.IsActive && x.IsDeleted != true && x.Id==Id)
                .ToListAsync();
            
             if (EducationFacility == 7&& departments.FirstOrDefault().DepatmentTypeID==1)
            {
                SetBreadcrumb(
                mapPath: _localizer["TrainingFacilities"],
                pageName: _localizer["Labs"],
                activePage: _localizer["Labs"]);
            }
            else if (EducationFacility == 7 && departments.FirstOrDefault().DepatmentTypeID == 3)
            {
                SetBreadcrumb(
                mapPath: _localizer["TrainingFacilities"],
                pageName: _localizer["Workshops"],
                activePage: _localizer["Workshops"]);
            }
            var vm = _mapper.Map<List<DepartmentsandbranchVM>>(departments);
           



           



            // === PAGINATION ===
            int totalItems = vm.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var pagedData = vm
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
            .ToList();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(pagedData);

        }
        private readonly string _basePath = @"D:\SharedStorageTrainigSector\"; // Change to your folder

        public IActionResult GetImage(string fileName)
        {
            var fullPath = @"D:\" + fileName;

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            var contentType = "image/jpeg"; // Change if you have png/gif
            return File(fileBytes, contentType);
        }
    }
}
