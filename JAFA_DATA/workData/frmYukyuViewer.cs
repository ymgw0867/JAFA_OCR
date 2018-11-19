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
//using LinqToExcel;
using ClosedXML.Excel;

namespace JAFA_DATA.workData
{
    public partial class frmYukyuViewer : Form
    {
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "有休取得一覧表";    // アプリケーション表題

        public frmYukyuViewer()
        {
            InitializeComponent();

            //adp.Fill(dts.勤怠データ);  // 2018/11/11 コメント化
            mAdp.Fill(dts.社員マスター);
            //fAdp.Fill(dts.有給休暇付与マスター);    // 2018/11/11 コメント化

            // 以下、2018/11/11 コメント化
            //// linqToExcel : excel過去１年間有給取得シート
            //if (System.IO.File.Exists(Properties.Settings.Default.exlMounthPath))
            //{
            //    // ターゲットのエクセルファイルが存在するとき
            //    var excel = new ExcelQueryFactory(Properties.Settings.Default.exlMounthPath);
            //    excel.ReadOnly = true;
            //    excel.AddMapping<exlMntData>(m => m.sCode, "職員コード");
            //    excel.AddMapping<exlMntData>(m => m.sName, "氏名");
            //    excel.AddMapping<exlMntData>(m => m.sYYMM, "年月");
            //    excel.AddMapping<exlMntData>(m => m.sYouDay, "要出勤日数");
            //    excel.AddMapping<exlMntData>(m => m.sKekkin, "欠勤");
            //    excel.AddMapping<exlMntData>(m => m.sDay, "有給休暇");
            //    excel.AddMapping<exlMntData>(m => m.sHan, "半休");
            //    excel.AddMapping<exlMntData>(m => m.sTotal, "合計");
            //    workSheet = excel.Worksheet<exlMntData>("sheet1");
            //}

            //// linqToExcel : excel前年有休付与日数シート
            //if (System.IO.File.Exists(Properties.Settings.Default.exlYukyuMstPath))
            //{
            //    // ターゲットのエクセルファイルが存在するとき
            //    var excelMst = new ExcelQueryFactory(Properties.Settings.Default.exlYukyuMstPath);
            //    excelMst.ReadOnly = true;
            //    excelMst.AddMapping<exlYukyuMst>(m => m.sCode, "職員コード");
            //    excelMst.AddMapping<exlYukyuMst>(m => m.sName, "氏名");
            //    excelMst.AddMapping<exlYukyuMst>(m => m.sYY, "年");
            //    excelMst.AddMapping<exlYukyuMst>(m => m.sMM, "月");
            //    excelMst.AddMapping<exlYukyuMst>(m => m.sFuyo, "当年付与日数");
            //    excelMst.AddMapping<exlYukyuMst>(m => m.sKurikoshi, "当年繰越日数");
            //    excelMst.AddMapping<exlYukyuMst>(m => m.sNensho, "当年初有給残");
            //    mstSheet = excelMst.Worksheet<exlYukyuMst>("sheet1");
            //}
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter adp = new JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter();
        JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter mAdp = new JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter();
        JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter fAdp = new JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter();
        
        // 2018/11/11 コメント化
        //LinqToExcel.Query.ExcelQueryable<exlMntData> workSheet = null;
        //LinqToExcel.Query.ExcelQueryable<exlYukyuMst> mstSheet = null;

        #region グリッドカラム定義
        string colYear = "c0";
        string colMonth = "c39";   
        string colShokuin = "c1";   
        string colShokuinName = "c2";   
        string colSzCode = "c3";    
        string colSzName = "c4";    
        string colZan = "c5";     
        string colSRT = "c6";     
        string colNenZan = "c7";            
        string colShinya = "c8";    
        string colHoutei = "c9";        
        string colKyujitsu = "c10"; 
        string colFurikyu = "c11";  
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

            // 年月表示
            txtYear.Text = global.cnfYear.ToString();
            txtMonth.Text = global.cnfMonth.ToString();
            txtYear.Focus();

            txtYear2.Text = global.cnfYear.ToString();
            txtMonth2.Text = global.cnfMonth.ToString();

            label10.Text = string.Empty;

            button1.Enabled = false;
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
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 11, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", (float)11, FontStyle.Regular);
                
                // 行の高さ
                tempDGV.ColumnHeadersHeight = 22;
                tempDGV.RowTemplate.Height = 22;

                // 全体の高さ
                tempDGV.Height = 574;

                // 奇数行の色
                //tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = false;

                // カラム定義
                tempDGV.Columns.Add(colYear, "年");
                tempDGV.Columns.Add(colMonth, "月");
                tempDGV.Columns.Add(colShokuin, "職員コード");
                tempDGV.Columns.Add(colShokuinName, "職員名");
                tempDGV.Columns.Add(colSzCode, "所属コード");
                tempDGV.Columns.Add(colSzName, "所属名");
                tempDGV.Columns.Add(colYukyu, "有休");
                tempDGV.Columns.Add(colHankyu, "半休");
                tempDGV.Columns.Add(colZan, "有休残");
                tempDGV.Columns.Add(colYukyuFuyo, "有給付与");
                tempDGV.Columns.Add(colYukyuKurikoshi, "有休繰越");
                tempDGV.Columns.Add(colNenZan, "年初残");
                tempDGV.Columns.Add(colSRT, "出勤率");
                tempDGV.Columns.Add(colYukyuFlg, "有休付与フラグ");
                
                tempDGV.Columns[colShokuinName].Frozen = true; 

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
                    if (i == 0 || i == 1 || i == 2 || i == 4)
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    }
                    else if (i == 3 || i == 5)
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
                tempDGV.Columns[colYear].Width = 60;
                tempDGV.Columns[colMonth].Width = 60;
                tempDGV.Columns[colYukyuFlg].Width = 130;
                tempDGV.Columns[colShokuinName].Width = 140;
                tempDGV.Columns[colSzCode].Width = 100;
                tempDGV.Columns[colSzName].Width = 200;
                tempDGV.Columns[colYukyu].Width = 70;
                tempDGV.Columns[colHankyu].Width = 70;

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
        private void GridViewShowData(DataGridView g, int sYY, int sMM, int eYY, int eMM)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //int sYYMM = Utility.StrtoInt(sYY.ToString() + sMM.ToString().PadLeft(2, '0'));
                //int eYYMM = Utility.StrtoInt(eYY.ToString() + eMM.ToString().PadLeft(2, '0'));

                int sYYMM = sYY * 100 + sMM;
                int eYYMM = eYY * 100 + eMM;

                adp.FillByDateSpan(dts.勤怠データ, sYYMM, eYYMM); // 2018/11/11
                var s = dts.勤怠データ.Where(a => a.対象月度 >= sYYMM && a.対象月度 <= eYYMM)
                                           .OrderBy(a => a.対象職員コード)
                                           .ThenBy(a => a.対象月度);
                
                // 社員番号
                if (txtShainNum.Text != string.Empty)
                {
                    s = s.Where(a => a.対象職員コード == Utility.StrtoInt(txtShainNum.Text))
                                           .OrderBy(a => a.対象職員コード)
                                           .ThenBy(a => a.対象月度);
                }
                                
                g.Rows.Clear();
                int i = 0;

                int shokuCode = 0;

                foreach (var t in s)
                {
                    g.Rows.Add();
                    g[colYear, i].Value = t.対象月度.ToString().Substring(0, 4);
                    g[colMonth, i].Value = t.対象月度.ToString().Substring(4, 2);
                    g[colShokuin, i].Value = t.対象職員コード.ToString();
                    g[colShokuinName, i].Value = t.対象職員名;
                    g[colSzCode, i].Value = t.対象職員所属コード.ToString();
                    g[colSzName, i].Value = t.対象職員所属名;
                    g[colYukyu, i].Value = t.有給休暇;
                    g[colHankyu, i].Value = t.有給半日;

                    double sZan = 0;
                    double sNissu = 0;
                    int sNen = 0;
                    int sTsuki = 0;
                    int yfMonth = 0;
                    double z = 0;       // 有給残

                    // 職員コードでブレーク時
                    if (shokuCode != t.対象職員コード)
                    {
                        if (dts.社員マスター.Any(a => a.職員コード == Utility.StrtoInt(t.対象職員コード.ToString())))
                        {
                            var ss = dts.社員マスター.Single(a => a.職員コード == Utility.StrtoInt(t.対象職員コード.ToString()));
                            yfMonth = ss.有給付与月;
                        }

                        // 当年初有休残日数取得
                        sZan = getNenshozan(t.対象職員コード.ToString(), t.対象月度, yfMonth, out sNen, out sTsuki);

                        // 当月が有給付与月以外のとき
                        if (t.対象月度.ToString().Substring(4, 2) != yfMonth.ToString())
                        {
                            // 当年初～表示前月までの消化日数を取得
                            int sSNenTsuki = sNen * 100 + sTsuki;
                            int sENenTsuki = t.対象月度 - 1;
                            if ((sENenTsuki % 100) == 0)
                            {
                                int y = (sENenTsuki / 100) - 1;
                                sENenTsuki = y * 100 + 12;
                            }

                            // 年初から前月までの消化日数
                            sNissu = getShoukaNissu(t.対象職員コード, sSNenTsuki, sENenTsuki);
                        }

                        // 有給残
                        z = sZan - sNissu - (double)t.有給休暇 - t.有給半日;
                    }
                    else
                    {
                        double nz = 0;

                        // 有給残
                        if (Utility.StrtoDouble(g[colNenZan, i - 1].Value.ToString()) != 0)
                        {
                            nz = Utility.StrtoDouble(g[colNenZan, i - 1].Value.ToString());
                        }
                        else
                        {
                            nz = Utility.StrtoDouble(g[colZan, i - 1].Value.ToString());
                        }

                        z = nz - (double)t.有給休暇 - t.有給半日;
                    }

                    // 有給残
                    g[colZan, i].Value = z.ToString("#0.0");

                    //g[colZan, i].Value = xxxxx;
                    g[colYukyuFuyo, i].Value = t.有休付与日数.ToString("#0");
                    g[colYukyuKurikoshi, i].Value = t.有休繰越日数.ToString("#0.0");

                    decimal yuTotal = (decimal)t.有休付与日数 + (decimal)t.有休繰越日数;
                    g[colNenZan, i].Value = yuTotal.ToString("#0.0");                    
                    g[colSRT, i].Value = string.Empty;

                    if (t.有休付与対象フラグ == global.flgOn)
                    {
                        // 出勤率を取得
                        int yy = Utility.StrtoInt(t.対象月度.ToString().Substring(0, 4));
                        int mm = Utility.StrtoInt(t.対象月度.ToString().Substring(4, 2)) + 1;

                        if (mm > 12)
                        {
                            yy++;
                            mm -= 12;
                        }

                        fAdp.FillBySCodeYYMM(dts.有給休暇付与マスター, t.対象職員コード, yy, mm);  // 2018/11/11
                        var fu = dts.有給休暇付与マスター.Where(a => a.社員番号 == t.対象職員コード &&
                                                              a.年 == yy && a.月 == mm);
                        foreach (var item in fu)
                        {
                            g[colSRT, i].Value = item.出勤率.ToString("n1");
                        }
                    }
                    
                    g[colYukyuFlg, i].Value = t.有休付与対象フラグ;

                    // 職員コード
                    shokuCode = t.対象職員コード;

                    i++;
                }

                // 対象者数表示
                if (i > 0)
                {
                    label10.Text = "対象者：" + i.ToString("#,###") + " 名";
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

        /// ----------------------------------------------------------------------
        /// <summary>
        ///     当年初の有給休暇残日数を取得する </summary>
        /// <param name="sCode">
        ///     職員コード</param>
        /// <param name="sYYMM">
        ///     年月</param>
        /// <returns>
        ///     当年初有給残日数</returns>
        /// ----------------------------------------------------------------------
        private double getNenshozan(string sCode, int sYYMM, int sFyMonth, out int sNen, out int sTsuki)
        {
            double zan = 0;
            bool sFms = false;

            sNen = 0;
            sTsuki = 0;

            fAdp.FillBySCode(dts.有給休暇付与マスター, Utility.StrtoInt(sCode)); // 2018/11/11
            foreach (var t in dts.有給休暇付与マスター
                .Where(a => (a.年 * 100 + a.月) <= sYYMM)
                .OrderByDescending(a => (a.年 * 100 + a.月)))
            {
                zan = t.当年初有給残日数;
                sNen = t.年;
                sTsuki = t.月;
                sFms = true;
                break;
            }

            // 有給休暇付与マスターが存在しなかったらExcelシートを読む
            if (!sFms)
            {
                //// 有給休暇付与マスターExcelシートが存在するとき
                //if (mstSheet != null)
                //{
                //    // 当年
                //    int sYear = sYYMM / 100;    // 月
                //    int sMonth = sYYMM % 100;   // 年

                //    // 前回の有給付与年
                //    if (sMonth < sFyMonth)
                //    {
                //        sYear --;
                //    }

                //    // 有給休暇付与Excelシートより前年初有給残日数（当年初有給残日数）を求めます
                //    foreach (var x in mstSheet.Where(a => a.sCode == sCode && a.sYY == sYear.ToString() && a.sMM == sFyMonth.ToString()))
                //    {
                //        zan = Utility.StrtoDouble(x.sNensho);
                //        sNen = Utility.StrtoInt(x.sYY);
                //        sTsuki = Utility.StrtoInt(x.sMM); 
                //        break;
                //    }
                //}
                

                // 有給付与マスター.xlsxシートより前年初有給残日数（当年初有給残日数）を求めます : closedxml　2018/11/11
                if (System.IO.File.Exists(Properties.Settings.Default.exlYukyuMstPath))
                {
                    // 当年
                    int sYear = sYYMM / 100;    // 月
                    int sMonth = sYYMM % 100;   // 年

                    // 前回の有給付与年
                    if (sMonth < sFyMonth)
                    {
                        sYear--;
                    }

                    using (var book = new XLWorkbook(Properties.Settings.Default.exlYukyuMstPath, XLEventTracking.Disabled))
                    {
                        var sheet1 = book.Worksheet(1);
                        var tbl = sheet1.RangeUsed().AsTable();

                        foreach (var t in tbl.DataRange.Rows())
                        {
                            if (Utility.StrtoInt(sCode) == Utility.StrtoInt(Utility.NulltoStr(t.Cell(1).Value)))
                            {
                                // ループで最後の情報を取得(一番最近の付与情報）: 2018/11/19
                                zan = Utility.StrtoDouble(Utility.NulltoStr(t.Cell(7).Value));
                                sNen = Utility.StrtoInt(Utility.NulltoStr(t.Cell(3).Value));
                                sTsuki = Utility.StrtoInt(Utility.NulltoStr(t.Cell(4).Value));
                            }
                        }

                        sheet1.Dispose();
                    }
                }
            }
            
            return zan;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     任意の社員の任意期間の有休・半休消化日数を求める </summary>
        /// <param name="sCode">
        ///     職員コード</param>
        /// <param name="sYYMM">
        ///     開始年月</param>
        /// <param name="eYYMM">
        ///     終了年月</param>
        /// <returns>
        ///     消化日数</returns>
        /// -----------------------------------------------------------------------------
        private double getShoukaNissu(int sCode, int sYYMM, int eYYMM)
        {
            double sNissu = 0;
            bool sFms = false;

            foreach (var t in dts.勤怠データ.Where(a => a.対象職員コード == sCode))
            {
                if (t.対象月度 >= sYYMM && t.対象月度 <= eYYMM)
                {
                    sNissu += (double)(t.有給休暇 + t.有給半日);
                    sFms = true;
                }
            }

            //// 勤怠データが存在しなかったらExcelシートを読む
            //if (!sFms)
            //{
                // 以下、コメント化 2018/11/11 
                //// Excel過去１年間有給取得ファイルが存在するとき
                //if (workSheet != null)
                //{
                //    // Excel過去１年間有給取得シートから日数を取得する
                //    foreach (var t in workSheet.Where(a => a.sCode == sCode.Substring(3, 5)))
                //    {
                //        if (Utility.StrtoInt(t.sYYMM) >= sYYMM && Utility.StrtoInt(t.sYYMM) <= eYYMM)
                //        {                        
                //            sNissu += Utility.StrtoDouble(t.sTotal);   // 有休＋半休
                //        }
                //    }
                //}
            //}



            // 月別有休日数.xlsxファイルが存在するとき : 2018/11/11
            if (System.IO.File.Exists(Properties.Settings.Default.exlMounthPath))
            {
                // 月別有休日数.xlsxシートより対象期間の日数を取得する : closedxml　2018/11/09
                using (var book = new XLWorkbook(Properties.Settings.Default.exlMounthPath, XLEventTracking.Disabled))
                {
                    var sheet1 = book.Worksheet(1);
                    var tbl = sheet1.RangeUsed().AsTable();

                    foreach (var t in tbl.DataRange.Rows())
                    {
                        if (sCode == Utility.StrtoInt(Utility.NulltoStr(t.Cell(1).Value)) &&
                            (Utility.StrtoInt(Utility.NulltoStr(t.Cell(3).Value))) >= sYYMM &&
                            (Utility.StrtoInt(Utility.NulltoStr(t.Cell(3).Value))) <= eYYMM)
                        {
                            sNissu += Utility.StrtoDouble(Utility.NulltoStr(t.Cell(8).Value)); // 有休＋半休
                        }
                    }

                    sheet1.Dispose();
                }
            }
            
            return sNissu;
        }

        private Boolean ErrCheck()
        {
            if (Utility.NumericCheck(txtYear.Text) == false)
            {
                MessageBox.Show("年は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            //if (Utility.StrtoInt(txtYear.Text) < 2014)
            //{
            //    MessageBox.Show("年が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txtYear.Focus();
            //    return false;
            //}

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

            if (Utility.NumericCheck(txtYear2.Text) == false)
            {
                MessageBox.Show("年は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear2.Focus();
                return false;
            }

            if (Utility.StrtoInt(txtYear2.Text) < 2014)
            {
                MessageBox.Show("年が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear2.Focus();
                return false;
            }

            if (Utility.NumericCheck(txtMonth2.Text) == false)
            {
                MessageBox.Show("月は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth2.Focus();
                return false;
            }

            if (int.Parse(txtMonth2.Text) < 1 || int.Parse(txtMonth2.Text) > 12)
            {
                MessageBox.Show("月が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth2.Focus();
                return false;
            }

            int sym = Utility.StrtoInt(txtYear.Text) * 100 + int.Parse(txtMonth.Text);
            int eym = Utility.StrtoInt(txtYear2.Text) * 100 + int.Parse(txtMonth2.Text);

            if (sym > eym)
            {
                MessageBox.Show("年月範囲が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear2.Focus();
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
            GridViewShowData(dg1, Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text), Utility.StrtoInt(txtYear2.Text), Utility.StrtoInt(txtMonth2.Text));
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
            MyLibrary.CsvOut.GridView(dg1, "有給取得一覧表");
        }
    }
}
