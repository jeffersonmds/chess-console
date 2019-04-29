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

    }
}
