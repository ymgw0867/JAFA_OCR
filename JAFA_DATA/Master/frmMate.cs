using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAFA_DATA.Common;

namespace JAFA_DATA.Master
{
    public partial class frmMate : Form
    {
        // マスター名
        string msName = "社員";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 社員マスターテーブルアダプター生成
        JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter adp = new JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter();

        // データテーブル生成
        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();

        public frmMate()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adp.Fill(dts.社員マスター);

            txtSzName.AutoSize = false;
            txtSzName.Height = 28;
        }

        private void frm_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // データグリッド定義
            GridViewSetting(dg);

            // データグリッドビューにデータを表示します
            GridViewShow(dg);

            // 画面初期化
            DispInitial();

            //// 週開始曜日コンボ
            //cmbYoubi.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        
        ////カラム定義
        //string C_Code = "col1";
        //string C_Name = "col2";
        //string C_ShukkinKbn = "col3";
        //string C_ShainKbn = "col4";
        //string C_PartKbn = "col5";
        //string C_Memo = "col6";
        //string C_Date = "col7";

        /// <summary>
        /// データグリッドビューの定義を行います
        /// </summary>
        /// <param name="tempDGV">データグリッドビューオブジェクト</param>
        private void GridViewSetting(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更する

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

                // 行の高さ
                tempDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                tempDGV.ColumnHeadersHeight = 20;
                tempDGV.RowTemplate.Height = 20;

                // 全体の高さ
                tempDGV.Height = 261;

                // 奇数行の色
                tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

                ////各列幅指定
                //tempDGV.Columns.Add(C_Code, "番号");
                //tempDGV.Columns.Add(C_Name, "勤怠区分名称");
                //tempDGV.Columns.Add(C_ShukkinKbn, "出勤区分");
                //tempDGV.Columns.Add(C_ShainKbn, "社員");
                //tempDGV.Columns.Add(C_PartKbn, "パート");
                //tempDGV.Columns.Add(C_Memo, "備考");
                //tempDGV.Columns.Add(C_Date, "更新日");

                //tempDGV.Columns[C_Code].Width = 70;
                //tempDGV.Columns[C_Name].Width = 160;
                //tempDGV.Columns[C_ShukkinKbn].Width = 80;
                //tempDGV.Columns[C_ShainKbn].Width = 60;
                //tempDGV.Columns[C_PartKbn].Width = 60;
                //tempDGV.Columns[C_Memo].Width = 300;
                //tempDGV.Columns[C_Date].Width = 150;

                //tempDGV.Columns[C_Memo].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = false;

                // 編集不可とする
                tempDGV.ReadOnly = true;

                // 追加行表示しない
                tempDGV.AllowUserToAddRows = false;

                // データグリッドビューから行削除を禁止する
                tempDGV.AllowUserToDeleteRows = false;

                // 手動による列移動の禁止
                tempDGV.AllowUserToOrderColumns = false;

                // 列サイズ変更可
                tempDGV.AllowUserToResizeColumns = true;

                // 行サイズ変更禁止
                tempDGV.AllowUserToResizeRows = false;

                // 行ヘッダーの自動調節
                //tempDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                //TAB動作
                tempDGV.StandardTab = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// データグリッドビューにデータを表示します
        /// </summary>
        /// <param name="tempGrid">データグリッドビューオブジェクト名</param>
        private void GridViewShow(DataGridView tempGrid)
        {
            try
            {
                // データソースにデータテーブルをバインド
                tempGrid.DataSource = dts.社員マスター;

                // 列幅調整
                tempGrid.Columns[0].Width = 70;
                tempGrid.Columns[1].Width = 140;
                tempGrid.Columns[2].Width = 120;
                tempGrid.Columns[3].Width = 100;
                tempGrid.Columns[4].Width = 150;

                //tempGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                tempGrid.CurrentCell = null;

                // ID列で自動ソートする
                tempGrid.Sort(tempGrid.Columns[0], ListSortDirection.Ascending);

                // IDコード列は非表示とする
                //tempGrid.Columns[0].Visible = false;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message); 
            }
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void DispInitial()
        {
            fMode.Mode = global.FORM_ADDMODE;
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFuri.Text = string.Empty;
            txtSzCode.Text = string.Empty;
            txtSzName.Text = string.Empty;
            dtNyusho.Checked = false;
            dtChousei.Checked = false;
            dtTaishoku.Checked = false;
            txtWshoTei.Text = string.Empty;
            rbTaishoku.Checked = false;
            rbZaiseki.Checked = true;
            //cmbYoubi.SelectedIndex = -1;
            txtFuyoTsuki.Text = string.Empty;
            txtMemo.Text = string.Empty;
            
            btnUpdate.Enabled = true;
            btnDel.Enabled = false;
            btnClear.Enabled = false;
            txtCode.Enabled = true;

            txtsNum.Text = string.Empty;

            // 2018/10/19
            cmbShortWork.SelectedIndex = -1;
            cmbShain.SelectedIndex = -1;
            cmbFarm.SelectedIndex = -1;

            txtCode.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //エラーチェック
            if (!fDataCheck()) return;

            switch (fMode.Mode)
            {
                // 新規登録
                case global.FORM_ADDMODE:

                    // 確認
                    if (MessageBox.Show(txtName.Text + "を登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセットにデータを追加します
                    dts.社員マスター.Add社員マスターRow(setMateNewRow(dts.社員マスター.New社員マスターRow()));
                    
                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(txtName.Text + "を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセット更新
                    //JAFA_OCRDataSet.社員マスターRow r = dts.社員マスター.Single(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                    //                           a.職員コード == Utility.StrtoInt(fMode.ID));

                    //if (!r.HasErrors)
                    //{
                    //    r = setMateNewRow(r);
                    //}
                    //else
                    //{
                    //    MessageBox.Show(fMode.ID + "がキー不在です：データの更新に失敗しました","更新エラー",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    //}

                    JAFA_OCRDataSet.社員マスターRow r = dts.社員マスター.Single(a => a.職員コード == Utility.StrtoInt(fMode.ID));

                    r = setMateNewRow(r);

                    //if (!r.HasErrors)
                    //{
                    //    r = setMateNewRow(r);
                    //}
                    //else
                    //{
                    //    MessageBox.Show(fMode.ID + "がキー不在です：データの更新に失敗しました", "更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //}

                    break;

                default:
                    break;
            }

            // マスター更新
            adp.Update(dts.社員マスター);
            adp.Fill(dts.社員マスター);

            // グリッドデータ再表示
            GridViewShow(dg);

            // 画面データ消去
            DispInitial();      
        }


        private JAFA_OCRDataSet.社員マスターRow setMateNewRow(JAFA_OCRDataSet.社員マスターRow r)
        {
            r.職員コード = Utility.StrtoInt(txtCode.Text);
            r.氏名 = txtName.Text;
            r.フリガナ = txtFuri.Text;
            r.所属コード = int.Parse(txtSzCode.Text);
            r.所属名 = txtSzName.Text;

            if (dtNyusho.Checked)
            {
                r.入所年月日 = DateTime.Parse(dtNyusho.Value.ToShortDateString());
            }
            else
            {
                r.入所年月日 = global.dtNon;
            }

            if (dtChousei.Checked)
            {
                r.調整年月日 = DateTime.Parse(dtChousei.Value.ToShortDateString());
            }
            else
            {
                r.調整年月日 = global.dtNon;
            }
            
            if (dtTaishoku.Checked)
            {
                r.退職年月日 = DateTime.Parse(dtTaishoku.Value.ToShortDateString());
            }
            else
            {
                r.退職年月日 = global.dtNon;
            }

            r.週所定労働日数 = int.Parse(txtWshoTei.Text);

            if (rbZaiseki.Checked)
            {
                r.退職区分 = global.flgOff;
            }
            else if (rbTaishoku.Checked)
            {
                r.退職区分 = global.flgOn;
            }

            //r.週開始曜日 = cmbYoubi.SelectedIndex;

            r.有給付与月 = int.Parse(txtFuyoTsuki.Text);
            r.備考 = txtMemo.Text;
            r.更新年月日 = DateTime.Now;

            r.短時間勤務 = cmbShortWork.SelectedIndex;   // 2018/10/22
            r.社員区分 = cmbShain.SelectedIndex;        // 2018/10/22
            r.農業従事 = cmbFarm.SelectedIndex;         // 2018/10/22

            return r;
        }


        //登録データチェック
        private Boolean fDataCheck()
        {
            try
            {
                //ゼロは不可
                if (this.txtCode.Text.ToString() == "0")
                {
                    this.txtCode.Focus();
                    throw new Exception("ゼロは登録できません");
                }

                //登録モードのとき番号をチェック
                if (fMode.Mode == global.FORM_ADDMODE)
                {
                    //登録済みコードか調べる
                    var s = dts.出勤区分.Where(a => a.ID.ToString() == txtCode.Text);
                    if (s.Count() > 0)
                    {
                        txtCode.Focus();
                        throw new Exception("既に登録済みのコードです");
                    }
                }

                // 氏名チェック
                if (txtName.Text.Trim() == string.Empty)
                {
                    txtName.Focus();
                    throw new Exception("氏名を入力してください");
                }

                // フリガナチェック
                if (txtFuri.Text.Trim() == string.Empty)
                {
                    txtFuri.Focus();
                    throw new Exception("フリガナを入力してください");
                }

                // 所属コードチェック
                if (txtSzCode.Text.Trim() == string.Empty)
                {
                    txtSzCode.Focus();
                    throw new Exception("所属コードを入力してください");
                }

                // 所属チェック
                if (txtSzName.Text.Trim() == string.Empty)
                {
                    txtSzName.Focus();
                    throw new Exception("所属名を入力してください");
                }
                
                // 週所定労働日数
                if (Utility.StrtoInt(txtWshoTei.Text) < 1 || Utility.StrtoInt(txtWshoTei.Text) > 7)
                {
                    txtWshoTei.Focus();
                    throw new Exception("週所定労働日数は1～7の範囲で入力してください");
                }

                // 調整年月日
                if (!dtChousei.Checked)
                {
                    dtChousei.Checked = true;
                    dtChousei.Focus();
                    throw new Exception("調整年月日を入力してください");
                }

                // 有休付与月
                int cm = dtChousei.Value.Month + 6;
                if (cm > 12) cm = cm - 12;
                if (cm != Utility.StrtoInt(txtFuyoTsuki.Text))
                {
                    txtFuyoTsuki.Focus();
                    throw new Exception("有休付与月が正しくありません");
                }

                //// 週開始曜日
                //if (cmbYoubi.SelectedIndex == -1)
                //{
                //    cmbYoubi.Focus();
                //    throw new Exception("週開始曜日を選択してください");
                //}

                // 2018/10/19
                if (cmbShortWork.SelectedIndex == -1)
                {
                    cmbShortWork.Focus();
                    throw new Exception("短時間勤務を選択してください");
                }

                // 2018/10/19
                if (cmbShain.SelectedIndex == -1)
                {
                    cmbShain.Focus();
                    throw new Exception("社員区分を選択してください");
                }

                // 2018/10/19
                if (cmbFarm.SelectedIndex == -1)
                {
                    cmbFarm.Focus();
                    throw new Exception("農業従事を選択してください");
                }

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, msName + "保守", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        /// <summary>
        /// グリッドビュー行選択時処理
        /// </summary>
        private void GridEnter()
        {
            string msgStr;
            fMode.rowIndex = dg.SelectedRows[0].Index;

            // 選択確認
            msgStr = "";
            msgStr += dg[1, fMode.rowIndex].Value.ToString() + "：" + dg[3, fMode.rowIndex].Value.ToString() + Environment.NewLine + Environment.NewLine;
            msgStr += "上記の" + msName + "が選択されました。よろしいですか？";

            if (MessageBox.Show(msgStr, "選択", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) 
                return;

            // 対象となるデータテーブルROWを取得します
            JAFA_OCRDataSet.社員マスターRow sQuery = dts.社員マスター.FindBy職員コード(Utility.StrtoInt(dg[0, fMode.rowIndex].Value.ToString()));

            if (sQuery != null)
            {
                // 編集画面に表示
                ShowData(sQuery);

                // モードステータスを「編集モード」にします
                fMode.Mode = global.FORM_EDITMODE;
            }
            else
            {
                MessageBox.Show(dg[0, fMode.rowIndex].Value.ToString() + "がキー不在です：データの読み込みに失敗しました", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// -------------------------------------------------------
        /// <summary>
        ///     マスターの内容を画面に表示する </summary>
        /// <param name="sTemp">
        ///     マスターインスタンス</param>
        /// -------------------------------------------------------
        private void ShowData(JAFA_OCRDataSet.社員マスターRow s)
        {
            fMode.ID = s.職員コード.ToString();
            txtCode.Enabled = false;
            txtCode.Text = s.職員コード.ToString();
            txtName.Text = s.氏名;

            if (s.IsフリガナNull())
            {
                txtFuri.Text = string.Empty;
            }
            else
            {
                txtFuri.Text = s.フリガナ;
            }

            txtSzCode.Text = s.所属コード.ToString();
            txtSzName.Text = s.所属名;

            if (s.入所年月日 == global.dtNon)
            {
                dtNyusho.Text = string.Empty;
                dtNyusho.Checked = false;
            }
            else
            {
                dtNyusho.Checked = true;
                dtNyusho.Value = s.入所年月日;
            }

            if (s.調整年月日 == global.dtNon)
            {
                dtChousei.Text = string.Empty;
                dtChousei.Checked = false;
            }
            else
            {
                dtChousei.Checked = true;
                dtChousei.Value = s.調整年月日;
            }

            if (s.退職年月日 == global.dtNon)
            {
                dtTaishoku.Text = string.Empty;
                dtTaishoku.Checked = false;
            }
            else
            {
                dtTaishoku.Checked = true;
                dtTaishoku.Value = s.退職年月日;
            }

            txtWshoTei.Text = s.週所定労働日数.ToString();

            if (s.退職区分 == global.flgOff)
            {
                rbZaiseki.Checked = true;
            }
            else
            {
                rbTaishoku.Checked = true;
            }

            //cmbYoubi.SelectedIndex = s.週開始曜日;

            txtFuyoTsuki.Text = s.有給付与月.ToString();

            if (s.Is備考Null())
            {
                txtMemo.Text = string.Empty;
            }
            else
            {
                txtMemo.Text = s.備考;
            }

            // 2018/10/19
            if (s.Is短時間勤務Null())
            {
                cmbShortWork.SelectedIndex = -1;
                cmbShortWork.Text = "";
            }
            else 
            {
                cmbShortWork.SelectedIndex = s.短時間勤務;
            }

            // 2018/10/22
            if (s.Is社員区分Null())
            {
                cmbShain.SelectedIndex = -1;
                cmbShain.Text = "";
            }
            else
            {
                cmbShain.SelectedIndex = s.社員区分;
            }

            // 2018/10/22
            if (s.Is農業従事Null())
            {
                cmbFarm.SelectedIndex = -1;
                cmbFarm.Text = "";
            }
            else
            {
                cmbFarm.SelectedIndex = s.農業従事;
            }

            btnDel.Enabled = true;
            btnClear.Enabled = true;
        }

        private void dg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GridEnter();
        }

        private void btnRtn_Click(object sender, EventArgs e)
        {
            // 入社時有給休暇日数をマスター登録する：2018/11/09
            setInitialYukyu();

            // フォームを閉じます
            this.Close();
        }

        ///--------------------------------------------------------------------------
        /// <summary>
        ///     入社時有給休暇日数を有給休暇付与マスターに登録する </summary>
        ///--------------------------------------------------------------------------
        private void setInitialYukyu()
        {
            this.Cursor = Cursors.WaitCursor;

            JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter yAdp = new JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter();

            // 正社員が対象
            adp.FillByShainkbn(dts.社員マスター, 1);

            foreach (var t in dts.社員マスター.Where(a => a.退職区分 == global.flgOff))
            {
                // 入社時有給休暇日数が有給休暇付与マスター登録済みか調べる
                if (yAdp.FillBySCodeYYMM(dts.有給休暇付与マスター, t.職員コード, t.調整年月日.Year, t.調整年月日.Month) == 0)
                {
                    // 未登録のとき入社時有給休暇日数を有給休暇付与マスターに登録する
                    yAdp.InsertQuery(t.職員コード, t.調整年月日.Year, t.調整年月日.Month,
                                     0, 0, global.YUKYUDAYS_NYUSHO, 0, global.YUKYUDAYS_NYUSHO, t.調整年月日, t.調整年月日, 
                                     0, 0, 0, DateTime.Now); 
                }
            }

            this.Cursor = Cursors.Default;
        }



        private void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //// データセットの内容をデータベースへ反映させます
            //adp.Update(dts.社員マスター);

            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DispInitial();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 確認
                if (MessageBox.Show("削除してよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                // 削除データ取得（エラー回避のためDataRowState.Deleted と DataRowState.Detachedは除外して抽出する）
                var d = dts.社員マスター.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.職員コード == Utility.StrtoInt(fMode.ID));

                // foreach用の配列を作成する
                var list = d.ToList();

                // 削除
                foreach (var it in list)
                {
                    JAFA_OCRDataSet.社員マスターRow dl = (JAFA_OCRDataSet.社員マスターRow)dts.社員マスター.FindBy職員コード(it.職員コード);
                    dl.Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの削除に失敗しました" + Environment.NewLine + ex.Message);
            }
            finally
            {
                // 削除をコミット
                adp.Update(dts.社員マスター);

                // データテーブルにデータを読み込む
                adp.Fill(dts.社員マスター);

                // 画面データ消去
                DispInitial();
            }
        }

        private void frmKintaiKbn_Shown(object sender, EventArgs e)
        {
            txtCode.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dtNyusho_ValueChanged(object sender, EventArgs e)
        {
            if (!dtChousei.Checked)
            {
                dtChousei.Checked = true;
                dtChousei.Value = dtNyusho.Value;
            }
        }

        private void dtChousei_ValueChanged(object sender, EventArgs e)
        {
            int m = dtChousei.Value.Month + 6;
            if (m > 12) m = m - 12;
            txtFuyoTsuki.Text = m.ToString();
        }

        private void dtTaishoku_ValueChanged(object sender, EventArgs e)
        {
            if (dtTaishoku.Checked)
            {
                rbTaishoku.Checked = true;
            }
            else
            {
                rbZaiseki.Checked = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MyLibrary.CsvOut.GridView(dg, "社員マスター");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtsNum.Text != string.Empty)
            {
                setCurrentRows();
            }
        }

        ///-------------------------------------------------------------
        /// <summary>
        ///     職員コードで検索 </summary>
        ///-------------------------------------------------------------
        private void setCurrentRows()
        {
            bool hit = false;

            for (int i = 0; i < dg.RowCount; i++)
            {
                if (dg[0, i].Value.ToString() == txtsNum.Text)
                {
                    dg.CurrentCell = dg[0, i];
                    hit = true;
                    break;
                }
            }

            // 概要データなし
            if (!hit)
            {
                MessageBox.Show("該当データはありませんでした","検索",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
