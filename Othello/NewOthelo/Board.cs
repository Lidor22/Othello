using System;

namespace NewOthelo
{
    public class Board
    {
        private char[,] m_BoardOfGame;
        private int m_RowsOfMat;
        private int m_ColumnsOfMat;

        public Board(int i_Rows, int i_Columns)
        {
            m_RowsOfMat = i_Rows;
            m_ColumnsOfMat = i_Columns;
            m_BoardOfGame = new char[i_Rows, i_Columns];
        }

        public char[,] BoardMat
        {
            get { return m_BoardOfGame; }
        }

        public int NumOfRows
        {
            get { return m_RowsOfMat; }
        }

        public int NumOFColumns
        {
            get { return m_ColumnsOfMat; }
        }

        public char GetMatrixValue(int i_RowOfSquare, int i_ColOfSquare)
        {
            return m_BoardOfGame[i_RowOfSquare, i_ColOfSquare];
        }

        public void InitializeTheFirstDefaultMoves(Board i_BoardToInitialize, int i_SizeOfBoard)
        {
            for (int i = 0; i < m_RowsOfMat; i++)
            {
                for (int j = 0; j < m_RowsOfMat; j++)
                {
                    i_BoardToInitialize.BoardMat[i, j] = Convert.ToChar(0);
                }
            }

            i_SizeOfBoard = i_SizeOfBoard - 1;
            i_BoardToInitialize.BoardMat[(i_SizeOfBoard / 2) + 1, (i_SizeOfBoard / 2) + 1] = 'X';
            i_BoardToInitialize.BoardMat[(i_SizeOfBoard / 2), (i_SizeOfBoard / 2)] = 'X';
            i_BoardToInitialize.BoardMat[(i_SizeOfBoard / 2) + 1, (i_SizeOfBoard / 2)] = 'O';
            i_BoardToInitialize.BoardMat[(i_SizeOfBoard / 2), (i_SizeOfBoard / 2) + 1] = 'O';
        }
    }
}
