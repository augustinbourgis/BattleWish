using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class UtilityTester
    {
        #region NextOrientation
        [TestMethod]
        public void NextOrientation_WithVertical_ThenDiagBR()
        {
            Assert.AreEqual(Orientation.DIAG_BR, Utility.NextOrientation(Orientation.VERTICAL));
        }

        [TestMethod]
        public void NextOrientation_WithDiagBR_ThenHorizonthal()
        {
            Assert.AreEqual(Orientation.HORIZONTHAL, Utility.NextOrientation(Orientation.DIAG_BR));
        }

        [TestMethod]
        public void NextOrientation_WithHorizonthal_ThenDiagTR()
        {
            Assert.AreEqual(Orientation.DIAG_TR, Utility.NextOrientation(Orientation.HORIZONTHAL));
        }

        [TestMethod]
        public void NextOrientation_WithDiagTR_ThenVertical()
        {
            Assert.AreEqual(Orientation.VERTICAL, Utility.NextOrientation(Orientation.DIAG_TR));
        }
        #endregion NextOrientation

        #region Contains Tile
        [TestMethod]
        public void Contains_Tile_WithNull_ThenFalse()
        {
            Assert.IsFalse(Utility.Contains(null, ShipFactory.Titanic().Tiles));
        }

        [TestMethod]
        public void Contains_Tile_WithContainedTileAndNotSameRamIdInMem_ThenTrue()
        {
            Assert.IsTrue(Utility.Contains(TileFactory.Tile(0,0), ShipFactory.Titanic().Tiles));
        }

        [TestMethod]
        public void Contains_Tile_WithSameRamIdInMemBut_ThenTrue()
        {
            var tiles = ShipFactory.Titanic().Tiles;
            Assert.IsTrue(Utility.Contains(tiles[0], tiles));
        }
        #endregion Contains Tile

        #region Contains Ship
        [TestMethod]
        public void Contains_Ship_WithNull_ThenFalse()
        {
            Assert.IsFalse(Utility.Contains(null, PlayerFactory.Player().Ships));
        }

        [TestMethod]
        public void Contains_Ship_WithContainedShipAndNotSameRamIdInMem_ThenTrue()
        {
            Assert.IsTrue(Utility.Contains(ShipFactory.Titanic(), PlayerFactory.Player().Ships));
        }

        [TestMethod]
        public void Contains_Ship_WithSameRamIdInMemBut_ThenTrue()
        {
            var ships = PlayerFactory.Player().Ships;
            Assert.IsTrue(Utility.Contains(ships[0], ships));
        }
        #endregion Contains Ship

        #region Index Tile
        [TestMethod]
        public void Index_Tile_WithNull_ThenMinus1()
        {
            Assert.AreEqual(Utility.Index(null,ShipFactory.Titanic().Tiles), -1);
        }

        [TestMethod]
        public void Index_Tile_WithTileInList_ThenIndex()
        {
            Assert.AreEqual(Utility.Index(TileFactory.Tile(2,0), ShipFactory.Titanic().Tiles),2);
        }

        [TestMethod]
        public void Index_Tile_WithTileNotInList_ThenMinus1()
        {
            Assert.AreEqual(Utility.Index(TileFactory.Tile(8, 0), ShipFactory.Titanic().Tiles), -1);
        }
        #endregion

        #region Index Ship
        [TestMethod]
        public void Index_Ship_WithNull_ThenMinus1()
        {
            Assert.AreEqual(Utility.Index(null, PlayerFactory.Player().Ships), -1);
        }

        [TestMethod]
        public void Index_Ship_WithShipInList_ThenIndex()
        {
            Assert.AreEqual(Utility.Index(ShipFactory.Titanic(), PlayerFactory.Player().Ships), 5);
        }

        [TestMethod]
        public void Index_Ship_WithShipNotInList_ThenMinus1()
        {
            List<Ship> list = new List<Ship>();
            Assert.AreEqual(Utility.Index(ShipFactory.Titanic(TileFactory.Tile(8,8)), list), -1);
        }
        #endregion
    }
}
