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

        //private ImageList _cardTheme;

        private RadioButton levelRadioButton1;
        private RadioButton levelRadioButton2;
        private RadioButton levelRadioButton3;

        private RadioButton themeRadioButton1;
        private RadioButton themeRadioButton2;
        private RadioButton themeRadioButton3;

        public Button changeButton;


        public SettingDialog(Game game, int[,] board_size_array/*, ImageList cardTheme*/)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            Text = "設定";
            Size = new Size(260, 300);

            _game = game;
            _board_size_array = board_size_array;
            //_cardTheme = cardTheme;

            Panel levelPanel = new Panel()
            {
                Size = new Size(this.Width, 115),
                Location = new Point(0, 0),
                //BackColor = Color.Blue,
            };
            Panel themePanel = new Panel()
            {
                Size = new Size(this.Width, 110),
                Location = new Point(0, 115),
                //BackColor = Color.Red,
            };


            Label levelLabel = new Label()
            {
                Text = "レベル",
                Font = new Font("Meiryo UI", 10),
                Location = new Point(15, 5)
            };

            levelRadioButton1 = new RadioButton()
            {
                Text = $"レベル１ ({_board_size_array[0, 0] * _board_size_array[0, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 30),
                AutoSize = true
            };
            levelRadioButton2 = new RadioButton()
            {
                Text = $"レベル２ ({_board_size_array[1, 0] * _board_size_array[1, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 55),
                AutoSize = true
            };
            levelRadioButton3 = new RadioButton()
            {
                Text = $"レベル３ ({_board_size_array[2, 0] * _board_size_array[2, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 80),
                AutoSize = true
            };
            levelRadioButton1.CheckedChanged += LevelRadioButton1_CheckedChanged;
            levelRadioButton2.CheckedChanged += LevelRadioButton2_CheckedChanged;
            levelRadioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            levelRadioButton1.Checked = true;

            Label themeLabel = new Label()
            {
                Text = "絵柄",
                Font = new Font("Meiryo UI", 10),
                Location = new Point(15, 5)
            };

            themeRadioButton1 = new RadioButton()
            {
                Text = "ノーマル",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 30),
                AutoSize = true
            };
            themeRadioButton2 = new RadioButton()
            {
                Text = "食べ物",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 55),
                AutoSize = true
            };
            themeRadioButton3 = new RadioButton()
            {
                Text = "動物",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(30, 80),
                AutoSize = true
            };

            themeRadioButton1.CheckedChanged += ThemeRadioButton1_CheckedChanged;
            themeRadioButton2.CheckedChanged += ThemeRadioButton2_CheckedChanged;
            themeRadioButton3.CheckedChanged += ThemeRadioButton3_CheckedChanged;
            themeRadioButton1.Checked = true;
            
            Controls.Add(levelPanel);
            levelPanel.Controls.Add(levelLabel);
            levelPanel.Controls.Add(levelRadioButton1);
            levelPanel.Controls.Add(levelRadioButton2);
            levelPanel.Controls.Add(levelRadioButton3);

            Controls.Add(themePanel);
            themePanel.Controls.Add(themeLabel);
            themePanel.Controls.Add(themeRadioButton1);
            themePanel.Controls.Add(themeRadioButton2);
            themePanel.Controls.Add(themeRadioButton3);


            changeButton = new Button()
            {
                Location = new Point(155, 225),
                Text = "変更",
                Font = new Font("Meiryo UI", 8)
            };
            changeButton.Click += ChangeButton_Click;
            Controls.Add(changeButton);

            //FormClosing += Setting_FormClosing;

        }

        private void LevelRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (levelRadioButton1.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[0, 0];
                _game.BOARD_SIZE_Y = _board_size_array[0, 1];
            }
        }

        private void LevelRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (levelRadioButton2.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[1, 0];
                _game.BOARD_SIZE_Y = _board_size_array[1, 1];
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (levelRadioButton3.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[2, 0];
                _game.BOARD_SIZE_Y = _board_size_array[2, 1];
            }
        }

        private void ThemeRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (themeRadioButton1.Checked)
            {
                _game.cardThemeIndex = 0;
            }
        }

        private void ThemeRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (themeRadioButton2.Checked)
            {
                _game.cardThemeIndex = 1;
            }
        }

        private void ThemeRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (themeRadioButton3.Checked)
            {
                _game.cardThemeIndex = 2;
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
