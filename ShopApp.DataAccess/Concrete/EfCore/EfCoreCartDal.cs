using ShopApp.DataAccess.Abstract;
using ShopApp.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart, ShopContext>, ICartDal
    {
        //ilişkili tablolarda güncelleme işlemi
        public override void Update(Cart entity)
        {
            using (var contex = new ShopContext())
            {
                contex.Carts.Update(entity);
                contex.SaveChanges();
            }
        }
        public Cart GetByUserId(string userId)
        {
            using (var context =  new ShopContext())
            {
                return context.Carts
                                    .Include(x=> x.CartItems)
                                    .ThenInclude(x=>x.Product)
                                    .FirstOrDefault(x=> x.UserId == userId);
            };
        }
        //ilişkili tablolarda veri silme işlemi
        public void DeleteFromCart(int cartId,int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd, cartId, productId);
            }
        }

        public void ClearCart(string cartId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlRaw(cmd, cartId, cartId);
            }
        }
    }
}
