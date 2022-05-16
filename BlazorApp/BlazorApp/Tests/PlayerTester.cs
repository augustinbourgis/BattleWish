using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class PlayerTester
    {
        Player p = PlayerFactory.Player();
        
        
        [TestMethod]
        public void GenerateShips_AlwaysTrue()
        {
            Assert.IsTrue(p.GenerateShips());
        }

        [TestMethod]
        public void GenerateShips_OneLengt5()
        {
            Assert.IsTrue(p.Ships.Where(s => s.Width == 5).Count() == 1);
        }

        [TestMethod]
        public void GenerateShips_2Lengt4()
        {
            Assert.IsTrue(p.Ships.Where(s => s.Width == 4).Count() == 2);
        }

        [TestMethod]
        public void GenerateShips_2Lengt3()
        {
            Assert.IsTrue(p.Ships.Where(s => s.Width == 3).Count() == 2);
        }

        [TestMethod]
        public void GenerateShips_1Lengt2()
        {
            Assert.IsTrue(p.Ships.Where(s => s.Width == 2).Count() == 1);
        }
    }
}
