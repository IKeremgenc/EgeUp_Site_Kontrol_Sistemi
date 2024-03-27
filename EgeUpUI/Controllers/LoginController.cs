using DataaccsessLayer.Table;
using EgeUpUI.Dtos.LoginDto;
using EgeUpUI.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EgeUpUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;


        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginDto model)
        {
            var validationResult = await new LoginValidator().ValidateAsync(model);
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if (user.EmailConfirmed)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);



                        if (userRoles.Contains("Musteri"))
                        {
                            return RedirectToAction("Index", "HomePage", new { area = "Musteri" });
                        }
                     
                        else if (userRoles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "HomePageAdmin", new { area = "Admin" });
                        }

                    }
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Hatalı kullanıcı adı veya şifre.");
                    return View(model);
                }
            }
                return View();

    }
}
}

