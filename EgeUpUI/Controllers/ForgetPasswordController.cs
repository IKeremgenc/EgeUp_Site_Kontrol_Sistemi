using BussinesLayer.Concrete;
using DataaccsessLayer.Table;
using EgeUpUI.Dtos.Forget_Password;
using EgeUpUI.Dtos.Forget_Password.ReserPassword;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Threading.Tasks;

namespace EgeUpUI.Controllers
{
    [Authorize]
    public class ForgetPasswordController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ForgetPasswordController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDtos forgetPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgetPassword.Email);
            string paswordresertoke = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordresetTokenLink = Url.Action("ResetPassword", "ForgetPassword", new
            {
                userId = user.Id,
                token = paswordresertoke
            }, HttpContext.Request.Scheme);
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom_ = new MailboxAddress("EgeMeet", "ismailkeremgenc@gmail.com");
            MailboxAddress mailboxAddress = new MailboxAddress("User", forgetPassword.Email);

            mimeMessage.From.Add(mailboxAddressFrom_);
            mimeMessage.To.Add(mailboxAddress);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = " Şifrenizi Yenilemek için Linke Tıklayın.): " + passwordresetTokenLink;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "EgeUp Şifre Yenileme ";

            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("ismailkeremgenc@gmail.com", "tpqxqycotxvmimaq");
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public IActionResult ResetPassword(string userid,string token)
        {
           TempData["userid"] = userid;
           TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task< IActionResult >ResetPassword(ResetPasswordDto resetPasswordDto)
        {
           var userid = TempData["userid"];
           var token = TempData["token"];
            if (userid == null || token==null) 
            { 
            //Hata Mesajı Eklicem
            
            }
            var user = await _userManager.FindByIdAsync(userid.ToString());
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordDto.password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
