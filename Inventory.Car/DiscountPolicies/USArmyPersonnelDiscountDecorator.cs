using Inventory.Core;

namespace Inventory.Car.DiscountPolicies
{
    public class UsArmyPersonnelDiscountDecorator : ProductDecorator
    {
        public UsArmyPersonnelDiscountDecorator(IProduct product) : base(product) {  }

        public override float BasePrice
        {
            get { return -1000; } 
        }

        public override string Category
        {
            get { return null; }
        }

        public override string Description
        {
            get { return "US Army member?"; }
        }
    }
}
