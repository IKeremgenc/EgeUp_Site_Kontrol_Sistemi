using BussinesLayer.Abstrack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using DataaccsessLayer.Table;
using EgeUpUI.Dtos.WebMailDto;
using EgeUpUI.Dtos.AddWebsiteDto;
using Microsoft.AspNetCore.Identity;
using BussinesLayer.Concrete;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using X.PagedList;
using System.Threading.Tasks;
using EgeUpUI.Dtos;
using System;
using Microsoft.AspNetCore.Authorization;
using DataaccsessLayer.Abstract;
using Microsoft.AspNetCore.Http;

namespace EgeUpUI.Controllers
{
    [Authorize]

    public class WebListController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebsiteService _websiteService;
        private readonly IWebSiteDal _webSiteDal;
        private readonly IMailService _mailService;

        public WebListController(UserManager<AppUser> userManager, IWebsiteService websiteService, IWebSiteDal webSiteDal, IMailService mailService)
        {
            _userManager = userManager;
            _websiteService = websiteService;
            _webSiteDal = webSiteDal;
            _mailService = mailService;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null && user.Companyid != null)
            {
                var companyId = user.Companyid;
                var websites = await _websiteService.TGetPageList(pageNumber, pageSize, w => w.CompanyId == companyId);
                var pagedWebsites = await websites.ToPagedListAsync(pageNumber, pageSize);
                return View(pagedWebsites);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string aramaTerimi = "")
        {
            var kullanici = await _userManager.GetUserAsync(HttpContext.User);

            if (kullanici != null && kullanici.Companyid != null)
            {
                var sirketId = kullanici.Companyid;

                if (!string.IsNullOrEmpty(aramaTerimi))
                {
                    var filtrelenmisKullanicilar = await _websiteService.TGetList()
                        .Where(k => k.CompanyId == sirketId && k.WebsiteName.ToLower().Contains(aramaTerimi.ToLower()))
                        .ToListAsync();

                    var pagedWebsites = filtrelenmisKullanicilar.ToPagedList(pageNumber, pageSize);
                    return View(pagedWebsites);
                }
                else
                {
                    var websites = await _websiteService.TGetPageList(pageNumber, pageSize, w => w.CompanyId == sirketId);
                    var pagedWebsites = await websites.ToPagedListAsync(pageNumber, pageSize);
                    return View(pagedWebsites);
                }
            }

            return View();
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
            return RedirectToAction("Index", "WebList");

        }
        [HttpGet]
        public IActionResult Update(int id)
        {

            var websiteToUpdate = _websiteService.TGetByID(id);


            var WebsiteListDto = new WebsiteListDto
            {
                WebsiteID = websiteToUpdate.WebsiteID,



            };


            ViewBag.WebsiteName = websiteToUpdate.WebsiteName;



            return PartialView("SetMail", WebsiteListDto);



        }


        [HttpPost]
        public async Task<IActionResult> Update(WebsiteListDto model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Mails.Email) && model.WebsiteID != 0)
            {
                var newMail = new Mail
                {
                    Email = model.Mails.Email,
                    WebsiteID = model.WebsiteID
                };

                _mailService.TInsert(newMail);

                return RedirectToAction("Index");
            }

            return View();
        }




        public IActionResult Add()
        {
            return PartialView("SetMail");

        }
        public IActionResult DetayMail()
        {
            return PartialView("_Detay");

        }
    }
}
