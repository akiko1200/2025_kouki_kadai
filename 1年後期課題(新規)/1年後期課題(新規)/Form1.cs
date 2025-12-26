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
    public partial class Form1 : Form
    {
        private Game _game;

        public StartButton startButton;

        public Label timeLabel;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            _game = new Game(3, 2, this);  //aiu

            startButton = new StartButton(_game, this);
            Controls.Add(startButton);

            timeLabel = new Label()
            {
                Text = "00:00",
                Location = new Point(220, 15),
                Font = new Font("Meiryo UI", 20),
                AutoSize = true,
            };
            Controls.Add(timeLabel);

            _game.GameCleard += Form1OnGameCleard;

            Controls.Add(_game.backButton);

            Controls.Add(_game.settingButton);

            _game.settingDialog.changeButton.Click += Form1SettingChanged_Click;
            
            //_game.settingDialog.ShowDialog();

            Form1CardAdd();
            _game.CardRandom();
            Form1SizeChange();


        }

        /// <summary>
        /// Form1のカードの追加
        /// </summary>
        private void Form1CardAdd()
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
                    TestCard testCard =
                        new TestCard(
                            //aiu
                            _game,
                            i, j,
                            new Size(_game.CARD_SIZE_X, _game.CARD_SIZE_Y),
                            _game.BOARD_SIZE_X, _game.BOARD_SIZE_Y);

                    // スタートボタンが押されるまでカードを無効にする
                    testCard.Enabled = false;

                    // 配列にカードの参照を追加
                    _game._cardArray[j, i] = testCard;

                    // コントロールにカードを追加
                    Controls.Add(testCard);
                }
            }
        }

        /// <summary>
        /// Form1でクリアしたときの処理
        /// </summary>
        private void Form1OnGameCleard()
        {
            startButton.TimerStop();
            if (startButton.m == 0)
            {
                MessageBox.Show($"{startButton.s}秒でクリア！！！", "おめでとう");
            }
            else
            {
                MessageBox.Show($"{startButton.m}分{startButton.s}秒でクリア！！！", "おめでとう");
            }
            startButton.m = 0;
            startButton.s = 0;
            timeLabel.Text = "00:00";
        }

        /// <summary>
        /// Form1で設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1SettingChanged_Click(object sender, EventArgs e)
        {
            Form1CardAdd();
            _game.CardRandom();
            Form1SizeChange();
            
            _game.pairCnt = 0;

            if (startButton.isPlaying == true)  // ゲーム中なら
            {
                // カード枚数を変えたときタイマーをストップする
                startButton.TimerStop();
                startButton.m = 0;
                startButton.s = 0;
                timeLabel.Text = "00:00";

            }
        }

        /// <summary>
        /// Form1のサイズ変更
        /// </summary>
        public void Form1SizeChange()
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

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
