using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderTime { get; set; }
        public String UserId { get; set; }

        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes MyProperty { get; set; }
        public String FistName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String OrderNote { get; set; }

        public String PaymentId { get; set; }
        public String PaymentToken { get; set; }
        public String ConversationId { get; set; }



        public List<OrderItem> OrderItem { get; set; }
    }

    public enum EnumOrderState
    {
        waiting = 0,
        Unpaid=1,
        Complated=2,

    }

    public enum EnumPaymentTypes
    {
        CreditCart=0,
        Eft=1,
    }
}



