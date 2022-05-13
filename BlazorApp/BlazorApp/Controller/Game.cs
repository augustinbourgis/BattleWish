using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;

namespace BlazorApp.Controller
{
    public class Game
    {
        public bool IsPlayerFirst { get; set; }
        public Player Player { get; set; }
        public IA Ia { get; set; }
        public bool IsPlayerTurn { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsFinished { get; set; }
        public Difficulties Difficuty { get; set; }
        public List<Tile> IANextTiles { get; set; } = new List<Tile>();

        public Game(Difficulties Diff)
        {
            Difficuty = Diff;
            IsPlayerFirst = Utility.Random(1, 10) > 5;
            IsPlayerTurn = IsPlayerFirst;
            Start = DateTime.Now;
        }

        public bool PlayerShoot(int x, int y)
        {
            Player.FiringBoard.Tiles[Utility.Index(new Tile(x,y), Ia.GameBoard.Tiles)].IsShot = true;
            IsPlayerTurn = Player.FiringBoard.Tiles[Utility.Index(new Tile(x, y), Ia.GameBoard.Tiles)].IsABoat();
            Ship target = GetIaShip(new Tile(x, y));
            if (target != null) target.Hits++;
            if (Ia.HasLost)
            {
                IsFinished = true;
                End = DateTime.Now;
            }
            if (!IsPlayerTurn) IaShoot();
            return true;
        }

        public bool IaShoot()
        {
            bool isABoat;
            int x = Utility.Random(0, Player.GameBoard.Width);
            int y = Utility.Random(0, Player.GameBoard.Height);
            if(IANextTiles.Count > 0 && Difficuty == Difficulties.MOYEN)
            {
                x = IANextTiles[0].X;
                y = IANextTiles[0].Y;
            }
            Ia.FiringBoard.Tiles[Utility.Index(GetTileNotShotYet(), Ia.FiringBoard.Tiles)].IsShot = true;
            isABoat = Ia.FiringBoard.Tiles[Utility.Index(new Tile(x, y), Ia.FiringBoard.Tiles)].IsABoat();
            IANextTiles = FilteredOkTiles(Utility.GetNextTileToShoot(Ia));
            IsPlayerTurn = !isABoat;
            if (Ia.HasLost || Player.HasLost)
            {
                IsFinished = true;
                End = DateTime.Now;
            }
            if (!IsPlayerTurn)
            {
                IsPlayerTurn = IaShoot();
            }
            return IsPlayerTurn;
        }

        public List<Tile> FilteredOkTiles(List<Tile> list)
        {
            List<Tile> news = new List<Tile>();
            foreach (Tile t in list)
            {
                if (!Utility.Contains(t, GetDoNotShootTilesForIA()))
                {
                    news.Add(t);
                }
            }
            return news;
        }

        public Tile GetTileNotShotYet()
        {
            if(Difficuty == Difficulties.MOYEN && IANextTiles.Count > 0)
            {
                return IANextTiles[0];
            }
            int x = Utility.Random(0, Player.GameBoard.Width);
            int y = Utility.Random(0, Player.GameBoard.Height);
            Tile tile = TileFactory.Tile(x, y);
            if (tile.IsShot)
            {
                return GetTileNotShotYet();
            }
            return tile;
        }

        public List<Tile> GetDoNotShootTilesForIA()
        {
            List<Tile> tiles = new List<Tile>();
            foreach(Ship s in Ia.FiringBoard.Ships)
            {
                if (s.IsSunk())
                {
                    foreach(Tile t in s.Tiles)
                    {
                        tiles.Add(t);
                    }
                }
            }
            foreach (Tile t in Ia.FiringBoard.Tiles)
            {
                if (t.IsShot)
                {
                    if(!Utility.Contains(t,tiles)) tiles.Add(t);
                }
            }
            return tiles;
        }

        public Ship GetIaShip(Tile t)
        {
            if (Ia.GameBoard.Tiles[Utility.Index(t, Ia.GameBoard.Tiles)].IsABoat())
            {
                foreach(Ship iaShip in Ia.Ships)
                {
                    if (Utility.Contains(t, iaShip.Tiles))
                    {
                        return iaShip;
                    }
                }
            }
            return null;
        }

        public Ship GetPlayerShip(Tile t)
        {
            if (Player.GameBoard.Tiles[Utility.Index(t, Player.GameBoard.Tiles)].IsABoat())
            {
                foreach (Ship playerShip in Player.Ships)
                {
                    if (Utility.Contains(t, playerShip.Tiles))
                    {
                        return playerShip;
                    }
                }
            }
            return null;
        }
    }
}
