using System.Collections.Generic;
using System.Linq;

namespace NewOthelo
{
    public class Player
    {
        private bool M_IsPc;
        private char m_PlayerType;
        private bool m_IsActive;
        private List<Square> m_PlayercurrentSquares;
        private int m_AmountOfSquares;
        private int m_WinsOfPlayer;

        public Player(bool i_IsPc, char i_Type, bool i_Active, int i_AmountOfSqaures)
        {
            M_IsPc = i_IsPc;
            m_PlayerType = i_Type;
            m_IsActive = i_Active;
            m_AmountOfSquares = i_AmountOfSqaures;
            m_PlayercurrentSquares = new List<Square>(i_AmountOfSqaures);
        }

        public void SwitchActivation()
        {
            m_IsActive = !(m_IsActive && m_IsActive);
        }

        public bool PlayerActivity
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }

        public bool IsPc
        {
            get { return M_IsPc; }
        }
        
        public int WinsOfPlayer
        {
            get { return m_WinsOfPlayer; }
            set { m_WinsOfPlayer = value; }
        }

        public int AmountOfSquaresForPlayer
        {
            get { return m_PlayercurrentSquares.Count(); }
        }

        public char PlayerType
        {
            get { return m_PlayerType; }
        }

        public List<Square> ListOfSquares
        {
            get { return m_PlayercurrentSquares; }
        }

        public void InitializeFirstMovesForFirstPlayer(int i_GameBoardSize)
        {
            i_GameBoardSize = i_GameBoardSize - 1;
            m_PlayercurrentSquares.Add(new Square((i_GameBoardSize / 2) + 1, (i_GameBoardSize / 2) + 1));
            m_PlayercurrentSquares.Add(new Square(i_GameBoardSize / 2, i_GameBoardSize / 2));
        }

        public void InitializeFirstMovesForSecondPlayer(int i_GameBoardSize)
        {
            i_GameBoardSize = i_GameBoardSize - 1;
            m_PlayercurrentSquares.Add(new Square(i_GameBoardSize / 2, (i_GameBoardSize / 2) + 1));
            m_PlayercurrentSquares.Add(new Square((i_GameBoardSize / 2) + 1, i_GameBoardSize / 2));
        }

        public void RemoveSqauFromeTheList(Square i_SquareToRemove)
        {
            foreach (Square searchSquare in m_PlayercurrentSquares)
            {
                if (searchSquare.isEqual(i_SquareToRemove))
                {
                    m_PlayercurrentSquares.Remove(searchSquare);
                    break;
                }
            }
        }

        public bool CheckWhoIsTheSecondPlayer()
        {
            bool result = false;
            if (M_IsPc)
            {
                result = true;
            }

            return result;
        }
    }
}
