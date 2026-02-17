using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Models;
using TrainigSectorWebSite.Models;

namespace TrainigSectorWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericService<TrainingSector> _trainingSectorService;
        private readonly IGenericService<Slider> _sliderService;
        public HomeController(ILogger<HomeController> logger, IGenericService<TrainingSector> trainingSectorService, IGenericService<Slider> sliderService)
        {
            _logger = logger;
            _trainingSectorService = trainingSectorService;
            _sliderService = sliderService;
        }
        public IActionResult SetLanguage(string culture, string returnUrl = null)
        {





            if (string.IsNullOrEmpty(culture))
                culture = "ar";

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl ?? Url.Action("Index", "Home"));
        }

        public async Task<IActionResult> Index()
        {


            var sliders = await _sliderService
                .GetAllAsync();

            return View(sliders);
        }

        public IActionResult Privacy()
        {
            return View();
        }

      

       
    }
}
