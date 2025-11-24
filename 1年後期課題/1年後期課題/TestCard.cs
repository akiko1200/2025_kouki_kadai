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
        //public Image CardImage { get; set; }


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
            Location = new Point(x * size.Width, y * size.Height);
            // カードの大きさを設定
            Size = size;

            // カードの列数を保管
            _board_size_x = board_size_x;

            // カードの行数を保管
            _board_size_y = board_size_y;

            SetEnable(false);

            //_form1.CardRandom();

            Click += ClickEvent;


        }

        /// <summary>onとoffの設定</summary>
        /// <param name="on"></param>
        private void SetEnable(bool on)
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
            _form1.GetTestCard(_x, _y).SetEnable(true);


            TestCard card = _form1.GetTestCard(_x, _y);
            
        }

    }
}
