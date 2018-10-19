using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAFA_DATA.Common;

namespace JAFA_DATA.workData
{
    public partial class frmKotsuhiCsv : Form
    {
        public frmKotsuhiCsv()
        {
            InitializeComponent();

            // データ読み込み
            hAdp.Fill(dts.確定勤務票ヘッダ);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter hAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter();
        
        private void button3_Click(object sender, EventArgs e)
        {
            // フォームを閉じる
            this.Close();
        }

        private void frmWorkdataCsv_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 後片付け
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 確定データ出力
            putCsvData();
        }

        private void putCsvData()
        {
            // 出力配列
            string[] arrayCsv = null;

            // 件数
            int cnt = 0;

            var s = dts.確定勤務票ヘッダ.OrderBy(a => a.社員番号);

            string ad = string.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (var t in s)
            {
                cnt++;
                sb.Clear();

                // ヘッダ
                if (cnt == 1)
                {
                    sb.Append("職員CD").Append(",");
                    sb.Append("氏名").Append(",");
                    sb.Append("所属CD").Append(",");
                    sb.Append("所属名").Append(",");
                    sb.Append("年").Append(",");
                    sb.Append("月").Append(",");
                    sb.Append("交通費").Append(",");
                    sb.Append("日当").Append(",");
                    sb.Append("宿泊費").Append(Environment.NewLine);
                }

                // 明細
                sb.Append(t.社員番号.ToString()).Append(",");
                sb.Append(t.社員名).Append(",");
                sb.Append(t.所属コード).Append(",");
                sb.Append(t.所属名).Append(",");
                sb.Append(t.年.ToString()).Append(",");
                sb.Append(t.月.ToString()).Append(",");
                sb.Append(t.交通費.ToString()).Append(",");
                sb.Append(t.日当.ToString()).Append(",");
                sb.Append(t.宿泊費.ToString());
                
                // 配列にセット
                Array.Resize(ref arrayCsv, cnt);        // 配列のサイズ拡張
                arrayCsv[cnt - 1] = sb.ToString();      // 文字列のセット
            }

            if (arrayCsv == null)
            {
                MessageBox.Show("確定データがありませんでした","確定データCSV出力",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                DialogResult ret;

                // ダイアログボックスの初期設定
                saveFileDialog1.Title = "交通費データCSV出力";
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = "交通費データ";
                saveFileDialog1.Filter = "CSVファイル(*.CSV)|*.CSV";

                // ダイアログボックスを表示し「保存」ボタンが選択されたらファイル名を表示
                string fileName;
                ret = saveFileDialog1.ShowDialog();

                if (ret == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = saveFileDialog1.FileName;

                    // CSVファイル出力
                    System.IO.File.WriteAllLines(fileName, arrayCsv, System.Text.Encoding.GetEncoding(932));

                    // 終了
                    MessageBox.Show("処理が終了しました");
                }
            }
        }

        private void frmWorkdataCsv_Load(object sender, EventArgs e)
        {

        }
    }
}
