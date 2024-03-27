using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaccsessLayer.Table
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ConfirmeCode { get; set; }
        public int ReferanceCode { get; set; }
        public AppUser ReferencedBy { get; set; }

        public int? Companyid { get; set; }

        public Company Company { get; set; }


    }

}
