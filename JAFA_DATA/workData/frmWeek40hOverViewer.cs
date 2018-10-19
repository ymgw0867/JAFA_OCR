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
    public partial class frmWeek40hOverViewer : Form
    {
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "週間勤務実績";    // アプリケーション表題

        public frmWeek40hOverViewer()
        {
            InitializeComponent();

            hAdp.Fill(dts.過去勤務票ヘッダ);
            mAdp.Fill(dts.過去勤務票明細);
            wAdp.Fill(dts.週実績);
            wmAdp.Fill(dts.週実績明細);
            mateAdp.Fill(dts.メイトマスター);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter hAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
        JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter mAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter();
        JAFA_OCRDataSetTableAdapters.週実績TableAdapter wAdp = new JAFA_OCRDataSetTableAdapters.週実績TableAdapter();
        JAFA_OCRDataSetTableAdapters.週実績明細TableAdapter wmAdp = new JAFA_OCRDataSetTableAdapters.週実績明細TableAdapter();
        JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter mateAdp = new JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter();

        int sWNum = 0;
        int eWNum = 0;

        #region グリッドカラム定義
        string colYear = "c0";
        string colMonth = "c39";   
        string colShokuin = "c1";   
        string colShokuinName = "c2";   
        string colSzCode = "c3";    
        string colSzName = "c4";    
        string colZan = "c5";     
        string col40h = "c6";     
        string colZangyo = "c7";            
        string colWork = "c8";    
        string colKyuyoZangyo = "c9";        
        string colYoubi = "c10"; 
        string colYYMMDD = "c11";  
        string colHankyu = "c12";   
        string colYukyu = "c13";    
        string colKekkin = "c14";   
        string colSonotaTotal = "c15";  
        string colKekkon = "c16";   
        string colKibiki = "c17";   
        string colSeiri = "c18";    
        string colKango = "c19";    
        string colKaigoKyuka = "c20";   
        string colRisai = "c21";    
        string colKakuri = "c22";   
        string colToku = "c23"; 
        string colKaigoKyushoku = "c24";    
        string colSanzensango = "c25";  
        string colIkujiKyushoku = "c26";    
        string colChikoku = "c27";  
        string colSoutai = "c28";   
        string colShiyouGaishutsu = "c29";  
        string colIkujitan = "c30"; 
        string colKaigotan = "c31";     
        string colKotsuhi = "c32";  
        string colNitto = "c33";    
        string colShukuhakuhi = "c34";  
        string colYouShukkin = "c35";   
        string colYukyuFuyo = "c36";    
        string colYukyuKurikoshi = "c37";
        string colYukyuFlg = "c38";
        string colDay = "c39";
        string colWeek = "c40";
        string colSt = "c41";
        string colEt = "c42";
        string colRes = "c43";
        string colJitsurou = "c44";
        string colMtotal = "c45"; 
        #endregion

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
            GridViewSetting2(dg2);

            // 年月表示
            txtYear.Text = (global.cnfYear + Properties.Settings.Default.rekiHosei).ToString();
            txtMonth.Text = global.cnfMonth.ToString();
            txtYear.Focus();

            label10.Text = string.Empty;

            button1.Enabled = false;
            button2.Enabled = false;
            linkLabel1.Visible = false;
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// データグリッドビューの定義を行います
        /// </summary>
        /// <param name="tempDGV">データグリッドビューオブジェクト</param>
        public void GridViewSetting(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更するe

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", (float)9.5, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", (float)9.5, FontStyle.Regular);
                
                // 行の高さ
                tempDGV.ColumnHeadersHeight = 21;
                tempDGV.RowTemplate.Height = 21;

                // 全体の高さ
                tempDGV.Height = 150;

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
                tempDGV.Columns.Add(colWeek, "週番号");
                tempDGV.Columns.Add(colWork, "勤務時間");
                tempDGV.Columns.Add(colZangyo, "残業時間");
                tempDGV.Columns.Add(col40h, "40H超時間");
                tempDGV.Columns.Add(colKyuyoZangyo, "給与残業");
                
                //tempDGV.Columns[colShokuinName].Frozen = true; 

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

                // 表示位置
                for (int i = 0; i < tempDGV.Columns.Count; i++)
                {
                    // Alignment
                    if (i == 0 || i == 2 || i == 4)
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    }
                    else if (i == 1 || i == 3)
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
                    }
                    else
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    }

                    // ソート機能制限
                    //tempDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // 各列幅指定
                tempDGV.Columns[colShokuinName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tempDGV.Columns[colSzCode].Width = 90;
                tempDGV.Columns[colSzName].Width = 160;
                tempDGV.Columns[colShokuin].Width = 90;
                tempDGV.Columns[colWeek].Width = 80;
                tempDGV.Columns[col40h].Width = 110;

                // 編集可否
                tempDGV.ReadOnly = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// データグリッドビューの定義を行います
        /// </summary>
        /// <param name="tempDGV">データグリッドビューオブジェクト</param>
        public void GridViewSetting2(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更するe

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", (float)9.5, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", (float)9.5, FontStyle.Regular);

                // 行の高さ
                tempDGV.ColumnHeadersHeight = 21;
                tempDGV.RowTemplate.Height = 21;

                // 全体の高さ
                tempDGV.Height = 446;

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
                tempDGV.Columns.Add(colYYMMDD, "日付");
                tempDGV.Columns.Add(colSt, "開始時刻");
                tempDGV.Columns.Add(colEt, "終了時刻");
                tempDGV.Columns.Add(colRes, "休憩時間");
                tempDGV.Columns.Add(colJitsurou, "勤務時間");
                tempDGV.Columns.Add(colZangyo, "残業時間");

                tempDGV.Columns[colSzCode].Visible = false;
                tempDGV.Columns[colSzName].Visible = false;
                tempDGV.Columns[colShokuin].Visible = false;
                tempDGV.Columns[colShokuinName].Visible = false;

                //tempDGV.Columns[colShokuinName].Frozen = true; 

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

                // 表示位置
                for (int i = 0; i < tempDGV.Columns.Count; i++)
                {
                    // Alignment
                    if (i == 4 || i == 5 || i == 6 || i == 7)
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    }
                    else
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    }

                    // ソート機能制限
                    //tempDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // 各列幅指定
                tempDGV.Columns[colYYMMDD].Width = 130;

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
        /// <param name="sNum">
        ///     社員番号</param>
        ///-----------------------------------------------------------------------------
        private void GridViewShowData(DataGridView g, int sYY, int sMM, int sCode, int cmbSel)
        {
            try
            {
                var s = dts.週実績.OrderBy(a => a.職員コード);

                // 社員番号指定のとき
                if (txtShainNum.Text != string.Empty)
                {
                    // 指定月の1日と月末日の週番号を求める
                    DateTime sDt;
                    DateTime eDt = DateTime.Today;

                    string cDt = sYY.ToString() + "/" + sMM.ToString() + "/01";
                    if (DateTime.TryParse(cDt, out sDt))
                    {
                        eDt = sDt.AddMonths(1).AddDays(-1);
                    }

                    // 指定月の1日の週番号を求める
                    foreach (var wm in dts.週実績明細.Where(a => a.年月日 == sDt && a.職員コード == sCode))
                    {
                        sWNum = wm.週番号;
                    }

                    // 指定月の月末日の週番号を求める
                    foreach (var wm in dts.週実績明細.Where(a => a.年月日 == eDt && a.職員コード == sCode))
                    {
                        eWNum = wm.週番号;
                    }

                    // 月すべて表示リンクボタン
                    linkLabel1.Visible = true;

                    // 情報がないとき
                    if (sWNum == 0 && eWNum == 0)
                    {
                        MessageBox.Show("該当期間の情報が存在しませんでした", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        linkLabel1.Visible = false;
                        return;
                    }
                }

                // コンボボックス選択
                if (cmbSel == 0)    // 全て
                {
                    // 社員番号指定
                    if (txtShainNum.Text != string.Empty)
                    {
                        s = dts.週実績.Where(a => a.週番号 >= sWNum && a.週番号 <= eWNum &&
                                                        a.年 == sYY && a.職員コード == sCode)
                                                    .OrderBy(a => a.週番号);
                    }
                    else
                    {
                        // 社員番号指定なし
                        s = dts.週実績.Where(a => a.年 == sYY && a.処理月 == sMM)
                                                    .OrderBy(a => a.職員コード)
                                                    .ThenBy(a => a.週番号);
                    }
                }
                else
                {
                    // 40H超えている週のみ
                    // 社員番号指定
                    if (txtShainNum.Text != string.Empty)
                    {
                        s = dts.週実績.Where(a => a.週番号 >= sWNum && a.週番号 <= eWNum &&
                                                 a.年 == sYY && a.職員コード == sCode && 
                                                 a._40H超時間 > 0)
                                                    .OrderBy(a => a.職員コード)
                                                    .ThenBy(a => a.週番号);
                    }
                    else
                    {
                        // 社員番号指定なし
                        s = dts.週実績.Where(a => a.年 == sYY && a.処理月 == sMM && a._40H超時間 > 0)
                                                    .OrderBy(a => a.職員コード)
                                                    .ThenBy(a => a.週番号);
                    }
                }

                g.Rows.Clear();
                int i = 0;

                //var s = dts.週実績.Where(a => a._40H超時間 > 0).OrderBy(a => a.職員コード).ThenBy(a => a.週番号);
                foreach (var m in s)
                {
                    // 社員情報を取得
                    string szCode = string.Empty;
                    string szName = string.Empty;
                    string sName = string.Empty;

                    foreach (var mate in dts.メイトマスター.Where(a => a.職員コード == m.職員コード))
                    {
                        szCode = mate.所属コード.ToString();
                        szName = mate.所属名;
                        sName = mate.氏名;
                    }

                    // 画面表示
                    g.Rows.Add();
                    g[colSzCode, i].Value = szCode;
                    g[colSzName, i].Value = szName;
                    g[colShokuin, i].Value = m.職員コード;
                    g[colShokuinName, i].Value = sName;
                    g[colWeek, i].Value = m.週番号.ToString();
                    g[colWork, i].Value = m.勤務時間.ToString("#,0");
                    g[colZangyo, i].Value = m.残業時間.ToString("#,0");
                    g[col40h, i].Value = m._40H超時間.ToString("#,0");
                    g[colKyuyoZangyo, i].Value = m.給与残業時間.ToString("#,0");

                    i++;
                }

                // 対象者数表示
                if (i > 0)
                {
                    button1.Enabled = true;
                    //linkLabel1.Visible = true;
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

            //社員情報がないとき
            if (g.RowCount == 0)
            {
                MessageBox.Show("該当する社員が存在しませんでした", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dg2.Rows.Clear();
                linkLabel1.Visible = false;
            }
        }

        ///-----------------------------------------------------------------------------
        /// <summary>
        ///     グリッドビューへ社員情報を表示する </summary>
        /// <param name="g">
        ///     DataGridViewオブジェクト名</param>
        /// <param name="sCode">
        ///     職員コード</param>
        /// <param name="sNum">
        ///     開始週番号</param>
        /// <param name="eNum">
        ///     終了週番号</param>
        ///-----------------------------------------------------------------------------
        private void GridViewShowMeisai(DataGridView g, int sCode,  int sNum, int eNum)
        {
            try
            {
                g.Rows.Clear();
                int i = 0;

                foreach (var t in dts.週実績明細.Where(a => a.職員コード == sCode && a.週番号 >= sNum && 
                                                           a.週番号 <= eNum && 
                                                           a.年月日.Year == Utility.StrtoInt(txtYear.Text)).OrderBy(a => a.年月日))
                {
                    string hdID = (t.年月日.Year - Properties.Settings.Default.rekiHosei).ToString() + 
                                  t.年月日.Month.ToString().PadLeft(2, '0') + 
                                  t.職員コード.ToString().PadLeft(5, '0');

                    foreach (var m in dts.過去勤務票明細.Where(a => a.ヘッダID == hdID && a.日付 == t.年月日.Day))
                    {
                        // 社員情報を取得
                        string szCode = string.Empty;
                        string szName = string.Empty;
                        string sName = string.Empty;

                        foreach (var mate in dts.メイトマスター.Where(a => a.職員コード == t.職員コード))
                        {
                            szCode = mate.所属コード.ToString();
                            szName = mate.所属名;
                            sName = mate.氏名;
                        }

                        // 画面表示
                        g.Rows.Add();
                        g[colSzCode, i].Value = szCode;
                        g[colSzName, i].Value = szName;
                        g[colShokuin, i].Value = t.職員コード;
                        g[colShokuinName, i].Value = sName;

                        g[colYYMMDD, i].Value = t.年月日.ToString("yyyy/MM/dd(ddd)");
                        //g[colYoubi, i].Value = ("日月火水木金土").Substring(int.Parse(t.年月日.DayOfWeek.ToString("d")), 1);
                        g[colSt, i].Value = m.開始時.ToString().PadLeft(1, '0') + ":" + m.開始分.ToString().PadLeft(2, '0');
                        g[colEt, i].Value = m.終了時.ToString().PadLeft(1, '0') + ":" + m.終了分.ToString().PadLeft(2, '0');
                        g[colRes, i].Value = m.休憩開始時.ToString().PadLeft(1, '0') + ":" + m.休憩開始分.ToString().PadLeft(2, '0');
                        g[colJitsurou, i].Value = t.勤務時間.ToString("#,0");
                        g[colZangyo, i].Value = t.残業時間.ToString("#,0");

                        i++;
                    }
                }

                // 対象者数表示
                if (i > 0)
                {
                    button2.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                }

                g.CurrentCell = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK);
            }

            // 情報がないとき
            if (g.RowCount == 0)
            {
                MessageBox.Show("該当する勤務データが存在しませんでした", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            //if (Utility.NumericCheck(txtShainNum.Text) == false)
            //{
            //    MessageBox.Show("社員番号は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txtShainNum.Focus();
            //    return false;
            //}

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
            if (MessageBox.Show("終了します。よろしいですか？",appName,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.No)
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
            
            //データ表示
            GridViewShowData(dg1, Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text), Utility.StrtoInt(txtShainNum.Text), comboBox1.SelectedIndex);
            dg2.Rows.Clear();
            button2.Enabled = false;
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

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int sCode = Utility.StrtoInt(dg1[colShokuin, e.RowIndex].Value.ToString());
            int wNum = Utility.StrtoInt(dg1[colWeek, e.RowIndex].Value.ToString());

            // 週実績明細表示
            GridViewShowMeisai(dg2, sCode, wNum, wNum);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 週実績明細表示
            if (txtShainNum.Text != string.Empty)
            {
                GridViewShowMeisai(dg2, Utility.StrtoInt(txtShainNum.Text), sWNum, eWNum);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyLibrary.CsvOut.GridView(dg2, "週明細");
        }
    }
}
