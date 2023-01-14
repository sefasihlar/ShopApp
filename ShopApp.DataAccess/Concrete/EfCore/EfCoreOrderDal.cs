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
    public class EfCoreOrderDal : EfCoreGenericRepository<Order, ShopContext>, IOrderDal
    {
        public List<Order> GetOrders(string? userId)
        {
            using(var context = new ShopContext())
            {
                var orders = context.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                    .AsQueryable();
                if (string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId == userId);
                }

                return orders.ToList();
            }
        }
    }
}
