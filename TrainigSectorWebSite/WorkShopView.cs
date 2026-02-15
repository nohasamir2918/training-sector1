using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrainigSectorDataEntry.Interface;
using TrainigSectorDataEntry.Logging;
using TrainigSectorDataEntry.Models;
using TrainigSectorDataEntry.ViewModel;

namespace TrainigSectorWebSite
{
    public class WorkShopViewComponent : ViewComponent
    {
        private readonly IGenericService<Departmentsandbranch> _Departmentsandbranch;



        IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;
        public WorkShopViewComponent(IGenericService<Departmentsandbranch> Departmentsandbranch, IStringLocalizer<SharedResource> localizer, IMapper mapper, ILoggerRepository logger)
        {
            _Departmentsandbranch = Departmentsandbranch;
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;
        }

       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menus = await _Departmentsandbranch.GetAllAsyncByEducationalFacilitiesId(false, 7, x => x.DepartmentsandBranchesImages);
            var menusList = _mapper.Map<List<MenuVm>>(menus);
            return View(menusList);
        }

    }
}
