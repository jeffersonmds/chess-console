using Tabuleiro;

namespace Chess
{
    class Dama : Piece
    {
        public Dama(Color color, Board tab) : base(color, tab)
        {
        }

        public override string ToString()
        {
            return "D";
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
            while (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.Row = pos.Row - 1;
            }

            // abaixo
            pos.defineValues(Position.Row + 1, Position.Column);
            while (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.Row = pos.Row + 1;
            }

            // direita
            pos.defineValues(Position.Row, Position.Column + 1);
            while (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.Column = pos.Column + 1;
            }

            // esquerda
            pos.defineValues(Position.Row, Position.Column - 1);
            while (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.Column = pos.Column - 1;
            }

            // no
            pos.defineValues(Position.Row - 1, Position.Column - 1);
            while (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.defineValues(Position.Row - 1, Position.Column - 1);
            }

            // ne
            pos.defineValues(Position.Row - 1, Position.Column + 1);
            while (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.defineValues(Position.Row - 1, Position.Column + 1);
            }

            // se
            pos.defineValues(Position.Row + 1, Position.Column + 1);
            while (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.defineValues(Position.Row + 1, Position.Column + 1);
            }

            // so
            pos.defineValues(Position.Row + 1, Position.Column - 1);
            while (Tab.IsValidPosition(pos) && IsAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.defineValues(Position.Row + 1, Position.Column - 1);
            }

            return mat;
        }
    }
}
