namespace JAFA_DATA
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
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(239, 365);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "終了(&E)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(53, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(364, 43);
            this.button2.TabIndex = 1;
            this.button2.Text = "勤怠データ作成(&D)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(53, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(364, 43);
            this.button3.TabIndex = 2;
            this.button3.Text = "ＪＡメイトOCRデータ作成(&C)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(53, 120);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(364, 43);
            this.button4.TabIndex = 3;
            this.button4.Text = "メイト出勤簿処理状況(&M)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(53, 169);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(364, 43);
            this.button8.TabIndex = 4;
            this.button8.Text = "過去勤怠データビューワー(&W)";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(53, 267);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(364, 43);
            this.button10.TabIndex = 6;
            this.button10.Text = "マスターメンテナンス(&T)";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(53, 316);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(364, 43);
            this.button11.TabIndex = 7;
            this.button11.Text = "環境設定(&F)";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(53, 218);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(364, 43);
            this.button12.TabIndex = 5;
            this.button12.Text = "勤怠集計データメニュー(&R)";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(53, 365);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(180, 43);
            this.button5.TabIndex = 8;
            this.button5.Text = "NG画像確認(&U)";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 431);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Meiryo UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出勤簿データ作成メニュー";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button5;
    }
}

