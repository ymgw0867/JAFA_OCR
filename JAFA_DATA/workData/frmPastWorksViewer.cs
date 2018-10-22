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
    public partial class frmPastWorksViewer : Form
    {
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "勤怠データビューワー";    // アプリケーション表題

        public frmPastWorksViewer()
        {
            InitializeComponent();

            adp.Fill(dts.勤怠データ);
            mAdp.Fill(dts.社員マスター);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter adp = new JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter();
        JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter mAdp = new JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter();

        #region グリッドカラム定義
        string colYear = "c0";
        string colMonth = "c39";   
        string colShokuin = "c1";   
        string colShokuinName = "c2";   
        string colSzCode = "c3";    
        string colSzName = "c4";    
        string colFutsu = "c5";     
        string colJitsuRodo = "c6";     
        string colZangyo = "c7";            
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

            // 部門コンボロード
            cmbShozokuLoad();
            cmbBumonS.MaxDropDownItems = 20;
            cmbBumonS.SelectedIndex = -1;

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
                tempDGV.Height = 637;

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
                tempDGV.Columns.Add(colFutsu, "普通出勤");
                tempDGV.Columns.Add(colJitsuRodo, "実労働時間");
                tempDGV.Columns.Add(colZangyo, "残業時間");
                tempDGV.Columns.Add(colShinya, "深夜時間");
                tempDGV.Columns.Add(colHoutei, "法定休日");
                tempDGV.Columns.Add(colKyujitsu, "休日");
                tempDGV.Columns.Add(colFurikyu, "振替休日");
                tempDGV.Columns.Add(colYukyu, "有休");
                tempDGV.Columns.Add(colHankyu, "半休");
                tempDGV.Columns.Add(colKekkin, "欠勤");
                tempDGV.Columns.Add(colSonotaTotal, "その他休暇計");
                tempDGV.Columns.Add(colKekkon, "結婚休暇");
                tempDGV.Columns.Add(colKibiki, "忌引");
                tempDGV.Columns.Add(colSeiri, "生理");
                tempDGV.Columns.Add(colKango, "看護");
                tempDGV.Columns.Add(colKaigoKyuka, "介護");
                tempDGV.Columns.Add(colRisai, "罹災");
                tempDGV.Columns.Add(colKakuri, "隔離");
                tempDGV.Columns.Add(colToku, "その他特休");
                tempDGV.Columns.Add(colKaigoKyushoku, "介護休職");
                tempDGV.Columns.Add(colSanzensango, "産前産後");
                tempDGV.Columns.Add(colIkujiKyushoku, "育児休職");
                tempDGV.Columns.Add(colKotsuhi, "交通費");
                tempDGV.Columns.Add(colNitto, "日当");
                tempDGV.Columns.Add(colShukuhakuhi, "宿泊費");
                tempDGV.Columns.Add(colYouShukkin, "要出勤日数");
                tempDGV.Columns.Add(colYukyuFuyo, "有給付与");
                tempDGV.Columns.Add(colYukyuKurikoshi, "有休繰越");
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
                tempDGV.Columns[colJitsuRodo].Width = 120;
                tempDGV.Columns[colSonotaTotal].Width = 120;
                tempDGV.Columns[colToku].Width = 120;
                tempDGV.Columns[colYouShukkin].Width = 120;
                tempDGV.Columns[colKyujitsu].Width = 70;
                tempDGV.Columns[colYukyu].Width = 70;
                tempDGV.Columns[colHankyu].Width = 70;
                tempDGV.Columns[colKekkin].Width = 70;
                tempDGV.Columns[colKibiki].Width = 70;
                tempDGV.Columns[colSeiri].Width = 70;
                tempDGV.Columns[colKango].Width = 70;
                tempDGV.Columns[colKaigoKyuka].Width = 70;
                tempDGV.Columns[colRisai].Width = 70;
                tempDGV.Columns[colKakuri].Width = 70;

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
        private void GridViewShowData(DataGridView g, int sYY, int sMM, int eYY, int eMM, string szName)
        {
            try
            {
                int sYYMM = Utility.StrtoInt(sYY.ToString() + sMM.ToString().PadLeft(2, '0'));
                int eYYMM = Utility.StrtoInt(eYY.ToString() + eMM.ToString().PadLeft(2, '0'));

                var s = dts.勤怠データ.Where(a => Utility.StrtoInt(a.対象月度) >= sYYMM && Utility.StrtoInt(a.対象月度) <= eYYMM)
                                           .OrderBy(a => a.対象月度)
                                           .ThenBy(a => a.対象職員所属コード)
                                           .ThenBy(a => a.対象職員コード);

                // 所属名指定
                if (szName != string.Empty)
                {
                    s = s.Where(a => a.対象職員所属名 == szName).OrderBy(a => a.対象月度)
                                                                .ThenBy(a => a.対象職員所属コード)
                                                               .ThenBy(a => a.対象職員コード);
                }

                // 社員番号開始
                if (txtShainNum.Text != string.Empty)
                {
                    s = s.Where(a => Utility.StrtoInt(a.対象職員コード.Substring(3, 5)) >= Utility.StrtoInt(txtShainNum.Text))
                                           .OrderBy(a => a.対象月度)
                                           .ThenBy(a => a.対象職員所属コード)
                                           .ThenBy(a => a.対象職員コード);
                }

                // 社員番号終了
                if (txtShainNum2.Text != string.Empty)
                {
                    s = s.Where(a => Utility.StrtoInt(a.対象職員コード.Substring(3, 5)) <= Utility.StrtoInt(txtShainNum2.Text))
                                           .OrderBy(a => a.対象月度)
                                           .ThenBy(a => a.対象職員所属コード)
                                           .ThenBy(a => a.対象職員コード);
                }
                
                g.Rows.Clear();
                int i = 0;

                foreach (var t in s)
                {
                    g.Rows.Add();
                    g[colYear, i].Value = t.対象月度.Substring(0, 4);
                    g[colMonth, i].Value = t.対象月度.Substring(4, 2);
                    g[colShokuin, i].Value = t.対象職員コード.Substring(3, 5);
                    g[colShokuinName, i].Value = t.対象職員名;
                    g[colSzCode, i].Value = t.対象職員所属コード.Substring(3, t.対象職員所属コード.Length - 3);
                    g[colSzName, i].Value = t.対象職員所属名;
                    g[colFutsu, i].Value = t.普通出勤日数;
                    g[colJitsuRodo, i].Value = t.実労働時間.ToString("#,0");
                    g[colZangyo, i].Value = t.残業時間.ToString("#,0");
                    g[colShinya, i].Value = t.深夜時間;
                    g[colHoutei, i].Value = t.法定休日出勤日数;
                    g[colKyujitsu, i].Value = t.休日日数;
                    g[colFurikyu, i].Value = t.振替休日日数;
                    g[colHankyu, i].Value = t.有給半日;
                    g[colYukyu, i].Value = t.有給休暇;
                    g[colKekkin, i].Value = t.欠勤日数;
                    g[colSonotaTotal, i].Value = t.その他休暇休職合計日数;
                    g[colKekkon, i].Value = t.結婚休暇日数;
                    g[colKibiki, i].Value = t.忌引休暇日数;
                    g[colSeiri, i].Value = t.生理休暇日数;
                    g[colKango, i].Value = t.看護休暇日数;
                    g[colKaigoKyuka, i].Value = t.介護休暇日数;
                    g[colRisai, i].Value = t.罹災休暇日数;
                    g[colKakuri, i].Value = t.隔離休暇日数;
                    g[colToku, i].Value = t.その他の特別休暇日数;
                    g[colKaigoKyushoku, i].Value = t.介護休職日数;
                    g[colSanzensango, i].Value = t.産前産後休暇日数;
                    g[colIkujiKyushoku, i].Value = t.育児休職日数;
                    g[colKotsuhi, i].Value = t.交通費.ToString("#,0");
                    g[colNitto, i].Value = t.日当.ToString("#,0");
                    g[colShukuhakuhi, i].Value = t.宿泊費.ToString("#,0");
                    g[colYouShukkin, i].Value = t.要出勤日数;
                    g[colYukyuFuyo, i].Value = t.有休付与日数;
                    g[colYukyuKurikoshi, i].Value = t.有休繰越日数;
                    g[colYukyuFlg, i].Value = t.有休付与対象フラグ;

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
            foreach (var t in dts.社員マスター.OrderBy(a => a.所属コード)
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
            
            if ((txtShainNum.Text != string.Empty && txtShainNum2.Text != string.Empty))
            {
                if (Utility.StrtoInt(txtShainNum.Text) > Utility.StrtoInt(txtShainNum2.Text))
                {
                    MessageBox.Show("社員番号範囲が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtYear2.Focus();
                    return false;
                }
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
            GridViewShowData(dg1, Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text), Utility.StrtoInt(txtYear2.Text), Utility.StrtoInt(txtMonth2.Text), cmbBumonS.Text);
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
            MyLibrary.CsvOut.GridView(dg1, "勤怠データ");
        }
    }
}
