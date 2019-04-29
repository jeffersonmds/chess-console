using Tabuleiro;

namespace Chess
{
    class Cavalo : Piece
    {
        public Cavalo(Color color, Board tab) : base(color, tab)
        {
        }

        public override string ToString()
        {
            return "C";
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

            pos.defineValues(Position.Row - 1, Position.Column - 2);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))            
                mat[pos.Row, pos.Column] = true;

            pos.defineValues(Position.Row - 2, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
                mat[pos.Row, pos.Column] = true;

            pos.defineValues(Position.Row - 2, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
                mat[pos.Row, pos.Column] = true;

            pos.defineValues(Position.Row - 1, Position.Column + 2);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
                mat[pos.Row, pos.Column] = true;

            pos.defineValues(Position.Row + 1, Position.Column + 2);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
                mat[pos.Row, pos.Column] = true;

            pos.defineValues(Position.Row + 2, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
                mat[pos.Row, pos.Column] = true;

            pos.defineValues(Position.Row + 2, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
                mat[pos.Row, pos.Column] = true;

            pos.defineValues(Position.Row + 1, Position.Column - 2);
            if (Tab.IsValidPosition(pos) && IsAvailabe(pos))
                mat[pos.Row, pos.Column] = true;

            return mat;
        }
    }
}
