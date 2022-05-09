using BlazorApp.Controller.Enums;

namespace BlazorApp.Controller.Ships
{
    public class Battleship : Ship
    {
        public Battleship()
        {
            Name = "Battleship";
            Width = 4;
            OccupationType = Occupation.Battleship;
        }
    }
}
