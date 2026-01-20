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

        // レベルごとのラジオボタン
        private RadioButton levelRadioButton1;
        private RadioButton levelRadioButton2;
        private RadioButton levelRadioButton3;

        // 絵柄ごとのラジオボタン
        public RadioButton themeRadioButton1;
        public RadioButton themeRadioButton2;
        public RadioButton themeRadioButton3;
        public RadioButton themeRadioButton4;

        /// <summary>変更ボタン</summary>
        public Button changeButton;


        public SettingDialog(Game game, int[,] board_size_array)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            Text = "設定";
            Size = new Size(310, 220);

            _game = game;
            _board_size_array = board_size_array;

            Panel levelPanel = new Panel()
            {
                Size = new Size(130, 110),
                Location = new Point(10, 0),
            };
            Panel themePanel = new Panel()
            {
                Size = new Size(120, 135),
                Location = new Point(165, 0),
            };


            Label levelLabel = new Label()
            {
                Text = "レベル",
                Font = new Font("Meiryo UI", 10),
                Location = new Point(5, 5)
            };

            levelRadioButton1 = new RadioButton()
            {
                Text = $"レベル１ ({_board_size_array[0, 0] * _board_size_array[0, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 30),
                AutoSize = true
            };
            levelRadioButton2 = new RadioButton()
            {
                Text = $"レベル２ ({_board_size_array[1, 0] * _board_size_array[1, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 55),
                AutoSize = true
            };
            levelRadioButton3 = new RadioButton()
            {
                Text = $"レベル３ ({_board_size_array[2, 0] * _board_size_array[2, 1]}枚)",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 80),
                AutoSize = true
            };
            levelRadioButton1.CheckedChanged += LevelRadioButton1_CheckedChanged;
            levelRadioButton2.CheckedChanged += LevelRadioButton2_CheckedChanged;
            levelRadioButton3.CheckedChanged += LevelRadioButton3_CheckedChanged;
            levelRadioButton1.Checked = true;  // レベル1をチェックしておく

            Label themeLabel = new Label()
            {
                Text = "絵柄",
                Font = new Font("Meiryo UI", 10),
                Location = new Point(5, 5)
            };

            themeRadioButton1 = new RadioButton()
            {
                Text = "ノーマル",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 30),
                AutoSize = true
            };
            themeRadioButton2 = new RadioButton()
            {
                Text = "食べ物",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 55),
                AutoSize = true
            };
            themeRadioButton3 = new RadioButton()
            {
                Text = "動物",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 80),
                AutoSize = true
            };
            themeRadioButton4 = new RadioButton()
            {
                Text = "植物",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 105),
                AutoSize = true
            };

            themeRadioButton1.CheckedChanged += ThemeRadioButton1_CheckedChanged;
            themeRadioButton2.CheckedChanged += ThemeRadioButton2_CheckedChanged;
            themeRadioButton3.CheckedChanged += ThemeRadioButton3_CheckedChanged;
            themeRadioButton4.CheckedChanged += ThemeRadioButton4_CheckedChanged;
            themeRadioButton1.Checked = true;  // チェックしておく
            
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
            themePanel.Controls.Add(themeRadioButton4);


            changeButton = new Button()
            {
                Location = new Point(200, 145),
                Text = "変更",
                Font = new Font("Meiryo UI", 9)
            };
            changeButton.Click += ChangeButton_Click;
            Controls.Add(changeButton);

        }

        /// <summary>
        /// レベル1のラジオボタンがチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevelRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (levelRadioButton1.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[0, 0];
                _game.BOARD_SIZE_Y = _board_size_array[0, 1];
            }
        }

        /// <summary>
        /// レベル2のラジオボタンがチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevelRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (levelRadioButton2.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[1, 0];
                _game.BOARD_SIZE_Y = _board_size_array[1, 1];
            }
        }

        /// <summary>
        /// レベル3のラジオボタンがチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevelRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (levelRadioButton3.Checked)
            {
                _game.BOARD_SIZE_X = _board_size_array[2, 0];
                _game.BOARD_SIZE_Y = _board_size_array[2, 1];
            }
        }

        /// <summary>
        /// 1つ目の絵柄がチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemeRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (themeRadioButton1.Checked)
            {
                _game.cardThemeIndex = 0;
            }
        }

        /// <summary>
        /// 2つ目の絵柄がチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemeRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (themeRadioButton2.Checked)
            {
                _game.cardThemeIndex = 1;
            }
        }

        /// <summary>
        /// 3つ目の絵柄がチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemeRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (themeRadioButton3.Checked)
            {
                _game.cardThemeIndex = 2;
            }
        }

        /// <summary>
        /// 4つ目の絵柄がチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemeRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (themeRadioButton4.Checked)
            {
                _game.cardThemeIndex = 3;
            }
        }

        /// <summary>
        /// 変更ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeButton_Click(object sender, EventArgs e)
        {
            _game.pairCnt = 0;

            _game.formWidth = _game.BOARD_SIZE_X * _game.CARD_SIZE_X;
            _game.formHeight = _game.BOARD_SIZE_Y * _game.CARD_SIZE_Y + _game.cardOffset + _game.backButton.Height;

            // 1枚目に押されたカードを削除
            _game.clickCard1 = null;

            switch (_game.cardThemeIndex)
            {
                case 0:
                    _game.themeLabel.Text = themeRadioButton1.Text;
                    break;
                case 1:
                    _game.themeLabel.Text = themeRadioButton2.Text;
                    break;
                case 2:
                    _game.themeLabel.Text = themeRadioButton3.Text;
                    break;
                case 3:
                    _game.themeLabel.Text = themeRadioButton4.Text;
                    break;
            };

            this.Close();
        }

        private void SettingDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
