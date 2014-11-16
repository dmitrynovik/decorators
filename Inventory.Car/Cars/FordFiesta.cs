namespace Inventory.Car.Cars
{
    public class FordFiesta : Car
    {
        public FordFiesta() : base(5000) { }

        public override string Description
        {
            get { return "Ford Fiesta"; }
        }
    }
}
