using DataaccsessLayer.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EgeUpUI.Dtos.AddWebsiteDto
{
    public class WebsiteListDto
    {


        public int WebsiteID { get; set; }
        public WebsiteUrl Website { get; set; }
        public int MailID { get; set; }
        public Mail Mails { get; set; }
        //public List<Mail> Mails { get; set; }



    }
}
