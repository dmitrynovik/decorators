using System.Linq;
using Inventory.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inventory.UnitTest
{
    [TestClass]
    public class InventoryUnitTest
    {
        [TestMethod]
        public void Test()
        {
            var locator = new ProductsLocator(@".\");
            var costCalculator = new CostCalculator();

            foreach (var product in locator.GetProducts())
            {
                var addOns = locator.GetAddOnsForProduct(product).ToArray();
                foreach (var addOn in addOns)
                    addOn.IsSelected = true;

                var price = product.BasePrice + addOns.Sum(addOn => addOn.BasePrice);

                Assert.AreEqual(price, costCalculator.CalculateCost(product, addOns));
            }
        }
    }
}
