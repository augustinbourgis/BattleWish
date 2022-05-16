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
    }
}
