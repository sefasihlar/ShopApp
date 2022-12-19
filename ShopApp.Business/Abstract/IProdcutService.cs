using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstract
{
    public interface IProdcutService
    {
        Product GetById(int id);
        List<Product> GetALl();

        List<Product> GetProductsByCategory(String category,int page, int pageSize);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

        int GetCountByCategory(string category);

    }
}
