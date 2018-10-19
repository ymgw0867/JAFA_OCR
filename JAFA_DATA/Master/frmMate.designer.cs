namespace JAFA_DATA.Master
{
    partial class frmMate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMate));
            this.dg = new System.Windows.Forms.DataGridView();
            this.btnRtn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtNyusho = new System.Windows.Forms.DateTimePicker();
            this.dtTaishoku = new System.Windows.Forms.DateTimePicker();
            this.txtFuri = new System.Windows.Forms.TextBox();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.txtFuyoTsuki = new System.Windows.Forms.TextBox();
            this.txtSzName = new System.Windows.Forms.TextBox();
            this.txtWshoTei = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSzCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbTaishoku = new System.Windows.Forms.RadioButton();
            this.rbZaiseki = new System.Windows.Forms.RadioButton();
            this.cmbYoubi = new System.Windows.Forms.ComboBox();
            this.dtChousei = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.txtsNum = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.dg.Size = new System.Drawing.Size(787, 262);
            this.dg.TabIndex = 0;
            this.dg.TabStop = false;
            this.dg.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellDoubleClick);
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Location = new System.Drawing.Point(689, 538);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(89, 31);
            this.btnRtn.TabIndex = 5;
            this.btnRtn.Text = "終了(&E)";
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.Location = new System.Drawing.Point(593, 538);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 31);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "取消(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDel.Location = new System.Drawing.Point(497, 538);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(89, 31);
            this.btnDel.TabIndex = 3;
            this.btnDel.Text = "削除(&D)";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(401, 538);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(89, 31);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "更新(&U)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCode.Location = new System.Drawing.Point(83, 4);
            this.txtCode.MaxLength = 5;
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(84, 24);
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
            this.label2.Size = new System.Drawing.Size(72, 31);
            this.label2.TabIndex = 43;
            this.label2.Text = "職員コード";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.51309F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.48691F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 329F));
            this.tableLayoutPanel1.Controls.Add(this.dtNyusho, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.dtTaishoku, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFuri, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtMemo, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtFuyoTsuki, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtSzName, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtWshoTei, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSzCode, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label13, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbYoubi, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.dtChousei, 1, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 300);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.45455F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(767, 232);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dtNyusho
            // 
            this.dtNyusho.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNyusho.Location = new System.Drawing.Point(83, 166);
            this.dtNyusho.Name = "dtNyusho";
            this.dtNyusho.ShowCheckBox = true;
            this.dtNyusho.Size = new System.Drawing.Size(153, 27);
            this.dtNyusho.TabIndex = 5;
            this.dtNyusho.ValueChanged += new System.EventHandler(this.dtNyusho_ValueChanged);
            // 
            // dtTaishoku
            // 
            this.dtTaishoku.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTaishoku.Location = new System.Drawing.Point(439, 36);
            this.dtTaishoku.Name = "dtTaishoku";
            this.dtTaishoku.ShowCheckBox = true;
            this.dtTaishoku.Size = new System.Drawing.Size(153, 27);
            this.dtTaishoku.TabIndex = 7;
            this.dtTaishoku.ValueChanged += new System.EventHandler(this.dtTaishoku_ValueChanged);
            // 
            // txtFuri
            // 
            this.txtFuri.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFuri.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFuri.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtFuri.Location = new System.Drawing.Point(83, 68);
            this.txtFuri.MaxLength = 100;
            this.txtFuri.Name = "txtFuri";
            this.txtFuri.Size = new System.Drawing.Size(264, 24);
            this.txtFuri.TabIndex = 2;
            // 
            // txtMemo
            // 
            this.txtMemo.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMemo.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMemo.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtMemo.Location = new System.Drawing.Point(439, 201);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(324, 24);
            this.txtMemo.TabIndex = 11;
            // 
            // txtFuyoTsuki
            // 
            this.txtFuyoTsuki.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFuyoTsuki.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtFuyoTsuki.Location = new System.Drawing.Point(439, 166);
            this.txtFuyoTsuki.MaxLength = 2;
            this.txtFuyoTsuki.Name = "txtFuyoTsuki";
            this.txtFuyoTsuki.Size = new System.Drawing.Size(79, 24);
            this.txtFuyoTsuki.TabIndex = 10;
            this.txtFuyoTsuki.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtSzName
            // 
            this.txtSzName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSzName.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSzName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtSzName.Location = new System.Drawing.Point(83, 132);
            this.txtSzName.Name = "txtSzName";
            this.txtSzName.Size = new System.Drawing.Size(264, 24);
            this.txtSzName.TabIndex = 4;
            // 
            // txtWshoTei
            // 
            this.txtWshoTei.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtWshoTei.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtWshoTei.Location = new System.Drawing.Point(439, 68);
            this.txtWshoTei.MaxLength = 1;
            this.txtWshoTei.Name = "txtWshoTei";
            this.txtWshoTei.Size = new System.Drawing.Size(79, 24);
            this.txtWshoTei.TabIndex = 8;
            this.txtWshoTei.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(4, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 31);
            this.label3.TabIndex = 46;
            this.label3.Text = "所属コード";
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
            this.label1.Size = new System.Drawing.Size(72, 31);
            this.label1.TabIndex = 45;
            this.label1.Text = "フリガナ";
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
            this.label6.Size = new System.Drawing.Size(72, 31);
            this.label6.TabIndex = 51;
            this.label6.Text = "氏名";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSzCode
            // 
            this.txtSzCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSzCode.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSzCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSzCode.Location = new System.Drawing.Point(83, 100);
            this.txtSzCode.Name = "txtSzCode";
            this.txtSzCode.Size = new System.Drawing.Size(264, 24);
            this.txtSzCode.TabIndex = 3;
            this.txtSzCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtName.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtName.Location = new System.Drawing.Point(83, 36);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(264, 24);
            this.txtName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label4.Location = new System.Drawing.Point(4, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 33);
            this.label4.TabIndex = 52;
            this.label4.Text = "所属名";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 34);
            this.label5.TabIndex = 53;
            this.label5.Text = "入所年月日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(4, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 33);
            this.label7.TabIndex = 54;
            this.label7.Text = "調整年月日";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(354, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 31);
            this.label9.TabIndex = 56;
            this.label9.Text = "退職年月日";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(354, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 31);
            this.label10.TabIndex = 57;
            this.label10.Text = "週所定日数";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(354, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 31);
            this.label11.TabIndex = 58;
            this.label11.Text = "在職区分";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(354, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 33);
            this.label12.TabIndex = 59;
            this.label12.Text = "週開始曜日";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(354, 163);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 34);
            this.label13.TabIndex = 60;
            this.label13.Text = "有給付与月";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(354, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 33);
            this.label8.TabIndex = 73;
            this.label8.Text = "備考";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbTaishoku);
            this.panel1.Controls.Add(this.rbZaiseki);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(439, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 25);
            this.panel1.TabIndex = 77;
            // 
            // rbTaishoku
            // 
            this.rbTaishoku.AutoSize = true;
            this.rbTaishoku.Location = new System.Drawing.Point(73, 1);
            this.rbTaishoku.Name = "rbTaishoku";
            this.rbTaishoku.Size = new System.Drawing.Size(53, 24);
            this.rbTaishoku.TabIndex = 1;
            this.rbTaishoku.TabStop = true;
            this.rbTaishoku.Text = "退職";
            this.rbTaishoku.UseVisualStyleBackColor = true;
            // 
            // rbZaiseki
            // 
            this.rbZaiseki.AutoSize = true;
            this.rbZaiseki.Location = new System.Drawing.Point(0, 1);
            this.rbZaiseki.Name = "rbZaiseki";
            this.rbZaiseki.Size = new System.Drawing.Size(53, 24);
            this.rbZaiseki.TabIndex = 0;
            this.rbZaiseki.TabStop = true;
            this.rbZaiseki.Text = "在職";
            this.rbZaiseki.UseVisualStyleBackColor = true;
            // 
            // cmbYoubi
            // 
            this.cmbYoubi.FormattingEnabled = true;
            this.cmbYoubi.Items.AddRange(new object[] {
            "日曜",
            "月曜",
            "火曜",
            "水曜",
            "木曜",
            "金曜",
            "土曜"});
            this.cmbYoubi.Location = new System.Drawing.Point(439, 132);
            this.cmbYoubi.Name = "cmbYoubi";
            this.cmbYoubi.Size = new System.Drawing.Size(153, 28);
            this.cmbYoubi.TabIndex = 9;
            // 
            // dtChousei
            // 
            this.dtChousei.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtChousei.Location = new System.Drawing.Point(83, 201);
            this.dtChousei.Name = "dtChousei";
            this.dtChousei.ShowCheckBox = true;
            this.dtChousei.Size = new System.Drawing.Size(153, 27);
            this.dtChousei.TabIndex = 6;
            this.dtChousei.ValueChanged += new System.EventHandler(this.dtChousei_ValueChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(285, 538);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "CSV出力(&F)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtsNum
            // 
            this.txtsNum.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtsNum.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtsNum.Location = new System.Drawing.Point(641, 268);
            this.txtsNum.MaxLength = 6;
            this.txtsNum.Name = "txtsNum";
            this.txtsNum.Size = new System.Drawing.Size(66, 24);
            this.txtsNum.TabIndex = 7;
            this.txtsNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtsNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(555, 264);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 31);
            this.label14.TabIndex = 44;
            this.label14.Text = "職員コード：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(713, 268);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 26);
            this.button2.TabIndex = 45;
            this.button2.Text = "検索";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmMate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 578);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtsNum);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button1);
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
            this.Name = "frmMate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "メイトマスター保守";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_FormClosing);
            this.Load += new System.EventHandler(this.frm_Load);
            this.Shown += new System.EventHandler(this.frmKintaiKbn_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSzCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtNyusho;
        private System.Windows.Forms.DateTimePicker dtTaishoku;
        private System.Windows.Forms.TextBox txtFuri;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TextBox txtFuyoTsuki;
        private System.Windows.Forms.TextBox txtSzName;
        private System.Windows.Forms.TextBox txtWshoTei;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbTaishoku;
        private System.Windows.Forms.RadioButton rbZaiseki;
        private System.Windows.Forms.ComboBox cmbYoubi;
        private System.Windows.Forms.DateTimePicker dtChousei;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtsNum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button2;
    }
}