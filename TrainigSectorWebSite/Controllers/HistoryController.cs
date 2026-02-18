using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TrainigSectorWebSite.Controllers
{
    public class HistoryController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;
        public HistoryController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Breadcrumb_MapPath"] = "معامل";
            ViewData["Breadcrumb_PageName"] = "History";
            ViewData["Breadcrumb_ActivePage"] = "معامل هندسية";
            return View();
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
