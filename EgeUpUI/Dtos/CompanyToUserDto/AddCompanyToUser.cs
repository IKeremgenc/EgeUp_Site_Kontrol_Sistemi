using DataaccsessLayer.Table;
using System.Collections.Generic;

namespace EgeUpUI.Dtos.CompanyToUserDto
{
    public class AddCompanyToUser
    {
        public AddCompanyToUser()
        {
            Companies = new List<Company>();
        }
        public int UserId { get; set; }
        public int? CompanyId { get; set; }
        public List<Company> Companies{ get; set; }
    }
}
