using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class IATester
    {
        [TestMethod]
        public void GetProb_Between0And40_ThenHORIZONTHAL()
        {
            IA ia = new IA();
            Assert.AreEqual(Orientation.HORIZONTHAL, ia.GetProb(Utility.Random(0, 40)));
        }
        [TestMethod]
        public void GetProb_Between40And80_ThenVERTICAL()
        {
            IA ia = new IA();
            Assert.AreEqual(Orientation.VERTICAL, ia.GetProb(Utility.Random(40, 80)));
        }
        [TestMethod]
        public void GetProb_Between80And90_ThenDIAG_BR()
        {
            IA ia = new IA();
            Assert.AreEqual(Orientation.DIAG_BR, ia.GetProb(Utility.Random(80, 90)));
        }
        [TestMethod]
        public void GetProb_Between90And100_ThenDIAG_BR()
        {
            IA ia = new IA();
            Assert.AreEqual(Orientation.DIAG_TR, ia.GetProb(Utility.Random(90, 100)));
        }

        [TestMethod]
        public void NotUsed_EmptyGameBoard_ThenCountWidthTimeHeight()
        {
            IA ia = new IA();
            ia.GameBoard = GameBoardFactory.GameBoard();
            Assert.IsTrue(ia.NotUsed().Count == ia.GameBoard.Width*ia.GameBoard.Height);
        }

        [TestMethod]
        public void NotUsed_TitanicMiddleMapGameBoard_ThenCountWidthTimeHeightMinus21()
        {
            IA ia = new IA();
            ia.GameBoard = GameBoardFactory.GameBoard();
            Ship s = ShipFactory.Titanic(TileFactory.Tile(2,2));
            ia.GameBoard.Add(s);
            Assert.IsTrue(ia.NotUsed().Count == ia.GameBoard.Width * ia.GameBoard.Height-21);
        }

        [TestMethod]
        public void PlaceShipsRandomly_AlwaysTrue()
        {
            IA ia = new IA();
            Assert.IsTrue(ia.PlaceShipsRandomly());
        }
    }
}
