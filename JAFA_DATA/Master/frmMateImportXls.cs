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

namespace JAFA_DATA.Master
{
    public partial class frmMateImportXls : Form
    {
        public frmMateImportXls()
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
        
        private void button1_Click(object sender, EventArgs e)
        {
            // 処理選択
            if (rBtn1.Checked == true)
            {
                s = sOVERWRITE;
            }
            else if (rBtn2.Checked == true)
            {
                s = sADD;
            }

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
            openFileDialog1.Title = "エクセル就業参考表の選択";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";

            if (s == sOVERWRITE)
            {
                openFileDialog1.Filter = "CSVファイル(*.csv)|*.csv";
            }
            else if (s == sADD)
            {
                openFileDialog1.Filter = "Excelブック(*.xls;*.xlsx)|*.xls;*.xlsx|全てのファイル(*.*)|*.*";
            }

            //ダイアログボックスの表示
            ret = openFileDialog1.ShowDialog();
            if (ret == System.Windows.Forms.DialogResult.Cancel)
            {
                return string.Empty;
            }

            if (MessageBox.Show(openFileDialog1.FileName + Environment.NewLine + " が選択されました。よろしいですか?", "呼び出しシート名確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return string.Empty;
            }

            return openFileDialog1.FileName;
        }
        
        /// ------------------------------------------------------------------
        /// <summary>
        ///     エクセルシートより社員マスターへ新規登録する </summary>
        /// ------------------------------------------------------------------
        private bool addMateData(string xlsFile)
        {
            //マウスポインタを待機にする
            this.Cursor = Cursors.WaitCursor;

            Excel.Application oXls = new Excel.Application();

            Excel.Workbook oXlsBook = (Excel.Workbook)(oXls.Workbooks.Open(xlsFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing));

            Excel.Worksheet oxlsSheet = (Excel.Worksheet)oXlsBook.Sheets[1];

            Excel.Range dRng;
            Excel.Range[] rng = new Microsoft.Office.Interop.Excel.Range[2];

            Excel.Range stRange = (Excel.Range)oxlsSheet.Cells[1, 1];
            Excel.Range edRange = (Excel.Range)oxlsSheet.Cells[63, 44];

            int dCnt = 0;

            try
            {
                // 読み込み開始行
                int fromRow = 2;

                // 利用領域行数を取得
                int toRow = oxlsSheet.UsedRange.Rows.Count;
                
                // エクセルシートの支払区分の列を順次読み込む
                for (int i = fromRow; i <= toRow; i++)
                {
                    // 職員コードの値を取得します
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_CODE];
                    string hCode = dRng.Text.ToString().Trim();

                    // セルに有効値があること
                    if (!Utility.NumericCheck(hCode))
                    {
                        continue;
                    }

                    // 登録済みか調べる
                    if (dts.社員マスター.Any(a => a.職員コード == Utility.StrtoInt(hCode)))
                    {
                        // 登録済のコードのときは読み飛ばす
                        continue;
                    }

                    // 氏名
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_NAME];
                    string sName = dRng.Text.ToString().Trim();

                    // フリガナ
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_FURI];
                    string sFuri = dRng.Text.ToString().Trim();

                    // 所属コード
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_SZCODE];
                    string szCode = dRng.Text.ToString().Trim();

                    // 所属名
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_SZNAME];
                    string szName = dRng.Text.ToString().Trim();

                    // 入所年月日
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_NYUYMD];
                    string nYMD = dRng.Text.ToString().Trim();

                    // 週労働日数
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_WORKDAYS];
                    string wDays = dRng.Text.ToString().Trim();

                    // 週開始曜日
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_WEEKDAY];
                    string wWeek = dRng.Text.ToString().Trim();

                    JAFA_OCRDataSet.社員マスターRow r = dts.社員マスター.New社員マスターRow();
                    r.職員コード = Utility.StrtoInt(hCode);
                    r.氏名 = sName;
                    r.フリガナ = sFuri;
                    r.所属コード = Utility.StrtoInt(szCode);
                    r.所属名 = szName;

                    DateTime dt = DateTime.Today;
                    if (DateTime.TryParse(nYMD, out dt))
                    {
                        r.入所年月日 = dt;
                        r.調整年月日 = dt;
                    }
                    else
                    {
                        r.入所年月日 = DateTime.Parse("1900/01/01");
                        r.調整年月日 = DateTime.Parse("1900/01/01");
                    }
                    
                    r.退職年月日 = DateTime.Parse("1900/01/01");
                    r.週所定労働日数 = Utility.StrtoInt(wDays);
                    r.退職区分 = global.flgOff;
                    //r.週開始曜日 = Utility.StrtoInt(wWeek) - 1;    // 2018/10/22 コメント化

                    int fm = r.調整年月日.Month + 6;
                    if (fm > 12)
                    {
                        fm -= 12;
                    }

                    r.有給付与月 = fm;
                    r.備考 = string.Empty;
                    r.更新年月日 = DateTime.Now;
                    
                    // 社員マスター追加登録
                    dts.社員マスター.Add社員マスターRow(r);

                    dCnt++;
                }

                // データベース更新
                adp.Update(dts.社員マスター);
                
                //マウスポインタを元に戻す
                this.Cursor = Cursors.Default;

                // 確認のためExcelのウィンドウを表示する
                //oXls.Visible = true;

                //印刷
                //oxlsSheet.PrintPreview(true);

                //保存処理
                oXls.DisplayAlerts = false;

                // 終了メッセージ
                MessageBox.Show("社員マスターのエクセルからのインポートが終了しました。" + Environment.NewLine + Environment.NewLine + dCnt.ToString() + "件のデータを追加登録しました。","社員マスター", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エクセルシートオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            finally
            {
                //Bookをクローズ
                oXlsBook.Close(Type.Missing, Type.Missing, Type.Missing);

                //Excelを終了
                oXls.Quit();

                // COM オブジェクトの参照カウントを解放する 
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oxlsSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXlsBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXls);

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

            string sMsg = string.Empty;
            if (s == sOVERWRITE)
            {
                // 一括上書き
                sMsg = "一括上書き";
            }
            else if (s == sADD)
            {
                // 差分登録
                sMsg = "差分登録";
            }

            if (MessageBox.Show(label1.Text + "で社員マスターへ" + sMsg + "します。よろしいですか", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            
            if (s == sOVERWRITE)
            {
                // 一括上書き
                Common.frmPrg frm = new frmPrg();
                CsvToMdb(label1.Text); 

            }
            else if (s == sADD)
            {
                // 差分登録
                addMateData(label1.Text);
            }
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
            button1.Enabled = false;
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
