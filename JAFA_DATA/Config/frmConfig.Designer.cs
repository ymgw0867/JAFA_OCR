﻿namespace JAFA_DATA.Config
{
    partial class frmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtArchive = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtCsvPath = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "年";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(98, 26);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(59, 27);
            this.txtYear.TabIndex = 0;
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // txtMonth
            // 
            this.txtMonth.Location = new System.Drawing.Point(187, 26);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(59, 27);
            this.txtMonth.TabIndex = 1;
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "月";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "受け渡しデータ作成先パス";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "参照";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "対象年月";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(393, 291);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 34);
            this.button2.TabIndex = 5;
            this.button2.Text = "登録";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(493, 291);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 34);
            this.button3.TabIndex = 6;
            this.button3.Text = "終了";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "履歴データ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(151, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "ヶ月経過したら自動削除する";
            // 
            // txtArchive
            // 
            this.txtArchive.Location = new System.Drawing.Point(98, 141);
            this.txtArchive.MaxLength = 2;
            this.txtArchive.Name = "txtArchive";
            this.txtArchive.Size = new System.Drawing.Size(53, 27);
            this.txtArchive.TabIndex = 3;
            this.txtArchive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtArchive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 19);
            this.label5.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(161, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(223, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "※自動削除しないときは「0」を登録して下さい";
            // 
            // txtPath
            // 
            this.txtPath.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPath.Location = new System.Drawing.Point(27, 95);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(482, 27);
            this.txtPath.TabIndex = 16;
            this.txtPath.TabStop = false;
            // 
            // txtCsvPath
            // 
            this.txtCsvPath.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCsvPath.Location = new System.Drawing.Point(27, 231);
            this.txtCsvPath.Name = "txtCsvPath";
            this.txtCsvPath.Size = new System.Drawing.Size(482, 27);
            this.txtCsvPath.TabIndex = 19;
            this.txtCsvPath.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(509, 230);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(69, 28);
            this.button4.TabIndex = 4;
            this.button4.Text = "参照";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(187, 19);
            this.label9.TabIndex = 18;
            this.label9.Text = "祝日ＣＳＶデータ作成先パス";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 337);
            this.Controls.Add(this.txtCsvPath);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtArchive);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "環境設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfig_FormClosing);
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtArchive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtCsvPath;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}