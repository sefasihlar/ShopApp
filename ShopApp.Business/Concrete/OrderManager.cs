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
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Create(Order entity)
        {
            _orderDal.Create(entity);
        }

        public List<Order> GetAll()
        {
            return _orderDal.GetAll().ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _orderDal.GetAllOrders();
        }

        public Order GetById(int id)
        {
            return _orderDal.GetById(id);
        }

        public List<Order> GetOrders(string? userId)
        {
            return _orderDal.GetOrders(userId);
        }

        public List<Order> GetWithOrderId(int orderId)
        {
            return _orderDal.GetWithOrderId(orderId);
        }
    }
}
