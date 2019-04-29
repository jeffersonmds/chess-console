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
        public bool Check { get; private set; }

        public Match()
        {
            Tab = new Board(8, 8);
            Turno = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
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

            // #jogadaespecial roque pequeno
            if (p is Rei && destination.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Row, origin.Column + 3);
                Position destinationT = new Position(origin.Row, origin.Column + 1);
                Piece T = Tab.RemovePiece(originT);
                T.IncreaseMovements();
                Tab.PutPiece(T, destinationT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destination.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Row, origin.Column - 4);
                Position destinationT = new Position(origin.Row, origin.Column - 1);
                Piece T = Tab.RemovePiece(originT);
                T.IncreaseMovements();
                Tab.PutPiece(T, destinationT);
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

            // #jogadaespecial roque pequeno
            if (p is Rei && destination.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Row, origin.Column + 3);
                Position destinationT = new Position(origin.Row, origin.Column + 1);
                Piece T = Tab.RemovePiece(destinationT);
                T.DecreaseMovements();
                Tab.PutPiece(T, originT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destination.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Row, origin.Column - 4);
                Position destinationT = new Position(origin.Row, origin.Column - 1);
                Piece T = Tab.RemovePiece(destinationT);
                T.DecreaseMovements();
                Tab.PutPiece(T, originT);
            }
        }

        public void DoPlay(Position origin, Position destination)
        {
            Piece capturada = DoMovement(origin, destination);
            if (IsCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturada);
                throw new BoardException("Você não pode se colocar em xeque");

            }
            if (IsCheck(Adversary(CurrentPlayer)))
                Check = true;
            else
                Check = false;
            if (CheckmateTest(Adversary(CurrentPlayer)))
                Finished = true;
            else
            {
                Turno++;
                ChangeCurrentPlayer();
            }
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

        public bool IsCheck(Color color)
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

        public bool CheckmateTest(Color color)
        {
            if (!IsCheck(color))
                return false;
            foreach (Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.AvailableMovements();
                for (int i = 0; i < Tab.Rows; i++)
                {
                    for (int j = 0; j < Tab.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destination = new Position(i, j);
                            Piece captured = DoMovement(origin, destination);
                            bool checktest = IsCheck(color);
                            UndoMovement(origin, destination, captured);
                            if (!checktest)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int row, Piece piece)
        {
            Tab.PutPiece(piece, new ChessPosition(column, row).toPosition());
            Pieces.Add(piece);
        }

        private void PutAllPieces()
        {
            PutNewPiece('a', 1, new Torre(Color.White, Tab));
            PutNewPiece('b', 1, new Cavalo(Color.White, Tab));
            PutNewPiece('c', 1, new Bispo(Color.White, Tab));
            PutNewPiece('d', 1, new Dama(Color.White, Tab));
            PutNewPiece('e', 1, new Rei(Color.White, Tab, this));
            PutNewPiece('f', 1, new Bispo(Color.White, Tab));
            PutNewPiece('g', 1, new Cavalo(Color.White, Tab));
            PutNewPiece('h', 1, new Torre(Color.White, Tab));
            PutNewPiece('a', 2, new Peao(Color.White, Tab));
            PutNewPiece('b', 2, new Peao(Color.White, Tab));
            PutNewPiece('c', 2, new Peao(Color.White, Tab));
            PutNewPiece('d', 2, new Peao(Color.White, Tab));
            PutNewPiece('e', 2, new Peao(Color.White, Tab));
            PutNewPiece('f', 2, new Peao(Color.White, Tab));
            PutNewPiece('g', 2, new Peao(Color.White, Tab));
            PutNewPiece('h', 2, new Peao(Color.White, Tab));

            PutNewPiece('a', 8, new Torre(Color.Black, Tab));
            PutNewPiece('b', 8, new Cavalo(Color.Black, Tab));
            PutNewPiece('c', 8, new Bispo(Color.Black, Tab));
            PutNewPiece('d', 8, new Dama(Color.Black, Tab));
            PutNewPiece('e', 8, new Rei(Color.Black, Tab, this));
            PutNewPiece('f', 8, new Bispo(Color.Black, Tab));
            PutNewPiece('g', 8, new Cavalo(Color.Black, Tab));
            PutNewPiece('h', 8, new Torre(Color.Black, Tab));
            PutNewPiece('a', 7, new Peao(Color.Black, Tab));
            PutNewPiece('b', 7, new Peao(Color.Black, Tab));
            PutNewPiece('c', 7, new Peao(Color.Black, Tab));
            PutNewPiece('d', 7, new Peao(Color.Black, Tab));
            PutNewPiece('e', 7, new Peao(Color.Black, Tab));
            PutNewPiece('f', 7, new Peao(Color.Black, Tab));
            PutNewPiece('g', 7, new Peao(Color.Black, Tab));
            PutNewPiece('h', 7, new Peao(Color.Black, Tab));
        }
    }
}
