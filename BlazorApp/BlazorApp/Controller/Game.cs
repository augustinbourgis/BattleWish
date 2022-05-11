using BlazorApp.Controller.Enums;

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
            Ia.GameBoard.Tiles[Utility.Index(new Tile(x,y), Ia.GameBoard.Tiles)].IsShot = true;
            IsPlayerTurn = Ia.GameBoard.Tiles[Utility.Index(new Tile(x, y), Ia.GameBoard.Tiles)].IsABoat();
            if (Ia.HasLost)
            {
                IsFinished = true;
                End = DateTime.Now;
            }
            return IsPlayerTurn;
        }

        public bool IaShoot(int x, int y)
        {
            Player.GameBoard.Tiles[Utility.Index(new Tile(x, y), Ia.GameBoard.Tiles)].IsShot = true;
            IsPlayerTurn = !Player.GameBoard.Tiles[Utility.Index(new Tile(x, y), Ia.GameBoard.Tiles)].IsABoat();
            if (Ia.HasLost || Player.HasLost)
            {
                IsFinished = true;
                End = DateTime.Now;
            }
            return IsPlayerTurn;
        }
    }
}
