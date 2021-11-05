using System;
using System.Collections.Generic;
using System.Linq;
using DiscoShoppingCart.Interfaces;

namespace DiscoShoppingCart
{
    internal sealed class DefaultInventory : IInventory
    {
        private readonly Dictionary<ShoppingCartItemType, int> _inventory;
        private readonly Dictionary<ShoppingCartItemType, decimal> _prices;

        public DefaultInventory()
        {
            _prices = new Dictionary<ShoppingCartItemType, decimal>
            {
                {ShoppingCartItemType.Chair, 100.01m},
                {ShoppingCartItemType.Couch, 749.99m},
                {ShoppingCartItemType.Desk, 249.90m},
                {ShoppingCartItemType.Table, 500.10m}
                
            };
            _inventory = Enum.GetValues<ShoppingCartItemType>().ToDictionary(t => t, v => 25);
        }

        public void ReturnItem(ShoppingCartItemType type)
        {
            _inventory[type]++;
        }

        public bool TryTakeItem(ShoppingCartItemType type, out ShoppingCartItem? item)
        {
            if (_inventory[type] >= 0)
            {
                _inventory[type]--;
                item = new ShoppingCartItem
                {
                    Id = Guid.NewGuid(),
                    Price = _prices[type],
                    Type = type
                };

                return true;
            }
            else
            {
                item = null;
                return false;
            }
        }

        public int RemainingStock(ShoppingCartItemType type)
        {
            return _inventory[type];
        }
    }
}