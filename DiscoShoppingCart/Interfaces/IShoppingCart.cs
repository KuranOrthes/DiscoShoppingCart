using System;
using System.Collections.Immutable;

namespace DiscoShoppingCart.Interfaces
{
    public interface IShoppingCart
    {
        bool TryAddItemToCart(ShoppingCartItemType type);
        void RemoveItemFromCart(Guid id);
        void EmptyCart();
        ImmutableList<ShoppingCartItem> GetItems { get; }
        decimal Subtotal { get; }
        decimal Total { get; }
    }
}