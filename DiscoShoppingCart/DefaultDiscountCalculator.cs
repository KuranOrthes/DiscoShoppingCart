using System;
using System.Collections.Generic;
using System.Linq;
using DiscoShoppingCart.Interfaces;

namespace DiscoShoppingCart
{
    internal sealed class DefaultDiscountCalculator : IDiscountCalculator
    {
        private readonly decimal _fourChairDiscount = decimal.One - .20m;
        private readonly decimal _setDiscount = decimal.One - .17m;
        private readonly decimal _over1000Discount = decimal.One - .15m;
        public decimal CalculateTotal(IEnumerable<ShoppingCartItem> cartItems)
        {
            var enumeratedCartItems = cartItems.ToList();
            var numChairs = enumeratedCartItems.Count(c => c.Type == ShoppingCartItemType.Chair);
            var numCouch = enumeratedCartItems.Count(c => c.Type == ShoppingCartItemType.Couch);
            var numDesk = enumeratedCartItems.Count(c => c.Type == ShoppingCartItemType.Desk);
            var numTable = enumeratedCartItems.Count(c => c.Type == ShoppingCartItemType.Table);
            var subtotal = enumeratedCartItems.Sum(c => c.Price);

            var chairPrice = enumeratedCartItems.FirstOrDefault(c => c.Type == ShoppingCartItemType.Chair)?.Price ?? decimal.Zero;
            var couchPrice = enumeratedCartItems.FirstOrDefault(c => c.Type == ShoppingCartItemType.Couch)?.Price ?? decimal.Zero;
            var deskPrice = enumeratedCartItems.FirstOrDefault(c => c.Type == ShoppingCartItemType.Desk)?.Price ?? decimal.Zero;
            var tablePrice = enumeratedCartItems.FirstOrDefault(c => c.Type == ShoppingCartItemType.Table)?.Price ?? decimal.Zero;
            var setPrice = chairPrice + couchPrice + deskPrice + tablePrice;

            var chairTotal = numChairs >= 4
                ? enumeratedCartItems.Sum(c => c.Type == ShoppingCartItemType.Chair ? c.Price * _fourChairDiscount : c.Price) 
                : subtotal;
            
            var totalSets = new int[] {numChairs, numCouch, numDesk, numTable}.Min();
            var setTotal = totalSets > 0
                ? subtotal - totalSets * setPrice +  _setDiscount * setPrice
                : subtotal;
            var over1000Total = subtotal > 1000m ? _over1000Discount * subtotal : subtotal;

            return decimal.Round(new decimal[]{chairTotal, setTotal, over1000Total}.Min(), 2);
        }
    }
}