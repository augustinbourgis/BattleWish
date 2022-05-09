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

        public IA()
        {
            Ships.Add(ShipFactory.Titanic());
            Ships.Add(ShipFactory.Battleship());
            Ships.Add(ShipFactory.Carrier());
            Ships.Add(ShipFactory.Submarine());
            Ships.Add(ShipFactory.Cruiser());
            Ships.Add((ShipFactory.Destroyer()));
            GameBoard = GameBoardFactory.GameBoard();
            int attempt = 100;
            PlaceBoatRandomly(ref attempt);
        }

        public string Debug()
        {
            string temp = "<div class=\"container\" style=\"width: 400px\">";
            int index = 0;
            for (int h = 0; h < GameBoard.Height; h++)
            {
                temp += "<div class=\"row\">";
                for (int w = 0; w < GameBoard.Width; w++)
                {
                    index = w * 8 + h;
                    string color = "background-color: red;";
                    if (GameBoard.Tiles[index].OccupationType != Occupation.Empty)
                    {
                        color = "background-color: blue;";
                    }
                    temp += $"<div class=\"col\" style=\"{color}justify-content: center;display: flex;border: 1px solid;height:50px\">{Utility.DescriptionAttr(GameBoard.Tiles[index].OccupationType)}</div>";
                }
                temp += "</div>";
            }
            temp += "</div>";
            return temp;
        }

        public bool PlaceBoatRandomly(ref int attempt)
        {
            attempt--;
            if (attempt < 0)
            {
                int newAttempt = 100;
                if (PlaceBoatRandomly(ref newAttempt))
                {
                    return true;
                }
            }
            List<Ship> listShip = Ships;
            foreach (Ship s in Ships)
            {
                s.TopLeft.X = Utility.Random(0, GameBoard.Width);
                s.TopLeft.Y = Utility.Random(0, GameBoard.Height);
                s.GenerateTiles();
                if (GameBoard.IsAddable(s))
                {
                    GameBoard.Add(s);
                    listShip.Remove(s);
                    if (PlaceBoatRandomly(ref listShip, ref attempt))
                    {
                        return true;
                    }
                }
                else
                {
                    if (PlaceBoatRandomly(ref attempt))
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public bool PlaceBoatRandomly(ref List<Ship> listShip, ref int attempt)
        {
            attempt--;
            if (attempt < 0)
            {
                int newAttempt = 100;
                if (PlaceBoatRandomly(ref listShip,ref newAttempt))
                {
                    return true;
                }
            }
            if (listShip.Count == 0) return true;
            foreach (Ship s in listShip)
            {
                s.TopLeft.X = Utility.Random(0, GameBoard.Width);
                s.TopLeft.Y = Utility.Random(0, GameBoard.Height);
                s.GenerateTiles();
                if (GameBoard.IsAddable(s))
                {
                    GameBoard.Add(s);
                    listShip.Remove(s);
                    if (PlaceBoatRandomly(ref listShip, ref attempt))
                    {
                        return true;
                    }
                }
                else
                {
                    if (PlaceBoatRandomly(ref listShip, ref attempt))
                    {
                        return true;
                    }
                }
            }
            return true;
        }
    }
}
