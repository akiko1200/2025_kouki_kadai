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
        /// <summary>カードの横幅</summary>
        const int CARD_SIZE_X = 90;

        /// <summary>カードの縦幅</summary>
        const int CARD_SIZE_Y = 90;

        /// <summary>カードが横に何個並ぶか</summary>
        const int BOARD_SIZE_X = 6;

        /// <summary>カードが縦に何個並ぶか</summary>
        const int BOARD_SIZE_Y = 5;

        /// <summary>TestCardの二次元配列</summary>
        private TestCard[,] _cardArray;

        /// <summary>どこがペアか格納する二次元配列</summary>
        int[,] pairArray = new int[BOARD_SIZE_X * BOARD_SIZE_Y, 2];

        public Form1()
        {
            InitializeComponent();

            // どこがペアか
            for (int i = 0; i < BOARD_SIZE_Y; i++)
            {
                for (int j = 0; j < BOARD_SIZE_X; j++)
                {
                    pairArray[i * BOARD_SIZE_X + j, 0] = i;
                    pairArray[i * BOARD_SIZE_X + j, 1] = j;
                }
            }
            //{0, 0},
            //{0, 1},
            //{0, 2},
            //{1, 0},
            //{1, 1},
            //{1, 2}



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
                            //"",
                            BOARD_SIZE_X, BOARD_SIZE_Y);

                    // 配列にカードの参照を追加
                    _cardArray[j, i] = testCard;

                    // コントロールにカードを追加
                    Controls.Add(testCard);
                }
            }
            CardRandom();

        }

        /// <summary>TestCardを取得する関数</summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public TestCard GetTestCard(int x, int y)
        {
            // 配列外参照対策
            if (x < 0 || x >= BOARD_SIZE_X) return null;
            if (y < 0 || y >= BOARD_SIZE_Y) return null;

            return _cardArray[y, x];
        }

        

        public void CardRandom()
        {
            // 0～カード数 の数字を配列に入れる
            int[] randNumbers = new int[BOARD_SIZE_X * BOARD_SIZE_Y];
            for (int i = 0; i < BOARD_SIZE_X * BOARD_SIZE_Y; i++)
            {
                randNumbers[i] = i;
            }
            Random random = new Random();

            for (int i = randNumbers.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                // randNumbersの中をランダムに
                int temp = randNumbers[i];
                randNumbers[i] = randNumbers[j];
                randNumbers[j] = temp;
            }

            for (int i = 0; i < (BOARD_SIZE_X * BOARD_SIZE_Y) / 2; i++)
            {
                TestCard card_1 = GetTestCard(pairArray[randNumbers[2 * i], 1], pairArray[randNumbers[2 * i], 0]);
                TestCard card_2 = GetTestCard(pairArray[randNumbers[2 * i + 1], 1], pairArray[randNumbers[2 * i + 1], 0]);

                if (card_1 != null)
                {
                    card_1.Image = imageList1.Images[i];
                }
                if (card_2 != null)
                {
                    card_2.Image = imageList1.Images[i];
                }
                //同じ画像なら同じタグ
                card_1.Tag = i;
                card_2.Tag = i;
            }
            

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
