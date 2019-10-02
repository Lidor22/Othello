namespace UI
{
    public partial class FormLoginD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BoardSizeButton = new System.Windows.Forms.Button();
            this.AgainstFriendButton = new System.Windows.Forms.Button();
            this.AgainstPcButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSizeButton
            // 
            this.BoardSizeButton.Location = new System.Drawing.Point(120, 35);
            this.BoardSizeButton.Name = "BoardSizeButton";
            this.BoardSizeButton.Size = new System.Drawing.Size(223, 44);
            this.BoardSizeButton.TabIndex = 0;
            this.BoardSizeButton.Text = "Board Size : 6x6(click to increase)";
            this.BoardSizeButton.UseVisualStyleBackColor = true;
            // 
            // AgainstFriendButton
            // 
            this.AgainstFriendButton.Location = new System.Drawing.Point(248, 97);
            this.AgainstFriendButton.Name = "AgainstFriendButton";
            this.AgainstFriendButton.Size = new System.Drawing.Size(156, 42);
            this.AgainstFriendButton.TabIndex = 1;
            this.AgainstFriendButton.Tag = string.Empty;
            this.AgainstFriendButton.Text = "Play against your friend";
            this.AgainstFriendButton.UseVisualStyleBackColor = true;
            this.AgainstFriendButton.Click += new System.EventHandler(this.againstFriendButton_Click);
            // 
            // AgainstPcButton
            // 
            this.AgainstPcButton.Location = new System.Drawing.Point(57, 97);
            this.AgainstPcButton.Name = "AgainstPcButton";
            this.AgainstPcButton.Size = new System.Drawing.Size(156, 42);
            this.AgainstPcButton.TabIndex = 2;
            this.AgainstPcButton.Text = "Play against the computer";
            this.AgainstPcButton.UseVisualStyleBackColor = true;
            this.AgainstPcButton.Click += new System.EventHandler(this.againstPcButton_Click);
            // 
            // FormLoginD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 169);
            this.Controls.Add(this.AgainstPcButton);
            this.Controls.Add(this.AgainstFriendButton);
            this.Controls.Add(this.BoardSizeButton);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLoginD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BoardSizeButton;
        private System.Windows.Forms.Button AgainstFriendButton;
        private System.Windows.Forms.Button AgainstPcButton;
    }
}