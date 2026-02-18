namespace TrainigSectorWebSite.ViewModel
{
    using Microsoft.AspNetCore.Mvc;
    using TrainigSectorDataEntry.Interface;
    using TrainigSectorDataEntry.Models;

    public class SiteVisitorViewComponent : ViewComponent
    {
        private readonly IGenericService<TrainingSector> _trainingSectorService;

        public SiteVisitorViewComponent(
            IGenericService<TrainingSector> trainingSectorService)
        {
            _trainingSectorService = trainingSectorService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
         {
            const string sessionKey = "VisitorId";
             var sector = await _trainingSectorService.GetByIdAsync(1);
            // لو session = null → زائر جديد
            if (HttpContext.Session.GetString(sessionKey) == null)
            {
                HttpContext.Session.SetString(sessionKey, Guid.NewGuid().ToString());

                

                if (sector != null)
                {
                    sector.NumberOfSiteVisitor += 1;
                    await _trainingSectorService.UpdateAsync(sector);
                }
            }

            var totalUsers = sector.NumberOfSiteVisitor;

            return View(totalUsers);
        }
    }

}
