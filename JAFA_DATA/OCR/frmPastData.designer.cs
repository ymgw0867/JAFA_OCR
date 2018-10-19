namespace JAFA_DATA.OCR
{
    partial class frmPastData
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPastData));
            this.lblName = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnRtn = new System.Windows.Forms.Button();
            this.lblNoImage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.leadImg = new Leadtools.WinForms.RasterImageViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKoutsuuhi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNittou = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtShukuhakuhi = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSyoubi = new System.Windows.Forms.Label();
            this.lblWdays = new System.Windows.Forms.Label();
            this.dgvJa = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.lblSzCode = new System.Windows.Forms.Label();
            this.lblSzName = new System.Windows.Forms.Label();
            this.dGV = new JAFA_DATA.DataGridViewEx();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.Window;
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblName.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblName.Location = new System.Drawing.Point(703, 47);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(176, 34);
            this.lblName.TabIndex = 58;
            this.lblName.Text = "山田　太郎";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNo
            // 
            this.txtNo.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNo.Location = new System.Drawing.Point(611, 47);
            this.txtNo.MaxLength = 5;
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(91, 34);
            this.txtNo.TabIndex = 3;
            this.txtNo.Text = "12345";
            this.txtNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNo.WordWrap = false;
            this.txtNo.TextChanged += new System.EventHandler(this.txtNo_TextChanged);
            this.txtNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(779, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 24);
            this.label3.TabIndex = 56;
            this.label3.Text = "月";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(702, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 24);
            this.label2.TabIndex = 55;
            this.label2.Text = "年";
            // 
            // txtMonth
            // 
            this.txtMonth.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMonth.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMonth.Location = new System.Drawing.Point(734, 10);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(42, 34);
            this.txtMonth.TabIndex = 2;
            this.txtMonth.Text = "12";
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.WordWrap = false;
            this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // txtYear
            // 
            this.txtYear.BackColor = System.Drawing.SystemColors.Window;
            this.txtYear.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtYear.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtYear.Location = new System.Drawing.Point(660, 10);
            this.txtYear.MaxLength = 2;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(42, 34);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "27";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.WordWrap = false;
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // btnMinus
            // 
            this.btnMinus.Image = ((System.Drawing.Image)(resources.GetObject("btnMinus.Image")));
            this.btnMinus.Location = new System.Drawing.Point(40, 813);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(27, 27);
            this.btnMinus.TabIndex = 109;
            this.btnMinus.TabStop = false;
            this.toolTip1.SetToolTip(this.btnMinus, "画像を縮小表示します");
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnPlus.Image")));
            this.btnPlus.Location = new System.Drawing.Point(14, 813);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(27, 27);
            this.btnPlus.TabIndex = 108;
            this.btnPlus.TabStop = false;
            this.toolTip1.SetToolTip(this.btnPlus, "画像を拡大表示します");
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Image = ((System.Drawing.Image)(resources.GetObject("btnRtn.Image")));
            this.btnRtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRtn.Location = new System.Drawing.Point(1085, 809);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(105, 33);
            this.btnRtn.TabIndex = 8;
            this.btnRtn.Text = "終了(&E)";
            this.toolTip1.SetToolTip(this.btnRtn, "プログラムを終了しメニューへ戻ります");
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // lblNoImage
            // 
            this.lblNoImage.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNoImage.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblNoImage.Location = new System.Drawing.Point(135, 336);
            this.lblNoImage.Name = "lblNoImage";
            this.lblNoImage.Size = new System.Drawing.Size(322, 42);
            this.lblNoImage.TabIndex = 119;
            this.lblNoImage.Text = "画像はありません";
            this.lblNoImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(591, 799);
            this.pictureBox1.TabIndex = 120;
            this.pictureBox1.TabStop = false;
            // 
            // leadImg
            // 
            this.leadImg.Location = new System.Drawing.Point(12, 9);
            this.leadImg.Name = "leadImg";
            this.leadImg.Size = new System.Drawing.Size(589, 798);
            this.leadImg.TabIndex = 121;
            this.leadImg.MouseLeave += new System.EventHandler(this.leadImg_MouseLeave);
            this.leadImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.leadImg_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(611, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 266;
            this.label1.Text = "平成";
            // 
            // txtKoutsuuhi
            // 
            this.txtKoutsuuhi.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKoutsuuhi.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtKoutsuuhi.Location = new System.Drawing.Point(674, 766);
            this.txtKoutsuuhi.MaxLength = 5;
            this.txtKoutsuuhi.Name = "txtKoutsuuhi";
            this.txtKoutsuuhi.Size = new System.Drawing.Size(71, 24);
            this.txtKoutsuuhi.TabIndex = 268;
            this.txtKoutsuuhi.Text = "50000";
            this.txtKoutsuuhi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(615, 771);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 269;
            this.label4.Text = "交通費：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(761, 771);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 271;
            this.label5.Text = "日当：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNittou
            // 
            this.txtNittou.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNittou.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNittou.Location = new System.Drawing.Point(807, 766);
            this.txtNittou.MaxLength = 5;
            this.txtNittou.Name = "txtNittou";
            this.txtNittou.Size = new System.Drawing.Size(72, 24);
            this.txtNittou.TabIndex = 270;
            this.txtNittou.Text = "98765";
            this.txtNittou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(899, 771);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 273;
            this.label6.Text = "宿泊費：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtShukuhakuhi
            // 
            this.txtShukuhakuhi.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtShukuhakuhi.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtShukuhakuhi.Location = new System.Drawing.Point(957, 766);
            this.txtShukuhakuhi.MaxLength = 5;
            this.txtShukuhakuhi.Name = "txtShukuhakuhi";
            this.txtShukuhakuhi.Size = new System.Drawing.Size(72, 24);
            this.txtShukuhakuhi.TabIndex = 272;
            this.txtShukuhakuhi.Text = "34500";
            this.txtShukuhakuhi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(939, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 15);
            this.label7.TabIndex = 275;
            this.label7.Text = "週開始曜日：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(1059, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 15);
            this.label8.TabIndex = 276;
            this.label8.Text = "週所定労働日数：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSyoubi
            // 
            this.lblSyoubi.BackColor = System.Drawing.SystemColors.Window;
            this.lblSyoubi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSyoubi.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSyoubi.Location = new System.Drawing.Point(1021, 19);
            this.lblSyoubi.Name = "lblSyoubi";
            this.lblSyoubi.Size = new System.Drawing.Size(26, 25);
            this.lblSyoubi.TabIndex = 277;
            this.lblSyoubi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWdays
            // 
            this.lblWdays.BackColor = System.Drawing.SystemColors.Window;
            this.lblWdays.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWdays.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblWdays.Location = new System.Drawing.Point(1165, 19);
            this.lblWdays.Name = "lblWdays";
            this.lblWdays.Size = new System.Drawing.Size(25, 25);
            this.lblWdays.TabIndex = 278;
            this.lblWdays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvJa
            // 
            this.dgvJa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJa.Location = new System.Drawing.Point(1033, 88);
            this.dgvJa.Name = "dgvJa";
            this.dgvJa.RowTemplate.Height = 21;
            this.dgvJa.Size = new System.Drawing.Size(157, 672);
            this.dgvJa.TabIndex = 279;
            this.dgvJa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJa_CellClick);
            this.dgvJa.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvJa_EditingControlShowing);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(974, 809);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 33);
            this.button1.TabIndex = 280;
            this.button1.Text = "印刷(&P)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblSzCode
            // 
            this.lblSzCode.BackColor = System.Drawing.SystemColors.Window;
            this.lblSzCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSzCode.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSzCode.Location = new System.Drawing.Point(883, 47);
            this.lblSzCode.Name = "lblSzCode";
            this.lblSzCode.Size = new System.Drawing.Size(79, 34);
            this.lblSzCode.TabIndex = 281;
            this.lblSzCode.Text = "12345";
            this.lblSzCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSzName
            // 
            this.lblSzName.BackColor = System.Drawing.SystemColors.Window;
            this.lblSzName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSzName.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSzName.Location = new System.Drawing.Point(964, 47);
            this.lblSzName.Name = "lblSzName";
            this.lblSzName.Size = new System.Drawing.Size(226, 34);
            this.lblSzName.TabIndex = 282;
            this.lblSzName.Text = "ﾔﾏﾀﾞﾀﾛｳ";
            this.lblSzName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dGV
            // 
            this.dGV.Location = new System.Drawing.Point(609, 88);
            this.dGV.Name = "dGV";
            this.dGV.Size = new System.Drawing.Size(423, 672);
            this.dGV.TabIndex = 0;
            this.dGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGV_CellClick);
            this.dGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGV_CellContentClick);
            this.dGV.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter_1);
            this.dGV.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dGV.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dGV.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // frmPastData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 848);
            this.Controls.Add(this.lblSzName);
            this.Controls.Add(this.lblSzCode);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvJa);
            this.Controls.Add(this.lblWdays);
            this.Controls.Add(this.lblSyoubi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtShukuhakuhi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNittou);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtKoutsuuhi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.dGV);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.lblNoImage);
            this.Controls.Add(this.leadImg);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPastData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "勤怠申請書データ登録";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCorrect_FormClosing);
            this.Load += new System.EventHandler(this.frmCorrect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Button btnRtn;
        private DataGridViewEx dGV;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblNoImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Leadtools.WinForms.RasterImageViewer leadImg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKoutsuuhi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNittou;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtShukuhakuhi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblSyoubi;
        private System.Windows.Forms.Label lblWdays;
        private System.Windows.Forms.DataGridView dgvJa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblSzCode;
        private System.Windows.Forms.Label lblSzName;
    }
}