namespace JAFA_DATA.workData
{
    partial class frmWeek40hOverViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWeek40hOverViewer));
            this.label2 = new System.Windows.Forms.Label();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.btnRtn = new System.Windows.Forms.Button();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtShainNum = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dg2 = new System.Windows.Forms.DataGridView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(203, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 27);
            this.label2.TabIndex = 5;
            this.label2.Text = "社員番号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dg1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.dg1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Location = new System.Drawing.Point(16, 79);
            this.dg1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.RowTemplate.Height = 21;
            this.dg1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg1.Size = new System.Drawing.Size(978, 164);
            this.dg1.TabIndex = 12;
            this.dg1.TabStop = false;
            this.dg1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellClick);
            // 
            // btnRtn
            // 
            this.btnRtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRtn.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Location = new System.Drawing.Point(875, 723);
            this.btnRtn.Margin = new System.Windows.Forms.Padding(4);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(119, 35);
            this.btnRtn.TabIndex = 0;
            this.btnRtn.Text = "終了(&E)";
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtYear.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtYear.Location = new System.Drawing.Point(21, 10);
            this.txtYear.Margin = new System.Windows.Forms.Padding(4);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(56, 27);
            this.txtYear.TabIndex = 0;
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.Enter += new System.EventHandler(this.txtYear_Enter);
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(77, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 27);
            this.label3.TabIndex = 12;
            this.label3.Text = "年";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMonth
            // 
            this.txtMonth.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMonth.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMonth.Location = new System.Drawing.Point(109, 10);
            this.txtMonth.Margin = new System.Windows.Forms.Padding(4);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(48, 27);
            this.txtMonth.TabIndex = 1;
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.Enter += new System.EventHandler(this.txtYear_Enter);
            this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(157, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 27);
            this.label4.TabIndex = 14;
            this.label4.Text = "月";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSel
            // 
            this.btnSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSel.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSel.Location = new System.Drawing.Point(881, 3);
            this.btnSel.Margin = new System.Windows.Forms.Padding(4);
            this.btnSel.Name = "btnSel";
            this.btnSel.Size = new System.Drawing.Size(89, 38);
            this.btnSel.TabIndex = 4;
            this.btnSel.Text = "検索(&P)";
            this.btnSel.UseVisualStyleBackColor = true;
            this.btnSel.Click += new System.EventHandler(this.btnSel_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.txtShainNum);
            this.panel1.Controls.Add(this.btnSel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtMonth);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtYear);
            this.panel1.Location = new System.Drawing.Point(16, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 48);
            this.panel1.TabIndex = 27;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "全て",
            "40時間を超えている週のみ"});
            this.comboBox1.Location = new System.Drawing.Point(418, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(203, 25);
            this.comboBox1.TabIndex = 3;
            // 
            // txtShainNum
            // 
            this.txtShainNum.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtShainNum.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtShainNum.Location = new System.Drawing.Point(300, 10);
            this.txtShainNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtShainNum.MaxLength = 5;
            this.txtShainNum.Name = "txtShainNum";
            this.txtShainNum.Size = new System.Drawing.Size(93, 27);
            this.txtShainNum.TabIndex = 2;
            this.txtShainNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShainNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 728);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 17);
            this.label10.TabIndex = 28;
            this.label10.Text = "label10";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(511, 723);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "週単位CSV出力(&C)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dg2
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.dg2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dg2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg2.Location = new System.Drawing.Point(16, 263);
            this.dg2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dg2.Name = "dg2";
            this.dg2.ReadOnly = true;
            this.dg2.RowTemplate.Height = 21;
            this.dg2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg2.Size = new System.Drawing.Size(978, 452);
            this.dg2.TabIndex = 29;
            this.dg2.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.linkLabel1.Location = new System.Drawing.Point(88, 246);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(117, 15);
            this.linkLabel1.TabIndex = 30;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "全ての週の明細を表示";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(693, 723);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 35);
            this.button2.TabIndex = 2;
            this.button2.Text = "日別明細CSV出力(&C)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(15, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 32;
            this.label1.Text = "【週単位】";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(15, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "【日別明細】";
            // 
            // frmWeek40hOverViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 763);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dg2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmWeek40hOverViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.prePrint_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Button btnRtn;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtShainNum;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dg2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}

