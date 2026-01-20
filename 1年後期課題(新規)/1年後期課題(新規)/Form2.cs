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
using System.Xml.Linq;

namespace _1年後期課題_新規_
{
    public partial class Form2 : Form
    {
        private Game _game;

        private List<PlayerUI> playerUI;

        /// <summary>現在のプレイヤー</summary>
        private int playing;

        // プレイ人数のラジオボタン
        private RadioButton playerRadioButton1;
        private RadioButton playerRadioButton2;
        private RadioButton playerRadioButton3;

        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            _game = new Game(this);

            playerUI = new List<PlayerUI>()
            {
                new PlayerUI("Player1", Color.Red),
                new PlayerUI("Player2", Color.Blue),
                new PlayerUI("Player3", Color.Green),
                new PlayerUI("Player4", Color.Goldenrod),
            };

            foreach (var p in playerUI)
            {
                Controls.Add(p.panel);
            }

            _game.GameCleard += Form2OnGameCleard;
            _game.settingDialog.changeButton.Click += Form2SettingChanged_Click;
            _game.PairMatched += Form2PairMatched;
            _game.PairNotMatched += Form2PairNotMatched;

            Controls.Add(_game.backButton);
            Controls.Add(_game.settingButton);


            // 設定ダイアログの変更
            _game.settingDialog.Height = 275;
            _game.settingDialog.changeButton.Location
                = new Point(_game.settingDialog.changeButton.Location.X, 195);  // X座標はそのまま

            Panel playerPanel = new Panel()
            {
                Size = new Size(110, 110),
                Location = new Point(10, 110),
            };
            Label playerLabel = new Label()
            {
                Text = "プレイ人数",
                Font = new Font("Meiryo UI", 10),
                Location = new Point(5, 5)
            };
            playerRadioButton1 = new RadioButton()
            {
                Text = "2人",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 30),
                AutoSize = true
            };
            playerRadioButton2 = new RadioButton()
            {
                Text = "3人",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 55),
                AutoSize = true
            };
            playerRadioButton3 = new RadioButton()
            {
                Text = "4人",
                Font = new Font("Meiryo UI", 9),
                Location = new Point(20, 80),
                AutoSize = true
            };

            playerRadioButton1.CheckedChanged += PlayerRadioButton1_CheckedChanged;
            playerRadioButton2.CheckedChanged += PlayerRadioButton2_CheckedChanged;
            playerRadioButton3.CheckedChanged += PlayerRadioButton3_CheckedChanged;
            playerRadioButton1.Checked = true;  // チェックしておく

            _game.settingDialog.Controls.Add(playerPanel);
            playerPanel.Controls.Add(playerLabel);
            playerPanel.Controls.Add(playerRadioButton1);
            playerPanel.Controls.Add(playerRadioButton2);
            playerPanel.Controls.Add(playerRadioButton3);


            _game.playerNum = 2;

            _game.CardAdd(this, true);
            _game.CardRandom();
            PlayerUIVisible();
            Form2SizeChange();
            PlayerReset();
        }

        /// <summary>
        /// 現在プレイヤー、枚数、赤枠の初期化
        /// </summary>
        private void PlayerReset()
        {
            foreach (var p in playerUI)
            {
                p.Reset();  // 赤枠をfalseに、pntCnt,pntLabelを0にする
            }

            playerUI[0].FrameActive(true);
            playing = 0;
        }

        /// <summary>
        /// Form2でクリアしたときの処理
        /// </summary>
        private void Form2OnGameCleard()
        {
            List<string> winList = new List<string>();  // 勝者を格納
            int max = playerUI[0].pntCnt;             // 1人目を格納
            winList.Add(playerUI[0].nameLabel.Text);  

            for (int i = 1; i < playerUI.Count; i++)
            {
                if (max == playerUI[i].pntCnt)
                {
                    winList.Add(playerUI[i].nameLabel.Text);  // 今の最大値と同じならwinListに足す
                }
                else if (max < playerUI[i].pntCnt)
                {
                    max = playerUI[i].pntCnt;
                    winList.Clear();                          // 今の最大値より大きいならwinListをクリアし
                    winList.Add(playerUI[i].nameLabel.Text);  // 1番目に格納
                }
            }

            string word = "";
            if (winList.Count == _game.playerNum)  // 全プレイヤーが同点
            {
                word = "引き分け";
            }
            else if (winList.Count == 1)  // 勝者が1人
            {
                word = $"{winList[0]}の勝利！！！";
            }
            else if (winList.Count == 2)  // 勝者が2人
            {
                word = $"{winList[0]}, {winList[1]}の勝利！";
            }
            else if (winList.Count == 3)  // 勝者が3人
            {
                word = $"{winList[0]}, {winList[1]}, {winList[2]}の勝利！";
            }
            MessageBox.Show($"{word}", "結果");


            PlayerReset();

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
            playerUI[playing].AddPoint();
        }

        /// <summary>
        /// Form2でペアができなかったときの処理
        /// </summary>
        private void Form2PairNotMatched()
        {
            playerUI[playing].FrameActive(false);
            playing = (playing + 1) % _game.playerNum;
            playerUI[playing].FrameActive(true);
        }

        /// <summary>
        /// プレイ人数2人のラジオボタンがチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (playerRadioButton1.Checked)
            {
                _game.playerNum = 2;
            }
        }

        /// <summary>
        /// プレイ人数3人のラジオボタンがチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (playerRadioButton2.Checked)
            {
                _game.playerNum = 3;
            }
        }

        /// <summary>
        /// プレイ人数4人のラジオボタンがチェックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (playerRadioButton3.Checked)
            {
                _game.playerNum = 4;
            }
        }

        /// <summary>
        /// Form2で設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2SettingChanged_Click(object sender, EventArgs e)
        {
            _game.CardAdd(this, true);
            _game.CardRandom();

            PlayerUIVisible();
            Form2SizeChange();

            PlayerReset();
        }

        /// <summary>
        /// プレイヤーパネルの表示、非表示
        /// </summary>
        private void PlayerUIVisible()
        {
            for (int i = 0; i < playerUI.Count; i++)
            {
                playerUI[i].panel.Visible = (i < _game.playerNum);  // 右辺はtrueかfalseになる
            }
        }

        /// <summary>
        /// Form2のサイズ変更
        /// </summary>
        public void Form2SizeChange()
        {
            int formW = _game.formWidth;
            int formH = _game.formHeight;

            // 最小サイズを更新
            this.MinimumSize = new Size(formW + (this.Width - this.ClientSize.Width),
                                        formH + (this.Height - this.ClientSize.Height));
            // フォームサイズ変更
            this.ClientSize = new Size(formW, formH);

            // フォームを画面中央に再配置
            var screen = Screen.FromControl(this);  // スクリーン情報を取得
            var workingArea = screen.WorkingArea;  // 作業領域
            this.Location = new Point(
                workingArea.Left + (workingArea.Width - this.Width) / 2,
                workingArea.Top + (workingArea.Height - this.Height) / 2);

            // プレイヤーパネルの位置変更
            for (int i = 0; i < _game.playerNum; i++)
            {
                playerUI[i].panel.Location = new Point(
                    formW / (_game.playerNum + 1) * (i + 1) - playerUI[0].panel.Width / 2, 0);
            }

            // backButton、themeLabel、settingButtonの位置変更
            _game.backButton.Location = new Point(0, formH - _game.backButton.Height);
            _game.themeLabel.Location = new Point(formW / 2 - _game.themeLabel.Width / 2, formH - _game.themeLabel.Height - 10);
            _game.settingButton.Location = new Point(formW - _game.settingButton.Width, formH - _game.settingButton.Height);

        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
