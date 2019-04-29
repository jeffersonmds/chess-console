using Tabuleiro;

namespace Chess
{
    class Rei : Piece
    {
        public Rei(Color color, Board tab) : base(color, tab)
        {
        }
        public override string ToString()
        {
            return "R";
        }

        private bool IsAvailabe(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] AvailableMovements()
        {
            bool[,] mat = new bool[Tab.Rows, Tab.Columns];
            Position pos = new Position(0, 0);

            // acima
            pos.defineValues(Position.Row - 1, Position.Column);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // ne
            pos.defineValues(Position.Row - 1, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // direita
            pos.defineValues(Position.Row, Position.Column);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // se
            pos.defineValues(Position.Row + 1, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // abaixo
            pos.defineValues(Position.Row + 1, Position.Column);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // so
            pos.defineValues(Position.Row + 1, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // esquerda
            pos.defineValues(Position.Row, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // no
            pos.defineValues(Position.Row - 1, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat;
        }
    }
}
