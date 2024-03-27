using DataaccsessLayer.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgeUpUI.Dtos.AddWebsiteDto
{
    public class AddWebDto
    {
        public AddWebDto()
        {
            Companies = new List<Company>();
        }
        public string WebsiteName { get; set; }
        public string Email { get; set; }

        public bool Statues { get; set; }
        public int? CompanyId { get; set; }
        public List<Company> Companies { get; set; }
    }
}
