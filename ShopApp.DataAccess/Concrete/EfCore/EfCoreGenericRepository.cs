using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T>
        where T : class
        where TContext : DbContext, new()

       
    {
        public void Create(T entity)
        {
            using (var _context = new TContext())
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var _context = new TContext())
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (var _context = new TContext())
            {
                return filter == null
                    ? _context.Set<T>().ToList()
                    :_context.Set<T>().Where(filter).ToList();

            }
        }

        public T GetById(int id)
        {
            using (var _context = new TContext())
            {
                return _context.Set<T>()
                               .Find(id);
            }
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            using (var _context = new TContext())
            {
                return _context.Set<T>().Where(filter).SingleOrDefault();
            }
        }

        public void Update(T entity)
        {
            using (var _context = new TContext())
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
