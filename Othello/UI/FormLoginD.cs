using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class FormLoginD : Form
    {
        private bool m_SecondUserIsPC;
        private int m_SizeOfBoard = 6;

        public FormLoginD()
        {
            InitializeComponent();

            this.BoardSizeButton.Click += boardSizeButton_Click;
            this.AgainstFriendButton.Click += againstFriendButton_Click;
            this.AgainstPcButton.Click += againstPcButton_Click;
        }

        public int SizeOfBoard
        {
            get { return m_SizeOfBoard; }
        }

        public bool SecondUserIsPC
        {
            get { return m_SecondUserIsPC; }
        }

        private void boardSizeButton_Click(object sender, EventArgs e)
        {
            m_SizeOfBoard += 2;
            if(m_SizeOfBoard > 12)
            {
                m_SizeOfBoard = 6;
            }

            this.BoardSizeButton.Text = string.Format("Board Size: {0}x{0}(click to increase)", m_SizeOfBoard);
        }

        private void againstFriendButton_Click(object sender, EventArgs e)
        {
            m_SecondUserIsPC = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void againstPcButton_Click(object sender, EventArgs e)
        {
            m_SecondUserIsPC = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void boardSizeButton_Click_1(object sender, EventArgs e)
        {
        }
    }
}
