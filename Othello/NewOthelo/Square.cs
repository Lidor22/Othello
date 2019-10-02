namespace NewOthelo
{
    public class Square
    {
        private int m_RowOfSquare;
        private int m_ColOfSquare;

        public Square(string i_SquareFromUser)
        {
                m_ColOfSquare = i_SquareFromUser[0] - 'A';
                m_RowOfSquare = i_SquareFromUser[1] - '1';
        }

        public Square(int i_RowFromUser, int i_ColumnFromUser)
        {
            m_RowOfSquare = i_RowFromUser;
            m_ColOfSquare = i_ColumnFromUser;
        }

        public int RowSquare
        {
            get { return m_RowOfSquare; }
        }

        public int ColSquare
        {
            get { return m_ColOfSquare; }
        }

        private bool testSquareFromUser(string i_StrFromUser)
        {
            bool result = true;
            if (i_StrFromUser[0] < 'A' || i_StrFromUser[0] > 'Z')
            {
                result = false;
            }

            return result;
        }

        public bool isEqual(Square i_OtherSquare)
        {
            return (this.m_ColOfSquare == i_OtherSquare.m_ColOfSquare) && (this.m_RowOfSquare == i_OtherSquare.m_RowOfSquare);
        }
    }
}
