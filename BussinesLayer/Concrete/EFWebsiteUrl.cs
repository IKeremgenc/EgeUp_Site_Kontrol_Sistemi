using BussinesLayer.Abstrack;
using DataaccsessLayer.Abstract;
using DataaccsessLayer.Concrete;
using DataaccsessLayer.EntitiyFreamWork;
using DataaccsessLayer.Table;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Concrete
{
    public class EFWebsiteUrl : IWebsiteService
    {
        private readonly Context _context;
        private readonly IWebSiteDal _webSiteDal;
        private readonly UserManager<AppUser> _userManager;

      

        public EFWebsiteUrl(Context context, IWebSiteDal webSiteDal, UserManager<AppUser> userManager)
        {
            _context = context;
            _webSiteDal = webSiteDal;
            _userManager = userManager;
        }

      
        public void TDelete(WebsiteUrl t)
        {
            _webSiteDal.Delete(t);
        }

        public WebsiteUrl TGetByID(int id)
        {
            return _webSiteDal.GetByID(id);
        }

        public List<WebsiteUrl> TGetList()
        {
            return _webSiteDal.GetList();
        }

  

        public Task<List<WebsiteUrl>> TGetPageList(int pagenumber, int sızenumber, Expression<Func<WebsiteUrl, bool>> predicate = null, params Expression<Func<WebsiteUrl, object>>[] includeProperties)
        {
            return  _webSiteDal.GetPageList(pagenumber, sızenumber,predicate);
        }

        public void TInsert(WebsiteUrl t)
        {
            _webSiteDal.Insert(t);
        }

        public void TUpdate(WebsiteUrl t)
        {
            _webSiteDal.Update(t);
        }
    }


}

