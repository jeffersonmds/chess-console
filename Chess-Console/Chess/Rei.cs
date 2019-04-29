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

    }
}
