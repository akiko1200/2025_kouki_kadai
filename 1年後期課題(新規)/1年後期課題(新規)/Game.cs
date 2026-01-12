using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _1年後期課題_新規_
{
    public class Game
    {
        const int card_size_x = 90;
        /// <summary>カードの横幅</summary>
        public int CARD_SIZE_X
        {
            get { return card_size_x; }
        }

        const int card_size_y = 90;
        /// <summary>カードの縦幅</summary>
        public int CARD_SIZE_Y
        {
            get { return card_size_y; }
        }

        public int BOARD_SIZE_X { get; set; }
        public int BOARD_SIZE_Y { get; set; }

        /// <summary>縦×横の枚数</summary>
        public int[,] board_size_array =
        {
            {5, 4},
            {6, 5},
            {8, 5}
        };

        /// <summary>絵柄のインデックスを格納</summary>
        public int cardThemeIndex = 0;

        public Form _thisForm;

        /// <summary>TestCardの二次元配列</summary>
        public TestCard[,] _cardArray;

        /// <summary>1つ目に押されたカードを保持</summary>
        public TestCard clickCard1 = null;

        /// <summary>2つ目に押されたカードを保持</summary>
        public TestCard clickCard2 = null;

        /// <summary>裏返すタイマーの待機中か</summary>
        public bool isWaiting = false;

        /// <summary>できたペアの数summary>
        public int pairCnt;

        /// <summary>カードの上のスペース</summary>
        public int cardOffset = 80;

        /// <summary>フォームの横幅</summary>
        public int formWidth;

        /// <summary>フォームの縦幅</summary>
        public int formHeight;

        public event Action GameCleard;

        /// <summary>前の画面に戻る</summary>
        public Button backButton;

        public Button settingButton;

        public SettingDialog settingDialog;

        /// <summary>裏返すときに待機するタイマー</summary>
        private Timer waitTimer;

        public Action PairMatched;
        public Action PairNotMatched;

        /// <summary>form2のプレイ人数を格納</summary>
        public int playerNum;

        public Game(Form thisForm)
        {
            _thisForm = thisForm;

            // 最初のカード枚数
            BOARD_SIZE_X = board_size_array[0, 0];
            BOARD_SIZE_Y = board_size_array[0, 1];

            backButton = new Button()
            {
                Size = new Size(80, 35),
                Text = "＜ 戻る",
                Font = new Font("Meiryo UI", 10),
            };
            backButton.Click += back_button_Click;

            settingButton = new Button()
            {
                Size = new Size(70, 35),
                Text = "設定",
                Font = new Font("Meiryo UI", 10),
                BackColor = Color.Silver,
            };
            settingButton.Click += SettingButton_Click;

            settingDialog = new SettingDialog(this, board_size_array);

            formWidth = BOARD_SIZE_X * CARD_SIZE_X;
            formHeight = BOARD_SIZE_Y * CARD_SIZE_Y + cardOffset + backButton.Height;

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
            // カードのインデックスを格納する二次元配列
            int[,] indexArray = new int[BOARD_SIZE_X * BOARD_SIZE_Y, 2];
            // カードのインデックスを格納
            for (int i = 0; i < BOARD_SIZE_Y; i++)
            {
                for (int j = 0; j < BOARD_SIZE_X; j++)
                {
                    indexArray[i * BOARD_SIZE_X + j, 0] = i;
                    indexArray[i * BOARD_SIZE_X + j, 1] = j;
                }
            }

            // 0～カード枚数 の数字を配列に入れる
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

            // ペアの作成
            for (int i = 0; i < (BOARD_SIZE_X * BOARD_SIZE_Y) / 2; i++)
            {
                TestCard card_1 = GetTestCard(indexArray[randNumbers[2 * i], 1], indexArray[randNumbers[2 * i], 0]);
                TestCard card_2 = GetTestCard(indexArray[randNumbers[2 * i + 1], 1], indexArray[randNumbers[2 * i + 1], 0]);

                // ペアで同じタグ
                card_1.Tag = i;
                card_2.Tag = i;
            }

        }

        /// <summary>
        /// 正しいペアか判定
        /// </summary>
        private bool CheckPair()
        {
            return (int)clickCard1?.Tag == (int)clickCard2?.Tag;
        }

        /// <summary>
        /// 2枚めくられた後の処理
        /// </summary>
        public void OnTwoCardsRevealed()
        {
            if (CheckPair())  // ペア完成
            {
                // PairMatchedがnullじゃないならPairMatchedを実行
                PairMatched?.Invoke();

                pairCnt++;

                clickCard1.Enabled = false;
                clickCard2.Enabled = false;

                clickCard1 = null;
                clickCard2 = null;
            }
            else  // ペア未完成
            {
                // PairNotMatchedがnullじゃないならPairNotMatchedを実行
                PairNotMatched?.Invoke();

                isWaiting = true;

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
            clickCard1.SetEnable(false);
            clickCard2.SetEnable(false);

            clickCard1 = null;
            clickCard2 = null;

            isWaiting = false;
        }


        /// <summary>
        /// クリア判定
        /// </summary>
        public bool IsClear()
        {
            return pairCnt == (BOARD_SIZE_X * BOARD_SIZE_Y) / 2;
        }

        /// <summary>
        /// クリア時の処理
        /// </summary>
        public void OnGameClear()
        {
            // GameCleardがnullじゃないならGameCleardを実行
            GameCleard?.Invoke();

            pairCnt = 0;

            CardRandom();
            for (int i = 0; i < BOARD_SIZE_X; i++)
            {
                for (int j = 0; j < BOARD_SIZE_Y; j++)
                {
                    GetTestCard(i, j).SetEnable(false);
                }
            }
        }

        /// <summary>
        /// 設定ボタンのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingButton_Click(object sender, EventArgs e)
        {
            settingDialog.ShowDialog();
        }


        private void back_button_Click(object sender, EventArgs e)
        {
            Program.Display_form0();
            _thisForm.Close();
        }

    }
}
