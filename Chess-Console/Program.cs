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
                        Screen.printBoard(match.tab);
                        Console.WriteLine("\nTurno: " + match.Turno);
                        Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);


                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.readPosition().toPosition();
                        match.validadeOriginPosition(origin);

                        bool[,] possiblePositions = match.tab.Piece(origin).availableMovements();


                        Console.Clear();
                        Screen.printBoard(match.tab, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destination = Screen.readPosition().toPosition();
                        match.validadeDestinationPosition(origin, destination);

                        match.doPlay(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
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
