namespace BlazorApp.Controller
{
    public class Identification
    {
        public Player Current { get; set; }
        public string Login;
        public string Password;
        public Player Player;

        public Boolean Connected { get { return Current != null; }  }

        // TEST
        public Boolean LogIn()
        {
            if(Login == "test" && Password == "test")
            {
                Current = new Player(Login);
            }
            return Connected;
        }

        // TEST
        public Boolean Deconnect()
        {
            Login = null;
            Password = null;
            Current = null;
            return !Connected;
        }
    }
}
