namespace Inventory.Car.Cars
{
    public class PorscheCayenne : Car
    {
        public PorscheCayenne() : base(100000) { }

        public override string Description
        {
            get { return "Porsche Cayenne"; }
        }

    }
}
