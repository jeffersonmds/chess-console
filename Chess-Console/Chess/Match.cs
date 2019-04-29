using System.Collections.Generic;
using Tabuleiro;

namespace Chess
{
    class Match
    {
        public Board Tab { get; private set; }
        public int Turno { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces { get; set; }
        private HashSet<Piece> CapturedPieces { get; set; }

        public Match()
        {
            Tab = new Board(8, 8);
            Turno = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            PutAllPieces();
        }

        public void DoMovement(Position origin, Position destination)
        {
            Piece p = Tab.RemovePiece(origin);
            p.increaseMovements();
            Piece captured = Tab.RemovePiece(destination);
            Tab.PutPiece(p, destination);
            if (captured != null)
            {
                CapturedPieces.Add(captured);
            }
        }

        public void DoPlay(Position origin, Position destination)
        {
            DoMovement(origin, destination);
            Turno++;
            ChangeCurrentPlayer();
        }

        public void ValidadeOriginPosition(Position pos)
        {
            if (Tab.Piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != Tab.Piece(pos).Color)
            {
                throw new BoardException("A peça escolhida não é sua!");
            }
            if (!Tab.Piece(pos).haveAvailableMovements())
            {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidadeDestinationPosition(Position origin, Position destination)
        {
            if (!Tab.Piece(origin).canMoveTo(destination))
            {
                throw new BoardException("Posição de destino inválida!");
            }
        }

        private void ChangeCurrentPlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        public HashSet<Piece> CapturedPiecesList(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in CapturedPieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPiecesList(color));
            return aux;
        }

        public void PutNewPiece(char column, int row, Piece piece)
        {
            Tab.PutPiece(piece, new ChessPosition(column, row).toPosition());
            Pieces.Add(piece);
        }

        private void PutAllPieces()
        {
            PutNewPiece('c', 1, new Torre(Color.White, Tab));
            PutNewPiece('c', 2, new Torre(Color.White, Tab));
            PutNewPiece('d', 2, new Torre(Color.White, Tab));
            PutNewPiece('e', 2, new Torre(Color.White, Tab));
            PutNewPiece('e', 1, new Torre(Color.White, Tab));
            PutNewPiece('d', 1, new Rei(Color.White, Tab));

            PutNewPiece('c', 7, new Torre(Color.Black, Tab));
            PutNewPiece('c', 8, new Torre(Color.Black, Tab));
            PutNewPiece('d', 7, new Torre(Color.Black, Tab));
            PutNewPiece('e', 7, new Torre(Color.Black, Tab));
            PutNewPiece('e', 8, new Torre(Color.Black, Tab));
            PutNewPiece('d', 8, new Rei(Color.Black, Tab));
        }
    }
}
