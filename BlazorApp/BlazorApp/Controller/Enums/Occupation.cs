using System.ComponentModel;

namespace BlazorApp.Controller.Enums
{
    public enum Occupation
    {
        [Description("E")]
        Empty,
        [Description("N")]
        Near,

        #region Ships
        [Description("D")]
        Destroyer,
        [Description("S")]
        Submarine,
        [Description("Cr")]
        Cruiser,
        [Description("B")]
        Battleship,
        [Description("Ca")]
        Carrier,
        [Description("T")]
        Titanic
        #endregion
    }
}
