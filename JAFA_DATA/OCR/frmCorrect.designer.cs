namespace JAFA_DATA.OCR
{
    partial class frmCorrect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorrect));
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
            this.lblZenKou = new System.Windows.Forms.Label();
            this.txtKoutsuuhi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNittou = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtShukuhakuhi = new System.Windows.Forms.TextBox();
            this.lblWarn = new System.Windows.Forms.Label();
            this.lblWdays = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblShainkbn = new System.Windows.Forms.Label();
            this.lblShainKbnName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.dGV = new JAFA_DATA.DataGridViewEx();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.Window;
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblName.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblName.Location = new System.Drawing.Point(707, 83);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(140, 28);
            this.lblName.TabIndex = 58;
            this.lblName.Text = "山田　太郎";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNo
            // 
            this.txtNo.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNo.Location = new System.Drawing.Point(624, 83);
            this.txtNo.MaxLength = 5;
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(82, 28);
            this.txtNo.TabIndex = 3;
            this.txtNo.Text = "123456";
            this.txtNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNo.WordWrap = false;
            this.txtNo.TextChanged += new System.EventHandler(this.txtNo_TextChanged);
            this.txtNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(748, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 20);
            this.label3.TabIndex = 56;
            this.label3.Text = "月";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(686, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 20);
            this.label2.TabIndex = 55;
            this.label2.Text = "年";
            // 
            // txtMonth
            // 
            this.txtMonth.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMonth.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMonth.Location = new System.Drawing.Point(712, 51);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(36, 28);
            this.txtMonth.TabIndex = 2;
            this.txtMonth.Text = "12";
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.WordWrap = false;
            this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // txtYear
            // 
            this.txtYear.BackColor = System.Drawing.SystemColors.Window;
            this.txtYear.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtYear.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtYear.Location = new System.Drawing.Point(624, 51);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(62, 28);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "2018";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.WordWrap = false;
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPage.Location = new System.Drawing.Point(1018, 56);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(97, 23);
            this.lblPage.TabIndex = 53;
            this.lblPage.Text = "lblPage";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(274, 28);
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
            this.btnEnd.Location = new System.Drawing.Point(311, 655);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(26, 32);
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
            this.btnNext.Location = new System.Drawing.Point(286, 655);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(26, 32);
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
            this.btnBefore.Location = new System.Drawing.Point(261, 655);
            this.btnBefore.Name = "btnBefore";
            this.btnBefore.Size = new System.Drawing.Size(26, 32);
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
            this.btnFirst.Location = new System.Drawing.Point(236, 655);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(26, 32);
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
            this.btnDel.Location = new System.Drawing.Point(874, 655);
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
            this.btnDataMake.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataMake.Location = new System.Drawing.Point(849, 52);
            this.btnDataMake.Name = "btnDataMake";
            this.btnDataMake.Size = new System.Drawing.Size(131, 28);
            this.btnDataMake.TabIndex = 6;
            this.btnDataMake.Text = "勤怠データ確定(&M)";
            this.btnDataMake.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnDataMake, "エラーチェックの後、勤怠データを作成します");
            this.btnDataMake.UseVisualStyleBackColor = true;
            this.btnDataMake.Click += new System.EventHandler(this.btnDataMake_Click);
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Image = ((System.Drawing.Image)(resources.GetObject("btnRtn.Image")));
            this.btnRtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRtn.Location = new System.Drawing.Point(1011, 655);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(108, 33);
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
            this.btnErrCheck.Location = new System.Drawing.Point(624, 655);
            this.btnErrCheck.Name = "btnErrCheck";
            this.btnErrCheck.Size = new System.Drawing.Size(121, 33);
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
            this.lblNoImage.Location = new System.Drawing.Point(135, 336);
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
            this.pictureBox1.Size = new System.Drawing.Size(533, 638);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 120;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblErrMsg);
            this.panel1.Location = new System.Drawing.Point(624, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 39);
            this.panel1.TabIndex = 162;
            // 
            // lblErrMsg
            // 
            this.lblErrMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErrMsg.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lblErrMsg.Location = new System.Drawing.Point(0, 0);
            this.lblErrMsg.Name = "lblErrMsg";
            this.lblErrMsg.Size = new System.Drawing.Size(491, 35);
            this.lblErrMsg.TabIndex = 0;
            this.lblErrMsg.Text = "label33";
            this.lblErrMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFuri
            // 
            this.lblFuri.BackColor = System.Drawing.SystemColors.Window;
            this.lblFuri.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFuri.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFuri.Location = new System.Drawing.Point(849, 83);
            this.lblFuri.Name = "lblFuri";
            this.lblFuri.Size = new System.Drawing.Size(131, 28);
            this.lblFuri.TabIndex = 265;
            this.lblFuri.Text = "ﾔﾏﾀﾞﾀﾛｳ";
            this.lblFuri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZenKou
            // 
            this.lblZenKou.BackColor = System.Drawing.SystemColors.Control;
            this.lblZenKou.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblZenKou.Location = new System.Drawing.Point(766, 56);
            this.lblZenKou.Name = "lblZenKou";
            this.lblZenKou.Size = new System.Drawing.Size(55, 20);
            this.lblZenKou.TabIndex = 267;
            this.lblZenKou.Text = "前半";
            this.lblZenKou.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtKoutsuuhi
            // 
            this.txtKoutsuuhi.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKoutsuuhi.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtKoutsuuhi.Location = new System.Drawing.Point(687, 592);
            this.txtKoutsuuhi.MaxLength = 5;
            this.txtKoutsuuhi.Name = "txtKoutsuuhi";
            this.txtKoutsuuhi.Size = new System.Drawing.Size(55, 23);
            this.txtKoutsuuhi.TabIndex = 268;
            this.txtKoutsuuhi.Text = "50000";
            this.txtKoutsuuhi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(622, 589);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 27);
            this.label4.TabIndex = 269;
            this.label4.Text = "交通費：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(743, 593);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 19);
            this.label5.TabIndex = 271;
            this.label5.Text = "日当：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNittou
            // 
            this.txtNittou.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNittou.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNittou.Location = new System.Drawing.Point(795, 592);
            this.txtNittou.MaxLength = 5;
            this.txtNittou.Name = "txtNittou";
            this.txtNittou.Size = new System.Drawing.Size(57, 23);
            this.txtNittou.TabIndex = 270;
            this.txtNittou.Text = "98765";
            this.txtNittou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(862, 593);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 19);
            this.label6.TabIndex = 273;
            this.label6.Text = "宿泊費：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtShukuhakuhi
            // 
            this.txtShukuhakuhi.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtShukuhakuhi.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtShukuhakuhi.Location = new System.Drawing.Point(918, 592);
            this.txtShukuhakuhi.MaxLength = 5;
            this.txtShukuhakuhi.Name = "txtShukuhakuhi";
            this.txtShukuhakuhi.Size = new System.Drawing.Size(59, 23);
            this.txtShukuhakuhi.TabIndex = 272;
            this.txtShukuhakuhi.Text = "34500";
            this.txtShukuhakuhi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblWarn
            // 
            this.lblWarn.BackColor = System.Drawing.SystemColors.Control;
            this.lblWarn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWarn.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblWarn.ForeColor = System.Drawing.Color.Red;
            this.lblWarn.Location = new System.Drawing.Point(0, 0);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(493, 27);
            this.lblWarn.TabIndex = 274;
            this.lblWarn.Text = "既にデータを確定済みです";
            this.lblWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWdays
            // 
            this.lblWdays.BackColor = System.Drawing.SystemColors.Window;
            this.lblWdays.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWdays.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblWdays.Location = new System.Drawing.Point(1062, 590);
            this.lblWdays.Name = "lblWdays";
            this.lblWdays.Size = new System.Drawing.Size(55, 25);
            this.lblWdays.TabIndex = 282;
            this.lblWdays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(979, 589);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 27);
            this.label8.TabIndex = 280;
            this.label8.Text = "週所定日数：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblShainkbn
            // 
            this.lblShainkbn.BackColor = System.Drawing.SystemColors.Window;
            this.lblShainkbn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShainkbn.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShainkbn.Location = new System.Drawing.Point(1078, 79);
            this.lblShainkbn.Name = "lblShainkbn";
            this.lblShainkbn.Size = new System.Drawing.Size(23, 34);
            this.lblShainkbn.TabIndex = 283;
            this.lblShainkbn.Text = "1";
            this.lblShainkbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShainkbn.Visible = false;
            // 
            // lblShainKbnName
            // 
            this.lblShainKbnName.BackColor = System.Drawing.SystemColors.Control;
            this.lblShainKbnName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShainKbnName.Font = new System.Drawing.Font("Meiryo UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShainKbnName.Location = new System.Drawing.Point(982, 83);
            this.lblShainKbnName.Name = "lblShainKbnName";
            this.lblShainKbnName.Size = new System.Drawing.Size(137, 28);
            this.lblShainKbnName.TabIndex = 284;
            this.lblShainKbnName.Text = "外国人技能実習生";
            this.lblShainKbnName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblWarn);
            this.panel2.Location = new System.Drawing.Point(624, 619);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 29);
            this.panel2.TabIndex = 285;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(11, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(604, 638);
            this.panel3.TabIndex = 286;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.hScrollBar1);
            this.panel4.Location = new System.Drawing.Point(339, 656);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(276, 30);
            this.panel4.TabIndex = 287;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(10, 655);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(223, 30);
            this.trackBar1.TabIndex = 288;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // dGV
            // 
            this.dGV.Location = new System.Drawing.Point(624, 116);
            this.dGV.Name = "dGV";
            this.dGV.Size = new System.Drawing.Size(495, 470);
            this.dGV.TabIndex = 0;
            this.dGV.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter_1);
            this.dGV.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dGV.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dGV.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // frmCorrect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 692);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.lblNoImage);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblWdays);
            this.Controls.Add(this.txtShukuhakuhi);
            this.Controls.Add(this.txtNittou);
            this.Controls.Add(this.txtKoutsuuhi);
            this.Controls.Add(this.lblShainKbnName);
            this.Controls.Add(this.lblShainkbn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblZenKou);
            this.Controls.Add(this.lblFuri);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMonth);
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
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCorrect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "勤怠申請書データ登録";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCorrect_FormClosing);
            this.Load += new System.EventHandler(this.frmCorrect_Load);
            this.Shown += new System.EventHandler(this.frmCorrect_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
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
        private System.Windows.Forms.Label lblZenKou;
        private System.Windows.Forms.TextBox txtKoutsuuhi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNittou;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtShukuhakuhi;
        private System.Windows.Forms.Label lblWarn;
        private System.Windows.Forms.Label lblWdays;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblShainkbn;
        private System.Windows.Forms.Label lblShainKbnName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}