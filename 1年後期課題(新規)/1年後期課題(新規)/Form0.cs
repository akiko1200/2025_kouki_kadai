using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題_新規_
{
    public partial class Form0 : Form
    {
        private Label titleLabel;

        private Button form1_button;
        private Button form2_button;

        private Label label;

        public Form0()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Location = new Point(20, 40),
                AutoSize = true,
                Text = "🃏神経衰弱🤔",
                Font = new Font("Noto Sans JP Black", 30),
                ForeColor = Color.Navy,
            };
            Controls.Add(titleLabel);

            form1_button = new Button()
            {
                Location = new Point(78, 130),
                Size = new Size(180, 70),
                Text = "1人でPlay!",
                Font = new Font("Meiryo UI", 13),
                BackColor = Color.CornflowerBlue,
            };
            form1_button.Click += form1_button_Click;

            form2_button = new Button()
            {
                Location = new Point(78, 210),
                Size = new Size(180, 70),
                Text = "2～4人でPlay!",
                Font = new Font("Meiryo UI", 13),
                BackColor = Color.CornflowerBlue,
            };
            form2_button.Click += form2_button_Click;

            Controls.Add(form1_button);
            Controls.Add(form2_button);

            label = new Label()
            {
                Location = new Point(30, 310),
                AutoSize = true,
                Text = "♣️ ♦️ ♠️ ♥️ ♣️ ♦️ ♠️ ♥️ ♣️ ♦️ ♠",
                Font = new Font("Noto Sans JP Black", 15),
                ForeColor = Color.Navy,
            };
            Controls.Add(label);

        }

        private void form1_button_Click(object sender, EventArgs e)
        {
            Program.Display_form1();
            this.Close();
        }


        private void form2_button_Click(object sender, EventArgs e)
        {
            Program.Display_form2();
            this.Close();
        }

        private void Form0_Load(object sender, EventArgs e)
        {

        }
    }
}
