using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAFA_DATA.Common;
using System.Data.OleDb;

namespace JAFA_DATA.Config
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            Utility.WindowsMaxSize(this, this.Width, this.Height);
            Utility.WindowsMinSize(this, this.Width, this.Height);

            adp.Fill(dts.環境設定);
            dhAdp.Fill(dtsD.勤務票ヘッダ);
            chAdp.Fill(dtsC.確定勤務票ヘッダ);

            //label5.Text = Properties.Settings.Default.gengou; // 西暦管理とする 2018/10/19
            getConfigData();
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.環境設定TableAdapter adp = new JAFA_OCRDataSetTableAdapters.環境設定TableAdapter();

        JAFA_DATADataSet dtsD = new JAFA_DATADataSet();
        JAFA_DATADataSetTableAdapters.勤務票ヘッダTableAdapter dhAdp = new JAFA_DATADataSetTableAdapters.勤務票ヘッダTableAdapter();

        JAFA_OCRDataSet dtsC = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter chAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter();
        
        private void getConfigData()
        {
            // 設定データ取得
            var s = dts.環境設定.Single(a => a.ID == global.configKEY);

            txtYear.Text = s.年.ToString();
            txtMonth.Text = s.月.ToString();
            txtPath.Text = s.受け渡しデータ作成パス;
            txtArchive.Text = s.データ保存月数.ToString();
            _YY = s.年.ToString();
            _MM = s.月.ToString();

            // FA仕様：2018/10/19
            if (s.Is祝日ＣＳＶデータパスNull())
            {
                txtCsvPath.Text = string.Empty;
            }
            else
            {
                txtCsvPath.Text = s.祝日ＣＳＶデータパス;
            }
        }

        string _YY = string.Empty;
        string _MM = string.Empty;

        /// <summary>
        /// フォルダダイアログ選択
        /// </summary>
        /// <returns>フォルダー名</returns>
        private string userFolderSelect()
        {
            string fName = string.Empty;

            //出力フォルダの選択ダイアログの表示
            // FolderBrowserDialog の新しいインスタンスを生成する (デザイナから追加している場合は必要ない)
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            // ダイアログの説明を設定する
            folderBrowserDialog1.Description = "フォルダを選択してください";

            // ルートになる特殊フォルダを設定する (初期値 SpecialFolder.Desktop)
            folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.Desktop;

            // 初期選択するパスを設定する
            folderBrowserDialog1.SelectedPath = @"C:\";

            // [新しいフォルダ] ボタンを表示する (初期値 true)
            folderBrowserDialog1.ShowNewFolderButton = true;

            // ダイアログを表示し、戻り値が [OK] の場合は、選択したディレクトリを表示する
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                fName = folderBrowserDialog1.SelectedPath + @"\";
            }
            else
            {
                // 不要になった時点で破棄する
                folderBrowserDialog1.Dispose();
                return fName;
            }

            // 不要になった時点で破棄する
            folderBrowserDialog1.Dispose();

            return fName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //フォルダーを選択する
            string flName = userFolderSelect();

            if (flName != string.Empty)
            {
                txtPath.Text = flName;
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // データ更新
            DataUpdate();
        }

        private void DataUpdate()
        {
            if ((anyOCRData() || anyKakuteiData()) && (txtYear.Text != _YY || txtMonth.Text != _MM))
            {
                MessageBox.Show("処理中の勤怠データがありますので対象年月の変更はできません","未処理データあり",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                txtYear.Text = _YY;
                txtMonth.Text = _MM;
                return;
            }

            if (Utility.StrtoInt(txtMonth.Text) < 1 || Utility.StrtoInt(txtMonth.Text) > 12)
            {
                MessageBox.Show("対象月が正しくありません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("データを更新してよろしいですか","確認",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            // エラーチェック
            if (!errCheck()) return;

            //// データ更新
            //OleDbCommand sCom = new OleDbCommand();
            //sCom.Connection = Utility.dbConnect();
            //StringBuilder sb = new StringBuilder();
            //sb.Append("update 環境設定 set ");
            //sb.Append("年=?, 月=?, 受け渡しデータ作成パス=?, データ保存月数=?, 更新年月日=? ");
            //sb.Append("where ID = ");
            //sb.Append(global.configKEY.ToString());
            //sCom.CommandText = sb.ToString();
            //sCom.Parameters.AddWithValue("@y", txtYear.Text);
            //sCom.Parameters.AddWithValue("@m", txtMonth.Text);
            //sCom.Parameters.AddWithValue("@p", lblPath.Text);
            //sCom.Parameters.AddWithValue("@d", txtArchive.Text);
            //sCom.Parameters.AddWithValue("@u", DateTime.Now.ToShortDateString());
            //sCom.ExecuteNonQuery();

            //global.cnfYear = int.Parse(txtYear.Text);
            //global.cnfMonth = int.Parse(txtMonth.Text);
            //global.cnfPath = lblPath.Text;
            //global.cnfArchived = int.Parse(txtArchive.Text);

            //sCom.Connection.Close();
            


            // 設定データ取得
            JAFA_OCRDataSet.環境設定Row s = dts.環境設定.Single(a => a.ID == global.configKEY);
            s.年 = int.Parse(txtYear.Text);
            s.月 = int.Parse(txtMonth.Text);
            s.受け渡しデータ作成パス = txtPath.Text;
            s.データ保存月数 = int.Parse(txtArchive.Text);
            s.更新年月日 = DateTime.Now;

            s.祝日ＣＳＶデータパス = txtCsvPath.Text; // FA仕様：2018/10/19

            // 環境設定データ更新
            adp.Update(dts.環境設定);

            // 設定値取得
            global.cnfYear = int.Parse(txtYear.Text);
            global.cnfMonth = int.Parse(txtMonth.Text);
            global.cnfPath = txtPath.Text;
            global.cnfArchived = int.Parse(txtArchive.Text);

            // 終了
            this.Close();
        }

        private bool errCheck()
        {
            // 処理年
            if (!Utility.NumericCheck(txtYear.Text))
            {
                MessageBox.Show("処理年が正しくありません","エラー",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            // 処理月
            if (!Utility.NumericCheck(txtMonth.Text))
            {
                MessageBox.Show("処理月が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            // パス
            if (txtPath.Text.Trim() == string.Empty)
            {
                MessageBox.Show("受け渡しデータ作成パスが指定されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPath.Focus();
                return false;
            }

            // データ保存期間
            if (!Utility.NumericCheck(txtArchive.Text))
            {
                MessageBox.Show("履歴データ保存期間が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 後片付け
            this.Dispose();
        }

        /// <summary>
        ///     勤務票ヘッダデータがあるか</summary>
        /// <returns>
        ///     true:ある,　falese:ない</returns>
        private bool anyOCRData()
        {
            if (dtsD.勤務票ヘッダ.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        ///     確定勤務票ヘッダがあるか</summary>
        /// <returns>
        ///     true:ある,　falese:ない</returns>
        private bool anyKakuteiData()
        {
            if (dtsC.確定勤務票ヘッダ.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //フォルダーを選択する
            string flName = userFolderSelect();

            if (flName != string.Empty)
            {
                txtCsvPath.Text = flName;
            }
        }
    }
}
