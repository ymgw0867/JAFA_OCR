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

namespace JAFA_DATA.OCR
{
    public partial class frmPastDataViewer : Form
    {
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "過去勤怠データビューワー";    // アプリケーション表題

        public frmPastDataViewer()
        {
            InitializeComponent();

            adp.Fill(dts.メイトマスター);
            hAdp.Fill(dts.過去勤務票ヘッダ);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter adp = new JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter();
        JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter hAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
        
        #region グリッドカラム定義
        string colNum = "c0";           //　社員番号
        string colName = "c1";          //　社員名
        string colSzCode = "c2";        //　所属コード
        string colSzName = "c3";        //　所属名
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            // ウィンドウズ最小サイズ
            Utility.WindowsMinSize(this, this.Size.Width, this.Size.Height);

            // ウィンドウズ最大サイズ
            //Utility.WindowsMaxSize(this, this.Size.Width, this.Size.Height);

            // キャプション
            this.Text = appName;

            // 部門コンボロード
            cmbShozokuLoad();
            cmbBumonS.MaxDropDownItems = 20;
            cmbBumonS.SelectedIndex = -1;

            // DataGridViewの設定
            GridViewSetting(dg1);

            // 年月表示
            txtYear.Text = (global.cnfYear + Properties.Settings.Default.rekiHosei).ToString();
            txtMonth.Text = global.cnfMonth.ToString();
            txtYear.Focus();

            label10.Text = string.Empty;
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
                tempDGV.Height = 637;

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
                tempDGV.Columns.Add(colNum, "職員番号");
                tempDGV.Columns.Add(colName, "職員名");

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
                tempDGV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                ////ソート機能制限
                //for (int i = 0; i < tempDGV.Columns.Count; i++)
                //{
                //    // Alignment
                //    if (i == 0 || i == 3)
                //    {
                //        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                //    }

                //    // ソート機能制限
                //    //tempDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                //}

                // 各列幅指定
                tempDGV.Columns[colSzCode].Width = 100;
                tempDGV.Columns[colSzName].Width = 260;
                tempDGV.Columns[colNum].Width = 100;
                tempDGV.Columns[colName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
        private void GridViewShowData(DataGridView g, int sYY, int sMM, string szName, string sCode)
        {
            try
            {
                var s = dts.過去勤務票ヘッダ.Where(a => a.年 == sYY && a.月 == sMM)
                                           .OrderBy(a => a.所属コード)
                                           .ThenBy(a => a.社員番号);

                // 社員コード指定
                if (sCode != string.Empty)
                {
                    s = s.Where(a => a.社員番号 == Utility.StrtoInt(sCode)).OrderBy(a => a.社員番号);
                }

                // 所属コード指定
                if (szName != string.Empty)
                {
                    s = s.Where(a => a.所属名 == szName).OrderBy(a => a.社員番号);
                }
                
                g.Rows.Clear();
                int i = 0;

                foreach (var t in s)
                {
                    g.Rows.Add();
                    g[colSzCode, i].Value = t.所属コード;
                    g[colSzName, i].Value = t.所属名;
                    g[colNum, i].Value = t.社員番号.ToString();
                    g[colName, i].Value = t.社員名;

                    i++;
                }

                // 対象者数表示
                if (i > 0)
                {
                    label10.Text = "対象者：" + i.ToString("#,###") + " 名";
                }
                else
                {
                    label10.Text = string.Empty;
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
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        ///     所属コンボボックス所属名ロード </summary>
        /// -------------------------------------------------------------------
        private void cmbShozokuLoad()
        {
            foreach (var t in dts.メイトマスター.OrderBy(a => a.所属コード)
                                    .GroupBy(a => a.所属名)
                                    .Select(a => a.Key))
            {
                cmbBumonS.Items.Add(t);
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
            GridViewShowData(dg1, Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text), cmbBumonS.Text, txtShainNum.Text);
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

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string rID = string.Empty;

            rID = dg1[colNum, dg1.SelectedRows[0].Index].Value.ToString();

            if (rID != string.Empty)
            {
                this.Hide();
                OCR.frmPastData frm = new OCR.frmPastData(Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text),Utility.StrtoInt(rID));
                frm.ShowDialog();
                this.Show();
            }
        }

    }
}
