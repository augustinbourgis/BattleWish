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
        public List<Ship> Ships { get; set; } = new List<Ship>();

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
            Ships.Add(s);
            return true;
        }

        public bool AddNear(Ship s)
        {
            var save = Tiles;
            foreach (Tile t in s.Near)
            {
                if (Utility.Contains(t, Tiles))
                {
                    if (Tiles[Utility.Index(t, Tiles)].OccupationType == Occupation.Empty)
                    {
                        Tiles[Utility.Index(t, Tiles)] = t;
                    }
                    else if(Tiles[Utility.Index(t, Tiles)].OccupationType != Occupation.Near)
                    {
                        Tiles = save;
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Rotate(Ship s, Orientation? o = null)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            save.Rotate(o);
            if (IsAddable(save))
            {
                s.Rotate(o);
                Add(s);
                return true;
            }
            else
            {
                Add(s);
                return false;
            }
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

        #region Direction
        public bool Top(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            save.Top();
            if (IsAddable(save))
            {
                s.Top();
                Add(s);
                return true;
            }
            Add(s);
            return false;
        }

        public bool Right(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            save.Right();
            if (IsAddable(save))
            {
                s.Right();
                Add(s);
                return true;
            }
            Add(s);
            return false;
        }

        public bool Bottom(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            save.Bottom();
            if (IsAddable(save))
            {
                s.Bottom();
                Add(s);
                return true;
            }
            Add(s);
            return false;
        }

        public bool Left(Ship s)
        {
            Ship save = (Ship)s.Clone();
            Remove(s);
            save.Left();
            if (IsAddable(save))
            {
                s.Left();
                Add(s);
                return true;
            }
            Add(s);
            return false;
        }
        #endregion Direction

        public Boolean Remove(Ship s)
        {
            if (!Ships.Contains(s)) return false;
            foreach (Tile t in s.Tiles)
            {
                if (Utility.Contains(t, Tiles))
                {
                    if(Tiles[Utility.Index(t,Tiles)].OccupationType != Occupation.Near)
                    {
                        Tiles[Utility.Index(t, Tiles)] = TileFactory.Tile(t.X, t.Y);
                    }
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
                    if (Tiles[Utility.Index(t, Tiles)].OccupationType != Occupation.Empty)
                    {
                        if ((Tiles[Utility.Index(t, Tiles)].OccupationType != Occupation.Near)) 
                        {
                            Tiles[Utility.Index(t, Tiles)] = TileFactory.Tile(t.X, t.Y);
                        }
                        else
                        {
                            bool nearBoat = false;
                            foreach (Tile tt in Utility.AllNear(Tiles[Utility.Index(t, Tiles)]))
                            {
                                if (!Utility.Contains(tt, s.Tiles))
                                {
                                    if(Utility.Contains(tt, Tiles))
                                    {
                                        if(Tiles[Utility.Index(tt, Tiles)].IsABoat())
                                        {
                                            nearBoat = true;
                                        }
                                    }
                                }
                            }
                            if (!nearBoat) Tiles[Utility.Index(t, Tiles)] = TileFactory.Tile(t.X, t.Y);
                        }
                    }
                }
            }

            Boats--;
            Ships.Remove(s);
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
