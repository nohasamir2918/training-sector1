using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.DataContext;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;
using TrainigSectorWebSite.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class DepartmentController : BaseController
    {

        private readonly IGenericService<Departmentsandbranch> _ProjectsService;

        private readonly TrainingSectorDbContext _context;

        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;

        public DepartmentController(TrainingSectorDbContext context, IStringLocalizer<SharedResource> localizer, IGenericService<Departmentsandbranch> ProjectsService, IMapper mapper, ILoggerRepository logger)
        {
            _ProjectsService = ProjectsService;

            _context = context;
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int Id = 0, int page = 1, int pageSize = 9)
        {

           
            var breadcrumb = await GetBreadcrumb(Id);

            SetBreadcrumb(
                mapPath: breadcrumb.MapPath,
                pageName: breadcrumb.PageName,
                activePage: breadcrumb.ActivePage
            );



  var projectsList = await _context.Departmentsandbranches
 .Where(x => x.Id == Id &&x.DepatmentTypeID==2)
 .Include(x=>x.DepartmentsandBranchesImages)
 .Include(x => x.Specializations)
     .ThenInclude(s => s.SpecializationImages)
 .ToListAsync();





  //          var projectsList = await _ProjectsService.GetManyAllAsyncByEducationalFacilitiesId(
  //    false,
  //    Id,
  //    q => q
          
  //        .Include(x => x.Specializations)
  //            .ThenInclude(s => s.SpecializationImages)   // 👈 هنا
  //);
         



            var vm = projectsList.Select(d => new DepartmentsandBranchesDetailVM
            {
                Id = d.Id,
                NameAr = d.NameAr,
                NameEn = d.NameEn,
                Specializations = d.Specializations.Select(s => new SpecializationVM
                {
                    Id = s.Id,
                    NameAr = s.NameAr,
                    NameEn = s.NameEn,
                    SpecializationImages = s.SpecializationImages
                        .Select(i => new SpecializationImageVM
                        {
                            Id = i.Id,
                            ImagePath = i.ImagePath
                        }).ToList()
                }).ToList()
            }).ToList();
          //  var viewModelList = _mapper.Map<List<DepartmentsandBranchesDetailVM>>(projectsList);



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


        public async Task<Breadcrumb> GetBreadcrumb(int id)
        {

            var Breadcrumb = await _ProjectsService.GetByIdAsync(

                    id,
                    x => x.EducationalFacilities
                );

            
            var obj= new TrainigSectorWebSite.ViewModel.Breadcrumb();
            var culture = Thread.CurrentThread.CurrentCulture.Name;

            obj.MapPath = LocalizationHelper.GetLocalized(
                Breadcrumb.EducationalFacilities.NameAr,
                Breadcrumb.EducationalFacilities.NameEn,
                culture
            );

            obj.ActivePage = LocalizationHelper.GetLocalized(
                Breadcrumb.NameAr,
                Breadcrumb.NameEn,
                culture
            );


          

            
            if (Breadcrumb.EducationalFacilitiesId == 2)
            {
                obj.PageName = _localizer["AdvancedTechnicalInstituteForIndustries"];
            }
            else if (Breadcrumb.EducationalFacilitiesId == 3)
            {
                obj.PageName = _localizer["ElSalamAppliedTechnologySecondarySchool"];
            }
            else if (Breadcrumb.EducationalFacilitiesId == 4)
            {
                obj.PageName = _localizer["HelwanSecondarySchoolForAppliedTechnology"];
            }
            else {
                obj.PageName = _localizer["TechnologicalCollege"];
            }

                return obj;

        }

    }
}
