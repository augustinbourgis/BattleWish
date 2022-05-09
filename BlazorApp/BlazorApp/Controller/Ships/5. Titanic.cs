using BlazorApp.Controller.Enums;

namespace BlazorApp.Controller.Ships
{
    public class Titanic : Ship
    {
        public Titanic()
        {
            Name = "Titanic";
            Width = 5;
            OccupationType = Occupation.Titanic;
        }
    }
}
