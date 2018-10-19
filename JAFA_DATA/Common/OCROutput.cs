using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using LinqToExcel;

namespace JAFA_DATA.Common
{
    ///------------------------------------------------------------------
    /// <summary>
    ///     給与計算受け渡しデータクラス </summary>
    ///     
    ///------------------------------------------------------------------
    class OCROutput
    {
        // 親フォーム
        Form _preForm;

        private const string CSVFILENAME = "JAメイトOCRデータ";
        private const string CSVNENKYUNAME = "JAメイト年休取得データ";

        JAFA_OCRDataSet _dts = new JAFA_OCRDataSet();
        JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter hAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票ヘッダTableAdapter();
        JAFA_OCRDataSetTableAdapters.確定勤務票明細TableAdapter mAdp = new JAFA_OCRDataSetTableAdapters.確定勤務票明細TableAdapter();
        JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter jaAdp = new JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter();
        JAFA_OCRDataSetTableAdapters.週実績明細TableAdapter wAdp = new JAFA_OCRDataSetTableAdapters.週実績明細TableAdapter();
        JAFA_OCRDataSetTableAdapters.週実績TableAdapter zAdp = new JAFA_OCRDataSetTableAdapters.週実績TableAdapter();
        JAFA_OCRDataSetTableAdapters.残業先払いTableAdapter sAdp = new JAFA_OCRDataSetTableAdapters.残業先払いTableAdapter();
        JAFA_OCRDataSetTableAdapters.勤怠年休データTableAdapter nAdp = new JAFA_OCRDataSetTableAdapters.勤怠年休データTableAdapter();
        JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter mateAdp = new JAFA_OCRDataSetTableAdapters.メイトマスターTableAdapter();
        JAFA_OCRDataSetTableAdapters.有休付与日数表TableAdapter yuMapAdp = new JAFA_OCRDataSetTableAdapters.有休付与日数表TableAdapter();
        JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter ymsAdp = new JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter();

        LinqToExcel.Query.ExcelQueryable<exlMntData> workSheet = null;
        LinqToExcel.Query.ExcelQueryable<exlYukyuMst> mstSheet = null;

        #region 出勤区分定数
        const string SHUKIN_SHUKIN = "1";       // 出勤
        const string SHUKIN_YUKYU = "2";        // 有休
        const string SHUKIN_HANKYU = "3";       // 半休
        const string SHUKIN_FURIKYU = "4";      // 振替休日
        const string SHUKIN_KYUSHU = "5";       // 休日出勤
        const string KYUKA_SEIRI = "6";         // 生理休暇
        const string KYUKA_KANGO = "7";         // 看護休暇
        const string KYUKA_KAIGO = "8";         // 介護休暇
        const string KYUKA_KEKKON = "9";        // 結婚休暇
        const string KYUKA_KIBIKI = "10";       // 忌引休暇
        const string KYUKA_SANZENSANGO = "11";  // 産前産後休暇
        const string KYUKA_RISAI = "12";        // 罹災休暇
        const string KYUKA_KAKURI = "13";       // 隔離休暇
        const string KYUKA_SONOTA = "14";       // その他休暇
        const string KYUSHOKU_IKUJI = "15";     // 育児休職
        const string KYUSHOKU_KAIGO = "16";     // 介護休職
        const string KEKKIN_JIKO = "17";        // 自己都合欠勤
        const string KEKKIN_BYOUKI = "18";      // 病気欠勤
        const string KEKKIN_MUDAN = "19";       // 無断欠勤
        #endregion

        // 有休計算出勤率
        const decimal D80 = 80.0M;

        ///--------------------------------------------------------------------------
        /// <summary>
        ///     給与計算用計算用受入データ作成クラスコンストラクタ</summary>
        /// <param name="preFrm">
        ///     親フォーム</param>
        /// <param name="hTbl">
        ///     勤務票ヘッダDataTable</param>
        /// <param name="mTbl">
        ///     勤務票明細DataTable</param>
        ///--------------------------------------------------------------------------
        public OCROutput(Form preFrm, JAFA_OCRDataSet dts)
        {
            _preForm = preFrm;
            _dts = dts;

            //hAdp.Fill(_dts.確定勤務票ヘッダ);
            //mAdp.Fill(_dts.確定勤務票明細);

            jaAdp.Fill(_dts.勤怠データ);
            nAdp.Fill(_dts.勤怠年休データ);
            wAdp.Fill(_dts.週実績明細);
            zAdp.Fill(_dts.週実績);
            sAdp.Fill(_dts.残業先払い);
            mateAdp.Fill(_dts.メイトマスター);
            yuMapAdp.Fill(_dts.有休付与日数表);
            ymsAdp.Fill(_dts.有給休暇付与マスター);

            // linqToExcel : excel過去１年間有給取得シート
            if (System.IO.File.Exists(Properties.Settings.Default.exlMounthPath))
            {
                // ターゲットのエクセルファイルが存在するとき
                var excel = new ExcelQueryFactory(Properties.Settings.Default.exlMounthPath);
                excel.ReadOnly = true;
                excel.AddMapping<exlMntData>(m => m.sCode, "職員コード");
                excel.AddMapping<exlMntData>(m => m.sName, "氏名");
                excel.AddMapping<exlMntData>(m => m.sYYMM, "年月");
                excel.AddMapping<exlMntData>(m => m.sYouDay, "要出勤日数");
                excel.AddMapping<exlMntData>(m => m.sKekkin, "欠勤");
                excel.AddMapping<exlMntData>(m => m.sDay, "有給休暇");
                excel.AddMapping<exlMntData>(m => m.sHan, "半休");
                excel.AddMapping<exlMntData>(m => m.sTotal, "合計");
                workSheet = excel.Worksheet<exlMntData>("sheet1");
            }
            
            // linqToExcel : excel前年有休付与日数シート
            if (System.IO.File.Exists(Properties.Settings.Default.exlYukyuMstPath))
            {
                // ターゲットのエクセルファイルが存在するとき
                var excelMst = new ExcelQueryFactory(Properties.Settings.Default.exlYukyuMstPath);
                excelMst.ReadOnly = true;
                excelMst.AddMapping<exlYukyuMst>(m => m.sCode, "職員コード");
                excelMst.AddMapping<exlYukyuMst>(m => m.sName, "氏名");
                excelMst.AddMapping<exlYukyuMst>(m => m.sYY, "年");
                excelMst.AddMapping<exlYukyuMst>(m => m.sMM, "月");
                excelMst.AddMapping<exlYukyuMst>(m => m.sFuyo, "当年付与日数");
                excelMst.AddMapping<exlYukyuMst>(m => m.sKurikoshi, "当年繰越日数");
                excelMst.AddMapping<exlYukyuMst>(m => m.sNensho, "当年初有給残");
                mstSheet = excelMst.Worksheet<exlYukyuMst>("sheet1");
            }
        }

        ///--------------------------------------------------------------------------------------
        /// <summary>
        ///     JAメイトOCRデータ作成(CSV)</summary>
        ///--------------------------------------------------------------------------------------     
        //public void SaveData()
        //{
        //    #region 出力件数変数
        //    int sCnt = 0;   // 社員出力件数
        //    #endregion

        //    StringBuilder sb = new StringBuilder();
        //    //Boolean pblFirstGyouFlg = true;
        //    string wID = string.Empty;
        //    string hDate = string.Empty;
        //    string hKinmutaikei = string.Empty;

        //    // 出力先フォルダがあるか？なければ作成する
        //    string cPath = global.cnfPath;
        //    if (!System.IO.Directory.Exists(cPath)) System.IO.Directory.CreateDirectory(cPath);

        //    try
        //    {
        //        //オーナーフォームを無効にする
        //        _preForm.Enabled = false;

        //        //プログレスバーを表示する
        //        frmPrg frmP = new frmPrg();
        //        frmP.Owner = _preForm;
        //        frmP.Show();

        //        int rCnt = 1;

        //        // 伝票最初行フラグ
        //        //pblFirstGyouFlg = true;

        //        // 勤務票データ取得
        //        var s = _mTbl.OrderBy(a => a.ID);

        //        foreach (var r in s)
        //        {
        //            // プログレスバー表示
        //            frmP.Text = "クロノス用受入データ作成中です・・・" + rCnt.ToString() + "/" + s.Count().ToString();
        //            frmP.progressValue = rCnt * 100 / s.Count();
        //            frmP.ProgressStep();

        //            // 無記入行は読み飛ばし
        //            if (r.勤務記号 == string.Empty && r.開始時 == string.Empty && r.開始分 == string.Empty &&
        //                r.終了時 == string.Empty && r.終了分 == string.Empty && r.休憩開始時 == string.Empty &&
        //                r.休憩開始分 == string.Empty && r.休憩終了時 == string.Empty && r.休憩終了分 == string.Empty &&
        //                r.実働時 == string.Empty && r.実働分 == string.Empty &&
        //                r.事由1 == string.Empty && r.事由2 == string.Empty)
        //            {
        //                rCnt++;
        //                continue;
        //            }

        //            sb.Clear();

        //            // 社員番号
        //            string sNum = r.勤務票ヘッダRow.社員番号.ToString();

        //            // 日付
        //            hDate = r.勤務票ヘッダRow.年.ToString().Substring(2,2) + r.勤務票ヘッダRow.月.ToString().PadLeft(2, '0') + r.日付.ToString().PadLeft(2, '0');
        //            sb.Append(hDate).Append(",");

        //            // ----------------------------------------------------------
        //            //      出勤データ
        //            // ----------------------------------------------------------
        //            string st = r.開始時 + r.開始分;

        //            if (st.Trim() != string.Empty)
        //            {
        //                st = r.開始時 + r.開始分.PadLeft(2, '0');

        //                // 配列にセット
        //                sCnt++;
        //                setArrayCSV(hDate, st, sNum, "0", "1", sCnt);
        //            }

        //            // ----------------------------------------------------------
        //            //      退出データ
        //            // ----------------------------------------------------------
        //            string et = r.終了時 + r.終了分;

        //            if (et.Trim() != string.Empty)
        //            {
        //                et = r.終了時 + r.終了分.PadLeft(2, '0');

        //                // 配列にセット
        //                sCnt++;
        //                setArrayCSV(hDate, et, sNum, "1", "2", sCnt);
        //            }

        //            // ----------------------------------------------------------
        //            //      外出データ
        //            // ----------------------------------------------------------
        //            string gt = r.休憩開始時 + r.休憩開始分;

        //            if (gt.Trim() != string.Empty)
        //            {
        //                gt = r.休憩開始時 + r.休憩開始分.PadLeft(2, '0');

        //                // 配列にセット
        //                sCnt++;
        //                setArrayCSV(hDate, gt, sNum, "1", "3", sCnt);
        //            }

        //            // ----------------------------------------------------------
        //            //      再入データ
        //            // ----------------------------------------------------------
        //            string rt = r.休憩終了時 + r.休憩終了分;

        //            if (rt.Trim() != string.Empty)
        //            {
        //                rt = r.休憩終了時 + r.休憩終了分.PadLeft(2, '0');

        //                // 配列にセット
        //                sCnt++;
        //                setArrayCSV(hDate, rt, sNum, "0", "4", sCnt);
        //            }

        //            // ----------------------------------------------------------
        //            //      事由データ_1
        //            // ----------------------------------------------------------
        //            if (r.事由1.Trim() != string.Empty)
        //            {
        //                // 配列にセット
        //                sCnt++;
        //                setArrayCSV(hDate, "", sNum, "2", r.事由1, sCnt);
        //            }

        //            // ----------------------------------------------------------
        //            //      事由データ_2
        //            // ----------------------------------------------------------
        //            if (r.事由2.Trim() != string.Empty)
        //            {
        //                // 配列にセット
        //                sCnt++;
        //                setArrayCSV(hDate, "", sNum, "2", r.事由2, sCnt);
        //            }

        //            // ----------------------------------------------------------
        //            //      勤務区分
        //            // ----------------------------------------------------------
        //            if (r.勤務記号.Trim() != string.Empty)
        //            {
        //                // 配列にセット
        //                sCnt++;
        //                setArrayCSV(hDate, "", sNum, "4", r.勤務記号, sCnt);
        //            }
                    
        //            // データ件数加算
        //            rCnt++;

        //            //pblFirstGyouFlg = false;
        //        }

        //        // 勤怠CSVファイル出力
        //        if (arrayCsv != null) txtFileWrite(cPath, arrayCsv);

        //        // いったんオーナーをアクティブにする
        //        _preForm.Activate();

        //        // 進行状況ダイアログを閉じる
        //        frmP.Close();

        //        // オーナーのフォームを有効に戻す
        //        _preForm.Enabled = true;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("クロノス受入データ作成中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
        //    }
        //    finally
        //    {
        //        //if (OutData.sCom.Connection.State == ConnectionState.Open) OutData.sCom.Connection.Close();
        //    }
        //}

        /// -----------------------------------------------------------------------
        /// <summary>
        ///     週実績明細データ登録　</summary>
        /// -----------------------------------------------------------------------
        public void saveWeekData()
        {
            // 確定勤務票ヘッダデータ取得
            var h = _dts.確定勤務票ヘッダ.OrderBy(a => a.ヘッダID);

            foreach (var t in h)
            {
                // 週実績明細データ追加
                setNewWeekDataItem(t);
            }

            // データベース更新
            wAdp.Update(_dts.週実績明細);

            // 再読み込み
            wAdp.Fill(_dts.週実績明細);
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        ///     残業時間を取得します </summary>
        /// <param name="sYear">
        ///     対象年（西暦）</param>
        /// <param name="sMonth">
        ///     対象月</param>
        /// <param name="sNum">
        ///     社員番号</param>
        /// <returns>
        ///     残業時間</returns>
        /// ----------------------------------------------------------------------------
        private int getZangyoTime(int sYear, int sMonth, int sNum)
        {
            // その社員の当月1日の週番号を取得する
            int wNum = 0;
            DateTime dt = DateTime.Parse(sYear.ToString() + "/" + sMonth.ToString() + "/1");

            //var s = _dts.週実績明細.Where(a => a.年月日 == dt);
            //foreach (var it in s)
            //{
            //    wNum = it.週番号;
            //}

            // 2015/12/16 「a.職員コード == sNum」追加
            if (_dts.週実績明細.Any(a => a.年月日 == dt && a.職員コード == sNum))
            {
                var s = _dts.週実績明細.Single(a => a.年月日 == dt && a.職員コード == sNum);
                wNum = s.週番号;
            }
            else
            {
                // 当月1日の実績がない場合は当月最小の週番号を取得する 2015/12/16
                var sg = _dts.週実績明細.Where(a => a.年月日.Year == sYear && 
                                                   a.年月日.Month == sMonth && 
                                                   a.職員コード == sNum)
                                        .OrderBy(a => a.年月日);
                foreach (var it in sg)
                {
                    wNum = it.週番号;
                    break;
                }
            }
            
            // 週実績集計値テーブルを抽出する
            OleDbConnection cn = Utility.dbConnect();
            OleDbCommand sCom = new OleDbCommand();
            sCom.Connection = cn;

            // 該当週の開始年月日と終了年月日を抽出：2016/08/15
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT 週実績明細.職員コード, first(year(週実績明細.年月日)) as 年, 週実績明細.週番号, Count(週実績明細.年月日) AS 日数,");
            sb.Append("Sum(週実績明細.勤務時間) AS 実働, IIf(Sum(勤務時間)>2400,Sum(勤務時間)-2400,0) AS 週40時間超,");
            sb.Append("Sum(週実績明細.残業時間) AS 週残業, min(週実績明細.年月日) as 開始年月日, max(週実績明細.年月日) as 終了年月日 ");
            sb.Append("FROM 週実績明細 ");
            sb.Append("where 週番号 >= ? and 職員コード = ? and year(年月日) = ? ");
            sb.Append("GROUP BY 週実績明細.職員コード, 週実績明細.週番号 ");
            sb.Append("ORDER BY 週実績明細.職員コード, 週実績明細.週番号");

            sCom.CommandText = sb.ToString();
            sCom.Parameters.AddWithValue("@wNum", wNum);    // 週番号
            sCom.Parameters.AddWithValue("@mm", sNum);      // 社員番号
            sCom.Parameters.AddWithValue("@Year", sYear);   // 処理年

            OleDbDataReader dR = null;
            int wCnt = 0;
            int zangyo = 0;
            int zangyoTl = 0;
            int sakibarai = 0;
            int zengetsu = 0;

            try
            {
                dR = sCom.ExecuteReader();

                while (dR.Read())
                {
                    wCnt++;

                    // 先頭行のとき
                    if (wCnt == 1)
                    {
                        // 年初の週のとき
                        if (int.Parse(dR["週番号"].ToString()) == 1)
                        {
                            // 前年の最終週に計上した時間を取得します
                            zengetsu = getLastYearEndWeek(dR);
                        }
                        else
                        {
                            // 週番号が１ではないとき　※年初の週ではないとき
                            // 先月の最終週に計上した先払い残業時間を取得します
                            zengetsu = getZengetsu(dR);
                        }
                    }

                    // 2017/03/04 元のソース
                    ////// 年初の週の残業時間を取得します ※年初週が前年と同じ週のとき、前年分と合算する
                    ////if (int.Parse(dR["週番号"].ToString()) == 1 && int.Parse(dR["日数"].ToString()) < 7)
                    ////{
                    ////    // 年初の週のとき：年末年始の残業時間を算出します
                    ////    zangyoTl += nenmatsuNenshiZan(dR, sYear);
                    ////    sakibarai = 0;
                    ////}
                    ////// 週の残業時間を取得します
                    ////else if (getWeekZangyo(wCnt, dR, out zangyo, out sakibarai))
                    ////{
                    ////    if (int.Parse(dR["週番号"].ToString()) != 1)
                    ////    {
                    ////        // 月間残業時間に加算
                    ////        zangyoTl += zangyo;
                    ////    }
                    ////}

                    // ここから 2017/03/04
                    // 年初の週の残業時間を取得します
                    if (int.Parse(dR["週番号"].ToString()) == 1)
                    {
                        // 年初週が前年最終週と同じとき、前年分と合算する
                        if (int.Parse(dR["日数"].ToString()) < 7)
                        {
                            // 年末年始の残業時間を算出します
                            zangyoTl += nenmatsuNenshiZan(dR, sYear);
                            sakibarai = 0;
                        }
                        else
                        {
                            /* 年初週が週開始曜日で開始のとき（年末年始が週を跨っていないとき）
                             * 年初週の残業時間を取得する 2017/03/04 */
                            if (getWeekZangyo(wCnt, dR, out zangyo, out sakibarai))
                            {
                                // 月間残業時間に加算
                                zangyoTl += zangyo;
                            }
                        }
                    }
                    else if (getWeekZangyo(wCnt, dR, out zangyo, out sakibarai))
                    {
                        // 週の残業時間を取得します
                        if (int.Parse(dR["週番号"].ToString()) != 1)
                        {
                            // 月間残業時間に加算
                            zangyoTl += zangyo;
                        }
                    }
                    // ここまで 2017/03/04

                    // 週実績テーブルに登録
                    saveWeekDataRow(dR, sYear, sMonth, zangyo, sakibarai);

                    // 先払いテーブルに登録
                    if (sakibarai > 0)
                    {
                        saveSakibaraiRow(dR, sakibarai);
                    }
                }
                
                // 残業時間を返す（先月最終週の残業時間を差し引く）
                return zangyoTl - zengetsu;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (!dR.IsClosed) dR.Close();
                if (sCom.Connection.State == System.Data.ConnectionState.Open) sCom.Connection.Close();
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     年末年始に跨る週の残業時間を取得します </summary>
        /// <param name="dR">
        ///     週実績集計値</param>
        /// <param name="sYear">
        ///     処理年</param>
        /// <returns>
        ///     該当週の残業時間</returns>
        /// -----------------------------------------------------------------------------
        private int nenmatsuNenshiZan(OleDbDataReader dR, int sYear)
        {
            int zan = 0;
            int wWorks = 0;
            int wZangyo = 0;
            int w40Over = 0;

            // 前年最終週の勤務実績を取得します
            if (_dts.週実績.Any(a => a.職員コード == int.Parse(dR["職員コード"].ToString()) &&
                                     a.年 == sYear - 1))
            {
                int s = _dts.週実績.Where(a => a.職員コード == int.Parse(dR["職員コード"].ToString()) &&
                                         a.年 == sYear - 1)
                                    .Max(a => a.週番号);

                JAFA_OCRDataSet.週実績Row aa = _dts.週実績.FindBy職員コード年週番号(
                                                int.Parse(dR["職員コード"].ToString()),
                                                sYear - 1, s);
                if (aa != null)
                {
                    wWorks = aa.勤務時間;
                    wZangyo = aa.残業時間;
                }
                else
                {
                    wWorks = 0;
                    wZangyo = 0;
                }
            }

            // 当年第1週の勤務実績を取得します
            wWorks += int.Parse(dR["実働"].ToString());
            wZangyo += int.Parse(dR["週残業"].ToString());

            // 週40H超時間
            if (wWorks > 2400)
            {
                w40Over = wWorks - 2400;
            }
            else
            {
                w40Over = 0;
            }

            // 年末年始の週の残業時間を算定します。
            // 日別の残業時間の週計と週40H超時間のどちらか大きい方を残業時間とします。
            if (wZangyo >= w40Over)
            {
                zan = wZangyo;
            }
            else
            {
                zan = w40Over;
            }
            
            return zan;
        }


        /// ------------------------------------------------------------------------------------
        /// <summary>
        ///     この週の残業時間および最終週の残業時間を取得する </summary>
        /// <param name="wCnt">
        ///     件数カウント</param>
        /// <param name="dR">
        ///     OleDbDataReader dR</param>
        /// <param name="zangyo">
        ///     残業時間</param>
        /// <param name="sakibarai">
        ///     最終週残業時間</param>
        /// <returns>
        ///     true, false</returns>
        /// ------------------------------------------------------------------------------------
        private bool getWeekZangyo(int wCnt, OleDbDataReader dR, out int zangyo, out int sakibarai)
        {
            zangyo = 0;
            sakibarai = 0;

            // 該当週の最後の日が月末日なら最終週 2016/08/15
            bool lastWeek = false;
            int syy = DateTime.Parse(dR["終了年月日"].ToString()).Year;
            int smm = DateTime.Parse(dR["終了年月日"].ToString()).Month;
            int sdd = DateTime.Parse(dR["終了年月日"].ToString()).Day;

            if (sdd == DateTime.DaysInMonth(syy, smm))
            {
                lastWeek = true;
            }

            // 月を跨っている週
            if (int.Parse(dR["日数"].ToString()) < 7)
            {
                // 2016/08/15 最初の週で
                if (wCnt == 1)
                {
                    // 月末週でないとき 2016/08/15
                    if (!lastWeek)
                    {
                        // 月初の週 ： 年初の週、または入社日の週のケース
                        // 日別の残業時間の週計と週40H超時間のどちらか大きい方を残業時間とする
                        if (int.Parse(dR["週残業"].ToString()) >= int.Parse(dR["週40時間超"].ToString()))
                        {
                            zangyo = int.Parse(dR["週残業"].ToString());
                        }
                        else
                        {
                            zangyo = int.Parse(dR["週40時間超"].ToString());
                        }
                    }
                    else
                    {
                        // 月末週に入社した時 2016/08/15
                        // 日別の残業時間の週計を残業時間とする
                        sakibarai = int.Parse(dR["週残業"].ToString());
                        zangyo = int.Parse(dR["週残業"].ToString());
                    }
                }
                else
                {
                    // 月末週
                    // 日別の残業時間の週計を残業時間とする
                    sakibarai = int.Parse(dR["週残業"].ToString());
                    zangyo = int.Parse(dR["週残業"].ToString());
                }
            }
            else
            {
                // 日別の残業時間の週計と週40H超時間のどちらか大きい方を残業時間とする
                if (int.Parse(dR["週残業"].ToString()) >= int.Parse(dR["週40時間超"].ToString()))
                {
                    zangyo = int.Parse(dR["週残業"].ToString());
                }
                else
                {
                    zangyo = int.Parse(dR["週40時間超"].ToString());
                }
            }
            
            return true;
        }

        /// ---------------------------------------------------------------------------
        /// <summary>
        ///     先月の最終週に計上した残業時間を取得します </summary>
        /// <param name="dR">
        ///     OleDbDataReader dR</param>
        /// <returns>
        ///     先月の最終週残業時間</returns>     
        /// ---------------------------------------------------------------------------
        private int getZengetsu(OleDbDataReader dR)
        {
            int zengetsu = 0;

            // 先月の最終週に計上した時間を取得します
            JAFA_OCRDataSet.残業先払いRow aa = _dts.残業先払い.FindBy職員コード年週番号(
                                            int.Parse(dR["職員コード"].ToString()),
                                            int.Parse(dR["年"].ToString()),
                                            int.Parse(dR["週番号"].ToString()));
            if (aa != null)
            {
                zengetsu = aa.先払い残業時間;
            }
            else
            {
                zengetsu = 0;
            }

            return zengetsu;
        }

        /// ---------------------------------------------------------------------------
        /// <summary>
        ///     前年の最終週に計上した残業時間を取得します </summary>
        /// <param name="dR">
        ///     OleDbDataReader dR</param>
        /// <returns>
        ///     前年の最終週残業時間</returns>     
        /// ---------------------------------------------------------------------------
        private int getLastYearEndWeek(OleDbDataReader dR)
        {
            int zengetsu = 0;

            // 前年の最終週に計上した時間を取得します
            if (_dts.週実績.Any(a => a.職員コード == int.Parse(dR["職員コード"].ToString()) &&
                a.年 == int.Parse(dR["年"].ToString()) - 1))
                {
                    int s = _dts.週実績.Where(a => a.職員コード == int.Parse(dR["職員コード"].ToString()) &&
                        a.年 == int.Parse(dR["年"].ToString()) - 1)
                        .Max(a => a.週番号);
                
                    JAFA_OCRDataSet.残業先払いRow aa = _dts.残業先払い.FindBy職員コード年週番号(
                                                    int.Parse(dR["職員コード"].ToString()),
                                                    int.Parse(dR["年"].ToString()) - 1,
                                                    s);
                    if (aa != null)
                    {
                        zengetsu = aa.先払い残業時間;
                    }
                    else
                    {
                        zengetsu = 0;
                    }
                }

            return zengetsu;
        }


        /// ------------------------------------------------------------------------
        /// <summary>
        ///     最終週残業時間を先払いテーブルに登録 </summary>
        /// <param name="dR">
        ///     OleDbDataReader dR</param>
        /// <param name="sakibarai">
        ///     残業時間</param>
        /// ------------------------------------------------------------------------
        void saveSakibaraiRow(OleDbDataReader dR, int sakibarai)
        {
            JAFA_OCRDataSet.残業先払いRow b = _dts.残業先払い.New残業先払いRow();

            b.職員コード = int.Parse(dR["職員コード"].ToString());
            b.年 = int.Parse(dR["年"].ToString());
            b.週番号 = int.Parse(dR["週番号"].ToString());
            b.先払い残業時間 = sakibarai;
            b.更新年月日 = DateTime.Now;

            JAFA_OCRDataSet.残業先払いRow bb = _dts.残業先払い.FindBy職員コード年週番号(
                int.Parse(dR["職員コード"].ToString()),
                int.Parse(dR["年"].ToString()), 
                int.Parse(dR["週番号"].ToString()));

            if (bb != null)
            {
                bb.Delete();
            }

            // 残業先払いRow追加
            _dts.残業先払い.Add残業先払いRow(b);

            sAdp.Update(_dts.残業先払い);
            sAdp.Fill(_dts.残業先払い);
        }

        /// ------------------------------------------------------------------------------------
        /// <summary>
        ///     週実績テーブルに登録 </summary>
        /// <param name="dR">
        ///     OleDbDataReader dR</param>
        /// <param name="sYear">
        ///     年</param>
        /// <param name="sMonth">
        ///     月</param>
        /// <param name="zangyo">
        ///     残業時間</param>
        /// <param name="sakibarai">
        ///     最終週残業時間</param>
        /// ------------------------------------------------------------------------------------
        private void saveWeekDataRow(OleDbDataReader dR, int sYear, int sMonth, int zangyo, int sakibarai)
        {
            // 週実績テーブルに登録
            JAFA_OCRDataSet.週実績Row r = _dts.週実績.New週実績Row();
            r.職員コード = int.Parse(dR["職員コード"].ToString());
            r.年 = int.Parse(dR["年"].ToString());
            r.週番号 = int.Parse(dR["週番号"].ToString());
            r.週開始曜日 = 0;
            r.処理年 = sYear;
            r.処理月 = sMonth;
            r.集計年 = sYear;
            r.集計月 = sMonth;
            r.勤務時間 = int.Parse(dR["実働"].ToString());
            r.残業時間 = int.Parse(dR["週残業"].ToString());
            r._40H超時間 = int.Parse(dR["週40時間超"].ToString());
            r.給与残業時間 = zangyo;
            r.先払い残業時間 = sakibarai;
            r.更新年月日 = DateTime.Now;

            JAFA_OCRDataSet.週実績Row sr = _dts.週実績.FindBy職員コード年週番号(r.職員コード, r.年, r.週番号);
            if (sr != null)
            {
                sr.Delete();
            }

            // 週実績Row追加
            _dts.週実績.Add週実績Row(r);

            zAdp.Update(_dts.週実績);
            zAdp.Fill(_dts.週実績);
        }


        ///--------------------------------------------------------------------------------------
        /// <summary>
        ///     JAメイト年休取得データ作成(mdb)</summary>
        ///--------------------------------------------------------------------------------------     
        public void saveJAMateNenkyuData()
        {
            try
            {
                //オーナーフォームを無効にする
                _preForm.Enabled = false;

                //プログレスバーを表示する
                frmPrg frmP = new frmPrg();
                frmP.Owner = _preForm;
                frmP.Show();

                int rCnt = 1;

                // 確定勤務票ヘッダデータ取得
                var h = _dts.確定勤務票ヘッダ.OrderBy(a => a.ヘッダID);

                foreach (var t in h)
                {
                    // プログレスバー表示
                    frmP.Text = "JAメイト年休取得データ(MDB)作成中です・・・" + rCnt.ToString() + "/" + h.Count().ToString();
                    frmP.progressValue = rCnt * 100 / h.Count();
                    frmP.ProgressStep();

                    // JAメイト年休取得データ作成
                    putJAMateNenkyuData(t);

                    // 件数カウント
                    rCnt++;
                }

                // いったんオーナーをアクティブにする
                _preForm.Activate();

                // 進行状況ダイアログを閉じる
                frmP.Close();

                // オーナーのフォームを有効に戻す
                _preForm.Enabled = true;

                // データベース更新
                nAdp.Update(_dts.勤怠年休データ);

                // 再読み込み
                nAdp.Fill(_dts.勤怠年休データ);
            }
            catch (Exception e)
            {
                MessageBox.Show("JAメイト年休取得データ(MDB)作成中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        ///--------------------------------------------------------------------------------------
        /// <summary>
        ///     JAメイトOCRデータ作成(mdb)</summary>
        ///--------------------------------------------------------------------------------------     
        public void saveJAMateOCRData()
        {
            try
            {
                //オーナーフォームを無効にする
                _preForm.Enabled = false;

                //プログレスバーを表示する
                frmPrg frmP = new frmPrg();
                frmP.Owner = _preForm;
                frmP.Show();

                int rCnt = 1;

                // 確定勤務票ヘッダデータ取得
                var h = _dts.確定勤務票ヘッダ.OrderBy(a => a.ヘッダID);

                foreach (var t in h)
                {
                    // プログレスバー表示
                    frmP.Text = "JAメイトOCRデータ(MDB)作成中です・・・" + rCnt.ToString() + "/" + h.Count().ToString();
                    frmP.progressValue = rCnt * 100 / h.Count();
                    frmP.ProgressStep();

                    // JAメイトOCRデータ作成
                    JAFA_OCRDataSet.勤怠データRow jr = putJAMateOCRData(t);

                    // 対象月度・社員番号で登録済みデータを取得
                    JAFA_OCRDataSet.勤怠データRow dr = _dts.勤怠データ.FindBy対象月度対象職員コード(jr.対象月度, jr.対象職員コード);
                    if (dr != null)
                    {
                        // 登録済みデータを削除する
                        dr.Delete();
                    }

                    // JAメイトOCRデータ追加
                    _dts.勤怠データ.Add勤怠データRow(jr);

                    // 件数カウント
                    rCnt++;
                }

                // いったんオーナーをアクティブにする
                _preForm.Activate();

                // 進行状況ダイアログを閉じる
                frmP.Close();

                // オーナーのフォームを有効に戻す
                _preForm.Enabled = true;

                // データベース更新
                jaAdp.Update(_dts.勤怠データ);
                zAdp.Update(_dts.週実績);
                sAdp.Update(_dts.残業先払い);

                // 再読み込み
                jaAdp.Fill(_dts.勤怠データ);
                //zAdp.Update(_dts.週実績);
                sAdp.Fill(_dts.残業先払い);
            }
            catch (Exception e)
            {
                MessageBox.Show("JAメイトOCRデータ(MDB)作成中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        ///--------------------------------------------------------------------------------------
        /// <summary>
        ///     JAメイトOCRデータ作成(mdb)に有給付与データを書き込む</summary>
        ///--------------------------------------------------------------------------------------     
        public void saveJAMateOCRYukyu()
        {
            try
            {
                //オーナーフォームを無効にする
                _preForm.Enabled = false;

                //プログレスバーを表示する
                frmPrg frmP = new frmPrg();
                frmP.Owner = _preForm;
                frmP.Show();

                int rCnt = 1;

                // 有休付与月
                int fYear = global.cnfYear + Properties.Settings.Default.rekiHosei;
                int fMonth = global.cnfMonth + 1;
                if (fMonth > 12)
                {
                    fYear++;
                    fMonth -= 12;
                }

                // 有休休暇付与マスターを取得
                var h = _dts.有給休暇付与マスター.Where(a => a.年 == fYear && a.月 == fMonth);
                foreach (var t in h)
                {
                    // プログレスバー表示
                    frmP.Text = "JAメイトOCRデータ(MDB)に有休付与データを書き込み中です・・・" + rCnt.ToString() + "/" + h.Count().ToString();
                    frmP.progressValue = rCnt * 100 / h.Count();
                    frmP.ProgressStep();

                    // 対象職員コード
                    string sShoCode = global.ROK + t.社員番号.ToString().PadLeft(5, '0');

                    // 対象月度
                    string sYYYYMM = (global.cnfYear + Properties.Settings.Default.rekiHosei).ToString() + global.cnfMonth.ToString().PadLeft(2, '0');

                    // 有休付与マスターを参照し有休付与日数、有休繰越日数を取得する
                    if (_dts.勤怠データ.Any(a => a.対象職員コード == sShoCode && a.対象月度 == sYYYYMM))
                    {
                        JAFA_OCRDataSet.勤怠データRow r = _dts.勤怠データ.Single(a => a.対象職員コード == sShoCode && a.対象月度 == sYYYYMM);
                        r.有休付与日数 = (int)t.当年付与日数;
                        r.有休繰越日数 = t.当年繰越日数;
                        r.有休付与対象フラグ = global.flgOn;

                        // データベース更新
                        jaAdp.Update(_dts.勤怠データ);
                    }

                    // 件数カウント
                    rCnt++;
                }

                // いったんオーナーをアクティブにする
                _preForm.Activate();

                // 進行状況ダイアログを閉じる
                frmP.Close();

                // オーナーのフォームを有効に戻す
                _preForm.Enabled = true;
                
                // 再読み込み
                jaAdp.Fill(_dts.勤怠データ);
            }
            catch (Exception e)
            {
                MessageBox.Show("JAメイトOCRデータ(MDB)作成中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        /// ---------------------------------------------------------------------------
        /// <summary>
        ///     JAメイト年休取得データＭＤＢ出力 </summary>
        /// <param name="r">
        ///     JAHR_OCRDataSet.確定勤務票ヘッダRow </param>
        /// ---------------------------------------------------------------------------
        private void putJAMateNenkyuData(JAFA_OCRDataSet.確定勤務票ヘッダRow r)
        {
            string tGatsudo = (r.年 + Properties.Settings.Default.rekiHosei).ToString() + r.月.ToString().PadLeft(2, '0');
            string tShokuin = global.ROK + r.社員番号.ToString().PadLeft(5, '0');

            // 対象月度、社員番号で登録済みデータを削除する
            if (_dts.勤怠年休データ.Any(a => a.対象月度 == tGatsudo && a.対象職員コード == tShokuin))
            {
                foreach (var item in _dts.勤怠年休データ.Where(a => a.対象月度 == tGatsudo && a.対象職員コード == tShokuin))
                {
                    // 登録済みデータを削除する
                    item.Delete();
                }

                // データベース更新
                nAdp.Update(_dts.勤怠年休データ);

                // 再読み込み
                nAdp.Fill(_dts.勤怠年休データ);
            }

            // 対象期間の確定明細オブジェクトを取得します（入所日～退職日で絞り込み）
            List<JAFA_OCRDataSet.確定勤務票明細Row> mr = getKakuteiItem(r);

            // 有休半休日数
            double nissu = 0;
            string skbn = string.Empty;

            foreach (var t in mr)
            {
                // 出勤区分先頭ゼロ消し：2015/09/24
                skbn = Utility.StrtoInt(t.出勤区分).ToString();

                // 有休、半休以外は読み飛ばし
                if (skbn != SHUKIN_YUKYU && skbn != SHUKIN_HANKYU)
                {
                    continue;
                }

                nissu = 0;
                string YYYYMMDD = string.Empty;

                if (skbn == SHUKIN_YUKYU)
                {
                    // 有給休暇
                    nissu = 1;
                }
                else if (skbn == SHUKIN_HANKYU)
                {
                    // 半休
                    nissu = 0.5;
                }

                // 有休・半休取得のとき
                if (nissu != 0)
                {
                    YYYYMMDD = (r.年 + Properties.Settings.Default.rekiHosei).ToString() + r.月.ToString().PadLeft(2, '0') + t.日付.ToString().PadLeft(2, '0');
                    
                    // ＪＡメイト年休取得データRow新規インスタンス
                    JAFA_OCRDataSet.勤怠年休データRow jr = _dts.勤怠年休データ.New勤怠年休データRow();

                    jr.対象月度 = (r.年 + Properties.Settings.Default.rekiHosei).ToString() + r.月.ToString().PadLeft(2, '0');
                    jr.対象職員コード = global.ROK + r.社員番号.ToString().PadLeft(5, '0');
                    jr.対象職員名 = r.社員名;
                    jr.対象職員所属コード = global.ROK + r.所属コード.PadLeft(5, '0');
                    jr.対象職員所属名 = r.所属名;
                    jr.取得日 = YYYYMMDD;
                    jr.取得日数 = nissu;

                    // JAメイト年休取得データ追加
                    _dts.勤怠年休データ.Add勤怠年休データRow(jr);

                    // データベース更新
                    nAdp.Update(_dts.勤怠年休データ);

                    // 再読み込み
                    nAdp.Fill(_dts.勤怠年休データ);
                }
            }
        }

        /// ---------------------------------------------------------------------------
        /// <summary>
        ///     JAメイトＯＣＲデータＭＤＢ出力 </summary>
        /// <param name="r">
        ///     JAHR_OCRDataSet.確定勤務票ヘッダRow </param>
        /// <param name="m">
        ///     List : JAHR_OCRDataSet.確定勤務票明細Row </param>
        /// <returns>
        ///     JAHR_OCRDataSet.勤怠データRow</returns>
        /// ---------------------------------------------------------------------------
        private JAFA_OCRDataSet.勤怠データRow putJAMateOCRData(JAFA_OCRDataSet.確定勤務票ヘッダRow r)
        {
            // 対象期間の確定明細オブジェクトを取得します（入所日～退職日で絞り込み）
            List<JAFA_OCRDataSet.確定勤務票明細Row> mr = getKakuteiItem(r);
            
            // 月間集計値を求める
            double sSHUKKIN = 0;    // 普通出勤日数
            int sKYUSHU = 0;        // 法定休日出勤日数
            int KYUJITSU = 0;       // 休日日数
            int sFURIKYU = 0;       // 振替休日日数
            int sYUKYU = 0;         // 有休
            double sHANKYU = 0;     // 半休
            int kSEIRI = 0;         // 生理休暇
            int kKANGO = 0;         // 看護休暇
            int kKAIGO = 0;         // 介護休暇
            int kKEKKON = 0;        // 結婚休暇
            int kKIBIKI = 0;        // 忌引休暇
            int kSANZENSANGO = 0;   // 産前産後休暇
            int kRISAI = 0;         // 罹災休暇
            int kKAKURI = 0;        // 隔離休暇
            int kSONOTA = 0;        // その他休暇
            int ksIKUJI = 0;        // 育児休職
            int ksKAIGO = 0;        // 介護休職
            int keKEKKIN = 0;       // 自己都合欠勤 + 病気欠勤 + 無断欠勤
            int kSonotaKyushoku = 0;    // その他休暇・休職合計日数
            int workTimes = 0;          // 実労働時間（分単位）
            double workShinya = 0;      // 深夜時間    

            OCRData ocr = new OCRData();

            foreach (var t in mr)
            {
                string sk = string.Empty;

                // 休日のとき 2015/08/31
                if (t.出勤区分 == "")
                {
                    sk = t.出勤区分;
                }
                else
                {
                    // 出勤区分の先頭「０」を数値に変換して撤廃する 2015/08/25
                    sk = Utility.StrtoInt(t.出勤区分).ToString();
                }

                switch (sk)
                {
                    case SHUKIN_SHUKIN:     // 普通出勤日数
                        sSHUKKIN++;
                        break;

                    case SHUKIN_HANKYU:     // 普通出勤日数・有休半日
                        sSHUKKIN++;
                        sHANKYU += 0.5;
                        break;

                    case SHUKIN_KYUSHU:     // 法定休日出勤日数
                        sKYUSHU++;
                        break;
                        
                    case "":                // 休日日数
                        KYUJITSU++;
                        break;

                    case SHUKIN_FURIKYU:    // 振替休日
                        sFURIKYU++;
                        break;

                    case SHUKIN_YUKYU:      // 有給1日
                        sYUKYU++;
                        break;

                    case KYUKA_KEKKON:      // 結婚休暇日数
                        kKEKKON++;
                        break;

                    case KYUKA_KIBIKI:      // 忌引休暇日数
                        kKIBIKI++;
                        break;

                    case KYUKA_SEIRI:       // 生理休暇日数
                        kSEIRI++;
                        break;

                    case KEKKIN_BYOUKI:     // 欠勤
                        keKEKKIN++;
                        break;
                        
                    case KEKKIN_JIKO:       // 欠勤
                        keKEKKIN++;
                        break;
                    
                    case KEKKIN_MUDAN:  // 欠勤
                        keKEKKIN++;
                        break;

                    case KYUKA_KANGO:   // 看護休暇
                        kKANGO++;
                        break;

                    case KYUKA_KAIGO:   // 介護休暇
                        kKAIGO++;
                        break;

                    case KYUKA_RISAI:   // 罹災休暇
                        kRISAI++;
                        break;

                    case KYUKA_KAKURI:  // 隔離休暇
                        kKAKURI++;
                        break;

                    case KYUKA_SONOTA:  // その他休暇
                        kSONOTA++;
                        break;

                    case KYUSHOKU_KAIGO:    // 介護休職
                        ksKAIGO++;
                        break;

                    case KYUKA_SANZENSANGO: // 産前産後休暇
                        kSANZENSANGO++;
                        break;

                    case KYUSHOKU_IKUJI:    // 育児休職
                        ksIKUJI++;
                        break;

                    default:
                        break;
                }

                // 実労働時間
                workTimes += t.実働時 * 60 + t.実働分;

                // 日毎の深夜時間計算
                workShinya += ocr.getShinyaWorkTime_JA(t.開始時, t.開始分, t.終了時, t.終了分);
            }

            // ＪＡメイトＯＣＲデータRow新規インスタンス
            JAFA_OCRDataSet.勤怠データRow jr = _dts.勤怠データ.New勤怠データRow();

            jr.対象月度 = (r.年 + Properties.Settings.Default.rekiHosei).ToString() + r.月.ToString().PadLeft(2, '0');
            jr.対象職員コード = global.ROK + r.社員番号.ToString().PadLeft(5, '0');
            jr.対象職員名 = r.社員名;
            jr.対象職員所属コード = global.ROK + r.所属コード.PadLeft(5, '0');
            jr.対象職員所属名 = r.所属名;

            jr.普通出勤日数 = sSHUKKIN;
            jr.実労働時間 = workTimes;
            jr.残業時間 = getZangyoTime(r.年 + Properties.Settings.Default.rekiHosei, r.月, r.社員番号);
            jr.深夜時間 =(int) workShinya;
            jr.法定休日出勤日数 = sKYUSHU;
            jr.休日日数 = KYUJITSU;
            jr.振替休日日数 = sFURIKYU;
            jr.有給半日 = sHANKYU;
            jr.有給休暇 = sYUKYU;
            jr.欠勤日数 = keKEKKIN;
            jr.その他休暇休職合計日数 = kKEKKON + kKIBIKI + kSEIRI + kKANGO + kKAIGO + kRISAI + 
                                       kKAKURI + kSONOTA + ksKAIGO + kSANZENSANGO + ksIKUJI;
            jr.結婚休暇日数 = kKEKKON;
            jr.忌引休暇日数 = kKIBIKI;
            jr.生理休暇日数 = kSEIRI;
            jr.看護休暇日数 = kKANGO;
            jr.介護休暇日数 = kKAIGO;
            jr.罹災休暇日数 = kRISAI;
            jr.隔離休暇日数 = kKAKURI;
            jr.その他の特別休暇日数 = kSONOTA;
            jr.介護休職日数 = ksKAIGO;
            jr.産前産後休暇日数 = kSANZENSANGO;
            jr.育児休職日数 = ksIKUJI;

            //jr.遅刻回数 = string.Empty;
            //jr.早退回数 = string.Empty;
            //jr.仕様外出回数 = string.Empty;
            //jr.育児短時間 = string.Empty;
            //jr.介護短時間 = string.Empty;

            // string.emptyではなく「0」をセットする 2015/08/25
            jr.遅刻回数 = global.FLGOFF;
            jr.早退回数 = global.FLGOFF;
            jr.仕様外出回数 = global.FLGOFF;
            jr.育児短時間 = global.FLGOFF;
            jr.介護短時間 = global.FLGOFF;

            jr.交通費 = r.交通費;
            jr.日当 = r.日当;
            jr.宿泊費 = r.宿泊費;

            //jr.要出勤日数 = mr.Count() - KYUJITSU;     // 2018/06/22 コメント化
            jr.要出勤日数 = mr.Count() - KYUJITSU - sFURIKYU;    // 2018/06/22 要出勤日数から振替休日数を差し引く

            // 有休付与情報を初期化する
            jr.有休付与日数 = 0;
            jr.有休繰越日数 = 0;
            jr.有休付与対象フラグ = 0;
            
            return jr;
        }

        /// -----------------------------------------------------------
        /// <summary>
        ///     対象期間の確定明細オブジェクトを取得します </summary>
        /// <returns>
        ///     List(JAHR_OCRDataSet.確定勤務票明細Row)</returns>
        /// -----------------------------------------------------------
        private List<JAFA_OCRDataSet.確定勤務票明細Row> getKakuteiItem(JAFA_OCRDataSet.確定勤務票ヘッダRow r)
        {
            // 明細
            var s = _dts.確定勤務票明細.Where(a => a.ヘッダID == r.ヘッダID).OrderBy(a => a.日付);
            
            DateTime inDt = DateTime.Parse("1900/01/01");
            DateTime reDt = DateTime.Parse("2999/12/31");

            // 当年月
            int yyyy = r.年 + Properties.Settings.Default.rekiHosei;
            int mm = r.月;

            // 入所日・退職日取得
            clsGetMst ms = new clsGetMst();
            JAFA_OCRDataSet.メイトマスターRow sr = ms.getKojinMstRow(r.社員番号);
            if (!sr.IsNull(0))
            {
                inDt = sr.入所年月日;
                reDt = sr.退職年月日;
            }

            // 当月が入所月のとき入所日以降で絞り込む
            if (yyyy == inDt.Year && mm == inDt.Month)
            {
                s = s.Where(a => a.日付 >= inDt.Day).OrderBy(a => a.日付);
            }

            // 当月が退職月のとき退職日以前で絞り込む
            if (yyyy == reDt.Year && mm == reDt.Month)
            {
                s = s.Where(a => a.日付 <= reDt.Day).OrderBy(a => a.日付);
            }

            // 対象期間の確定明細データを取得
            List<JAFA_OCRDataSet.確定勤務票明細Row> rtn = s.ToList();
            return rtn;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        ///     週実績明細データ登録 </summary>
        /// <param name="h">
        ///     JAHR_OCRDataSet.確定勤務票ヘッダRow</param>
        /// <param name="c">
        ///     List(JAHR_OCRDataSet.確定勤務票明細Row)</param>
        /// ----------------------------------------------------------------------------
        public void setNewWeekDataItem(JAFA_OCRDataSet.確定勤務票ヘッダRow h)
        {
            // 対象期間の確定明細オブジェクトを取得します（入所日～退職日で絞り込み）
            List<JAFA_OCRDataSet.確定勤務票明細Row> mr = getKakuteiItem(h);

            foreach (var m in mr)
            {
                JAFA_OCRDataSet.週実績明細Row r = _dts.週実績明細.New週実績明細Row();

                r.職員コード = h.社員番号;
                r.年月日 = DateTime.Parse((h.年 + Properties.Settings.Default.rekiHosei).ToString() + "/" + h.月 + "/" + m.日付);
                r.処理年 = h.年;
                r.処理月 = h.月;
                r.集計年 = h.年;
                r.集計月 = h.月;
                r.週番号 = Utility.StrtoInt(Utility.NulltoStr(m.週番号));

                //// 2015/12 まで
                //r.勤務時間 = m.実働時 * 60 + m.実働分;

                //int wt = m.実働時 * 60 + m.実働分;
                //if (wt > 480)
                //{
                //    r.残業時間 = wt - 480;
                //}
                //else
                //{
                //    r.残業時間 = 0;
                //}

                // 休日出勤でないとき　2016/01/26
                if (m.出勤区分 != SHUKIN_KYUSHU)
                {
                    r.勤務時間 = m.実働時 * 60 + m.実働分;

                    int wt = m.実働時 * 60 + m.実働分;
                    if (wt > 480)
                    {
                        r.残業時間 = wt - 480;
                    }
                    else
                    {
                        r.残業時間 = 0;
                    }
                }
                else
                {
                    // 休日出勤のときは残業計算に含めない 2016/01/26
                    r.勤務時間 = 0;
                    r.残業時間 = 0;
                }

                r.更新年月日 = DateTime.Now;

                // 登録済みデータ存在チェック
                JAFA_OCRDataSet.週実績明細Row c = _dts.週実績明細.FindBy職員コード年月日(r.職員コード, r.年月日);

                if (c != null)
                {
                    // 登録済みデータ削除
                    c.Delete();
                }

                // 週実績明細Row追加
                _dts.週実績明細.Add週実績明細Row(r);                
            }
        }

        
        ///----------------------------------------------------------------------------
        /// <summary>
        ///     配列にテキストデータをセットする </summary>
        /// <param name="array">
        ///     社員、パート、出向社員の各配列</param>
        /// <param name="cnt">
        ///     拡張する配列サイズ</param>
        /// <param name="txtData">
        ///     セットする文字列</param>
        ///----------------------------------------------------------------------------
        private void txtArraySet(string [] array, int cnt, string txtData)
        {
            Array.Resize(ref array, cnt);   // 配列のサイズ拡張
            array[cnt - 1] = txtData;       // 文字列のセット
        }
        
        ///----------------------------------------------------------------------------
        /// <summary>
        ///     CSVファイルを出力する</summary>
        /// <param name="sPath">
        ///     出力するパス</param>
        /// <param name="arrayData">
        ///     書き込む配列データ</param>
        /// <param name="sFileName">
        ///     CSVファイル名</param>
        ///----------------------------------------------------------------------------
        //private void csvFileWrite(string sPath, string [] arrayData, string sFileName)
        //{
        //    // ファイル名
        //    string outFileName = sPath + sFileName + ".csv";

        //    // 出力ファイルが存在するとき
        //    if (System.IO.File.Exists(outFileName))
        //    {
        //        // リネーム付加文字列（タイムスタンプ）
        //        string newFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') +
        //                             DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') +
        //                             DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

        //        // リネーム後ファイル名
        //        string reFileName = sPath + sFileName + newFileName + ".csv";

        //        // 既存のファイルをリネーム
        //        File.Move(outFileName, reFileName);
        //    }

        //    // CSVファイル出力
        //    File.WriteAllLines(outFileName, arrayData, System.Text.Encoding.GetEncoding(932));
        //}

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     JAメイトOCRデータ CSVファイル出力　</summary>
        /// <param name="sYear">
        ///     対象年</param>
        /// <param name="sMonth">
        ///     対象月</param>
        /// -------------------------------------------------------------------------
        public void saveJAMateOcrCsv(int sYear, int sMonth)
        {
            // 出力配列
            string[] arrayCsv = null;

            // 対象月度
            string YYYYMM = sYear.ToString() + sMonth.ToString().PadLeft(2, '0');
            
            StringBuilder sb = new StringBuilder();
            int cnt = 0;

            foreach (var t in _dts.勤怠データ.Where(a => a.対象月度 == YYYYMM).OrderBy(a => a .対象職員コード))
            {
                cnt++;

                sb.Clear();
                sb.Append(t.対象月度).Append(",");
                sb.Append(t.対象職員コード).Append(",");
                sb.Append(t.対象職員名).Append(",");
                sb.Append(t.対象職員所属コード).Append(",");
                sb.Append(t.対象職員所属名).Append(",");
                sb.Append(t.普通出勤日数).Append(",");
                sb.Append(t.実労働時間).Append(",");
                sb.Append(t.残業時間).Append(",");
                sb.Append(t.深夜時間).Append(",");
                sb.Append(t.法定休日出勤日数).Append(",");
                sb.Append(t.休日日数).Append(",");
                sb.Append(t.振替休日日数).Append(",");
                sb.Append(t.有給半日).Append(",");
                sb.Append(t.有給休暇).Append(",");
                sb.Append(t.欠勤日数).Append(",");
                sb.Append(t.その他休暇休職合計日数).Append(",");
                sb.Append(t.結婚休暇日数).Append(",");
                sb.Append(t.忌引休暇日数).Append(",");
                sb.Append(t.生理休暇日数).Append(",");
                sb.Append(t.看護休暇日数).Append(",");
                sb.Append(t.介護休暇日数).Append(",");
                sb.Append(t.罹災休暇日数).Append(",");
                sb.Append(t.隔離休暇日数).Append(",");
                sb.Append(t.その他の特別休暇日数).Append(",");
                sb.Append(t.介護休職日数).Append(",");
                sb.Append(t.産前産後休暇日数).Append(",");
                sb.Append(t.育児休職日数).Append(",");
                sb.Append(t.遅刻回数).Append(",");
                sb.Append(t.早退回数).Append(",");
                sb.Append(t.仕様外出回数).Append(",");
                sb.Append(t.育児短時間).Append(",");
                sb.Append(t.介護短時間).Append(",");
                sb.Append(t.交通費).Append(",");
                sb.Append(t.日当).Append(",");
                sb.Append(t.宿泊費).Append(",");
                sb.Append(t.要出勤日数).Append(",");
                sb.Append(t.有休付与日数).Append(",");
                sb.Append(t.有休繰越日数).Append(",");
                sb.Append(t.有休付与対象フラグ);

                // 配列にセット
                Array.Resize(ref arrayCsv, cnt);        // 配列のサイズ拡張
                arrayCsv[cnt - 1] = sb.ToString();      // 文字列のセット
            }

            // CSVファイル出力
            Utility.csvFileWrite(global.cnfPath, arrayCsv, CSVFILENAME);
        }
        
        /// -------------------------------------------------------------------------
        /// <summary>
        ///     JAメイト年休取得データ CSVファイル出力　</summary>
        /// <param name="sYear">
        ///     対象年</param>
        /// <param name="sMonth">
        ///     対象月</param>
        /// -------------------------------------------------------------------------
        public void saveJAMateNenkyuCsv(int sYear, int sMonth)
        {
            // 出力配列
            string[] arrayCsv = null;

            // 対象月度
            string YYYYMM = sYear.ToString() + sMonth.ToString().PadLeft(2, '0');

            StringBuilder sb = new StringBuilder();
            int cnt = 0;

            foreach (var t in _dts.勤怠年休データ.Where(a => a.対象月度 == YYYYMM).OrderBy(a => a.対象職員コード))
            {
                cnt++;

                sb.Clear();
                sb.Append(t.対象月度).Append(",");
                sb.Append(t.対象職員コード).Append(",");
                sb.Append(t.対象職員名).Append(",");
                sb.Append(t.対象職員所属コード).Append(",");
                sb.Append(t.対象職員所属名).Append(",");
                sb.Append(t.取得日).Append(",");
                sb.Append(t.取得日数);

                // 配列にセット
                Array.Resize(ref arrayCsv, cnt);        // 配列のサイズ拡張
                arrayCsv[cnt - 1] = sb.ToString();      // 文字列のセット
            }

            // CSVファイル出力
            if (cnt > 0)
            {
                Utility.csvFileWrite(global.cnfPath, arrayCsv, CSVNENKYUNAME);
            }
        }
        
        /// -----------------------------------------------------------------------
        /// <summary>
        ///     有給休暇付与マスター作成 </summary>
        /// -----------------------------------------------------------------------
        public void addYukyuData()
        {
            int fuyo = 0;   // 当年付与日数
            int zfuyo = 0;  // 前年付与日数

            // 有休付与月
            int fYear = global.cnfYear + Properties.Settings.Default.rekiHosei;
            int fMonth = global.cnfMonth + 1;
            if (fMonth > 12)
            {
                fYear++;
                fMonth -= 12;
            }

            // 算定期間
            string sMM = string.Empty;

            if ((global.cnfMonth + 1) > 12)
            {
                sMM = "01";
            }
            else
            {
                sMM = (global.cnfMonth + 1).ToString().PadLeft(2, '0');
            }

            string sYYYYMM = string.Empty;

            // 算定開始年月
            if (sMM == "01")
            {
                // 1月付与のとき当年の1月から：2017/01/30
                sYYYYMM = (global.cnfYear + Properties.Settings.Default.rekiHosei).ToString() + sMM;
            }
            else
            {
                // ２～12月付与のとき前年の付与月から
                sYYYYMM = (global.cnfYear + Properties.Settings.Default.rekiHosei - 1).ToString() + sMM;
            }

            // 算定終了年月
            string eYYYYMM = (global.cnfYear + Properties.Settings.Default.rekiHosei).ToString() + global.cnfMonth.ToString().PadLeft(2, '0');
            
            decimal y;       // 要出勤日数
            decimal k;       // 欠勤日数
            decimal sRT;     // 出勤率
            decimal dd;      // 有休＋半休日数

            // 有休付与対象者抽出
            foreach (var s in _dts.メイトマスター.Where(a => a.有給付与月 == fMonth))
            {
                zfuyo = 0;

                // 出勤率が80％以上の職員を対象とする
                if (getShukinRt(Utility.StrtoInt(sYYYYMM), Utility.StrtoInt(eYYYYMM), global.ROK + s.職員コード.ToString().PadLeft(5, '0'),
                    out y, out k, out sRT, out dd))
                {
                    // 週所定労働日数
                    int wDays = s.週所定労働日数;

                    // 勤続年数
                    //decimal wYY = fYear - s.調整年月日.Year - 1 + 0.5M;
                    decimal wYY = fYear - s.調整年月日.Year;
                    if (fMonth < s.調整年月日.Month)
                    {
                        wYY--;
                    }
                    wYY += 0.5M;

                    // 有休付与日数取得                
                    foreach (var j in _dts.有休付与日数表.Where(a => a.週所定労働日数 == wDays))
                    {
                        if (wYY == 0.5M)
                        {
                            fuyo = j.勤続05年;
                            zfuyo = 0;
                        }
                        else if (wYY == 1.5M)
                        {
                            fuyo = j.勤続15年;
                            zfuyo = j.勤続05年;    // 前年付与実績データがない場合の仮日数：2015/09/30
                        }
                        else if (wYY == 2.5M)
                        {
                            fuyo = j.勤続25年;
                            zfuyo = j.勤続15年;    // 前年付与実績データがない場合の仮日数：2015/09/30
                        }
                        else if (wYY == 3.5M)
                        {
                            fuyo = j.勤続35年;
                            zfuyo = j.勤続25年;    // 前年付与実績データがない場合の仮日数：2015/09/30
                        }
                        else if (wYY == 4.5M)
                        {
                            fuyo = j.勤続45年;
                            zfuyo = j.勤続35年;    // 前年付与実績データがない場合の仮日数：2015/09/30
                        }
                        else if (wYY == 5.5M)
                        {
                            fuyo = j.勤続55年;
                            zfuyo = j.勤続45年;    // 前年付与実績データがない場合の仮日数：2015/09/30
                        }
                        else if (wYY == 6.5M)
                        {
                            fuyo = j.勤続65年;
                            zfuyo = j.勤続55年;    // 前年付与実績データがない場合の仮日数：2015/09/30
                        }
                        else if (wYY > 6.5M)
                        {
                            fuyo = j.勤続65年;
                            zfuyo = j.勤続65年;    // 前年付与実績データがない場合の仮日数：2015/09/30
                        }
                    }
                }
                else
                {
                    fuyo = 0;   // 出勤率が80％未満のとき、明示的に当年付与日数を０にする：2018/04/12
                }

                // 有給休暇付与マスター追加Row取得
                JAFA_OCRDataSet.有給休暇付与マスターRow r = _dts.有給休暇付与マスター.New有給休暇付与マスターRow();
                r.社員番号 = s.職員コード;
                r.年 = fYear;
                r.月 = fMonth;
                r.前年初有給残日数 = 0;

                // 前年の有給休暇付与マスターのレコードが存在するか
                if (_dts.有給休暇付与マスター.Any(a => a.社員番号 == s.職員コード && a.年 == fYear - 1 && a.月 == fMonth))
                {
                    // 前年の有給休暇付与マスターより前年初有給残日数（当年初有給残日数）を求めます
                    var zy = _dts.有給休暇付与マスター.Where(a => a.社員番号 == s.職員コード && a.年 == fYear - 1 && a.月 == fMonth);
                    foreach (var z in zy)
                    {
                        r.前年初有給残日数 = z.当年初有給残日数;

                        // 繰越日数は前年の付与実績から求める：2015/09/30
                        zfuyo = (int)z.当年付与日数;

                        break;
                    }
                }
                else
                {
                    // 前年の有給休暇付与マスターExcelシートが存在するとき
                    if (mstSheet != null)
                    {
                        // 前年の有給休暇付与Excelシートより前年初有給残日数（当年初有給残日数）を求めます
                        foreach (var x in mstSheet.Where(a => a.sCode == s.職員コード.ToString() && a.sYY == (fYear - 1).ToString() && a.sMM == fMonth.ToString()))
                        {
                            r.前年初有給残日数 = Utility.StrtoDouble(x.sNensho);

                            // 繰越日数は前年の付与実績から求める：2015/09/30
                            zfuyo = Utility.StrtoInt(x.sFuyo);

                            break;
                        }
                    }
                }
                
                r.前年有休消化日数 = (double)dd;
                r.当年付与日数 = fuyo;

                // 有休消化日数より前年初有給残日数が多いとき繰越日数を計算します
                if (r.前年初有給残日数 >= (double)dd)
                {
                    double zan = r.前年初有給残日数 - (double)dd;

                    // 有給残が前年付与日数より多いとき
                    if (zan > zfuyo)
                    {
                        // 繰越日数は前年付与日数とします
                        r.当年繰越日数 = (double)zfuyo;
                    }
                    else
                    {
                        r.当年繰越日数 = zan;
                    }
                }
                else
                {
                    r.当年繰越日数 = 0;
                }

                r.当年初有給残日数 = r.当年付与日数 + r.当年繰越日数;
                r.出勤率算定開始年月日 = DateTime.Parse(sYYYYMM.Substring(0, 4) + "/" + sYYYYMM.Substring(4, 2) + "/01");
                r.出勤率算定終了年月日 = r.出勤率算定開始年月日.AddYears(1).AddDays(-1);
                r.要出勤日数 = (int)y;
                r.欠勤日数 = (int)k;
                r.出勤率 = (double)sRT;
                r.更新年月日 = DateTime.Now;

                // 社員番号、年、月で登録済みデータを取得
                foreach (var dr in _dts.有給休暇付与マスター.Where(a => a.社員番号 == s.職員コード && a.年 == fYear && a.月 == fMonth))
                {
                    // 登録済みデータを削除します
                    dr.Delete();
                }

                // 有給休暇付与マスターを追加登録
                _dts.有給休暇付与マスター.Add有給休暇付与マスターRow(r);

                // データベース更新
                ymsAdp.Update(_dts.有給休暇付与マスター);

                // 再読み込み
                ymsAdp.Fill(_dts.有給休暇付与マスター);
            }

            // データベース更新
            ymsAdp.Update(_dts.有給休暇付与マスター);

            // 再読み込み
            ymsAdp.Fill(_dts.有給休暇付与マスター);
        }

        /// ----------------------------------------------------------------------
        /// <summary>
        ///     出勤率が80%以上か調べて算定期間中の「要出勤日数」「欠勤日数」「出勤率」「有休+半休」を返す</summary>
        /// <param name="sYM">
        ///     算定開始年月 YYYYMM</param>
        /// <param name="eYM">
        ///     算定終了年月 YYYYMM</param>
        /// <param name="sCode">
        ///     社員コード（ROK00000）</param>
        /// <param name="y">
        ///     要出勤日数</param>
        /// <param name="k">
        ///     欠勤日数</param>
        /// <param name="sRT">
        ///     出勤率</param>
        /// <param name="dd">
        ///     有休+半休</param>
        /// <returns>
        ///     80%以上: true, 80%未満: false</returns>
        /// ----------------------------------------------------------------------
        private bool getShukinRt(int sYM, int eYM, string sCode, out decimal y, out decimal k, out decimal sRT, out decimal dd)
        {
            y = 0;
            k = 0;
            sRT = 0;
            dd = 0;

            // 過去１年間の勤怠データから日数を取得する
            foreach (var t in _dts.勤怠データ.Where(a => Utility.StrtoInt(a.対象月度) >= sYM && Utility.StrtoInt(a.対象月度) <= eYM && a.対象職員コード == sCode))
            {
                y += t.要出勤日数;
                k += t.欠勤日数;
                dd += (decimal)t.有給休暇 + (decimal)t.有給半日;
            }

            // Excel過去１年間有給取得ファイルが存在するとき
            if (workSheet != null)
            {
                // Excel過去１年間有給取得シートから日数を取得する
                foreach (var t in workSheet.Where(a => a.sCode == sCode.Substring(3, 5)))
                {
                    if (Utility.StrtoInt(t.sYYMM) >= sYM && Utility.StrtoInt(t.sYYMM) <= eYM)
                    {
                        y += Utility.StrtoInt(t.sYouDay);               // 要出勤日数
                        k += Utility.StrtoInt(t.sKekkin);               // 欠勤
                        dd += (decimal)Utility.StrtoDouble(t.sTotal);   // 有休＋半休
                    }
                }
            }
            
            // 出勤率計算
            if (y != 0)
            {
                sRT = (y - k) * 100 / y;
            }
            else
            {
                sRT = 0;
            }

            if (sRT >= D80)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
