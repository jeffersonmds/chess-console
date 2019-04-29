using System;
using Tabuleiro;

namespace Chess
{
    class Match
    {
        public Board tab { get; private set; }
        public int Turno { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public Match()
        {
            tab = new Board(8, 8);
            Turno = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            putAllPieces();
        }

        public void doMovement(Position origin, Position destination)
        {
            Piece p = tab.removePiece(origin);
            p.increaseMovements();
            Piece captured = tab.removePiece(destination);
            tab.putPiece(p, destination);
        }

        public void doPlay(Position origin, Position destination)
        {
            doMovement(origin, destination);
            Turno++;
            changeCurrentPlayer();
        }

        public void validadeOriginPosition(Position pos)
        {
            if (tab.Piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != tab.Piece(pos).Color)
            {
                throw new BoardException("A peça escolhida não é sua!");
            }
            if (!tab.Piece(pos).haveAvailableMovements())
            {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validadeDestinationPosition(Position origin, Position destination)
        {
            if (!tab.Piece(origin).canMoveTo(destination))
            {
                throw new BoardException("Posição de destino inválida!");
            }
        }

        private void changeCurrentPlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        private void putAllPieces()
        {
            tab.putPiece(new Torre(Color.White, tab), new ChessPosition('c', 1).toPosition());
            tab.putPiece(new Torre(Color.White, tab), new ChessPosition('c', 2).toPosition());
            tab.putPiece(new Torre(Color.White, tab), new ChessPosition('d', 2).toPosition());
            tab.putPiece(new Torre(Color.White, tab), new ChessPosition('e', 2).toPosition());
            tab.putPiece(new Torre(Color.White, tab), new ChessPosition('e', 1).toPosition());
            tab.putPiece(new Rei(Color.White, tab), new ChessPosition('d', 1).toPosition());

            tab.putPiece(new Torre(Color.Black, tab), new ChessPosition('c', 7).toPosition());
            tab.putPiece(new Torre(Color.Black, tab), new ChessPosition('c', 8).toPosition());
            tab.putPiece(new Torre(Color.Black, tab), new ChessPosition('d', 7).toPosition());
            tab.putPiece(new Torre(Color.Black, tab), new ChessPosition('e', 7).toPosition());
            tab.putPiece(new Torre(Color.Black, tab), new ChessPosition('e', 8).toPosition());
            tab.putPiece(new Rei(Color.Black, tab), new ChessPosition('d', 8).toPosition());

        }
    }
}
