using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.Services;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class OpinionController : BaseController
    {
        IStringLocalizer<SharedResource> _localizer;
        private readonly IGenericService<ComplaintsAndSuggestion> _ComplaintsAndSuggestion;
        private readonly IFileStorageService _FileStorageService;
        public OpinionController(IGenericService<ComplaintsAndSuggestion> ComplaintsAndSuggestion, IStringLocalizer<SharedResource> localizer, IFileStorageService FileStorageService)
        {
            _ComplaintsAndSuggestion = ComplaintsAndSuggestion;
            _localizer = localizer;
            _FileStorageService = FileStorageService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            SetBreadcrumb(
          mapPath: _localizer["MainPage"],
          pageName: _localizer["ComplaintsSuggestions"],
          activePage: _localizer["ComplaintsSuggestions"]
);
            
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ComplaintsAndSuggestionVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Save file
            var folder = Path.Combine( "uploads/complaints");
            Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(model.UploadedFile.FileName);
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await model.UploadedFile.CopyToAsync(stream);
            string? imagePath = null;

            if (model.UploadedFile != null)
            {
                imagePath = await _FileStorageService
                    .UploadImageAsync(model.UploadedFile, "complaints");
            }



            var entity = new ComplaintsAndSuggestion
            {
                Name = model.Name,
                Telephone = model.Telephone,
                Email = model.Email,
                ComplaintText = model.ComplaintText,
                FilePath = "/uploads/complaints/" + fileName,
                IsActive = true,
                TrainigSectorId=1,
                UserCreationDate = DateOnly.FromDateTime(DateTime.Now)
            };

            await _ComplaintsAndSuggestion.AddAsync(entity);
            //await _context.SaveChangesAsync();

            TempData["Success"] = "تم إرسال رسالتك بنجاح";
            return RedirectToAction("Index","Opinion");
        }
    }
}
