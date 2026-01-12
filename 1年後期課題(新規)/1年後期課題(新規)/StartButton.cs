using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題_新規_
{
    public class StartButton : Button
    {
        private Game _game;

        private Form1 _form1;

        /// <summary>経過時間のタイマー</summary>
        private Timer cntTimer;

        /// <summary>経過時間のカウント</summary>
        public int counter;

        /// <summary>経過時間(秒)を保持</summary>
        public int s;

        /// <summary>経過時間(分)を保持</summary>
        public int m;

        /// <summary>プレイ中かどうか</summary>
        public bool isPlaying = false;


        public StartButton(Game game, Form1 form1)
        {
            _game = game;
            _form1 = form1;

            Size = new Size(130, 70);
            BackColor = Color.RoyalBlue;
            Text = "START";
            Font = new Font("Meiryo UI", 15);
            ForeColor = Color.White;

            Click += ClickEvent;

        }

        /// <summary>
        /// スタートボタンのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent(object sender, EventArgs e)
        {
            isPlaying = true;

            foreach (var card in _game._cardArray)
            {
                card.Enabled = true;  // カードを有効にする
            }

            cntTimer = new Timer();
            cntTimer.Interval = 1000;
            cntTimer.Start();
            cntTimer.Tick += Timer_Tick;

            counter = 0;

            Enabled = false;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            counter++;
            s = counter % 60;
            m = counter / 60;
            _form1.timeLabel.Text = $"{m:D2}:{s:D2}";
        }


        public void TimerStop()
        {
            isPlaying = false;
            cntTimer.Stop();
            Enabled = true;
        }


    }
}
