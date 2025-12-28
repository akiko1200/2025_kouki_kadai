using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題_新規_
{
    public partial class Form2 : Form
    {
        private Game _game;

        private TestCard testCard;

        private Label p1;
        private Label p2;

        private Label p1_pntLabel;
        private Label p2_pntLabel;

        private int p1_pntCnt = 0;
        private int p2_pntCnt = 0;

        private PictureBox frame1;
        private PictureBox frame2;

        /// <summary>現在のプレイヤー</summary>
        private int playing;

        /// <summary>勝者</summary>
        private string winner;


        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            _game = new Game(/*3, 2, */this);  //aiu

            p1 = new Label()
            {
                Text = "Player1",
                Location = new Point(60, 10),
                Font = new Font("Meiryo UI", 12),
                ForeColor = Color.Red,
                //BackColor = Color.Gray,
                AutoSize = true,
            };
            p2 = new Label()
            {
                Text = "Player2",
                Location = new Point(210, 10),
                Font = new Font("Meiryo UI", 12),
                ForeColor = Color.Blue,
                //BackColor = Color.Gray,
                AutoSize = true,
            };
            Controls.Add(p1);
            Controls.Add(p2);

            p1_pntLabel = new Label()
            {
                Text = $"{p1_pntCnt}",
                Location = new Point(80, 35),
                Font = new Font("Meiryo UI", 15),
                //BackColor = Color.Gray,
                AutoSize = true,
            };
            p2_pntLabel = new Label()
            {
                Text = $"{p2_pntCnt}",
                Location = new Point(230, 35),
                Font = new Font("Meiryo UI", 15),
                //BackColor = Color.Gray,
                AutoSize = true,
            };
            Controls.Add(p1_pntLabel);
            Controls.Add(p2_pntLabel);

            frame1 = new PictureBox()
            {
                Image = Properties.Resources.赤枠,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(130, 70),
                Location = new Point(35, 5),
            };
            frame2 = new PictureBox()
            {
                Image = Properties.Resources.赤枠,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(130, 70),
                Location = new Point(190, 5),
            };
            Controls.Add(frame1);
            Controls.Add(frame2);

            frame2.Visible = false;

            _game.GameCleard += Form2OnGameCleard;

            _game.backButton.Location = new Point(0, _game.formHeight - _game.backButton.Height);
            Controls.Add(_game.backButton);

            _game.settingButton.Location = new Point(_game.formWidth - _game.settingButton.Width, _game.formHeight - _game.settingButton.Height);
            Controls.Add(_game.settingButton);

            _game.settingDialog.changeButton.Click += Form2SettingChanged_Click;

            //_game.settingDialog.ShowDialog();

            Form2CardAdd();
            _game.CardRandom();
            Form2SizeChange();

            playing = 1;
            _game.PairMatched += Form2PairMatched;
            _game.PairNotMatched += Form2PairNotMatched;

        }

        /// <summary>
        /// Form2のカードの追加
        /// </summary>
        private void Form2CardAdd()
        {
            if (_game._cardArray != null)  // 2回目以降なら
            {
                foreach (var card in _game._cardArray)
                {
                    if (card != null)
                    {
                        Controls.Remove(card);  // 元のカードを削除
                        card.Dispose();  // リソースを解放
                    }
                }
            }

            _game._cardArray = new TestCard[_game.BOARD_SIZE_Y, _game.BOARD_SIZE_X];

            for (int i = 0; i < _game.BOARD_SIZE_X; i++)
            {
                for (int j = 0; j < _game.BOARD_SIZE_Y; j++)
                {
                    // インスタンスの作成
                    testCard =
                        new TestCard(
                            //aiu
                            _game,
                            i, j,
                            new Size(_game.CARD_SIZE_X, _game.CARD_SIZE_Y),
                            _game.BOARD_SIZE_X, _game.BOARD_SIZE_Y);

                    // 配列にカードの参照を追加
                    _game._cardArray[j, i] = testCard;

                    // コントロールにカードを追加
                    Controls.Add(testCard);
                }
            }
        }

        /// <summary>
        /// Form2でクリアしたときの処理
        /// </summary>
        private void Form2OnGameCleard()
        {
            if (p1_pntCnt == p2_pntCnt)
            {
                MessageBox.Show("引き分け", "結果");
            }
            else
            {
                if (p1_pntCnt > p2_pntCnt)
                {
                    winner = "Player1";
                }
                else
                {
                    winner = "Player2";
                }
                MessageBox.Show($"{winner} の勝利！", "結果");//{p1_pntCnt}－{p2_pntCnt} で
            }

            frame1.Visible = true;
            frame2.Visible = false;

            p1_pntCnt = 0;
            p2_pntCnt = 0;
            p1_pntLabel.Text = $"{p1_pntCnt}";
            p2_pntLabel.Text = $"{p2_pntCnt}";


            foreach (var card in _game._cardArray)
            {
                card.Enabled = true;  // カードを有効にする
            }
        }

        /// <summary>
        /// Form2でペアができたときの処理
        /// </summary>
        private void Form2PairMatched()
        {
            if (playing == 1)
            {
                p1_pntCnt += 2;
                p1_pntLabel.Text = $"{p1_pntCnt}";
            }
            else
            {
                p2_pntCnt += 2;
                p2_pntLabel.Text = $"{p2_pntCnt}";
            }
        }

        /// <summary>
        /// Form2でペアができなかったときの処理
        /// </summary>
        private void Form2PairNotMatched()
        {
            if (playing == 1)
            {
                frame1.Visible = false;
                frame2.Visible = true;
                playing = 2;
            }
            else
            {
                frame2.Visible = false;
                frame1.Visible = true;
                playing = 1;
            }
        }

        /// <summary>
        /// Form2で設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2SettingChanged_Click(object sender, EventArgs e)
        {
            Form2CardAdd();
            _game.CardRandom();
            Form2SizeChange();

            _game.pairCnt = 0;

            frame1.Visible = true;
            frame2.Visible = false;
            playing = 1;

            p1_pntCnt = 0;
            p2_pntCnt = 0;
            p1_pntLabel.Text = $"{p1_pntCnt}";
            p2_pntLabel.Text = $"{p2_pntCnt}";

        }

        /// <summary>
        /// Form2のサイズ変更
        /// </summary>
        public void Form2SizeChange()
        {
            // 最小サイズを更新
            this.MinimumSize = new Size(_game.formWidth + (this.Width - this.ClientSize.Width),
                                        _game.formHeight + (this.Height - this.ClientSize.Height));

            this.ClientSize = new Size(_game.formWidth, _game.formHeight);

            // 中央に再配置
            var screen = Screen.FromControl(this);  // スクリーン情報を取得
            var workingArea = screen.WorkingArea;  // 作業領域
            this.Location = new Point(
                workingArea.Left + (workingArea.Width - this.Width) / 2,
                workingArea.Top + (workingArea.Height - this.Height) / 2);
            _game.backButton.Location = new Point(0, _game.formHeight - _game.backButton.Height);

            // backButton、settingButtonの位置変更
            _game.backButton.Location = new Point(0, _game.formHeight - _game.backButton.Height);
            _game.settingButton.Location = new Point(_game.formWidth - _game.settingButton.Width, _game.formHeight - _game.settingButton.Height);

            p1.Location = new Point(_game.formWidth / 2 - 110, 10);
            p2.Location = new Point(_game.formWidth / 2 + 40, 10);
            p1_pntLabel.Location = new Point(_game.formWidth / 2 - 90, 35);
            p2_pntLabel.Location = new Point(_game.formWidth / 2 + 60, 35);
            frame1.Location = new Point(_game.formWidth / 2 - 140, 0);
            frame2.Location = new Point(_game.formWidth / 2 + 10, 0);
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
