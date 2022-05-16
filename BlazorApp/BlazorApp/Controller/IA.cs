using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;

namespace BlazorApp.Controller
{
    public class IA
    {
        public GameBoard GameBoard { get; set; }
        public GameBoard FiringBoard { get; set; }
        public List<Ship> Ships { get; set; } = new List<Ship>();
        public bool HasLost { get { return Ships.All(x => x.IsSunk()); } }

        public const int ProbHorizonthal = 40;
        public const int ProbVertical = 80;
        public const int ProbDiagBR = 90;
        public const int ProbDiagTR = 100;

        public Orientation GetProb(int p = -1)
        {

            int prob = Utility.Random(0, 100);
            if (p != -1) prob = p;
            Orientation o = Orientation.HORIZONTHAL;
            if (prob < ProbDiagTR) o = Orientation.DIAG_TR;
            if (prob < ProbDiagBR) o = Orientation.DIAG_BR;
            if (prob < ProbVertical) o = Orientation.VERTICAL;
            if (prob < ProbHorizonthal) o = Orientation.HORIZONTHAL;
            return o;
        }

        
        public IA()
        {
            Ships.Add((ShipFactory.Destroyer()));
            Ships.Add(ShipFactory.Cruiser());
            Ships.Add(ShipFactory.Submarine());
            Ships.Add(ShipFactory.Carrier());
            Ships.Add(ShipFactory.Battleship());
            Ships.Add(ShipFactory.Titanic());
            GameBoard = GameBoardFactory.GameBoard();
            FiringBoard = GameBoardFactory.GameBoard();
            int attempt = 1000;
            //AUBOPlaceBoatRandomly(ref attempt);
            while (!PlaceShipsRandomly())
            {
            }
        }

        public bool PlaceShipsRandomly()
        {
            GameBoard = GameBoardFactory.GameBoard();
            List<Ship> listShip = new List<Ship>();
            foreach (Ship ship in Ships)
            {
                listShip.Add(ship);
            }
            int attempt = 10;
            for(int i = 5; i > -1; i--)
            {
                attempt--;
                Ship s = listShip[i];
                Tile t = NotUsed()[Utility.Random(0, NotUsed().Count)];
                s.TopLeft.X = t.X;
                s.TopLeft.Y = t.Y;
                s.OrientationType = GetProb();
                s.GenerateTiles();
                s.GenerateNear();
                if (GameBoard.IsAddable(s))
                {
                    GameBoard.Add(s);
                }
                else
                {
                    i++;
                }
                if(attempt < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Tile> NotUsed()
        {
            List<Tile> list = new List<Tile>();
            foreach(Tile t in GameBoard.Tiles)
            {
                if (t.Available())
                {
                    list.Add(t);
                }
            }
            return list;
        }
    }
}
