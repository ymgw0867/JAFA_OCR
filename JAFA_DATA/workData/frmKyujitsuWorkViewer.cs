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
    public partial class frmKyujitsuWorkViewer : Form
    {
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "休日出勤／休憩時間1時間超 一覧表";    // アプリケーション表題

        public frmKyujitsuWorkViewer()
        {
            InitializeComponent();

            // 2018/11/12 コメント化
            //hAdp.Fill(dts.過去勤務票ヘッダ);
            //mAdp.Fill(dts.過去勤務票明細);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter hAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
        JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter mAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter();

        #region グリッドカラム定義
        string colShokuin = "c1";   
        string colShokuinName = "c2";   
        string colSzCode = "c3";    
        string colSzName = "c4";
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

            // 年月表示
            txtYear.Text = global.cnfYear.ToString();
            txtMonth.Text = global.cnfMonth.ToString();
            txtYear.Focus();

            label10.Text = string.Empty;

            button1.Enabled = false;

            // 初期状態は休日出勤 2015/09/01
            radioButton1.Checked = true;
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
                tempDGV.Columns.Add(colSzCode, "所属コード");
                tempDGV.Columns.Add(colSzName, "所属名");
                tempDGV.Columns.Add(colShokuin, "職員コード");
                tempDGV.Columns.Add(colShokuinName, "職員名");
                tempDGV.Columns.Add(colDay, "日付");
                tempDGV.Columns.Add(colWeek, "曜日");
                tempDGV.Columns.Add(colSt, "始業時刻");
                tempDGV.Columns.Add(colEt, "終業時刻");
                tempDGV.Columns.Add(colRes, "休憩時間");
                tempDGV.Columns.Add(colJitsurou, "実労働時間");
                tempDGV.Columns.Add(colMtotal, "月間合計");
                
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
                    if (i == 0 || i == 2 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9 || i == 10)
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
                tempDGV.Columns[colShokuinName].Width = 140;
                tempDGV.Columns[colSzCode].Width = 100;
                tempDGV.Columns[colSzName].Width = 200;
                tempDGV.Columns[colDay].Width = 110;
                tempDGV.Columns[colWeek].Width = 64;
                tempDGV.Columns[colJitsurou].Width = 130;

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
        private void GridViewShowData(DataGridView g, int sYY, int sMM, string sCode, int sSel)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                hAdp.FillByYYMM(dts.過去勤務票ヘッダ, sYY * 100 + sMM);
                var s = dts.過去勤務票ヘッダ.Where(a => a.年 == sYY && a.月 == sMM)
                                           .OrderBy(a => a.所属コード)
                                           .ThenBy(a => a.社員番号);

                //// 所属名指定
                //if (szName != string.Empty)
                //{
                //    s = s.Where(a => a.対象職員所属名 == szName).OrderBy(a => a.対象月度)
                //                                                .ThenBy(a => a.対象職員所属コード)
                //                                               .ThenBy(a => a.対象職員コード);
                //}

                // 社員番号
                if (sCode != string.Empty)
                {
                    s = s.Where(a => a.社員番号 == Utility.StrtoInt(sCode))
                                           .OrderBy(a => a.所属コード)
                                           .ThenBy(a => a.社員番号);
                }

                int wHH = 0;
                int wMM = 0;
                int wCode = 0;

                g.Rows.Clear();
                int i = 0;

                foreach (var t in s)
                {
                    // 2018/11/12
                    mAdp.FillByHID(dts.過去勤務票明細, t.ヘッダID);

                    var im = dts.過去勤務票明細.Where(a => a.ヘッダID == t.ヘッダID).OrderBy(a => a.日付);

                    // 休日出勤一覧表のとき
                    if (sSel == global.flgOff)
                    {
                        im = im.Where(a => a.出勤区分 == "5").OrderBy(a => a.日付);
                    }

                    //foreach (var m in dts.過去勤務票明細.Where(a => a.ヘッダID == t.ヘッダID && a.出勤区分 == "5").OrderBy(a => a.日付))
                    foreach (var m in im)
                    {
                        // 休憩時間のとき、60分以下のときネグる
                        if (sSel == global.flgOn)
                        {
                            int ktm = Utility.StrtoInt(m.休憩開始時) * 60 + Utility.StrtoInt(m.休憩開始分);
                            if (ktm <= 60)
                            {
                                continue;
                            }
                        }

                        // 休日出勤一覧表で社員番号でブレーク発生したら月間合計を表示
                        if (sSel == global.flgOff)
                        {
                            if ((wCode != 0) && (wCode != t.社員番号))
                            {
                                int k = wMM / 60;
                                int j = wMM % 60;
                                g[colMtotal, i - 1].Value = (wHH + k).ToString().PadLeft(1, '0') + ":" + j.ToString().PadLeft(2, '0');
                                wMM = 0;
                                wHH = 0;
                            }
                        }

                        // 画面表示
                        g.Rows.Add();
                        g[colShokuin, i].Value = t.社員番号;
                        g[colShokuinName, i].Value = t.社員名;
                        g[colSzCode, i].Value = t.所属コード;
                        g[colSzName, i].Value = t.所属名;
                        g[colDay, i].Value = t.年.ToString() + "/" + t.月.ToString().PadLeft(2, '0') + "/" + m.日付.ToString().PadLeft(2, '0');

                        DateTime eDate = DateTime.Parse(t.年.ToString() + "/" + t.月.ToString() + "/" + m.日付.ToString());
                        g[colWeek, i].Value = ("日月火水木金土").Substring(int.Parse(eDate.DayOfWeek.ToString("d")), 1);

                        g[colSt, i].Value = m.開始時.ToString().PadLeft(1, '0') + ":" + m.開始分.ToString().PadLeft(2, '0');
                        g[colEt, i].Value = m.終了時.ToString().PadLeft(1, '0') + ":" + m.終了分.ToString().PadLeft(2, '0');
                        g[colRes, i].Value = m.休憩開始時.ToString().PadLeft(1, '0') + ":" + m.休憩開始分.ToString().PadLeft(2, '0');
                        g[colJitsurou, i].Value = m.実働時.ToString().PadLeft(1, '0') + ":" + m.実働分.ToString().PadLeft(2, '0');
                        g[colMtotal, i].Value = string.Empty;
                        
                        // 休日出勤一覧表のとき
                        if (sSel == global.flgOff)
                        {
                            wHH += m.実働時;
                            wMM += m.実働分;
                            wCode = t.社員番号;
                        }

                        i++;
                    }
                }

                // 対象者数表示
                if (i > 0)
                {
                    // 休日出勤一覧表のとき
                    if (sSel == global.flgOff)
                    {
                        int k = wMM / 60;
                        int j = wMM % 60;
                        g[colMtotal, i - 1].Value = (wHH + k).ToString().PadLeft(1, '0') + ":" + j.ToString().PadLeft(2, '0');
                    }

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
            int sSel = 0;

            // 休日出勤一覧表、休憩時間1時間超の選択 2015/09/01
            if (radioButton1.Checked)
            {
                sSel = global.flgOff;
                dg1.Columns[colMtotal].Visible = true;
                appName = txtYear.Text + "年" + txtMonth.Text + "月 休日出勤一覧表";
            }
            else if (radioButton2.Checked)
            {
                sSel = global.flgOn;
                dg1.Columns[colMtotal].Visible = false;
                appName = txtYear.Text + "年" + txtMonth.Text + "月 休憩時間1時間超一覧表";
            }
            
            // 一覧表表示
            GridViewShowData(dg1, Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text), txtShainNum.Text, sSel);
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
    }
}
