using System;
using Tabuleiro;

namespace Chess_Console
{
    class Screen
    {
        public static void printBoard(Board tab)
        {
            for(int i = 0; i < tab.Rows; i++)
            {
                for (int j = 0; j < tab.Columns; j++)
                {
                    if (tab.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.Piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
