using System;
using Tabuleiro;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board tab = new Board(8,8);

            Screen.printBoard(tab);
            Position P; 
        }
    }
}
