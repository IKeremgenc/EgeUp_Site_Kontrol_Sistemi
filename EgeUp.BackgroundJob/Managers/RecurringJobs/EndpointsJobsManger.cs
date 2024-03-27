using BussinesLayer.Abstrack;
using BussinesLayer.Concrete;
using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EgeUp.BackgroundJob.Managers.RecurringJobs
{
    public class EndpointsJobsManger
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebsiteService _websiteService;
        private readonly Context _context;

        public EndpointsJobsManger(UserManager<AppUser> userManager, IWebsiteService websiteService, Context context)
        {
            _userManager = userManager;
            _websiteService = websiteService;
            _context = context;
        }

        public async Task CheckWebsiteStatus()
        {
            List<WebsiteUrl> websites = await _context.WebsiteUrls
                                         .Where(w => w.Statues == true && w.FailReports == false) 
                                         .ToListAsync();


            foreach (var website in websites)
            {
                bool isWebsiteUp = await ServerCheck(website.WebsiteName);

                website.Statues = isWebsiteUp;

                _context.WebsiteUrls.Update(website);

                if (isWebsiteUp==false)
                {
                    website.wevsitedown = true; 
                    await SendFailReports(website);
                    website.FailReports = true;

                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> ServerCheck(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                    HttpResponseMessage response = await client.SendAsync(request);

                   
                    if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.BadGateway)
                    {
                        return false; 
                    }

                    return true; 
                }
            }
            catch (Exception)
            {
                return false; 
            }
        }

       
        public async Task SendFailReports(WebsiteUrl website)
        {
            var users = await _context.Mails.Where(u => u.WebsiteID == website.WebsiteID).ToListAsync();
            var websites = await _context.WebsiteUrls.Where(u => u.WebsiteID == website.WebsiteID).ToListAsync();

            foreach (var user in users)
            {
                await SendFailEmail(user.Email, website.WebsiteName);
            }
            foreach (var user in websites)
            {
                await SendFailEmail(user.Email, website.WebsiteName);
            }
        }
        public async Task SendFailEmail(string userEmail, string websiteUrl)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom_ = new MailboxAddress("EgeUp", "ismailkeremgenc@gmail.com");
            MailboxAddress mailboxAddress = new MailboxAddress("User", userEmail);

            mimeMessage.From.Add(mailboxAddressFrom_);
            mimeMessage.To.Add(mailboxAddress);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Sitenize Erişim Sağlanmıyor. Lütfen Kontrolediniz : " + websiteUrl;

        

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "EgeUp Site Hata Raporu";

            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("ismailkeremgenc@gmail.com", "tpqxqycotxvmimaq");
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
        }




    }
}






