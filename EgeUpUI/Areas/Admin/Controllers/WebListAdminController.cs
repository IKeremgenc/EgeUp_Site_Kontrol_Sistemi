using BussinesLayer.Abstrack;
using DataaccsessLayer.Abstract;
using DataaccsessLayer.Table;
using EgeUpUI.Areas.Admin.Models;
using EgeUpUI.Dtos.AddWebsiteDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EgeUpUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WebListAdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebsiteService _websiteService;
        private readonly IWebSiteDal _webSiteDal;
        private readonly IMailService _mailService;

        public WebListAdminController(UserManager<AppUser> userManager, IWebsiteService websiteService, IWebSiteDal webSiteDal, IMailService mailService)
        {
            _userManager = userManager;
            _websiteService = websiteService;
            _webSiteDal = webSiteDal;
            _mailService = mailService;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {

            var websites = _websiteService.TGetList();
            var pagedWebsites = await websites.ToPagedListAsync(pageNumber, pageSize);
            return View(pagedWebsites);

        }


        [HttpPost]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string aramaTerimi = "")
        {


            if (!string.IsNullOrEmpty(aramaTerimi))
            {
                var filtrelenmisKullanicilar = await _websiteService.TGetList()
                    .Where(k => k.WebsiteName.ToLower().Contains(aramaTerimi.ToLower()))
                    .ToListAsync();

                var pagedWebsites = filtrelenmisKullanicilar.ToPagedList(pageNumber, pageSize);
                return View(pagedWebsites);
            }
            else
            {
                var websites = await _websiteService.TGetPageList(pageNumber, pageSize);
                var pagedWebsites = await websites.ToPagedListAsync(pageNumber, pageSize);
                return View(pagedWebsites);
            }



        }

        [HttpGet]
        public IActionResult Detay(int id)
        {
            var mails = _mailService.TGetList().Where(m => m.WebsiteID == id).ToList();
            return PartialView("_Detay", mails);
        }
        [HttpPost]
        public IActionResult DetayDelete(int id)
        {
            var mailToDelete = _mailService.TGetByID(id);

            if (mailToDelete != null)
            {
                _mailService.TDelete(mailToDelete);

                var updatedMails = _mailService.TGetList().Where(m => m.WebsiteID == mailToDelete.WebsiteID).ToList();
                return PartialView("_Detay", updatedMails);
            }

            return NotFound();
        }






        public IActionResult Delet(int id)
        {
            var website = _websiteService.TGetByID(id);


            _websiteService.TDelete(website);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult createMail(int id)
        {
            var websiteToUpdate = _websiteService.TGetByID(id);


            var WebsiteListDto = new WebsiteListDto
            {
                WebsiteID = websiteToUpdate.WebsiteID,



            };


            ViewBag.WebsiteName = websiteToUpdate.WebsiteName;





            return PartialView("_SetMail", WebsiteListDto);


        }


        [HttpPost]
        public IActionResult createMail2(CreateMaillModal modal)
        {
            if (modal != null && !string.IsNullOrEmpty(modal.email) && modal.webSiteId != 0)
            {
                var newMail = new Mail
                {
                    Email = modal.email,
                    WebsiteID = modal.webSiteId
                };

                _mailService.TInsert(newMail);

                return RedirectToAction("Index", "WebListAdmin");
            }

            return View();

         
        }



        public IActionResult Add()
        {
            return PartialView("_SetMail");

        }
        public IActionResult DetayMail()
        {
            return PartialView("_Detay");

        }
    }
}