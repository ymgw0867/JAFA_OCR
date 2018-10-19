using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JAFA_DATA.OCR
{
    public partial class frmNGReNew : Form
    {
        public frmNGReNew()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("前半・後半を指定してください","未指定",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            if (radioButton1.Checked)
            {
                zkKbn = 1;  // 前半
            }
            else if (radioButton2.Checked)
            {
                zkKbn = 2;  // 後半
            }

            if (MessageBox.Show("NG画像からデータ未入力状態の出勤簿データを作成します。よろしいですか", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            // 終了
            this.Close();
        }

        public int zkKbn { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            zkKbn = 0;
            this.Close();
        }
    }
}
