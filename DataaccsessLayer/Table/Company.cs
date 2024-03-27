using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataaccsessLayer.Table
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public bool Statues { get; set; }
        public int ReferenceCode { get; set; }
        public bool Statues50 { get; set; }
        public bool Statues100 { get; set; }
        public bool Statuesinfinty { get; set; }
    }
}
