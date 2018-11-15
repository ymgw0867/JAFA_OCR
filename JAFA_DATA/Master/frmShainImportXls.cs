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

            adp.Fill(dts.社員マスター);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter adp = new JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter();
        
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
            try
            {
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
            masterUpdate();

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

            //if (s == sOVERWRITE)
            //{
            //    // 一括上書き
            //    sMsg = "一括上書き";
            //}
            //else if (s == sADD)
            //{
            //    // 差分登録
            //    sMsg = "差分登録";
            //}

            //// 差分登録
            //sMsg = "差分登録";

            if (MessageBox.Show(label1.Text + "で社員マスターへ" + sMsg + "します。よろしいですか", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //if (s == sOVERWRITE)
            //{
            //    // 一括上書き
            //    Common.frmPrg frm = new frmPrg();
            //    CsvToMdb(label1.Text); 

            //}
            //else if (s == sADD)
            //{
            //    // 差分登録
            //    addMateData(label1.Text);
            //}

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


        /// -------------------------------------------------------------------
        /// <summary>
        ///     CSVファイルインポート </summary>
        /// <param name="_InPath">
        ///     CSVファイルパス</param>
        /// -------------------------------------------------------------------
        public void CsvToMdb(string _InPath)
        {
            string headerKey = string.Empty;    // ヘッダキー
            int cnt = 0;

            try
            {
                // CSVファイルインポート
                var s = System.IO.File.ReadAllLines(_InPath, Encoding.Default);
                foreach (var stBuffer in s)
                {
                    // カンマ区切りで分割して配列に格納する
                    string[] stCSV = stBuffer.Split(',');
                        
                    // 職員コード
                    if (Utility.StrtoInt(stCSV[0]) == 0)
                    {
                        continue;
                    }

                    // マスター更新
                    upMateData(stCSV);
                    cnt++;
                }

                // データベースへ反映
                adp.Update(dts.社員マスター);

                // 終了
                MessageBox.Show(cnt.ToString() + "件、処理しました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CSVインポート処理", MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        /// ---------------------------------------------------------------
        /// <summary>
        ///     社員マスター更新 </summary>
        /// <param name="c">
        ///     CSVデータ配列</param>
        /// ---------------------------------------------------------------
        private void upMateData(string [] c)
        {
            int sCode = Utility.StrtoInt(c[0]);

            if (dts.社員マスター.Any(a => a.職員コード == sCode))
            {
                // 登録済みのとき：上書き更新
                sOverWriteMaster(sCode, c);
            }
            else
            {
                // 新規登録
                sAddMaster(sCode, c);
            }
        }

        /// ---------------------------------------------------------------
        /// <summary>
        ///     社員マスターレコード上書き </summary>
        /// <param name="sCode">
        ///     職員コード</param>
        /// <param name="c">
        ///     CSVデータ配列</param>
        /// ---------------------------------------------------------------
        private void sOverWriteMaster(int sCode, string [] c)
        {
            JAFA_OCRDataSet.社員マスターRow r = dts.社員マスター.Single(a => a.職員コード == sCode);

            setMasterRow(r, c);
        }

        /// ---------------------------------------------------------------
        /// <summary>
        ///     社員マスターレコード追加 </summary>
        /// <param name="sCode">
        ///     職員コード</param>
        /// <param name="c">
        ///     CSVデータ配列</param>
        /// ---------------------------------------------------------------
        private void sAddMaster(int sCode, string[] c)
        {
            JAFA_OCRDataSet.社員マスターRow r = dts.社員マスター.New社員マスターRow();

            r.職員コード = sCode;
            dts.社員マスター.Add社員マスターRow(setMasterRow(r, c));
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        ///     社員マスターレコードにデータをセットする </summary>
        /// <param name="r">
        ///     JAHR_OCRDataSet.社員マスターRow </param>
        /// <param name="c">
        ///     CSVデータ配列</param>
        /// <returns>
        ///     JAHR_OCRDataSet.社員マスターRow</returns>
        /// ----------------------------------------------------------------------------
        private JAFA_OCRDataSet.社員マスターRow setMasterRow(JAFA_OCRDataSet.社員マスターRow r, string[] c)
        {
            DateTime dt;

            r.氏名 = c[1];
            r.フリガナ = c[2];
            r.所属コード = Utility.StrtoInt(c[3]);
            r.所属名 = c[4];

            if (DateTime.TryParse(c[5], out dt))
            {
                r.入所年月日 = dt;
            }

            if (DateTime.TryParse(c[6], out dt))
            {
                r.調整年月日 = dt;
            }

            if (DateTime.TryParse(c[7], out dt))
            {
                r.退職年月日 = dt;
            }

            r.週所定労働日数 = Utility.StrtoInt(c[8]);
            r.退職区分 = Utility.StrtoInt(c[9]);
            //r.週開始曜日 = Utility.StrtoInt(c[10]);    // 2018/10/22 コメント化
            r.有給付与月 = Utility.StrtoInt(c[11]);
            r.備考 = c[12];
            r.更新年月日 = DateTime.Now;

            return r;
        }
    }
}
