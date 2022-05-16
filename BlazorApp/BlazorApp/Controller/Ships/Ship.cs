using BlazorApp.Controller.Enums;


namespace BlazorApp.Controller.Ships
{
    public class Ship:ICloneable
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public List<Tile> Tiles { get; set; } = new List<Tile>();
        public List<Tile> Near { get; set; } = new List<Tile>();
        public Tile TopLeft { get; set; }
        public int Hits { get; set; }
        public Occupation OccupationType { get; set; }
        public Orientation OrientationType { get; set; }
        public bool IsPlaced { get; set; }

        public bool IsSunk()
        {
            return Hits >= Width;
        }

        #region Direction
        public Ship Direction(string dir)
        {
            switch (dir.ToLower())
            {
                case "right":
                    return this.Right();
                case "left": return this.Left();
                case "top": return this.Top();
                case "bottom": return this.Bottom();
                case "spawn": return this;
                default: return null;
            }
        }
        public Ship Right()
        {
            TopLeft = TopLeft.Right();
            GenerateTiles();
            return this;
        }
        public Ship Left()
        {
            TopLeft = TopLeft.Left();
            GenerateTiles();
            return this;
        }
        public Ship Top()
        {
            TopLeft = TopLeft.Top();
            GenerateTiles();
            return this;
        }
        public Ship Bottom()
        {
            TopLeft = TopLeft.Bottom();
            GenerateTiles();
            return this;
        }
        #endregion Direction

        #region Generation of tiles
        public int GenerateTiles()
        {
            Tiles = new List<Tile>();
            if (TopLeft == null || Width <= 0) return -1;

            Tile tile = TopLeft;
            Tiles.Add(tile);
            for(int i = 1; i < Width; i++)
            {
                switch (OrientationType)
                {
                    case Orientation.HORIZONTHAL:
                        tile = tile.Right();
                        break;
                    case Orientation.VERTICAL:
                        tile = tile.Bottom();
                        break;
                    case Orientation.DIAG_BR:
                        tile = tile.BottomRight();
                        break;
                    case Orientation.DIAG_TR:
                        tile = tile.TopRight();
                        break;
                }
                tile.OccupationType = OccupationType;
                Tiles.Add(tile);
            }

            GenerateNear();
            return Tiles.Count;
        }

        public int GenerateNear()
        {
            Near = new List<Tile>();
            foreach (Tile t in Tiles)
            {
                foreach (Tile t2 in Utility.AllNear(t))
                {
                    if (!Utility.Contains(t2, Near) && !Utility.Contains(t2,Tiles))
                    {
                        t2.OccupationType = Occupation.Near;
                        Near.Add(t2);
                    }
                }
            }
            return Near.Count;
        }
        #endregion Generation of tiles

        // Test
        public Ship Rotate(Orientation? o = null)
        {
            if (o == null)
            {
                o = Utility.NextOrientation(OrientationType);
            }
            OrientationType = o.Value;
            GenerateTiles();
            GenerateNear();
            return this;
        }

        public object Clone()
        {
            Ship newShip = new Ship(); 
            foreach (var prop in this.GetType().GetProperties())
            {
                prop.SetValue(newShip, prop.GetValue(this));
            }
            return newShip;
        }
    }
}
