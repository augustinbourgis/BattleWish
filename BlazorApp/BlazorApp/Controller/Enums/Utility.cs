using System.ComponentModel;
using System.Reflection;
using BlazorApp.Controller.Ships;

namespace BlazorApp.Controller.Enums
{
    public static class Utility
    {

        public static Orientation NextOrientation(Orientation src)
        {
            Orientation[] Arr = (Orientation[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<Orientation>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static Boolean Contains(Tile t, List<Tile> list)
        {
            if (t == null) return false;
            foreach (Tile t2 in list)
            {
                if(t2.X == t.X && t2.Y == t.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public static Boolean Contains(Ship s, List<Ship> list)
        {
            if (s == null) return false;
            foreach (Ship t2 in list)
            {
                if (t2.Width == s.Width && t2.Name == s.Name)
                {
                    return true;
                }
            }
            return false;
        }

        // TEST
        public static int Index(Tile t, List<Tile> list)
        {
            if (t == null) return -1;
            foreach (Tile t2 in list)
            {
                if (t2.X == t.X && t2.Y == t.Y)
                {
                    return list.IndexOf(t2);
                }
            }
            return -1;
        }

        // TEST
        public static int Index(Ship s, List<Ship> list)
        {
            if (s == null) return -1;
            foreach (Ship t2 in list)
            {
                if (t2.Width == s.Width && t2.Name == s.Name)
                {
                    return list.IndexOf(t2);
                }
            }
            return -1;
        }

        // TEST
        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static int Random(int low, int up)
        {
            Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(0,10));
            var rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(low, up);
        }

        public static List<Tile> AllNear(Tile t)
        {
            List<Tile> Tiles = new List<Tile>();
            Tiles.Add(t.Top());
            Tiles.Add(t.Bottom());
            Tiles.Add(t.TopLeft());
            Tiles.Add(t.TopRight());
            Tiles.Add(t.BottomLeft());
            Tiles.Add(t.BottomRight());
            Tiles.Add(t.Right());
            Tiles.Add(t.Left());
            return Tiles;
        }

        public static List<Tile> GetAllShipShot(IA ia)
        {
            List<Tile> shipShot = new List<Tile>(); 
            foreach (Tile t in ia.FiringBoard.Tiles)
            {
                if (t.IsShot && t.IsABoat())
                {
                    shipShot.Add(t);
                }
            }
            return shipShot;
        }

        public static Tile VerifyLeftTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            if (tileToAnalyse.Right().IsShot && tileToAnalyse.Right().IsABoat())
            {
                if (!tileToAnalyse.Left().IsShot)
                {
                    return tileToAnalyse.Left();
                }
            }
            return t;
        }
        public static Tile VerifyRightTileIsProbAShip(Tile tileToAnalyse)
        {
            // Ne vérifie pas la grid de jeu
            Tile t = null;
            if (tileToAnalyse.Left().IsShot && tileToAnalyse.Left().IsABoat())
            {
                if (!tileToAnalyse.Right().IsShot)
                {
                    return tileToAnalyse.Right();
                }
            }
            return t;
        }
        public static Tile VerifyTopTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            if (tileToAnalyse.Bottom().IsShot && tileToAnalyse.Bottom().IsABoat())
            {
                if (!tileToAnalyse.Top().IsShot)
                {
                    return tileToAnalyse.Top();
                }
            }
            return t;
        }
        public static Tile VerifyBottomTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            if (tileToAnalyse.Top().IsShot && tileToAnalyse.Top().IsABoat())
            {
                if (!tileToAnalyse.Bottom().IsShot)
                {
                    return tileToAnalyse.Bottom();
                }
            }
            return t;
        }
        public static Tile VerifyTopRightTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            if (tileToAnalyse.BottomLeft().IsShot && tileToAnalyse.BottomLeft().IsABoat())
            {
                if (!tileToAnalyse.TopRight().IsShot)
                {
                    return tileToAnalyse.TopRight();
                }
            }
            return t;
        }
        public static Tile VerifyBottomLeftTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            if (tileToAnalyse.TopRight().IsShot && tileToAnalyse.TopRight().IsABoat())
            {
                if (!tileToAnalyse.BottomLeft().IsShot)
                {
                    return tileToAnalyse.BottomLeft();
                }
            }
            return t;
        }
        public static Tile VerifyBottomRightTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            if (tileToAnalyse.TopLeft().IsShot && tileToAnalyse.TopLeft().IsABoat())
            {
                if (!tileToAnalyse.BottomRight().IsShot)
                {
                    return tileToAnalyse.BottomRight();
                }
            }
            return t;
        }
        public static Tile VerifyTopLeftTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            if (tileToAnalyse.BottomRight().IsShot && tileToAnalyse.BottomRight().IsABoat())
            {
                if (!tileToAnalyse.TopLeft().IsShot)
                {
                    return tileToAnalyse.TopLeft();
                }
            }
            return t;
        }

        public static List<Tile> GetNextTile(Tile t)
        {
            List<Tile> tiles = new List<Tile>();
            if(VerifyTopTileIsProbAShip(t) != null) tiles.Add(VerifyTopTileIsProbAShip(t));
            if (VerifyTopRightTileIsProbAShip(t) != null) tiles.Add(VerifyTopRightTileIsProbAShip(t));
            if (VerifyRightTileIsProbAShip(t) != null) tiles.Add(VerifyRightTileIsProbAShip(t));
            if (VerifyBottomRightTileIsProbAShip(t) != null) tiles.Add(VerifyBottomRightTileIsProbAShip(t));
            if (VerifyBottomTileIsProbAShip(t) != null) tiles.Add(VerifyBottomTileIsProbAShip(t));
            if (VerifyBottomLeftTileIsProbAShip(t) != null) tiles.Add(VerifyBottomLeftTileIsProbAShip(t));
            if (VerifyLeftTileIsProbAShip(t) != null) tiles.Add(VerifyLeftTileIsProbAShip(t));
            if (VerifyTopLeftTileIsProbAShip(t) != null) tiles.Add(VerifyTopLeftTileIsProbAShip(t));
            return tiles;
        }

        public static List<Tile> GetNextTileToShoot(IA ia)
        {
            List <Tile> tiles = new List<Tile>();
            foreach (Tile t in GetAllShipShot(ia))
            {
                foreach(Tile tt in GetNextTile(t))
                {
                    if(tt != null) tiles.Add(tt);
                }
            }
            if(tiles.Count == 0)
            {
                foreach(Tile t in GetAllShipShot(ia))
                {
                    foreach(Tile tt in AllNear(t))
                    {
                        if(Utility.Contains(tt, ia.FiringBoard.Tiles))
                        {
                            if(!ia.FiringBoard.Tiles[Index(tt,ia.FiringBoard.Tiles)].IsShot) tiles.Add(tt);
                        }
                    }
                }
            }
            return tiles;
        }
    }
}
