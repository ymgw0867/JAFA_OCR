using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAFA_DATA.Common;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;

namespace JAFA_DATA.Master
{
    public partial class frmShainImportXls : Form
    {
        public frmShainImportXls()
        {
            InitializeComponent();
        }

        #region // エクセルシートデータ列定義
        const int COL_CODE = 1;         // 職員コード
        const int COL_NAME = 2;         // 氏名
        const int COL_FURI = 3;         // フリガナ
        const int COL_SZCODE = 4;       // 所属コード
        const int COL_SZNAME = 5;       // 所属名
        const int COL_NYUYMD = 6;       // 入所年月日
        const int COL_WORKDAYS = 7;     // 週契約日
        const int COL_WEEKDAY = 8;      // 週開始曜日
        #endregion

        // 処理選択
        int s = 0;
        const int sOVERWRITE = 1;
        const int sADD = 2;

        DateTime taishakuDate = DateTime.Parse("1900/01/01"); 

        private void button1_Click(object sender, EventArgs e)
        {
            // エクセルファイル選択
            string file = openExcelFile(s);

            if (file == string.Empty)
            {
                button2.Enabled = false;
                return;
            }

            if (file != string.Empty)
            {
                label1.Text = file;
                button2.Enabled = true;
            }
        }

        private string openExcelFile(int s)
        {
            DialogResult ret;

            //ダイアログボックスの初期設定
            openFileDialog1.Title = "エクセル社員マスターシートの選択";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";

            openFileDialog1.Filter = "Excelブック(*.xls;*.xlsx)|*.xls;*.xlsx|全てのファイル(*.*)|*.*";

            //if (s == sOVERWRITE)
            //{
            //    openFileDialog1.Filter = "Excelブック(*.xls; *.xlsx)| *.xls; *.xlsx | 全てのファイル(*.*) | *.* ";
            //}
            //else if (s == sADD)
            //{
            //    openFileDialog1.Filter = "Excelブック(*.xls;*.xlsx)|*.xls;*.xlsx|全てのファイル(*.*)|*.*";
            //}

            //ダイアログボックスの表示
            ret = openFileDialog1.ShowDialog();
            if (ret == System.Windows.Forms.DialogResult.Cancel)
            {
                return string.Empty;
            }

            //if (MessageBox.Show(openFileDialog1.FileName + Environment.NewLine + " が選択されました。よろしいですか?", "呼び出しシート名確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //{
            //    return string.Empty;
            //}

            return openFileDialog1.FileName;
        }
        
        /// ------------------------------------------------------------------
        /// <summary>
        ///     エクセルシートより社員マスターへ新規登録する </summary>
        /// ------------------------------------------------------------------
        private void addMateData(string xlsFile)
        {
            JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
            JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter adp = new JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter();
            
            try
            {
                adp.Fill(dts.社員マスター);

                int cnt = 0;

                //マウスポインタを待機にする
                this.Cursor = Cursors.WaitCursor;

                // Excel社員マスターシートの存在を確認
                if (System.IO.File.Exists(xlsFile))
                {
                    using (var book = new XLWorkbook(xlsFile, XLEventTracking.Disabled))
                    {
                        var sheet1 = book.Worksheet(1);
                        var tbl = sheet1.RangeUsed().AsTable();

                        foreach (var t in tbl.DataRange.Rows())
                        {
                            int sCode = Utility.StrtoInt(Utility.NulltoStr(t.Cell(1).Value));

                            if (sCode == global.flgOff)
                            {
                                // 社員コード欄が数字ではないとき読み飛ばす
                                continue;
                            }

                            if (dts.社員マスター.Any(a => a.職員コード == sCode))
                            {
                                // 登録済み社員コードのとき読み飛ばす
                                continue;
                            }

                            // 入所年月日
                            DateTime nyuDt;
                            if (!DateTime.TryParse(Utility.NulltoStr(t.Cell(6).Value), out nyuDt))
                            {
                                nyuDt = DateTime.Parse("1900/01/01");
                            }

                            // 調整年月日
                            DateTime choDt;
                            int yukyuFuyoMonth = 0;
                            if (DateTime.TryParse(Utility.NulltoStr(t.Cell(7).Value), out choDt))
                            {
                                // 有給付与月
                                yukyuFuyoMonth = choDt.AddMonths(6).Month;
                            }
                            else
                            {
                                choDt = DateTime.Parse("1900/01/01");

                                // 有給付与月
                                yukyuFuyoMonth = 0;
                            }

                            adp.Insert(Utility.StrtoInt(Utility.NulltoStr(t.Cell(1).Value)),
                                       Utility.NulltoStr(t.Cell(2).Value),
                                       Utility.NulltoStr(t.Cell(3).Value),
                                       Utility.StrtoInt(Utility.NulltoStr(t.Cell(4).Value)),
                                       Utility.NulltoStr(t.Cell(5).Value),
                                       nyuDt, choDt, taishakuDate,
                                       Utility.StrtoInt(Utility.NulltoStr(t.Cell(8).Value)),
                                       global.flgOff,
                                       yukyuFuyoMonth,
                                       string.Empty,
                                       DateTime.Now,
                                       Utility.StrtoInt(Utility.NulltoStr(t.Cell(9).Value)),
                                       Utility.StrtoInt(Utility.NulltoStr(t.Cell(10).Value)),
                                       Utility.StrtoInt(Utility.NulltoStr(t.Cell(11).Value)));

                            cnt++;
                        }

                        sheet1.Dispose();
                    }

                    // 終了
                    MessageBox.Show(cnt.ToString() + "件、追加登録されました。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                //マウスポインタを元に戻す
                this.Cursor = Cursors.Default;
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Excelファイルより差分登録
            masterUpdate();

            // 入社時有給休暇日数をマスター登録する
            this.Cursor = Cursors.WaitCursor;
            Utility.setInitialYukyu();
            this.Cursor = Cursors.Default;

            // 終了
            this.Close();
        }

        private void masterUpdate()
        {
            if (label1.Text == string.Empty)
            {
                MessageBox.Show("読み込むファイルを選択してください", "ファイル未選択", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string sMsg = "差分登録";

            if (MessageBox.Show(label1.Text + "で社員マスターへ" + sMsg + "します。よろしいですか", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            // 差分登録
            addMateData(label1.Text);
        }

        private void frmMateImportXls_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMateImportXls_Load(object sender, EventArgs e)
        {
            //button1.Enabled = false;
            button2.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void rBtn1_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }        
    }
}
