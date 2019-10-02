using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewOthelo
{
    public class Game
    {
        private Board m_GameBoard;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private List<Square> m_PotenitalMovesForPlayers;
        private Random m_RndForPc;

       public Board GameBoard
        {
            get { return m_GameBoard; }
        }

        public Player FirstPlayer
        {
            get { return m_FirstPlayer; }
        }

        public Player SecondPlayer
        {
            get { return m_SecondPlayer; }
        }

        public List<Square> PotenitalMovesForPlayers
        {
            get { return m_PotenitalMovesForPlayers; }
        }

        public Game(bool i_IsSecondUserPC, int i_SizeOfBoard)
        {
            int numOfSquareOnBoard = 2;
            m_FirstPlayer = new Player(false, 'X', true, numOfSquareOnBoard);
            m_SecondPlayer = new Player(i_IsSecondUserPC, 'O', false, numOfSquareOnBoard);
            m_GameBoard = new Board(i_SizeOfBoard, i_SizeOfBoard);
            m_RndForPc = new Random();
            m_PotenitalMovesForPlayers = new List<Square>();
            m_GameBoard.InitializeTheFirstDefaultMoves(m_GameBoard, i_SizeOfBoard);
            m_FirstPlayer.InitializeFirstMovesForFirstPlayer(i_SizeOfBoard);
            m_SecondPlayer.InitializeFirstMovesForSecondPlayer(i_SizeOfBoard);
        }

        public void GetActionFromComputer()
        {
            Square currentSquare = new Square(getSquareForComputer());
            PerformAction(currentSquare);
        }

        public void PerformAction(Square i_currentSquare)
        {
            updateTheSelectedSquare(i_currentSquare);
            navigateToEightDirections(i_currentSquare, false);
        }

        public bool IsMoveExist()
        {
            Player nextPlayerToPlay;
            bool isPotentialMoveExists = true;
            if (m_FirstPlayer.PlayerActivity)
            {
                nextPlayerToPlay = m_FirstPlayer;
            }
            else
            {
                nextPlayerToPlay = m_SecondPlayer;
            }

            buildPotentialMovesList(nextPlayerToPlay.ListOfSquares);
            isPotentialMoveExists = false;
            if (m_PotenitalMovesForPlayers.Count() != 0)
            {
                isPotentialMoveExists = true;
            }

            return isPotentialMoveExists;
        }

        private void buildPotentialMovesList(List<Square> i_PlayerCurrentSqaursOnBoard)
        {
            m_PotenitalMovesForPlayers.Clear();
            m_PotenitalMovesForPlayers = new List<Square>();
            foreach (Square currentPlayerSquare in i_PlayerCurrentSqaursOnBoard)
            {
                navigateToEightDirections(currentPlayerSquare, true);
            }
        }

        private bool checkTheSelectedSquare(Square i_SquareFromTheUser)
        {
            bool resultOfScan = true;
            foreach (Square currentSquarInArray in m_PotenitalMovesForPlayers)
            {
                if (currentSquarInArray.isEqual(i_SquareFromTheUser))
                {
                    resultOfScan = false;
                    break;
                }
            }

            return !resultOfScan;
        }

        private void addPotenitalSquareIntoTheList(Square i_PotenitalSqaureForUser)
        {
            m_PotenitalMovesForPlayers.Add(i_PotenitalSqaureForUser);
        }

        private void updateTheSelectedSquare(Square i_SquareFromTheUser)
        {
            int rows = i_SquareFromTheUser.RowSquare;
            int column = i_SquareFromTheUser.ColSquare;
            if (m_FirstPlayer.PlayerActivity)
            {
                m_GameBoard.BoardMat[rows, column] = 'X';
                m_FirstPlayer.ListOfSquares.Add(i_SquareFromTheUser);
            }
            else
            {
                m_GameBoard.BoardMat[rows, column] = 'O';
                m_SecondPlayer.ListOfSquares.Add(i_SquareFromTheUser);
            }
        }

        private void navigateToEightDirections(Square i_CurrentPlayerSquare, bool i_IsBuildArrayOrUpdateTheBoard)
        {
            bool moveIsOver = true;
            int i, j, currentTempColumn, currentTempRow, movementDirectionOfRow, movementDirectionOfColumn;
            int orignialRow = i_CurrentPlayerSquare.RowSquare;
            int orignialColumn = i_CurrentPlayerSquare.ColSquare;
            for (i = orignialRow - 1; i <= orignialRow + 1; i++)
            {
                for (j = orignialColumn - 1; j <= orignialColumn + 1; j++)
                {
                    if (i < 0 || j < 0 || i > m_GameBoard.NumOfRows - 1 || j > m_GameBoard.NumOFColumns - 1)
                    {
                        continue;
                    }

                    if (i == orignialRow && j == orignialColumn)
                    {
                        continue;
                    }

                    if (m_GameBoard.BoardMat[i, j] == m_GameBoard.BoardMat[orignialRow, orignialColumn] || m_GameBoard.BoardMat[i, j] == 0)
                    {
                        continue;
                    }

                    movementDirectionOfRow = i - orignialRow;
                    movementDirectionOfColumn = j - orignialColumn;
                    currentTempColumn = j + movementDirectionOfColumn;
                    currentTempRow = i + movementDirectionOfRow;
                    moveIsOver = false;
                    while (!moveIsOver)
                    {
                        if (currentTempRow >= 0 && currentTempColumn >= 0 && currentTempRow < m_GameBoard.NumOfRows && currentTempColumn < m_GameBoard.NumOFColumns)
                        {
                            if (m_GameBoard.BoardMat[i, j].Equals(m_GameBoard.BoardMat[currentTempRow, currentTempColumn]))
                            {
                                currentTempColumn += movementDirectionOfColumn;
                                currentTempRow += movementDirectionOfRow;
                                continue;
                            }
                            else
                            {
                                moveIsOver = true;
                                if ((m_GameBoard.BoardMat[currentTempRow, currentTempColumn] == 0) && i_IsBuildArrayOrUpdateTheBoard)
                                {
                                    addToPotenitalSquareArray(currentTempRow, currentTempColumn);
                                }
                                else if (!i_IsBuildArrayOrUpdateTheBoard && (m_GameBoard.BoardMat[currentTempRow, currentTempColumn] == 0))
                                {
                                    continue;
                                }
                                else if (i_IsBuildArrayOrUpdateTheBoard)
                                {
                                    continue;
                                }
                                else
                                {
                                    updateTheNewSquareOnBoard(currentTempRow, currentTempColumn, movementDirectionOfRow * (-1), movementDirectionOfColumn * (-1));
                                }
                            }
                        }
                        else
                        {
                            moveIsOver = true;
                        }
                    }
                }
            }
        }

        private void addToPotenitalSquareArray(int i_CurrentTempRow, int i_CurrentTempColumn)
        {
            bool isSquareAlreadyInList = true;
            Square potentialSquareForUser = new Square(i_CurrentTempRow, i_CurrentTempColumn);
            isSquareAlreadyInList = checkTheSelectedSquare(potentialSquareForUser);
            if (!isSquareAlreadyInList)
            {
                addPotenitalSquareIntoTheList(potentialSquareForUser);
            }
        }

        private void updateTheNewSquareOnBoard(int i_RowOfSquare, int i_ColumnOfSquare, int i_RowDirection, int i_ColmunDirection)
        {
            Player activePlayer, notActivePlayer;
            int currentTempColumn, currentTempRow;
            if (m_FirstPlayer.PlayerActivity)
            {
                activePlayer = m_FirstPlayer;
                notActivePlayer = m_SecondPlayer;
            }
            else
            {
                activePlayer = m_SecondPlayer;
                notActivePlayer = m_FirstPlayer;
            }

            currentTempColumn = i_ColumnOfSquare + i_ColmunDirection;
            currentTempRow = i_RowOfSquare + i_RowDirection;
            while (!m_GameBoard.BoardMat[currentTempRow, currentTempColumn].Equals(activePlayer.PlayerType))
            {
                Square toChange = new Square(currentTempRow, currentTempColumn);
                m_GameBoard.BoardMat[currentTempRow, currentTempColumn] = activePlayer.PlayerType;
                activePlayer.ListOfSquares.Add(toChange);
                notActivePlayer.RemoveSqauFromeTheList(toChange);
                currentTempColumn += i_ColmunDirection;
                currentTempRow += i_RowDirection;
            }
        }

        private string getSquareForComputer()
        {
            StringBuilder stringOfSquare = new StringBuilder();
            int i = 0;
            int indexOfSquare = m_RndForPc.Next(m_PotenitalMovesForPlayers.Count());
            foreach (Square searchSquare in m_PotenitalMovesForPlayers)
            {
                if (i == indexOfSquare)
                {
                    stringOfSquare.Append((char)(searchSquare.ColSquare + 'A'));
                    stringOfSquare.Append(searchSquare.RowSquare + 1);
                    break;
                }

                i++;
            }

            return stringOfSquare.ToString();
        }

        private bool isBoardFull()
        {
            bool isBoardFullOfMoves = false;
            if (m_FirstPlayer.ListOfSquares.Count + m_SecondPlayer.ListOfSquares.Count == Math.Pow(m_GameBoard.NumOFColumns, 2))
            {
                isBoardFullOfMoves = true;
            }

            return isBoardFullOfMoves;
        }

        public void RestartGame(int i_SizeOfBoard)
        {
            GameBoard.InitializeTheFirstDefaultMoves(GameBoard, GameBoard.NumOFColumns);
            FirstPlayer.PlayerActivity = true;
            SecondPlayer.PlayerActivity = true;
            FirstPlayer.ListOfSquares.Clear();
            SecondPlayer.ListOfSquares.Clear();

            FirstPlayer.InitializeFirstMovesForFirstPlayer(i_SizeOfBoard);
            SecondPlayer.InitializeFirstMovesForSecondPlayer(i_SizeOfBoard);
            bool start = IsMoveExist();
        }

        public void SwitchTurns()
        {
            FirstPlayer.SwitchActivation();
            SecondPlayer.SwitchActivation();
        }
        
    }
}
