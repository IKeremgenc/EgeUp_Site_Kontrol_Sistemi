using BussinesLayer.Abstrack;
using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using EgeUpUI.Dtos.AddWebsiteDto;
using EgeUpUI.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EgeUpUI.Controllers
{
  [Authorize]
    public class AddWebsiteController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebsiteService _websiteService;
        private readonly IMailService _mailService;

        public AddWebsiteController(Context context, UserManager<AppUser> userManager, IWebsiteService websiteService, IMailService mailService)
        {
            _context = context;
            _userManager = userManager;
            _websiteService = websiteService;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.Companyid != null)
            {
                var companyId = user.Companyid;

                var websitestrue = await _websiteService.TGetPageList(pageNumber, pageSize, w => w.CompanyId == companyId);
                var pagedWebsitestrue = await websitestrue.ToPagedListAsync(pageNumber, pageSize);
                return View(pagedWebsitestrue);

            }
         return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(AddWebDto addWebDto)
        {
          

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var companyIdClaim = user.Companyid.Value;

                    var siteCount = await _context.WebsiteUrls
               .Where(x => x.CompanyId == companyIdClaim)
               .CountAsync();



                    var company50 = await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyIdClaim && c.Statues50 == true); 
                    var company100 = await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyIdClaim && c.Statues100 == true); 
                    var companyinfinty = await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyIdClaim && c.Statuesinfinty == true);
                    if (company50 != null)
                    {
                        if (siteCount >= 50)
                        {
                            ModelState.AddModelError("Hata", "Maksimum site sınırına ulaşıldı.");
                            var errorMessage = ModelState["Hata"].Errors.FirstOrDefault()?.ErrorMessage;
                            return Json(new { success = false, message = errorMessage });
                        }
                        else
                        {
                            var website50 = new WebsiteUrl
                            {
                                WebsiteName = addWebDto.WebsiteName,
                                Email = user.Email,
                                Statues = addWebDto.Statues=true,

                                CompanyId = companyIdClaim,
                            };
                            _websiteService.TInsert(website50);
                            await _context.SaveChangesAsync();

                            return Json(new { success = true });
                           

                        }
                    }
                    else if (company100 != null)
                    {
                        if (siteCount >= 100)
                        {
                            ModelState.AddModelError("Hata", "Maksimum site sınırına ulaşıldı.");
                            var errorMessage = ModelState["Hata"].Errors.FirstOrDefault()?.ErrorMessage;
                            return Json(new { success = false, message = errorMessage });
                        }

                        var website100 = new WebsiteUrl
                        {
                            WebsiteName = addWebDto.WebsiteName,
                            Email = user.Email,
                            Statues = addWebDto.Statues,

                            CompanyId = companyIdClaim,
                        };


                        _websiteService.TInsert(website100);
                        await _context.SaveChangesAsync();


                        return PartialView("_AddOrUpdateWebSite", new AddWebDto());

                    }
                    else if (companyinfinty != null)
                    {

                        var website = new WebsiteUrl
                        {
                            WebsiteName = addWebDto.WebsiteName,
                            Email = user.Email,
                            Statues = addWebDto.Statues,

                            CompanyId = companyIdClaim,
                        };


                        _websiteService.TInsert(website);
                        await _context.SaveChangesAsync();


                        return PartialView("_AddOrUpdateWebSite", new AddWebDto());
                    }

                }
            }

            return BadRequest(ModelState);
        }


        public async Task<IActionResult> Add()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewBag.CompanyId = user.Companyid;
            }

            return PartialView("_AddOrUpdateWebSite");
        }





    }

}


