using BlazorApp.Controller.Enums;

namespace BlazorApp.Controller
{
    public class Tile : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Occupation OccupationType { get; set; }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Available()
        {
            return OccupationType == Occupation.Empty;
        }

        #region Directions
        public Tile Top()
        {
            Tile temp = (Tile)Clone();
            temp.Y -= 1;
            return temp;
        }
        public Tile TopRight()
        {
            Tile temp = (Tile)Clone();
            temp.Y -= 1;
            temp.X += 1;
            return temp;
        }
        public Tile Right()
        {
            Tile temp = (Tile)Clone();
            temp.X += 1;
            return temp;
        }
        public Tile BottomRight()
        {
            Tile temp = (Tile)Clone();
            temp.Y += 1;
            temp.X += 1;
            return temp;
        }
        public Tile Bottom()
        {
            Tile temp = (Tile)Clone();
            temp.Y += 1;
            return temp;
        }
        public Tile BottomLeft()
        {
            Tile temp = (Tile)Clone();
            temp.Y += 1;
            temp.X -= 1;
            return temp;
        }
        public Tile Left()
        {
            Tile temp = (Tile)Clone();
            temp.X -= 1;
            return temp;
        }
        public Tile TopLeft()
        {
            Tile temp = (Tile)Clone();
            temp.Y -= 1;
            temp.X -= 1;
            return temp;
        }
        #endregion Directions

        #region Clone()
        public object Clone()
        {
            Tile newTile = new Tile(0,0);
            foreach (var prop in this.GetType().GetProperties())
            {
                prop.SetValue(newTile, prop.GetValue(this));
            }
            return newTile;
        }
        #endregion

    }
}
