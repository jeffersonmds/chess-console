using System;
using Tabuleiro;
using Chess;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Match match = new Match();
                while (!match.Finished)
                {
                    Console.Clear();
                    Screen.printBoard(match.tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Screen.readPosition().toPosition();

                    bool[,] possiblePositions = match.tab.Piece(origin).availableMovements();


                    Console.Clear();
                    Screen.printBoard(match.tab, possiblePositions);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Position destination = Screen.readPosition().toPosition();
                    match.doMovement(origin, destination);

                }

                Screen.printBoard(match.tab);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
