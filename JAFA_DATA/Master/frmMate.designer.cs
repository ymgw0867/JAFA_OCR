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
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rbTaishoku = new System.Windows.Forms.RadioButton();
            this.rbZaiseki = new System.Windows.Forms.RadioButton();
            this.dtChousei = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.txtsNum = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbShain = new System.Windows.Forms.ComboBox();
            this.cmbFarm = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbShortWork = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg
            // 
            this.dg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(7, 8);
            this.dg.Name = "dg";
            this.dg.RowTemplate.Height = 21;
            this.dg.Size = new System.Drawing.Size(798, 261);
            this.dg.TabIndex = 0;
            this.dg.TabStop = false;
            this.dg.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellDoubleClick);
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Location = new System.Drawing.Point(710, 574);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(89, 31);
            this.btnRtn.TabIndex = 19;
            this.btnRtn.Text = "終了(&E)";
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.Location = new System.Drawing.Point(614, 574);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 31);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "取消(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDel.Location = new System.Drawing.Point(518, 574);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(89, 31);
            this.btnDel.TabIndex = 17;
            this.btnDel.Text = "削除(&D)";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(422, 574);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(89, 31);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "更新(&U)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCode.Location = new System.Drawing.Point(95, 317);
            this.txtCode.MaxLength = 5;
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(153, 24);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(4, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 31);
            this.label2.TabIndex = 43;
            this.label2.Text = "職員コード";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtNyusho
            // 
            this.dtNyusho.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNyusho.Location = new System.Drawing.Point(95, 466);
            this.dtNyusho.Name = "dtNyusho";
            this.dtNyusho.ShowCheckBox = true;
            this.dtNyusho.Size = new System.Drawing.Size(153, 27);
            this.dtNyusho.TabIndex = 5;
            this.dtNyusho.ValueChanged += new System.EventHandler(this.dtNyusho_ValueChanged);
            // 
            // dtTaishoku
            // 
            this.dtTaishoku.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTaishoku.Location = new System.Drawing.Point(95, 532);
            this.dtTaishoku.Name = "dtTaishoku";
            this.dtTaishoku.ShowCheckBox = true;
            this.dtTaishoku.Size = new System.Drawing.Size(153, 27);
            this.dtTaishoku.TabIndex = 7;
            this.dtTaishoku.ValueChanged += new System.EventHandler(this.dtTaishoku_ValueChanged);
            // 
            // txtFuri
            // 
            this.txtFuri.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFuri.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtFuri.Location = new System.Drawing.Point(95, 375);
            this.txtFuri.MaxLength = 100;
            this.txtFuri.Name = "txtFuri";
            this.txtFuri.Size = new System.Drawing.Size(264, 24);
            this.txtFuri.TabIndex = 2;
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMemo.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtMemo.Location = new System.Drawing.Point(494, 532);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(305, 24);
            this.txtMemo.TabIndex = 14;
            // 
            // txtFuyoTsuki
            // 
            this.txtFuyoTsuki.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFuyoTsuki.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtFuyoTsuki.Location = new System.Drawing.Point(494, 404);
            this.txtFuyoTsuki.MaxLength = 2;
            this.txtFuyoTsuki.Name = "txtFuyoTsuki";
            this.txtFuyoTsuki.Size = new System.Drawing.Size(126, 24);
            this.txtFuyoTsuki.TabIndex = 10;
            this.txtFuyoTsuki.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtSzName
            // 
            this.txtSzName.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSzName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtSzName.Location = new System.Drawing.Point(95, 433);
            this.txtSzName.Name = "txtSzName";
            this.txtSzName.Size = new System.Drawing.Size(264, 24);
            this.txtSzName.TabIndex = 4;
            // 
            // txtWshoTei
            // 
            this.txtWshoTei.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtWshoTei.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtWshoTei.Location = new System.Drawing.Point(494, 375);
            this.txtWshoTei.MaxLength = 1;
            this.txtWshoTei.Name = "txtWshoTei";
            this.txtWshoTei.Size = new System.Drawing.Size(126, 24);
            this.txtWshoTei.TabIndex = 9;
            this.txtWshoTei.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(4, 400);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 31);
            this.label3.TabIndex = 46;
            this.label3.Text = "所属コード";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(4, 371);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 31);
            this.label1.TabIndex = 45;
            this.label1.Text = "フリガナ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(4, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 31);
            this.label6.TabIndex = 51;
            this.label6.Text = "氏名";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSzCode
            // 
            this.txtSzCode.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSzCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSzCode.Location = new System.Drawing.Point(95, 404);
            this.txtSzCode.Name = "txtSzCode";
            this.txtSzCode.Size = new System.Drawing.Size(153, 24);
            this.txtSzCode.TabIndex = 3;
            this.txtSzCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtName.Location = new System.Drawing.Point(95, 346);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(264, 24);
            this.txtName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label4.Location = new System.Drawing.Point(4, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 52;
            this.label4.Text = "所属名";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 471);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 25);
            this.label5.TabIndex = 53;
            this.label5.Text = "入所年月日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(4, 504);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 20);
            this.label7.TabIndex = 54;
            this.label7.Text = "調整年月日";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(4, 535);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 20);
            this.label9.TabIndex = 56;
            this.label9.Text = "退職年月日";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(388, 378);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.TabIndex = 57;
            this.label10.Text = "週所定労働日数";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(388, 346);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 20);
            this.label11.TabIndex = 58;
            this.label11.Text = "在職区分";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(388, 407);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 20);
            this.label13.TabIndex = 60;
            this.label13.Text = "有給付与月";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(388, 535);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 73;
            this.label8.Text = "備考";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbTaishoku
            // 
            this.rbTaishoku.AutoSize = true;
            this.rbTaishoku.Location = new System.Drawing.Point(65, 4);
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
            this.rbZaiseki.Location = new System.Drawing.Point(6, 4);
            this.rbZaiseki.Name = "rbZaiseki";
            this.rbZaiseki.Size = new System.Drawing.Size(53, 24);
            this.rbZaiseki.TabIndex = 0;
            this.rbZaiseki.TabStop = true;
            this.rbZaiseki.Text = "在職";
            this.rbZaiseki.UseVisualStyleBackColor = true;
            // 
            // dtChousei
            // 
            this.dtChousei.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtChousei.Location = new System.Drawing.Point(95, 499);
            this.dtChousei.Name = "dtChousei";
            this.dtChousei.ShowCheckBox = true;
            this.dtChousei.Size = new System.Drawing.Size(153, 27);
            this.dtChousei.TabIndex = 6;
            this.dtChousei.ValueChanged += new System.EventHandler(this.dtChousei_ValueChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(306, 574);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 31);
            this.button1.TabIndex = 15;
            this.button1.Text = "CSV出力(&F)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtsNum
            // 
            this.txtsNum.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtsNum.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtsNum.Location = new System.Drawing.Point(658, 3);
            this.txtsNum.MaxLength = 6;
            this.txtsNum.Name = "txtsNum";
            this.txtsNum.Size = new System.Drawing.Size(66, 24);
            this.txtsNum.TabIndex = 0;
            this.txtsNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtsNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(572, -1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 31);
            this.label14.TabIndex = 44;
            this.label14.Text = "職員コード：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(730, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "検索";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbZaiseki);
            this.panel1.Controls.Add(this.rbTaishoku);
            this.panel1.Location = new System.Drawing.Point(494, 339);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(126, 31);
            this.panel1.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(388, 471);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 20);
            this.label12.TabIndex = 75;
            this.label12.Text = "社員区分";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbShain
            // 
            this.cmbShain.FormattingEnabled = true;
            this.cmbShain.Items.AddRange(new object[] {
            "役員",
            "正社員",
            "臨時社員",
            "外国人技能実習生"});
            this.cmbShain.Location = new System.Drawing.Point(494, 466);
            this.cmbShain.Name = "cmbShain";
            this.cmbShain.Size = new System.Drawing.Size(126, 28);
            this.cmbShain.TabIndex = 12;
            // 
            // cmbFarm
            // 
            this.cmbFarm.FormattingEnabled = true;
            this.cmbFarm.Items.AddRange(new object[] {
            "従事せず",
            "従事している"});
            this.cmbFarm.Location = new System.Drawing.Point(494, 499);
            this.cmbFarm.Name = "cmbFarm";
            this.cmbFarm.Size = new System.Drawing.Size(126, 28);
            this.cmbFarm.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(388, 502);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 20);
            this.label15.TabIndex = 77;
            this.label15.Text = "農業従事";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbShortWork
            // 
            this.cmbShortWork.FormattingEnabled = true;
            this.cmbShortWork.Items.AddRange(new object[] {
            "非該当者",
            "該当者"});
            this.cmbShortWork.Location = new System.Drawing.Point(494, 433);
            this.cmbShortWork.Name = "cmbShortWork";
            this.cmbShortWork.Size = new System.Drawing.Size(126, 28);
            this.cmbShortWork.TabIndex = 11;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(388, 437);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 20);
            this.label16.TabIndex = 79;
            this.label16.Text = "短時間勤務";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtsNum);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Location = new System.Drawing.Point(7, 276);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 32);
            this.panel2.TabIndex = 81;
            // 
            // frmMate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 612);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtFuri);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmbShortWork);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbFarm);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbShain);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.txtFuyoTsuki);
            this.Controls.Add(this.dtNyusho);
            this.Controls.Add(this.txtWshoTei);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtTaishoku);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtSzName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtSzCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtChousei);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmMate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "社員マスター保守";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_FormClosing);
            this.Load += new System.EventHandler(this.frm_Load);
            this.Shown += new System.EventHandler(this.frmKintaiKbn_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtNyusho;
        private System.Windows.Forms.DateTimePicker dtTaishoku;
        private System.Windows.Forms.TextBox txtFuri;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TextBox txtFuyoTsuki;
        private System.Windows.Forms.TextBox txtSzName;
        private System.Windows.Forms.TextBox txtWshoTei;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbTaishoku;
        private System.Windows.Forms.RadioButton rbZaiseki;
        private System.Windows.Forms.DateTimePicker dtChousei;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtsNum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbShain;
        private System.Windows.Forms.ComboBox cmbFarm;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbShortWork;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel2;
    }
}