using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EgeUpUI.Controllers
{
    [Authorize]

    public class HomePageController : Controller
    {
        private readonly Context _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public HomePageController(Context dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var companyuser = await _userManager.GetUserAsync(User);
            int? companyId = companyuser.Companyid;
            if (companyuser != null)
            {

                if (companyId != null)
                {
                    var totalWebsites = _dbContext.WebsiteUrls
                        .Where(w => w.CompanyId == companyId)
                        .Count();

                    ViewBag.TotalWebsites = totalWebsites;
                }
            }




            if (companyId != null)
            {
                var offlineWebsites = _dbContext.WebsiteUrls.Where(w => w.CompanyId == companyId).Count(w => !w.Statues);
                ViewBag.OfflineWebsites = offlineWebsites;
            }
            if (companyId != null)
            {
                var onlineWebsites = _dbContext.WebsiteUrls.Where(w => w.CompanyId == companyId).Count(w => w.Statues);
                ViewBag.OnlineWebsites = onlineWebsites;
            }
            if (companyId != null)
            {
                var company = _dbContext.Companies.FirstOrDefault(c => c.Id == companyId);

                if (company != null)
                {
                    ViewBag.CompanyName = company.CompanyName;
                }
            }

            var usersInSameCompany = _dbContext.Users.Where(u => u.Companyid == companyId && u.Id != companyuser.Id).ToList();
            return View(usersInSameCompany);
        }
    }
}
