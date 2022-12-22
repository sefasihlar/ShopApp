using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{

    public class EfCoreProductDal : EfCoreGenericRepository<Product, ShopContext>, IProductDal
    {
        public Product GetByIdWithCategories(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                    .Where(x => x.Id == id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var contex = new ShopContext())

            {
                //ekstra sorgu gönderebilmek için "asQueryable" yi kullandik
                var products = contex.Products.AsQueryable();
                //category bilgisi eger null degilse categoryle gore filtreleme yaparız
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        //"Any"bize true yada false deger dondurur
                        .Where(x => x.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }
                //sayisal bir değer döndürsün
                return products.Count();
            };
        }

        public IEnumerable<Product> GetPopularProduct()
        {
            throw new NotImplementedException();
        }
        //Kategoriye göre filtereleme işlemi
        public List<Product> GetProductsByCategory(string category, int page,int pageSize)
        {
            using (var contex = new ShopContext())

            {
                //ekstra sorgu gönderebilmek için "asQueryable" yi kullandik
                var products = contex.Products.AsQueryable();
                //category bilgisi eger null degilse categoryle gore filtreleme yaparız
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        //"Any"bize true yada false deger dondurur
                        .Where(x => x.ProductCategories.Any(a=>a.Category.Name.ToLower()==category.ToLower()));
                }
                //hangi saydaki ürünlerin alınacaginin yapıldıgı kısım
                return products.Skip((page-1) * pageSize).Take(pageSize).ToList();
            };
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using ( var contex = new ShopContext())
            {
                var product = contex.Products
                                .Include(x => x.ProductCategories)
                                .FirstOrDefault(x => x.Id == entity.Id);
                if (product != null)
                {
                    product.Name = entity.Name;
                    product.ImageUrl = entity.ImageUrl;
                    product.Price = entity.Price;
                    product.Gender = entity.Gender;

                    product.ProductCategories = categoryIds.Select(x => new ProductCategory()
                    {
                        CategoryId = x,
                        ProductId = entity.Id
                    }).ToList();

                    contex.SaveChanges();



                }
            }
        }
    }
}
