using BlazorApp.Controller.Enums;

namespace BlazorApp.Controller.Ships
{
    public class Carrier : Ship
    {
        public Carrier()
        {
            Name = "Aircraft Carrier";
            Width = 4;
            OccupationType = Occupation.Carrier;
        }
    }
}
