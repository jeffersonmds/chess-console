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
                Board tab = new Board(8, 8);
                tab.putPiece(new Torre(Color.Black, tab), new Position(0, 0));
                tab.putPiece(new Torre(Color.Black, tab), new Position(1, 3));
                tab.putPiece(new Rei(Color.Black, tab), new Position(2, 4));

                Screen.printBoard(tab);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
