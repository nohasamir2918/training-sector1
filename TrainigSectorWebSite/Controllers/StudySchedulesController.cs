using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.Services;
using TrainigSectorDataEntry.ViewModel;



namespace TrainigSectorWebSite.Controllers
{
    public class StudySchedulesController : BaseController
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IGenericService<EducationalLevel> _EducationalLevelService;
        private readonly IGenericService<Term> _TermService;
        private readonly IGenericService<Specialization> _SpecializationService;
        private readonly IGenericService<StudentTablesAttachment> _StudentTablesAttachmentService;
        private readonly IGenericService<Departmentsandbranch> _DepartmentsService;
        private readonly IFileStorageService _fileStorageService;
        public StudySchedulesController(
            IStringLocalizer<SharedResource> localizer,
            IGenericService<EducationalLevel> educationalLevelService,
            IGenericService<Term> termService,
            IGenericService<Specialization> specializationService,
            IGenericService<StudentTablesAttachment> studentTablesAttachmentService,
            IGenericService<Departmentsandbranch> departmentsService, IFileStorageService fileStorageService)
        {
            _localizer = localizer;
            _EducationalLevelService = educationalLevelService;
            _TermService = termService;
            _SpecializationService = specializationService;
            _StudentTablesAttachmentService = studentTablesAttachmentService;
            _DepartmentsService = departmentsService;
            _fileStorageService = fileStorageService;
        }

        // Id = EducationalFacilitiesId
        public async Task<IActionResult> Index(int Id = 0)
        {
            if (Id == 1)
            {
                SetBreadcrumb(
                 mapPath: _localizer["LeadershipDevelopmentCenter"],
                 pageName: _localizer["StudySchedules"],
                 activePage: _localizer["StudySchedules"]);
            }
            else if (Id == 2)
            {
                SetBreadcrumb(
                mapPath: _localizer["AdvancedTechnicalInstituteForIndustries"],
                pageName: _localizer["StudySchedules"],
                activePage: _localizer["StudySchedules"]);
            }
            else if (Id == 3)
            {
                SetBreadcrumb(
                mapPath: _localizer["ElSalamAppliedTechnologySecondarySchool"],
                pageName: _localizer["StudySchedules"],
                activePage: _localizer["StudySchedules"]);

            }
            else if (Id == 4)
            {
                SetBreadcrumb(
                mapPath: _localizer["HelwanSecondarySchoolForAppliedTechnology"],
                pageName: _localizer["StudySchedules"],
                activePage: _localizer["StudySchedules"]);
            }
            else
            {
                SetBreadcrumb(
                mapPath: _localizer["TechnologicalCollege"],
                pageName: _localizer["StudySchedules"],
                activePage: _localizer["StudySchedules"]);
            }
            bool isInstitute = (Id == 2);
            ViewBag.IsInstitute = isInstitute;

            // السنة الدراسية (متفلترة حسب الجهة)
            var educationalLevels =
                await _EducationalLevelService.GetAllAsyncByEducationalFacilitiesId(
                    includeDeleted: false,
                    EducationalFacilitiesId: Id
                );

            ViewBag.EducationalLevelList =
                new SelectList(educationalLevels, "Id", "NameAr");

            // الترم
            ViewBag.TermList =
                new SelectList(await _TermService.GetDropdownListAsync(), "Id", "NameAr");

            // الأقسام
            var departments = await _DepartmentsService.GetByFilterAsync(x =>
                x.EducationalFacilitiesId == Id &&
                x.IsActive &&
                x.IsDeleted != true
            );

            ViewBag.Departments =
                new SelectList(departments, "Id", "NameAr");

            // التخصص
            ViewBag.Specialization =
                new SelectList(await _SpecializationService.GetDropdownListAsync(), "Id", "NameAr");

            return View(new StudyScheduleVM
            {
                EducationalFacilitiesId = Id
            });
        }



        //[HttpPost]
        //public async Task<IActionResult> DownloadSchedule(StudyScheduleVM model)
        //{


        //    var files = await _StudentTablesAttachmentService.GetByFilterAsync(x =>
        //        x.EducationalLevelId == model.EducationalLevelId &&
        //        x.TermsId == model.TermId &&
        //        x.DepartmentsandbranchesId == model.DepartmentsandbranchesId &&
        //        x.TableTypeId == 1 &&
        //        (
        //            model.SpecializationId == 0 ||     // مدرسة
        //            x.SpecializationId == model.SpecializationId // معهد
        //        ) &&
        //        x.IsActive &&
        //        x.IsDeleted != true
        //    );

        //    var attachment = files.FirstOrDefault();

        //    if (attachment == null || string.IsNullOrEmpty(attachment.FilePath))
        //        return NotFound("لا يوجد جدول مطابق للاختيارات");
        //    _fileStorageService.GetFileAsync()
        //    var fullPath = Path.Combine(
        //        Directory.GetCurrentDirectory(),
        //        "wwwroot",
        //        attachment.FilePath
        //    );



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadSchedule(StudyScheduleVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var fileEntity = await _StudentTablesAttachmentService.GetByFilterAsync(x =>
                x.EducationalLevelId == model.EducationalLevelId &&
                x.TermsId == model.TermId &&
                x.DepartmentsandbranchesId == model.DepartmentsandbranchesId &&
                (
                    model.SpecializationId == 0 ||
                    x.SpecializationId == model.SpecializationId
                ) &&
                x.IsActive &&
                x.IsDeleted != true
            );

            if (fileEntity == null || string.IsNullOrEmpty(fileEntity.FirstOrDefault().FilePath))
            {
                TempData["Error"] = "لا يوجد ملف مطابق للاختيارات";
                return RedirectToAction(nameof(Index));
            }

            //var fullPath = Path.Combine(
            //    Directory.GetCurrentDirectory(),
            //    "wwwroot",
            //    fileEntity.FilePath
            //);
            var fullPath = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot",
               ""
           );
            if (!System.IO.File.Exists(fullPath))
            {
                TempData["Error"] = "الملف غير موجود";
                return RedirectToAction(nameof(Index));
            }

            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

            return File(
                fileStream,
                "application/octet-stream",
                Path.GetFileName(fullPath)
            );
        }


        //    return File(
        //        System.IO.File.ReadAllBytes(fullPath),
        //        "application/octet-stream",
        //        Path.GetFileName(fullPath)
        //    );
        //}

        [HttpGet]
        public async Task<IActionResult> GetSpecializationsByDepartment(int departmentId)
        {
            var specializations = await _SpecializationService.GetByFilterAsync(x =>
                x.DepartmentsandbranchesId == departmentId &&
                x.IsActive &&
                x.IsDeleted != true
            );

            var result = specializations.Select(x => new
            {
                id = x.Id,
                name = x.NameAr
            });

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> CheckScheduleExists(
    int educationalLevelId,
    int termId,
    int departmentsandbranchesId,
    int specializationId = 0)
        {
            if (educationalLevelId == 0 || termId == 0 || departmentsandbranchesId == 0)
                return Json(new { exists = false });

            var files = await _StudentTablesAttachmentService.GetByFilterAsync(x =>
                x.EducationalLevelId == educationalLevelId &&
                x.TermsId == termId &&
                x.DepartmentsandbranchesId == departmentsandbranchesId &&
                (specializationId == 0 || x.SpecializationId == specializationId) &&
                x.TableTypeId == 1 &&
                x.IsActive &&
                x.IsDeleted != true
            );

            bool exists = files.Any(f => !string.IsNullOrEmpty(f.FilePath));

            return Json(new { exists });
        }

    }
}
