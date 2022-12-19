using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductDal:IRepository<Product>
    {
        int GetCountByCategory(string category);
        IEnumerable<Product> GetPopularProduct();
        List<Product> GetProductsByCategory(string category, int page,int pageSize);
    }
}
