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
    public partial class frmWorkdataCsv : Form
    {
        public frmWorkdataCsv()
        {
            InitializeComponent();

            // データ読み込み
            hAdp.Fill(dts.確定勤務票ヘッダ);
            iAdp.Fill(dts.確定勤務票明細);
            sAdp.Fill(dts.出勤区分);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter hAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter();
        JAFA_OCRDataSetTableAdapters.確定勤務票明細TableAdapter iAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票明細TableAdapter();
        JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter sAdp = new JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter();

        const int ZENHAN = 1;   // 前半
        const int KOUHAN = 2;   // 後半
        const int ALL = 3;      // 全て

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
            if (radioButton1.Checked)
            {
                putCsvData(ZENHAN);
            }
            else if (radioButton2.Checked)
            {
                putCsvData(KOUHAN);
            }
            else if (radioButton3.Checked)
            {
                putCsvData(ALL);
            }
        }

        private void putCsvData(int zk)
        {
            // 出力配列
            string[] arrayCsv = null;

            // 件数
            int cnt = 0;

            var s = dts.確定勤務票明細.OrderBy(a => a.確定勤務票ヘッダRow.社員番号).ThenBy(a => a.日付);

            if (zk == ZENHAN)
            {
                s = s.Where(a => a.日付 < 16).OrderBy(a => a.確定勤務票ヘッダRow.社員番号).ThenBy(a => a.日付);
            }
            else if (zk == KOUHAN)
            {
                s = s.Where(a => a.日付 > 15).OrderBy(a => a.確定勤務票ヘッダRow.社員番号).ThenBy(a => a.日付);
            }

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
                    sb.Append("日").Append(",");
                    sb.Append("曜日").Append(",");
                    sb.Append("出勤区分").Append(",");
                    sb.Append("出勤区分名").Append(",");
                    sb.Append("始業").Append(",");
                    sb.Append("終業").Append(",");
                    sb.Append("休憩").Append(",");
                    sb.Append("実労働時間").Append(",");
                    sb.Append("訂正チェック").Append(Environment.NewLine);
                }

                // 明細
                sb.Append(t.確定勤務票ヘッダRow.社員番号.ToString()).Append(",");
                sb.Append(t.確定勤務票ヘッダRow.社員名).Append(",");
                sb.Append(t.確定勤務票ヘッダRow.所属コード).Append(",");
                sb.Append(t.確定勤務票ヘッダRow.所属名).Append(",");
                sb.Append(t.確定勤務票ヘッダRow.年.ToString()).Append(",");
                sb.Append(t.確定勤務票ヘッダRow.月.ToString()).Append(",");
                sb.Append(t.日付.ToString()).Append(",");

                DateTime eDate = DateTime.Parse((t.確定勤務票ヘッダRow.年 + Properties.Settings.Default.rekiHosei).ToString() + "/" + t.確定勤務票ヘッダRow.月.ToString() + "/" + t.日付.ToString());
                //sb.Append(t.日付.ToString()).Append(","); // 曜日
                sb.Append(("日月火水木金土").Substring(int.Parse(eDate.DayOfWeek.ToString("d")), 1)).Append(",");

                sb.Append(t.出勤区分).Append(",");    // 出勤区分名称

                // // 出勤区分名称
                if (!dts.出勤区分.Any(a => a.ID == Utility.StrtoInt(t.出勤区分)))
                {
                    sb.Append(string.Empty).Append(",");
                }
                else
                {
                    var k = dts.出勤区分.Single(a => a.ID == Utility.StrtoInt(t.出勤区分));
                    sb.Append(k.名称).Append(",");
                }

                // 開始時刻
                if (t.開始時 != string.Empty || t.開始分 != string.Empty)
                {
                    sb.Append(t.開始時).Append(":").Append(t.開始分).Append(",");
                }
                else
                {
                    sb.Append(string.Empty).Append(",");
                }

                // 終了時刻
                if (t.終了時 != string.Empty || t.終了分 != string.Empty)
                {
                    sb.Append(t.終了時).Append(":").Append(t.終了分).Append(",");
                }
                else
                {
                    sb.Append(string.Empty).Append(",");
                }

                // 休憩時間
                if (t.休憩開始時 != string.Empty || t.休憩開始分 != string.Empty)
                {
                    string kh = t.休憩開始時;
                    if (t.休憩開始時 == string.Empty)
                    {
                        kh = global.FLGOFF;
                    }

                    sb.Append(kh).Append(":").Append(t.休憩開始分).Append(",");
                }
                else
                {
                    sb.Append(string.Empty).Append(",");
                }

                // 実働時間
                if (t.実働時 == global.flgOff && t.実働分 == global.flgOff)
                {
                    sb.Append(string.Empty).Append(",");
                }
                else
                {
                    sb.Append(t.実働時.ToString()).Append(":").Append(t.実働分.ToString()).Append(",");
                }

                // 訂正
                sb.Append(t.訂正.ToString());

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
                saveFileDialog1.Title = "確定データCSV出力";
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = "確定データ";
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
            // 初期設定は前半
            radioButton1.Checked = true;
        }
    }
}
