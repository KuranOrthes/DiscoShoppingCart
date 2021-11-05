using System;

namespace DiscoShoppingCart
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public ShoppingCartItemType Type { get; set; }
    }
}