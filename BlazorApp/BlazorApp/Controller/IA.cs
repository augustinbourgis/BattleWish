﻿using BlazorApp.Controller.Enums;
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

        public Orientation GetProb()
        {
            int prob = Utility.Random(0, 100);
            Orientation o = Orientation.HORIZONTHAL;
            if (prob < ProbDiagTR) o = Orientation.DIAG_TR;
            if (prob < ProbDiagBR) o = Orientation.DIAG_BR;
            if (prob < ProbVertical) o = Orientation.VERTICAL;
            if (prob < ProbHorizonthal) o = Orientation.HORIZONTHAL;
            return o;
        }

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
            string temp = "<div class=\"container\" style=\"width: 800px\">";
            int index = 0;
            for (int h = 0; h < GameBoard.Height; h++)
            {
                temp += "<div class=\"row\">";
                for (int w = 0; w < GameBoard.Width; w++)
                {
                    index = w * GameBoard.Width + h;
                    string color = "";
                    if (GameBoard.Tiles[index].OccupationType == Occupation.Empty)
                    {
                        color = "background-color: red;";
                    }
                    else
                    {
                        if (GameBoard.Tiles[index].OccupationType == Occupation.Near)
                        {
                            color = "background-color: blue;";
                        }
                        else
                        {
                            color = "background-color: green;";
                        }
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
                s.OrientationType = GetProb();
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
                var WorkingShips = new List<Ship>();
                WorkingShips.Add(ShipFactory.Titanic());
                WorkingShips.Add(ShipFactory.Battleship());
                WorkingShips.Add(ShipFactory.Carrier());
                WorkingShips.Add(ShipFactory.Submarine());
                WorkingShips.Add(ShipFactory.Cruiser());
                WorkingShips.Add((ShipFactory.Destroyer()));
                GameBoard = GameBoardFactory.GameBoard();
                if (PlaceBoatRandomly(ref WorkingShips,ref newAttempt))
                {
                    return true;
                }
            }
            if (listShip.Count == 0) return true;
            foreach (Ship s in listShip)
            {
                s.TopLeft.X = Utility.Random(0, GameBoard.Width);
                s.TopLeft.Y = Utility.Random(0, GameBoard.Height);
                s.OrientationType = GetProb();
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
