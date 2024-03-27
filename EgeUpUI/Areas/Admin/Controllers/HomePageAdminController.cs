using BussinesLayer.Abstrack;
using DataaccsessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EgeUpUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class HomePageAdminController : Controller
    {
        private readonly Context _dbContext;
        private readonly IWebsiteService _websiteService;

        public HomePageAdminController(Context dbContext, IWebsiteService websiteService)
        {
            _dbContext = dbContext;
            _websiteService = websiteService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var totalWebsites = _dbContext.WebsiteUrls.Count();
            var totalUsers = _dbContext.Users.Count();
            var offlineWebsites = _dbContext.WebsiteUrls.Count(w => !w.Statues);
            var onlineWebsites = _dbContext.WebsiteUrls.Count(w => w.Statues);
            var totalcompany=_dbContext.Companies.Count();

            ViewBag.TotalWebsites = totalWebsites;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.OfflineWebsites = offlineWebsites;
            ViewBag.OnlineWebsites = onlineWebsites;
            ViewBag.totalcompany = totalcompany;

            var usersInSameCompany = _dbContext.Users.ToList();
        
            var pagedUser = await usersInSameCompany.ToPagedListAsync(pageNumber, pageSize);
            return View(pagedUser);

           
        }
    }
}
