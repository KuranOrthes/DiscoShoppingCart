using System;
using DiscoShoppingCart;
using DiscoShoppingCart.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ShoppingCartTests
{
    public class ShoppingCartTests
    {
        private readonly decimal _chairPrice = 100.01m;
        private readonly decimal _couchPrice = 749.99m;
        private readonly decimal _deskPrice = 249.90m;
        private readonly decimal _tablePrice = 500.10m;

        private readonly decimal _chairDiscount = decimal.One - .2m;
        private readonly decimal _setDiscount = decimal.One - .17m;
        private readonly decimal _over1000Discount = decimal.One - .15m;
        
        private readonly IShoppingCart _cart;
        public ShoppingCartTests()
        {
            var services = new ServiceCollection();
            services.ConfigureShoppingCart();
            var serviceProvider = services.BuildServiceProvider();
            _cart = serviceProvider.GetService<IShoppingCart>();
            _cart.EmptyCart();
        }
        
        [Fact]
        public void EmptyCart()
        {
            Assert.Equal(decimal.Zero, _cart.Subtotal);
        }
        
        [Fact]
        public void NoDiscount()
        {
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Couch);
            Assert.Equal(decimal.Round(_chairPrice * 2 + _couchPrice, 2), _cart.Total);
        }
        
        [Fact]
        public void ChairDiscount()
        {
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            Assert.Equal(decimal.Round(_chairPrice * 6 * _chairDiscount, 2), _cart.Total);
        }
        
        [Fact]
        public void SetDiscount()
        {
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Couch);
            _cart.TryAddItemToCart(ShoppingCartItemType.Desk);
            _cart.TryAddItemToCart(ShoppingCartItemType.Table);
            Assert.Equal(decimal.Round((_chairPrice + _couchPrice + _deskPrice + _tablePrice)  * _setDiscount, 2), _cart.Total);
        }
        
        [Fact]
        public void Over1000Discount()
        {
            _cart.TryAddItemToCart(ShoppingCartItemType.Table);
            _cart.TryAddItemToCart(ShoppingCartItemType.Table);
            Assert.Equal(decimal.Round(_tablePrice * 2  * _over1000Discount, 2), _cart.Total);
        }
        
        [Fact]
        public void MinDiscount()
        {
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Chair);
            _cart.TryAddItemToCart(ShoppingCartItemType.Couch);
            _cart.TryAddItemToCart(ShoppingCartItemType.Desk);
            _cart.TryAddItemToCart(ShoppingCartItemType.Table);
            Assert.NotEqual(decimal.Round((_chairPrice + _couchPrice + _deskPrice + _tablePrice)  * _setDiscount + _chairPrice * 3, 2), _cart.Total);
            Assert.NotEqual(decimal.Round(_chairPrice * 4 * _chairDiscount + _couchPrice + _deskPrice + _tablePrice, 2), _cart.Total);
            Assert.Equal(decimal.Round((_chairPrice * 4 + _couchPrice + _deskPrice + _tablePrice) * _over1000Discount, 2), _cart.Total);
        }
    }
}