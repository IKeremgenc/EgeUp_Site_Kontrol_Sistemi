using DataaccsessLayer.Abstract;
using DataaccsessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataaccsessLayer.Repositories
{
    public class GenericRepositories<T> : IGenericDal<T> where T : class
    {
        private readonly Context _context;

        public GenericRepositories(Context context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            var countData = await query.CountAsync();
            return countData;
        }

        public void Delete(T t)
        {
            _context.Remove(t);
            _context.SaveChanges();
        }

        public T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            return _context.Set<T>().ToList();
        }

    

        public Task<List<T>> GetPageList(int pagenumber, int sızenumber, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate).Skip((pagenumber - 1) * sızenumber).Take(sızenumber);
            else
                query = query.Skip((pagenumber - 1) * sızenumber).Take(sızenumber);

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return  query.ToListAsync();
        }

        public void Insert(T t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            _context.Update(t);
            _context.SaveChanges();
        }
    }
}
////query.Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize)
//IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();