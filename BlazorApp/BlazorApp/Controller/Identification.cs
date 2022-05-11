using BlazorApp.Controller.Enums;

namespace BlazorApp.Controller
{
    public class Identification
    {
        public Player Current { get; set; }
        public string Login;
        public string Password;
        public Player Player;
        public Game Game;
        public Difficulties Difficulty = Difficulties.FACILE;
        public IA Ia { get; set; }

        public Boolean Connected { get { return Current != null; }  }

        // TEST
        public Boolean LogIn()
        {
            if(Login == "test" && Password == "test")
            {
                Current = new Player(Login);
            }
            Ia = new IA();
            Game = new Game();
            Game.Ia = Ia;
            return Connected;
        }

        // TEST
        public Boolean Deconnect()
        {
            Login = null;
            Password = null;
            Current = null;
            Ia = null;
            Game = null;
            return !Connected;
        }
    }
}
