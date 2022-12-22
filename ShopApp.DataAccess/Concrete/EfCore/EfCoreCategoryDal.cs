using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
        public void DeleteFromCategory(int id, int categoryid)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from ProductCategory where ProductId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd,id,categoryid);
            }
        }

        public Category GetByIdWithProducuts(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Categories
                    .Where(x => x.Id == id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefault();
            }
        }
    }
}
