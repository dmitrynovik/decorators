using Inventory.Core;

namespace Inventory.Car.CarAddOns
{
    public class SpoilerDecorator : ProductDecorator
    {
        public SpoilerDecorator(IProduct product) : base(product) {  }

        public override float BasePrice
        {
            get { return 500; }
        }

        public override string Category
        {
            get { return "Car"; }
        }

        public override string Description
        {
            get { return "Spoiler"; }
        }
    }
}
