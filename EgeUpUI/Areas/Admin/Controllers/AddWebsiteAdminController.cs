using BussinesLayer.Abstrack;
using DataaccsessLayer.Table;
using EgeUpUI.Areas.Admin.Models.AddWEbModels;
using EgeUpUI.Dtos.AddWebsiteDto;
using EgeUpUI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EgeUpUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AddWebsiteAdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebsiteService _websiteService;
        private readonly IMailService _mailService;

        public AddWebsiteAdminController(UserManager<AppUser> userManager, IWebsiteService websiteService, IMailService mailService)
        {
            _userManager = userManager;
            _websiteService = websiteService;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {

            var websites =  _websiteService.TGetList();
            var pagedWebsites = await websites.ToPagedListAsync(pageNumber, pageSize);
            return View(pagedWebsites);

        }


        [HttpPost]
        public async Task<IActionResult> Index(WebAdd addWebDto)
        {
            //AddWebvALİDATOR validator = new AddWebvALİDATOR();
            //var validationResult = validator.Validate(addWebDto);

            //if (!validationResult.IsValid)
            //{
            //    foreach (var error in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    return BadRequest(ModelState);
            //}

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var companyIdClaim = user.Companyid.Value;
              
                    var website = new WebsiteUrl
                    {
                        WebsiteName = addWebDto.WebsiteName,
                        Email = user.Email,
                        Statues = addWebDto.Statues,

                        CompanyId = companyIdClaim,
                    };

                    try
                    {
                        _websiteService.TInsert(website);
                        return Json(new { isAdded = true });
                    }
                    catch (System.Exception ex)
                    {
                       
                        return Json(new { isAdded = false });
                    }


                    //return PartialView("_AddWebs", new WebAdd());
                }
            }

            return BadRequest(ModelState);
        }


        public IActionResult Add()
        {
            return PartialView("_AddWebs");

        }




    }
}
