namespace JAFA_DATA.Master
{
    partial class frmPastYukyuData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPastYukyuData));
            this.btnRtn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtsYear = new System.Windows.Forms.TextBox();
            this.txtsMonth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtZzan = new System.Windows.Forms.TextBox();
            this.txtTkuri = new System.Windows.Forms.TextBox();
            this.txtTfuyo = new System.Windows.Forms.TextBox();
            this.txtZyu = new System.Windows.Forms.TextBox();
            this.txtTzan = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Location = new System.Drawing.Point(352, 305);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(89, 31);
            this.btnRtn.TabIndex = 8;
            this.btnRtn.Text = "終了(&E)";
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.Location = new System.Drawing.Point(257, 305);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 31);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "取消(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(162, 305);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(89, 31);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "更新(&U)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "年";
            // 
            // txtsYear
            // 
            this.txtsYear.Location = new System.Drawing.Point(13, 10);
            this.txtsYear.MaxLength = 4;
            this.txtsYear.Name = "txtsYear";
            this.txtsYear.Size = new System.Drawing.Size(54, 27);
            this.txtsYear.TabIndex = 0;
            this.txtsYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtsYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtsMonth
            // 
            this.txtsMonth.Location = new System.Drawing.Point(89, 10);
            this.txtsMonth.MaxLength = 2;
            this.txtsMonth.Name = "txtsMonth";
            this.txtsMonth.Size = new System.Drawing.Size(39, 27);
            this.txtsMonth.TabIndex = 1;
            this.txtsMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtsMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "月";
            // 
            // txtsNum
            // 
            this.txtsNum.Location = new System.Drawing.Point(240, 10);
            this.txtsNum.MaxLength = 5;
            this.txtsNum.Name = "txtsNum";
            this.txtsNum.Size = new System.Drawing.Size(76, 27);
            this.txtsNum.TabIndex = 2;
            this.txtsNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtsNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "職員番号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "氏名：";
            // 
            // lblName
            // 
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblName.Location = new System.Drawing.Point(58, 83);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(383, 29);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "label5";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "前年初有給残日数：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "前年有休消化日数：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "当年付与日数：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(107, 227);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "繰越日数：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(56, 260);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "当年初有給残日数：";
            // 
            // txtZzan
            // 
            this.txtZzan.Location = new System.Drawing.Point(188, 125);
            this.txtZzan.Name = "txtZzan";
            this.txtZzan.Size = new System.Drawing.Size(168, 27);
            this.txtZzan.TabIndex = 1;
            this.txtZzan.Text = "20";
            this.txtZzan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtZzan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZyu_KeyPress);
            // 
            // txtTkuri
            // 
            this.txtTkuri.Location = new System.Drawing.Point(188, 224);
            this.txtTkuri.Name = "txtTkuri";
            this.txtTkuri.Size = new System.Drawing.Size(168, 27);
            this.txtTkuri.TabIndex = 4;
            this.txtTkuri.Text = "20";
            this.txtTkuri.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTkuri.TextChanged += new System.EventHandler(this.txtTkuri_TextChanged);
            this.txtTkuri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZyu_KeyPress);
            // 
            // txtTfuyo
            // 
            this.txtTfuyo.Location = new System.Drawing.Point(188, 191);
            this.txtTfuyo.Name = "txtTfuyo";
            this.txtTfuyo.Size = new System.Drawing.Size(168, 27);
            this.txtTfuyo.TabIndex = 3;
            this.txtTfuyo.Text = "20";
            this.txtTfuyo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTfuyo.TextChanged += new System.EventHandler(this.txtTfuyo_TextChanged);
            this.txtTfuyo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtZyu
            // 
            this.txtZyu.Location = new System.Drawing.Point(188, 158);
            this.txtZyu.Name = "txtZyu";
            this.txtZyu.Size = new System.Drawing.Size(168, 27);
            this.txtZyu.TabIndex = 2;
            this.txtZyu.Text = "20";
            this.txtZyu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtZyu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZyu_KeyPress);
            // 
            // txtTzan
            // 
            this.txtTzan.Location = new System.Drawing.Point(188, 257);
            this.txtTzan.Name = "txtTzan";
            this.txtTzan.Size = new System.Drawing.Size(168, 27);
            this.txtTzan.TabIndex = 5;
            this.txtTzan.Text = "20";
            this.txtTzan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTzan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZyu_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtsNum);
            this.panel1.Controls.Add(this.txtsMonth);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtsYear);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(11, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 50);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(339, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "検索(&D)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmPastYukyuData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 348);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtTzan);
            this.Controls.Add(this.txtZyu);
            this.Controls.Add(this.txtTfuyo);
            this.Controls.Add(this.txtTkuri);
            this.Controls.Add(this.txtZzan);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnUpdate);
            this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmPastYukyuData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "有休付与マスター保守";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_FormClosing);
            this.Load += new System.EventHandler(this.frm_Load);
            this.Shown += new System.EventHandler(this.frmKintaiKbn_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRtn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtsYear;
        private System.Windows.Forms.TextBox txtsMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtsNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtZzan;
        private System.Windows.Forms.TextBox txtTkuri;
        private System.Windows.Forms.TextBox txtTfuyo;
        private System.Windows.Forms.TextBox txtZyu;
        private System.Windows.Forms.TextBox txtTzan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}