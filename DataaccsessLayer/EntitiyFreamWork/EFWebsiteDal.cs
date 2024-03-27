using DataaccsessLayer.Abstract;
using DataaccsessLayer.Concrete;
using DataaccsessLayer.Repositories;
using DataaccsessLayer.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaccsessLayer.EntitiyFreamWork
{
    public class EFWebsiteDal : GenericRepositories<WebsiteUrl>, IWebSiteDal
    {
        private readonly Context _context;
        public EFWebsiteDal(Context context) : base(context)
        {
        }

      
    }
}
