using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BlazorApp.Tests
{
    [TestClass]
    public class GameTester
    {
        private Game g = new Game(Difficulties.FACILE);

        [TestMethod]
        public void PlayerShoot_OnEmptyTile_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Player.GameBoard = GameBoardFactory.GameBoard();
            Assert.IsTrue(g.PlayerShoot(Utility.Random(0, g.Ia.FiringBoard.Width),Utility.Random(0,g.Ia.FiringBoard.Height)));
        }

        [TestMethod]
        public void PlayerShoot_OnBoatTile_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            foreach (Tile t in g.Player.GameBoard.Tiles)
            {
                t.OccupationType = Occupation.Battleship;
            }
            Assert.IsTrue(g.PlayerShoot(Utility.Random(0, g.Ia.FiringBoard.Width), Utility.Random(0, g.Ia.FiringBoard.Height)));
        }

        [TestMethod]
        public void GetShipTilesShot_OnNewMap_Then0()
        {
            g.Ia = new IA();
            Assert.IsTrue(g.GetShipTilesShot().Count == 0);
        }

        [TestMethod]
        public void GetShipTilesShot_OneShipTileShot_Then1()
        {
            g.Ia = new IA();
            g.Ia.FiringBoard.Add(ShipFactory.Titanic());
            g.Ia.FiringBoard.Tiles[0].IsShot = true;
            Assert.IsTrue(g.GetShipTilesShot().Count == 1);
        }

        [TestMethod]
        public void GetShipTilesShot_EmptyMap_ThenCount0()
        {
            g.Ia = new IA();
            Assert.IsTrue(g.GetShipTilesShot().Count == 0);
        }

        [TestMethod]
        public void GetShipTilesShot_TitanicFullyShoot_ThenCount5()
        {
            g.Ia = new IA();
            var s = ShipFactory.Titanic();
            g.Ia.FiringBoard.Add(s);
            foreach (Tile t in s.Tiles)
            {
                if (Utility.Contains(t, g.Ia.FiringBoard.Tiles))
                {
                    g.Ia.FiringBoard.Tiles[Utility.Index(t, g.Ia.FiringBoard.Tiles)].IsShot = true;
                }
            }
            Assert.IsTrue(g.GetShipTilesShot().Count == 5);
        }

        [TestMethod]
        public void IaShoot2_AlwaysTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            Assert.IsTrue(g.IaShoot2());
        }

        [TestMethod]
        public void VerifyLeftTileIsProbAShip_RightIsShotAndBoat_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.VerifyLeftTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyLeftTileIsProbAShip_OneTileShot_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsFalse(g.VerifyLeftTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }

        [TestMethod]
        public void VerifyTopLeftTileIsProbAShip_BotttomRightIsShotAndBoat_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 3), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3,3 ), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.VerifyTopLeftTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyTopLeftTileIsProbAShip_OneTileShot_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsFalse(g.VerifyTopLeftTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }

        [TestMethod]
        public void VerifyTopTileIsProbAShip_BotttomRightIsShotAndBoat_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 3), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 3), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.VerifyTopTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyTopTileIsProbAShip_OneTileShot_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsFalse(g.VerifyTopTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyTopRightTileIsProbAShip_BotttomRightIsShotAndBoat_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(1, 3), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(1, 3), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.VerifyTopRightTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyTopRightTileIsProbAShip_OneTileShot_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsFalse(g.VerifyTopRightTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyRightTileIsProbAShip_BotttomRightIsShotAndBoat_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(1, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(1, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.VerifyRightTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyRightTileIsProbAShip_OneTileShot_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsFalse(g.VerifyRightTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyBottomRightTileIsProbAShip_BotttomRightIsShotAndBoat_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(1, 1), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(1, 1), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.VerifyBottomRightTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyBottomRightTileIsProbAShip_OneTileShot_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsFalse(g.VerifyBottomRightTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyBottomTileIsProbAShip_BotttomRightIsShotAndBoat_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 1), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 1), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.VerifyBottomTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyBottomTileIsProbAShip_OneTileShot_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsFalse(g.VerifyBottomTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyBottomLeftTileIsProbAShip_BotttomRightIsShotAndBoat_ThenTrue()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 1), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 1), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.VerifyBottomLeftTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }
        [TestMethod]
        public void VerifyBottomLeftTileIsProbAShip_OneTileShot_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsFalse(g.VerifyBottomLeftTileIsProbAShip(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)]) != null);
        }

        [TestMethod]
        public void GetNextTile_NoShotMap_ThenFalse()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            Assert.IsTrue(g.GetNextTile(g.Ia.FiringBoard.Tiles[0]).Count == 0);
        }

        [TestMethod]
        public void GetNextTile_2HorizonthalShot_ThenCount1()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue(g.GetNextTile(g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2,2),g.Ia.FiringBoard.Tiles)]).Count == 1);
        }

        [TestMethod]
        public void GetTilesNotShotYet_EmptyMap_ThenCountWidthTimesHeight()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            Assert.IsTrue((g.Ia.FiringBoard.Width*g.Ia.FiringBoard.Height) == g.GetTilesNotShotYet().Count);
        }

        [TestMethod]
        public void GetTilesNotShotYet_2ShotTile_ThenCountWidthTimesHeightMinus2()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(2, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 2), g.Ia.FiringBoard.Tiles)].IsShot = true;
            g.Ia.FiringBoard.Tiles[Utility.Index(new Tile(3, 2), g.Ia.FiringBoard.Tiles)].OccupationType = Occupation.Battleship;
            Assert.IsTrue((g.Ia.FiringBoard.Width * g.Ia.FiringBoard.Height)-2 == g.GetTilesNotShotYet().Count);
        }

        [TestMethod]
        public void GetIaShip_OnEmptyTile_ThenNull()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.GameBoard = GameBoardFactory.GameBoard();
            Assert.IsNull(g.GetIaShip(new Tile(2,2)));
        }

        [TestMethod]
        public void GetIaShip_OnShipTile_ThenShip()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Ia.Ships[0] = ShipFactory.Titanic(new Tile(2, 2));
            g.Ia.GameBoard = GameBoardFactory.GameBoard();
            g.Ia.GameBoard.Add(g.Ia.Ships[0]);
            Assert.IsNotNull(g.GetIaShip(new Tile(2, 2)));
        }

        [TestMethod]
        public void GetPlayerShip_OnEmptyTile_ThenNull()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            Assert.IsNull(g.GetPlayerShip(new Tile(2, 2)));
        }

        [TestMethod]
        public void GetPlayerShip_OnShipTile_ThenShip()
        {
            g.Ia = new IA();
            g.Player = PlayerFactory.Player();
            g.Player.Ships[0] = ShipFactory.Titanic(new Tile(2, 2));
            g.Player.GameBoard.Add(g.Player.Ships[0]);
            Assert.IsNotNull(g.GetPlayerShip(new Tile(2, 2)));
        }
    }
}
