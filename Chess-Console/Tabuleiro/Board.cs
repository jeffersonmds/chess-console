namespace Tabuleiro
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }

        public Piece Piece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Piece Piece(Position pos)
        {
            return Pieces[pos.Row, pos.Column];
        }

        public bool pieceExists(Position pos)
        {
            validatePosition(pos);
            return Piece(pos) != null;
        }

        public void putPiece(Piece p, Position pos)
        {
            if (pieceExists(pos))
            {
                throw new BoardException("Já existe peça nessa posição");
            }
            Pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        public bool isValidPosition(Position pos)
        {
            if (pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns)
                return false;
            return true;
        }

        public void validatePosition(Position pos)
        {
            if (!isValidPosition(pos))
            {
                throw new BoardException("Posição inválida");
            }
        }
    }
}
