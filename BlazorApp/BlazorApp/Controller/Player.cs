using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;

namespace BlazorApp.Controller
{
    public class Player
    {
        public string Login { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMale { get; set; }
        public GameBoard GameBoard { get; set; }
        public GameBoard FiringBoard { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasLost { get{ return Ships.All(x => x.IsSunk()); }}

        public Player(string login)
        {
            Login = login;
            GenerateShips();
            GameBoard = GameBoardFactory.GameBoard();
            FiringBoard = GameBoardFactory.GameBoard();
        }

        public bool GenerateShips()
        {
            Ships = new List<Ship>()
            {
            ShipFactory.Destroyer(),
            ShipFactory.Cruiser(),
            ShipFactory.Submarine(),
            ShipFactory.Battleship(),
            ShipFactory.Carrier(),
            ShipFactory.Titanic()
            };
            return Ships.Count == 6;
        }

        public Player(string login, bool isAdmin, bool isMale)
        {
            Login = login;
            IsAdmin = isAdmin;
            IsMale = isMale;
            GenerateShips();
            GameBoard = GameBoardFactory.GameBoard();
            FiringBoard = GameBoardFactory.GameBoard();
        }
    }
}
