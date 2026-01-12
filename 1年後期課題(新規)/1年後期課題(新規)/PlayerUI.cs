using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題_新規_
{
    internal class PlayerUI
    {
        public Panel panel { get; }
        
        /// <summary>Player1,2,3,4を表示するラベル</summary>
        public Label nameLabel { get; }

        /// <summary>枚数を表示するラベル</summary>
        private Label pntLabel { get; }

        /// <summary>赤枠を表示</summary>
        public PictureBox frame { get; }

        /// <summary>枚数を格納</summary>
        public int pntCnt{ get; private set; }

        const int panelWidth = 75;
        const int panelHeight = 70;


        public PlayerUI(string name, Color color)
        {
            panel = new Panel()
            {
                Size = new Size(panelWidth, panelHeight),
            };

            nameLabel = new Label()
            {
                Text = name,
                Font = new Font("Meiryo UI", 12),
                ForeColor = color,
                Location = new Point(5, 10),
                AutoSize = true,
            };

            pntLabel = new Label()
            {
                Font = new Font("Meiryo UI", 15),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(36, 25),
                Location = new Point(20, 35),
            };

            frame = new PictureBox()
            {
                Size = new Size(panelWidth, panelHeight),
                Location = new Point(0, 0),
                Image = Properties.Resources.赤枠,
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            panel.Controls.Add(nameLabel);
            panel.Controls.Add(pntLabel);
            panel.Controls.Add(frame);

        }

        public void Reset()
        {
            frame.Visible = false;
            pntCnt = 0;
            pntLabel.Text = "0";
        }

        public void AddPoint()
        {
            pntCnt += 2;
            pntLabel.Text = pntCnt.ToString();
        }

        /// <summary>
        /// 赤枠の表示、非表示
        /// </summary>
        /// <param name="active"></param>
        public void FrameActive(bool active)
        {
            frame.Visible = active;
        }

    }
}
