using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class TileTester
    {
        #region Direction
        #region Top
        [TestMethod]
        public void Top_WithNegativeXorY_ThenNotNull()
        {
            Tile t = TileFactory.Tile(-2, -2);
            Assert.IsNotNull(t.Top());
        }

        [TestMethod]
        public void Top_WithX3andY3_ThenX3andY2()
        {
            Tile t = TileFactory.Tile(3, 3);
            Assert.AreEqual(t.Top().X, 3);
            Assert.AreEqual(t.Top().Y, 2);
        }
        #endregion Top

        #region TopRight
        [TestMethod]
        public void TopRight_WithNegativeXorY_ThenNotNull()
        {
            Tile t = TileFactory.Tile(-2, -2);
            Assert.IsNotNull(t.TopRight());
        }

        [TestMethod]
        public void TopRight_WithX3andY3_ThenX4andY2()
        {
            Tile t = TileFactory.Tile(3, 3);
            Assert.AreEqual(t.TopRight().X, 4);
            Assert.AreEqual(t.TopRight().Y, 2);
        }
        #endregion TopRight

        #region Right
        [TestMethod]
        public void Right_WithNegativeXorY_ThenNotNull()
        {
            Tile t = TileFactory.Tile(-2, -2);
            Assert.IsNotNull(t.Right());
        }

        [TestMethod]
        public void Right_WithX3andY3_ThenX4andY3()
        {
            Tile t = TileFactory.Tile(3, 3);
            Assert.AreEqual(t.Right().X, 4);
            Assert.AreEqual(t.Right().Y, 3);
        }
        #endregion Right

        #region BottomRight
        [TestMethod]
        public void BottomRight_WithNegativeXorY_ThenNotNull()
        {
            Tile t = TileFactory.Tile(-2, -2);
            Assert.IsNotNull(t.BottomRight());
        }

        [TestMethod]
        public void BottomRight_WithX3andY3_ThenX4andY4()
        {
            Tile t = TileFactory.Tile(3, 3);
            Assert.AreEqual(t.BottomRight().X, 4);
            Assert.AreEqual(t.BottomRight().Y, 4);
        }
        #endregion BottomRight

        #region Bottom
        [TestMethod]
        public void Bottom_WithNegativeXorY_ThenNotNull()
        {
            Tile t = TileFactory.Tile(-2, -2);
            Assert.IsNotNull(t.Bottom());
        }

        [TestMethod]
        public void BottomRight_WithX3andY3_ThenX3andY4()
        {
            Tile t = TileFactory.Tile(3, 3);
            Assert.AreEqual(t.Bottom().X, 3);
            Assert.AreEqual(t.Bottom().Y, 4);
        }
        #endregion Bottom

        #region BottomLeft
        [TestMethod]
        public void BottomLeft_WithNegativeXorY_ThenNotNull()
        {
            Tile t = TileFactory.Tile(-2, -2);
            Assert.IsNotNull(t.BottomLeft());
        }

        [TestMethod]
        public void BottomLeft_WithX3andY3_ThenX2andY4()
        {
            Tile t = TileFactory.Tile(3, 3);
            Assert.AreEqual(t.BottomLeft().X, 2);
            Assert.AreEqual(t.BottomLeft().Y, 4);
        }
        #endregion BottomLeft

        #region Left
        [TestMethod]
        public void Left_WithNegativeXorY_ThenNotNull()
        {
            Tile t = TileFactory.Tile(-2, -2);
            Assert.IsNotNull(t.Left());
        }

        [TestMethod]
        public void Left_WithX3andY3_ThenX2andY3()
        {
            Tile t = TileFactory.Tile(3, 3);
            Assert.AreEqual(t.Left().X, 2);
            Assert.AreEqual(t.Left().Y, 3);
        }
        #endregion Left

        #region TopLeft
        [TestMethod]
        public void TopLeft_WithNegativeXorY_ThenNotNull()
        {
            Tile t = TileFactory.Tile(-2, -2);
            Assert.IsNotNull(t.TopLeft());
        }

        [TestMethod]
        public void TopLeft_WithX3andY3_ThenX2andY2()
        {
            Tile t = TileFactory.Tile(3, 3);
            Assert.AreEqual(t.TopLeft().X, 2);
            Assert.AreEqual(t.TopLeft().Y, 2);
        }
        #endregion TopLeft
        #endregion #region Direction

        #region IsABoat
        [TestMethod]
        public void IsABoat_OccupationTypeTitanic_True()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Titanic;
            Assert.IsTrue(t.IsABoat());
        }
        [TestMethod]
        public void IsABoat_OccupationTypeCarrier_True()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Carrier;
            Assert.IsTrue(t.IsABoat());
        }
        [TestMethod]
        public void IsABoat_OccupationTypeBattleship_True()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Battleship;
            Assert.IsTrue(t.IsABoat());
        }
        [TestMethod]
        public void IsABoat_OccupationTypeSubmarine_True()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Submarine;
            Assert.IsTrue(t.IsABoat());
        }
        [TestMethod]
        public void IsABoat_OccupationTypeCruiser_True()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Cruiser;
            Assert.IsTrue(t.IsABoat());
        }
        [TestMethod]
        public void IsABoat_OccupationTypeDesctroyer_True()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Destroyer;
            Assert.IsTrue(t.IsABoat());
        }
        [TestMethod]
        public void IsABoat_OccupationTypeEmpty_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Empty;
            Assert.IsFalse(t.IsABoat());
        }
        [TestMethod]
        public void IsABoat_OccupationTypeNear_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Near;
            Assert.IsFalse(t.IsABoat());
        }
        #endregion

        #region Available
        [TestMethod]
        public void Available_OccupationTypeTitanic_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Titanic;
            Assert.IsFalse(t.Available());
        }
        [TestMethod]
        public void Available_OccupationTypeCarrier_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Carrier;
            Assert.IsFalse(t.Available());
        }
        [TestMethod]
        public void Available_OccupationTypeBattleship_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Battleship;
            Assert.IsFalse(t.Available());
        }
        [TestMethod]
        public void Available_OccupationTypeSubmarine_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Submarine;
            Assert.IsFalse(t.Available());
        }
        [TestMethod]
        public void Available_OccupationTypeCruiser_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Cruiser;
            Assert.IsFalse(t.Available());
        }
        [TestMethod]
        public void Available_OccupationTypeDesctroyer_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Destroyer;
            Assert.IsFalse(t.Available());
        }
        [TestMethod]
        public void Available_OccupationTypeEmpty_True()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Empty;
            Assert.IsTrue(t.Available());
        }
        [TestMethod]
        public void ABoat_OccupationTypeNear_False()
        {
            Tile t = TileFactory.Tile(0, 0);
            t.OccupationType = Occupation.Near;
            Assert.IsFalse(t.Available());
        }
        #endregion IsAvailable

        #region Clone
        [TestMethod]
        public void Clone_ThenNotSameIDInRamMem()
        {
            Tile t = TileFactory.Tile(2, 2);
            Assert.IsFalse(object.ReferenceEquals(t,t.Clone()));
        }
        #endregion Clone
    }
}
