using Microsoft.AspNetCore.Mvc;

namespace AdminPanelPractice.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
