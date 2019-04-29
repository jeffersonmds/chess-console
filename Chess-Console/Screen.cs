using System;
using System.Collections.Generic;
using Tabuleiro;
using Chess;

namespace Chess_Console
{
    class Screen
    {
        public static void PrintMatch(Match match)
        {
            PrintBoard(match.Tab);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine("\nTurno: " + match.Turno);
            if (!match.Finished)
            {
                Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);
                if (match.Check)
                {
                    Console.WriteLine("XEQUE");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!!");
                Console.WriteLine("Vencedor: " + match.CurrentPlayer);
            }
        }

        public static void PrintCapturedPieces(Match match)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            PrintSet(match.CapturedPiecesList(Color.White));
            Console.Write("\nPretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.CapturedPiecesList(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[ ");
            foreach(Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board tab)
        {
            for(int i = 0; i < tab.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Columns; j++)
                {
                    PrintPiece(tab.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board tab, bool[,] possiblePositions)
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
                    PrintPiece(tab.Piece(i, j));
                    Console.BackgroundColor = defaultBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = defaultBackground;
        }

        public static ChessPosition ReadPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }

        public static void PrintPiece(Piece piece)
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
