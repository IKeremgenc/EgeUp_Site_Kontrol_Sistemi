using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaccsessLayer.Table
{
    public class Mail
    {
        [Key]
        public int MailID { get; set; }
        public string Email { get; set; }

        
        public int WebsiteID { get; set; }
        public WebsiteUrl Website { get; set; }


    }
}
