using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class TrainingHistoryController : BaseController
    {
        IStringLocalizer<SharedResource> _localizer;
        private readonly IGenericService<HistoryBreif> _HistoryBreif;
        private readonly IGenericService<HistoryBerifImage> _HistoryBerifImage;

        private readonly IGenericService<TrainingSector> _trainingSectorService;

        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;


        public TrainingHistoryController(IStringLocalizer<SharedResource> localizer, IGenericService<HistoryBreif> HistoryBreif, IGenericService<HistoryBerifImage> HistoryBerifImage, IGenericService<TrainingSector> trainingSectorService, IMapper mapper)
        {
            _HistoryBreif = HistoryBreif;
            _HistoryBerifImage = HistoryBerifImage;
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
            var result = await _HistoryBreif.GetAllAsyncByEducationalFacilitiesId(false, 7, x => x.HistoryBerifImages);






            ViewData["Breadcrumb_MapPath"] = "معامل";
            ViewData["Breadcrumb_PageName"] = "TrainingHistory";
            ViewData["Breadcrumb_ActivePage"] = "معامل هندسية";

            var viewModelList = _mapper.Map<List<HistoryBreifVM>>(result);
            return View(viewModelList);




        }
    }
}
