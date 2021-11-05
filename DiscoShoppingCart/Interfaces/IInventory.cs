namespace DiscoShoppingCart.Interfaces
{
    public interface IInventory
    {
        void ReturnItem(ShoppingCartItemType type);
        bool TryTakeItem(ShoppingCartItemType type, out ShoppingCartItem? item);
        int RemainingStock(ShoppingCartItemType type);
    }
}