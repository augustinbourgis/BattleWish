using BlazorApp.Controller.Enums;

namespace BlazorApp.Controller.Ships
{
    public class Submarine : Ship
    {
        public Submarine()
        {
            Name = "Submarine";
            Width = 3;
            OccupationType = Occupation.Submarine;
        }
    }
}
