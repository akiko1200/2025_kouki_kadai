using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題_新規_
{
    public partial class SettingDialog : Form
    {
        private Game _game;

        private int[,] _board_size_array;

        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;

        public Button changeButton;


        public SettingDialog(Game game, int[,] board_size_array)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            Text = "設定";
            Size = new Size(260, 250);

            _game = game;

            _board_size_array = board_size_array;

            Label levelLabel = new Label()
            {
                Text = "レベル変更",
                Font = new Font("Meiryo UI", 10),
                Location = new Point(10, 15)
            };
            Controls.Add(levelLabel);

            radioButton1 = new RadioButton()
            {
                Text = $"レベル１ ({_board_size_array[0, 0] * _board_size_array[0, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 50),
                AutoSize = true
            };
            radioButton2 = new RadioButton()
            {
                Text = $"レベル２ ({_board_size_array[1, 0] * _board_size_array[1, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 85),
                AutoSize = true
            };
            radioButton3 = new RadioButton()
            {
                Text = $"レベル３ ({_board_size_array[2, 0] * _board_size_array[2, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 120),
                AutoSize = true
            };
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;

            radioButton1.Checked = true;

            Controls.Add(radioButton1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton3);

            changeButton = new Button()
            {
                Location = new Point(150, 170),
                Text = "変更",
                Font = new Font("Meiryo UI", 8)
            };
            changeButton.Click += ChangeButton_Click;
            Controls.Add(changeButton);

            //FormClosing += Setting_FormClosing;

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[0, 0];
                _game.BOARD_SIZE_Y = _board_size_array[0, 1];
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[1, 0];
                _game.BOARD_SIZE_Y = _board_size_array[1, 1];
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[2, 0];
                _game.BOARD_SIZE_Y = _board_size_array[2, 1];
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            _game.formWidth = _game.BOARD_SIZE_X * _game.CARD_SIZE_X;
            _game.formHeight = _game.BOARD_SIZE_Y * _game.CARD_SIZE_Y + _game.cardOffset + _game.backButton.Height;

            this.Close();
        }

        /// <summary>
        /// 設定ダイアログが閉じられた時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Setting_FormClosing(Object sender, FormClosingEventArgs e)
        //{
        //    //Program.Display_form0();
        //    //Form1.Close();
        //}


        private void SettingDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
