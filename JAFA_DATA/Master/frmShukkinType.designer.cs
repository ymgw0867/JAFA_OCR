namespace JAFA_DATA.Master
{
    partial class frmShukkinType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShukkinType));
            this.dg = new System.Windows.Forms.DataGridView();
            this.btnRtn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.cmbKbn = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg
            // 
            this.dg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Dock = System.Windows.Forms.DockStyle.Top;
            this.dg.Location = new System.Drawing.Point(0, 0);
            this.dg.Name = "dg";
            this.dg.RowTemplate.Height = 21;
            this.dg.Size = new System.Drawing.Size(787, 341);
            this.dg.TabIndex = 0;
            this.dg.TabStop = false;
            this.dg.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellDoubleClick);
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Location = new System.Drawing.Point(686, 511);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(89, 31);
            this.btnRtn.TabIndex = 4;
            this.btnRtn.Text = "終了(&E)";
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.Location = new System.Drawing.Point(591, 511);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 31);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "取消(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDel.Location = new System.Drawing.Point(496, 511);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(89, 31);
            this.btnDel.TabIndex = 2;
            this.btnDel.Text = "削除(&D)";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(401, 511);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(89, 31);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "更新(&U)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCode.Location = new System.Drawing.Point(154, 4);
            this.txtCode.MaxLength = 2;
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(38, 24);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(4, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 31);
            this.label2.TabIndex = 43;
            this.label2.Text = "コード";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.58224F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.41776F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtMemo, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbKbn, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 365);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(767, 130);
            this.tableLayoutPanel1.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(4, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 30);
            this.label3.TabIndex = 46;
            this.label3.Text = "備考";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(4, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 33);
            this.label1.TabIndex = 45;
            this.label1.Text = "実働区分";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(4, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 31);
            this.label6.TabIndex = 51;
            this.label6.Text = "名称";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMemo
            // 
            this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMemo.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMemo.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtMemo.Location = new System.Drawing.Point(154, 102);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(609, 24);
            this.txtMemo.TabIndex = 3;
            // 
            // cmbKbn
            // 
            this.cmbKbn.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbKbn.FormattingEnabled = true;
            this.cmbKbn.Items.AddRange(new object[] {
            "実働なし",
            "実働あり"});
            this.cmbKbn.Location = new System.Drawing.Point(154, 68);
            this.cmbKbn.Name = "cmbKbn";
            this.cmbKbn.Size = new System.Drawing.Size(110, 25);
            this.cmbKbn.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtName.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtName.Location = new System.Drawing.Point(154, 36);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(609, 24);
            this.txtName.TabIndex = 1;
            // 
            // frmShukkinType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 545);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dg);
            this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmShukkinType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出勤区分マスター保守";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_FormClosing);
            this.Load += new System.EventHandler(this.frm_Load);
            this.Shown += new System.EventHandler(this.frmKintaiKbn_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btnRtn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbKbn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TextBox txtName;
    }
}