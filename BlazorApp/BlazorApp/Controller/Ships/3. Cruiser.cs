using BlazorApp.Controller.Enums;

namespace BlazorApp.Controller.Ships
{
    public class Cruiser : Ship
    {
        public Cruiser()
        {
            Name = "Cruiser";
            Width = 3;
            OccupationType = Occupation.Cruiser;
        }
    }
}
