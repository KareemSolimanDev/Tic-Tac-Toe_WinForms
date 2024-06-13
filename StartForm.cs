using System;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();
        }
        public enum playMode
        {
            Computer=1,
            MultiPlayers,
        }

        public static playMode modeVal = playMode.Computer;
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        { 
            modeVal = radioButton2.Checked?playMode.MultiPlayers: playMode.Computer;
            frmGame newGame = new frmGame();
            newGame.ShowDialog();
        }
        
    }
}
