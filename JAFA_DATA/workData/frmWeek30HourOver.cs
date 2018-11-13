using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using JAFA_DATA.Common;

namespace JAFA_DATA.workData
{
    public partial class frmWeek30HourOver : Form
    {
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "週30時間超労働時間一覧表";    // アプリケーション表題

        public frmWeek30HourOver()
        {
            InitializeComponent();
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter wAdp = new JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter();
        JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter sAdp = new JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter();
        
        #region グリッドカラム定義
        string colShokuin = "c1";
        string colShokuinName = "c2";
        string colSzCode = "c3";
        string colSzName = "c4";
        string colShoteiTime = "c5";
        string colShoteiDays = "c6";
        string colOverTime = "c7";
        string colDay = "c39";
        string colShainKbn = "c40";
        string colSt = "c41";
        string colEt = "c42";
        string colRes = "c43";
        string colJitsurou = "c44";
        string colMtotal = "c45";
        #endregion
        
        string[] holArray = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            // ウィンドウズ最小サイズ
            Utility.WindowsMinSize(this, this.Size.Width, this.Size.Height);

            // ウィンドウズ最大サイズ
            //Utility.WindowsMaxSize(this, this.Size.Width, this.Size.Height);

            // キャプション
            this.Text = appName;

            // DataGridViewの設定
            GridViewSetting(dg1);

            // 年月表示
            txtYear.Text = global.cnfYear.ToString();
            txtMonth.Text = global.cnfMonth.ToString();
            txtYear.Focus();

            label10.Text = string.Empty;

            button1.Enabled = false;

            // 初期状態は全て
            //radioButton1.Checked = true;

            // 休日CSVファイルの休日日付を配列に読み込む
            holCsvToArray();
        }

        ///---------------------------------------------------------------------
        /// <summary>
        ///     データグリッドビューの定義を行います </summary>
        /// <param name="tempDGV">
        ///     データグリッドビューオブジェクト</param>
        ///---------------------------------------------------------------------
        public void GridViewSetting(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更するe

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", (float)9, FontStyle.Regular);

                // 行の高さ
                tempDGV.ColumnHeadersHeight = 20;
                tempDGV.RowTemplate.Height = 20;

                // 全体の高さ
                tempDGV.Height = 562;

                // 奇数行の色
                //tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = false;

                // カラム定義
                tempDGV.Columns.Add(colSzCode, "所属コード");
                tempDGV.Columns.Add(colSzName, "所属名");
                tempDGV.Columns.Add(colShokuin, "職員コード");
                tempDGV.Columns.Add(colShokuinName, "職員名");
                tempDGV.Columns.Add(colShainKbn, "社員区分");
                tempDGV.Columns.Add(colDay, "出勤日数");
                tempDGV.Columns.Add(colJitsurou, "実労働時間");
                tempDGV.Columns.Add(colShoteiDays, "所定日数");
                tempDGV.Columns.Add(colShoteiTime, "所定労働時間");
                tempDGV.Columns.Add(colOverTime, "超過時間");

                //tempDGV.Columns[colShokuinName].Frozen = true;

                // 各列幅指定
                tempDGV.Columns[colSzCode].Width = 90;
                tempDGV.Columns[colSzName].Width = 150;
                tempDGV.Columns[colShokuin].Width = 90;
                //tempDGV.Columns[colShokuinName].Width = 140;
                tempDGV.Columns[colShokuinName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tempDGV.Columns[colShainKbn].Width = 100;
                tempDGV.Columns[colDay].Width = 80;
                tempDGV.Columns[colJitsurou].Width = 100;
                tempDGV.Columns[colShoteiDays].Width = 80;
                tempDGV.Columns[colShoteiTime].Width = 110;
                tempDGV.Columns[colOverTime].Width = 100;

                // 表示位置
                tempDGV.Columns[colSzCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[colShokuin].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[colDay].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[colJitsurou].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[colShoteiDays].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[colShoteiTime].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[colOverTime].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // 追加行表示しない
                tempDGV.AllowUserToAddRows = false;

                // データグリッドビューから行削除を禁止する
                tempDGV.AllowUserToDeleteRows = false;

                // 手動による列移動の禁止
                tempDGV.AllowUserToOrderColumns = false;

                // 列サイズ変更禁止
                tempDGV.AllowUserToResizeColumns = true;

                // 行サイズ変更禁止
                tempDGV.AllowUserToResizeRows = false;

                // 行ヘッダーの自動調節
                //tempDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                // 編集可否
                tempDGV.ReadOnly = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///-----------------------------------------------------------------------------
        /// <summary>
        ///     グリッドビューへ社員情報を表示する </summary>
        /// <param name="g">
        ///     DataGridViewオブジェクト名</param>
        /// <param name="sYY">
        ///     年</param>
        /// <param name="sMM">
        ///     月</param>
        /// <param name="sSel">
        ///     対象社員区分</param>
        ///-----------------------------------------------------------------------------
        private void GridViewShowData(DataGridView g, int sYY, int sMM)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 対象年月の土日祝日日数を取得する
                int holDays = getHolDays(sYY, sMM);

                // 所定日数
                DateTime ddt = DateTime.Parse(sYY + "/" + sMM + "/01");
                int mDays = ddt.AddMonths(1).AddDays(-1).Day; // 対象年月の歴日数
                int shoDays = mDays - holDays;                

                // 所定労働時間（分）× ３ ÷ ４
                int shoTime = shoDays * 480 * 3 / 4;

                int sYYMM = sYY * 100 + sMM;

                wAdp.FillByDateSpan(dts.勤怠データ, sYYMM, sYYMM);

                // 臨時社員を対象とする
                sAdp.FillByShainkbn(dts.社員マスター, global.RINJISHAIN);

                g.Rows.Clear();
                int i = 0;

                foreach (var t in dts.勤怠データ.OrderBy(a => a.対象職員所属コード).ThenBy(a => a.対象職員コード))
                {
                    if (dts.社員マスター.Any(a => a.職員コード == t.対象職員コード))
                    {
                        var s = dts.社員マスター.Single(a => a.職員コード == t.対象職員コード);

                        if (s.短時間勤務 == global.flgOff)
                        {
                            // 短時間勤務者を対象とする
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    // 実労働時間が所定労働時間*3/4未満のとき読み飛ばす
                    if (t.実労働時間 < shoTime)
                    {
                        continue;
                    }
                    
                    // 画面表示
                    g.Rows.Add();
                    g[colSzCode, i].Value = t.対象職員所属コード;
                    g[colSzName, i].Value = t.対象職員所属名;
                    g[colShokuin, i].Value = t.対象職員コード;
                    g[colShokuinName, i].Value = t.対象職員名;
                    g[colShainKbn, i].Value = global.shainKbnArray[global.RINJISHAIN];
                    g[colDay, i].Value = t.普通出勤日数;
                    g[colJitsurou, i].Value = Utility.getHHMM(t.実労働時間);
                    g[colShoteiDays, i].Value = shoDays;
                    g[colShoteiTime, i].Value = Utility.getHHMM(shoTime);
                    g[colOverTime, i].Value = Utility.getHHMM(t.実労働時間 - shoTime);

                    i++;
                }

                // 対象者数表示
                if (i > 0)
                {
                    label10.Text = i.ToString("#,###") + " 件";
                    button1.Enabled = true;
                }
                else
                {
                    label10.Text = string.Empty;
                    button1.Enabled = false;
                }

                g.CurrentCell = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            //社員情報がないとき
            if (g.RowCount == 0)
            {
                MessageBox.Show("該当する社員が存在しませんでした", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private Boolean ErrCheck()
        {
            if (Utility.NumericCheck(txtYear.Text) == false)
            {
                MessageBox.Show("年は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            if (Utility.StrtoInt(txtYear.Text) < 2014)
            {
                MessageBox.Show("年が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            if (Utility.NumericCheck(txtMonth.Text) == false)
            {
                MessageBox.Show("月は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            if (int.Parse(txtMonth.Text) < 1 || int.Parse(txtMonth.Text) > 12)
            {
                MessageBox.Show("月が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            return true;
        }

        private void txtYear_Enter(object sender, EventArgs e)
        {
            TextBox txtObj = new TextBox();

            if (sender == txtYear) txtObj = txtYear;
            if (sender == txtMonth) txtObj = txtMonth;

            txtObj.SelectAll();
        }

        private void btnRtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("終了します。よろしいですか？", appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            this.Dispose();
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            string sDate = string.Empty;

            //エラーチェック
            if (!ErrCheck()) return;

            // 一覧表表示
            GridViewShowData(dg1, Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (!e.Control)
            //    {
            //        this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            //    }
            //}
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    e.Handled = true;
            //}
        }

        private void rBtnPrn_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void prePrint_Shown(object sender, EventArgs e)
        {
            txtYear.Focus();
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
                e.Handled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyLibrary.CsvOut.GridView(dg1, appName);
        }

        ///----------------------------------------------------------
        /// <summary>
        ///     休日配列を作成する </summary>
        ///----------------------------------------------------------
        private void holCsvToArray()
        {
            JAFA_OCRDataSetTableAdapters.環境設定TableAdapter cAdp = new JAFA_OCRDataSetTableAdapters.環境設定TableAdapter();

            cAdp.Fill(dts.環境設定);

            string cPath = string.Empty;

            foreach (var t in dts.環境設定.Where(a => a.ID == global.configKEY))
            {
                if (!t.Is祝日ＣＳＶデータパスNull())
                {
                    cPath = t.祝日ＣＳＶデータパス;
                }

                break;
            }

            if (cPath == string.Empty)
            {
                return;
            }

            // ファイルが存在しないとき
            if (!System.IO.File.Exists(cPath))
            {
                MessageBox.Show(cPath + " は存在しません。休日CSVファイルを再設定してください", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // 休日配列作成
            int iX = 0;
            foreach (var stBuffer in System.IO.File.ReadAllLines(cPath, Encoding.Default))
            {
                // カンマ区切りで分割して配列に格納する
                string[] stCSV = stBuffer.Split(',');

                if (stCSV.Length > 1)
                {
                    DateTime dt;

                    // 先頭項目が日付形式なら
                    if (DateTime.TryParse(stCSV[0], out dt))
                    {
                        // 休日配列に追加する
                        Array.Resize(ref holArray, iX + 1);
                        holArray[iX] = dt.ToShortDateString();
                        iX++;
                    }
                }
            }
        }

        ///-----------------------------------------------------------------
        /// <summary>
        ///     対象月の土日祝日日数を取得する </summary>
        /// <param name="yy">
        ///     年</param>
        /// <param name="mm">
        ///     月</param>
        /// <returns>
        ///     土日祝日日数</returns>
        ///-----------------------------------------------------------------
        private int getHolDays(int yy, int mm)
        {
            int sDays = 0;

            DateTime dt;

            string yymm = yy + "/" + mm + "/";

            for (int i = 1; i <= 31; i++)
            {
                if (DateTime.TryParse(yymm + i.ToString(), out dt))
                {
                    // 土・日か？
                    string wk = ("日月火水木金土").Substring(int.Parse(dt.DayOfWeek.ToString("d")), 1);

                    if (wk == "日" || wk == "土")
                    {
                        sDays++;
                    }
                    else
                    {
                        // 祝日か調べる
                        for (int iX = 0; iX < holArray.Length; iX++)
                        {
                            if (dt.ToShortDateString() == holArray[iX])
                            {
                                sDays++;
                                break;
                            }
                        }
                    }
                }
            }

            return sDays;
        }
    }
}
