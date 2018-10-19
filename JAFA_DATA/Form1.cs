using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAFA_DATA.Common;

namespace JAFA_DATA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Config.getConfig cnf = new Config.getConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 確定データが存在するときMDBをコピーする
            copyMdbData();

            // 閉じる
            this.Close();
        }

        /// ---------------------------------------------------------------
        /// <summary>
        ///     MDBファイルのコピー </summary>
        /// ---------------------------------------------------------------
        private void copyMdbData()
        {
            JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
            JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter kHD = new JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter();
            kHD.Fill(dts.確定勤務票ヘッダ);

            // 確定勤務票ヘッダのデータが存在するときに実施する
            if (dts.確定勤務票ヘッダ.Count() > 0)
            {
                // コピー先フォルダが存在しないときは作成する
                if (!System.IO.Directory.Exists(Properties.Settings.Default.kaBackupPath))
                {
                    System.IO.Directory.CreateDirectory(Properties.Settings.Default.kaBackupPath);
                }

                // 元ファイル名とコピー先ファイル名
                string frmMDB = Properties.Settings.Default.ocrMdbPath;
                string toMDB = Properties.Settings.Default.kaBackupPath + System.IO.Path.GetFileName(Properties.Settings.Default.ocrMdbPath);

                // コピーします
                System.IO.File.Copy(frmMDB, toMDB, true);
            }
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 後片付け
            this.Dispose();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new Config.frmConfig();
            frm.ShowDialog();
            this.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Master.frmMsMenu frm = new Master.frmMsMenu();
            frm.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 環境設定年月の確認
            string msg = "処理対象年月は " + Properties.Settings.Default.gengou + global.cnfYear.ToString() + "年 " + global.cnfMonth.ToString() + "月です。よろしいですか？";
            if (MessageBox.Show(msg, "勤務データ登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;
            
            this.Hide();
            OCR.frmCorrect frm = new OCR.frmCorrect();
            frm.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 環境設定年月の確認
            string msg = "処理対象年月は " + Properties.Settings.Default.gengou + global.cnfYear.ToString() + "年 " + global.cnfMonth.ToString() + "月です。よろしいですか？";
            if (MessageBox.Show(msg, "勤務データ登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            this.Hide();
            OCR.frmCorrectMonth frm = new OCR.frmCorrectMonth();
            frm.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCR.frmRecovery frm = new OCR.frmRecovery();
            frm.ShowDialog();
            this.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCR.frmPastDataViewer frm = new OCR.frmPastDataViewer();
            frm.ShowDialog();
            this.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            workData.frmWorkMenu frm = new workData.frmWorkMenu();
            frm.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCR.frmNG frm = new OCR.frmNG();
            frm.ShowDialog();
            this.Show();
        }

    }
}
