using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstrack
{
    public interface IGnericService<T> where T : class
    {
        void TInsert(T t);
        void TDelete(T t);
        void TUpdate(T t);
        List<T> TGetList();


        Task<List<T>> TGetPageList(int pagenumber, int sızenumber, Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties);
        T TGetByID(int id);
    }
}
