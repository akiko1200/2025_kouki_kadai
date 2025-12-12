namespace _1年後期課題
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ハート.png");
            this.imageList1.Images.SetKeyName(1, "ダイヤ.png");
            this.imageList1.Images.SetKeyName(2, "太陽.png");
            this.imageList1.Images.SetKeyName(3, "音符.png");
            this.imageList1.Images.SetKeyName(4, "キラキラ.png");
            this.imageList1.Images.SetKeyName(5, "夜空.png");
            this.imageList1.Images.SetKeyName(6, "スペード.png");
            this.imageList1.Images.SetKeyName(7, "クローバー.png");
            this.imageList1.Images.SetKeyName(8, "星.png");
            this.imageList1.Images.SetKeyName(9, "肉球.png");
            this.imageList1.Images.SetKeyName(10, "王冠.png");
            this.imageList1.Images.SetKeyName(11, "雪の結晶.png");
            this.imageList1.Images.SetKeyName(12, "水滴.png");
            this.imageList1.Images.SetKeyName(13, "雷.png");
            this.imageList1.Images.SetKeyName(14, "雪だるま.png");
            this.imageList1.Images.SetKeyName(15, "桜.png");
            this.imageList1.Images.SetKeyName(16, "チューリップ.png");
            this.imageList1.Images.SetKeyName(17, "火の玉.png");
            this.imageList1.Images.SetKeyName(18, "雨.png");
            this.imageList1.Images.SetKeyName(19, "ベル.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ImageList imageList1;
    }
}

