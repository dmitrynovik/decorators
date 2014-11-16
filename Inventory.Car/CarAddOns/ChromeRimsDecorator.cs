using Inventory.Core;

namespace Inventory.Car.CarAddOns
{
    public class ChromeRimsDecorator : ProductDecorator
    {
        public ChromeRimsDecorator(IProduct product) : base(product) {  }

        public override float BasePrice
        {
            get { return 100; }
        }

        public override string Category
        {
            get { return "Car"; }
        }

        public override string Description
        {
            get { return "Chrome Rims"; }
        }
    }
}
