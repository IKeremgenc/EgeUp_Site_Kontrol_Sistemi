using DataaccsessLayer.Table;
using EgeUpUI.Areas.Admin.Models;

using EgeUpUI.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgeUpUI.Areas.AreaPerseonel.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class RoleUsingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleUsingController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> UssingnRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault();

            var allRoles = _roleManager.Roles.ToList();
           



            var viewModel = new UserRoleViewModel
            {
                UserId = id,
                UserRole = userRole,
         
            };
            ViewBag.OtherRoles = new SelectList(allRoles, "Name", "Name");
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateRole(int userId, string selectedRole)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

          

            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            await _userManager.AddToRoleAsync(user, selectedRole);

            return RedirectToAction("Index", "RoleUsing");

        }

    }
}
