using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Inventory.Core;

namespace Inventory.UI
{
    public class ProductConfigurationController
    {
        private readonly ProductsLocator productsLocator = new ProductsLocator();

        public IEnumerable<IProduct> Products
        {
            get { return productsLocator.GetProducts(); }
        }

        public IProduct[] GetAddOnsForProduct(IProduct product)
        {
            var result =  productsLocator.GetAddOnsForProduct(product).ToArray();
            foreach (var addOn in result)
            {
                addOn.IsSelected = false;
            }
            return result;
        }


        internal float CalculateCost(object selectedProduct, CheckedListBox.ObjectCollection addOns)
        {
            var calculator = new CostCalculator();
            return calculator.CalculateCost(selectedProduct as IProduct, addOns.Cast<ProductDecorator>());
        }

        internal void SelectUnselect(CheckedListBox.ObjectCollection objectCollection, ItemCheckEventArgs e)
        {
            ((ProductDecorator) objectCollection[e.Index]).IsSelected = (e.NewValue == CheckState.Checked);
        }
    }
}
