using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using JAFA_DATA.Common;

namespace JAFA_DATA.Config
{
    public class getConfig
    {
        public getConfig()
        {
            try
            {
                //OleDbCommand sCom = new OleDbCommand();
                //sCom.Connection = Utility.dbConnect();
                //OleDbDataReader dR;
                //StringBuilder sb = new StringBuilder();
                //sb.Append("select * from 環境設定 where ID = ");
                //sb.Append(global.configKEY.ToString());
                //sCom.CommandText = sb.ToString();

                //dR = sCom.ExecuteReader();

                //while (dR.Read())
                //{
                //    global.cnfYear = int.Parse(dR["年"].ToString());
                //    global.cnfMonth = int.Parse(dR["月"].ToString());
                //    global.cnfPath = dR["受け渡しデータ作成パス"].ToString();
                //    global.cnfArchived = int.Parse(dR["データ保存月数"].ToString());
                //}

                //dR.Close();
                //sCom.Connection.Close();

                JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
                JAFA_OCRDataSetTableAdapters.環境設定TableAdapter adp = new JAFA_OCRDataSetTableAdapters.環境設定TableAdapter();
                adp.Fill(dts.環境設定);

                JAFA_OCRDataSet.環境設定Row r = dts.環境設定.Single(a => a.ID == global.configKEY);

                global.cnfYear = int.Parse(r.年.ToString());
                global.cnfMonth = int.Parse(r.月.ToString());
                global.cnfPath = r.受け渡しデータ作成パス;
                global.cnfArchived = int.Parse(r.データ保存月数.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "環境設定年月取得", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
            }
        }
    }
}
