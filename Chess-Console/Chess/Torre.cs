using Tabuleiro;

namespace Chess
{
    class Torre : Piece
    {
        public Torre(Color color, Board tab) : base(color, tab)
        {
        }
        public override string ToString()
        {
            return "T";
        }

        private bool isAvailabe(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] availableMovements()
        {
            bool[,] mat = new bool[Tab.Rows, Tab.Columns];
            Position pos = new Position(0, 0);

            // acima
            pos.defineValues(Position.Row - 1, Position.Column);
            while(Tab.IsValidPosition(pos) && isAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if(Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)           
                    break;
                pos.Row = pos.Row - 1;
            }

            // abaixo
            pos.defineValues(Position.Row + 1, Position.Column);
            while (Tab.IsValidPosition(pos) && isAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.Row = pos.Row + 1;
            }

            // direita
            pos.defineValues(Position.Row, Position.Column + 1);
            while (Tab.IsValidPosition(pos) && isAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.Column = pos.Column + 1;
            }

            // esquerda
            pos.defineValues(Position.Row, Position.Column - 1);
            while (Tab.IsValidPosition(pos) && isAvailabe(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    break;
                pos.Column = pos.Column - 1;
            }
            return mat;
        }
    }
}
