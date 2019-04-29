namespace Tabuleiro
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int Movements { get; protected set; }
        public Board Tab { get; set; }

        public Piece(Position position, Color color, Board tab)
        {
            Position = position;
            Color = color;
            Tab = tab;
            this.Movements = 0;
        }
    }
}
