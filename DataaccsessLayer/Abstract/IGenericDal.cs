using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataaccsessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Insert(T t);
        void Delete(T t);
        void Update(T t);

        Task<int> CountAsync();
        List<T> GetList();
        Task<List<T>> GetPageList(int pagenumber, int sızenumber, Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties);
        T GetByID(int id);
    }
}
