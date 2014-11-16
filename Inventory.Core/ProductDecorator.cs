using System;

namespace Inventory.Core
{
    /// <summary>
    /// The base decorator class for product add-ons.
    /// </summary>
    /// <remarks>To allow dynamic additions </remarks>
    public abstract class ProductDecorator : IProduct
    {
        private readonly IProduct product;

        protected ProductDecorator(IProduct product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            this.product = product;
        }

        public static ProductDecorator Decorate(Type type, IProduct product)
        {
            return (ProductDecorator) Activator.CreateInstance(type, product);
        }

        public bool IsSelected { get; set; }

        public abstract string Category { get; }
        public abstract string Description { get;  }
        public abstract float BasePrice { get; }

        public virtual float GetPrice() // Leave an option to override if needs be
        {
            return Math.Max(0, product.GetPrice() + BasePrice);
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
