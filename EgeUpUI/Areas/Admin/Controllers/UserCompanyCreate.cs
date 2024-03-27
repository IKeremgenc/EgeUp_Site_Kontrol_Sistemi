using BussinesLayer.Abstrack;
using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using EgeUpUI.Areas.Admin.Models.DetayCompany;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EgeUpUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserCompanyCreate : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public UserCompanyCreate(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var companies = _context.Companies.ToList();
            var CompaniesList = await companies.ToPagedListAsync(pageNumber, pageSize);
            return View(CompaniesList);
            //return View(companies);
        }


        [HttpPost]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string aramaTerimi = "")
        {


            if (!string.IsNullOrEmpty(aramaTerimi))
            {
                var filtrelenmisKullanicilar = await _context.Companies.ToList()
                    .Where(k => k.CompanyName.ToLower().Contains(aramaTerimi.ToLower()))
                    .ToListAsync();

                var CompaniesList = filtrelenmisKullanicilar.ToPagedList(pageNumber, pageSize);
                return View(CompaniesList);
            }
            else
            {
                var companies = _context.Companies.ToList();
                var CompaniesList = await companies.ToPagedListAsync(pageNumber, pageSize);
                return View(CompaniesList);
            }



        }
        [HttpGet]
        public async Task<IActionResult> UpdateCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound(); 
            }
            var users = await _context.Users.Where(u => u.Companyid == id).ToListAsync();

            
            var viewModel = new DetayCompanyViewModel
            {
                CompanyId=company.Id,
                CompanyName = company.CompanyName,
                Status50=company.Statues50,
                Status100=company.Statues100,
                Statuesinfinty = company.Statuesinfinty,
                Company = company,
                Users = users
            };

            return View("UpdateCompany", viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateCompany(string selectedStatus, int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (company == null)
            {
                return NotFound();
            }

          
            switch (selectedStatus)
            {
                case "Statues50":
                    company.Statues50 = true;
                    company.Statues100 = false;
                    company.Statuesinfinty = false;
                    break;
                case "Statues100":
                    company.Statues50 = false;
                    company.Statues100 = true;
                    company.Statuesinfinty = false;
                    break;
                case "Statuesinfinty":
                    company.Statues50 = false;
                    company.Statues100 = false;
                    company.Statuesinfinty = true;
                    break;
            }

            _context.Companies.Update(company);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "UserCompanyCreate");
        }



        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int code;

                code = random.Next(100000, 1000000);

                int referenceCode = random.Next(100000, 999999);

                var newCompany = new Company
                {
                    CompanyName = company.CompanyName,
                    Statues = company.Statues=true,
                    ReferenceCode = referenceCode,
                    Statues50 = company.Statues50=true,
                    Statues100 = company.Statues100 = false,
                    Statuesinfinty = company.Statuesinfinty = false,
                };

                _context.Companies.Add(newCompany);
                _context.SaveChanges();

                return Json(new { success = true });
            }

            return View("Index", company);
        }
        [HttpPost]
        public IActionResult DeleteCompany(int companyId)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == companyId);
            if (company != null)
            {
                _context.Companies.Remove(company);
                _context.SaveChanges();
                return Ok("Şirket başarıyla silindi.");
            }
            else
            {
                return NotFound("Şirket bulunamadı.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> SendReferenceCode(int companyId, string email)
        {
            try
            {
                var company = _context.Companies.FirstOrDefault(c => c.Id == companyId);

                if (company != null)
                {
                    Random random = new Random();
                    int code = random.Next(100000, 1000000);

                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom_ = new MailboxAddress("EgeUp", "ismailkeremgenc@gmail.com");
                    MailboxAddress mailboxAddress = new MailboxAddress("User", email);

                    mimeMessage.From.Add(mailboxAddressFrom_);
                    mimeMessage.To.Add(mailboxAddress);
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için Referans kodunuzu kullanın: " + company.ReferenceCode;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    mimeMessage.Subject = "EgeUp Referans Kodunuz ";

                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        await smtpClient.ConnectAsync("smtp.gmail.com", 587, false);
                        await smtpClient.AuthenticateAsync("ismailkeremgenc@gmail.com", "tpqxqycotxvmimaq");
                        await smtpClient.SendAsync(mimeMessage);
                        await smtpClient.DisconnectAsync(true);
                    }

                    TempData["Mail"] = email;
                    return Ok("E-posta gönderildi!");
                }
                else
                {
                    return NotFound("Şirket bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "E-posta gönderilirken bir hata oluştu: " + ex.Message);
            }
        }
        public IActionResult companyadd()
        {
            return PartialView("_Company");
        }
    }
}
