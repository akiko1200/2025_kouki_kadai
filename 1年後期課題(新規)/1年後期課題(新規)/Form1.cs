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

        private StartButton startButton;

        /// <summary>経過時間を表示</summary>
        public Label timeLabel;


        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            _game = new Game(this);

            startButton = new StartButton(_game, this);
            Controls.Add(startButton);

            timeLabel = new Label()
            {
                Text = "00:00",
                Font = new Font("Meiryo UI", 20),
                AutoSize = true,
            };
            Controls.Add(timeLabel);
            
            _game.GameCleard += Form1OnGameCleard;

            Controls.Add(_game.backButton);

            Controls.Add(_game.settingButton);

            _game.settingDialog.changeButton.Click += Form1SettingChanged_Click;

            _game.CardAdd(this, false);
            _game.CardRandom();
            Form1SizeChange();
        }

        /// <summary>
        /// Form1でクリアしたときの処理
        /// </summary>
        private void Form1OnGameCleard()
        {
            startButton.TimerStop();

            if (startButton.m == 0)
            {
                MessageBox.Show($"{startButton.s}秒でクリア！", "結果");
            }
            else
            {
                MessageBox.Show($"{startButton.m}分{startButton.s}秒でクリア！", "結果");
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
            _game.CardAdd(this, false);
            _game.CardRandom();
            Form1SizeChange();
            
            if (startButton.isPlaying == true)  // ゲーム中なら
            {
                // タイマーをストップする
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
            int formW = _game.formWidth;
            int formH = _game.formHeight;

            // 最小サイズを更新
            this.MinimumSize = new Size(formW + (this.Width - this.ClientSize.Width),
                                        formH + (this.Height - this.ClientSize.Height));

            this.ClientSize = new Size(formW, formH);

            // 中央に再配置
            var screen = Screen.FromControl(this);  // スクリーン情報を取得
            var workingArea = screen.WorkingArea;  // 作業領域
            this.Location = new Point(
                workingArea.Left + (workingArea.Width - this.Width) / 2,
                workingArea.Top + (workingArea.Height - this.Height) / 2);


            // timeLabel、startButtonの位置変更
            timeLabel.Location = new Point(formW / 2 + 10, 15);
            startButton.Location = new Point(formW / 2 - startButton.Width - 10, 0);

            // backButton、settingButtonの位置変更
            _game.backButton.Location = new Point(0, formH - _game.backButton.Height);
            _game.settingButton.Location = new Point(formW - _game.settingButton.Width, formH - _game.settingButton.Height);
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
