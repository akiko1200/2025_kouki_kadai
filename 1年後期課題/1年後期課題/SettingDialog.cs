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

        private int[,] _board_size_array;

        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;

        private Button okButton;

        public SettingDialog(Form1 form1, int[,] board_size_array)
        {
            _form1 = form1;
            Text = "設定";
            this.StartPosition = FormStartPosition.CenterParent;  // 元のフォームの中央に配置
            Size size = this.Size;
            int height = size.Height;

            _board_size_array = board_size_array;

            Label sizeLabel = new Label()
            {
                Text = "カード枚数",
                Location = new Point(10, 0)
            };
            Controls.Add(sizeLabel);

            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton1.Text = $"{_board_size_array[0, 0]} × {_board_size_array[0, 1]}";
            radioButton2.Text = $"{_board_size_array[1, 0]} × {_board_size_array[1, 1]}";
            radioButton3.Text = $"{_board_size_array[2, 0]} × {_board_size_array[2, 1]}";
            radioButton1.Location = new Point(30, 30);
            radioButton2.Location = new Point(30, 60);
            radioButton3.Location = new Point(30, 90);
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            Controls.Add(radioButton1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton3);

            okButton = new Button()
            {
                Text = "OK",
                Location = new Point(150, 200)
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
            this.Close();
        }

    }
}
