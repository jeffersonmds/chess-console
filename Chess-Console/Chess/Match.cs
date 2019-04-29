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
        public bool Checkmate { get; private set; }

        public Match()
        {
            Tab = new Board(8, 8);
            Turno = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Checkmate = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            PutAllPieces();
        }

        public Piece DoMovement(Position origin, Position destination)
        {
            Piece p = Tab.RemovePiece(origin);
            p.IncreaseMovements();
            Piece captured = Tab.RemovePiece(destination);
            Tab.PutPiece(p, destination);
            if (captured != null)
            {
                CapturedPieces.Add(captured);
            }
            return captured;
        }

        public void UndoMovement(Position origin, Position destination, Piece captured)
        {
            Piece p = Tab.RemovePiece(destination);
            p.DecreaseMovements();
            if (captured != null)
            {
                Tab.PutPiece(captured, destination);
                CapturedPieces.Remove(captured);
            }
            Tab.PutPiece(p, origin);
        }

        public void DoPlay(Position origin, Position destination)
        {
            Piece capturada = DoMovement(origin, destination);

            if (IsCheckmate(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturada);
                throw new BoardException("Você não pode se colocar em xeque");

            }
            if (IsCheckmate(Adversary(CurrentPlayer)))
                Checkmate = true;
            else
                Checkmate = false;

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
            if (!Tab.Piece(pos).HaveAvailableMovements())
            {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidadeDestinationPosition(Position origin, Position destination)
        {
            if (!Tab.Piece(origin).CanMoveTo(destination))
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

        private Color Adversary(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece King (Color color)
        {
            foreach(Piece x in PiecesInGame(color))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsCheckmate(Color color)
        {
            Piece R = King(color);
            if (R == null)
            {
                throw new BoardException("Não tem rei da cor " + color + " no tabuleiro");
            }
            foreach(Piece x in PiecesInGame(Adversary(color)))
            {
                bool[,] mat = x.AvailableMovements();
                if (mat[R.Position.Row, R.Position.Column])
                    return true;
            }
            return false;
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
