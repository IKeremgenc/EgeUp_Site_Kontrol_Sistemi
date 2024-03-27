using DataaccsessLayer.Abstract;
using DataaccsessLayer.Concrete;
using DataaccsessLayer.Repositories;
using DataaccsessLayer.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaccsessLayer.EntitiyFreamWork
{
    public class EFMailDal : GenericRepositories<Mail>,IMailDal
    {
      private readonly Context _context;

        public EFMailDal(Context context) : base(context)
        {
          
        }
    }
}
