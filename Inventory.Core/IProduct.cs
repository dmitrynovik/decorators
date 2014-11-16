namespace Inventory.Core
{
    public interface IProduct
    {
        float BasePrice { get; }
        string Category { get; }
        string Description { get; }

        float GetPrice();
    }
}
