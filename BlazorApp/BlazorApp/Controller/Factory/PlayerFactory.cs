namespace BlazorApp.Controller.Factory
{
    public static class PlayerFactory
    {
        public static Player Player()
        {
            Player p = new Player("factory made");
            return p;
        }

        public static Player Player(string name)
        {
            return new Player(name);
        }
    }
}
