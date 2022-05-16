using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class GameBoardTester
    {
        #region GenerateTiles
        [TestMethod]
        public void GenerateTiles_TilesCountSameAsWidthTimesHeight()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Assert.AreEqual(gb.Width * gb.Height, gb.GenerateTiles());
        }
        #endregion GenerateTiles

        #region Add
        [TestMethod]
        public void Add_TilesAvailable_ThenShipInShips()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship s = ShipFactory.Carrier();
            gb.Add(s);
            Assert.IsTrue(gb.Ships.Contains(s));
        }
        [TestMethod]
        public void Add_TilesNotAvailable_ThenShipNotInShips()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship s = ShipFactory.Carrier();
            gb.Tiles[Utility.Index(s.Tiles[0], gb.Tiles)].OccupationType = Occupation.Submarine;
            gb.Add(s);
            Assert.IsFalse(gb.Ships.Contains(s));
        }
        [TestMethod]
        public void Add_TilesAreNearBoat_ThenShipNotInShips()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship s = ShipFactory.Carrier();
            gb.Tiles[Utility.Index(s.Tiles[0], gb.Tiles)].OccupationType = Occupation.Near;
            gb.Add(s);
            Assert.IsFalse(gb.Ships.Contains(s));
        }
        #endregion Add

        #region AddNear
        [TestMethod]
        public void AddNear_TilesAreEmpty_ThenTrue()
        {
            Ship s = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            Assert.IsTrue(gb.AddNear(s));
        }
        [TestMethod]
        public void AddNear_TilesAreBoat_ThenFalse()
        {
            Ship s = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            gb.Tiles[Utility.Index(s.Near[5], gb.Tiles)].OccupationType = Occupation.Submarine;
            Assert.IsFalse(gb.AddNear(s));
        }
        [TestMethod]
        public void AddNear_TilesAreNear_ThenTrue()
        {
            Ship s = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            gb.Tiles[Utility.Index(s.Near[5], gb.Tiles)].OccupationType = Occupation.Near;
            Assert.IsTrue(gb.AddNear(s));
        }
        #endregion AddNear

        #region Rotate
        [TestMethod]
        public void Rotate_TilesAreAvailable_ThenShipRotated()
        {
            Ship s = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            gb.Rotate(s,Orientation.VERTICAL);
            Assert.AreEqual(s.OrientationType, Orientation.VERTICAL);
        }
        [TestMethod]
        public void Rotate_TilesAreNotAvailable_ThenShipNotRotated()
        {
            Ship s = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            Tile t = TileFactory.Tile(0, 1);
            t.OccupationType = Occupation.Near;
            gb.Tiles[Utility.Index(t, gb.Tiles)] = t;
            gb.Rotate(s, Orientation.VERTICAL);
            Assert.AreEqual(s.OrientationType, Orientation.HORIZONTHAL);
        }
        #endregion Rotate

        #region IsAddable
        [TestMethod]
        public void IsAddable_TilesAreEmpty_ThenTrue()
        {
            Ship s = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            Assert.IsTrue(gb.IsAddable(s));
        }
        [TestMethod]
        public void IsAddable_ShipTilesOnNearTiles_ThenFalse()
        {
            Ship s = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Near;
            gb.Tiles[Utility.Index(t, gb.Tiles)] = t;
            Assert.IsFalse(gb.IsAddable(s));
        }
        [TestMethod]
        public void IsAddable_ShipNearTilesOnNearTiles_ThenTrue()
        {
            Ship s = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            Tile t = TileFactory.Tile(0, 1);
            t.OccupationType = Occupation.Near;
            gb.Tiles[Utility.Index(t, gb.Tiles)] = t;
            Assert.IsTrue(gb.IsAddable(s));
        }
        #endregion IsAddable

        #region Direction
        #region Top
        [TestMethod]
        public void Top_TilesAreEmpty_ThenTrue()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship ship = ShipFactory.Carrier(TileFactory.Tile(3, 3));
            Assert.IsTrue(gb.Top(ship));
        }
        [TestMethod]
        public void Top_TilesAreOccupied_ThenFalse()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship ship = ShipFactory.Carrier();
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Submarine;
            gb.Tiles[Utility.Index(t, gb.Tiles)] = t;
            Assert.IsFalse(gb.Top(ship));
        }
        #endregion Top
        #region Right
        [TestMethod]
        public void Right_TilesAreEmpty_ThenTrue()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship ship = ShipFactory.Carrier(TileFactory.Tile(3, 3));
            Assert.IsTrue(gb.Right(ship));
        }
        [TestMethod]
        public void Right_TilesAreOccupied_ThenFalse()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship ship = ShipFactory.Carrier();
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Submarine;
            gb.Tiles[Utility.Index(t, gb.Tiles)] = t;
            Assert.IsFalse(gb.Right(ship));
        }
        #endregion Right
        #region Bottom
        [TestMethod]
        public void Bottom_TilesAreEmpty_ThenTrue()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship ship = ShipFactory.Carrier(TileFactory.Tile(3, 3));
            Assert.IsTrue(gb.Bottom(ship));
        }
        [TestMethod]
        public void Bottom_TilesAreOccupied_ThenFalse()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship ship = ShipFactory.Carrier();
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Submarine;
            gb.Tiles[Utility.Index(t, gb.Tiles)] = t;
            Assert.IsFalse(gb.Bottom(ship));
        }
        #endregion Bottom
        #region Left
        [TestMethod]
        public void Left_TilesAreEmpty_ThenTrue()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship ship = ShipFactory.Carrier(TileFactory.Tile(3, 3));
            Assert.IsTrue(gb.Left(ship));
        }
        [TestMethod]
        public void Left_TilesAreOccupied_ThenFalse()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Ship ship = ShipFactory.Carrier();
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Submarine;
            gb.Tiles[Utility.Index(t, gb.Tiles)] = t;
            Assert.IsFalse(gb.Left(ship));
        }
        #endregion Left
        #endregion Direction

        #region Remove
        [TestMethod]
        public void Remove_ShipNotPlaced_ThenTrue()
        {
            Ship ship = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            Assert.IsFalse(gb.Remove(ship));
        }
        [TestMethod]
        public void Remove_ShipPlaced_ThenTrue()
        {
            Ship ship = ShipFactory.Carrier();
            GameBoard gb = GameBoardFactory.GameBoard();
            gb.Add(ship);
            Assert.IsTrue(gb.Remove(ship));
        }
        [TestMethod]
        public void Remove_ShipPlacedNearBoat_ThenNearTilesOfOtherBoatStillOnMap()
        {
            Ship ship = ShipFactory.Carrier();
            Ship ship2 = ShipFactory.Titanic(TileFactory.Tile(0,2));
            GameBoard gb = GameBoardFactory.GameBoard();
            gb.Add(ship);
            gb.Add(ship2);
            Assert.IsTrue(gb.Remove(ship));
            Assert.IsTrue(gb.Tiles[Utility.Index(TileFactory.Tile(0, 1),gb.Tiles)].OccupationType == Occupation.Near);
        }
        #endregion Remove

        #region Clone
        [TestMethod]
        public void Clone_ThenNotSameIDInRamMem()
        {
            GameBoard gb = GameBoardFactory.GameBoard();
            Assert.IsFalse(object.ReferenceEquals(gb, gb.Clone()));
        }
        #endregion Clone
    }
}
