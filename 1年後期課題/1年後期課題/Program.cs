using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1年後期課題
{
    internal static class Program
    {
        public static ApplicationContext main_form;
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            
            main_form = new ApplicationContext();
            main_form.MainForm = new Form0();
            Application.Run(main_form);
        }

        /// <summary>
        /// Form1に切り替える処理
        /// </summary>
        public static void Display_form1()
        {
            main_form.MainForm = new Form1();
            main_form.MainForm.Show();
        }

        /// <summary>
        /// Form2に切り替える処理
        /// </summary>
        public static void Display_form2()
        {
            main_form.MainForm = new Form2();
            main_form.MainForm.Show();
        }

        
    }
}
