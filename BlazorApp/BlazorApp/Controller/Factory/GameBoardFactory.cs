namespace BlazorApp.Controller.Factory
{
    public static class GameBoardFactory
    {
        public static GameBoard GameBoard(int w, int h)
        {
            GameBoard board = new GameBoard(w, h);

            board.GenerateTiles();

            return board;
        }

        public static GameBoard GameBoard()
        {
            return GameBoard(12, 12);
        }
    }
}
