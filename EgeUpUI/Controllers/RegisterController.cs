using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using EgeUpUI.Dtos.RegisterDto;
using EgeUpUI.Validators;
using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace EgeUpUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RegisterController(Context context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateNewUSerDto creatNewUser)
        {

            var validator = new Creetnewuserdto();
            var validationResult = await validator.ValidateAsync(creatNewUser);
   
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(creatNewUser);
            }
            var existingUser = await _userManager.FindByEmailAsync(creatNewUser.Mail);
            if (existingUser != null)
            {
                ModelState.AddModelError("Mail", "Bu e-posta adresi zaten kullanılıyor.");
                return View(creatNewUser);
            }


            var existingCompany = await _context.Companies.FirstOrDefaultAsync(c => c.ReferenceCode == creatNewUser.ReferanceCode);
            if (existingCompany != null)
            {
                if (ModelState.IsValid)
                {

                    Random random = new Random();
                    int code;

                    code = random.Next(100000, 1000000);

                    //int referenceCode = random.Next(100000, 999999);
                    var appuser = new AppUser()

                    {

                        Name = creatNewUser.Name,
                        UserName = creatNewUser.Name,
                        Surname = creatNewUser.Name,
                        Email = creatNewUser.Mail,
                        Companyid = existingCompany.Id,
                        //ReferanceCode = referenceCode,


                        ConfirmeCode = code


                    };
                    //if (creatNewUser.ReferanceCode != null)
                    //{
                    //    var referencedUser = await _userManager.Users.FirstOrDefaultAsync(u => u.ReferanceCode == creatNewUser.ReferanceCode);

                    //    if (referencedUser != null)
                    //    {
                    //        appuser.ReferencedBy = referencedUser;
                    //        appuser.Companyid = referencedUser.Companyid;
                    //        appuser.ReferanceCode = referencedUser.ReferanceCode;
                    //    }
                    //}

                    var result = await _userManager.CreateAsync(appuser, creatNewUser.Password);
                    if (result.Succeeded)
                    {

                        MimeMessage mimeMessage = new MimeMessage();
                        MailboxAddress mailboxAddressFrom_ = new MailboxAddress("EgeMeet", "ismailkeremgenc@gmail.com");
                        MailboxAddress mailboxAddress = new MailboxAddress("User", appuser.Email);

                        mimeMessage.From.Add(mailboxAddressFrom_);
                        mimeMessage.To.Add(mailboxAddress);
                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz:" + code;
                        mimeMessage.Body = bodyBuilder.ToMessageBody();
                        mimeMessage.Subject = "EgeMeet Onay Kodu";
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Connect("smtp.gmail.com", 587, false);
                        smtpClient.Authenticate("ismailkeremgenc@gmail.com", "tpqxqycotxvmimaq");
                        smtpClient.Send(mimeMessage);
                        smtpClient.Disconnect(true);
                        TempData["Mail"] = creatNewUser.Mail;
                        await _userManager.AddToRoleAsync(appuser, "Musteri");
                        await _userManager.AddToRoleAsync(appuser, SystemRoles.MyEnum.Musteri.ToString());

                        return RedirectToAction("Index", "Confirme");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("Bu Kullanıcı Zaten Sİstemde var", item.Description);

                        }
                    }
                }



            }

            return View(creatNewUser);
        }
    }
}

