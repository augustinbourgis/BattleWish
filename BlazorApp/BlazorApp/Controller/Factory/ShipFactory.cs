using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Ships;

namespace BlazorApp.Controller.Factory
{
    public class ShipFactory
    {
        public static Ship Destroyer(Tile topleft)
        {
            Ship ship = null;

            ship = new Destroyer();
            ship.TopLeft = topleft;
            ship.TopLeft.OccupationType = ship.OccupationType;
            ship.OrientationType = Orientation.HORIZONTHAL;
            ship.GenerateTiles();

            return ship;
        }
        public static Ship Destroyer() { return Destroyer(TileFactory.Tile(0,0)); }

        public static Ship Cruiser(Tile topleft)
        {
            Ship ship = null;

            ship = new Cruiser();
            ship.TopLeft = topleft;
            ship.OrientationType = Orientation.HORIZONTHAL;
            ship.TopLeft.OccupationType = ship.OccupationType;
            ship.GenerateTiles();

            return ship;
        }
        public static Ship Cruiser() { return Cruiser(TileFactory.Tile(0, 0)); }

        public static Ship Submarine(Tile topleft)
        {
            Ship ship = null;

            ship = new Submarine();
            ship.TopLeft = topleft;
            ship.OrientationType = Orientation.HORIZONTHAL;
            ship.TopLeft.OccupationType = ship.OccupationType;
            ship.GenerateTiles();

            return ship;
        }
        public static Ship Submarine() { return Submarine(TileFactory.Tile(0, 0)); }

        public static Ship Battleship(Tile topleft)
        {
            Ship ship = null;

            ship = new Battleship();
            ship.TopLeft = topleft;
            ship.OrientationType = Orientation.HORIZONTHAL;
            ship.TopLeft.OccupationType = ship.OccupationType;
            ship.GenerateTiles();

            return ship;
        }
        public static Ship Battleship() { return Battleship(TileFactory.Tile(0, 0)); }

        public static Ship Carrier(Tile topleft)
        {
            Ship ship = null;

            ship = new Carrier();
            ship.TopLeft = topleft;
            ship.TopLeft.OccupationType = Occupation.Carrier;
            ship.OrientationType = Orientation.HORIZONTHAL;
            ship.GenerateTiles();

            return ship;
        }
        public static Ship Carrier() { return Carrier(TileFactory.Tile(0, 0)); }

        public static Ship Titanic(Tile topleft)
        {
            Ship ship = null;

            ship = new Titanic();
            ship.TopLeft = topleft;
            ship.OrientationType = Orientation.HORIZONTHAL;
            ship.TopLeft.OccupationType = ship.OccupationType;
            ship.GenerateTiles();

            return ship;
        }
        public static Ship Titanic() { return Titanic(TileFactory.Tile(0, 0)); }

        public static Ship Ship(Occupation occ, Tile topLeft)
        {
            switch (occ)
            {
                case Occupation.Destroyer: return Destroyer(topLeft);
                case Occupation.Submarine: return Submarine(topLeft);
                case Occupation.Titanic: return Titanic(topLeft);
                case Occupation.Cruiser: return Cruiser(topLeft);
                case Occupation.Battleship: return Battleship(topLeft);
                default: return Carrier(topLeft);
            }
        }
        public static Ship Ship(Occupation occ)
        {
            return Ship(occ, TileFactory.Tile(0, 0));
        }
    }
}
