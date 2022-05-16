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
        public int PlayerShootAmount { get; set; } = 0;
        public int IaShootAmount { get; set; } = 0;
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
            PlayerShootAmount++;
            Player.FiringBoard.Tiles[Utility.Index(new Tile(x,y), Ia.GameBoard.Tiles)].IsShot = true;
            IsPlayerTurn = Player.FiringBoard.Tiles[Utility.Index(new Tile(x, y), Ia.GameBoard.Tiles)].IsABoat();
            Ship target = GetIaShip(new Tile(x, y));
            if (target != null) target.Hits++;
            if (Ia.HasLost)
            {
                IsFinished = true;
                End = DateTime.Now;
            }
            if (!IsPlayerTurn) IaShoot2();
            return true;
        }

       public List<Tile> GetShipTilesShot()
        {
            var tiles = new List<Tile>();
            foreach(Tile tile in Ia.FiringBoard.Tiles)
            {
                if(tile.IsShot && tile.IsABoat())
                {
                    tiles.Add(tile);
                }
            }
            return tiles;
        }

        public bool IaShoot2()
        {
            var tilesNotShotYet = GetTilesNotShotYet();
            Tile tileToShoot = tilesNotShotYet[Utility.Random(0, tilesNotShotYet.Count)];
            if(Difficuty == Difficulties.MOYEN)
            {
                foreach(Tile tile in GetShipTilesShot())
                {
                    foreach(Tile nextTile in GetNextTile(tile))
                    {
                        IANextTiles.Add(nextTile);
                    }
                }
                if(IANextTiles.Count == 0)
                {
                    foreach(Tile shipShot in GetShipTilesShot())
                    {
                        foreach(Tile near in Utility.AllNear(shipShot))
                        {
                            if(Utility.Contains(near, Ia.FiringBoard.Tiles))
                            {
                                if(!Ia.FiringBoard.Tiles[Utility.Index(near, Ia.FiringBoard.Tiles)].IsShot)
                                {
                                    if(!GetPlayerShip(Ia.FiringBoard.Tiles[Utility.Index(shipShot, Ia.FiringBoard.Tiles)]).IsSunk())
                                    {
                                        IANextTiles.Add(near);
                                    }
                                }
                            }
                        }
                    }
                }
                if(IANextTiles.Count > 0)
                {
                    tileToShoot = Ia.FiringBoard.Tiles[Utility.Index(IANextTiles[0],Ia.FiringBoard.Tiles)];
                    IANextTiles = new List<Tile>();
                }
            }
            var index = Utility.Index(tileToShoot, Ia.FiringBoard.Tiles);
            Ia.FiringBoard.Tiles[index].IsShot = true;
            IsPlayerTurn = !Ia.FiringBoard.Tiles[index].IsABoat();
            if (!IsPlayerTurn)
            {
                GetPlayerShip(tileToShoot).Hits++;
            }
            if (Player.HasLost)
            {
                IsFinished = true;
                End = DateTime.Now;
            }
            if (!IsPlayerTurn)
            {
                return IaShoot2();
            }
            else
            {
                return IsPlayerTurn;
            }
        }

        public Tile VerifyLeftTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            int index = Utility.Index(tileToAnalyse.Right(), Ia.FiringBoard.Tiles);
            if (index == -1) return t;
            if (Ia.FiringBoard.Tiles[index].IsShot && Ia.FiringBoard.Tiles[index].IsABoat())
            {
                if(Utility.Contains(tileToAnalyse, Ia.FiringBoard.Tiles))
                {
                    int posIndex = Utility.Index(tileToAnalyse.Left(), Ia.FiringBoard.Tiles);
                    if(posIndex != -1){
                        if (!Ia.FiringBoard.Tiles[posIndex].IsShot)
                        {
                            return tileToAnalyse.Left();
                        }
                    }
                }
            }
            return t;
        }
        public  Tile VerifyRightTileIsProbAShip(Tile tileToAnalyse)
        {
            // Ne vérifie pas la grid de jeu
            Tile t = null;
            int index = Utility.Index(tileToAnalyse.Left(), Ia.FiringBoard.Tiles);
            if (index == -1) return t;
            if (Ia.FiringBoard.Tiles[index].IsShot && Ia.FiringBoard.Tiles[index].IsABoat())
            {
                if (Utility.Contains(tileToAnalyse, Ia.FiringBoard.Tiles))
                {
                    int posIndex = Utility.Index(tileToAnalyse.Right(), Ia.FiringBoard.Tiles);
                    if (posIndex != -1){
                        if (!Ia.FiringBoard.Tiles[posIndex].IsShot)
                        {
                            return tileToAnalyse.Right();
                        }
                    }
                }
            }
            return t;
        }
        public Tile VerifyTopTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            int index = Utility.Index(tileToAnalyse.Bottom(), Ia.FiringBoard.Tiles);
            if (index == -1) return t;
            if (Ia.FiringBoard.Tiles[index].IsShot && Ia.FiringBoard.Tiles[index].IsABoat())
            {
                if (Utility.Contains(tileToAnalyse, Ia.FiringBoard.Tiles))
                {
                    int posIndex = Utility.Index(tileToAnalyse.Top(), Ia.FiringBoard.Tiles);
                    if (posIndex != -1){
                        if (!Ia.FiringBoard.Tiles[posIndex].IsShot)
                        {
                            return tileToAnalyse.Top();
                        }
                    }
                }
            }
            return t;
        }
        public Tile VerifyBottomTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            int index = Utility.Index(tileToAnalyse.Top(), Ia.FiringBoard.Tiles);
            if (index == -1) return t;
            if (Ia.FiringBoard.Tiles[index].IsShot && Ia.FiringBoard.Tiles[index].IsABoat())
            {
                if (Utility.Contains(tileToAnalyse, Ia.FiringBoard.Tiles))
                {
                    int posIndex = Utility.Index(tileToAnalyse.Bottom(), Ia.FiringBoard.Tiles);
                    if (posIndex != -1){
                        if (!Ia.FiringBoard.Tiles[posIndex].IsShot)
                        {
                            return tileToAnalyse.Bottom();
                        }
                    }
                }
            }
            return t;
        }
        public Tile VerifyTopRightTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            int index = Utility.Index(tileToAnalyse.BottomLeft(), Ia.FiringBoard.Tiles);
            if (index == -1) return t;
            if (Ia.FiringBoard.Tiles[index].IsShot && Ia.FiringBoard.Tiles[index].IsABoat())
            {
                if (Utility.Contains(tileToAnalyse, Ia.FiringBoard.Tiles))
                {
                    int posIndex = Utility.Index(tileToAnalyse.TopRight(), Ia.FiringBoard.Tiles);
                    if (posIndex != -1){
                        if (!Ia.FiringBoard.Tiles[posIndex].IsShot)
                        {
                            return tileToAnalyse.TopRight();
                        }
                    }
                }
            }
            return t;
        }
        public Tile VerifyBottomLeftTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            int index = Utility.Index(tileToAnalyse.TopRight(), Ia.FiringBoard.Tiles);
            if (index == -1) return t;
            if (Ia.FiringBoard.Tiles[index].IsShot && Ia.FiringBoard.Tiles[index].IsABoat())
            {
                if (Utility.Contains(tileToAnalyse, Ia.FiringBoard.Tiles))
                {
                    int posIndex = Utility.Index(tileToAnalyse.BottomLeft(), Ia.FiringBoard.Tiles);
                    if (posIndex != -1){
                        if (!Ia.FiringBoard.Tiles[posIndex].IsShot)
                        {
                            return tileToAnalyse.BottomLeft();
                        }
                    }
                }
            }
            return t;
        }
        public Tile VerifyBottomRightTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            int index = Utility.Index(tileToAnalyse.TopLeft(), Ia.FiringBoard.Tiles);
            if (index == -1) return t;
            if (Ia.FiringBoard.Tiles[index].IsShot && Ia.FiringBoard.Tiles[index].IsABoat())
            {
                if (Utility.Contains(tileToAnalyse, Ia.FiringBoard.Tiles))
                {
                    int posIndex = Utility.Index(tileToAnalyse.BottomRight(), Ia.FiringBoard.Tiles);
                    if (posIndex != -1){
                        if (!Ia.FiringBoard.Tiles[posIndex].IsShot)
                        {
                            return tileToAnalyse.BottomRight();
                        }
                    }
                }
            }
            return t;
        }
        public Tile VerifyTopLeftTileIsProbAShip(Tile tileToAnalyse)
        {
            Tile t = null;
            int index = Utility.Index(tileToAnalyse.BottomRight(), Ia.FiringBoard.Tiles);
            if (index == -1) return t;
            if (Ia.FiringBoard.Tiles[index].IsShot && Ia.FiringBoard.Tiles[index].IsABoat())
            {
                if (Utility.Contains(tileToAnalyse, Ia.FiringBoard.Tiles))
                {
                    int posIndex = Utility.Index(tileToAnalyse.TopLeft(), Ia.FiringBoard.Tiles);
                    if (posIndex != -1){
                        if (!Ia.FiringBoard.Tiles[posIndex].IsShot)
                        {
                            return tileToAnalyse.TopLeft();
                        }
                    }
                }
            }
            return t;
        }

        public List<Tile> GetNextTile(Tile t)
        {
            List<Tile> tiles = new List<Tile>();
            if (VerifyTopTileIsProbAShip(t) != null) tiles.Add(VerifyTopTileIsProbAShip(t));
            if (VerifyTopRightTileIsProbAShip(t) != null) tiles.Add(VerifyTopRightTileIsProbAShip(t));
            if (VerifyRightTileIsProbAShip(t) != null) tiles.Add(VerifyRightTileIsProbAShip(t));
            if (VerifyBottomRightTileIsProbAShip(t) != null) tiles.Add(VerifyBottomRightTileIsProbAShip(t));
            if (VerifyBottomTileIsProbAShip(t) != null) tiles.Add(VerifyBottomTileIsProbAShip(t));
            if (VerifyBottomLeftTileIsProbAShip(t) != null) tiles.Add(VerifyBottomLeftTileIsProbAShip(t));
            if (VerifyLeftTileIsProbAShip(t) != null) tiles.Add(VerifyLeftTileIsProbAShip(t));
            if (VerifyTopLeftTileIsProbAShip(t) != null) tiles.Add(VerifyTopLeftTileIsProbAShip(t));
            return tiles;
        }
        
        public List<Tile> GetTilesNotShotYet()
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Tile tile in Ia.FiringBoard.Tiles)
            {
                if (!tile.IsShot)
                {
                    tiles.Add(tile);
                }
            }
            foreach(Tile tile in tiles)
            {
                if(tile.IsShot && tile.IsABoat())
                {
                    if(GetPlayerShip(tile).IsSunk())
                    {
                        foreach(Tile t in GetPlayerShip(tile).Tiles)
                        {
                            if (Utility.Contains(t, tiles))
                            {
                                Tile tt = tiles[Utility.Index(t, tiles)];
                                tiles.Remove(tt);
                            }
                        }
                    }
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
