using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using EgeUpUI.Dtos.UsereditDto;
using EgeUpUI.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EgeUpUI.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

      
        public SettingsController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            int? companyId = user.Companyid;
            int siteCount = _context.WebsiteUrls.Count(s => s.CompanyId == companyId);
            var company = _context.Companies.FirstOrDefault(c => c.Id == companyId);
            if (company != null)
            {
                if (company.Statues50)
                {
                    Useredit Useredit50 = new Useredit();
                    Useredit50.Name = user.Name;
                    Useredit50.SiteCount = siteCount;
                    Useredit50.Surname = user.Surname;
                    Useredit50.Username = user.UserName;
                    Useredit50.Statues50 = company.Statues50;
                    Useredit50.Companynames = user.Company.CompanyName;
                    Useredit50.Email = user.Email;
                    return View(Useredit50);
                }
                else if (company.Statues100)
                {
                    Useredit Useredit100 = new Useredit();
                    Useredit100.Name = user.Name;
                    Useredit100.SiteCount = siteCount;
                    Useredit100.Surname = user.Surname;
                    Useredit100.Username = user.UserName;
                    Useredit100.Statues100 = company.Statues100;
                    Useredit100.Companynames = user.Company.CompanyName;
                    Useredit100.Email = user.Email;
                    return View(Useredit100);
                }
                else if (company.Statuesinfinty)
                {
                    Useredit Useredit = new Useredit();
                    Useredit.Name = user.Name;
                    Useredit.SiteCount = siteCount;
                    Useredit.Surname = user.Surname;
                    Useredit.Username = user.UserName;
                    Useredit.Statuesinfinty = company.Statuesinfinty;
                    Useredit.Companynames = user.Company.CompanyName;
                    Useredit.Email = user.Email;
                    return View(Useredit);
                }
                else
                {
                    return BadRequest();
                }
            }
            return View();

            
        }
        [HttpPost]
        public async Task<IActionResult> Index(Useredit userEditViewModel)
        {
            UserEditValidator validator = new UserEditValidator();
            var validationResult = await validator.ValidateAsync(userEditViewModel);

            if (validationResult.IsValid)
            {
                if (userEditViewModel.Password == userEditViewModel.ConfirmPassword)
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    user.Name = userEditViewModel.Name;
                    user.Surname = userEditViewModel.Surname;
                    user.Email = userEditViewModel.Email;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditViewModel.Password);
                    await _userManager.UpdateAsync(user);

                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(userEditViewModel);
            }

            return View(userEditViewModel);
        }


    }
}
