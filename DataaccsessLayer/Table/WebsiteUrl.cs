using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaccsessLayer.Table
{
    public class WebsiteUrl
    {
        [Key]
        public int WebsiteID { get; set; }
        public string WebsiteName { get; set; }
        public string Email { get; set; }
        public bool Statues { get; set; }
        public bool wevsitedown { get; set; }
        public bool FailReports { get; set; }
        public WebsiteUrl()
        {
            FailReports = false;
            wevsitedown = false;
        }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Mail> Mails { get; set; }
    }
}
