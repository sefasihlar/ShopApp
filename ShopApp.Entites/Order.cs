using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Entites
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderTime { get; set; }

        public AppUser User { get; set; }
        public  int UserId { get; set; }
      

        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes EnumPaymentTypes { get; set; }
        public String FistName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String? PaymentId { get; set; }
        public String? PaymentToken { get; set; }
        public String? ConversationId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }

    public enum EnumOrderState
    {
        waiting = 0,
        Unpaid=1,
        Completed=2,

    }

    public enum EnumPaymentTypes
    {
        CreditCart=0,
        Eft=1,
    }
}



