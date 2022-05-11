using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;

namespace BlazorApp.Controller
{
    public class GameBoard : ICloneable
    {
        public List<Tile> Tiles { get; set; } = new List<Tile>();
        public int Width { get; set; }
        public int Height { get; set; }
        public int Boats { get; set; }

        public GameBoard(int w, int h)
        {
            Width = w;
            Height = h;
        }

        // TEST
        public int GenerateTiles()
        {
            Tiles = new List<Tile>();
            if (Width <= 0 || Height <= 0) return -1;

            for(int w = 0; w < Width; w++)
            {
                for(int h = 0; h < Height; h++)
                {
                    Tiles.Add(TileFactory.Tile(w, h));
                }
            }

            return Tiles.Count;
        }

        // TEST
        public bool Add(Ship s)
        {
            GameBoard save = this;
            foreach(Tile t in s.Tiles)
            {
                if(Utility.Contains(t, Tiles))
                {
                    if (Tiles[Utility.Index(t, Tiles)].Available())
                    {
                        Tiles[Utility.Index(t, Tiles)] = t;
                    }
                    else
                    {
                        Tiles = save.Tiles;
                        return false;
                    }
                }
            }
            AddNear(s);
            Boats++;
            return true;
        }

        public bool AddNear(Ship s)
        {
            foreach (Tile t in s.Near)
            {
                if (Utility.Contains(t, Tiles))
                {
                    if (Tiles[Utility.Index(t, Tiles)].OccupationType == Occupation.Empty)
                    {
                        Tiles[Utility.Index(t, Tiles)] = t;
                    }
                }
            }

            return true;
        }

        public bool Right(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            s.Right();
            if (IsAddable(s))
            {
                Add(s);
                return true;
            }
            s = save;
            Add(save);
            return false;
        }

        public bool Rotate(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            s.OrientationType = Utility.NextOrientation(s.OrientationType);
            s.GenerateTiles();
            if (IsAddable(s))
            {
                Add(s);
                return true;
            }
            else
            {
                s = save;
                s.GenerateTiles();
                Add(save);
            }
            return false;
        }

        public bool IsAddable(Ship s)
        {
            foreach (Tile t in s.Tiles)
            {
                if (!Utility.Contains(t, Tiles))
                {
                    return false;
                }
                else
                {
                    if (Tiles[Utility.Index(t, Tiles)].OccupationType != Occupation.Empty)
                    {
                        return false;
                    }
                }
            }
            foreach (Tile t in s.Near)
            {
                if (Utility.Contains(t, Tiles))
                {
                    if (Tiles[Utility.Index(t, Tiles)].OccupationType != Occupation.Empty && Tiles[Utility.Index(t, Tiles)].OccupationType != Occupation.Near)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // TEST
        public bool Left(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            s.Left();
            if (IsAddable(s))
            {
                Add(s);
                return true;
            }
            Add(save);
            return false;
        }

        // TEST
        public bool Top(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            s.Top();
            if (IsAddable(s))
            {
                Add(s);
                return true;
            }
            Add(save);
            return false;
        }

        // TEST
        public bool Bottom(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            s.Bottom();
            if (IsAddable(s))
            {
                Add(s);
                return true;
            }
            Add(save);
            return false;
        }

        // TEST
        public Boolean Remove(Ship s)
        {
            foreach (Tile t in s.Tiles)
            {
                if (Utility.Contains(t, Tiles))
                {
                    Tiles[Utility.Index(t, Tiles)] = TileFactory.Tile(t.X, t.Y);
                }
                else
                {
                    return false;
                }
            }
            foreach (Tile t in s.Near)
            {
                if (Utility.Contains(t, Tiles))
                {
                    Tiles[Utility.Index(t, Tiles)] = TileFactory.Tile(t.X, t.Y);
                }
            }

            Boats--;
            return true;
        }

        public object Clone()
        {
            GameBoard newGB = GameBoardFactory.GameBoard(Width,Height);
            foreach (var prop in this.GetType().GetProperties())
            {
                prop.SetValue(newGB, prop.GetValue(this));
            }
            return newGB;
        }
    }
}
