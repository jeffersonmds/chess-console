using System;
using Tabuleiro;
using Chess;

namespace Chess_Console
{
    class Screen
    {
        public static void printBoard(Board tab)
        {
            for(int i = 0; i < tab.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Columns; j++)
                {
                    printPiece(tab.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Board tab, bool[,] possiblePositions)
        {
            ConsoleColor defaultBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Columns; j++)
                {
                    if (possiblePositions[i, j])
                        Console.BackgroundColor = changedBackground;
                    else
                        Console.BackgroundColor = defaultBackground;
                    printPiece(tab.Piece(i, j));
                    Console.BackgroundColor = defaultBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = defaultBackground;
        }

        public static ChessPosition readPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }

        public static void printPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
