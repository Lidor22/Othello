using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NewOthelo;

namespace UI
{
    public partial class FormGame : Form
    {
        private FormLoginD m_SettingForm;
        private Game m_NewOtheloGame;
        private PictureBox[,] m_ButtonmmArray;

        public FormGame(FormLoginD i_SettingForm)
        {
            m_SettingForm = i_SettingForm;
            if (i_SettingForm.ShowDialog() == DialogResult.OK)
            {
                m_NewOtheloGame = new Game(m_SettingForm.SecondUserIsPC, m_SettingForm.SizeOfBoard);
                this.setButtonMatrixOnForm();
                InitializeComponent();
                this.ClientSize = new System.Drawing.Size((50 * m_SettingForm.SizeOfBoard) + 20, (50 * m_SettingForm.SizeOfBoard) + 20);
            }
            
            ShowActivePlayerTurn();
            bool start = m_NewOtheloGame.IsMoveExist();
            this.ChangeButtonsStatusOncePotenial();
        }

        public FormLoginD SettingForm
        {
            get { return m_SettingForm; }
            set { m_SettingForm = value; }
        }

        public Game NewOtheloGame
        {
            get { return m_NewOtheloGame; }
            set { m_NewOtheloGame = value; }
        }

        private void setButtonMatrixOnForm()
        {
            m_ButtonmmArray = new PictureBox[m_SettingForm.SizeOfBoard, m_SettingForm.SizeOfBoard];
            for (int i = 0; i < m_SettingForm.SizeOfBoard; i++)
            {
                for (int j = 0; j < m_SettingForm.SizeOfBoard; j++)
                {
                    m_ButtonmmArray[i, j] = new PictureBox();
                    m_ButtonmmArray[i, j].Enabled = false;
                    string textToPrint = string.Format("{0},{1}", i, j); 
                    m_ButtonmmArray[i, j].Tag = textToPrint;
                    m_ButtonmmArray[i, j].Width = 50;
                    m_ButtonmmArray[i, j].BorderStyle = BorderStyle.Fixed3D;
                    m_ButtonmmArray[i, j].Height = 50;
                    m_ButtonmmArray[i, j].Location = new Point((j * 50) + 10, (i * 50) + 10);
                    m_ButtonmmArray[i, j].Click += new EventHandler(ButtonClicked_Click);
                    this.Controls.Add(m_ButtonmmArray[i, j]);
                }
            }

            setFirstDefualtPlaysOnBoard();
        }

        private void ButtonClicked_Click(object sender, EventArgs e)
        {
            bool isPc = false;
            int rowOfButton = (sender as PictureBox).Tag.ToString()[0] - '0';
            int colOfButton = (sender as PictureBox).Tag.ToString()[2] - '0';

            Square selectedSquareFromUser = new Square(rowOfButton, colOfButton);
            NewOtheloGame.PerformAction(selectedSquareFromUser);
            this.changeButtonsStatusOnceNoLongerPotenial();
            showNewBoardAfterAction();

            NewOtheloGame.SwitchTurns();

            isPc = NewOtheloGame.SecondPlayer.CheckWhoIsTheSecondPlayer();

            if (NewOtheloGame.IsMoveExist())
            {
                if (isPc && NewOtheloGame.SecondPlayer.PlayerActivity)
                {
                    doComputerTurn();
                }
                else
                {
                    ShowActivePlayerTurn();
                    this.ChangeButtonsStatusOncePotenial();
                }
            }
            else
            {
                NewOtheloGame.SwitchTurns();
                ShowActivePlayerTurn();
                this.changeButtonsStatusOnceNoLongerPotenial();
                if (NewOtheloGame.IsMoveExist())
                {
                    this.ChangeButtonsStatusOncePotenial();
                }
                else
                {
                    GatherTheWinnerInfo();
                }
            }
        }

        public void ShowActivePlayerTurn()
        {
            if (NewOtheloGame.FirstPlayer.PlayerActivity)
            {
                this.Text = "Othello - Red's Turn";
            }
            else
            {
                this.Text = "Othello - Yellow's Turn";
            }
        }

        private void setFirstDefualtPlaysOnBoard()
        {
            int sizeOfBoard = m_SettingForm.SizeOfBoard - 1;

            m_ButtonmmArray[(sizeOfBoard / 2) + 1, (sizeOfBoard / 2) + 1].Image = Properties.Resources.CoinRed;
            m_ButtonmmArray[(sizeOfBoard / 2) + 1, (sizeOfBoard / 2) + 1].SizeMode = PictureBoxSizeMode.StretchImage;
            m_ButtonmmArray[(sizeOfBoard / 2), (sizeOfBoard / 2)].Image = Properties.Resources.CoinRed;
            m_ButtonmmArray[(sizeOfBoard / 2), (sizeOfBoard / 2)].SizeMode = PictureBoxSizeMode.StretchImage;
            m_ButtonmmArray[(sizeOfBoard / 2) + 1, (sizeOfBoard / 2)].Image = Properties.Resources.CoinYellow;
            m_ButtonmmArray[(sizeOfBoard / 2) + 1, (sizeOfBoard / 2)].SizeMode = PictureBoxSizeMode.StretchImage;
            m_ButtonmmArray[(sizeOfBoard / 2), (sizeOfBoard / 2) + 1].Image = Properties.Resources.CoinYellow;
            m_ButtonmmArray[(sizeOfBoard / 2), (sizeOfBoard / 2) + 1].SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void ChangeButtonsStatusOncePotenial()
        {
            foreach (Square currentSquare in NewOtheloGame.PotenitalMovesForPlayers)
            {
                m_ButtonmmArray[currentSquare.RowSquare, currentSquare.ColSquare].BackColor = System.Drawing.Color.Green;
                m_ButtonmmArray[currentSquare.RowSquare, currentSquare.ColSquare].Enabled = true;
            }
        }

        private void changeButtonsStatusOnceNoLongerPotenial()
        {
            foreach (Square currentSquare in NewOtheloGame.PotenitalMovesForPlayers)
            {
                m_ButtonmmArray[currentSquare.RowSquare, currentSquare.ColSquare].BackColor = System.Drawing.Color.Empty;
                m_ButtonmmArray[currentSquare.RowSquare, currentSquare.ColSquare].Enabled = false;
            }
        }

        private void showNewBoardAfterAction()
        {
            for (int i = 0; i < m_NewOtheloGame.GameBoard.NumOfRows; i++)
            {
                for (int j = 0; j < m_NewOtheloGame.GameBoard.NumOFColumns; j++)
                {
                    if (m_NewOtheloGame.GameBoard.BoardMat[i, j].Equals('X'))
                    {
                        m_ButtonmmArray[i, j].Image = Properties.Resources.CoinRed;
                        m_ButtonmmArray[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    if (m_NewOtheloGame.GameBoard.BoardMat[i, j].Equals('O'))
                    {
                        m_ButtonmmArray[i, j].Image = Properties.Resources.CoinYellow;
                        m_ButtonmmArray[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    m_ButtonmmArray[i, j].Enabled = false;
                }
            }
        }

        private void doComputerTurn()
        {
            NewOtheloGame.GetActionFromComputer();
            NewOtheloGame.SwitchTurns();

            while (true)
            {
                if (NewOtheloGame.IsMoveExist())
                {
                    ShowActivePlayerTurn();
                    showNewBoardAfterAction();
                    this.ChangeButtonsStatusOncePotenial();
                    return;
                }
                else
                {
                    NewOtheloGame.SwitchTurns();
                    if (NewOtheloGame.IsMoveExist())
                    {
                        NewOtheloGame.GetActionFromComputer();
                        NewOtheloGame.SwitchTurns();
                    }
                    else
                    {
                        GatherTheWinnerInfo();
                        return;
                    }
                }
            }
        }
        public void GatherTheWinnerInfo()
        {
            Player theWinnerPlayer;
            Player theLoserPlayer;
            string winnersColor = string.Empty;

            if (NewOtheloGame.FirstPlayer.AmountOfSquaresForPlayer > NewOtheloGame.SecondPlayer.AmountOfSquaresForPlayer)
            {
                theWinnerPlayer = NewOtheloGame.FirstPlayer;
                theLoserPlayer = NewOtheloGame.SecondPlayer;
                winnersColor = "Red";
            }
            else
            {
                theWinnerPlayer = NewOtheloGame.SecondPlayer;
                theLoserPlayer = NewOtheloGame.FirstPlayer;
                winnersColor = "Yellow";
            }

            theWinnerPlayer.WinsOfPlayer += 1;
            this.Show();
            retryOrEnd(winnersColor, theWinnerPlayer.AmountOfSquaresForPlayer, theLoserPlayer.AmountOfSquaresForPlayer, theWinnerPlayer.WinsOfPlayer, theLoserPlayer.WinsOfPlayer);
        }

        private void retryOrEnd(string i_WinnerColor, int i_NumberOfSquaresForWinner, int i_NumberOfSquaresForLoser, int i_NumOfWinnersWins, int i_NumOfLosersWins)
        {
            string textToUser = string.Format("{0} Won! ({1}/{2})/({3}/{4}),would you like another round?", i_WinnerColor, i_NumberOfSquaresForWinner, i_NumberOfSquaresForLoser, i_NumOfWinnersWins, i_NumOfLosersWins);
            if (MessageBox.Show(
                    textToUser,
                    "Othello",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                resetAllAttributes();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void resetAllAttributes()
        {
            this.Controls.Clear();
            setButtonMatrixOnForm();
            setFirstDefualtPlaysOnBoard();
            NewOtheloGame.RestartGame(m_SettingForm.SizeOfBoard);
            this.ChangeButtonsStatusOncePotenial();
            ShowActivePlayerTurn();
        }
    }
}
