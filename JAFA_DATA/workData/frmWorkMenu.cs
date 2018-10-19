using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JAFA_DATA.workData
{
    public partial class frmWorkMenu : Form
    {
        public frmWorkMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWorkMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void frmWorkMenu_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            workData.frmPastWorksViewer frm = new frmPastWorksViewer();
            frm.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            workData.frmYukyuViewer frm = new frmYukyuViewer();
            frm.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            workData.frmKyujitsuWorkViewer frm = new frmKyujitsuWorkViewer();
            frm.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            workData.frmWeek40hOverViewer frm = new frmWeek40hOverViewer();
            frm.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            workData.frmWorkdataCsv frm = new frmWorkdataCsv();
            frm.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            workData.frmKotsuhiCsv frm = new frmKotsuhiCsv();
            frm.ShowDialog();
            this.Show();
        }
    }
}
