using Inventory.Core;

namespace Inventory.Car
{
    public abstract class Car : IProduct
    {
        public float BasePrice { get; private set; }

        protected Car(float basePrice)
        {
            this.BasePrice = basePrice;
        }

        public float GetPrice()
        {
            return BasePrice;
        }

        public string Category
        {
            get { return "Car"; }
        }

        public abstract string Description { get;  }

        public override string ToString()
        {
            return Description;
        }
    }
}
