namespace JAFA_DATA.OCR
{
    partial class frmCorrectMonth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorrectMonth));
            this.lblName = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBefore = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnDataMake = new System.Windows.Forms.Button();
            this.btnRtn = new System.Windows.Forms.Button();
            this.btnErrCheck = new System.Windows.Forms.Button();
            this.lblNoImage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblErrMsg = new System.Windows.Forms.Label();
            this.lblFuri = new System.Windows.Forms.Label();
            this.txtKoutsuuhi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNittou = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtShukuhakuhi = new System.Windows.Forms.TextBox();
            this.lblWarn = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblWdays = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtJmpCode = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.lblShainKbnName = new System.Windows.Forms.Label();
            this.lblShainkbn = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dGV = new JAFA_DATA.DataGridViewEx();
            this.lblNougyou = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.Window;
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblName.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblName.Location = new System.Drawing.Point(788, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(155, 24);
            this.lblName.TabIndex = 58;
            this.lblName.Text = "山田　太郎";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNo
            // 
            this.txtNo.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNo.Location = new System.Drawing.Point(735, 8);
            this.txtNo.MaxLength = 5;
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(52, 24);
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
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(708, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 56;
            this.label3.Text = "月";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(657, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 15);
            this.label2.TabIndex = 55;
            this.label2.Text = "年";
            // 
            // txtMonth
            // 
            this.txtMonth.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMonth.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMonth.Location = new System.Drawing.Point(680, 8);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(28, 24);
            this.txtMonth.TabIndex = 2;
            this.txtMonth.Text = "12";
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.WordWrap = false;
            this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // txtYear
            // 
            this.txtYear.BackColor = System.Drawing.SystemColors.Window;
            this.txtYear.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtYear.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtYear.Location = new System.Drawing.Point(611, 8);
            this.txtYear.MaxLength = 2;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(46, 24);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "2018";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.WordWrap = false;
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPage.Location = new System.Drawing.Point(1096, 8);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(107, 23);
            this.lblPage.TabIndex = 53;
            this.lblPage.Text = "1000/1000";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(269, 30);
            this.hScrollBar1.TabIndex = 65;
            this.toolTip1.SetToolTip(this.hScrollBar1, "出勤簿を移動します");
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("メイリオ", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnEnd.Image = ((System.Drawing.Image)(resources.GetObject("btnEnd.Image")));
            this.btnEnd.Location = new System.Drawing.Point(303, 656);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(26, 33);
            this.btnEnd.TabIndex = 74;
            this.btnEnd.TabStop = false;
            this.toolTip1.SetToolTip(this.btnEnd, "最後尾の出勤簿データへ移動します");
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("メイリオ", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(278, 656);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(26, 33);
            this.btnNext.TabIndex = 73;
            this.btnNext.TabStop = false;
            this.toolTip1.SetToolTip(this.btnNext, "次の出勤簿データへ移動します");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBefore
            // 
            this.btnBefore.Font = new System.Drawing.Font("メイリオ", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnBefore.Image = ((System.Drawing.Image)(resources.GetObject("btnBefore.Image")));
            this.btnBefore.Location = new System.Drawing.Point(253, 656);
            this.btnBefore.Name = "btnBefore";
            this.btnBefore.Size = new System.Drawing.Size(26, 33);
            this.btnBefore.TabIndex = 72;
            this.btnBefore.TabStop = false;
            this.toolTip1.SetToolTip(this.btnBefore, "前の出勤簿データへ移動します");
            this.btnBefore.UseVisualStyleBackColor = true;
            this.btnBefore.Click += new System.EventHandler(this.btnBefore_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Font = new System.Drawing.Font("メイリオ", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.Location = new System.Drawing.Point(228, 656);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(26, 33);
            this.btnFirst.TabIndex = 71;
            this.btnFirst.TabStop = false;
            this.toolTip1.SetToolTip(this.btnFirst, "先頭の出勤簿データへ移動します");
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(943, 656);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(131, 33);
            this.btnDel.TabIndex = 7;
            this.btnDel.Text = "出勤簿削除(&D)";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnDel, "表示中の出勤簿データを削除します");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnDataMake
            // 
            this.btnDataMake.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDataMake.Image = ((System.Drawing.Image)(resources.GetObject("btnDataMake.Image")));
            this.btnDataMake.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDataMake.Location = new System.Drawing.Point(1113, 36);
            this.btnDataMake.Name = "btnDataMake";
            this.btnDataMake.Size = new System.Drawing.Size(93, 54);
            this.btnDataMake.TabIndex = 6;
            this.btnDataMake.Text = "汎用データ作成(&M)";
            this.btnDataMake.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnDataMake, "エラーチェックの後、勤怠データを作成します");
            this.btnDataMake.UseVisualStyleBackColor = true;
            this.btnDataMake.Click += new System.EventHandler(this.btnDataMake_Click);
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Image = ((System.Drawing.Image)(resources.GetObject("btnRtn.Image")));
            this.btnRtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRtn.Location = new System.Drawing.Point(1076, 656);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(131, 33);
            this.btnRtn.TabIndex = 8;
            this.btnRtn.Text = "終了(&E)";
            this.toolTip1.SetToolTip(this.btnRtn, "プログラムを終了しメニューへ戻ります");
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // btnErrCheck
            // 
            this.btnErrCheck.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnErrCheck.Image = ((System.Drawing.Image)(resources.GetObject("btnErrCheck.Image")));
            this.btnErrCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnErrCheck.Location = new System.Drawing.Point(610, 656);
            this.btnErrCheck.Name = "btnErrCheck";
            this.btnErrCheck.Size = new System.Drawing.Size(131, 33);
            this.btnErrCheck.TabIndex = 5;
            this.btnErrCheck.Text = "エラーチェック(&C)";
            this.btnErrCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnErrCheck, "エラーチェックを実行します");
            this.btnErrCheck.UseVisualStyleBackColor = true;
            this.btnErrCheck.Click += new System.EventHandler(this.btnErrCheck_Click);
            // 
            // lblNoImage
            // 
            this.lblNoImage.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNoImage.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblNoImage.Location = new System.Drawing.Point(132, 273);
            this.lblNoImage.Name = "lblNoImage";
            this.lblNoImage.Size = new System.Drawing.Size(322, 42);
            this.lblNoImage.TabIndex = 119;
            this.lblNoImage.Text = "画像はありません";
            this.lblNoImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(584, 592);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 120;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblErrMsg);
            this.panel1.Location = new System.Drawing.Point(609, 617);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 33);
            this.panel1.TabIndex = 162;
            // 
            // lblErrMsg
            // 
            this.lblErrMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErrMsg.Font = new System.Drawing.Font("Meiryo UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lblErrMsg.Location = new System.Drawing.Point(0, 0);
            this.lblErrMsg.Name = "lblErrMsg";
            this.lblErrMsg.Size = new System.Drawing.Size(594, 29);
            this.lblErrMsg.TabIndex = 0;
            this.lblErrMsg.Text = "label33";
            this.lblErrMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFuri
            // 
            this.lblFuri.BackColor = System.Drawing.SystemColors.Window;
            this.lblFuri.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFuri.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFuri.Location = new System.Drawing.Point(945, 8);
            this.lblFuri.Name = "lblFuri";
            this.lblFuri.Size = new System.Drawing.Size(159, 24);
            this.lblFuri.TabIndex = 265;
            this.lblFuri.Text = "ﾔﾏﾀﾞﾀﾛｳ";
            this.lblFuri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtKoutsuuhi
            // 
            this.txtKoutsuuhi.Font = new System.Drawing.Font("Meiryo UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKoutsuuhi.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtKoutsuuhi.Location = new System.Drawing.Point(1113, 376);
            this.txtKoutsuuhi.MaxLength = 5;
            this.txtKoutsuuhi.Name = "txtKoutsuuhi";
            this.txtKoutsuuhi.Size = new System.Drawing.Size(93, 26);
            this.txtKoutsuuhi.TabIndex = 268;
            this.txtKoutsuuhi.Text = "50000";
            this.txtKoutsuuhi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(1149, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 27);
            this.label4.TabIndex = 269;
            this.label4.Text = "交通費";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(1150, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 19);
            this.label5.TabIndex = 271;
            this.label5.Text = "日当";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNittou
            // 
            this.txtNittou.Font = new System.Drawing.Font("Meiryo UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNittou.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNittou.Location = new System.Drawing.Point(1113, 427);
            this.txtNittou.MaxLength = 5;
            this.txtNittou.Name = "txtNittou";
            this.txtNittou.Size = new System.Drawing.Size(93, 26);
            this.txtNittou.TabIndex = 270;
            this.txtNittou.Text = "98765";
            this.txtNittou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(1149, 463);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 19);
            this.label6.TabIndex = 273;
            this.label6.Text = "宿泊費";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtShukuhakuhi
            // 
            this.txtShukuhakuhi.Font = new System.Drawing.Font("Meiryo UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtShukuhakuhi.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtShukuhakuhi.Location = new System.Drawing.Point(1113, 482);
            this.txtShukuhakuhi.MaxLength = 5;
            this.txtShukuhakuhi.Name = "txtShukuhakuhi";
            this.txtShukuhakuhi.Size = new System.Drawing.Size(93, 26);
            this.txtShukuhakuhi.TabIndex = 272;
            this.txtShukuhakuhi.Text = "34500";
            this.txtShukuhakuhi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblWarn
            // 
            this.lblWarn.BackColor = System.Drawing.SystemColors.Control;
            this.lblWarn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWarn.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblWarn.ForeColor = System.Drawing.Color.Red;
            this.lblWarn.Location = new System.Drawing.Point(417, 0);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(177, 26);
            this.lblWarn.TabIndex = 274;
            this.lblWarn.Text = "既にデータを確定済です";
            this.lblWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWarn.Visible = false;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(1111, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 27);
            this.label8.TabIndex = 276;
            this.label8.Text = "週所定労働日数";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWdays
            // 
            this.lblWdays.BackColor = System.Drawing.SystemColors.Window;
            this.lblWdays.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWdays.Font = new System.Drawing.Font("Meiryo UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblWdays.Location = new System.Drawing.Point(1113, 185);
            this.lblWdays.Name = "lblWdays";
            this.lblWdays.Size = new System.Drawing.Size(93, 25);
            this.lblWdays.TabIndex = 278;
            this.lblWdays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(788, 658);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 29);
            this.button1.TabIndex = 279;
            this.button1.Text = "データ選択";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtJmpCode
            // 
            this.txtJmpCode.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtJmpCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtJmpCode.Location = new System.Drawing.Point(1113, 544);
            this.txtJmpCode.MaxLength = 5;
            this.txtJmpCode.Name = "txtJmpCode";
            this.txtJmpCode.Size = new System.Drawing.Size(93, 23);
            this.txtJmpCode.TabIndex = 280;
            this.txtJmpCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtJmpCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Meiryo UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(1113, 567);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 47);
            this.button2.TabIndex = 281;
            this.button2.Text = "指定職員へジャンプ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblShainKbnName
            // 
            this.lblShainKbnName.BackColor = System.Drawing.Color.White;
            this.lblShainKbnName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShainKbnName.Font = new System.Drawing.Font("Meiryo UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShainKbnName.Location = new System.Drawing.Point(1113, 93);
            this.lblShainKbnName.Name = "lblShainKbnName";
            this.lblShainKbnName.Size = new System.Drawing.Size(93, 29);
            this.lblShainKbnName.TabIndex = 285;
            this.lblShainKbnName.Text = "外国人技能実習生";
            this.lblShainKbnName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShainkbn
            // 
            this.lblShainkbn.BackColor = System.Drawing.SystemColors.Window;
            this.lblShainkbn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShainkbn.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShainkbn.Location = new System.Drawing.Point(1183, 99);
            this.lblShainkbn.Name = "lblShainkbn";
            this.lblShainkbn.Size = new System.Drawing.Size(20, 23);
            this.lblShainkbn.TabIndex = 286;
            this.lblShainkbn.Text = "1";
            this.lblShainkbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShainkbn.Visible = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblWarn);
            this.panel2.Controls.Add(this.lblNoImage);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(6, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(597, 642);
            this.panel2.TabIndex = 287;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(8, 658);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(216, 27);
            this.trackBar1.TabIndex = 288;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.hScrollBar1);
            this.panel3.Location = new System.Drawing.Point(331, 656);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(271, 32);
            this.panel3.TabIndex = 289;
            // 
            // dGV
            // 
            this.dGV.Location = new System.Drawing.Point(611, 36);
            this.dGV.Name = "dGV";
            this.dGV.Size = new System.Drawing.Size(493, 578);
            this.dGV.TabIndex = 0;
            this.dGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGV_CellClick);
            this.dGV.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter_1);
            this.dGV.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dGV.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dGV.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // lblNougyou
            // 
            this.lblNougyou.BackColor = System.Drawing.Color.White;
            this.lblNougyou.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNougyou.Font = new System.Drawing.Font("Meiryo UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNougyou.Location = new System.Drawing.Point(1113, 125);
            this.lblNougyou.Name = "lblNougyou";
            this.lblNougyou.Size = new System.Drawing.Size(93, 29);
            this.lblNougyou.TabIndex = 290;
            this.lblNougyou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmCorrectMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 692);
            this.Controls.Add(this.lblNougyou);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.lblShainKbnName);
            this.Controls.Add(this.lblShainkbn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtJmpCode);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblWdays);
            this.Controls.Add(this.txtShukuhakuhi);
            this.Controls.Add(this.txtNittou);
            this.Controls.Add(this.txtKoutsuuhi);
            this.Controls.Add(this.lblFuri);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnDataMake);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.btnErrCheck);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBefore);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.dGV);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCorrectMonth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "勤怠申請書データ登録";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCorrect_FormClosing);
            this.Load += new System.EventHandler(this.frmCorrect_Load);
            this.Shown += new System.EventHandler(this.frmCorrect_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel3.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnDataMake;
        private System.Windows.Forms.Button btnRtn;
        private System.Windows.Forms.Button btnErrCheck;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBefore;
        private System.Windows.Forms.Button btnFirst;
        private DataGridViewEx dGV;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblNoImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblErrMsg;
        private System.Windows.Forms.Label lblFuri;
        private System.Windows.Forms.TextBox txtKoutsuuhi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNittou;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtShukuhakuhi;
        private System.Windows.Forms.Label lblWarn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblWdays;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtJmpCode;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblShainKbnName;
        private System.Windows.Forms.Label lblShainkbn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblNougyou;
    }
}