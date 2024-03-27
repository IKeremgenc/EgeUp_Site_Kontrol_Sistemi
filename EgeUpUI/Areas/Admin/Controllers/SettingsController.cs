using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using EgeUpUI.Dtos.UsereditDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EgeUpUI.Areas.Admin.Controllers
{
    [Area("Admin")]
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

            Useredit userEditViewModel = new Useredit();
            userEditViewModel.Name = user.Name;
            userEditViewModel.Surname = user.Surname;
            userEditViewModel.Username = user.UserName;
            userEditViewModel.Email = user.Email;

            ViewBag.UserData = userEditViewModel; 

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string Password, string ConfirmPassword)
        {
            if (Password == ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, Password);
                await _userManager.UpdateAsync(user);

                return RedirectToAction("Index", "Login");
            }
            return View();
        }


    }
}

