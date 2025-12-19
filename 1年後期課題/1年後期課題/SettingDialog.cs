using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _1年後期課題
{
    internal class SettingDialog : Form
    {
        private Form1 _form1;

        private StartButton _startButton;

        private int[,] _board_size_array;

        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;

        private Button okButton;

        public SettingDialog(Form1 form1, StartButton startButton , int[,] board_size_array)
        {
            _form1 = form1;
            _startButton = startButton;

            Text = "設定";
            this.StartPosition = FormStartPosition.CenterParent;  // 元のフォームの中央に配置
            Size size = this.Size;
            int height = size.Height;

            _board_size_array = board_size_array;

            Label sizeLabel = new Label()
            {
                Text = "レベル変更",
                Font = new Font("Meiryo UI", 8),
                Location = new Point(10, 10)
            };
            Controls.Add(sizeLabel);

            radioButton1 = new RadioButton()
            {
                Text = $"レベル１ ({_board_size_array[0, 0]*_board_size_array[0, 1]}枚)",
                Font = new Font("Meiryo UI", 8),
                Location = new Point(30, 30),
                
            };
            radioButton2 = new RadioButton()
            {
                Text = $"レベル２ ({_board_size_array[1, 0]*_board_size_array[1, 1]}枚)",
                Font = new Font("Meiryo UI", 8),
                Location = new Point(30, 60),
                
            };
            radioButton3 = new RadioButton()
            {
                Text = $"レベル３ ({_board_size_array[2, 0]*_board_size_array[2, 1]}枚)",
                Font = new Font("Meiryo UI", 8),
                Location = new Point(30, 90),

            };
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;

            radioButton1.Checked = true;

            Controls.Add(radioButton1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton3);

            okButton = new Button()
            {
                Location = new Point(190, 220),
                Text = "OK",
                Font = new Font("Meiryo UI", 8)
            };
            okButton.Click += OkButton_Click;
            Controls.Add(okButton);


        }


        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                _form1.BOARD_SIZE_X = _board_size_array[0, 0];
                _form1.BOARD_SIZE_Y = _board_size_array[0, 1];
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                _form1.BOARD_SIZE_X = _board_size_array[1, 0];
                _form1.BOARD_SIZE_Y = _board_size_array[1, 1];
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                _form1.BOARD_SIZE_X = _board_size_array[2, 0];
                _form1.BOARD_SIZE_Y = _board_size_array[2, 1];
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _form1.CardReset();
            _form1.FormSizeAuto();

            if (_form1.isPlaying == true)  // ゲーム中なら
            {
                // カード枚数を変えたときタイマーをストップする
                _startButton.TimerStop();
                _form1.timeLabel.Text = "00:00";

            }

            this.Close();
        }

    }
}
