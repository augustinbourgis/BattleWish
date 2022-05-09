namespace BlazorApp.Controller.Factory
{
    public static class TileFactory
    {
        public static Tile Tile(int x, int y)
        {
            Tile tile = new Tile(x,y);

            tile.OccupationType = Enums.Occupation.Empty;

            return tile;
        }
    }
}
