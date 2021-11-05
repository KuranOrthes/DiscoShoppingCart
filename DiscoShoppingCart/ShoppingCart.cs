using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DiscoShoppingCart.Interfaces;

namespace DiscoShoppingCart
{
    internal sealed class ShoppingCart : IShoppingCart
    {
        private readonly IInventory _inventory;
        private readonly IDiscountCalculator _discountCalculator;
        private readonly Dictionary<Guid, ShoppingCartItem> _cart;

        public ShoppingCart(IInventory inventory, IDiscountCalculator discountCalculator)
        {
            _inventory = inventory;
            _discountCalculator = discountCalculator;
            _cart = new Dictionary<Guid, ShoppingCartItem>();
        }

        public bool TryAddItemToCart(ShoppingCartItemType type)
        {
            var itemInInventory = _inventory.TryTakeItem(type, out var item);
            if (!itemInInventory || item is null) return false;
            
            var successfullyAdded = _cart.TryAdd(item.Id, item);

            return successfullyAdded; 
        }

        public void RemoveItemFromCart(Guid id)
        {
            var itemPresent = _cart.TryGetValue(id, out var item);
            if (!itemPresent || item is null) return;
            
            _inventory.ReturnItem(item.Type);
            _cart.Remove(id);
        }

        public void EmptyCart()
        {
            _cart.Clear();
        }

        public ImmutableList<ShoppingCartItem> GetItems => _cart.Values.ToImmutableList();

        public decimal Subtotal => decimal.Round(_cart.Values.Sum(i => i.Price), 2);

        public decimal Total => _discountCalculator.CalculateTotal(_cart.Values);
    }
}