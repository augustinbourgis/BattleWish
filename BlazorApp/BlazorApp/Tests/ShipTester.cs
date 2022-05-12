using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

        #region GenerateNear
        #region Horizonthal
        [TestMethod]
        public void GenerateNear_HorizonthalLength1ThenNearCount8()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 1;
            s.GenerateTiles();
            Assert.AreEqual(8, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_HorizonthalLength2ThenNearCount10()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 2;
            s.GenerateTiles();
            Assert.AreEqual(10, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_HorizonthalLength3ThenNearCount12()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 3;
            s.GenerateTiles();
            Assert.AreEqual(12, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_HorizonthalLength4ThenNearCoun14()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 4;
            s.GenerateTiles();
            Assert.AreEqual(14, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_HorizonthalLength5ThenNearCount16()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 5;
            s.GenerateTiles();
            Assert.AreEqual(16, s.GenerateNear());
        }
        #endregion Horizonthal
        #region Vertical
        [TestMethod]
        public void GenerateNear_VerticalLength1ThenNearCount8()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 1;
            s.OrientationType = Orientation.VERTICAL;
            s.GenerateTiles();
            Assert.AreEqual(8, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_VerticalLength2ThenNearCount10()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 2;
            s.OrientationType = Orientation.VERTICAL;
            s.GenerateTiles();
            Assert.AreEqual(10, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_VerticalLength3ThenNearCount12()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 3;
            s.OrientationType = Orientation.VERTICAL;
            s.GenerateTiles();
            Assert.AreEqual(12, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_VerticalLength4ThenNearCoun14()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 4;
            s.OrientationType = Orientation.VERTICAL;
            s.GenerateTiles();
            Assert.AreEqual(14, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_VerticalLength5ThenNearCount16()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 5;
            s.OrientationType = Orientation.VERTICAL;
            s.GenerateTiles();
            Assert.AreEqual(16, s.GenerateNear());
        }
        #endregion Vertical
        #region Diag_BR
        [TestMethod]
        public void GenerateNear_Diag_BRLength1ThenNearCount8()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 1;
            s.OrientationType = Orientation.DIAG_BR;
            s.GenerateTiles();
            Assert.AreEqual(8, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_Diag_BRLength2ThenNearCount10()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 2;
            s.OrientationType = Orientation.DIAG_BR;
            s.GenerateTiles();
            Assert.AreEqual(12, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_Diag_BRLength3ThenNearCount12()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 3;
            s.OrientationType = Orientation.DIAG_BR;
            s.GenerateTiles();
            Assert.AreEqual(16, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_Diag_BRLength4ThenNearCoun14()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 4;
            s.OrientationType = Orientation.DIAG_BR;
            s.GenerateTiles();
            Assert.AreEqual(20, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_Diag_BRLength5ThenNearCount16()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 5;
            s.OrientationType = Orientation.DIAG_BR;
            s.GenerateTiles();
            Assert.AreEqual(24, s.GenerateNear());
        }
        #endregion Diag_BR
        #region Diag_TR
        [TestMethod]
        public void GenerateNear_Diag_TRLength1ThenNearCount8()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 1;
            s.OrientationType = Orientation.DIAG_TR;
            s.GenerateTiles();
            Assert.AreEqual(8, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_Diag_TRLength2ThenNearCount10()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 2;
            s.OrientationType = Orientation.DIAG_TR;
            s.GenerateTiles();
            Assert.AreEqual(12, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_Diag_TRLength3ThenNearCount12()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 3;
            s.OrientationType = Orientation.DIAG_TR;
            s.GenerateTiles();
            Assert.AreEqual(16, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_Diag_TRLength4ThenNearCoun14()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 4;
            s.OrientationType = Orientation.DIAG_TR;
            s.GenerateTiles();
            Assert.AreEqual(20, s.GenerateNear());
        }
        [TestMethod]
        public void GenerateNear_Diag_TRLength5ThenNearCount16()
        {
            Ship s = ShipFactory.Carrier();
            s.Width = 5;
            s.OrientationType = Orientation.DIAG_TR;
            s.GenerateTiles();
            Assert.AreEqual(24, s.GenerateNear());
        }
        #endregion Diag_BR
        #endregion GenerateNear

        #region IsSunk
        [TestMethod]
        public void IsSunk_HitsLessThanWidth_False()
        {
            Ship ship = ShipFactory.Carrier();
            ship.Hits = ship.Width - 1;
            Assert.IsFalse(ship.IsSunk());
        }
        [TestMethod]
        public void IsSunk_HitsEqualWidth_True()
        {
            Ship ship = ShipFactory.Carrier();
            ship.Hits = ship.Width;
            Assert.IsTrue(ship.IsSunk());
        }
        [TestMethod]
        public void IsSunk_HitsMoreThanWidth_True()
        {
            Ship ship = ShipFactory.Carrier();
            ship.Hits = ship.Width + 1;
            Assert.IsTrue(ship.IsSunk());
        }
        #endregion IsSunk

        #region Rotate
        [TestMethod]
        public void Rotate_SameOrientation_ShipNoChange()
        {
            Ship s = ShipFactory.Carrier();
            Assert.AreEqual(s.OrientationType,s.Rotate(s.OrientationType).OrientationType);
        }
        [TestMethod]
        public void Rotate_NoOrientationOnHorizonthalShip_ThenOrientedDiagTR()
        {
            Ship s = ShipFactory.Carrier();
            Assert.AreEqual(Orientation.DIAG_TR, s.Rotate().OrientationType);
        }
        [TestMethod]
        public void Rotate_OrientationDiagBR_ThenOrientedDiagBR()
        {
            Ship s = ShipFactory.Carrier();
            Assert.AreEqual(Orientation.DIAG_BR, s.Rotate(Orientation.DIAG_BR).OrientationType);
        }
        #endregion Rotate

        #region Direction
        #region Direction()
        [TestMethod]
        public void Direction_Unknown_ThenNull()
        {
            Ship s = ShipFactory.Titanic();
            Ship s2 = (Ship)s.Clone();
            Assert.IsNull(s.Direction("bla"));
        }
        [TestMethod]
        public void Direction_spawn_ThenThis()
        {
            Ship s = ShipFactory.Titanic();
            Ship s2 = (Ship)s.Clone();
            Assert.AreEqual(s.Direction("spawn"),s);
        }
        [TestMethod]
        public void Direction_top_ThenTopIsCalled()
        {
            Ship s = ShipFactory.Titanic();
            Ship s2 = (Ship)s.Clone();
            Assert.AreEqual(s.Direction("top").TopLeft.Y, s2.Top().TopLeft.Y);
            Assert.AreEqual(s.Direction("top").TopLeft.X, s2.Top().TopLeft.X);
        }
        [TestMethod]
        public void Direction_right_ThenTopIsCalled()
        {
            Ship s = ShipFactory.Titanic();
            Ship s2 = (Ship)s.Clone();
            Assert.AreEqual(s.Direction("right").TopLeft.Y, s2.Right().TopLeft.Y);
            Assert.AreEqual(s.Direction("right").TopLeft.X, s2.Right().TopLeft.X);
        }
        [TestMethod]
        public void Direction_bottom_ThenTopIsCalled()
        {
            Ship s = ShipFactory.Titanic();
            Ship s2 = (Ship)s.Clone();
            Assert.AreEqual(s.Direction("bottom").TopLeft.Y, s2.Bottom().TopLeft.Y);
            Assert.AreEqual(s.Direction("bottom").TopLeft.X, s2.Bottom().TopLeft.X);
        }
        [TestMethod]
        public void Direction_left_ThenTopIsCalled()
        {
            Ship s = ShipFactory.Titanic();
            Ship s2 = (Ship)s.Clone();
            Assert.AreEqual(s.Direction("left").TopLeft.Y, s2.Left().TopLeft.Y);
            Assert.AreEqual(s.Direction("left").TopLeft.X, s2.Left().TopLeft.X);
        }
        #endregion Direction()
        #region Top
        [TestMethod]
        public void Top_ThenTopLeftTop()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreNotEqual(s.TopLeft, s.Top().TopLeft);
        }
        [TestMethod]
        public void Top_ThenTilesCountIsTheSame()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreEqual(s.Tiles.Count, s.Top().Tiles.Count);
        }
        [TestMethod]
        public void Top_ThenTilesAreUpdated()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreNotEqual(s.Tiles, s.Top().Tiles);
        }
        #endregion Top
        #region Right
        [TestMethod]
        public void Right_ThenTopLeftTop()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreNotEqual(s.TopLeft, s.Right().TopLeft);
        }
        [TestMethod]
        public void Right_ThenTilesCountIsTheSame()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreEqual(s.Tiles.Count, s.Right().Tiles.Count);
        }
        [TestMethod]
        public void Right_ThenTilesAreUpdated()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreNotEqual(s.Tiles, s.Right().Tiles);
        }
        #endregion Right
        #region Bottom
        [TestMethod]
        public void Bottom_ThenTopLeftTop()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreNotEqual(s.TopLeft, s.Bottom().TopLeft);
        }
        [TestMethod]
        public void Bottom_ThenTilesCountIsTheSame()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreEqual(s.Tiles.Count, s.Bottom().Tiles.Count);
        }
        [TestMethod]
        public void Bottom_ThenTilesAreUpdated()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreNotEqual(s.Tiles, s.Bottom().Tiles);
        }
        #endregion Bottom
        #region Left
        [TestMethod]
        public void Left_ThenTopLeftTop()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreNotEqual(s.TopLeft, s.Left().TopLeft);
        }
        [TestMethod]
        public void Left_ThenTilesCountIsTheSame()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreEqual(s.Tiles.Count, s.Left().Tiles.Count);
        }
        [TestMethod]
        public void Left_ThenTilesAreUpdated()
        {
            Ship s = ShipFactory.Titanic();
            Assert.AreNotEqual(s.Tiles, s.Left().Tiles);
        }
        #endregion Left
        #endregion Direction

        #region Clone
        [TestMethod]
        public void Clone_ThenNotSameIDInRamMem()
        {
            Ship s = ShipFactory.Carrier();
            Assert.IsFalse(object.ReferenceEquals(s, s.Clone()));
        }
        #endregion Clone
    }
}
