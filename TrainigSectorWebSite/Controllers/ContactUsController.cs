using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class ContactUsController : BaseController
    {
       


        IStringLocalizer<SharedResource> _localizer;
        private readonly IGenericService<ContactU> _ContactU;


        private readonly IGenericService<TrainingSector> _trainingSectorService;

        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;


        public ContactUsController(IStringLocalizer<SharedResource> localizer, IGenericService<ContactU> ContactU, IGenericService<TrainingSector> trainingSectorService, IMapper mapper)
        {
            _ContactU = ContactU;

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
            var result = await _ContactU.GetAllAsync(false);








            var viewModelList = _mapper.Map<List<ContactUVM>>(result);
            return View(viewModelList);




        }



    }
}
