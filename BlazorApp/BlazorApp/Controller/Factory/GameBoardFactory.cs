namespace BlazorApp.Controller.Factory
{
    public static class GameBoardFactory
    {
        public static GameBoard GameBoard(int? w = null, int? h = null)
        { 
            GameBoard board = null;
            if(w.HasValue && h.HasValue)
            {
                 board = new GameBoard() { Width = w.Value, Height = h.Value };
            }
            else
            {
                 board = new GameBoard() { Height = 12, Width = 12 };
            }
            board.GenerateTiles();
            return board;
        }
    }
}
