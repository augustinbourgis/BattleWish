namespace BlazorApp.Models
{
    public class HistoryModel
    {
        public int IdHistory { get; set; }
        public string IALevel { get; set; }
        private string _victoryForPlayer;
        public string VictoryForPlayer
        {
            get => _victoryForPlayer;
            set
            {
                if (value == "True")
                {
                    _victoryForPlayer = "Victory";
                }
                else
                {
                    _victoryForPlayer = "Defeat";
                }
            }
        }
        public int IAShoot { get; set; }
        public int PlayerShoot { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string PlayerPseudo { get; set; }
        public TimeSpan GameTime { get; set; }
        

    }
}
