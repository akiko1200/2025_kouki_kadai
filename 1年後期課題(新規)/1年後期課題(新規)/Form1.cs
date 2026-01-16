using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace _1年後期課題_新規_
{
    public partial class Form1 : Form
    {
        private Game _game;

        private StartButton startButton;

        /// <summary>経過時間を表示</summary>
        public Label timeLabel;

        /// <summary>ベストタイム(レベル～)</summary>
        private Label bestLabel;//aiu

        /// <summary>ベストタイムを表示</summary>
        private Label[] bestTimeLabels = new Label[3];

        /// <summary>ベストタイムを格納</summary>
        private int?[] bestTimes;

        /// <summary>ベストタイムを記録するファイル</summary>
        private readonly string bestFile =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Shinkeisuijaku",
            "bestTime.txt"
        );


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

            bestLabel = new Label()
            {
                Font = new Font("Meiryo UI", 8),
                ForeColor = Color.Navy,
                AutoSize = true,
            };

            for (int i = 0; i < 3; i++)
            {
                bestTimeLabels[i] = new Label()
                {
                    Text = "-- : --",
                    Font = new Font("Meiryo UI", 10),
                    ForeColor = Color.Navy,
                    AutoSize = true,
                };
            }

            bestTimes = new int?[3];
            LoadBestTimes();//aiu

            //// 設定ダイアログの変更
            _game.settingDialog.Height = 245;
            _game.settingDialog.changeButton.Location
                = new Point(_game.settingDialog.changeButton.Location.X, 170);  // X座標はそのまま

            Label resetLabel = new Label()
            {
                Text = "ベストタイムのリセット",
                Font = new Font("Meiryo UI", 10),
                Location = new Point(15, 130),
                AutoSize = true
            };
            Button resetButton = new Button()
            {
                Location = new Point(40, 155),
                Text = "リセット",
                Font = new Font("Meiryo UI", 9),
                Size = new Size(55, 35),
                BackColor = Color.Silver,
            };

            resetButton.Click += ResetButton_Click;

            // 設定ダイアログにリセットボタンを追加
            _game.settingDialog.Controls.Add(resetLabel);
            _game.settingDialog.Controls.Add(resetButton);


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

            int clearSec = startButton.counter;
            string bestWord = null;

            int m = startButton.m;
            int s = startButton.s;

            if (bestTimes[GetLevel()] > clearSec || bestTimes[GetLevel()] == null)
            {
                bestTimes[GetLevel()] = clearSec;
                bestTimeLabels[GetLevel()].Text = $"{m:D2}:{s:D2}";
                bestWord = $"\r\nレベル{GetLevel() + 1}でベストタイム！！！";
            }

            if (startButton.m == 0)
            {
                MessageBox.Show($"{s}秒でクリア！{bestWord}", "結果");
            }
            else
            {
                MessageBox.Show($"{m}分{s}秒でクリア！{bestWord}", "結果");
            }

            startButton.m = 0;
            startButton.s = 0;
            timeLabel.Text = "00:00";

            SaveBestTimes();
        }

        /// <summary>
        /// ベストタイムをファイルから読み込む
        /// </summary>
        private void LoadBestTimes()
        {
            if (!File.Exists(bestFile))
            {
                return;  // ファイルが無ければなにもしない
            }

            var lines = File.ReadAllLines(bestFile);  // ファイルを1行ずつ読み込みstring[]で返す
            // linesは配列

            for (int i = 0; i < 3 && i < lines.Length; i++)
            {
                if (int.TryParse(lines[i], out int sec))  // 数値に変換出来たらsecに格納
                {
                    bestTimes[i] = sec;

                    int s = sec % 60;
                    int m = sec / 60;
                    bestTimeLabels[i].Text = $"{m:D2}:{s:D2}";
                }
                else
                {
                    bestTimes[i] = null;
                    bestTimeLabels[i].Text = "-- : --";
                }
            }
        }

        /// <summary>
        /// ベストタイムをファイルに書き込む
        /// </summary>
        private void SaveBestTimes()
        {
            string dir = Path.GetDirectoryName(bestFile);  // フォルダのパスを取り出す

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);  // フォルダが存在しなければ作る
            }

            string[] lines = new string[3];

            for (int i = 0; i < 3; i++)
            {
                lines[i] = bestTimes[i]?.ToString() ?? "";  // bestTimesがnullでないなら文字列にする
            }                                               // 左側がnullなら""

            File.WriteAllLines(bestFile, lines);  // ファイルに書き込む
        }

        /// <summary>
        /// ベストタイムをリセットする
        /// </summary>
        private void ResetBestTimes()
        {
            for (int i = 0; i < bestTimes.Length; i++)
            {
                bestTimes[i] = null;
                bestTimeLabels[i].Text = "-- : --";
            }

            if (File.Exists(bestFile))
            {
                File.Delete(bestFile);  // ファイルがあればファイルを消す
            }
        }

        /// <summary>
        /// ベストタイムのリセットボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ベストタイムがリセットされます。\r\n本当によろしいですか？",
                                                  "ベストタイムのリセット", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)  // はいが押されたとき
            {
                ResetBestTimes();
            }
        }

        /// <summary>
        /// 現在のレベルを取得
        /// </summary>
        private int GetLevel()
        {
            for (int i = 0; i < 3; i ++)
            {
                if (_game.BOARD_SIZE_X  == _game.board_size_array[i, 0] && _game.BOARD_SIZE_Y == _game.board_size_array[i, 1])
                {
                    return i;
                }
            }
            return -1;
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

            // bestLabel, bestTimeLabelsの削除
            Controls.Remove(bestLabel);
            for (int i = 0; i < bestTimeLabels.Length; i++)
            {
                Controls.Remove(bestTimeLabels[i]);
            }

            // bestLabel, bestTimeLabelsの位置変更、追加
            bestLabel.Location = new Point(formW - bestLabel.Width - 10, 15);
            bestLabel.Text = $"ベストタイム(レベル{GetLevel() + 1})";
            bestTimeLabels[GetLevel()].Location = new Point(bestLabel.Location.X + 20, bestLabel.Location.Y + 20);
            Controls.Add(bestLabel);
            Controls.Add(bestTimeLabels[GetLevel()]);

            // backButton、settingButtonの位置変更
            _game.backButton.Location = new Point(0, formH - _game.backButton.Height);
            _game.settingButton.Location = new Point(formW - _game.settingButton.Width, formH - _game.settingButton.Height);
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
