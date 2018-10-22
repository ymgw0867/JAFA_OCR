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
    public partial class frmPastYukyuData : Form
    {
        // マスター名
        string msName = "出勤区分";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 有給付与マスターテーブルアダプター生成
        JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter adp = new JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter();
        JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter mAdp = new JAFA_OCRDataSetTableAdapters.社員マスターTableAdapter();

        // データテーブル生成
        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();

        public frmPastYukyuData()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adp.Fill(dts.有給休暇付与マスター);
            mAdp.Fill(dts.社員マスター);
        }

        private void frm_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // 画面初期化
            DispInitial();
        }
        
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void DispInitial()
        {
            fMode.Mode = global.FORM_EDITMODE;

            txtsYear.Text = string.Empty;
            txtsMonth.Text = string.Empty;
            txtsNum.Text = string.Empty;

            lblName.Text = string.Empty;
            txtZzan.Text = string.Empty;
            txtZyu.Text = string.Empty;
            txtTfuyo.Text = string.Empty;
            txtTkuri.Text = string.Empty;
            txtTzan.Text = string.Empty;

            btnUpdate.Enabled = true;
            btnClear.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //エラーチェック
            if (!fDataCheck()) return;

            switch (fMode.Mode)
            {
                // 新規登録
                case global.FORM_ADDMODE:

                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(lblName.Text + " の有休付与マスターを更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセット更新
                    int sYear = Utility.StrtoInt(txtsYear.Text);
                    int sMonth = Utility.StrtoInt(txtsMonth.Text);
                    int sNum = Utility.StrtoInt(txtsNum.Text);

                    if (dts.有給休暇付与マスター.Any(a => a.年 == sYear && a.月 == sMonth && a.社員番号 == sNum))
                    {
                        JAFA_OCRDataSet.有給休暇付与マスターRow r = dts.有給休暇付与マスター
                                                                    .Single(a => a.年 == sYear &&
                                                                                a.月 == sMonth &&
                                                                                a.社員番号 == sNum);

                        r.前年初有給残日数 = Utility.StrtoDouble(txtZzan.Text);
                        r.前年有休消化日数 = Utility.StrtoDouble(txtZyu.Text);
                        r.当年繰越日数 = Utility.StrtoDouble(txtTkuri.Text);
                        r.当年付与日数 = Utility.StrtoDouble(txtTfuyo.Text);
                        r.当年初有給残日数 = Utility.StrtoDouble(txtTzan.Text);
                        r.更新年月日 = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show("データの更新に失敗しました", "更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;

                default:
                    break;
            }

            // 画面データ消去
            DispInitial();      
        }

        //登録データチェック
        private Boolean fDataCheck()
        {
            try
            {
                // 年初有休残日数
                double sz = Utility.StrtoInt(txtTfuyo.Text) + Utility.StrtoDouble(txtTkuri.Text);

                if (sz != double.Parse(txtTzan.Text))
                {
                    this.txtTzan.Focus();
                    throw new Exception("当年初有休残日数が正しくありません");
                }

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, msName + "保守", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        
        private void btnRtn_Click(object sender, EventArgs e)
        {
            // フォームを閉じます
            this.Close();
        }

        private void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // データセットの内容をデータベースへ反映させます
            adp.Update(dts.有給休暇付与マスター);

            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DispInitial();
        }

        private void frmKintaiKbn_Shown(object sender, EventArgs e)
        {
            txtsYear.Focus();
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

        private void txtZyu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataSerach(Utility.StrtoInt(txtsYear.Text), Utility.StrtoInt(txtsMonth.Text), Utility.StrtoInt(txtsNum.Text));
        }

        private void dataSerach(int sYear, int sMonth, int sNUm)
        {
            if (dts.有給休暇付与マスター.Any(a => a.年 == sYear && a.月 == sMonth && a.社員番号 == sNUm))
            {
                JAFA_OCRDataSet.有給休暇付与マスターRow r = dts.有給休暇付与マスター
                                                                .Single(a => a.年 == sYear &&
                                                                            a.月 == sMonth &&
                                                                            a.社員番号 == sNUm);

                txtZzan.Text = r.前年初有給残日数.ToString();
                txtZyu.Text = r.前年有休消化日数.ToString();
                txtTfuyo.Text = r.当年付与日数.ToString();
                txtTkuri.Text = r.当年繰越日数.ToString();
                txtTzan.Text = r.当年初有給残日数.ToString();

                // 社員マスター
                foreach (var t in dts.社員マスター.Where(a => a.職員コード == sNUm))
                {
                    lblName.Text = t.氏名;                    
                }

                btnClear.Enabled = true;
            }
            else
            {
                MessageBox.Show("該当データするデータがありません","有給付与データなし",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnClear.Enabled = false;
            }
        }

        private void txtTfuyo_TextChanged(object sender, EventArgs e)
        {
            txtTzan.Text = (Utility.StrtoInt(txtTfuyo.Text) + Utility.StrtoDouble(txtTkuri.Text)).ToString();
        }

        private void txtTkuri_TextChanged(object sender, EventArgs e)
        {
            txtTzan.Text = (Utility.StrtoInt(txtTfuyo.Text) + Utility.StrtoDouble(txtTkuri.Text)).ToString();
        }
    }
}
