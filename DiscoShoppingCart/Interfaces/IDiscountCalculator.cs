using System.Collections.Generic;

namespace DiscoShoppingCart.Interfaces
{
    public interface IDiscountCalculator
    {
        decimal CalculateTotal(IEnumerable<ShoppingCartItem> cartItems);
    }
}