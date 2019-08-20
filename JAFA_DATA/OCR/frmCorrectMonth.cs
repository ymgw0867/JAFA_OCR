using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using JAFA_DATA.Common;
using OpenCvSharp;
using ClosedXML.Excel;

namespace JAFA_DATA.OCR
{
    public partial class frmCorrectMonth : Form
    {
        /// ------------------------------------------------------------
        /// <summary>
        ///     コンストラクタ </summary>
        /// <param name="sID">
        ///     処理モード</param>
        /// ------------------------------------------------------------
        public frmCorrectMonth()
        {
            InitializeComponent();

            // 画像パス取得
            global.pblImagePath = Properties.Settings.Default.tifPath;
            
            // テーブルアダプターマネージャーに確定勤務票ヘッダ、明細テーブルアダプターを割り付ける
            adpMn.確定勤務票ヘッダTableAdapter = hAdp;
            adpMn.確定勤務票明細TableAdapter = iAdp;

            // 出勤区分マスター
            sAdp.Fill(dts.出勤区分);
        }
        
        // openCvSharp 関連　2018/10/26
        const float B_WIDTH = 0.35f;
        const float B_HEIGHT = 0.35f;
        const float A_WIDTH = 0.05f;
        const float A_HEIGHT = 0.05f;

        float n_width = 0f;
        float n_height = 0f;

        Mat mMat = new Mat();

        // データアダプターオブジェクト
        JAFA_OCRDataSetTableAdapters.TableAdapterManager adpMn = new JAFA_OCRDataSetTableAdapters.TableAdapterManager();
        JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter hAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter();
        JAFA_OCRDataSetTableAdapters.確定勤務票明細TableAdapter iAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票明細TableAdapter();
        JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter sAdp = new JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter();

        // データセットオブジェクト
        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        
        /// <summary>
        ///     カレントデータRowsインデックス</summary>
        int cI = 0;

        // 社員マスターより取得した所属コード
        string mSzCode = string.Empty;

        #region 終了ステータス定数
        const string END_BUTTON = "btn";
        const string END_MAKEDATA = "data";
        const string END_CONTOROL = "close";
        const string END_NODATA = "non Data";
        #endregion

        string sDBNM = string.Empty;                // データベース名

        string _PCADBName = string.Empty;           // 会社領域データベース識別番号
        string _PCAComNo = string.Empty;            // 会社番号
        string _PCAComName = string.Empty;          // 会社名

        // dataGridView1_CellEnterステータス
        bool gridViewCellEnterStatus = true;
        
        clsYukyuFuyo[] yArray = null;
        clsKintaiXls[] kArray = null;

        private void frmCorrect_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // フォーム最大値
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小値
            Utility.WindowsMinSize(this, this.Width, this.Height);
            
            // データセットへデータを読み込みます
            adpMn.確定勤務票ヘッダTableAdapter.Fill(dts.確定勤務票ヘッダ);
            adpMn.確定勤務票明細TableAdapter.Fill(dts.確定勤務票明細);

            // データテーブル件数カウント
            if (dts.確定勤務票ヘッダ.Count == 0)
            {
                MessageBox.Show("対象となる確定データがありません", "確定出勤簿データ登録", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //終了処理
                Environment.Exit(0);
            }
            
            // キャプション
            this.Text = "確定出勤簿データ表示";

            // グリッドビュー定義
            GridviewSet gs = new GridviewSet();
            gs.Setting_Shain(dGV);
            
            // 編集作業、過去データ表示の判断
            // 最初のレコードを表示
            cI = 0;
            showOcrData(cI);
            
            // tagを初期化
            this.Tag = string.Empty;

            // 2018/11/20
            putYukyuFuyoArray();

            // 2018/11/20
            putKintaiArray();
        }

        #region データグリッドビューカラム定義
        private static string cDay = "col1";        // 日付
        private static string cWeek = "col2";       // 曜日
        private static string cKintai1 = "col3";    // 勤怠記号
        private static string cKName = "col22";     // 出勤区分名称
        private static string cSH = "col4";         // 開始時
        private static string cSE = "col5";
        private static string cSM = "col6";         // 開始分
        private static string cEH = "col7";         // 終了時
        private static string cEE = "col8";
        private static string cEM = "col9";         // 終了分
        private static string cKSH = "col10";       // 休憩開始時
        private static string cKSE = "col11";
        private static string cKSM = "col12";       // 休憩開始分
        private static string cWH = "col16";        // 実労働時間時
        private static string cWE = "col17";
        private static string cWM = "col18";        // 実労働時間分
        private static string cTeisei = "col21";    // 訂正
        private static string cID = "colID";
        private static string cImg = "colImg";      // 画像名
        #endregion




        private void putYukyuFuyoArray()
        {
            int cnt = 0;

            JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter yAdp = new JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter();
            yAdp.Fill(dts.有給休暇付与マスター);

            // 有給休暇付与マスターより前回の年初有給残日数（当年初有給残日数）を求めます
            //foreach (var z in dts.有給休暇付与マスター.Where(a => (a.年 * 100 + a.月) != (global.cnfYear * 100 + global.cnfMonth)))   // 2019/05/09 コメント化

            // 2019/05/09 対象年月までの付与実績を含む
            foreach (var z in dts.有給休暇付与マスター
                    .Where(a => (a.年 * 100 + a.月) <= (global.cnfYear * 100 + global.cnfMonth))
                    .OrderBy(a => a.年).ThenBy(a => a.月))
            {
                Array.Resize(ref yArray, cnt + 1);

                yArray[cnt] = new clsYukyuFuyo();
                yArray[cnt].sCode = z.社員番号;
                yArray[cnt].sYear = z.年;
                yArray[cnt].sMonth = z.月;
                yArray[cnt].sNensho = (decimal)z.当年初有給残日数;
                yArray[cnt].sFuyo = (decimal)z.当年付与日数;

                cnt++;
            }

            // 有給付与マスター.xlsxシートより前回の年初有給残日数（当年初有給残日数）を求めます : closedxml　2018/11/09
            using (var book = new XLWorkbook(Properties.Settings.Default.exlYukyuMstPath, XLEventTracking.Disabled))
            {
                var sheet1 = book.Worksheet(1);
                var tbl = sheet1.RangeUsed().AsTable();

                foreach (var t in tbl.DataRange.Rows())
                {
                    Array.Resize(ref yArray, cnt + 1);

                    yArray[cnt] = new clsYukyuFuyo();
                    yArray[cnt].sCode = Utility.StrtoInt(Utility.NulltoStr(t.Cell(1).Value));
                    yArray[cnt].sYear = Utility.StrtoInt(Utility.NulltoStr(t.Cell(3).Value));
                    yArray[cnt].sMonth = Utility.StrtoInt(Utility.NulltoStr(t.Cell(4).Value));
                    yArray[cnt].sNensho = (decimal)Utility.StrtoDouble(Utility.NulltoStr(t.Cell(7).Value));
                    yArray[cnt].sFuyo = Utility.StrtoInt(Utility.NulltoStr(t.Cell(5).Value));

                    cnt++;
                }

                sheet1.Dispose();
            }
        }


        private void putKintaiArray()
        {
            int cnt = 0;

            // 月別有給日数.xlsxシート
            using (var book = new XLWorkbook(Properties.Settings.Default.exlMounthPath, XLEventTracking.Disabled))
            {
                var sheet1 = book.Worksheet(1);
                var tbl = sheet1.RangeUsed().AsTable();

                foreach (var t in tbl.DataRange.Rows())
                {
                    Array.Resize(ref kArray, cnt + 1);

                    kArray[cnt] = new clsKintaiXls();
                    kArray[cnt].sCode = Utility.StrtoInt(Utility.NulltoStr(t.Cell(1).Value));
                    kArray[cnt].sYYMM = Utility.StrtoInt(Utility.NulltoStr(t.Cell(3).Value));
                    kArray[cnt].sYoushukkin = Utility.StrtoInt(Utility.NulltoStr(t.Cell(4).Value));
                    kArray[cnt].sKekkin = Utility.StrtoInt(Utility.NulltoStr(t.Cell(5).Value));
                    kArray[cnt].sYuckyu = (decimal)Utility.StrtoDouble(Utility.NulltoStr(t.Cell(8).Value));

                    cnt++;
                }

                sheet1.Dispose();
            }
        }




        ///----------------------------------------------------------------------------
        /// <summary>
        ///     データグリッドビュークラス </summary>
        ///----------------------------------------------------------------------------
        private class GridviewSet
        {
            ///----------------------------------------------------------------------------
            /// <summary>
            ///     データグリッドビューの定義を行います</summary> 
            /// <param name="gv">
            ///     データグリッドビューオブジェクト</param>
            ///----------------------------------------------------------------------------
            public void Setting_Shain(DataGridView gv)
            {
                try
                {
                    // データグリッドビューの基本設定
                    setGridView_Properties(gv);

                    // Tagをセット
                    //gv.Tag = global.SHAIN_ID;

                    // カラムコレクションを空にします
                    gv.Columns.Clear();

                    // 行数をクリア            
                    gv.Rows.Clear();                                       

                    //各列幅指定
                    gv.Columns.Add(cDay, "日");
                    gv.Columns.Add(cWeek, "曜");
                    gv.Columns.Add(cKintai1, "区");
                    gv.Columns.Add(cKName, "出勤区分");
                    gv.Columns.Add(cSH, "始");
                    gv.Columns.Add(cSE, "");
                    gv.Columns.Add(cSM, "業");
                    gv.Columns.Add(cEH, "終");
                    gv.Columns.Add(cEE, "");
                    gv.Columns.Add(cEM, "業");
                    gv.Columns.Add(cKSH, "休");
                    gv.Columns.Add(cKSE, "");
                    gv.Columns.Add(cKSM, "憩");
                    gv.Columns.Add(cWH, "実");
                    gv.Columns.Add(cWE, "");
                    gv.Columns.Add(cWM, "労");

                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    gv.Columns.Add(column);
                    gv.Columns[16].Name = cTeisei;
                    gv.Columns[16].HeaderText = "訂正";

                    gv.Columns.Add(cID, "");   // 明細ID
                    gv.Columns[cID].Visible = false;

                    gv.Columns.Add(cImg, "");   // 画像名
                    gv.Columns[cImg].Visible = false;

                    foreach (DataGridViewColumn c in gv.Columns)
                    {
                        // 幅                       
                        if (c.Name == cSE || c.Name == cEE || c.Name == cKSE || c.Name == cWE)
                        {
                            c.Width = 10;
                        }
                        else if (c.Name == cKintai1)
                        {
                            c.Width = 30;
                        }
                        else if (c.Name == cTeisei)
                        {
                            c.Width = 40;
                        }
                        else
                        {
                            c.Width = 30;
                        }

                        gv.Columns[cKName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        
                        // 表示位置
                        if (c.Index < 3 || c.Name == cSE || c.Name == cEE || c.Name == cKSE ||
                            c.Name == cWE || c.Name == cTeisei)
                        {
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                        }
                        else
                        {
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        }

                        if (c.Name == cSH || c.Name == cEH || c.Name == cKSH || c.Name == cWH)
                        {
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        }

                        if (c.Name == cKName || c.Name == cSM || c.Name == cEM || c.Name == cKSM ||
                            c.Name == cWM)
                        {
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
                        }

                        // 編集可否
                        // 実労時間を編集不可とした 2018/03/27
                        if (c.Index < 2 || c.Name == cKName || c.Name == cSE || c.Name == cEE ||
                            c.Name == cKSE || c.Name == cWE || c.Name == cWH || c.Name == cWM)
                        {
                            c.ReadOnly = true;
                        }
                        else
                        {
                            c.ReadOnly = false;
                        }

                        // 区切り文字
                        if (c.Name == cSE || c.Name == cEE || c.Name == cKSE || c.Name == cWE)
                        {
                            c.DefaultCellStyle.Font = new Font("ＭＳＰゴシック", 8, FontStyle.Regular);
                        }

                        // 入力可能桁数
                        if (c.Name == cKSH)
                        {
                            DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)c;
                            col.MaxInputLength = 1;
                        }
                        else if (c.Name != cTeisei)
                        {
                            DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)c;
                            col.MaxInputLength = 2;
                        }

                        // ソート禁止
                        c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ///----------------------------------------------------------------------------
            /// <summary>
            ///     データグリッドビュー基本設定</summary>
            /// <param name="gv">
            ///     データグリッドビューオブジェクト</param>
            ///----------------------------------------------------------------------------
            private void setGridView_Properties(DataGridView gv)
            {
                // 列スタイルを変更する
                gv.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                gv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                gv.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

                // データフォント指定
                gv.DefaultCellStyle.Font = new Font("Meiryo UI", (Single)9, FontStyle.Regular);
                //gv.DefaultCellStyle.Font = new Font("ＭＳＰゴシック", (Single)10, FontStyle.Regular);

                // 行の高さ
                gv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                gv.ColumnHeadersHeight = 18;
                gv.RowTemplate.Height = 18;

                // 全体の高さ
                gv.Height = 578;

                // 全体の幅
                gv.Width = 493;

                // 奇数行の色
                //gv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

                //テキストカラーの設定
                gv.RowsDefaultCellStyle.ForeColor = Color.Navy;       
                gv.DefaultCellStyle.SelectionBackColor = Color.Empty;
                gv.DefaultCellStyle.SelectionForeColor = Color.Navy;

                // 行ヘッダを表示しない
                gv.RowHeadersVisible = false;

                // 選択モード
                gv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                gv.MultiSelect = false;

                // データグリッドビュー編集可
                gv.ReadOnly = false;

                // 追加行表示しない
                gv.AllowUserToAddRows = false;

                // データグリッドビューから行削除を禁止する
                gv.AllowUserToDeleteRows = false;

                // 手動による列移動の禁止
                gv.AllowUserToOrderColumns = false;

                // 列サイズ変更不可
                gv.AllowUserToResizeColumns = false;

                // 行サイズ変更禁止
                gv.AllowUserToResizeRows = false;

                // 行ヘッダーの自動調節
                //gv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                //TAB動作
                gv.StandardTab = false;

                // 編集モード
                gv.EditMode = DataGridViewEditMode.EditOnEnter;
            }
        }

        private void txtNo_TextChanged(object sender, EventArgs e)
        {
            // チェンジバリューステータス
            if (!global.ChangeValueStatus) return;

            // 社員番号のとき
            lblName.Text = string.Empty;

            if (txtNo.Text != string.Empty)
            {
                string[] sName = new string[6];

                clsGetMst ms = new clsGetMst();
                sName = ms.getKojinMst(Utility.StrtoInt(txtNo.Text));
                lblName.Text = sName[0];
                lblFuri.Text = sName[1];
                //lblSyoubi.Text = sName[4];
                lblWdays.Text = sName[5];

                // 社員区分 2018/11/08
                if (sName[6] != global.NOT_FOUND)
                {
                    lblShainkbn.Text = sName[6];
                    lblShainKbnName.Text = global.shainKbnArray[Utility.StrtoInt(sName[6])];
                }
                else
                {
                    lblShainkbn.Text = string.Empty;
                    lblShainKbnName.Text = string.Empty;
                }

                // 農業従事区分 2018/11/07
                if (sName[7] != global.NOT_FOUND)
                {
                    if (sName[7] == global.FLGON)
                    {
                        lblNougyou.Text = "農業従事";
                    }
                    else
                    {
                        lblNougyou.Text = "農業非従事";
                    }
                }
                else
                {
                    lblNougyou.Text = string.Empty;
                    lblNougyou.Text = string.Empty;
                }
            }
        }


        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                //イベントハンドラが複数回追加されてしまうので最初に削除する
                e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                //e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress2);

                //イベントハンドラを追加する
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            }
        }

        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '\t')
                e.Handled = true;
        }

        void Control_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
                e.KeyChar == '\b' || e.KeyChar == '\t')
                e.Handled = false;
            else e.Handled = true;
        }

        /// -------------------------------------------------------------------
        /// <summary>
        ///     曜日をセットする </summary>
        /// <param name="tempRow">
        ///     MultiRowのindex</param>
        /// -------------------------------------------------------------------
        private void YoubiSet(int tempRow)
        {
            string sDate;
            DateTime eDate;
            Boolean bYear = false;
            Boolean bMonth = false;

            //年月を確認
            if (txtYear.Text != string.Empty)
            {
                if (Utility.NumericCheck(txtYear.Text))
                {
                    if (int.Parse(txtYear.Text) > 0)
                    {
                        bYear = true;
                    }
                }
            }

            if (txtMonth.Text != string.Empty)
            {
                if (Utility.NumericCheck(txtMonth.Text))
                {
                    if (int.Parse(txtMonth.Text) >= 1 && int.Parse(txtMonth.Text) <= 12)
                    {
                        for (int i = 0; i < global._MonthMULTIGYO; i++)
                        {
                            bMonth = true;
                        }
                    }
                }
            }

            //年月の値がfalseのときは曜日セットは行わずに終了する
            if (bYear == false || bMonth == false) return;

            //行の色を初期化
            dGV.Rows[tempRow].DefaultCellStyle.BackColor = Color.Empty;

            //Nullか？
            dGV[cWeek, tempRow].Value = string.Empty;
            if (dGV[cDay, tempRow].Value != null) 
            {
                if (dGV[cDay, tempRow].Value.ToString() != string.Empty)
                {
                    if (Utility.NumericCheck(dGV[cDay, tempRow].Value.ToString()))
                    {
                        {
                            sDate = Utility.StrtoInt(txtYear.Text) + "/" + 
                                    Utility.EmptytoZero(txtMonth.Text) + "/" +
                                    Utility.EmptytoZero(dGV[cDay, tempRow].Value.ToString());
                            
                            // 存在する日付と認識された場合、曜日を表示する
                            if (DateTime.TryParse(sDate, out eDate))
                            {
                                dGV[cWeek, tempRow].Value = ("日月火水木金土").Substring(int.Parse(eDate.DayOfWeek.ToString("d")), 1);

                                // 休日背景色設定・日曜日
                                if (dGV[cWeek, tempRow].Value.ToString() == "日")
                                    dGV.Rows[tempRow].DefaultCellStyle.BackColor = Color.MistyRose;

                                // 時刻区切り文字
                                dGV[cSE, tempRow].Value = ":";
                                dGV[cEE, tempRow].Value = ":";
                                dGV[cKSE, tempRow].Value = ":";
                                dGV[cWE, tempRow].Value = ":";
                            }
                        }
                    }
                }
             }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!global.ChangeValueStatus) return;

            if (e.RowIndex < 0) return;

            string colName = dGV.Columns[e.ColumnIndex].Name;

            // 日付
            if (colName == cDay)
            {
                // 曜日を表示します
                YoubiSet(e.RowIndex);
            }
            
            // 出勤日数
            //txtShukkinTl.Text = getWorkDays(_YakushokuType);

            //// 休日チェック
            //if (colName == cKyuka || colName == cCheck)
            //{
            //    // 休日行背景色設定
            //    if (dGV[cKyuka, e.RowIndex].Value.ToString() == "True")
            //        dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
            //    else dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
            //}

            // 出勤区分：出勤区分名称を表示する
            if (colName == cKintai1)
            {
                global.ChangeValueStatus = false;

                if (dGV[cKintai1, e.RowIndex].Value != null)
                {
                    if (dts.出勤区分.Any(a => a.ID == Utility.StrtoInt(dGV[cKintai1, e.RowIndex].Value.ToString())))
                    {
                        JAFA_OCRDataSet.出勤区分Row r = dts.出勤区分.Single(a => a.ID == Utility.StrtoInt(dGV[cKintai1, e.RowIndex].Value.ToString()));
                        dGV[cKName, e.RowIndex].Value = r.名称;
                    }
                    else
                    {
                        dGV[cKName, e.RowIndex].Value = string.Empty;
                    }
                }
                else
                {
                    dGV[cKName, e.RowIndex].Value = string.Empty;
                }

                global.ChangeValueStatus = true;
            }

            double wTime = 0;   // 2019/03/14
            OCRData ocr = new OCRData();    // 2019/03/14

            // 出勤時刻、退勤時刻
            if (colName == cSH || colName == cSM || colName == cEH || colName == cEM || 
                colName == cKSH || colName == cKSM)
            {
                // 実働時間を計算して表示する
                if (dGV[cSH, e.RowIndex].Value != null && dGV[cSM, e.RowIndex].Value != null &&
                    dGV[cEH, e.RowIndex].Value != null && dGV[cEM, e.RowIndex].Value != null)
                {
                    if (dGV[cSH, e.RowIndex].Value.ToString() != string.Empty &&
                        dGV[cSM, e.RowIndex].Value.ToString() != string.Empty &&
                        dGV[cEH, e.RowIndex].Value.ToString() != string.Empty &&
                        dGV[cEM, e.RowIndex].Value.ToString() != string.Empty)
                    {
                        // 出勤時刻、退勤時刻、休憩時間から実働時間を取得する
                        //OCRData ocr = new OCRData(); // 2019/03/14
                        wTime = ocr.getWorkTime(dGV[cSH, e.RowIndex].Value.ToString(),
                            dGV[cSM, e.RowIndex].Value.ToString(), 
                            dGV[cEH, e.RowIndex].Value.ToString(), 
                            dGV[cEM, e.RowIndex].Value.ToString(),
                            Utility.NulltoStr(dGV[cKSH, e.RowIndex].Value),
                            Utility.NulltoStr(dGV[cKSM, e.RowIndex].Value));

                        // 実働時間
                        if (wTime >= 0)
                        {
                            double wTimeH = Math.Floor(wTime / 60);
                            double wTimeM = wTime % 60;

                            // ChangeValueイベントを発生させない
                            global.ChangeValueStatus = false;
                            dGV[cWH, e.RowIndex].Value = wTimeH.ToString();
                            dGV[cWM, e.RowIndex].Value = wTimeM.ToString().PadLeft(2, '0');
                            global.ChangeValueStatus = true;
                        }
                        else
                        {
                            // ChangeValueイベントを発生させない
                            dGV[cWH, e.RowIndex].Value = string.Empty;
                            dGV[cWM, e.RowIndex].Value = string.Empty;
                            global.ChangeValueStatus = true;
                        }

                        // 警告表示初期化 : 曜日を表示します（日曜日は色表示のため）
                        dGV[cKSH, e.RowIndex].Style.BackColor = Color.Empty;
                        dGV[cKSE, e.RowIndex].Style.BackColor = Color.Empty;
                        dGV[cKSM, e.RowIndex].Style.BackColor = Color.Empty;
                        YoubiSet(e.RowIndex);

                        /*  正社員で残業時間２時間につき15分の休憩がないとき警告表示
                         *  2019/03/14  
                         */

                        if (lblShainkbn.Text == global.SEISHAIN.ToString())
                        {
                            wTime = ocr.getWorkTime(dGV[cSH, e.RowIndex].Value.ToString(),
                                dGV[cSM, e.RowIndex].Value.ToString(),
                                dGV[cEH, e.RowIndex].Value.ToString(),
                                dGV[cEM, e.RowIndex].Value.ToString(), "0", "0");

                            // 始業時刻から終業時刻が11時間超え (労働時間, 残業時間)
                            // (540, 0)(660,2)(780,4)(900, 6)(1020, 8)
                            if (wTime >= 660)
                            {
                                int z = (int)((wTime - 540) / 60 / 2);  // 残業2時間単位数
                                int rest = z * 15 + 60; // 計算上の休憩時間・分

                                // 記入休憩時間が計算上の休憩時間未満のとき
                                if (Utility.StrtoInt(Utility.NulltoStr(dGV[cKSH, e.RowIndex].Value)) * 60 + Utility.StrtoInt(Utility.NulltoStr(dGV[cKSM, e.RowIndex].Value)) < rest)
                                {
                                    dGV[cKSH, e.RowIndex].Style.BackColor = Color.LightPink;
                                    dGV[cKSM, e.RowIndex].Style.BackColor = Color.LightPink;
                                }
                            }
                        }


                        /*  正社員、臨時社員、外国人技能実習生で始業時刻から終業時刻が6時間を超えて
                         *  休憩時間が1時間未満のとき警告
                         *  2019/03/14   
                         */
                        if (lblShainkbn.Text == global.SEISHAIN.ToString() ||
                            lblShainkbn.Text == global.RINJISHAIN.ToString() ||
                            lblShainkbn.Text == global.GAIKOKUJINGINOU.ToString())
                        {
                            wTime = ocr.getWorkTime(dGV[cSH, e.RowIndex].Value.ToString(),
                                dGV[cSM, e.RowIndex].Value.ToString(),
                                dGV[cEH, e.RowIndex].Value.ToString(),
                                dGV[cEM, e.RowIndex].Value.ToString(), "0", "0");

                            // 始業時刻から終業時刻が6時間超え
                            if (wTime > 360)
                            {
                                // 休憩時間が1時間未満のとき
                                if (Utility.StrtoInt(Utility.NulltoStr(dGV[cKSH, e.RowIndex].Value)) < 1)
                                {
                                    dGV[cKSH, e.RowIndex].Style.BackColor = Color.LightPink;
                                    dGV[cKSM, e.RowIndex].Style.BackColor = Color.LightPink;
                                }
                            }
                        }

                        /* 外国人技能実習生で休憩時間が1時間30分以外のとき警告
                         * 2019/03/14  
                         */
                        if (lblShainkbn.Text == global.GAIKOKUJINGINOU.ToString())
                        {
                            if ((Utility.StrtoInt(Utility.NulltoStr(dGV[cKSH, e.RowIndex].Value)) * 60 + Utility.StrtoInt(Utility.NulltoStr(dGV[cKSM, e.RowIndex].Value))) != 90)
                            {
                                dGV[cKSH, e.RowIndex].Style.BackColor = Color.LightPink;
                                dGV[cKSM, e.RowIndex].Style.BackColor = Color.LightPink;
                            }
                        }
                    }
                    else
                    {
                        // ChangeValueイベントを発生させない
                        global.ChangeValueStatus = false;
                        dGV[cWH, e.RowIndex].Value = string.Empty;
                        dGV[cWM, e.RowIndex].Value = string.Empty;
                        global.ChangeValueStatus = true;
                    }
                }
                else
                {
                    // ChangeValueイベントを発生させない
                    global.ChangeValueStatus = false;
                    dGV[cWH, e.RowIndex].Value = string.Empty;
                    dGV[cWM, e.RowIndex].Value = string.Empty;
                    global.ChangeValueStatus = true;
                }
            }

            //// 警告：休憩時間が1時間以上のときバックカラーを変える 2015/08/31
            //if (colName == cKSH || colName == cKSM)
            //{
            //    int kh = 0;
            //    int km = 0;

            //    if (dGV[cKSH, e.RowIndex].Value != null && dGV[cKSM, e.RowIndex].Value != null)
            //    {
            //        kh = Utility.StrtoInt(dGV[cKSH, e.RowIndex].Value.ToString());
            //        km = Utility.StrtoInt(dGV[cKSM, e.RowIndex].Value.ToString());

            //        if ((kh * 60 + km) > 60)
            //        {
            //            dGV[cKSH, e.RowIndex].Style.BackColor = Color.LightPink;
            //            dGV[cKSE, e.RowIndex].Style.BackColor = Color.LightPink;
            //            dGV[cKSM, e.RowIndex].Style.BackColor = Color.LightPink;
            //        }
            //        else
            //        {
            //            // null処理を追加 2018/03/27
            //            if (Utility.NulltoStr(dGV[cWeek, e.RowIndex].Value) != "日")
            //            {
            //                dGV[cKSH, e.RowIndex].Style.BackColor = Color.White;
            //                dGV[cKSE, e.RowIndex].Style.BackColor = Color.White;
            //                dGV[cKSM, e.RowIndex].Style.BackColor = Color.White;
            //            }
            //        }
            //    }
            //}

            // 訂正チェック : 2019/03/14
            if (colName == cTeisei || Utility.NulltoStr(dGV[cTeisei, e.RowIndex].Value) == "True")
            {
                if (dGV[cTeisei, e.RowIndex].Value.ToString() == "True")
                {
                    dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    // 曜日を表示します（日曜日は色表示のため）
                    YoubiSet(e.RowIndex);
                }
            }
        }

        /// <summary>
        /// 与えられた休暇記号に該当する休暇日数取得
        /// </summary>
        /// <param name="kigou">休暇記号</param>
        /// <returns>休暇日数</returns>
        private string getKyukaTotal(string kigou)
        {
            int days = 0;
            //for (int i = 0; i < dGV.RowCount; i++)
            //{
            //    if (dGV[cKyuka, i].Value != null)
            //    {
            //        if (dGV[cKyuka, i].Value.ToString() == kigou)
            //            days++;
            //    }
            //}

            return days.ToString();
        }

        /// <summary>
        /// 深夜勤務時間取得(22:00～05:00)
        /// </summary>
        /// <returns>深夜勤務時間・分</returns>
        private double getShinyaTime()
        {
            int wHour = 0;
            int wMin = 0;
            int wHourk = 0;
            int wMink = 0;
            int sKyukei = 0;

            int sHour = 0;
            int sMin = 0;

            DateTime stTM;
            DateTime edTM;
            double spanMin = 0;

            for (int i = 0; i < dGV.RowCount; i++)
            {
                // 開始が５：００以前のとき
                if (Utility.NulltoStr(dGV[cSH, i].Value) != string.Empty && 
                    Utility.NulltoStr(dGV[cSM, i].Value) != string.Empty)
                {
                    wHour = Utility.StrtoInt(Utility.NulltoStr(dGV[cSH, i].Value));
                    wMin = Utility.StrtoInt(Utility.NulltoStr(dGV[cSM, i].Value));

                    if (wHour == 24) wHour = 0;

                    if (wHour < 5 && wMin < 60)
                    {
                        // 深夜勤務時間
                        stTM = DateTime.Parse(wHour.ToString() + ":" + wMin.ToString());
                        spanMin += Utility.GetTimeSpan(stTM, global.dt0500).TotalMinutes;
                    }
                }

                // 終了が２２：００以降のとき
                if (Utility.NulltoStr(dGV[cEH, i].Value) != string.Empty && 
                    Utility.NulltoStr(dGV[cEM, i].Value) != string.Empty)
                {
                    wHour = Utility.StrtoInt(Utility.NulltoStr(dGV[cEH, i].Value));
                    wMin = Utility.StrtoInt(Utility.NulltoStr(dGV[cEM, i].Value));

                    if (wHour >= 22)
                    {
                        // 深夜勤務時間
                        //sHour = (wHour - 22) * 60 + wMin;

                        if (wHour < 25 && wMin < 60)
                        {
                            if (wHour < 24)
                            {
                                edTM = DateTime.Parse(wHour.ToString() + ":" + wMin.ToString());
                                spanMin += Utility.GetTimeSpan(global.dt2200, edTM).TotalMinutes;
                            }
                            // 24:00のときは23:59まで計算して1分加算する
                            else if (wMin == 0)
                            {
                                edTM = DateTime.Parse("23:59");
                                spanMin += Utility.GetTimeSpan(global.dt2200, edTM).TotalMinutes + 1;
                            }
                        }
                    }
                }

                // 深夜勤務時間
                spanMin -= sKyukei;
            }

            return spanMin;
        }

        private void frmCorrect_Shown(object sender, EventArgs e)
        {
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            if (cI + 1 < dts.確定勤務票ヘッダ.Rows.Count)
            {
                cI++;
                showOcrData(cI);
            }   
        }

        ///-----------------------------------------------------------------------------------
        /// <summary>
        ///     カレントデータを更新する</summary>
        /// <param name="iX">
        ///     カレントレコードのインデックス</param>
        ///-----------------------------------------------------------------------------------
        private void CurDataUpDate(int iX)
        {
            // エラーメッセージ
            string errMsg = "出勤簿テーブル更新";

            try
            {
                // 確定勤務票ヘッダテーブル行を取得
                JAFA_OCRDataSet.確定勤務票ヘッダRow r = (JAFA_OCRDataSet.確定勤務票ヘッダRow)dts.確定勤務票ヘッダ.Rows[iX];
                
                // 確定勤務票ヘッダテーブルセット更新
                r.年 = Utility.StrtoInt(txtYear.Text);
                r.月 = Utility.StrtoInt(txtMonth.Text);
                r.社員番号 = Utility.StrtoInt(txtNo.Text);
                r.社員名 = Utility.NulltoStr(lblName.Text);
                r.交通費 = Utility.StrtoInt(txtKoutsuuhi.Text.Replace(",", ""));
                r.日当 = Utility.StrtoInt(txtNittou.Text.Replace(",", ""));
                r.宿泊費 = Utility.StrtoInt(txtShukuhakuhi.Text.Replace(",", ""));
                r.更新年月日 = DateTime.Now;

                // 確定勤務票明細テーブルセット更新
                for (int i = 0; i < global._MonthMULTIGYO; i++)
                {
                    // 存在する日付か検証
                    if (Utility.NulltoStr(dGV[cWeek, i].Value) != string.Empty)
                    {
                        JAFA_OCRDataSet.確定勤務票明細Row m = (JAFA_OCRDataSet.確定勤務票明細Row)dts.確定勤務票明細.FindByID(int.Parse(dGV[cID, i].Value.ToString()));

                        m.出勤区分 = Utility.NulltoStr(dGV[cKintai1, i].Value);
                        m.開始時 = timeValH(dGV[cSH, i].Value);
                        m.開始分 = timeVal(dGV[cSM, i].Value, 2);
                        m.終了時 = timeValH(dGV[cEH, i].Value);
                        m.終了分 = timeVal(dGV[cEM, i].Value, 2);
                        m.休憩開始時 = timeValH(dGV[cKSH, i].Value);
                        m.休憩開始分 = timeVal(dGV[cKSM, i].Value, 2);
                        m.実働時 = Utility.StrtoInt(timeValH(dGV[cWH, i].Value));
                        m.実働分 = Utility.StrtoInt(timeVal(dGV[cWM, i].Value, 2));

                        if (dGV[cTeisei, i].Value.ToString() == "True")
                        {
                            m.訂正 = global.flgOn;
                        }
                        else
                        {
                            m.訂正 = global.flgOff;
                        }

                        m.更新年月日 = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errMsg, MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        /// ----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     空白以外のとき、指定された文字数になるまで左側に０を埋めこみ、右寄せした文字列を返す
        /// </summary>
        /// <param name="tm">
        ///     文字列</param>
        /// <param name="len">
        ///     文字列の長さ</param>
        /// <returns>
        ///     文字列</returns>
        /// ----------------------------------------------------------------------------------------------------
        private string timeVal(object tm, int len)
        {
            string t = Utility.NulltoStr(tm);
            if (t != string.Empty) return t.PadLeft(len, '0');
            else return t;
        }

        /// ----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     空白以外のとき、先頭文字が０のとき先頭文字を削除した文字列を返す　
        ///     先頭文字が０以外のときはそのまま返す
        /// </summary>
        /// <param name="tm">
        ///     文字列</param>
        /// <returns>
        ///     文字列</returns>
        /// ----------------------------------------------------------------------------------------------------
        private string timeValH(object tm)
        {
            string t = Utility.NulltoStr(tm);

            if (t != string.Empty)
            {
                t = t.PadLeft(2, '0');
                if (t.Substring(0, 1) == "0")
                {
                    t = t.Substring(1, 1);
                }
            }

            return t;
        }

        /// ------------------------------------------------------------------------------------
        /// <summary>
        ///     Bool値を数値に変換する </summary>
        /// <param name="b">
        ///     True or False</param>
        /// <returns>
        ///     true:1, false:0</returns>
        /// ------------------------------------------------------------------------------------
        private int booltoFlg(string b)
        {
            if (b == "True") return global.flgOn;
            else return global.flgOff;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            cI =  dts.確定勤務票ヘッダ.Rows.Count - 1;
            showOcrData(cI);
        }

        private void btnBefore_Click(object sender, EventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            if (cI > 0)
            {
                cI--;
                showOcrData(cI);
            }   
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            cI = 0;
            showOcrData(cI);
        }

        /// <summary>
        ///     エラーチェックボタン </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrCheck_Click(object sender, EventArgs e)
        {
            // OCRDataクラス生成
            OCRData ocr = new OCRData();

            // エラーチェックを実行
            if (getErrData(cI, ocr, yArray, kArray))
            {
                MessageBox.Show("エラーはありませんでした", "エラーチェック", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dGV.CurrentCell = null;

                // データ表示
                showOcrData(cI);
            }
            else
            {
                // カレントインデックスをエラーありインデックスで更新
                cI = ocr._errHeaderIndex;

                // データ表示
                showOcrData(cI);

                // エラー表示
                ErrShow(ocr);
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            cI = hScrollBar1.Value;
            showOcrData(cI);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("表示中の勤務管理表データを削除します。よろしいですか", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // レコードと画像ファイルを削除する
            DataDelete(cI);

            // 確定勤務票ヘッダテーブル件数カウント
            if (dts.確定勤務票ヘッダ.Count() > 0)
            {
                // カレントレコードインデックスを再設定
                if (dts.確定勤務票ヘッダ.Count() - 1 < cI) cI = dts.確定勤務票ヘッダ.Count() - 1;

                // データ画面表示
                showOcrData(cI);
            }
            else
            {
                // ゼロならばプログラム終了
                MessageBox.Show("全ての勤務票データが削除されました。処理を終了します。", "勤務票削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //終了処理
                this.Tag = END_NODATA;
                this.Close();
            }
        }

        ///-------------------------------------------------------------------------------
        /// <summary>
        ///     １．指定した確定勤務票ヘッダデータと確定勤務票明細データを削除する　
        ///     ２．該当する画像データを削除する</summary>
        /// <param name="i">
        ///     確定勤務票ヘッダRow インデックス</param>
        ///-------------------------------------------------------------------------------
        private void DataDelete(int i)
        {
            string sImgNm = string.Empty;
            string errMsg = string.Empty;

            // 勤務票データ削除
            try
            {
                // ヘッダIDを取得します
                JAFA_OCRDataSet.確定勤務票ヘッダRow r = (JAFA_OCRDataSet.確定勤務票ヘッダRow)dts.確定勤務票ヘッダ.Rows[i];
                
                // データテーブルからヘッダIDが一致する確定勤務票明細データを削除します。
                errMsg = "確定勤務票明細データ";
                foreach (JAFA_OCRDataSet.確定勤務票明細Row item in dts.確定勤務票明細.Rows)
                {
                    if (item.RowState != DataRowState.Deleted && item.ヘッダID == r.ヘッダID)
                    {
                        // 画像ファイル名を取得します
                        sImgNm = item.画像名;

                        // 画像ファイルを削除します
                        errMsg = "勤務管理表画像";
                        if (sImgNm != string.Empty)
                        {
                            if (System.IO.File.Exists(Properties.Settings.Default.tifPath + sImgNm))
                            {
                                System.IO.File.Delete(Properties.Settings.Default.tifPath + sImgNm);
                            }
                        }

                        // 確定勤務票明細データを削除します
                        item.Delete();
                    }
                }

                // データテーブルから確定勤務票ヘッダデータを削除します
                errMsg = "確定勤務票ヘッダデータ";
                dts.確定勤務票ヘッダ.Rows[i].Delete();

                // データベース更新
                adpMn.UpdateAll(dts);
            }
            catch (Exception ex)
            {
                MessageBox.Show(errMsg + "の削除に失敗しました" + Environment.NewLine + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
            }
        }

        private void btnRtn_Click(object sender, EventArgs e)
        {
            // フォームを閉じる
            this.Tag = END_BUTTON;
            this.Close();
        }

        private void frmCorrect_FormClosing(object sender, FormClosingEventArgs e)
        {
            //「受入データ作成終了」「勤務票データなし」以外での終了のとき
            if (this.Tag.ToString() != END_MAKEDATA && this.Tag.ToString() != END_NODATA)
            {
                if (MessageBox.Show("終了します。よろしいですか", "終了確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                // カレントデータ更新
                CurDataUpDate(cI);
            }

            // データベース更新
            adpMn.UpdateAll(dts);

            // 解放する
            this.Dispose();
        }

        private void btnDataMake_Click(object sender, EventArgs e)
        {
            //// Big給与計算Pro勤怠データ作成：2018/11/07 コメント化
            //if (MessageBox.Show("Big給与計算Pro勤怠データ作成が選択されました。よろしいですか", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //{
            //    return;
            //}

            // Big給与計算Pro勤怠データ作成
            textDataMake();
        }

        /// -----------------------------------------------------------------------
        /// <summary>
        ///     Big給与計算Pro勤怠データ作成 </summary>
        /// -----------------------------------------------------------------------
        private void textDataMake()
        {
            // 出勤簿前半後半回収チェック
            string msg = chkDataZenKou();

            // 警告表示
            if (msg != string.Empty)
            {
                if (MessageBox.Show(msg, "警告表示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            // 確認メッセージ
            if (MessageBox.Show("Big給与計算Pro勤怠データを作成します。よろしいですか", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            
            try
            {
                // カーソルを待機にします
                this.Cursor = Cursors.WaitCursor;

                // OCRDataクラス生成
                OCRData ocr = new OCRData();

                // エラーチェックを実行する
                if (getErrData(cI, ocr, yArray, kArray)) // エラーがなかったとき
                {
                    adpMn.UpdateAll(dts);

                    // OCROutputクラス インスタンス生成
                    OCROutput kd = new OCROutput(this, dts, global.cnfYear, global.cnfMonth);

                    Cursor = Cursors.WaitCursor;

                    // 週実績明細集計 : 2018/10/27
                    kd.saveWeekData();                    

                    // JAメイト年休取得データ(mdb)作成 : 2018/10/27
                    kd.saveJAMateNenkyuData();

                    // 勤怠データ作成
                    kd.saveJAMateOCRData();

                    /* 有給休暇付与日数計算・有給付与マスター・有休付与データ登録：
                     * 臨時社員、外国人技能実習生 
                     */
                    kd.addYukyuData();

                    /* 有給休暇付与日数計算・有給付与マスター・有休付与データ登録：
                     * 正社員：入社後６ヶ月経過時
                     */
                    kd.sumYukyuAfter6();

                    /* 有給休暇付与日数計算・有給付与マスター・有休付与データ登録：
                     * 正社員：４月付与
                     */
                    if (global.cnfMonth == 3)
                    {
                        kd.sumYukyu04Month();
                    }

                    // 勤怠データに有給付与情報を書き込み
                    kd.saveJAMateOCRYukyu();

                    //// JAメイト年休取得データ CSVファイル出力：2018/11/02 コメント化
                    //kd.saveJAMateNenkyuCsv(global.cnfYear, global.cnfMonth);

                    // Big給与計算Pro有休付与データ CSVファイル出力
                    kd.saveBigProNenkyuCsv(global.cnfYear, global.cnfMonth);

                    // Big給与計算Pro勤怠データ(mdb)作成 : 2018/11/05
                    kd.saveBigProKintaiData();

                    //// JAメイトOCRデータ CSVファイル出力 2018/11/05 コメント化
                    //kd.saveJAMateOcrCsv(global.cnfYear, global.cnfMonth);

                    // Big給与計算勤怠ProデータJAメイトOCRデータ CSVファイル出力
                    kd.saveBigProOcrCsv(global.cnfYear, global.cnfMonth);

                    // バックアップ用画像コピー
                    tifFileBackup();

                    // 過去データ作成
                    saveLastData();
                    //lastDataYearWaToSe();   // 年を和暦から西暦に書き直す

                    // 設定月数分経過した過去画像と過去勤務データを削除する
                    deleteArchived();

                    // 確定勤務票データ削除
                    deleteDataAll();

                    // 週実績明細削除　(過去1年以上前）: 2018/11/19
                    weekItemDataDelete();

                    // 週実績削除　(過去1年以上前）: 2018/11/19
                    weekDataDelete();

                    // MDBファイル最適化
                    mdbCompact();

                    //終了
                    MessageBox.Show("終了しました。Big給与計算Proシステムで勤怠データと有給付与データ受け入れを行ってください。", "給与計算受け入れデータ作成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Tag = END_MAKEDATA;
                    this.Close();
                }
                else
                {
                    // カレントインデックスをエラーありインデックスで更新
                    cI = ocr._errHeaderIndex;

                    // エラーあり
                    showOcrData(cI);    // データ表示
                    ErrShow(ocr);   // エラー表示
                }
            }
            catch (Exception ex)
            {
                // カーソルを戻します
                this.Cursor = Cursors.Default;

                MessageBox.Show(ex.Message);
            }
            finally
            {
                // カーソルを戻します
                this.Cursor = Cursors.Default;
            }

        }

        /// ----------------------------------------------------------------
        /// <summary>
        ///     出勤簿前半後半回収チェック </summary>
        /// <returns>
        ///     メッセージ文字列</returns>
        /// ----------------------------------------------------------------
        private string chkDataZenKou()
        {
            StringBuilder sb = new StringBuilder();

            var h = dts.確定勤務票ヘッダ.OrderBy(a => a.ヘッダID);

            foreach (var hr in h)
            {
                // ヘッダＩＤ毎に確定勤務票明細の件数を取得します
                int s = dts.確定勤務票明細.Where(a => a.ヘッダID == hr.ヘッダID).Count();

                if (s <= 16)
                {
                    if (sb.ToString() == string.Empty)
                    {
                        sb.Append("下記の社員は出勤簿が前半または後半のみです。").Append(Environment.NewLine).Append(Environment.NewLine);
                    }

                    sb.Append("社員番号：").Append(hr.社員番号).Append("  ");
                    sb.Append("氏名：").Append(hr.社員名).Append("  ").Append(Environment.NewLine);
                } 
            }

            if (sb.ToString() != string.Empty)
            {
                sb.Append(Environment.NewLine);
                sb.Append("Ｂｉｇ給与計算Ｐｒｏデータを作成してよろしいですか？");
            }

            return sb.ToString();
        }


        /// -----------------------------------------------------------------------------------
        /// <summary>
        ///     エラーチェックを実行する</summary>
        /// <param name="cIdx">
        ///     現在表示中の確定勤務票ヘッダデータインデックス</param>
        /// <param name="ocr">
        ///     OCRDATAクラスインスタンス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        /// -----------------------------------------------------------------------------------
        private bool getErrData(int cIdx, OCRData ocr, clsYukyuFuyo [] yArray, clsKintaiXls[] kArray)
        {
            // カレントレコード更新
            CurDataUpDate(cIdx);

            // エラー番号初期化
            ocr._errNumber = ocr.eNothing;

            // エラーメッセージクリーン
            ocr._errMsg = string.Empty;         

            // エラーチェック実行①:カレントの社員から最終社員まで
            if (!ocr.errCheckMain(cIdx, (dts.確定勤務票ヘッダ.Rows.Count - 1), this, dts, yArray, kArray))
            {
                return false;
            }

            // エラーチェック実行②:最初のレコードからカレントレコードの前のレコードまで
            if (cIdx > 0)
            {
                if (!ocr.errCheckMain(0, (cIdx - 1), this, dts, yArray, kArray))
                {
                    return false;
                }
            }

            // エラーなし
            lblErrMsg.Text = string.Empty;

            return true;
        }
        
        ///----------------------------------------------------------------------------------
        /// <summary>
        ///     画像ファイル退避処理 </summary>
        ///----------------------------------------------------------------------------------
        private void tifFileMove()
        {
            // 移動先フォルダがあるか？なければ作成する（TIFフォルダ）
            if (!System.IO.Directory.Exists(Properties.Settings.Default.tifPath))
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.tifPath);

            // 確定出勤簿ヘッダデータを取得する
            //var s = dts.確定勤務票ヘッダ.OrderBy(a => a.ID);

            // 確定出勤簿明細データから画像名を取得する
            var s = dts.確定勤務票明細.Select(a => new
            {
                画像名 = a.画像名
            }).Distinct();

            foreach (var t in s)
            {
                //string NewFilenameYearMonth = t.年.ToString() + t.月.ToString().PadLeft(2, '0');

                // 画像ファイルパスを取得する
                string fromImg = Properties.Settings.Default.dataPath + t.画像名;

                //// ファイル名を「対象年月・個人番号_前半後半」に変えて退避先フォルダへ移動する
                //string NewFilename = NewFilenameYearMonth + t.社員番号.ToString().PadLeft(global.ShainMaxLength, '0') + "_" + t.前半後半 + ".tif";
                string toImg = Properties.Settings.Default.tifPath + t.画像名;

                // 同名ファイルが既に登録済みのときは削除する
                if (System.IO.File.Exists(toImg)) System.IO.File.Delete(toImg);

                // ファイルを移動する
                if (System.IO.File.Exists(fromImg)) System.IO.File.Move(fromImg, toImg);

                //// 出勤簿ヘッダレコードの画像ファイル名を書き換える
                //JAHR_OCRDataSet.確定勤務票ヘッダRow r = dts.確定勤務票ヘッダ.FindByID(t.ID);
                //r.画像名 = NewFilename;
            }
        }

        ///----------------------------------------------------------------------------------
        /// <summary>
        ///     画像ファイルを年月フォルダへバックアップ処理 </summary>
        ///----------------------------------------------------------------------------------
        private void tifFileBackup()
        {
            // バックアップフォルダがあるか？なければ作成する（TIFフォルダ）
            if (!System.IO.Directory.Exists(Properties.Settings.Default.tifBackupPath))
            {
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.tifBackupPath);
            }

            // 当月バックアップフォルダがあるか？なければ作成する（TIFフォルダ）
            string yymmPath = global.cnfYear.ToString() + global.cnfMonth.ToString().PadLeft(2, '0'); 
            if (!System.IO.Directory.Exists(Properties.Settings.Default.tifBackupPath + yymmPath))
            {
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.tifBackupPath + yymmPath);
            }

            string imgName = string.Empty;
            string hdID = string.Empty;

            iAdp.Fill(dts.確定勤務票明細);    // 2018/11/09

            foreach (var m in dts.確定勤務票明細.OrderBy(a => a.画像名))
            {
                // 画像コピー処理
                if (imgName != m.画像名)
                {
                    // 画像ファイルパスを取得する
                    string fromImg = Properties.Settings.Default.tifPath + m.画像名;

                    // 画像ファイル名を「社員番号 氏名 前半・後半」に変えてバックアップフォルダへコピーする
                    string NewFilename = string.Empty;

                    // 前半後半の判断
                    string zk = string.Empty;
                    if (m.日付 < 16)
                    {
                        zk = "前 ";
                    }
                    else
                    {
                        zk = "後 ";
                    }

                    if (dts.確定勤務票ヘッダ.Any(a => a.ヘッダID == m.ヘッダID))
                    {
                        JAFA_OCRDataSet.確定勤務票ヘッダRow r = dts.確定勤務票ヘッダ.Single(a => a.ヘッダID == m.ヘッダID);
                        NewFilename = r.社員番号.ToString().PadLeft(5, '0') + zk + r.社員名 + ".tif";
                    }
                    else
                    {
                        // 通常は明細があってヘッダレコードがないことは有り得ないが念のため
                        NewFilename = m.ヘッダID + ".tif";
                    }

                    string toImg = Properties.Settings.Default.tifBackupPath + yymmPath + @"\" + NewFilename;

                    // 同名ファイルが既に登録済みのときは削除する
                    if (System.IO.File.Exists(toImg)) System.IO.File.Delete(toImg);

                    // ファイルをコピーする
                    if (System.IO.File.Exists(fromImg)) System.IO.File.Copy(fromImg, toImg);
                }

                imgName = m.画像名;
                hdID = m.ヘッダID;
            }
        }

        ///------------------------------------------------------------------------
        /// <summary>
        ///     過去の週実績明細データを削除する：2018/11/19
        ///     ※1年以上前データを削除対象        /// </summary>
        ///------------------------------------------------------------------------
        private void weekItemDataDelete()
        {
            DateTime dt = DateTime.Today.AddYears(-1);
            dt = DateTime.Parse(dt.Year + "/" + dt.Month + "/01");

            JAFA_OCRDataSetTableAdapters.週実績明細TableAdapter adp = new JAFA_OCRDataSetTableAdapters.週実績明細TableAdapter();
            adp.DeleteQueryPastymd(dt);
        }

        ///------------------------------------------------------------------------
        /// <summary>
        ///     過去の週実績データを削除する：2018/11/19
        ///     ※1年以上前データを削除対象        /// </summary>
        ///------------------------------------------------------------------------
        private void weekDataDelete()
        {
            DateTime dt = DateTime.Today.AddYears(-1);
            int dd = dt.Year * 100 + dt.Month;

            JAFA_OCRDataSetTableAdapters.週実績TableAdapter adp = new JAFA_OCRDataSetTableAdapters.週実績TableAdapter();
            adp.DeleteQueryYYMM(dd);
        }


        /// ---------------------------------------------------------------------
        /// <summary>
        ///     MDBファイルを最適化する </summary>
        /// ---------------------------------------------------------------------
        private void mdbCompact()
        {
            try
            {
                JRO.JetEngine jro = new JRO.JetEngine();
                string OldDb = Properties.Settings.Default.mdbOlePath;
                string NewDb = Properties.Settings.Default.mdbPathTemp;

                // 一時ファイルが存在しているとき削除する：2018/11/16
                if (System.IO.File.Exists(Properties.Settings.Default.mdbPath + global.MDBTEMP))
                {
                    System.IO.File.Delete(Properties.Settings.Default.mdbPath + global.MDBTEMP);
                }

                // 最適化した結果を一時ファイルに出力
                jro.CompactDatabase(OldDb, NewDb);

                //今までのバックアップファイルを削除する
                System.IO.File.Delete(Properties.Settings.Default.mdbPath + global.MDBBACK);

                //今までのファイルをバックアップとする
                System.IO.File.Move(Properties.Settings.Default.mdbPath + global.MDBFILE, Properties.Settings.Default.mdbPath + global.MDBBACK);

                //一時ファイルをMDBファイルとする
                System.IO.File.Move(Properties.Settings.Default.mdbPath + global.MDBTEMP, Properties.Settings.Default.mdbPath + global.MDBFILE);
            }
            catch (Exception e)
            {
                MessageBox.Show("MDB最適化中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
            }
        }
        
        /// ---------------------------------------------------------------------------------
        /// <summary>
        ///     設定月数分経過した過去画像と過去勤務データを削除する </summary>        ///     
        /// ---------------------------------------------------------------------------------
        private void deleteArchived()
        {
            // 削除月設定が0のとき、「過去画像削除しない」とみなし終了する
            if (global.cnfArchived == global.flgOff) return;

            try
            {
                // 削除年月の取得
                DateTime dt = DateTime.Parse(DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString() + "/01");
                DateTime delDate = dt.AddMonths(global.cnfArchived * (-1));
                int _dYY = delDate.Year;            //基準年
                int _dMM = delDate.Month;           //基準月
                int _dYYMM = _dYY * 100 + _dMM;     //基準年月
                int _waYYMM = delDate.Year * 100 + _dMM;   //基準年月(和暦）

                // 設定月数分経過した過去勤務票データを削除する
                deleteLastDataArchived(_dYYMM);

                // 設定月数分経過した過去画像を削除する
                deleteImageArchived(_dYYMM);
            }
            catch (Exception e)
            {
                MessageBox.Show("過去画像・過去勤務票データ削除中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                //if (ocr.sCom.Connection.State == ConnectionState.Open) ocr.sCom.Connection.Close();
            }
        }

        /// ---------------------------------------------------------------------------
        /// <summary>
        ///     過去勤務票データ登録 </summary>
        /// ---------------------------------------------------------------------------
        private void saveLastData()
        {
            try
            {
                // データベース更新
                adpMn.UpdateAll(dts);

                // 年月、個人番号が一致する過去勤務票ヘッダデータとその明細データを削除します
                deleteLastData();
               
                // 過去勤務票ヘッダデータと過去勤務票明細データを作成します
                addLastdata();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "過去勤務票データ作成エラー", MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        /// ------------------------------------------------------------------------
        /// <summary>
        ///     過去勤務票ヘッダの年を和暦から西暦に書き直す </summary>
        /// ------------------------------------------------------------------------
        private void lastDataYearWaToSe()
        {
            //OleDbCommand sCom = new OleDbCommand();
            //sCom.Connection = Utility.dbConnect();

            //StringBuilder sb = new StringBuilder();
            //sb.Clear();
            //sb.Append("update 過去勤務票ヘッダ set 年 = 年 + ? ");
            //sb.Append("where 年 < 2014");
            //sCom.CommandText = sb.ToString();

            //sCom.Parameters.Clear();
            //sCom.Parameters.AddWithValue("@re", Properties.Settings.Default.rekiHosei);
            //sCom.ExecuteNonQuery();

            //sCom.Connection.Close();
        }
        

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     勤務票データの帳票番号、データ領域名、年月、個人番号が一致する
        ///     過去確定勤務票ヘッダデータとその明細データを削除します</summary>    
        ///     
        /// -------------------------------------------------------------------------
        private void deleteLastData()
        {
            OleDbCommand sCom = new OleDbCommand();
            sCom.Connection = Utility.dbConnect();
            OleDbCommand sCom2 = new OleDbCommand();
            sCom2.Connection = Utility.dbConnect();
            OleDbDataReader dR;

            // 確定勤務票ヘッダを取得します
            var s = dts.確定勤務票ヘッダ
                .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached)
                .OrderBy(a => a.ID);

            foreach (var r in s)
            {
                //// ヘッダIDが一致する過去勤務票ヘッダデータを取得します
                //StringBuilder sb = new StringBuilder();
                //sb.Append("select ヘッダID from 過去勤務票ヘッダ ");
                //sb.Append("where ヘッダID = ? ");
                //sCom.CommandText = sb.ToString();
                //sCom.Parameters.Clear();
                //sCom.Parameters.AddWithValue("@hID", r.ヘッダID);

                //dR = sCom.ExecuteReader();

                //// 過去勤務票明細データ削除
                //while(dR.Read())
                //{
                //    sb = new StringBuilder();
                //    sb.Append("delete from 過去勤務票明細 ");
                //    sb.Append("where ヘッダID = ?");
                //    sCom2.CommandText = sb.ToString();
                //    sCom2.Parameters.Clear();
                //    sCom2.Parameters.AddWithValue("@y", dR["ヘッダID"].ToString());
                //    sCom2.ExecuteNonQuery();
                //}

                //dR.Close();

                // 過去勤務票明細データ削除
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from 過去勤務票明細 ");
                sb.Append("where ヘッダID = ?");
                sCom2.CommandText = sb.ToString();
                sCom2.Parameters.Clear();
                sCom2.Parameters.AddWithValue("@y", r.ヘッダID);
                sCom2.ExecuteNonQuery();

                // 過去勤務票ヘッダデータ削除
                //sb.Clear();
                //sb.Append("delete from 過去勤務票ヘッダ ");
                //sb.Append("where 年 = ? and 月 = ? and 社員番号 = ?");
                //sCom.CommandText = sb.ToString();
                //sCom.Parameters.Clear();
                //sCom.Parameters.AddWithValue("@y", r.年 + Properties.Settings.Default.rekiHosei);
                //sCom.Parameters.AddWithValue("@m", r.月);
                //sCom.Parameters.AddWithValue("@s", r.社員番号);
                //sCom.ExecuteNonQuery();

                // 過去勤務票ヘッダデータ削除
                sb.Clear();
                sb.Append("delete from 過去勤務票ヘッダ ");
                sb.Append("where ヘッダID = ?");
                sCom.CommandText = sb.ToString();
                sCom.Parameters.Clear();
                sCom.Parameters.AddWithValue("@y", r.ヘッダID);
                sCom.ExecuteNonQuery();                
            }

            sCom.Connection.Close();
            sCom2.Connection.Close();
        }

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     過去確定勤務票ヘッダデータと過去確定勤務票明細データを作成します</summary>
        ///     
        /// -------------------------------------------------------------------------
        private void addLastdata()
        {            
            OleDbCommand sCom = new OleDbCommand();
            sCom.Connection = Utility.dbConnect();

            // 過去確定勤務票ヘッダレコードを作成します
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [");
            sb.Append(Properties.Settings.Default.ocrMdbPath + "].過去勤務票ヘッダ ");
            sb.Append("SELECT * ");
            sb.Append("FROM [");
            sb.Append(Properties.Settings.Default.ocrMdbPath +  "].確定勤務票ヘッダ;");
            sCom.CommandText = sb.ToString();
            sCom.ExecuteNonQuery();

            // 過去確定勤務票明細レコードを作成します
            sb = new StringBuilder();
            sb.Append("INSERT INTO [");
            sb.Append(Properties.Settings.Default.ocrMdbPath + "].過去勤務票明細 ");
            sb.Append("(ヘッダID,日付,出勤区分,開始時,開始分,終了時,終了分,");
            sb.Append("時間外時,時間外分,休憩開始時,休憩開始分,休憩終了時,休憩終了分,");
            sb.Append("休日出勤時,休日出勤分,所定内休日時,所定内休日分,深夜時,深夜分,実働時,実働分,");
            sb.Append("週番号,訂正,画像名,更新年月日) ");
            sb.Append("SELECT ヘッダID,日付,出勤区分,開始時,開始分,終了時,終了分,");
            sb.Append("時間外時,時間外分,休憩開始時,休憩開始分,休憩終了時,休憩終了分,");
            sb.Append("休日出勤時,休日出勤分,所定内休日時,所定内休日分,深夜時,深夜分,実働時,実働分,");
            sb.Append("週番号,訂正,画像名,更新年月日 ");
            sb.Append("FROM [");
            sb.Append(Properties.Settings.Default.ocrMdbPath + "].確定勤務票明細;");
            sCom.CommandText = sb.ToString();
            sCom.ExecuteNonQuery();

            sCom.Connection.Close();
        }

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     確定確定勤務票ヘッダデータと確定確定勤務票明細データを作成します</summary>
        ///     
        /// -------------------------------------------------------------------------
        private void addKakuteiData()
        {
            OleDbCommand sCom = new OleDbCommand();
            sCom.Connection = Utility.dbConnect();

            // 確定確定勤務票ヘッダレコードを作成します
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [");
            sb.Append(Properties.Settings.Default.ocrMdbPath + "].過去勤務票ヘッダ ");
            sb.Append("SELECT * ");
            sb.Append("FROM [");
            sb.Append(Properties.Settings.Default.dataMdbPath + "].確定勤務票ヘッダ;");
            sCom.CommandText = sb.ToString();
            sCom.ExecuteNonQuery();

            // 確定確定勤務票明細レコードを作成します
            sb = new StringBuilder();
            sb.Append("INSERT INTO [");
            sb.Append(Properties.Settings.Default.ocrMdbPath + "].過去勤務票明細 ");
            sb.Append("(ヘッダID,日付,出勤区分,開始時,開始分,終了時,終了分,");
            sb.Append("休憩開始時,休憩開始分,実働時,実働分,訂正,更新年月日) ");
            sb.Append("SELECT ヘッダID,日付,出勤区分,開始時,開始分,終了時,終了分,");
            sb.Append("休憩開始時,休憩開始分,実働時,実働分,訂正,更新年月日 ");
            sb.Append("FROM [");
            sb.Append(Properties.Settings.Default.dataMdbPath + "].確定勤務票明細;");
            sCom.CommandText = sb.ToString();
            sCom.ExecuteNonQuery();

            sCom.Connection.Close();
        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex < 0) return;

            string colName = dGV.Columns[e.ColumnIndex].Name;

            if (colName == cSH || colName == cSE || colName == cEH || colName == cEE ||
                colName == cKSH || colName == cKSE || colName == cWH || colName == cWE)
            {
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            string colName = dGV.Columns[dGV.CurrentCell.ColumnIndex].Name;

            if (colName == cTeisei)
            {
                if (dGV.IsCurrentCellDirty)
                {
                    dGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dGV.RefreshEdit();
                }
            }

            //if (colName == cKyuka || colName == cCheck)
            //{
            //    if (dGV.IsCurrentCellDirty)
            //    {
            //        dGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //        dGV.RefreshEdit();
            //    }
            //}
        }

        private void dataGridView1_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            // エラー表示時には処理を行わない
            if (!gridViewCellEnterStatus) return;
 
            string ColH = string.Empty;
            string ColM = dGV.Columns[dGV.CurrentCell.ColumnIndex].Name;

            // 開始時間または終了時間を判断
            if (ColM == cSM)            // 開始時刻
            {
                ColH = cSH;
            }
            else if (ColM == cEM)       // 終了時刻
            {
                ColH = cEH;
            }
            else if (ColM == cKSM)      // 休憩開始時刻
            {
                ColH = cKSH;
            }
            else
            {
                return;
            }

            // 時が入力済みで分が未入力のとき分に"00"を表示します
            if (dGV[ColH, dGV.CurrentRow.Index].Value != null)
            {
                if (dGV[ColH, dGV.CurrentRow.Index].Value.ToString().Trim() != string.Empty)
                {
                    if (dGV[ColM, dGV.CurrentRow.Index].Value == null)
                    {
                        dGV[ColM, dGV.CurrentRow.Index].Value = "00";
                    }
                    else if (dGV[ColM, dGV.CurrentRow.Index].Value.ToString().Trim() == string.Empty)
                    {
                        dGV[ColM, dGV.CurrentRow.Index].Value = "00";
                    }
                }
            }

        }

        ///-----------------------------------------------------------
        /// <summary>
        ///     画像表示 openCV：2018/10/24 </summary>
        /// <param name="img">
        ///     表示画像ファイル名</param>
        ///-----------------------------------------------------------
        private void showImage_openCv(string img)
        {
            n_width = B_WIDTH;
            n_height = B_HEIGHT;

            imgShow(img, n_width, n_height);

            trackBar1.Value = 0;
        }

        ///---------------------------------------------------------
        /// <summary>
        ///     画像表示メイン openCV : 2018/10/24 </summary>
        /// <param name="mImg">
        ///     Mat形式イメージ</param>
        /// <param name="w">
        ///     width</param>
        /// <param name="h">
        ///     height</param>
        ///---------------------------------------------------------
        private void imgShow(string filePath, float w, float h)
        {
            mMat = new Mat(filePath, ImreadModes.GrayScale);
            Bitmap bt = MatToBitmap(mMat);

            // Bitmap を生成
            Bitmap canvas = new Bitmap((int)(bt.Width * w), (int)(bt.Height * h));

            Graphics g = Graphics.FromImage(canvas);

            g.DrawImage(bt, 0, 0, bt.Width * w, bt.Height * h);

            //メモリクリア
            bt.Dispose();
            g.Dispose();

            pictureBox1.Image = canvas;
        }

        ///---------------------------------------------------------
        /// <summary>
        ///     画像表示メイン openCV : 2018/10/24 </summary>
        /// <param name="mImg">
        ///     Mat形式イメージ</param>
        /// <param name="w">
        ///     width</param>
        /// <param name="h">
        ///     height</param>
        ///---------------------------------------------------------
        private void imgShow(Mat mImg, float w, float h)
        {
            int cWidth = 0;
            int cHeight = 0;

            Bitmap bt = MatToBitmap(mImg);

            // Bitmapサイズ
            if (panel1.Width < (bt.Width * w) || panel1.Height < (bt.Height * h))
            {
                cWidth = (int)(bt.Width * w);
                cHeight = (int)(bt.Height * h);
            }
            else
            {
                cWidth = panel1.Width;
                cHeight = panel1.Height;
            }

            // Bitmap を生成
            Bitmap canvas = new Bitmap(cWidth, cHeight);

            // ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            // 画像をcanvasの座標(0, 0)の位置に指定のサイズで描画する
            g.DrawImage(bt, 0, 0, bt.Width * w, bt.Height * h);

            //メモリクリア
            bt.Dispose();
            g.Dispose();

            // PictureBox1に表示する
            pictureBox1.Image = canvas;
        }

        // GUI上に画像を表示するには、OpenCV上で扱うMat形式をBitmap形式に変換する必要がある
        public static Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }

        /// ------------------------------------------------------------------------------
        /// <summary>
        ///     伝票画像表示 </summary>
        /// <param name="iX">
        ///     現在の伝票</param>
        /// <param name="tempImgName">
        ///     画像名</param>
        /// ------------------------------------------------------------------------------
        public void ShowImage(string tempImgName)
        {
            ////修正画面へ組み入れた画像フォームの表示    
            ////画像の出力が無い場合は、画像表示をしない。
            //if (tempImgName == string.Empty)
            //{
            //    leadImg.Visible = false;
            //    lblNoImage.Visible = false;
            //    global.pblImagePath = string.Empty;
            //    return;
            //}

            ////画像ファイルがあるとき表示
            //if (File.Exists(tempImgName))
            //{
            //    lblNoImage.Visible = false;
            //    leadImg.Visible = true;

            //    // 画像操作ボタン : 2018/10/26 コメント化
            //    //btnPlus.Enabled = true;
            //    //btnMinus.Enabled = true;

            //    trackBar1.Enabled = true;   // 2018/10/26

            //    //画像ロード
            //    Leadtools.Codecs.RasterCodecs.Startup();
            //    Leadtools.Codecs.RasterCodecs cs = new Leadtools.Codecs.RasterCodecs();

            //    // 描画時に使用される速度、品質、およびスタイルを制御します。 
            //    Leadtools.RasterPaintProperties prop = new Leadtools.RasterPaintProperties();
            //    prop = Leadtools.RasterPaintProperties.Default;
            //    prop.PaintDisplayMode = Leadtools.RasterPaintDisplayModeFlags.Resample;
            //    leadImg.PaintProperties = prop;

            //    leadImg.Image = cs.Load(tempImgName, 0, Leadtools.Codecs.CodecsLoadByteOrder.BgrOrGray, 1, 1);

            //    //画像表示倍率設定
            //    if (global.miMdlZoomRate == 0f)
            //    {
            //        leadImg.ScaleFactor *= global.ZOOM_RATE;
            //    }
            //    else
            //    {
            //        leadImg.ScaleFactor *= global.miMdlZoomRate;
            //    }

            //    //画像のマウスによる移動を可能とする
            //    leadImg.InteractiveMode = Leadtools.WinForms.RasterViewerInteractiveMode.Pan;

            //    // グレースケールに変換
            //    Leadtools.ImageProcessing.GrayscaleCommand grayScaleCommand = new Leadtools.ImageProcessing.GrayscaleCommand();
            //    grayScaleCommand.BitsPerPixel = 8;
            //    grayScaleCommand.Run(leadImg.Image);
            //    leadImg.Refresh();

            //    cs.Dispose();
            //    Leadtools.Codecs.RasterCodecs.Shutdown();
            //    //global.pblImagePath = tempImgName;
            //}
            //else
            //{
            //    //画像ファイルがないとき
            //    lblNoImage.Visible = true;

            //    // 画像操作ボタン　：2018/10/26 コメント化
            //    //btnPlus.Enabled = false;
            //    //btnMinus.Enabled = false;

            //    trackBar1.Enabled = false; // 2018/10/26

            //    leadImg.Visible = false;
            //    //global.pblImagePath = string.Empty;
            //}
        }

        private void leadImg_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void leadImg_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     基準年月以前の過去確定勤務票ヘッダデータとその明細データを削除します</summary>
        /// <param name="sYYMM">
        ///     基準年月</param>     
        /// -------------------------------------------------------------------------
        private void deleteLastDataArchived(int sYYMM)
        {
            OleDbCommand sCom = new OleDbCommand();
            sCom.Connection = Utility.dbConnect();
            OleDbCommand sCom2 = new OleDbCommand();
            sCom2.Connection = Utility.dbConnect();
            OleDbDataReader dR;

            // 基準年月以前の過去確定勤務票ヘッダデータを取得します
            StringBuilder sb = new StringBuilder();
            sb.Append("select ヘッダID from 過去勤務票ヘッダ ");
            sb.Append("where (年 * 100 + 月) < ?");
            sCom.CommandText = sb.ToString();
            sCom.Parameters.Clear();
            sCom.Parameters.AddWithValue("@y", sYYMM);

            dR = sCom.ExecuteReader();

            // 過去確定勤務票明細データ削除
            while (dR.Read())
            {
                sb = new StringBuilder();
                sb.Append("delete from 過去勤務票明細 ");
                sb.Append("where ヘッダID = ?");
                sCom2.CommandText = sb.ToString();
                sCom2.Parameters.Clear();
                sCom2.Parameters.AddWithValue("@y", dR["ヘッダID"].ToString());
                sCom2.ExecuteNonQuery();
            }

            dR.Close();

            // 過去確定勤務票ヘッダデータ削除
            sb = new StringBuilder();
            sb.Append("delete from 過去勤務票ヘッダ ");
            sb.Append("where (年 * 100 + 月) < ?");
            sCom.CommandText = sb.ToString();
            sCom.Parameters.Clear();
            sCom.Parameters.AddWithValue("@y", sYYMM);
            sCom.ExecuteNonQuery();

            sCom.Connection.Close();
            sCom2.Connection.Close();
        }
        
        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     設定月数分経過した過去画像を削除する</summary>
        /// <param name="_dYYMM">
        ///     基準年月 (例：201401)</param>
        /// -----------------------------------------------------------------------------
        private void deleteImageArchived(int _dYYMM)
        {
            int _DataYYMM;
            string fileYYMM;

            //// 設定月数分経過した過去画像を削除する            
            //foreach (string files in System.IO.Directory.GetFiles(Properties.Settings.Default.tifPath, "*.tif"))
            //{
            //    // ファイル名が規定外のファイルは読み飛ばします
            //    if (System.IO.Path.GetFileName(files).Length < 20) continue;

            //    //ファイル名より年月を取得する
            //    fileYYMM = System.IO.Path.GetFileName(files).Substring(5, 6);

            //    if (Utility.NumericCheck(fileYYMM))
            //    {
            //        _DataYYMM = int.Parse(fileYYMM);

            //        //基準年月以前なら削除する
            //        if (_DataYYMM <= _dYYMM) File.Delete(files);
            //    }
            //}

            
            // 過去勤務票明細にない画像は削除する
            JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter sAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter();
            sAdp.Fill(dts.過去勤務票明細);

            foreach (string files in System.IO.Directory.GetFiles(Properties.Settings.Default.tifPath, "*.tif"))
            {
                string f = System.IO.Path.GetFileName(files);

                if (!dts.過去勤務票明細.Any(a => a.画像名 == f))
                {
                    System.IO.File.Delete(files);
                }
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        ///     確定勤務票ヘッダデータと確定勤務票明細データを全件削除します</summary>
        /// -------------------------------------------------------------------
        private void deleteDataAll() 
        {
            // 確定勤務票明細全行削除
            iAdp.DeleteQueryAll();  // 2018/11/09

            // 確定勤務票ヘッダ全行削除
            hAdp.DeleteQueryAll();  // 2018/11/09

            // 後片付け
            dts.確定勤務票明細.Dispose();
            dts.確定勤務票ヘッダ.Dispose();
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 画像表示
            //ShowImage(global.pblImagePath + Utility.NulltoStr(dGV[cImg, dGV.CurrentRow.Index].Value).ToString());

            // openCV:2018/10/26
            string img = global.pblImagePath + Utility.NulltoStr(dGV[cImg, dGV.CurrentRow.Index].Value).ToString();

            // 2019/03/13
            if (System.IO.File.Exists(img))
            {
                showImage_openCv(img);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmOCRIndex frm = new frmOCRIndex();
            frm.ShowDialog();
            string hID = frm.hdID;
            frm.Dispose();

            if (hID != string.Empty)
            {
                //カレントデータの更新
                CurDataUpDate(cI);

                // レコード検索
                for (int i = 0; i < dts.確定勤務票ヘッダ.Rows.Count; i++)
                {
                    JAFA_OCRDataSet.確定勤務票ヘッダRow r = (JAFA_OCRDataSet.確定勤務票ヘッダRow)dts.確定勤務票ヘッダ.Rows[i];
                    
                    //レコードの移動・表示
                    if (r.ID == hID)
                    {
                        cI = i;
                        showOcrData(cI);
                        break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 社員番号の指定
            if (Utility.StrtoInt(txtJmpCode.Text) == global.flgOff)
            {
                MessageBox.Show("ジャンプ先の職員番号を指定してください", "勤怠データジャンプ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 表示中のデータなら何もしない
            if (txtNo.Text == txtJmpCode.Text)
            {
                return;
            }

            //カレントデータの更新
            CurDataUpDate(cI);

            bool dhi = false;

            // レコード検索
            for (int i = 0; i < dts.確定勤務票ヘッダ.Rows.Count; i++)
            {
                JAFA_OCRDataSet.確定勤務票ヘッダRow r = (JAFA_OCRDataSet.確定勤務票ヘッダRow)dts.確定勤務票ヘッダ.Rows[i];

                //レコードの移動・表示
                if (r.社員番号 == Utility.StrtoInt(txtJmpCode.Text))
                {
                    cI = i;
                    showOcrData(cI);
                    dhi = true;
                    break;
                }
            }

            // 該当なし
            if (!dhi)
            {
                MessageBox.Show("該当するデータがありません","勤怠データ検索",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            n_width = B_WIDTH + (float)trackBar1.Value * 0.05f;
            n_height = B_HEIGHT + (float)trackBar1.Value * 0.05f;
            
            imgShow(mMat, n_width, n_height);
        }
    }
}
