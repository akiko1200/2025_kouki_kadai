using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題
{
    public partial class Form0 : Form
    {
        private Button form1_button = new Button();
        private Button form2_button = new Button();

        public Form0()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            form1_button.Location = new Point(70, 130);
            form1_button.Size = new Size(180, 70);
            //form1_button.AutoSize = true;
            form1_button.Text = "1";
            form1_button.Font = new Font("Meiryo UI", 10);
            form1_button.BackColor = Color.LemonChiffon;
            //form1_button.ForeColor = Color.;
            form1_button.Click += form1_button_Click;

            form2_button.Location = new Point(70, 210);
            form2_button.Size = new Size(180, 70);
            //form2_button.AutoSize = true;
            form2_button.Text = "2";
            form2_button.Font = new Font("Meiryo UI", 10);
            form2_button.BackColor = Color.LemonChiffon;
            //form2_button.ForeColor = Color.;
            form2_button.Click += form2_button_Click;

            Controls.Add(form1_button);
            Controls.Add(form2_button);

        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //MessageBox.Show("aiueo");
        //    Form1 form1 = new Form1();
        //    form1.Show();
        //    this.Close();
        //}
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
