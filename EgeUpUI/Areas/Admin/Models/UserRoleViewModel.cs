using DataaccsessLayer.Table;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EgeUpUI.Areas.Admin.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public List<AppUser> Users { get; set; }
        public SelectList OtherRoles { get; set; }
    }
}
