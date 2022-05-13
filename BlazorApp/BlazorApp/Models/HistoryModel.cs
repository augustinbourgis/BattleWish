namespace BlazorApp.Models
{
    public class HistoryModel
    {
        public int IdHistory { get; set; }
        public string IALevel { get; set; }
        public bool VictoryForPlayer { get; set; }
        public int IAShoot { get; set; }
        public int PlayerShoot { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string PlayerPseudo { get; set; }

    }
}
