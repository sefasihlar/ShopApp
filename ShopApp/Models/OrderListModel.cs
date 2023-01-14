using ShopApp.Entites;

namespace ShopApp.WebUI.Models
{
    public class OrderListModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderTime { get; set; }
        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes EnumPaymentTypes { get; set; }
        public String FistName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }

        public List<OrderItemModel>? OrderItems { get; set; }

        public decimal TotalPrice()
        {
            return OrderItems.Sum(x => x.Price * x.Quantity);
        }

    }

    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public decimal Price { get; set; }
        public string? Name { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }

    }
}
