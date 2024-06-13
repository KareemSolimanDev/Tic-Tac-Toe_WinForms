using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class frmGame : Form
    {
        public frmGame()
        {
            InitializeComponent();

        }

        private char _whichTurn = 'O';
        Dictionary<char, Bitmap> _players = new Dictionary<char, Bitmap>()
        {
            ['O'] = Properties.Resources.O,
            ['X'] = Properties.Resources.X
        };

        private char[] _board = {'?', '?', '?',
                                 '?', '?', '?',
                                 '?', '?', '?'};
        private char _checkWinners(byte pos1,byte pos2,byte pos3)
        {
            if((_board[pos1 - 1] == _board[pos2 - 1]) && (_board[pos2 - 1] == _board[pos3 - 1]))
            {
                return _board[pos1 - 1];
            }
            return '?';
        }
        private char _isWinner()
        {
            char winner = _checkWinners(1,2,3);
            if (winner == '?')
                winner = _checkWinners(4, 5, 6);
            if (winner == '?')
                winner = _checkWinners(7, 8, 9);
            if (winner == '?')
                winner = _checkWinners(1, 4, 7);
            if (winner == '?')
                winner = _checkWinners(2,5, 8);
            if (winner == '?')
                winner = _checkWinners(3, 6, 9);
            if (winner == '?')
                winner = _checkWinners(1, 5, 9);
            if (winner == '?')
                winner = _checkWinners(3, 5, 7);

            return winner;
        }
        private bool _checkWinning()
        {
            char winner = _isWinner();
            if (winner != '?')
            {
                pbWinner.Image = _players[winner];
                gameBox.Enabled = false;
                MessageBox.Show((_whichTurn + " Player" + " Win!!"), "Congratulation");
                return true;
            }
            return false;
        }
        private bool _checkDrawing()
        {
            if (Array.IndexOf(_board, '?') == -1)
            {
                MessageBox.Show("Draw , play again :)", "GameOver!!");
                return true;
            }
            return false;
        }
        private bool _checkGameStatus()
        {
            if (_checkWinning())
                return true;

            if (_checkDrawing())
                return true;

            return false;

        }
        private void _changeTurn()
        {
            _whichTurn = (_whichTurn == 'O' ? 'X' : 'O');
            pbTurn.Image = _players[_whichTurn];
        }
        private void _playPlayerTurn(PictureBox chosenCell)
        {
            _board[  Convert.ToByte(chosenCell.Tag)-1  ] = _whichTurn;
            chosenCell.Image = _players[_whichTurn];
            chosenCell.Enabled = false;
        }

        private List<PictureBox> _getEmptyCells()
        {
            List<PictureBox> emptyCells= new List<PictureBox>();

            foreach(PictureBox cell in gameBox.Controls.OfType<PictureBox>())
            {
                
                if(_board[Convert.ToSByte(cell.Tag)-1] == '?')
                {
                    emptyCells.Add(cell);
                }
            }

            return emptyCells;

        }
        private void playComputerTurn()
        {
            List<PictureBox> emptyCells=_getEmptyCells();
            Random rcell = new Random();
            PictureBox playedPb = emptyCells[rcell.Next(0, emptyCells.Count-1)];
            playedPb.Image = _players[_whichTurn];
            playedPb.Enabled = false;
            _board[Convert.ToByte(playedPb.Tag) - 1] = _whichTurn;
        }

        private void _startGame(PictureBox cell)
        {
            _playPlayerTurn(cell);
            

            if (_checkGameStatus())
                return;

            _changeTurn();

            if (frmStart.modeVal == frmStart.playMode.Computer)
            {
                playComputerTurn();
                if (_checkGameStatus())
                    return;
                _changeTurn();
            }
        }
        private void _cell_Click(object sender, EventArgs e)
        {
            _startGame(((PictureBox)sender));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _board = new char[]{'?', '?', '?','?', '?', '?','?', '?', '?'};
            pbWinner.Image = Properties.Resources.QMark;
            gameBox.Enabled = true;
            foreach(PictureBox cell in gameBox.Controls.OfType<PictureBox>())
            {
                cell.Image = Properties.Resources.QMark;
                cell.Enabled = true;
            }

        }
    }
}
