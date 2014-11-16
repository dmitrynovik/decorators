using System.Linq;
using System.Windows.Forms;
using Inventory.Core;

namespace Inventory.UI
{
    public partial class ProductConfigurationForm : Form
    {
        private readonly ProductConfigurationController controller = new ProductConfigurationController();

        public ProductConfigurationForm()
        {
            InitializeComponent();
            LoadProducts();
            CalculateTotal();
        }

        private void LoadProducts()
        {
            this.comboBoxProducts.Items.AddRange(controller.Products.ToArray());
            if (this.comboBoxProducts.Items.Count > 0)
                this.comboBoxProducts.SelectedIndex = 0;
        }

        private void comboBoxInventories_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DisplayAddOns();
            CalculateTotal();
        }

        private void DisplayAddOns()
        {
            this.checkedListBoxAddOns.Items.Clear();
            var product = this.comboBoxProducts.SelectedItem as IProduct;
            if (product != null)
                this.checkedListBoxAddOns.Items.AddRange(controller.GetAddOnsForProduct(product));
        }

        private void CalculateTotal()
        {
            this.textBoxTotal.Text = controller.CalculateCost(this.comboBoxProducts.SelectedItem, this.checkedListBoxAddOns.Items).ToString();
        }

        private void checkedListBoxAddOns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            controller.SelectUnselect(this.checkedListBoxAddOns.Items, e);
            CalculateTotal();
        }
    }
}
