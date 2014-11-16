using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Inventory.Core
{
    /// <summary>
    /// Loads the available products from an assemblies in specified path.
    /// </summary>
    public class ProductsLocator
    {
        private const string InventoryFilePattern = "Inventory.*.dll";
        private readonly IDictionary<IProduct, ProductDecorator[]> products = new Dictionary<IProduct, ProductDecorator[]>();

        private static string directory;

        public ProductsLocator()
            : this(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)) {  }

        public ProductsLocator(string assembliesPath)
        {
            ProductsPath = assembliesPath;
        }

        /// <summary>
        /// The directory from where the products are located.
        /// </summary>
        public string ProductsPath
        {
            get { return directory; }
            set
            {
                directory = value;
                products.Clear();
                LoadProducts();
            }
        }

        public IEnumerable<IProduct> GetProducts()
        {
            return products.Keys;
        }

        public IEnumerable<ProductDecorator> GetAddOnsForProduct(IProduct product)
        {
            if (products.ContainsKey(product))
                return products[product];

            return Enumerable.Empty<ProductDecorator>();
        }

        private void LoadProducts()
        {
            var dir = new DirectoryInfo(ProductsPath);
            foreach (var assembly in dir.GetFiles(InventoryFilePattern).Select(f => SafeLoadAssembly(f.FullName)))
            {
                LoadProductFromAssembly(assembly);
            }
        }

        private void LoadProductFromAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                var reflectedTypes = assembly.GetTypes();
                foreach (var productType in reflectedTypes.Where(IsProduct))
                {
                    var product = SafeCreateInstance<IProduct>(productType);
                    if (product != null)
                    {
                        products[product] = reflectedTypes.Where(IsAddOn)
                            .Select(t => SafeCreateInstance<ProductDecorator>(product, t))
                            .Where(addOn => addOn != null && (addOn.Category == null || addOn.Category == product.Category))
                            .ToArray();
                    }
                }
            }
        }

        private static Assembly SafeLoadAssembly(string assemblyFile)
        {
            try
            {
                return Assembly.LoadFile(assemblyFile);
            }
            catch (BadImageFormatException)
            {
                // not a .NET assembly:
                return null;
            }
        }


        private static T SafeCreateInstance<T>(IProduct product, Type addOnType) where T: class, IProduct
        {
            try
            {
                return (T)Activator.CreateInstance(addOnType, product);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static T SafeCreateInstance<T>(Type productType) where T: class, IProduct
        {
            try
            {
                return (T)Activator.CreateInstance(productType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool IsProduct(Type type)
        {
            return type.IsClass && !type.IsAbstract
                   && typeof (IProduct).IsAssignableFrom(type)
                   && !typeof (ProductDecorator).IsAssignableFrom(type);
        }

        private static bool IsAddOn(Type type)
        {
            return type.IsClass && !type.IsAbstract && typeof (ProductDecorator).IsAssignableFrom(type);
        }
    }
}
