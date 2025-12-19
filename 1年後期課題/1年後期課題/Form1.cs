using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _1年後期課題
{
    public partial class Form1 : Form
    {
        /// <summary>カードの横幅</summary>
        const int CARD_SIZE_X = 90;

        /// <summary>カードの縦幅</summary>
        const int CARD_SIZE_Y = 90;
        
        /// <summary>
        /// 縦×横の枚数
        /// </summary>
        public int[,] board_size_array =
        {
            {5, 4},
            {6, 5},
            {8, 5}
        };

        public int BOARD_SIZE_X { get; set; }
        public int BOARD_SIZE_Y { get; set; }


        /// <summary>TestCardの二次元配列</summary>
        private TestCard[,] _cardArray;

        /// <summary>1つ目に押されたカードを保持</summary>
        public TestCard clickCard1 = null;

        /// <summary>2つ目に押されたカードを保持</summary>
        public TestCard clickCard2 = null;

        /// <summary>裏返すタイマーの待機中か</summary>
        public bool isWaiting = false;

        /// <summary>できたペアの数summary>
        public int pairCnt;

        private StartButton startButton;

        /// <summary>経過時間を記録するラベル</summary>
        public Label timeLabel;

        private Button settingButton;

        /// <summary>前の画面に戻る</summary>
        private Button backButton;

        private SettingDialog settingDialog;

        /// <summary>プレイ中かどうか</summary>
        public bool isPlaying = false;

        
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;


            startButton = new StartButton(this);
            Controls.Add(startButton);

            timeLabel = new Label()
            {
                Text = "00:00",
                Location = new Point(220, 15),
                Font = new Font("Meiryo UI", 20),
                //BackColor = Color.Aqua,
                AutoSize = true,
            };
            Controls.Add(timeLabel);

            settingButton = new Button()
            {
                Location = new Point(350, 15),
                Size = new Size(70, 40),
                //BackColor = Color.RoyalBlue,
                Text = "設定",
                Font = new Font("Meiryo UI", 10),
                //ForeColor = Color.White,
            };
            settingButton.Click += settingButton_Click;
            Controls.Add(settingButton);

            Size size = this.Size;
            int height = size.Height;
            backButton = new Button()
            {
                Size = new Size(80, 35),
                Text = "＜ 戻る",
                Font = new Font("Meiryo UI", 10),
            };
            backButton.Click += back_button_Click;
            Controls.Add(backButton);

            // 最初のカード枚数
            BOARD_SIZE_X = board_size_array[0, 0];
            BOARD_SIZE_Y = board_size_array[0, 1];

            settingDialog = new SettingDialog(this, startButton, board_size_array);
            settingDialog.ShowDialog();

            CardReset();

            //GetTestCard(0, 0).Tag = 0;
            //GetTestCard(0, 1).Tag = 0;
            //GetTestCard(1, 0).Tag = 1;
            //GetTestCard(1, 1).Tag = 1;
            //GetTestCard(2, 0).Tag = 2;
            //GetTestCard(2, 1).Tag = 2;
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

        /// <summary>
        /// カードの初期化
        /// </summary>
        public void CardReset()
        {
            if (_cardArray != null)  // 2回目以降なら
            {
                foreach (var card in _cardArray)
                {
                    if (card != null)
                    {
                        Controls.Remove(card);  // 元のカードを削除
                        card.Dispose();  // リソースを解放
                    }
                }
            }

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
            FormSizeAuto();
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
        /// 別のランダム方法
        /// </summary>
        public void CardRandom2()
        {
            int[] tagArray = new int[BOARD_SIZE_X * BOARD_SIZE_Y];
            for (int i = 0; i < (BOARD_SIZE_X * BOARD_SIZE_Y / 2); i++)
            {
                tagArray[i * 2] = i;
                tagArray[i * 2 + 1] = i;
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

            int cnt = 0;

            for (int i = 0; i < BOARD_SIZE_X; i++)
            {
                for (int j = 0; j < BOARD_SIZE_Y; j++)
                {
                    TestCard card_1 = GetTestCard(i, j);

                    card_1.Tag = tagArray[randNumbers[cnt]];
                    cnt++;
                }
            }

        }

        public void FormSizeAuto()
        {
            int width = BOARD_SIZE_X * CARD_SIZE_X;
            int height = BOARD_SIZE_Y * CARD_SIZE_Y + 80;                             // 戻るボタン

            this.ClientSize = new Size(width, height);

            // 中央に再配置
            var screen = Screen.FromControl(this);  // スクリーン情報を取得
            var workingArea = screen.WorkingArea;  // 作業領域
            this.Location = new Point(
                workingArea.Left + (workingArea.Width - this.Width) / 2,
                workingArea.Top + (workingArea.Height - this.Height) / 2);
            backButton.Location = new Point(0, height);
        }


        /// <summary>
        /// クリア判定
        /// </summary>
        public bool IsClear()
        {
            if (pairCnt == (BOARD_SIZE_X * BOARD_SIZE_Y) / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// クリア時の処理
        /// </summary>
        public void OnGameClear()
        {
            if (IsClear())
            {
                startButton.TimerStop();
                MessageBox.Show("クリア！！！", "おめでとう");

                pairCnt = 0;
                timeLabel.Text = "00:00";

                CardRandom();
                for (int i = 0; i < BOARD_SIZE_X; i++)
                {
                    for (int j = 0; j < BOARD_SIZE_Y; j++)
                    {
                        GetTestCard(i, j).Enabled = true;
                        GetTestCard(i, j).SetEnable(false);
                    }
                }
            }
        }

        /// <summary>
        /// 設定ボタンのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingButton_Click(object sender, EventArgs e)
        {
            settingDialog.ShowDialog();
        }


        private void back_button_Click(object sender, EventArgs e)
        {
            Program.Display_form0();
            this.Close();
        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
