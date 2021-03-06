﻿namespace Tabuleiro
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

        public void IncreaseMovements()
        {
            Movements++;
        }

        public void DecreaseMovements()
        {
            Movements--;
        }

        public bool HaveAvailableMovements()
        {
            bool[,] mat = AvailableMovements();
            for (int i = 0; i < Tab.Rows; i++)
            {
                for (int j = 0; j < Tab.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return AvailableMovements()[pos.Row, pos.Column];
        }

        public abstract bool[,] AvailableMovements();
    }
}
