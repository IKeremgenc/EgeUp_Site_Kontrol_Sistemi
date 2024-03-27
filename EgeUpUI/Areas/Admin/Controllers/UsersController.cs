using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using EgeUpUI.Areas.Admin.Models.DetayCompany;
using EgeUpUI.Dtos.CompanyToUserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EgeUpUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;


        public UsersController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

    
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchString="" )
        {
            var users = from u in _userManager.Users
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString));
            }

            var filteredUsers = await users.Include(u => u.Company).ToListAsync();
            var pageduser = await filteredUsers.ToPagedListAsync(pageNumber, pageSize);
            return View(pageduser);

            //return View(filteredUsers);
        }


        [HttpGet]
        public async Task<IActionResult> AddCompanyToUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var allCompanies = await _context.Companies.ToListAsync();
            var addModel = new AddCompanyToUser();
            addModel.Companies = allCompanies;
            addModel.UserId = user.Id;

          
           
            var company = await _context.Companies
                .Where(c => c.Id == user.Companyid)
                .FirstOrDefaultAsync();
            if (company!=null)
            {
                addModel.CompanyId = company.Id;
                ViewBag.UserName = $"{user.Name} {user.Surname}";
                ViewBag.UserId = user.Id;
                return View(addModel);

            }
         
            ViewBag.UserName = $"{user.Name} {user.Surname}";
            ViewBag.UserId = user.Id;
            return View(addModel); 
        }

      

        [HttpPost]
        public async Task<IActionResult> AddCompanyToUser(AddCompanyToUser model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            var allCompanies = await _context.Companies.ToListAsync();
            model.Companies = allCompanies;

           
      
            user.Companyid = model.CompanyId;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index"); 
            }
            else
            {
              
                return BadRequest(); 
            }
        }
       

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id) 
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return BadRequest();
        }




    }
}

