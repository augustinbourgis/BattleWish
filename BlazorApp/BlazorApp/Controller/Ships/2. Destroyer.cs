using BlazorApp.Controller.Enums;

namespace BlazorApp.Controller.Ships
{
    public class Destroyer : Ship
    {
        public Destroyer()
        {
            Name = "Destroyer";
            Width = 2;
            OccupationType = Occupation.Destroyer;
        }
    }
}
