namespace Tabuleiro
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int Movements { get; protected set; }
        public Board Tab { get; set; }

        public Piece(Color color, Board tab)
        {
            Position = null;
            Color = color;
            Tab = tab;
            this.Movements = 0;
        }

        public void increaseMovements()
        {
            Movements++;
        }

        public abstract bool[,] availableMovements();
    }
}
