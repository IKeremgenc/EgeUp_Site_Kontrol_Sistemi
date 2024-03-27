using BussinesLayer.Abstrack;
using DataaccsessLayer.Abstract;
using DataaccsessLayer.Concrete;
using DataaccsessLayer.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Concrete
{
    public class EFMail : IMailService
    {
        private readonly Context _context;
        private readonly IMailDal _mailDal;

        public EFMail(Context context, IMailDal mailDal)
        {
            _context = context;
            _mailDal = mailDal;
        }

        public List<Mail> GetMailsByWebsiteID(int websiteID)
        {
            return _context.Mails.Where(m => m.WebsiteID == websiteID).ToList();
        }


        public void TDelete(Mail t)
        {
            _mailDal.Delete(t);
        }

        public Mail TGetByID(int id)
        {
           return _mailDal.GetByID(id);   
        }

        public List<Mail> TGetList()
        {
            return _mailDal.GetList();
        }

        public Task<List<Mail>> TGetPageList(int pagenumber, int sızenumber)
        {
            throw new NotImplementedException();
        }

        public Task<List<Mail>> TGetPageList(int pagenumber, int sızenumber, Expression<Func<Mail, bool>> predicate = null, params Expression<Func<Mail, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void TInsert(Mail t)
        {
           _mailDal.Insert(t);
        }

        public void TUpdate(Mail t)
        {
            _mailDal.Update(t);
        }
    }
}
