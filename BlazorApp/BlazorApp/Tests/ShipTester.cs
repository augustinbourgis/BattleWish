using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class ShipTester
    {
        #region GenerateTiles
        [TestMethod]
        public void GenerateTiles_WithBadWidth_ThenMinus1()
        {
            Ship s = ShipFactory.Titanic();
            s.Width = 0;
            Assert.AreEqual(-1, s.GenerateTiles());
        }

        [TestMethod]
        public void GenerateTile_WithNoTopLeft_ThenMinus1()
        {
            Ship s = ShipFactory.Titanic();
            s.TopLeft = null;
            Assert.AreEqual(-1, s.GenerateTiles());
        }

        [TestMethod]
        public void GenerateTiles_WithOKProp_ThenWidth()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreEqual(s.Width, s.GenerateTiles());
        }

        [TestMethod]
        public void GenerateTiles_WithTitanicHorizonthal_ThenResult()
        {
            List<Tile> result = new List<Tile>();
            result.Add(TileFactory.Tile(0,0));
            result.Add(TileFactory.Tile(1, 0));
            result.Add(TileFactory.Tile(2, 0));
            result.Add(TileFactory.Tile(3, 0));
            result.Add(TileFactory.Tile(4, 0));

            Ship s = ShipFactory.Titanic();
            s.GenerateTiles();

            foreach (Tile t in s.Tiles)
            {
                Assert.IsTrue(Utility.Contains(t, result));
            }
        }

        [TestMethod]
        public void GenerateTiles_WithTitanicVertical_ThenResult()
        {
            List<Tile> result = new List<Tile>();
            result.Add(TileFactory.Tile(0, 0));
            result.Add(TileFactory.Tile(0, 1));
            result.Add(TileFactory.Tile(0, 2));
            result.Add(TileFactory.Tile(0, 3));
            result.Add(TileFactory.Tile(0, 4));

            Ship s = ShipFactory.Titanic();
            s.OrientationType = Orientation.VERTICAL;
            s.GenerateTiles();

            foreach (Tile t in s.Tiles)
            {
                Assert.IsTrue(Utility.Contains(t, result));
            }
        }

        [TestMethod]
        public void GenerateTiles_WithTitanicDiagBR_ThenResult()
        {
            List<Tile> result = new List<Tile>();
            result.Add(TileFactory.Tile(0, 0));
            result.Add(TileFactory.Tile(1, 1));
            result.Add(TileFactory.Tile(2, 2));
            result.Add(TileFactory.Tile(3, 3));
            result.Add(TileFactory.Tile(4, 4));

            Ship s = ShipFactory.Titanic();
            s.OrientationType = Orientation.DIAG_BR;
            s.GenerateTiles();

            foreach (Tile t in s.Tiles)
            {
                Assert.IsTrue(Utility.Contains(t, result));
            }
        }

        [TestMethod]
        public void GenerateTiles_WithTitanicDiagTR_ThenResult()
        {
            List<Tile> result = new List<Tile>();
            result.Add(TileFactory.Tile(0, 0));
            result.Add(TileFactory.Tile(1, -1));
            result.Add(TileFactory.Tile(2, -2));
            result.Add(TileFactory.Tile(3, -3));
            result.Add(TileFactory.Tile(4, -4));

            Ship s = ShipFactory.Titanic();
            s.OrientationType = Orientation.DIAG_TR;
            s.GenerateTiles();

            foreach (Tile t in s.Tiles)
            {
                Assert.IsTrue(Utility.Contains(t, result));
            }
        }
        #endregion GenerateTiles
    }
}
