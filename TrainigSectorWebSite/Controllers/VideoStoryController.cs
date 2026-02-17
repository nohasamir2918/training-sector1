using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class VideoStoryController : BaseController
    {
        IStringLocalizer<SharedResource> _localizer;
        private readonly IGenericService<SucessStory> _SucessStory;


        private readonly IGenericService<TrainingSector> _trainingSectorService;

        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;


        public VideoStoryController(IStringLocalizer<SharedResource> localizer, IGenericService<SucessStory> SucessStory, IGenericService<TrainingSector> trainingSectorService, IMapper mapper)
        {
            _SucessStory = SucessStory;

            _trainingSectorService = trainingSectorService;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int ID)
        {
            SetBreadcrumb(
            mapPath: _localizer["MainPage"],
            pageName: _localizer["TrainingHistory"],
            activePage: _localizer["TrainingHistory"]
);
            var result = await _SucessStory.GetAllAsyncByEducationalFacilitiesId(false, ID);








            var viewModelList = _mapper.Map<List<SucessStoryVM>>(result);
            return View(viewModelList);




        }
        private readonly string _basePath = @"D:\"; // Change to your folder

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
