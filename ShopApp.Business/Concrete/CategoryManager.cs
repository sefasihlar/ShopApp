using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Concrete
{
    public  class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categorydal)
        {
            _categoryDal = categorydal;
        }

        public void Create(Category entity)
        {
            _categoryDal.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryDal.Delete(entity);
        }

        public void DeleteFromCategory(int id, int categoryid)
        {
            _categoryDal.DeleteFromCategory(id, categoryid);
        }

        public List<Category> GetALl()
        {
            return _categoryDal.GetAll().ToList();
        }

        public Category GetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public Category GetByIdWithProducuts(int id)
        {
            return _categoryDal.GetByIdWithProducuts(id);
        }

        public void Update(Category entity)
        {
            _categoryDal.Update(entity);
        }
    }
}
