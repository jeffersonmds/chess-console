using Tabuleiro;

namespace Chess
{
    class Peao : Piece
    {
        private Match match;

        public Peao(Color color, Board tab, Match match) : base(color, tab)
        {
            this.match = match;
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
                Position p2 = new Position(Position.Row - 1, Position.Column);
                if (Tab.IsValidPosition(p2) && Free(p2) && Tab.IsValidPosition(pos) && Free(pos) && Movements == 0)
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row - 1, Position.Column - 1);
                if (Tab.IsValidPosition(pos) && HaveEnemy(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row - 1, Position.Column + 1);
                if (Tab.IsValidPosition(pos) && HaveEnemy(pos))
                    mat[pos.Row, pos.Column] = true;

                // #jogadaespecial en passant
                if (Position.Row == 3)
                {
                    Position esquerda = new Position(Position.Row, Position.Column - 1);
                    if (Tab.IsValidPosition(esquerda) && HaveEnemy(esquerda) && Tab.Piece(esquerda) == match.VulnerableEnPassant)
                    {
                        mat[esquerda.Row - 1, esquerda.Column] = true;
                    }
                    Position direita = new Position(Position.Row, Position.Column + 1);
                    if (Tab.IsValidPosition(direita) && HaveEnemy(direita) && Tab.Piece(direita) == match.VulnerableEnPassant)
                    {
                        mat[direita.Row - 1, direita.Column] = true;
                    }
                }
                    
            }
            else
            {
                pos.defineValues(Position.Row + 1, Position.Column);
                if (Tab.IsValidPosition(pos) && Free(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row + 2, Position.Column);
                Position p2 = new Position(Position.Row + 1, Position.Column);
                if (Tab.IsValidPosition(p2) && Free(p2) && Tab.IsValidPosition(pos) && Free(pos) && Movements == 0)
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row + 1, Position.Column - 1);
                if (Tab.IsValidPosition(pos) && HaveEnemy(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.defineValues(Position.Row + 1, Position.Column + 1);
                if (Tab.IsValidPosition(pos) && HaveEnemy(pos))
                    mat[pos.Row, pos.Column] = true;

                // #jogadaespecial en passant
                if (Position.Row == 4)
                {
                    Position esquerda = new Position(Position.Row, Position.Column - 1);
                    if (Tab.IsValidPosition(esquerda) && HaveEnemy(esquerda) && Tab.Piece(esquerda) == match.VulnerableEnPassant)
                    {
                        mat[esquerda.Row + 1, esquerda.Column] = true;
                    }
                    Position direita = new Position(Position.Row, Position.Column + 1);
                    if (Tab.IsValidPosition(direita) && HaveEnemy(direita) && Tab.Piece(direita) == match.VulnerableEnPassant)
                    {
                        mat[direita.Row + 1, direita.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
