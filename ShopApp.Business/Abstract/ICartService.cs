using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);

        void DeleteFromCart(string userId,int productId);

        Cart GetCartByUserId(string userId);

        void AddToCart(string userId, int productId, int quantity);


    }
}
