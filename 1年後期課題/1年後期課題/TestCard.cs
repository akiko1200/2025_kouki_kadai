using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題
{
    public class TestCard : Button
    {
        /// <summary>on時の色</summary>
        private Color _frontColor = Color.Navy;

        /// <summary>off時の色</summary>
        private Color _backColor = Color.White;

        /// <summary>現在onかoffか</summary>
        private bool _enable;

        /// <summary>Form1の参照</summary>
        private Form1 _form1;

        /// <summary>横位置</summary>
        private int _x;

        /// <summary>縦位置</summary>
        private int _y;

        /// <summary>ボタンの列数</summary>
        private int _board_size_x;

        /// <summary>ボタンの行数</summary>
        private int _board_size_y;

        /// <summary>裏返すときに待機するタイマー</summary>
        private Timer waitTimer;


        public TestCard(Form1 form1, int x, int y, Size size,
                        int board_size_x, int board_size_y)
        {
            // Form1の参照を保管
            _form1 = form1;

            // 横位置を保管
            _x = x;

            // 縦位置を保管
            _y = y;

            // カードの位置を設定
            Location = new Point(x * size.Width, 80 + y * size.Height);
            // カードの大きさを設定
            Size = size;

            // カードの列数を保管
            _board_size_x = board_size_x;

            // カードの行数を保管
            _board_size_y = board_size_y;

            SetEnable(false);

            Click += ClickEvent;

        }

        /// <summary>裏と表の設定</summary>
        /// <param name="on"></param>
        public void SetEnable(bool on)
        {
            _enable = on;
            if (on)
            {
                BackColor = _backColor;
                Image = _form1.imageList1.Images[(int)Tag];
            }
            else
            {
                BackColor = _frontColor;
                Image = null;
            }
        }

        /// <summary>クリックイベント</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent(object sender, EventArgs e)
        {
            // 裏返すタイマー待機中かゲーム開始前ならクリックを無視
            if (_form1.isWaiting || _form1.isPlaying == false)
            {
                return;
            }
            // 押されたカードを格納
            TestCard card = _form1.GetTestCard(_x, _y);

            card.SetEnable(true);
            
            if (_form1.clickCard1 == null)
            {
                _form1.clickCard1 = card;
            }
            else
            {
                _form1.clickCard2 = card;
            }

            if (_form1.clickCard1 != null && _form1.clickCard2 != null)  // 2つ押された
            {
                if (_form1.clickCard1 == _form1.clickCard2)
                {
                    return;  // 同じボタンを押されたら無視
                }
                OnPairMatched();
            }
            
            _form1.OnGameClear();
        }

        /// <summary>
        /// 正しいペアか判定
        /// </summary>
        private bool CheckPair()
        {
            if ((int)_form1.clickCard1?.Tag == (int)_form1.clickCard2?.Tag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ペアができたとき、できなかったときの処理
        /// </summary>
        private void OnPairMatched()
        {
            if (CheckPair())  // ペア完成
            {
                _form1.pairCnt++;

                _form1.clickCard1.Enabled = false;
                _form1.clickCard2.Enabled = false;

                _form1.clickCard1 = null;
                _form1.clickCard2 = null;
            }
            else  // ペア未完成
            {
                _form1.isWaiting = true;

                waitTimer = new Timer();
                waitTimer.Interval = 500;
                waitTimer.Tick += Wait_Timer_Tick;
                waitTimer.Start();
            }
        }

        /// <summary>
        /// ペアが未完成のときに呼び出される
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wait_Timer_Tick(object sender, EventArgs e)
        {
            waitTimer.Stop();
            waitTimer.Dispose();
            _form1.clickCard1.SetEnable(false);
            _form1.clickCard2.SetEnable(false);

            _form1.clickCard1 = null;
            _form1.clickCard2 = null;

            _form1.isWaiting = false;
        }


    }
}
