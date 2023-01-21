using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entites;
using ShopApp.WebUI.Models;
using System.Net;
using System;
using IyzipayCore;
using IyzipayCore.Request;
using IyzipayCore.Model;
using Options = IyzipayCore.Options;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private UserManager<AppUser> _userManager;
        OrderManager _orderManager = new OrderManager(new EfCoreOrderDal());
        CartManager _cartManager = new CartManager(new EfCoreCartDal());

        public CartController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = _cartManager.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(x=> new CartItemModel()
                {
                    CartItemId = x.Id,
                    ProductId = x.ProductId,
                    Name = x.Product.Name,
                    Price =Convert.ToDecimal(x.Product.Price),
                    ImageUrl = x.Product.ImageUrl,
                    Quantity = x.Quantity


                     
                }).ToList()
            });
        }

     

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartManager.AddToCart(userId,productId,quantity);
            return RedirectToAction("Index","Cart");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartManager.DeleteFromCart(_userManager.GetUserId(User),productId);
            return RedirectToAction("Index","Cart");
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = _cartManager.GetCartByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderModel();

            orderModel.CartModel =

                new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(x => new CartItemModel()
                    {
                        CartItemId = x.Id,
                        ProductId = x.ProductId,
                        Name = x.Product.Name,
                        Price = Convert.ToDecimal(x.Product.Price),
                        ImageUrl = x.Product.ImageUrl,
                        Quantity = x.Quantity

                    }).ToList()
                };
            return View(orderModel);
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cart = _cartManager.GetCartByUserId(userId);
                model.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(x=> new CartItemModel()
                    {
                        CartItemId = x.Id,
                        ProductId = x.ProductId,
                        Name = x.Product.Name,
                        Price = Convert.ToDecimal(x.Product.Price),
                        ImageUrl = x.Product.ImageUrl,
                        Quantity = x.Quantity

                    }).ToList()
                };

                var payment = PaymentRrocess(model);

                if (payment.Status == "success")
                {
                    SaveOrder(model, payment, userId);
                    ClearCart(cart.Id.ToString());
                    return View("Success");
                }


            }

            return View(model);
        }

        private void SaveOrder(OrderModel model, Payment payment, string userId)
        {
            var order = new Order();

            order.OrderNumber = new Random().Next(111111,999999).ToString();
            order.OrderState = EnumOrderState.Completed;
            order.EnumPaymentTypes = EnumPaymentTypes.CreditCart;
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.OrderTime = new DateTime();
            order.FistName = model.FirstName;
            order.LastName = model.LastName;
            order.Email = model.Mail;
            order.Phone = model.Phone;
            order.Address = model.Address;
            order.UserId =Convert.ToInt32(userId);

            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,

                };
                order.OrderItems.Add(orderItem);
            }

            _orderManager.Create(order);

        }

        private void ClearCart(string cartId)
        {
            _cartManager.ClearCart(cartId);
        }

        private Payment PaymentRrocess(OrderModel model)
        {
            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;

            Options options = new Options();
            options.SecretKey = "";
            options.ApiKey = "";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
       
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = model.CartModel.TotalPrice().ToString().Split(",")[0];
            request.PaidPrice = model.CartModel.TotalPrice().ToString().Split(",")[0];
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = model.CartModel.CartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            //PaymentCard paymentCard = new PaymentCard();
            //paymentCard.CardHolderName = model.CardName;
            //paymentCard.CardNumber = "5528790000000008";
            //paymentCard.ExpireMonth = "12";
            //paymentCard.ExpireYear = "2030";
            //paymentCard.Cvc = "123";

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = model.FirstName;
            buyer.Surname = model.LastName;
            buyer.GsmNumber = model.Phone;
            buyer.Email = model.Mail;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = model.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = model.City;
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = model.FirstName;
            shippingAddress.City = model.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = model.City; 
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = model.FirstName;
            billingAddress.City = model.FirstName;
            billingAddress.Country = "Turkey";
            billingAddress.Description = model.Address;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

           


            foreach (var item in model.CartModel.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = "Phone";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Price = item.Price.ToString().Split(",")[0];
                basketItems.Add(basketItem);

               
            }

         
            request.BasketItems = basketItems;

            return Payment.Create(request, options);

         
        }


        public IActionResult GetOrders()
        {
           var orders =  _orderManager.GetOrders(_userManager.GetUserId(User));
            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;
            foreach (var item in orders)
            {
                orderModel = new OrderListModel();
                orderModel.OrderId = item.Id;
                orderModel.OrderNumber = item.OrderNumber;
                orderModel.OrderTime = item.OrderTime;
                orderModel.Phone = item.Phone;
                orderModel.FistName = item.FistName;
                orderModel.LastName = item.LastName;
                orderModel.Email = item.Email;
                orderModel.Address = item.Address;

                orderModel.OrderItems = item.OrderItems.Select(x => new OrderItemModel()
                {
                    OrderItemId = x.OrderId,
                    Name = x.Product.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    ImgUrl = x.Product.ImageUrl,


                }).ToList();


                orderListModel.Add(orderModel);
               
            }
            return View(orderListModel);
        }


        public IActionResult OrderDetails(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Order values = _orderManager.GetById((int)id);

            return View(values);
        }
    }
}
