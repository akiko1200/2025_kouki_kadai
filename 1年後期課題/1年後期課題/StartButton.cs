using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題
{
    internal class StartButton : Button
    {
        /// <summary>Form1の参照</summary>
        private Form1 _form1;

        /// <summary>ボタンの列数</summary>
        //private int _board_size_x;

        /// <summary>ボタンの行数</summary>
        //private int _board_size_y;

        /// <summary>TestCardの参照</summary>
        private TestCard _testCard;

        /// <summary>経過時間のタイマー</summary>
        private Timer cntTimer;

        /// <summary>経過時間のカウント</summary>
        public int counter;


        public StartButton(Form1 form1/*, int board_size_x, int board_size_y*/)
        {
            _form1 = form1;

            Size = new Size(180, 70);
            BackColor = Color.RoyalBlue;
            Text = "START";
            Font = new Font("Meiryo UI", 15);
            ForeColor = Color.White;
            //Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            Click += ClickEvent;
        }

        /// <summary>
        /// スタートボタンのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent(object sender, EventArgs e)
        {
            _form1.isPlaying = true;

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
            int s = counter % 60;
            int m = counter / 60;
            _form1.timeLabel.Text = $"{m.ToString("D2")}:{s.ToString("D2")}";
        }


        public void TimerStop()
        {
            _form1.isPlaying = false;
            cntTimer.Stop();
            Enabled = true;
        }

    }
}
