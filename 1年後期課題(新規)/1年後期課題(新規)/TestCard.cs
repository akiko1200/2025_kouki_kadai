using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題_新規_
{
    public class TestCard : Button
    {
        /// <summary>on時の色</summary>
        private Color _frontColor = Color.Navy;

        /// <summary>off時の色</summary>
        private Color _backColor = Color.White;

        /// <summary>現在onかoffか</summary>
        private bool _enable;

        private Game _game;

        /// <summary>横位置</summary>
        private int _x;

        /// <summary>縦位置</summary>
        private int _y;

        private ImageList imageList1;
        private System.ComponentModel.IContainer components;

        ///// <summary>裏返すときに待機するタイマー</summary>
        //private Timer waitTimer;

        //public Action PairMatched;//aiueo

        public TestCard(Game game, int x, int y, Size size,
                int board_size_x, int board_size_y)
        {
            InitializeComponent();

            _game = game;

            // 横位置を保管
            _x = x;

            // 縦位置を保管
            _y = y;

            // カードの位置を設定
            Location = new Point(x * size.Width, game.cardOffset + y * size.Height);

            // カードの大きさを設定
            Size = size;

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
                Image = imageList1.Images[(int)Tag];
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
            // 裏返すタイマー待機中ならクリックを無視
            if (_game.isWaiting)
            {
                return;
            }

            // 押されたカードを格納
            TestCard card = _game.GetTestCard(_x, _y);

            card.SetEnable(true);

            if (_game.clickCard1 == null)
            {
                _game.clickCard1 = card;
            }
            else
            {
                _game.clickCard2 = card;
            }

            if (_game.clickCard1 != null && _game.clickCard2 != null)  // 2つ押された
            {
                if (_game.clickCard1 == _game.clickCard2)
                {
                    return;  // 同じボタンを押されたら無視
                }
                _game.OnTwoCardsRevealed();
            }

            if (_game.IsClear())
            {
                _game.OnGameClear();
            }


        }

        ///// <summary>
        ///// 正しいペアか判定
        ///// </summary>
        //private bool CheckPair()
        //{
        //    if ((int)_game.clickCard1?.Tag == (int)_game.clickCard2?.Tag)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// ペアができたとき、できなかったときの処理
        ///// </summary>
        //private void OnPairMatched()
        //{
        //    if (CheckPair())  // ペア完成
        //    {
        //        PairMatched?.Invoke();//aiueo

        //        _game.pairCnt++;

        //        _game.clickCard1.Enabled = false;
        //        _game.clickCard2.Enabled = false;

        //        _game.clickCard1 = null;
        //        _game.clickCard2 = null;
        //    }
        //    else  // ペア未完成
        //    {
        //        PairMatched?.Invoke();//aiueo

        //        _game.isWaiting = true;

        //        waitTimer = new Timer();
        //        waitTimer.Interval = 500;
        //        waitTimer.Tick += Wait_Timer_Tick;
        //        waitTimer.Start();
        //    }
        //}

        ///// <summary>
        ///// ペアが未完成のときに呼び出される
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Wait_Timer_Tick(object sender, EventArgs e)
        //{
        //    waitTimer.Stop();
        //    waitTimer.Dispose();
        //    _game.clickCard1.SetEnable(false);
        //    _game.clickCard2.SetEnable(false);

        //    _game.clickCard1 = null;
        //    _game.clickCard2 = null;

        //    _game.isWaiting = false;
        //}




        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestCard));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ハート.png");
            this.imageList1.Images.SetKeyName(1, "クローバー.png");
            this.imageList1.Images.SetKeyName(2, "ダイヤ.png");
            this.imageList1.Images.SetKeyName(3, "スペード.png");
            this.imageList1.Images.SetKeyName(4, "星.png");
            this.imageList1.Images.SetKeyName(5, "夜空.png");
            this.imageList1.Images.SetKeyName(6, "キラキラ.png");
            this.imageList1.Images.SetKeyName(7, "音符.png");
            this.imageList1.Images.SetKeyName(8, "太陽.png");
            this.imageList1.Images.SetKeyName(9, "肉球.png");
            this.imageList1.Images.SetKeyName(10, "王冠.png");
            this.imageList1.Images.SetKeyName(11, "水滴.png");
            this.imageList1.Images.SetKeyName(12, "雪だるま.png");
            this.imageList1.Images.SetKeyName(13, "雷.png");
            this.imageList1.Images.SetKeyName(14, "火の玉.png");
            this.imageList1.Images.SetKeyName(15, "雪の結晶.png");
            this.imageList1.Images.SetKeyName(16, "雨.png");
            this.imageList1.Images.SetKeyName(17, "ベル.png");
            this.imageList1.Images.SetKeyName(18, "チューリップ.png");
            this.imageList1.Images.SetKeyName(19, "桜.png");
            this.ResumeLayout(false);

        }
    }
}