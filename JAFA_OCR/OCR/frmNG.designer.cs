namespace RELOCLUB_QR.QR
{
    partial class frmNG
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNG));
            this.dg = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNoImage = new System.Windows.Forms.Label();
            this.leadImg = new Leadtools.WinForms.RasterImageViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.dg2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).BeginInit();
            this.SuspendLayout();
            // 
            // dg
            // 
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(524, 36);
            this.dg.Margin = new System.Windows.Forms.Padding(4);
            this.dg.Name = "dg";
            this.dg.RowTemplate.Height = 21;
            this.dg.Size = new System.Drawing.Size(358, 288);
            this.dg.TabIndex = 0;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblNoImage);
            this.panel1.Controls.Add(this.leadImg);
            this.panel1.Location = new System.Drawing.Point(16, 17);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 643);
            this.panel1.TabIndex = 1;
            // 
            // lblNoImage
            // 
            this.lblNoImage.AutoSize = true;
            this.lblNoImage.Font = new System.Drawing.Font("Meiryo UI", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNoImage.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblNoImage.Location = new System.Drawing.Point(80, 270);
            this.lblNoImage.Name = "lblNoImage";
            this.lblNoImage.Size = new System.Drawing.Size(337, 44);
            this.lblNoImage.TabIndex = 1;
            this.lblNoImage.Text = "画像データがありません";
            // 
            // leadImg
            // 
            this.leadImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leadImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leadImg.Location = new System.Drawing.Point(-2, -2);
            this.leadImg.Name = "leadImg";
            this.leadImg.Size = new System.Drawing.Size(500, 638);
            this.leadImg.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(668, 626);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "削除(&D)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(778, 626);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 39);
            this.button2.TabIndex = 3;
            this.button2.Text = "終了(&E)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnPlus.Image")));
            this.btnPlus.Location = new System.Drawing.Point(521, 632);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(27, 27);
            this.btnPlus.TabIndex = 110;
            this.btnPlus.TabStop = false;
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Image = ((System.Drawing.Image)(resources.GetObject("btnMinus.Image")));
            this.btnMinus.Location = new System.Drawing.Point(547, 632);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(27, 27);
            this.btnMinus.TabIndex = 111;
            this.btnMinus.TabStop = false;
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // dg2
            // 
            this.dg2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg2.Location = new System.Drawing.Point(524, 392);
            this.dg2.Margin = new System.Windows.Forms.Padding(4);
            this.dg2.Name = "dg2";
            this.dg2.RowTemplate.Height = 21;
            this.dg2.Size = new System.Drawing.Size(358, 216);
            this.dg2.TabIndex = 112;
            this.dg2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg2_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(522, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 17);
            this.label1.TabIndex = 113;
            this.label1.Text = "【QRコード認証エラー】";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(522, 371);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 114;
            this.label2.Text = "【アンマッチ画像】";
            // 
            // frmNG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 679);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dg2);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmNG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNG";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNG_FormClosing);
            this.Load += new System.EventHandler(this.frmNG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Leadtools.WinForms.RasterImageViewer leadImg;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Label lblNoImage;
        private System.Windows.Forms.DataGridView dg2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}