using Tabuleiro;

namespace Chess
{
    class Peao : Piece
    {
        public Peao(Color color, Board tab) : base(color, tab)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool HaveEnemy(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Tab.Piece(pos) == null;
        }

        public override bool[,] AvailableMovements()
        {
            bool[,] mat = new bool[Tab.Rows, Tab.Columns];
            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.defineValues(Position.Row - 1, Position.Column);
                if (Tab.IsValidPosition(pos) && Free(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row - 2, Position.Column);
                if (Tab.IsValidPosition(pos) && Free(pos) && Movements == 0)
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row - 1, Position.Column - 1);
                if (Tab.IsValidPosition(pos) && HaveEnemy(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row - 1, Position.Column + 1);
                if (Tab.IsValidPosition(pos) && HaveEnemy(pos))
                    mat[pos.Row, pos.Column] = true;
            }
            else
            {
                pos.defineValues(Position.Row + 1, Position.Column);
                if (Tab.IsValidPosition(pos) && Free(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row + 2, Position.Column);
                if (Tab.IsValidPosition(pos) && Free(pos) && Movements == 0)
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row + 1, Position.Column - 1);
                if (Tab.IsValidPosition(pos) && HaveEnemy(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row + 1, Position.Column + 1);
                if (Tab.IsValidPosition(pos) && HaveEnemy(pos))
                    mat[pos.Row, pos.Column] = true;
            }


            return mat;
        }
    }
}
