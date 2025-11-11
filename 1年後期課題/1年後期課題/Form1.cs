using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// カードの横幅
        /// </summary>
        const int CARD_SIZE_X = 100;

        /// <summary>
        /// カードの縦幅
        /// </summary>
        const int CARD_SIZE_Y = 100;

        /// <summary>
        /// カードが横に何個並ぶか
        /// </summary>
        const int BOARD_SIZE_X = 3;

        /// <summary>
        /// カードが縦に何個並ぶか
        /// </summary>
        const int BOARD_SIZE_Y = 2;

        /// <summary>
        /// TestCardの二次元配列
        /// </summary>
        private TestCard[,] _cardArray;


        public Form1()
        {
            InitializeComponent();

            // _cardArrayの初期化
            _cardArray = new TestCard[BOARD_SIZE_Y, BOARD_SIZE_X];

            for (int i = 0; i < BOARD_SIZE_X; i++)
            {
                for (int j = 0; j < BOARD_SIZE_Y; j++)
                {
                    // インスタンスの作成
                    TestCard testCard =
                        new TestCard(
                            this,
                            i, j,
                            new Size(CARD_SIZE_X, CARD_SIZE_Y),
                            "",
                            BOARD_SIZE_X, BOARD_SIZE_Y);

                    // 配列にカードの参照を追加
                    _cardArray[j, i] = testCard;

                    // コントロールにカードを追加
                    Controls.Add(testCard);
                }
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
