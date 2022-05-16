using BlazorApp.Controller;
using BlazorApp.Controller.Enums;
using BlazorApp.Controller.Factory;
using BlazorApp.Controller.Ships;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class IdentificationTester
    {
        [TestMethod]
        public void Connect_AlwaysTrue()
        {
            var identification = new Identification();
            Assert.IsTrue(identification.LogIn("de la merde", false, false));
        }

        [TestMethod]
        public void Deconnect_AlwaysTrue()
        {
            var identification = new Identification();
            Assert.IsTrue(identification.Deconnect());
        }
    }
}
