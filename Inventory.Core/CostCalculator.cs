using System.Collections.Generic;
using System.Linq;

namespace Inventory.Core
{
    public class CostCalculator
    {
        public float CalculateCost(IProduct product, IEnumerable<ProductDecorator> addOns)
        {
            if (product == null)
                return 0;

            foreach (var addOn in addOns.Where(a => a.IsSelected))
            {
                // Add selected add-ons using decorating technique:
                product = ProductDecorator.Decorate(addOn.GetType(), product);
            }
            return product.GetPrice();
        }
    }
}
