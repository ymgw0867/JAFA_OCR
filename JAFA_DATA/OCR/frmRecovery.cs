using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Odbc;
using JAFA_DATA.Common;

namespace JAFA_DATA.OCR
{
    public partial class frmRecovery : Form
    {
        //所属コード設定桁数
        int ShozokuLen = 0;
         
        string _ComNo = string.Empty;                   // 会社番号
        string _ComName = string.Empty;                 // 会社名
        string _ComDatabeseName = string.Empty;         // 会社データベース名

        const int MODE_ALL = 0;                         // 全員
        const int MODE_ZMIKAISHU = 1;                   // 前半未回収
        const int MODE_ZSHORICHU = 2;                   // 前半処理中
        const int MODE_ZSHORIZUMI = 3;                  // 前半処理済み
        const int MODE_KMIKAISHU = 4;                   // 後半未回収
        const int MODE_KSHORICHU = 5;                   // 後半処理中
        const int MODE_KSHORIZUMI = 6;                  // 後半処理済み
        const int MODE_SHORIZUMI = 7;                   // 月間処理済み
        const int MODE_MISHORI = 8;                     // 月間未処理

        string appName = "メイト出勤簿処理実施状況";      // アプリケーション表題

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter mAdp = new JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter();
        JAFA_OCRDataSetTableAdapters.確定勤務票明細TableAdapter kmAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票明細TableAdapter();
        JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter khAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter();
        JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter phAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
        JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter pmAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter();
        JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter jaAdp = new JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter();

        JAFA_DATADataSet dtsData = new JAFA_DATADataSet();
        JAFA_DATADataSetTableAdapters.勤務票ヘッダTableAdapter hDtAdp = new JAFA_DATADataSetTableAdapters.勤務票ヘッダTableAdapter();
        JAFA_DATADataSetTableAdapters.勤務票明細TableAdapter mDtAdp = new JAFA_DATADataSetTableAdapters.勤務票明細TableAdapter();
        
        public frmRecovery()
        {
            InitializeComponent();

            // データセットへデータを読み込む
            mAdp.Fill(dts.メイトマスター);
            khAdp.Fill(dts.確定勤務票ヘッダ);
            kmAdp.Fill(dts.確定勤務票明細);
            phAdp.Fill(dts.過去勤務票ヘッダ);
            pmAdp.Fill(dts.過去勤務票明細);
            jaAdp.Fill(dts.勤怠データ);

            hDtAdp.Fill(dtsData.勤務票ヘッダ);
            mDtAdp.Fill(dtsData.勤務票明細);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ウィンドウズ最小サイズ
            Utility.WindowsMinSize(this, this.Size.Width, this.Size.Height);

            //ウィンドウズ最大サイズ
            //Utility.WindowsMaxSize(this, this.Size.Width, this.Size.Height);

            //所属コードの桁数を取得する
            string sqlSTRING = string.Empty;
            
            // 部門コンボロード
            cmbShozokuLoad();
            cmbBumonS.MaxDropDownItems = 20;
            cmbBumonS.SelectedIndex = -1;

            //DataGridViewの設定
            GridViewSetting(dg1);

            // 対象年月を取得
            txtYear.Text = global.cnfYear.ToString();
            txtMonth.Text = global.cnfMonth.ToString();

            txtYear.Focus();
            
            //元号表示　2011/03/24
            label5.Text = Properties.Settings.Default.gengou;

            // 表示選択コンボボックス
            comboBox1.Items.Add("すべて表示");
            comboBox1.Items.Add("前半ＯＣＲ未実施");
            comboBox1.Items.Add("前半処理中");
            comboBox1.Items.Add("前半データ確定済み");
            comboBox1.Items.Add("後半ＯＣＲ未実施");
            comboBox1.Items.Add("後半処理中");
            comboBox1.Items.Add("後半データ確定済み");
            comboBox1.Items.Add("月間処理済");
            comboBox1.Items.Add("月間未処理");
            comboBox1.SelectedIndex = 0;

            // CSV出力ボタン
            button1.Enabled = false;
        }

        private void cmbShozokuLoad()
        {
            foreach (var t in dts.メイトマスター.OrderBy(a => a.所属コード)
                                    .GroupBy(a => a.所属名)
                                    .Select(a => a.Key))
            {
                cmbBumonS.Items.Add(t);
            }
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
                tempDGV.Height = 532;

                // 奇数行の色
                //tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;

                // 各列幅指定
                tempDGV.Columns.Add("col1", "コード");
                tempDGV.Columns.Add("col2", "所属");
                tempDGV.Columns.Add("col3", "職員番号");
                tempDGV.Columns.Add("col4", "職員名");
                tempDGV.Columns.Add("col5", "前半");
                tempDGV.Columns.Add("col6", "状況");
                tempDGV.Columns.Add("col7", "後半");
                tempDGV.Columns.Add("col8", "状況");
                tempDGV.Columns.Add("col9", "CSVデータ作成");
                tempDGV.Columns.Add("col10", "作成日");

                tempDGV.Columns[0].Width = 80;
                tempDGV.Columns[1].Width = 220;
                tempDGV.Columns[2].Width = 100;
                tempDGV.Columns[3].Width = 180;
                tempDGV.Columns[4].Width = 80;
                tempDGV.Columns[5].Width = 120;
                tempDGV.Columns[6].Width = 80;
                tempDGV.Columns[7].Width = 120;
                tempDGV.Columns[8].Width = 160;
                tempDGV.Columns[9].Width = 160;

                //tempDGV.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                tempDGV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                tempDGV.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                tempDGV.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                tempDGV.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                tempDGV.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // 編集可否
                tempDGV.ReadOnly = false;
                tempDGV.Columns[0].ReadOnly = true;
                tempDGV.Columns[1].ReadOnly = true;
                tempDGV.Columns[2].ReadOnly = true;
                tempDGV.Columns[3].ReadOnly = true;
                tempDGV.Columns[4].ReadOnly = true;
                tempDGV.Columns[5].ReadOnly = true;
                tempDGV.Columns[6].ReadOnly = true;
                tempDGV.Columns[7].ReadOnly = true;
                tempDGV.Columns[8].ReadOnly = true;
                tempDGV.Columns[9].ReadOnly = true;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = true;

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

                ////ソート機能制限
                //for (int i = 0; i < tempDGV.Columns.Count; i++)
                //{
                //    tempDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                //}

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// ----------------------------------------------------------------------
        /// <summary>
        ///     グリッドビューへ社員情報を表示する </summary>
        /// <param name="tempDGV">
        ///     DataGridViewオブジェクト名</param>
        /// <param name="sCode">
        ///     指定所属コード</param>
        /// ----------------------------------------------------------------------
        private void GridViewShowData(DataGridView tempDGV, string sCode, int sMode)
        {
            // カーソル待機中
            this.Cursor = Cursors.WaitCursor;

            // データグリッド行クリア
            tempDGV.Rows.Clear();
            int iX = 0;

            try 
	        {
                // 対象年月
                int syymm = (Utility.StrtoInt(txtYear.Text) + Properties.Settings.Default.rekiHosei) * 100 + Utility.StrtoInt(txtMonth.Text);

                // メイトマスター
                // 当月以降の退職者も含める 2016/01/08
                var sss = dts.メイトマスター.Where(a => a.退職区分 == global.flgOff || 
                                            (a.退職区分 == global.flgOn && 
                                            (a.退職年月日.Year * 100 + a.退職年月日.Month) >= syymm))
                                            .OrderBy(a => a.所属コード).ThenBy(a => a.職員コード);

                if (cmbBumonS.SelectedIndex != -1)
                {
                    sss = sss.Where(a => a.所属名 == cmbBumonS.Text).OrderBy(a => a.職員コード);
                }
                
                foreach (JAFA_OCRDataSet.メイトマスターRow t in sss)
                {
                    // 前半処理
                    int zMode = 0;
                    bool zRec = false;

                    // 後半処理
                    int kMode = 0;
                    bool kRec = false;

                    // 月間処理
                    int mMode = 0;
                    bool mRec = false;

                    // 処理年月日
                    string kDt = string.Empty;

                    // ヘッダＩＤ
                    string hID = string.Empty;

                    // ヘッダ情報取得
                    var ss = dts.確定勤務票ヘッダ.Where(a => a.年 == Utility.StrtoInt(txtYear.Text) && 
                                                            a.月 == Utility.StrtoInt(txtMonth.Text) && 
                                                            a.社員番号 == t.職員コード);
          
                    foreach (var item in ss)
                    {
                        hID = item.ヘッダID;
                    }

                    // 前半処理状況を調べる
                    if (hID != string.Empty)
                    {
                        if (dts.確定勤務票明細.Any(a => a.ヘッダID == hID && a.日付 <= 15))
                        {
                            // 前半確定済み
                            zMode = MODE_ZSHORIZUMI;
                            zRec = true;
                        }
                    }
                    else  // 確定データなし
                    {
                        if (dtsData.勤務票ヘッダ.Any(a => a.年 == Utility.StrtoInt(txtYear.Text) &&
                                                            a.月 == Utility.StrtoInt(txtMonth.Text) &&
                                                            a.社員番号 == t.職員コード && 
                                                            a.前半処理 == global.flgOn))
                        {
                            // 現在処理中
                            zMode = MODE_ZSHORICHU;
                            zRec = true;
                        }
                        else
                        {
                            // 前半未回収
                            zMode = MODE_ZMIKAISHU;
                            zRec = true;
                        }
                    }

                    // 後半処理状況を調べる
                    if (hID != string.Empty)
                    {
                        if (dts.確定勤務票明細.Any(a => a.ヘッダID == hID && a.日付 >= 16))
                        {
                            // 後半確定済み
                            kMode = MODE_KSHORIZUMI;
                            kRec = true;
                        }
                        else
                        {
                            // 後半の確定データなし
                            if (dtsData.勤務票ヘッダ.Any(a => a.年 == Utility.StrtoInt(txtYear.Text) &&
                                                                a.月 == Utility.StrtoInt(txtMonth.Text) &&
                                                                a.社員番号 == t.職員コード &&
                                                                a.後半処理 == global.flgOn))
                            {
                                // 現在処理中
                                kMode = MODE_KSHORICHU;
                                kRec = true;
                            }
                            else
                            {
                                // 後半未回収
                                kMode = MODE_KMIKAISHU;
                                kRec = true;
                            }
                        }
                    }
                    else  // 確定データなし
                    {
                        if (dtsData.勤務票ヘッダ.Any(a => a.年 == Utility.StrtoInt(txtYear.Text) &&
                                                            a.月 == Utility.StrtoInt(txtMonth.Text) &&
                                                            a.社員番号 == t.職員コード &&
                                                            a.後半処理 == global.flgOn))
                        {
                            // 現在処理中
                            kMode = MODE_KSHORICHU;
                            kRec = true;
                        }
                        else
                        {
                            // 後半未回収
                            kMode = MODE_KMIKAISHU;
                            kRec = true;
                        }
                    }

                    // 当月処理済み（JAメイトOCRデータ作成済み）か調べる
                    string kymd = string.Empty;
                    string yymm = (Utility.StrtoInt(txtYear.Text) + Properties.Settings.Default.rekiHosei).ToString() + txtMonth.Text.Trim().PadLeft(2, '0');
                    string sNum = global.ROK + t.職員コード.ToString().PadLeft(5, '0');

                    if (dts.勤怠データ.Any(a => a.対象月度 == yymm && a.対象職員コード == sNum))
                    {
                        var k = dts.過去勤務票ヘッダ.Where(a => a.年 == Utility.StrtoInt(txtYear.Text) + Properties.Settings.Default.rekiHosei &&
                            a.月 == Utility.StrtoInt(txtMonth.Text) && a.社員番号 == t.職員コード);

                        foreach (var tt in k)
                        {
                            kymd = tt.更新年月日.ToString();
                        }

                        // 処理済み
                        mMode = MODE_SHORIZUMI;
                        mRec = true;

                        // 前半確定済み
                        zMode = MODE_ZSHORIZUMI;
                        zRec = true;

                        // 後半確定済み
                        kMode = MODE_KSHORIZUMI;
                        kRec = true;
                    }
                    else
                    {
                        mMode = MODE_MISHORI;
                        mRec = false;
                    }

                    // データ表示
                    switch (sMode)
                    {
                        case MODE_ZMIKAISHU:    // 前半未回収のみ
                            if (zMode == MODE_ZMIKAISHU)
                            {
                                //データグリッドにデータを表示する
                                gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                                iX++;
                            }
                            break;

                        case MODE_ZSHORICHU:    // 前半処理中のみ
                            if (zMode == MODE_ZSHORICHU)
                            {
                                //データグリッドにデータを表示する
                                gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                                iX++;
                            }
                            break;

                        case MODE_ZSHORIZUMI:    // 前半処理済みのみ
                            if (zMode == MODE_ZSHORIZUMI)
                            {
                                //データグリッドにデータを表示する
                                gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                                iX++;
                            }
                            break;

                        case MODE_KMIKAISHU:    // 後半未回収のみ
                            if (kMode == MODE_KMIKAISHU)
                            {
                                //データグリッドにデータを表示する
                                gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                                iX++;
                            }
                            break;

                        case MODE_KSHORICHU:    // 後半処理中のみ
                            if (kMode == MODE_KSHORICHU)
                            {
                                //データグリッドにデータを表示する
                                gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                                iX++;
                            }
                            break;

                        case MODE_KSHORIZUMI:    // 後半処理済みのみ
                            if (kMode == MODE_KSHORIZUMI)
                            {
                                //データグリッドにデータを表示する
                                gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                                iX++;
                            }
                            break;

                        case MODE_SHORIZUMI:    // 月間処理済みのみ
                            if (mMode == MODE_SHORIZUMI)
                            {
                                //データグリッドにデータを表示する
                                gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                                iX++;
                            }
                            break;

                        case MODE_MISHORI:      // 月間未処理のみ
                            if (mMode == MODE_MISHORI)
                            {
                                //データグリッドにデータを表示する
                                gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                                iX++;
                            }
                            break;

                        case MODE_ALL:          // 全表示
                            //データグリッドにデータを表示する
                            gridRowAdd(tempDGV, iX, zMode, kMode, mMode, t, kymd);
                            iX++;
                            break;
                    }
                }
            
                tempDGV.CurrentCell = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK);
            }
            finally
            {
                // カーソルを戻す
                this.Cursor = Cursors.Default;
            }

            // 該当するデータがないとき
            if (tempDGV.RowCount == 0)
            {
                MessageBox.Show("該当するデータはありませんでした", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void gridRowAdd(DataGridView d, int iX, int zMode, int kMode, int mMode, JAFA_OCRDataSet.メイトマスターRow t, string kYmd)
        {
            //データグリッドにデータを表示する
            d.Rows.Add();
            GridViewCellData(d, iX, t);
            GridRowData(d, iX, zMode, kMode, mMode, kYmd);
        }


        /// <summary>
        /// 回収状況項目をグリッドに表示する
        /// </summary>
        /// <param name="dg">データグリッドオブジェクト</param>
        /// <param name="r">行インデックス</param>
        /// <param name="sMode">回収ステータス</param>
        /// <param name="sDate">処理完了日付</param>
        private void GridRowData(DataGridView g, int r, int zMode, int kMode, int mMode, string kYmd)
        {
            // 前半処理状況
            switch (zMode)
            {
                // 未回収
                case MODE_ZMIKAISHU:
                    g[4, r].Value = "－";
                    g[5, r].Value = "OCR未実施";
                    g.Rows[r].DefaultCellStyle.ForeColor = Color.Red;
                    break;

                // 処理中
                case MODE_ZSHORICHU:
                    g[4, r].Value = "△";
                    g[5, r].Value = "処理中";
                    g.Rows[r].DefaultCellStyle.ForeColor = Color.Blue;
                    break;

                // 確定済み
                case MODE_ZSHORIZUMI:
                    g[4, r].Value = "○";
                    g[5, r].Value = "確定済み";
                    g.Rows[r].DefaultCellStyle.ForeColor = Color.Black;
                    break;
            }

            // 後半処理状況
            switch (kMode)
            {
                // 未回収
                case MODE_KMIKAISHU:
                    g[6, r].Value = "－";
                    g[7, r].Value = "OCR未実施";
                    g.Rows[r].DefaultCellStyle.ForeColor = Color.Red;
                    break;

                // 処理中
                case MODE_KSHORICHU:
                    g[6, r].Value = "△";
                    g[7, r].Value = "処理中";
                    g.Rows[r].DefaultCellStyle.ForeColor = Color.Blue;
                    break;

                // 確定済み
                case MODE_KSHORIZUMI:
                    g[6, r].Value = "○";
                    g[7, r].Value = "確定済み";
                    g.Rows[r].DefaultCellStyle.ForeColor = Color.Black;
                    break;
            }

            // 月間状況
            switch (mMode)
            {
                // 処理済み
                case MODE_SHORIZUMI:
                    g[8, r].Value = "○";
                    g[9, r].Value = kYmd;
                    g.Rows[r].DefaultCellStyle.ForeColor = Color.Black;
                    break;

                // 未処理
                case MODE_MISHORI:
                    g[8, r].Value = "－";
                    g[9, r].Value = string.Empty;
                    g.Rows[r].DefaultCellStyle.ForeColor = Color.Red;
                    break;
            }

        }

        /// <summary>
        /// データグリッドに表示データをセットする
        /// </summary>
        /// <param name="tempDGV">datagridviewオブジェクト名</param>
        /// <param name="iX">Row№</param>
        /// <param name="dR">データリーダーオブジェクト名</param>
        private void GridViewCellData(DataGridView g, int iX, JAFA_OCRDataSet.メイトマスターRow t)
        {
            g[0, iX].Value = t.所属コード.ToString();
            g[1, iX].Value = t.所属名;
            g[2, iX].Value = t.職員コード.ToString().PadLeft(5, '0');
            g[3, iX].Value = t.氏名;
        }

        private Boolean ErrCheck()
        {
            if (Utility.NumericCheck(txtYear.Text) == false)
            {
                MessageBox.Show("年は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (MessageBox.Show("終了します。よろしいですか？",appName,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            this.Dispose();
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            DataSelect(comboBox1.SelectedIndex);
        }

        private void DataSelect(int sMode)
        {
            string sDate = string.Empty;
            string sCode;

            //エラーチェック
            if (ErrCheck() == false) return;

            //基準日付
            sDate = (int.Parse(txtYear.Text) + Properties.Settings.Default.rekiHosei).ToString() + "/" + txtMonth.Text + "/01";

            // 部門コード取得
            sCode = "";
            if (cmbBumonS.Text == string.Empty)
            {
                sCode = "";
            }
            else
            {
                //Utility.ComboBumon cmbs = new Utility.ComboBumon();
                //cmbs = (Utility.ComboBumon)cmbBumonS.SelectedItem;
                //sCode = cmbs.ID;
            }

            //データ表示
            GridViewShowData(dg1, sCode, sMode);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyLibrary.CsvOut.GridView(dg1, "メイト出勤簿処理状況");
        }

    }
}
