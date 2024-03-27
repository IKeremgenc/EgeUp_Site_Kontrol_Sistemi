using DataaccsessLayer.Table;
using System.Collections.Generic;

namespace EgeUpUI.Areas.Admin.Models.DetayCompany
{
    public class DetayCompanyViewModel
    {
        public DetayCompanyViewModel()
        {
            Companies = new List<Company>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CompanyName { get; set; }
        public bool Status100 { get; set; }
        public bool Status50 { get; set; }
        public bool Statuesinfinty { get; set; }
        public Company Company { get; set; } 
        public List<AppUser> Users { get; set; } 

        public int? CompanyId { get; set; }
        public List<Company> Companies { get; set; }
    }
}
