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

        /// <summary>絵柄のImageListを格納</summary>
        private ImageList[] cardThemeArray;

        private ImageList imageList1;
        private ImageList imageList2;
        private ImageList imageList3;
        private ImageList imageList4;

        private System.ComponentModel.IContainer components;


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

            cardThemeArray = new ImageList[] { imageList1, imageList2, imageList3, imageList4};
        }


        /// <summary>裏と表の設定</summary>
        /// <param name="on"></param>
        public void SetEnable(bool on)
        {
            _enable = on;
            if (on)
            {
                BackColor = _backColor;
                Image = cardThemeArray[_game.cardThemeIndex].Images[(int)Tag];
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestCard));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.imageList4 = new System.Windows.Forms.ImageList(this.components);
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
            this.imageList1.Images.SetKeyName(6, "音符.png");
            this.imageList1.Images.SetKeyName(7, "キラキラ.png");
            this.imageList1.Images.SetKeyName(8, "太陽.png");
            this.imageList1.Images.SetKeyName(9, "肉球.png");
            this.imageList1.Images.SetKeyName(10, "王冠.png");
            this.imageList1.Images.SetKeyName(11, "リボン.png");
            this.imageList1.Images.SetKeyName(12, "水滴.png");
            this.imageList1.Images.SetKeyName(13, "雪だるま.png");
            this.imageList1.Images.SetKeyName(14, "雷.png");
            this.imageList1.Images.SetKeyName(15, "火の玉.png");
            this.imageList1.Images.SetKeyName(16, "雪の結晶.png");
            this.imageList1.Images.SetKeyName(17, "雨.png");
            this.imageList1.Images.SetKeyName(18, "ベル.png");
            this.imageList1.Images.SetKeyName(19, "家.png");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "リンゴ.png");
            this.imageList2.Images.SetKeyName(1, "さくらんぼ.png");
            this.imageList2.Images.SetKeyName(2, "バナナ.png");
            this.imageList2.Images.SetKeyName(3, "桃.png");
            this.imageList2.Images.SetKeyName(4, "プリン.png");
            this.imageList2.Images.SetKeyName(5, "ラーメン.png");
            this.imageList2.Images.SetKeyName(6, "寿司.png");
            this.imageList2.Images.SetKeyName(7, "アイスクリーム.png");
            this.imageList2.Images.SetKeyName(8, "ケーキ.png");
            this.imageList2.Images.SetKeyName(9, "イチゴ.png");
            this.imageList2.Images.SetKeyName(10, "だんご.png");
            this.imageList2.Images.SetKeyName(11, "おにぎり.png");
            this.imageList2.Images.SetKeyName(12, "ドーナツ.png");
            this.imageList2.Images.SetKeyName(13, "レモン.png");
            this.imageList2.Images.SetKeyName(14, "キャンディ.png");
            this.imageList2.Images.SetKeyName(15, "トウモロコシ.png");
            this.imageList2.Images.SetKeyName(16, "ハンバーガー.png");
            this.imageList2.Images.SetKeyName(17, "ホットケーキ.png");
            this.imageList2.Images.SetKeyName(18, "ミカン.png");
            this.imageList2.Images.SetKeyName(19, "ぶどう.png");
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "犬.png");
            this.imageList3.Images.SetKeyName(1, "猫.png");
            this.imageList3.Images.SetKeyName(2, "リス.png");
            this.imageList3.Images.SetKeyName(3, "ヒツジ.png");
            this.imageList3.Images.SetKeyName(4, "ヒヨコ.png");
            this.imageList3.Images.SetKeyName(5, "象.png");
            this.imageList3.Images.SetKeyName(6, "カバ.png");
            this.imageList3.Images.SetKeyName(7, "フラミンゴ.png");
            this.imageList3.Images.SetKeyName(8, "牛.png");
            this.imageList3.Images.SetKeyName(9, "クジラ.png");
            this.imageList3.Images.SetKeyName(10, "ブタ.png");
            this.imageList3.Images.SetKeyName(11, "馬.png");
            this.imageList3.Images.SetKeyName(12, "コアラ.png");
            this.imageList3.Images.SetKeyName(13, "ペンギン.png");
            this.imageList3.Images.SetKeyName(14, "チョウ.png");
            this.imageList3.Images.SetKeyName(15, "こうもり.png");
            this.imageList3.Images.SetKeyName(16, "クラゲ.png");
            this.imageList3.Images.SetKeyName(17, "カニ.png");
            this.imageList3.Images.SetKeyName(18, "クマノミ.png");
            this.imageList3.Images.SetKeyName(19, "パンダ.png");
            // 
            // imageList4
            // 
            this.imageList4.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList4.ImageStream")));
            this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList4.Images.SetKeyName(0, "チューリップ.png");
            this.imageList4.Images.SetKeyName(1, "桜.png");
            this.imageList4.Images.SetKeyName(2, "梅.png");
            this.imageList4.Images.SetKeyName(3, "バラ.png");
            this.imageList4.Images.SetKeyName(4, "葉.png");
            this.imageList4.Images.SetKeyName(5, "竹.png");
            this.imageList4.Images.SetKeyName(6, "たけのこ.png");
            this.imageList4.Images.SetKeyName(7, "木.png");
            this.imageList4.Images.SetKeyName(8, "つくし.png");
            this.imageList4.Images.SetKeyName(9, "ドングリ.png");
            this.imageList4.Images.SetKeyName(10, "松.png");
            this.imageList4.Images.SetKeyName(11, "イチョウ.png");
            this.imageList4.Images.SetKeyName(12, "椿.png");
            this.imageList4.Images.SetKeyName(13, "ヒイラギ.png");
            this.imageList4.Images.SetKeyName(14, "やしの木.png");
            this.imageList4.Images.SetKeyName(15, "サボテン.png");
            this.imageList4.Images.SetKeyName(16, "小麦.png");
            this.imageList4.Images.SetKeyName(17, "切り株.png");
            this.imageList4.Images.SetKeyName(18, "もみじ.png");
            this.imageList4.Images.SetKeyName(19, "ハイビスカス.png");
            this.ResumeLayout(false);

        }
    }
}