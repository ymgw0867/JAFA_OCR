using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JAFA_DATA.Common
{
    class clsGetMst
    {
        string[] kojinCsv;        // 社員マスター
        string[] kinmuCsv;        // 勤務区分マスター
        string[] jiyuCsv;         // 事由マスター

        public clsGetMst()
        {
            adp.Fill(dts.メイトマスター);
            adpS.Fill(dts.出勤区分);
        }

        JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter adp = new JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter();
        JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter adpS = new JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter();
        
        /// -------------------------------------------------------------------------
        /// <summary>
        ///     メイトマスターから社員名を社員コードで検索する </summary>
        /// <param name="code">
        ///     社員コード </param>
        /// <returns>
        ///     社員名,フリガナ,所属コード,所属名の配列 </returns>
        /// -------------------------------------------------------------------------
        public string[] getKojinMst(int code)
        {
            string[] sName = new string[6];
            sName[0] = global.NOT_FOUND;
            sName[1] = global.NOT_FOUND;
            sName[2] = global.NOT_FOUND;
            sName[3] = global.NOT_FOUND;
            sName[4] = global.NOT_FOUND;
            sName[5] = global.NOT_FOUND;

            var s = dts.メイトマスター.Where(a => a.職員コード == code);

            foreach (var t in s)
            {
                sName[0] = t.氏名;
                sName[1] = t.フリガナ;
                sName[2] = t.所属コード.ToString();
                sName[3] = t.所属名;
                sName[4] = "日月火水木金土".Substring(t.週開始曜日, 1);
                sName[5] = t.週所定労働日数.ToString();

                break;
            }

            return sName;
        }

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     社員コードでメイトマスターRowオブジェクトを取得する </summary>
        /// <param name="code">
        ///     社員コード </param>
        /// <returns>
        ///     JAHR_OCRDataSet.メイトマスターRow </returns>
        /// -------------------------------------------------------------------------
        public JAFA_OCRDataSet.メイトマスターRow getKojinMstRow(int code)
        {
            JAFA_OCRDataSet.メイトマスターRow s = (JAFA_OCRDataSet.メイトマスターRow)dts.メイトマスター.FindBy職員コード(code);
            if (s == null)
            {
                return null;
            }
            else
            {
                return s;
            }
        }

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     出勤区分から出勤区分名、実働区分を検索する </summary>
        /// <param name="code">
        ///     出勤区分 </param>
        /// <returns>
        ///     出勤区分名、実働区分の配列 </returns>
        /// -------------------------------------------------------------------------
        public string[] getKinmuMst(string code)
        {            
            string[] sName = new string[2];
            sName[0] = global.NOT_FOUND;
            sName[1] = global.NOT_FOUND;

            var s = dts.出勤区分.Where(a => a.ID == int.Parse(code));

            foreach (var t in s)
            {
                sName[0] = t.名称;
                sName[1] = t.実働区分.ToString();
                break;
            }

            return sName;
        }

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     事由.txtファイルから事由コードを検索する </summary>
        /// <param name="code">
        ///     事由コード </param>
        /// <returns>
        ///     事由名 </returns>
        /// -------------------------------------------------------------------------
        public string getJiyuMst(string code)
        {
            string sName = global.NOT_FOUND;

            foreach (var item in jiyuCsv)
            {
                // カンマ区切りで1行のデータ配列を取得
                string[] k = item.Split(',');

                // 配列の要素数が2つ以上あるか
                if (k.Length > 1)
                {
                    // コードを照合
                    if (k[0] == code)
                    {
                        sName = k[1];
                        break;
                    }
                }
            }

            //var s = jiyuCsv.Select(a => a.Split(','))
            //        .Select(items => new
            //        {
            //            code = items[0].Replace(@"""", string.Empty),
            //            name = items[1].Replace(@"""", string.Empty),
            //        })
            //        .Where(a => a.code == code);

            //if (s.Count() > 0)
            //{
            //    foreach (var t in s)
            //    {
            //        sName = t.name;
            //        break;
            //    }
            //}

            return sName;
        }
    }
}
