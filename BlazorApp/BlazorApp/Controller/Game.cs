using BlazorApp.Controller.Enums;
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

        public Game()
        {
            //IsPlayerFirst = Utility.Random(1, 10) > 5;
            IsPlayerFirst = true;
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
            int x = Utility.Random(0, Player.GameBoard.Width);
            int y = Utility.Random(0, Player.GameBoard.Height);
            Ia.FiringBoard.Tiles[Utility.Index(new Tile(x, y), Ia.GameBoard.Tiles)].IsShot = true;
            IsPlayerTurn = !Ia.FiringBoard.Tiles[Utility.Index(new Tile(x, y), Ia.GameBoard.Tiles)].IsABoat();
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
