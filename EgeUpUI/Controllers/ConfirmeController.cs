using DataaccsessLayer.Table;
using EgeUpUI.Models.ConfirmMail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EgeUpUI.Controllers
{
    [Authorize]
    public class ConfirmeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            var value = TempData["Mail"];
            ViewBag.v = value;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModels confirmCodeDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmCodeDto.Mail);
            if (user.ConfirmeCode == confirmCodeDto.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Login");


            }
            return View();

        }
    }
   
}
