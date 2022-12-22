using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);
        List<Category> GetALl();
        void DeleteFromCategory(int id,int categoryid);
        Category GetByIdWithProducuts(int id);
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
