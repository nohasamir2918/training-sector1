using Microsoft.AspNetCore.Mvc;
using TrainigSectorWebSite.ViewModel;

namespace TrainigSectorWebSite.Controllers
{
    public class BaseController : Controller
    {
        protected void SetBreadcrumb(string mapPath, string pageName, string activePage)
        {
            ViewData["Breadcrumb"] = new Breadcrumb
            {
                MapPath = mapPath,
                PageName = pageName,
                ActivePage = activePage
            };


            ViewData["Breadcrumb_MapPath"] = mapPath;
            ViewData["Breadcrumb_PageName"] = "معامل كيميائية";
            ViewData["Breadcrumb_ActivePage"] = "معامل كيميائية";
        }
    }

  
}
