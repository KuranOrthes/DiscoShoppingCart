using DiscoShoppingCart.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiscoShoppingCart
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureShoppingCart(this IServiceCollection serviceCollection, IDiscountCalculator? discountCalculator = null, IInventory? inventory = null)
        {
            if (discountCalculator is null) serviceCollection.AddScoped<IDiscountCalculator, DefaultDiscountCalculator>();
            else serviceCollection.AddScoped(typeof(IDiscountCalculator), discountCalculator.GetType());
            if (inventory is null) serviceCollection.AddScoped<IInventory, DefaultInventory>();
            else serviceCollection.AddScoped(typeof(IInventory), inventory.GetType());

            serviceCollection.AddScoped<IShoppingCart, ShoppingCart>();
        }
    }
}