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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.ReadPosition().toPosition();
                        match.ValidadeOriginPosition(origin);

                        bool[,] possiblePositions = match.Tab.Piece(origin).AvailableMovements();


                        Console.Clear();
                        Screen.PrintBoard(match.Tab, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destination = Screen.ReadPosition().toPosition();
                        match.ValidadeDestinationPosition(origin, destination);

                        match.DoPlay(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintMatch(match);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
