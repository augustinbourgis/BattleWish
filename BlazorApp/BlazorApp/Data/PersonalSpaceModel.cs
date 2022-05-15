namespace BlazorApp.Data
{
    public class PersonalSpaceModel
    {
        public string? IALevel { get; set; }

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

        private TimeSpan _gameTime;

        public TimeSpan GameTime
        {
            get => _gameTime;
            set
            {
                _gameTime = End.Subtract(Begin);
            }
        }

    }
}