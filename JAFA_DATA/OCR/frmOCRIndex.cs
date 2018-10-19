using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAFA_DATA.Common;

namespace JAFA_DATA.OCR
{
    public partial class frmOCRIndex : Form
    {
        public frmOCRIndex()
        {
            InitializeComponent();

            adp.Fill(dts.確定勤務票ヘッダ);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter adp = new JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter();
        
        private void frmOCRIndex_Load(object sender, EventArgs e)
        {
            Utility.WindowsMaxSize(this, this.Width, this.Height);
            Utility.WindowsMinSize(this, this.Width, this.Height);
            
            // データグリッドビュー定義
            GridViewSetting(dataGridView1);

            // データグリッドビュー表示
            GridViewShowData(dataGridView1);
        }
        
        #region グリッドカラム定義
        string colShokuin = "c1";
        string colShokuinName = "c2";
        string colSzCode = "c3";
        string colSzName = "c4";
        string colID = "c5";
        #endregion

        /// ------------------------------------------------------------------
        /// <summary>
        ///     データグリッドビューの定義を行います </summary>
        /// <param name="tempDGV">
        ///     データグリッドビューオブジェクト</param>
        /// ------------------------------------------------------------------
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
                tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = false;

                // カラム定義
                tempDGV.Columns.Add(colShokuin, "職員コード");
                tempDGV.Columns.Add(colShokuinName, "職員名");
                tempDGV.Columns.Add(colSzCode, "所属コード");
                tempDGV.Columns.Add(colSzName, "所属名");
                tempDGV.Columns.Add(colID, "ID");

                // IDは非表示
                tempDGV.Columns[colID].Visible = false;

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
                tempDGV.Columns[colShokuin].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                tempDGV.Columns[colSzCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                              
                // 各列幅指定
                tempDGV.Columns[colShokuin].Width = 100;
                tempDGV.Columns[colShokuinName].Width = 200;
                tempDGV.Columns[colSzCode].Width = 100;
                tempDGV.Columns[colSzName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // 編集可否
                tempDGV.ReadOnly = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewShowData(DataGridView g)
        {
            try
            {
                g.Rows.Clear();
                int i = 0;

                foreach (var t in dts.確定勤務票ヘッダ.OrderBy(a => a.社員番号))
                {
                    g.Rows.Add();
                    g[colShokuin, i].Value = t.社員番号.ToString();
                    g[colShokuinName, i].Value = t.社員名;
                    g[colSzCode, i].Value = t.所属コード.ToString();
                    g[colSzName, i].Value = t.所属名.ToString();
                    g[colID, i].Value = t.ID.ToString();

                    i++;
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
                MessageBox.Show("確定データが存在しませんでした", "データなし", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // ID初期化
            hdID = string.Empty;

            // 確認
            string msg = dataGridView1[colShokuinName, e.RowIndex].Value.ToString() + " が選択されました。よろしいですか？";
            if (MessageBox.Show(msg,"確認",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            // ID取得
            hdID = dataGridView1[colID, e.RowIndex].Value.ToString();

            // 閉じる
            this.Close();
        }

        // 選択したデータのID
        public string hdID { get; set; }

    }
}
