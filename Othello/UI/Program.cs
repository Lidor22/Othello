using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormLoginD newFormLogin = new FormLoginD();
            FormGame newFormGame = new FormGame(newFormLogin);
            Application.Run(newFormGame);
        }
    }
}
