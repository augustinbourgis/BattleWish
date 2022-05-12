using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
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
    }
}
