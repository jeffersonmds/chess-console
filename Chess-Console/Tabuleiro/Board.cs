namespace Tabuleiro
{
    class Board
    {
        public int Rows { get; set; }
        public int Colums { get; set; }
        private Piece[,] Pieces;

        public Board(int rows, int colums)
        {
            Rows = rows;
            Colums = colums;
            Pieces = new Piece[rows, colums];
        }


    }
}
