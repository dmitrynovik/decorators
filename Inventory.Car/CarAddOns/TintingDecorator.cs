using Inventory.Core;

namespace Inventory.Car.CarAddOns
{
    public class TintingDecorator : ProductDecorator
    {
        public TintingDecorator(IProduct product) : base(product) { }

        public override float BasePrice
        {
            get { return 250f; }
        }

        public override string Category
        {
            get { return "Car"; }
        }

        public override string Description
        {
            get { return "Tinting"; }
        }
    }
}
