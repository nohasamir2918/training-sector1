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
        public async Task<IActionResult> Index(int Id = 0, int page = 1, int pageSize = 9)
        {

            if (Id == 1)
            {
                SetBreadcrumb(
                 mapPath: _localizer["LeadershipDevelopmentCenter"],
                 pageName: _localizer["LabsWorkshops"],
                 activePage: _localizer["LabsWorkshops"]);
            }
            else if (Id == 2)
            {
                SetBreadcrumb(
                mapPath: _localizer["AdvancedTechnicalInstituteForIndustries"],
                pageName: _localizer["LabsWorkshops"],
                activePage: _localizer["LabsWorkshops"]);
            }
            else if (Id == 3)
            {
                SetBreadcrumb(
                mapPath: _localizer["ElSalamAppliedTechnologySecondarySchool"],
                pageName: _localizer["LabsWorkshops"],
                activePage: _localizer["LabsWorkshops"]);

            }
            else if (Id == 4)
            {
                SetBreadcrumb(
                mapPath: _localizer["HelwanSecondarySchoolForAppliedTechnology"],
                pageName: _localizer["LabsWorkshops"],
                activePage: _localizer["LabsWorkshops"]);
            }
            else
            {
                SetBreadcrumb(
                mapPath: _localizer["TechnologicalCollege"],
                pageName: _localizer["LabsWorkshops"],
                activePage: _localizer["LabsWorkshops"]);
            }


            var projectsList = await _context.Departmentsandbranches
 .Where(x => (x.DepatmentTypeID == 1|| x.DepatmentTypeID == 3))
 .Include(x => x.Specializations)
     .ThenInclude(s => s.SpecializationImages)
 .ToListAsync();



            var viewModelList = _mapper.Map<List<Departmentsandbranch>>(projectsList);



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
    }
}
