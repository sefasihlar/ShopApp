using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstract
{
    public interface IProdcutService
    {
        Product GetById(int id);
        List<Product> GetALl();

        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

    }
}
