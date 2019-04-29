using Tabuleiro;

namespace Chess
{
    class Rei : Piece
    {
        public Match Match { get; set; }

        public Rei(Color color, Board tab, Match match) : base(color, tab)
        {
            Match = match;
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

        private bool TestTorreRoque(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p != null && p is Torre && p.Color == Color && p.Movements == 0;
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
            pos.defineValues(Position.Row, Position.Column + 1);
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

            // #jogadaespecial roque
            if (Movements == 0 && !Match.Check)
            {
                // #jogadaespecial roque pequeno
                Position posT1 = new Position(Position.Row, Position.Column + 3);
                if (TestTorreRoque(posT1))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Tab.Piece(p1) == null && Tab.Piece(p2) == null)
                    {
                        mat[Position.Row, Position.Column + 2] = true;
                    }
                }

                // #jogadaespecial roque grande
                Position posT2 = new Position(Position.Row, Position.Column - 4);
                if (TestTorreRoque(posT2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Tab.Piece(p1) == null && Tab.Piece(p2) == null && Tab.Piece(p3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
