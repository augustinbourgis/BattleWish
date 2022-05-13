namespace BlazorApp.Controller
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Identification
    {
        public Player? Current { get; set; }
        public Player? Player;

        [Required]
        [StringLength(20, ErrorMessage = "Login too long (20 character limit).")]
        public string? Login { get; set; }

        [Required]
        public string? Password { get; set; }

        public Boolean Connected { get { return Current != null; }  }

        // TEST
        public Boolean LogIn(string login, bool isAdmin, bool isMale)
        {
            Login = login;
            Current = new Player(Login, isAdmin, isMale);
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
