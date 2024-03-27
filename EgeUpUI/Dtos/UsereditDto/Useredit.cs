using DataaccsessLayer.Table;
using System.Collections.Generic;

namespace EgeUpUI.Dtos.UsereditDto
{
    public class Useredit
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Companynames { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ReferanceCode { get; set; }
        public int? SiteCount { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Statues50 { get; set; }
        public bool Statues100 { get; set; }
        public bool Statuesinfinty { get; set; }
        public List<AppUser> AppUsers { get;}
    }
}
