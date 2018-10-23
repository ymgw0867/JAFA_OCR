using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using LinqToExcel;

namespace JAFA_DATA.Common
{
    class OCRData
    {
        // 奉行シリーズデータ領域データベース名
        string _dbName = string.Empty;

        #region エラー項目番号プロパティ
        //---------------------------------------------------
        //          エラー情報
        //---------------------------------------------------
        /// <summary>
        ///     エラーヘッダ行RowIndex</summary>
        public int _errHeaderIndex { get; set; }

        /// <summary>
        ///     エラー項目番号</summary>
        public int _errNumber { get; set; }

        /// <summary>
        ///     エラー明細行RowIndex </summary>
        public int _errRow { get; set; }

        /// <summary> 
        ///     エラーメッセージ </summary>
        public string _errMsg { get; set; }

        /// <summary> 
        ///     エラーなし </summary>
        public int eNothing = 0;

        /// <summary> 
        ///     エラー項目 = 対象年月日 </summary>
        public int eYearMonth = 1;

        /// <summary> 
        ///     エラー項目 = 対象月 </summary>
        public int eMonth = 2;

        /// <summary> 
        ///     エラー項目 = 日 </summary>
        public int eDay = 3;
        
        /// <summary> 
        ///     エラー項目 = 個人番号 </summary>
        public int eShainNo = 4;

        /// <summary> 
        ///     エラー項目 = 出勤区分 </summary>
        public int eKintaiKigou = 5;

        /// <summary> 
        ///     エラー項目 = 開始時 </summary>
        public int eSH = 6;

        /// <summary> 
        ///     エラー項目 = 開始分 </summary>
        public int eSM = 7;

        /// <summary> 
        ///     エラー項目 = 終了時 </summary>
        public int eEH = 8;

        /// <summary> 
        ///     エラー項目 = 終了分 </summary>
        public int eEM = 9;

        /// <summary> 
        ///     エラー項目 = 休憩開始時 </summary>
        public int eKSH = 10;

        /// <summary> 
        ///     エラー項目 = 休憩開始分 </summary>
        public int eKSM = 11;

        /// <summary> 
        ///     エラー項目 = 休憩終了時 </summary>
        public int eKEH = 12;

        /// <summary> 
        ///     エラー項目 = 休憩終了分 </summary>
        public int eKEM = 13;

        /// <summary> 
        ///     エラー項目 = 実労働時間 </summary>
        public int eWH = 14;

        /// <summary> 
        ///     エラー項目 = 実労働分 </summary>
        public int eWM = 15;

        /// <summary> 
        ///     エラー項目 = 事由１ </summary>
        public int eJiyu1 = 16;

        /// <summary> 
        ///     エラー項目 = 事由２ </summary>
        public int eJiyu2 = 17;

        /// <summary> 
        ///     エラー項目 = 週間出勤記号 </summary>
        public int eWeek = 18;

        /// <summary> 
        ///     エラー項目 = 有給残日数超過 </summary>
        public int eYukyuZan = 19;
        #endregion
        
        #region 警告項目
        ///     <!--警告項目配列 -->
        public int[] warArray = new int[6];

        /// <summary>
        ///     警告項目番号</summary>
        public int _warNumber { get; set; }

        /// <summary>
        ///     警告明細行RowIndex </summary>
        public int _warRow { get; set; }

        /// <summary> 
        ///     警告項目 = 勤怠記号1&2 </summary>
        public int wKintaiKigou = 0;

        /// <summary> 
        ///     警告項目 = 開始終了時分 </summary>
        public int wSEHM = 1;

        /// <summary> 
        ///     警告項目 = 時間外時分 </summary>
        public int wZHM = 2;

        /// <summary> 
        ///     警告項目 = 深夜勤務時分 </summary>
        public int wSIHM = 3;

        /// <summary> 
        ///     警告項目 = 休日出勤時分 </summary>
        public int wKSHM = 4;

        /// <summary> 
        ///     警告項目 = 出勤形態 </summary>
        public int wShukeitai = 5;

        #endregion

        #region フィールド定義
        /// <summary> 
        ///     警告項目 = 時間外1.25時 </summary>
        public int [] wZ125HM = new int[global.MAX_GYO];

        /// <summary> 
        ///     実働時間 </summary>
        public double _workTime;

        /// <summary> 
        ///     深夜稼働時間 </summary>
        public double _workShinyaTime;
        #endregion

        #region 単位時間フィールド
        /// <summary> 
        ///     ３０分単位 </summary>
        private int tanMin30 = 30;

        /// <summary> 
        ///     １５分単位 </summary> 
        private int tanMin15 = 15;

        /// <summary> 
        ///     １分単位 </summary>
        private int tanMin1 = 1;
        #endregion

        #region 時間チェック記号定数
        private const string cHOUR = "H";           // 時間をチェック
        private const string cMINUTE = "M";         // 分をチェック
        private const string cTIME = "HM";          // 時間・分をチェック
        #endregion

        #region 出勤区分定数
        const string SHUKIN_SHUKIN = "1";       // 出勤
        const string SHUKIN_YUKYU = "2";        // 有休
        const string SHUKIN_HANKYU = "3";       // 半休
        const string SHUKIN_KYUSHU = "5";       // 休日出勤
        #endregion

        // 週日数 ：月最初の週開始曜日以降
        int cDays = 0;

        // 週勤務した日 ：月最初の週開始曜日以降
        int wDays = 0;

        // 休日出勤日 ：月最初の週開始曜日以降
        int hWorkDays = 0;

        // 週日数 ：月最初の週開始曜日以前
        int cDaysF = 0;
        int cDaysF2 = 0;

        // 週勤務した日 ：月最初の週開始曜日以前
        int wDaysF = 0;
        int wDaysF2 = 0;

        // 休日出勤日 ：月最初の週開始曜日以前
        int hWorkDaysF = 0;
        int hWorkDaysF2 = 0;

        // 該当社員の週開始曜日
        int kaishiYoubi = 0;

        // テーブルアダプターマネージャーインスタンス
        JAFA_DATADataSetTableAdapters.TableAdapterManager adpMn = new JAFA_DATADataSetTableAdapters.TableAdapterManager();
        JAFA_OCRDataSetTableAdapters.TableAdapterManager adpOCR = new JAFA_OCRDataSetTableAdapters.TableAdapterManager();

        JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter yAdp = new JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter();
        JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter kAdp = new JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter();

        // 有給休暇残日数チェック用 2015/07/02
        LinqToExcel.Query.ExcelQueryable<exlMntData> workSheet = null;
        LinqToExcel.Query.ExcelQueryable<exlYukyuMst> mstSheet = null;

        ///-----------------------------------------------------------------------
        /// <summary>
        ///     ＣＳＶデータをＭＤＢに登録する：DataSet Version </summary>
        /// <param name="_InPath">
        ///     CSVデータパス</param>
        /// <param name="frmP">
        ///     プログレスバーフォームオブジェクト</param>
        /// <param name="dts">
        ///     データセット</param>
        ///-----------------------------------------------------------------------
        public void CsvToMdb(string _InPath, frmPrg frmP, JAFA_DATADataSet dts)
        {
            string headerKey = string.Empty;    // ヘッダキー

            // テーブルセットオブジェクト
            JAFA_DATADataSet tblSt = new JAFA_DATADataSet();

            try
            {
                // 勤務表ヘッダデータセット読み込み
                JAFA_DATADataSetTableAdapters.勤務票ヘッダTableAdapter hAdp = new JAFA_DATADataSetTableAdapters.勤務票ヘッダTableAdapter();
                adpMn.勤務票ヘッダTableAdapter = hAdp;
                //adpMn.勤務票ヘッダTableAdapter.Fill(tblSt.勤務票ヘッダ);  // 2018/10/23 コメント化

                // 勤務表明細データセット読み込み
                JAFA_DATADataSetTableAdapters.勤務票明細TableAdapter iAdp = new JAFA_DATADataSetTableAdapters.勤務票明細TableAdapter();
                adpMn.勤務票明細TableAdapter = iAdp;
                //adpMn.勤務票明細TableAdapter.Fill(tblSt.勤務票明細);  // 2018/10/23 コメント化

                // 対象CSVファイル数を取得
                int cLen = System.IO.Directory.GetFiles(_InPath, "*.csv").Count();

                //CSVデータをMDBへ取込
                int cCnt = 0;
                foreach (string files in System.IO.Directory.GetFiles(_InPath, "*.csv"))
                {
                    //件数カウント
                    cCnt++;

                    //プログレスバー表示
                    frmP.Text = "OCR変換CSVデータロード中　" + cCnt.ToString() + "/" + cLen.ToString();
                    frmP.progressValue = cCnt * 100 / cLen;
                    frmP.ProgressStep();

                    ////////OCR処理対象のCSVファイルかファイル名の文字数を検証する
                    //////string fn = Path.GetFileName(files);

                    int sDays = 0;              // 日付
                    int zkKbn = global.flgOff;  // 前半後半

                    // CSVファイルインポート
                    var s = System.IO.File.ReadAllLines(files, Encoding.Default);
                    foreach (var stBuffer in s)
                    {
                        // カンマ区切りで分割して配列に格納する
                        string[] stCSV = stBuffer.Split(',');

                        // ヘッダ行
                        if (stCSV[0] == "*")
                        {
                            // ヘッダーキー取得
                            headerKey = Utility.GetStringSubMax(stCSV[3].Trim(), 17);   

                            // 前半後半を判定
                            if (stCSV[1].Trim() == global.FLGOFF || stCSV[2].Trim() == global.FLGOFF)
                            {
                                zkKbn = global.flgZenhan;   // 前半
                                sDays = 0;  // 日付初期値
                            }
                            else
                            {
                                zkKbn = global.flgKouhan;   // 後半
                                sDays = 15; // 日付初期値
                            }

                            // MDBへ登録する：勤務票ヘッダテーブル
                            tblSt.勤務票ヘッダ.Add勤務票ヘッダRow(setNewHeadRecRow(tblSt, stCSV, zkKbn));
                        }
                        else
                        {
                            // 勤務票明細テーブル
                            DateTime dt;

                            sDays++;

                            // 日付を取得
                            string tempDt = global.cnfYear.ToString() + "/" + global.cnfMonth.ToString() + "/" + sDays.ToString();

                            // 存在する日付か？
                            if (DateTime.TryParse(tempDt, out dt))
                            {
                                // 前半は15日まで
                                if ((zkKbn == global.flgZenhan && sDays < 16) || zkKbn == global.flgKouhan)
                                {
                                    // データセットに勤務報告書明細データを追加する
                                    tblSt.勤務票明細.Add勤務票明細Row(setNewItemRecRow(tblSt, headerKey, stCSV, sDays));
                                }
                            }
                        }
                    }
                }

                // データベースへ反映
                adpMn.UpdateAll(tblSt);

                //CSVファイルを削除する
                foreach (string files in System.IO.Directory.GetFiles(_InPath, "*.csv"))
                {
                    System.IO.File.Delete(files);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "勤務票CSVインポート処理", MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        ///     追加用JAFA_DATADataSet.勤務票ヘッダRowオブジェクトを作成する </summary>
        /// <param name="tblSt">
        ///     テーブルセット</param>
        /// <param name="stCSV">
        ///     CSV配列</param>
        /// <param name="zkKbn">
        ///     前半後半区分：０：前半、１：後半</param>
        /// <returns>
        ///     追加するJAFA_DATADataSet.勤務票ヘッダRowオブジェクト</returns>
        ///---------------------------------------------------------------------------------
        private JAFA_DATADataSet.勤務票ヘッダRow setNewHeadRecRow(JAFA_DATADataSet tblSt, string[] stCSV, int zkKbn)
        {
            // 勤務票ヘッダRowへデータをセットします
            JAFA_DATADataSet.勤務票ヘッダRow r = tblSt.勤務票ヘッダ.New勤務票ヘッダRow();
            r.ID = Utility.GetStringSubMax(stCSV[3].Trim(), 17);
            //r.年 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[4].Trim().Replace("-", ""), 2)); 2018/10/19 コメント化
            r.年 = 2000 + Utility.StrtoInt(Utility.GetStringSubMax(stCSV[4].Trim().Replace("-", ""), 2)); // FA仕様：西暦化 2018/10/19
            r.月 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[5].Trim().Replace("-", ""), 2));
            r.社員番号 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[6].Trim().Replace("-", ""), 5));

            string [] sName = new string[6];
            clsGetMst ms = new clsGetMst();
            sName = ms.getKojinMst(r.社員番号);
            r.社員名 = sName[0];
            r.所属コード = sName[2];
            r.所属名 = sName[3];

            // 前半のとき
            if (zkKbn == global.flgZenhan)
            {
                r.交通費 = global.flgOff;
                r.日当 = global.flgOff;
                r.宿泊費 = global.flgOff;
                r.前半処理 = global.flgOn;
                r.後半処理 = global.flgOff;
            }
            else
            {
                // 後半のとき
                r.交通費 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[7].Trim().Replace("-", ""), 5));
                r.日当 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[8].Trim().Replace("-", ""), 5));
                r.宿泊費 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[9].Trim().Replace("-", ""), 5));
                r.前半処理 = global.flgOff;
                r.後半処理 = global.flgOn;
            }

            r.確定 = global.flgOff;
            r.更新年月日 = DateTime.Now;

            return r;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        ///     追加用JAFA_DATADataSet.勤務票明細Rowオブジェクトを作成する</summary>
        /// <param name="headerKey">
        ///     ヘッダキー</param>
        /// <param name="stCSV">
        ///     CSV配列</param>
        /// <returns>
        ///     追加するJAFA_DATADataSet.勤務票明細Rowオブジェクト</returns>
        ///---------------------------------------------------------------------------------
        private JAFA_DATADataSet.勤務票明細Row setNewItemRecRow(JAFA_DATADataSet tblSt, string headerKey, string[] stCSV, int day)
        {
            JAFA_DATADataSet.勤務票明細Row r = tblSt.勤務票明細.New勤務票明細Row();

            r.ヘッダID = headerKey;
            r.日付 = day;

            // 出勤区分の先頭ゼロ消去：2015/09/24
            string skbn = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[0].Trim().Replace("-", ""), 2)).ToString();
            if (skbn == "0")
            {
                skbn = string.Empty;
            }

            r.出勤区分 = skbn;
            r.開始時 = Utility.GetStringSubMax(stCSV[1].Trim().Replace("-", ""), 2);
            r.開始分 = Utility.GetStringSubMax(stCSV[2].Trim().Replace("-", ""), 2);
            r.終了時 = Utility.GetStringSubMax(stCSV[3].Trim().Replace("-", ""), 2);
            r.終了分 = Utility.GetStringSubMax(stCSV[4].Trim().Replace("-", ""), 2);
            r.休憩開始時 = Utility.GetStringSubMax(stCSV[5].Trim().Replace("-", ""), 1);
            r.休憩開始分 = Utility.GetStringSubMax(stCSV[6].Trim().Replace("-", ""), 2);

            // 出勤時刻、退勤時刻、休憩時間から実働時間を取得する
            OCRData ocr = new OCRData();
            double wTime = ocr.getWorkTime(r.開始時, r.開始分, r.終了時, r.終了分, r.休憩開始時, r.休憩開始分);
            
            // 実働時間
            if (wTime >= 0)
            {
                double wTimeH = Math.Floor(wTime / 60);
                double wTimeM = wTime % 60;
                r.実働時 = (int)wTimeH;
                r.実働分 = (int)wTimeM;
            }
            else
            {
                r.実働時 = 0;
                r.実働分 = 0;
            }

            r.訂正 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[7].Trim().Replace("-", ""), 1));
            r.画像名 = headerKey + ".tif";
            r.更新年月日 = DateTime.Now;
            
            return r;
        }

        ///----------------------------------------------------------------------------------------
        /// <summary>
        ///     値1がemptyで値2がNot string.Empty のとき "0"を返す。そうではないとき値1をそのまま返す</summary>
        /// <param name="str1">
        ///     値1：文字列</param>
        /// <param name="str2">
        ///     値2：文字列</param>
        /// <returns>
        ///     文字列</returns>
        ///----------------------------------------------------------------------------------------
        private string hmStrToZero(string str1, string str2)
        {
            string rVal = str1;
            if (str1 == string.Empty && str2 != string.Empty)
                rVal = "0";

            return rVal;
        }

        ///--------------------------------------------------------------------------------------------------
        /// <summary>
        ///     エラーチェックメイン処理。
        ///     エラーのときOCRDataクラスのヘッダ行インデックス、フィールド番号、明細行インデックス、
        ///     エラーメッセージが記録される </summary>
        /// <param name="sIx">
        ///     開始ヘッダ行インデックス</param>
        /// <param name="eIx">
        ///     終了ヘッダ行インデックス</param>
        /// <param name="frm">
        ///     親フォーム</param>
        /// <param name="dts">
        ///     データセット</param>
        /// <returns>
        ///     True:エラーなし、false:エラーあり</returns>
        ///-----------------------------------------------------------------------------------------------
        public Boolean errCheckMain(int sIx, int eIx, Form frm, JAFA_DATADataSet dts)
        {
            // 2017/11/02 前月の年月を取得
            int sYY = global.cnfYear;
            int sMM = global.cnfMonth - 1;
            if (sMM == 0)
            {
                sYY--;
                sMM = 12;
            }

            int sYYMM = sYY * 100 + sMM;

            // 2017/11/02 過去勤務データは前月分を取得
            JAFA_OCRDataSet cDts = new JAFA_OCRDataSet();
            JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter phAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
            JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter pmAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter();
            
            phAdp.FillByYYMM(cDts.過去勤務票ヘッダ, sYYMM);
            pmAdp.FillByYYMM(cDts.過去勤務票明細, sYYMM);

            //phAdp.Fill(cDts.過去勤務票ヘッダ);
            //pmAdp.Fill(cDts.過去勤務票明細);

            int rCnt = 0;

            // オーナーフォームを無効にする
            frm.Enabled = false;

            // プログレスバーを表示する
            frmPrg frmP = new frmPrg();
            frmP.Owner = frm;
            frmP.Show();

            // レコード件数取得
            int cTotal = dts.勤務票ヘッダ.Rows.Count;

            // 出勤簿データ読み出し
            Boolean eCheck = true;

            for (int i = 0; i < cTotal; i++)
            {
                //データ件数加算
                rCnt++;

                //プログレスバー表示
                frmP.Text = "エラーチェック実行中　" + rCnt.ToString() + "/" + cTotal.ToString();
                frmP.progressValue = rCnt * 100 / cTotal;
                frmP.ProgressStep();

                //指定範囲ならエラーチェックを実施する：（i:行index）
                if (i >= sIx && i <= eIx)
                {
                    // 勤務票ヘッダ行のコレクションを取得します
                    JAFA_DATADataSet.勤務票ヘッダRow r = (JAFA_DATADataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Rows[i];

                    // エラーチェック実施
                    eCheck = errCheckData(dts, r, cDts);

                    if (!eCheck)　//エラーがあったとき
                    {
                        _errHeaderIndex = i;     // エラーとなったヘッダRowIndex
                        break;
                    }
                }
            }

            // いったんオーナーをアクティブにする
            frm.Activate();

            // 進行状況ダイアログを閉じる
            frmP.Close();

            // オーナーのフォームを有効に戻す
            frm.Enabled = true;

            return eCheck;
        }

        ///--------------------------------------------------------------------------------------------------
        /// <summary>
        ///     エラーチェックメイン処理。
        ///     エラーのときOCRDataクラスのヘッダ行インデックス、フィールド番号、明細行インデックス、
        ///     エラーメッセージが記録される </summary>
        /// <param name="sIx">
        ///     開始ヘッダ行インデックス</param>
        /// <param name="eIx">
        ///     終了ヘッダ行インデックス</param>
        /// <param name="frm">
        ///     親フォーム</param>
        /// <param name="dts">
        ///     データセット</param>
        /// <returns>
        ///     True:エラーなし、false:エラーあり</returns>
        ///-----------------------------------------------------------------------------------------------
        public Boolean errCheckMain(int sIx, int eIx, Form frm, JAFA_OCRDataSet dts)
        {
            // 有給休暇残日数チェック用 2015/07/02
            yAdp.Fill(dts.有給休暇付与マスター);
            kAdp.Fill(dts.勤怠データ);

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

            int rCnt = 0;

            // オーナーフォームを無効にする
            frm.Enabled = false;

            // プログレスバーを表示する
            frmPrg frmP = new frmPrg();
            frmP.Owner = frm;
            frmP.Show();

            // レコード件数取得
            int cTotal = dts.確定勤務票ヘッダ.Rows.Count;

            // 出勤簿データ読み出し
            Boolean eCheck = true;

            for (int i = 0; i < cTotal; i++)
            {
                //データ件数加算
                rCnt++;

                //プログレスバー表示
                frmP.Text = "エラーチェック実行中　" + rCnt.ToString() + "/" + cTotal.ToString();
                frmP.progressValue = rCnt * 100 / cTotal;
                frmP.ProgressStep();

                //指定範囲ならエラーチェックを実施する：（i:行index）
                if (i >= sIx && i <= eIx)
                {
                    // 確定ヘッダ行のコレクションを取得します
                    JAFA_OCRDataSet.確定勤務票ヘッダRow r = (JAFA_OCRDataSet.確定勤務票ヘッダRow)dts.確定勤務票ヘッダ.Rows[i];

                    // エラーチェック実施
                    eCheck = errCheckData(dts, r);

                    if (!eCheck)　//エラーがあったとき
                    {
                        _errHeaderIndex = i;     // エラーとなったヘッダRowIndex
                        break;
                    }
                }
            }

            // いったんオーナーをアクティブにする
            frm.Activate();

            // 進行状況ダイアログを閉じる
            frmP.Close();

            // オーナーのフォームを有効に戻す
            frm.Enabled = true;

            return eCheck;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        ///     エラー情報を取得します </summary>
        /// <param name="eID">
        ///     エラーデータのID</param>
        /// <param name="eNo">
        ///     エラー項目番号</param>
        /// <param name="eRow">
        ///     エラー明細行</param>
        /// <param name="eMsg">
        ///     表示メッセージ</param>
        ///---------------------------------------------------------------------------------
        private void setErrStatus(int eNo, int eRow, string eMsg)
        {
            //errHeaderIndex = eHRow;
            _errNumber = eNo;
            _errRow = eRow;
            _errMsg = eMsg;
        }

        ///-----------------------------------------------------------------------------------------------
        /// <summary>
        ///     項目別エラーチェック。
        ///     エラーのときヘッダ行インデックス、フィールド番号、明細行インデックス、エラーメッセージが記録される </summary>
        /// <param name="dts">
        ///     データセット</param>
        /// <param name="r">
        ///     勤務票ヘッダ行コレクション</param>
        /// <param name="cDts">
        ///     JAHR_OCRDataSetオブジェクト 過去勤務データ</param>
        /// <returns>
        ///     エラーなし：true, エラー有り：false</returns>
        ///-----------------------------------------------------------------------------------------------
        public Boolean errCheckData(JAFA_DATADataSet dts, JAFA_DATADataSet.勤務票ヘッダRow r, JAFA_OCRDataSet cDts)
        {
            // 対象年月
            if (!errCheckYearMonth(r)) return false;

            // 社員マスター
            if (!errCheckShain(r)) return false;

            // 同じ社員番号の勤務票データが複数存在しているか
            if (!getSameNumber(dts.勤務票ヘッダ, r.社員番号, r.ID))
            {
                setErrStatus(eShainNo, 0, "同じ社員番号のデータが複数あります");
                return false;
            }

            // 社員情報取得（入所年月日、退職年月日、週開始曜日）
            //DateTime dtIn = global.dtNon;
            //DateTime dtOut = global.dtNon;
            //int wStYoubi = global.flgOff;

            clsGetMst ms = new clsGetMst();
            JAFA_OCRDataSet.社員マスターRow mr = ms.getKojinMstRow(r.社員番号);
            if (mr != null)
            {
                //dtIn = mr.入所年月日;
                //dtOut = mr.退職年月日;
                //wStYoubi = mr.週開始曜日;

                // 対象年月と入所年月日
                int inYYMM = mr.入所年月日.Year * 100 + mr.入所年月日.Month;
                int ouYYMM = mr.退職年月日.Year * 100 + mr.退職年月日.Month;
                int tYYMM = r.年 * 100 + r.月;
                if (tYYMM < inYYMM)
                {
                    setErrStatus(eShainNo, 0, "入所日以前の出勤簿です");
                    return false;
                }

                // 退職日
                if (mr.退職年月日 != global.dtNon && tYYMM > ouYYMM)
                {
                    setErrStatus(eShainNo, 0, "退職者です");
                    return false;
                }
            }

            // -------------------------------------------------------------------------
            //
            //      日付別勤怠記入データ
            //
            // -------------------------------------------------------------------------
            
            int iX = 0;

            // 週日数
            cDays = 0;
            cDaysF = 0;

            // 週勤務した日
            wDays = 0;
            wDaysF = 0;

            // 休日出勤日
            hWorkDays = 0;
            hWorkDaysF = 0;

            //kaishiYoubi = mr.週開始曜日;   2018/10/22 コメント化
            kaishiYoubi = 0;    // 週開始曜日は日曜で統一：2018/10/22

            int fistDay = 0;

            bool fistFlg = true;

            // 月最初の週開始日がきたか
            bool sWeekStatus = false;
                        
            // 勤務票明細データ行を取得
            var mData = dts.勤務票明細.Where(a => a.ヘッダID == r.ID).OrderBy(a => a.日付);

            foreach (var m in mData)
            {
                if (fistFlg)
                {
                    fistDay = m.日付;
                    fistFlg = false;
                }

                // 週間の勤務実績を判定します
                if (!errCheck7Works(iX))
                {
                    return false;
                }

                // 行数
                iX++;

                // 曜日を取得
                DateTime sDt = DateTime.Parse(global.cnfYear.ToString() + "/" + global.cnfMonth.ToString() + "/" + m.日付.ToString());

                // 週開始曜日のとき
                //if (mr.週開始曜日 == int.Parse(sDt.DayOfWeek.ToString("d")))
                if (int.Parse(sDt.DayOfWeek.ToString("d")) == 0)    // 週開始は日曜日 2018/10/22
                {
                    // カウント日数を初期化
                    cDays = 0;
                    wDays = 0;
                    hWorkDays = 0;
                    sWeekStatus = true;
                }

                // 週日数カウント（月最初の週開始日以降）
                if (sWeekStatus)
                {
                    cDays++;
                }
                else
                {
                    cDaysF++;
                }

                // 無記入の行のとき
                if (Utility.NulltoStr(m.出勤区分) == string.Empty &&
                    Utility.NulltoStr(m.開始時) == string.Empty && Utility.NulltoStr(m.開始分) == string.Empty &&
                    Utility.NulltoStr(m.終了時) == string.Empty && Utility.NulltoStr(m.終了分) == string.Empty &&
                    Utility.NulltoStr(m.休憩開始時) == string.Empty && Utility.NulltoStr(m.休憩開始分) == string.Empty)
                {
                    // 週間の勤務実績を判定します
                    if (!errCheck7Works(iX))
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }

                // 明細記入チェック
                if (!errCheckRow(m, "出勤簿明細", iX)) return false;

                // 当月入所のとき入所日以前に勤怠が記入されていたらエラー
                if (mr.入所年月日.Year == global.cnfYear &&
                    mr.入所年月日.Month == global.cnfMonth)
                {
                    if (!errNDateBeforeWork(m, "", iX, mr.入所年月日)) return false;
                }

                // 当月退職のとき退職日翌日以降に勤怠が記入されていたらエラー
                if (mr.退職年月日.Year == global.cnfYear &&
                    mr.退職年月日.Month == global.cnfMonth)
                {
                    if (!errRDateAfterWork(m, "", iX, mr.退職年月日)) return false;
                }
                
                // 出勤区分
                if (!errCheckKinmuKigou(m, "出勤区分", iX)) return false;

                // 始業時刻・終業時刻チェック
                if (!errCheckTime(m, "出退時間", tanMin1, iX)) return false;

                // 休憩開始時刻・休憩終了時刻チェック
                if (!errCheckRestTime(m, "休憩時間", tanMin1, iX)) return false;

                // 実労働時間チェック : 2018/04/05 コメント解除
                if (!errCheckWorktime(m, "実労働時間", tanMin1, iX)) return false;

                // 出勤区分の先頭ゼロを消去する：2015/09/24
                string skbn = Utility.StrtoInt(m.出勤区分).ToString();
                if (skbn == "0") skbn = string.Empty;

                // 勤務日カウント（月最初の週開始日以降）
                if (sWeekStatus)
                {
                    if (skbn == SHUKIN_SHUKIN || skbn == SHUKIN_YUKYU ||
                        skbn == SHUKIN_HANKYU || skbn == SHUKIN_KYUSHU)
                    {
                        wDays++;
                    }

                    // 休日出勤日カウント
                    if (skbn == SHUKIN_KYUSHU)
                    {
                        hWorkDays++;
                    }
                }
                else
                {
                    // 月最初の週開始日以前
                    if (skbn == SHUKIN_SHUKIN || skbn == SHUKIN_YUKYU ||
                        skbn == SHUKIN_HANKYU || skbn == SHUKIN_KYUSHU)
                    {
                        wDaysF++;
                    }

                    // 休日出勤日カウント
                    if (skbn == SHUKIN_KYUSHU)
                    {
                        hWorkDaysF++;
                    }
                }
            }

            // 週間の勤務実績を判定します（最後の日が7日目のとき）
            if (!errCheck7Works(iX))
            {
                return false;
            }

            // 前月最終週と月初の週の勤務実績で休日出勤記入を確認する・前半のみ
            if (fistDay == 1)
            {
                // 月跨ぎがある月のみ実施：2017/11/14
                if (cDaysF > 0)
                {
                    lastWeekCheck(r.社員番号, cDts);
                    int iix = cDaysF;
                    cDaysF += cDaysF2;
                    wDaysF += wDaysF2;
                    hWorkDaysF += hWorkDaysF2;

                    if (cDaysF > 0)
                    {
                        if (!errCheck7WorksF(iix))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        ///--------------------------------------------------------------------------------
        /// <summary>
        ///     前月最終週と月初の週の勤務実績で休日出勤記入を確認する : 2017/11/02</summary>
        /// <param name="sCode">
        ///     社員番号</param>
        /// <param name="cDts">
        ///     JAHR_OCRDataSet : 2017/11/02</param>
        /// <returns></returns>
        ///--------------------------------------------------------------------------------
        private bool lastWeekCheck(int sCode, JAFA_OCRDataSet cDts)
        {
            // 2017/11/02 その都度Fillだと遅いので撤廃
            //JAHR_OCRDataSet dts = new JAHR_OCRDataSet();
            //JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter hAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
            //JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter mAdp = new JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter();
            //hAdp.Fill(dts.過去勤務票ヘッダ);
            //mAdp.Fill(dts.過去勤務票明細);

            int sYY = global.cnfYear;
            int sMM = global.cnfMonth - 1;
            if (sMM == 0)
            {
                sYY--;
                sMM = 12;
            }

            if (!cDts.過去勤務票ヘッダ.Any(a => a.年 == sYY && a.月 == sMM && a.社員番号 == sCode))
            {
                return true;
            }
            else
            {
                var s = cDts.過去勤務票ヘッダ.Single(a => a.年 == sYY && a.月 == sMM && a.社員番号 == sCode);

                foreach (var item in cDts.過去勤務票明細.Where(a => a.ヘッダID == s.ヘッダID).OrderBy(a =>a .日付))
                {                    
                    // 曜日を取得
                    DateTime sDt = DateTime.Parse(sYY.ToString() + "/" + sMM.ToString() + "/" + item.日付.ToString());

                    // 週開始曜日のとき
                    if (kaishiYoubi == int.Parse(sDt.DayOfWeek.ToString("d")))
                    {
                        // カウント日数を初期化
                        cDaysF2 = 0;
                        wDaysF2 = 0;
                        hWorkDaysF2 = 0;
                    }

                    cDaysF2++;
                    
                    // 出勤区分の先頭ゼロを消去：2015/09/24
                    string skbn = Utility.StrtoInt(item.出勤区分).ToString();
                    if (skbn == "0") skbn = string.Empty;

                    if (skbn == SHUKIN_SHUKIN || skbn == SHUKIN_YUKYU ||
                        skbn == SHUKIN_HANKYU || skbn == SHUKIN_KYUSHU)
                    {
                        wDaysF2++;
                    }

                    // 休日出勤日カウント
                    if (skbn == SHUKIN_KYUSHU)
                    {
                        hWorkDaysF2++;
                    }
                }
            }

            return true;
        }
        

        ///-----------------------------------------------------------------------------------------------
        /// <summary>
        ///     項目別エラーチェック。
        ///     エラーのときヘッダ行インデックス、フィールド番号、明細行インデックス、エラーメッセージが記録される </summary>
        /// <param name="dts">
        ///     データセット</param>
        /// <param name="r">
        ///     確定ヘッダ行コレクション</param>
        /// <returns>
        ///     エラーなし：true, エラー有り：false</returns>
        ///-----------------------------------------------------------------------------------------------
        /// 
        public Boolean errCheckData(JAFA_OCRDataSet dts, JAFA_OCRDataSet.確定勤務票ヘッダRow r)
        {
            // 対象年月
            if (!errCheckYearMonth(r)) return false;

            // 社員マスター
            if (!errCheckShain(r)) return false;

            // 社員情報取得（入所年月日、退職年月日、週開始曜日）
            //DateTime dtIn = global.dtNon;
            //DateTime dtOut = global.dtNon;
            //int wStYoubi = global.flgOff;

            clsGetMst ms = new clsGetMst();
            JAFA_OCRDataSet.社員マスターRow mr = ms.getKojinMstRow(r.社員番号);
            if (mr != null)
            {
                //dtIn = mr.入所年月日;
                //dtOut = mr.退職年月日;
                //wStYoubi = mr.週開始曜日;

                // 対象年月と入所年月日
                int inYYMM = mr.入所年月日.Year * 100 + mr.入所年月日.Month;
                int ouYYMM = mr.退職年月日.Year * 100 + mr.退職年月日.Month;
                int tYYMM = r.年 * 100 + r.月;
                if (tYYMM < inYYMM)
                {
                    setErrStatus(eShainNo, 0, "入所日以前の出勤簿です");
                    return false;
                }

                // 退職日
                if (mr.退職年月日 != global.dtNon && tYYMM > ouYYMM)
                {
                    setErrStatus(eShainNo, 0, "退職者です");
                    return false;
                }
            }

            //// 同じ社員番号の勤務票データが複数存在しているか
            //if (!getSameNumber(dts.勤務票ヘッダ, r.社員番号, r.ID))
            //{
            //    setErrStatus(eShainNo, 0, "同じ社員番号のデータが複数あります");
            //    return false;
            //}

            // -------------------------------------------------------------------------
            //
            //      日付別勤怠記入データ
            //
            // -------------------------------------------------------------------------


            int iX = 0;

            // 週日数
            cDays = 0;

            // 週勤務した日
            wDays = 0;

            // 休日出勤日
            hWorkDays = 0;

            // 有休・半休日数
            double yukyuNissu = 0;

            // 確定勤務票データ行を取得
            var mData = dts.確定勤務票明細.Where(a => a.ヘッダID == r.ヘッダID).OrderBy(a => a.日付);

            foreach (var m in mData)
            {
                // 週間の勤務実績を判定します
                if (!errCheck7Works(iX))
                {
                    return false;
                }

                // 行数
                iX++;
                
                // 曜日を取得
                DateTime sDt = DateTime.Parse(global.cnfYear.ToString() + "/" + global.cnfMonth.ToString() + "/" + m.日付.ToString());

                // 週開始曜日のとき
                //if (mr.週開始曜日 == int.Parse(sDt.DayOfWeek.ToString("d"))) 2018/10/22 コメント化
                if (int.Parse(sDt.DayOfWeek.ToString("d")) == 0)    // 週開始日は日曜日 2018/10/22
                {
                    // カウント日数を初期化
                    cDays = 0;
                    wDays = 0;
                    hWorkDays = 0;
                }

                // 週日数カウント
                cDays++;

                // 無記入の行のとき
                if (Utility.NulltoStr(m.出勤区分) == string.Empty &&
                    Utility.NulltoStr(m.開始時) == string.Empty && Utility.NulltoStr(m.開始分) == string.Empty &&
                    Utility.NulltoStr(m.終了時) == string.Empty && Utility.NulltoStr(m.終了分) == string.Empty &&
                    Utility.NulltoStr(m.休憩開始時) == string.Empty && Utility.NulltoStr(m.休憩開始分) == string.Empty)
                {
                    // 週間の勤務実績を判定します
                    if (!errCheck7Works(iX))
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }

                //// 明細記入チェック
                //if (!errCheckRow(m, "出勤簿明細", iX)) return false;

                // 当月入所のとき入所日以前に勤怠が記入されていたらエラー
                if (mr.入所年月日.Year == global.cnfYear && mr.入所年月日.Month == global.cnfMonth)
                {
                    if (!errNDateBeforeWork(m, "", iX, mr.入所年月日)) return false;
                }

                // 当月退職のとき退職日翌日以降に勤怠が記入されていたらエラー
                if (mr.退職年月日.Year == global.cnfYear && mr.退職年月日.Month == global.cnfMonth)
                {
                    if (!errRDateAfterWork(m, "", iX, mr.退職年月日)) return false;
                }
                
                // 出勤区分
                if (!errCheckKinmuKigou(m, "出勤区分", iX)) return false;

                // 始業時刻・終業時刻チェック
                if (!errCheckTime(m, "出退時間", tanMin1, iX)) return false;

                // 休憩開始時刻・休憩終了時刻チェック
                if (!errCheckRestTime(m, "休憩時間", tanMin1, iX)) return false;

                // 実労働時間チェック：2018/04/05 コメント解除
                if (!errCheckWorktime(m, "実労働時間", tanMin1, iX)) return false;

                // 出勤区分の先頭ゼロを消去する：2015/09/24
                string skbn = Utility.StrtoInt(m.出勤区分).ToString();
                if (skbn == "0") skbn = string.Empty;

                // 勤務日カウント
                if (skbn == SHUKIN_SHUKIN || skbn == SHUKIN_YUKYU ||
                    skbn == SHUKIN_HANKYU || skbn == SHUKIN_KYUSHU)
                {
                    wDays++;
                }

                // 休日出勤日カウント
                if (skbn == SHUKIN_KYUSHU)
                {
                    hWorkDays++;
                }

                // 有休・半休日数カウント
                if (skbn == SHUKIN_YUKYU)
                {
                    yukyuNissu++;
                }

                if (skbn == SHUKIN_HANKYU)
                {
                    yukyuNissu += 0.5; 
                }
            }

            // 有給残チェック
            if (!errCheckYukyuZan(dts, mr, yukyuNissu))
            {
                return false;
            }

            return true;
        }

        /// -------------------------------------------------------------------------------------
        /// <summary>
        ///     有給残日数チェック </summary>
        /// <param name="dts">
        ///     JAHR_OCRDataSet</param>
        /// <param name="mr">
        ///     JAHR_OCRDataSet.社員マスターRow</param>
        /// <param name="sYukyu">
        ///     当月有休日数</param>
        /// <returns>
        ///     残日数あり：true, 残日数超過：false </returns>
        /// -------------------------------------------------------------------------------------
        private bool errCheckYukyuZan(JAFA_OCRDataSet dts, JAFA_OCRDataSet.社員マスターRow mr, double sYukyu)
        {
            bool result = true;
            
            int pYYMM = global.cnfYear * 100 + global.cnfMonth; 
            int sNen = 0;
            int sTsuki = 0;
            
            // 当年初の有給残日数を求める
            double sZan = getNenshozan(dts, mstSheet, mr, pYYMM, out sNen, out sTsuki);

            // 消化日数期間を設定
            int sYYMM = sNen * 100 + sTsuki;　// 開始年月 201410
            
            int eYYMM = pYYMM - 1;  // 終了年月（当月の前月）201506
            if ((eYYMM % 100) == 0)
            {
                int y = (eYYMM / 100) - 1;
                eYYMM = y * 100 + 12;
            }

            // 前月までの消化日数を求める
            double sNissu = getShoukaNissu(dts, workSheet, mr, sYYMM, eYYMM);

            // 有休残日数
            if (sZan < (sNissu + sYukyu))
            {
                setErrStatus(eYukyuZan, 0, "有給残日数を超過しました。（残日数：" + (sZan - sNissu).ToString() + "　当月取得日数：" + sYukyu.ToString()  + ")" );
                result = false;
            }

            return result;
        }


        /// ----------------------------------------------------------------------
        /// <summary>
        ///     当年初の有給休暇残日数を取得する </summary>
        /// <param name="sCode">
        ///     職員コード</param>
        /// <param name="sYYMM">
        ///     年月</param>
        /// <returns>
        ///     当年初有給残日数</returns>
        /// ----------------------------------------------------------------------
        private double getNenshozan(JAFA_OCRDataSet dts, LinqToExcel.Query.ExcelQueryable<exlYukyuMst> mstSheet, JAFA_OCRDataSet.社員マスターRow mr, int sYYMM, out int sNen, out int sTsuki)
        {
            double zan = 0;
            bool sFms = false;

            sNen = 0;
            sTsuki = 0;

            int sCode = mr.職員コード;
            int sFyMonth = mr.有給付与月;

            JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter yAdp = new JAFA_OCRDataSetTableAdapters.有給休暇付与マスターTableAdapter();
            yAdp.Fill(dts.有給休暇付与マスター);

            foreach (var t in dts.有給休暇付与マスター
                .Where(a => a.社員番号 == sCode && (a.年 * 100 + a.月) <= sYYMM)
                .OrderByDescending(a => (a.年 * 100 + a.月)))
            {
                zan = t.当年初有給残日数;
                sNen = t.年;
                sTsuki = t.月;
                sFms = true;
                break;
            }

            // 有給休暇付与マスターが存在しなかったらExcelシートを読む
            if (!sFms)
            {
                // 有給休暇付与マスターExcelシートが存在するとき
                if (mstSheet != null)
                {
                    // 当年
                    int sYear = sYYMM / 100;    // 月
                    int sMonth = sYYMM % 100;   // 年

                    // 前回の有給付与年
                    if (sMonth < sFyMonth)
                    {
                        sYear--;
                    }

                    // 有給休暇付与Excelシートより前年初有給残日数（当年初有給残日数）を求めます
                    foreach (var x in mstSheet.Where(a => a.sCode == sCode.ToString() && a.sYY == sYear.ToString() && a.sMM == sFyMonth.ToString()))
                    {
                        zan = Utility.StrtoDouble(x.sNensho);
                        sNen = Utility.StrtoInt(x.sYY);
                        sTsuki = Utility.StrtoInt(x.sMM);
                        break;
                    }
                }
            }

            return zan;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     任意の社員の任意期間の有休・半休消化日数を求める </summary>
        /// <param name="sCode">
        ///     職員コード</param>
        /// <param name="sYYMM">
        ///     開始年月</param>
        /// <param name="eYYMM">
        ///     終了年月</param>
        /// <returns>
        ///     消化日数</returns>
        /// -----------------------------------------------------------------------------
        private double getShoukaNissu(JAFA_OCRDataSet dts, LinqToExcel.Query.ExcelQueryable<exlMntData> workSheet, JAFA_OCRDataSet.社員マスターRow mr, int sYYMM, int eYYMM)
        {
            double sNissu = 0;
            bool sFms = false;

            JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter kAdp = new JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter();
            kAdp.Fill(dts.勤怠データ);

            string sCode = global.ROK + mr.職員コード.ToString().PadLeft(5, '0');

            foreach (var t in dts.勤怠データ.Where(a => a.対象職員コード == sCode))
            {
                if (Utility.StrtoInt(t.対象月度) >= sYYMM && Utility.StrtoInt(t.対象月度) <= eYYMM)
                {
                    sNissu += (double)(t.有給休暇 + t.有給半日);
                    sFms = true;
                }
            }

            //// 勤怠データが存在しなかったらExcelシートを読む
            //if (!sFms)
            //{
            // Excel過去１年間有給取得ファイルが存在するとき
            if (workSheet != null)
            {
                // Excel過去１年間有給取得シートから日数を取得する
                foreach (var t in workSheet.Where(a => a.sCode == mr.職員コード.ToString()))
                {
                    if (Utility.StrtoInt(t.sYYMM) >= sYYMM && Utility.StrtoInt(t.sYYMM) <= eYYMM)
                    {
                        sNissu += Utility.StrtoDouble(t.sTotal);   // 有休＋半休
                    }
                }
            }
            //}

            return sNissu;
        }
        
        ///-----------------------------------------------------------------------------
        /// <summary>
        ///     週間の勤務実績を判定します＜勤務日数と休日勤務記入＞ </summary>
        /// <param name="iX">
        ///     行インデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///-----------------------------------------------------------------------------
        private bool errCheck7Works(int iX)
        {
            // 
            if (cDays == 7)
            {
                if (wDays == 7 && hWorkDays == 0)
                {
                    // 週７日勤務があり休日出勤が記入されていない
                    setErrStatus(eWeek, iX - 1, "該当週は７日勤務があり休日出勤が記入されていません");
                    return false;
                }

                if (wDays != 7 && hWorkDays != 0)
                {
                    // 週７日未満勤務で休日出勤が記入されている
                    setErrStatus(eWeek, iX - 1, "該当週は７日勤務がありませんが休日出勤が記入されています");
                    return false;
                }
            }

            return true;
        }

        ///-----------------------------------------------------------------------------
        /// <summary>
        ///     月跨ぎの月初の週の勤務実績を判定します＜勤務日数と休日勤務記入＞ </summary>
        /// <param name="iX">
        ///     行インデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///-----------------------------------------------------------------------------
        private bool errCheck7WorksF(int iX)
        {
            // 
            if (cDaysF == 7)
            {
                if (wDaysF == 7 && hWorkDaysF == 0)
                {
                    // 週７日勤務があり休日出勤が記入されていない
                    setErrStatus(eWeek, iX - 1, "該当週は７日勤務があり休日出勤が記入されていません");
                    return false;
                }

                if (wDaysF != 7 && hWorkDaysF != 0)
                {
                    // 週７日未満勤務で休日出勤が記入されている
                    setErrStatus(eWeek, iX - 1, "該当週は７日勤務がありませんが休日出勤が記入されています");
                    return false;
                }
            }

            return true;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        ///     年月チェック </summary>
        /// <param name="r">
        ///     JAFA_DATADataSet.勤務票ヘッダRow</param>
        /// <returns>
        ///     true:エラーなし、false:エラーあり</returns>
        ///--------------------------------------------------------------------
        private bool errCheckYearMonth(JAFA_DATADataSet.勤務票ヘッダRow r)
        {
            // 対象年
            if (Utility.NumericCheck(r.年.ToString()) == false)
            {
                setErrStatus(eYearMonth, 0, "年が正しくありません");
                return false;
            }

            if (r.年 < 1)
            {
                setErrStatus(eYearMonth, 0, "年が正しくありません");
                return false;
            }

            if (r.年 != global.cnfYear)
            {
                setErrStatus(eYearMonth, 0, "対象年（" + global.cnfYear + "年）と一致していません");
                return false;
            }

            // 対象月
            if (!Utility.NumericCheck(r.月.ToString()))
            {
                setErrStatus(eMonth, 0, "月が正しくありません");
                return false;
            }

            if (int.Parse(r.月.ToString()) < 1 || int.Parse(r.月.ToString()) > 12)
            {
                setErrStatus(eMonth, 0, "月が正しくありません");
                return false;
            }

            if (int.Parse(r.月.ToString()) != global.cnfMonth)
            {
                setErrStatus(eMonth, 0, "対象月（" + global.cnfMonth + "月）と一致していません");
                return false;
            }

            return true;
        }
        
        ///--------------------------------------------------------------------
        /// <summary>
        ///     年月チェック </summary>
        /// <param name="r">
        ///     JAHR_OCRDataSet.確定勤務票ヘッダRow</param>
        /// <returns>
        ///     true:エラーなし、false:エラーあり</returns>
        ///--------------------------------------------------------------------
        private bool errCheckYearMonth(JAFA_OCRDataSet.確定勤務票ヘッダRow r)
        {
            // 対象年
            if (Utility.NumericCheck(r.年.ToString()) == false)
            {
                setErrStatus(eYearMonth, 0, "年が正しくありません");
                return false;
            }

            if (r.年 < 1)
            {
                setErrStatus(eYearMonth, 0, "年が正しくありません");
                return false;
            }

            if (r.年 != global.cnfYear)
            {
                setErrStatus(eYearMonth, 0, "対象年（" + global.cnfYear + "年）と一致していません");
                return false;
            }

            // 対象月
            if (!Utility.NumericCheck(r.月.ToString()))
            {
                setErrStatus(eMonth, 0, "月が正しくありません");
                return false;
            }

            if (int.Parse(r.月.ToString()) < 1 || int.Parse(r.月.ToString()) > 12)
            {
                setErrStatus(eMonth, 0, "月が正しくありません");
                return false;
            }

            if (int.Parse(r.月.ToString()) != global.cnfMonth)
            {
                setErrStatus(eMonth, 0, "対象月（" + global.cnfMonth + "月）と一致していません");
                return false;
            }

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     社員マスターチェック </summary>
        /// <param name="obj">
        ///     勤務票ヘッダRowコレクション</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckShain(JAFA_DATADataSet.勤務票ヘッダRow r)
        {
            // 数字以外のとき
            if (!Utility.NumericCheck(Utility.NulltoStr(r.社員番号)))
            {
                setErrStatus(eShainNo, 0, "社員番号が入力されていません");
                return false;
            }

            // 社員番号マスター検証
            clsGetMst ms = new clsGetMst();
            JAFA_OCRDataSet.社員マスターRow mr = ms.getKojinMstRow(r.社員番号);
            if (mr == null)
            {
                setErrStatus(eShainNo, 0, "マスター未登録の社員番号です");
                return false;
            }

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     社員マスターチェック </summary>
        /// <param name="r">
        ///     JAHR_OCRDataSet.確定勤務票ヘッダRow</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckShain(JAFA_OCRDataSet.確定勤務票ヘッダRow r)
        {
            // 数字以外のとき
            if (!Utility.NumericCheck(Utility.NulltoStr(r.社員番号)))
            {
                setErrStatus(eShainNo, 0, "社員番号が入力されていません");
                return false;
            }

            // 社員番号マスター検証
            clsGetMst ms = new clsGetMst();
            JAFA_OCRDataSet.社員マスターRow mr = ms.getKojinMstRow(r.社員番号);
            if (mr == null)
            {
                setErrStatus(eShainNo, 0, "マスター未登録の社員番号です");
                return false;
            }

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     明細記入チェック </summary>
        /// <param name="obj">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="iX">
        ///     行を表すインデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckRow(JAFA_DATADataSet.勤務票明細Row m, string tittle, int iX)
        {
            //// 社員番号以外に記入項目なしのときエラーとする
            //if (m.出勤区分 == string.Empty && 
            //    m.時間外時 == string.Empty && m.時間外分 == string.Empty && 
            //    m.深夜時 == string.Empty && m.深夜分 == string.Empty && 
            //    m.開始時 == string.Empty && m.開始分 == string.Empty && 
            //    m.終了時 == string.Empty && m.終了分 == string.Empty)

            //{
            //    setErrStatus(eSH, iX - 1, tittle + "が未入力です");
            //    return false;
            //}

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     時間記入チェック </summary>
        /// <param name="obj">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="stKbn">
        ///     勤怠記号の出勤怠区分</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckTime(JAFA_DATADataSet.勤務票明細Row m, string tittle, int Tani, int iX)
        {
            // 開始時間と終了時間
            string sTimeW = m.開始時.Trim() + m.開始分.Trim();
            string eTimeW = m.終了時.Trim() + m.終了分.Trim();

            if (sTimeW != string.Empty && eTimeW == string.Empty)
            {
                setErrStatus(eEH, iX - 1, tittle + "：終業時刻が未入力です");
                return false;
            }

            if (sTimeW == string.Empty && eTimeW != string.Empty)
            {
                setErrStatus(eSH, iX - 1, tittle + "：始業時刻が未入力です");
                return false;
            }

            // 記入のとき
            if (m.開始時 != string.Empty || m.開始分 != string.Empty ||
                m.終了時 != string.Empty || m.終了分 != string.Empty)
            {
                // 数字範囲、単位チェック
                if (!checkHourSpan(m.開始時))
                {
                    setErrStatus(eSH, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkMinSpan(m.開始分, Tani))
                {
                    setErrStatus(eSM, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkEndHourSpan(m.終了時))
                {
                    setErrStatus(eEH, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkMinSpan(m.終了分, Tani))
                {
                    setErrStatus(eEM, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                // 開始時刻 > 終了時刻
                if (int.Parse(sTimeW) > int.Parse(eTimeW))
                {
                    setErrStatus(eSH, iX - 1, tittle + "：開始時刻が終了時刻以降です");
                    return false;
                }

                //// 終了時刻範囲
                //if (Utility.StrtoInt(Utility.NulltoStr(m.終了時)) == 24 &&
                //    Utility.StrtoInt(Utility.NulltoStr(m.終了分)) > 0)
                //{
                //    setErrStatus(eEM, iX - 1, tittle + "終了時刻範囲を超えています（～２４：００）");
                //    return false;
                //}
            }

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     時間記入チェック </summary>
        /// <param name="obj">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="stKbn">
        ///     勤怠記号の出勤怠区分</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckTime(JAFA_OCRDataSet.確定勤務票明細Row m, string tittle, int Tani, int iX)
        {
            // 開始時間と終了時間
            string sTimeW = m.開始時.Trim() + m.開始分.Trim();
            string eTimeW = m.終了時.Trim() + m.終了分.Trim();

            if (sTimeW != string.Empty && eTimeW == string.Empty)
            {
                setErrStatus(eEH, iX - 1, tittle + "：終業時刻が未入力です");
                return false;
            }

            if (sTimeW == string.Empty && eTimeW != string.Empty)
            {
                setErrStatus(eSH, iX - 1, tittle + "：始業時刻が未入力です");
                return false;
            }

            // 記入のとき
            if (m.開始時 != string.Empty || m.開始分 != string.Empty ||
                m.終了時 != string.Empty || m.終了分 != string.Empty)
            {
                // 数字範囲、単位チェック
                if (!checkHourSpan(m.開始時))
                {
                    setErrStatus(eSH, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkMinSpan(m.開始分, Tani))
                {
                    setErrStatus(eSM, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkEndHourSpan(m.終了時))
                {
                    setErrStatus(eEH, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkMinSpan(m.終了分, Tani))
                {
                    setErrStatus(eEM, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                // 開始時刻 > 終了時刻
                if (int.Parse(sTimeW) > int.Parse(eTimeW))
                {
                    setErrStatus(eSH, iX - 1, tittle + "：開始時刻が終了時刻以降です");
                    return false;
                }

                //// 終了時刻範囲
                //if (Utility.StrtoInt(Utility.NulltoStr(m.終了時)) == 24 &&
                //    Utility.StrtoInt(Utility.NulltoStr(m.終了分)) > 0)
                //{
                //    setErrStatus(eEM, iX - 1, tittle + "終了時刻範囲を超えています（～２４：００）");
                //    return false;
                //}
            }

            return true;
        }


        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     休憩時間記入チェック </summary>
        /// <param name="obj">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="stKbn">
        ///     勤怠記号の出勤怠区分</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckRestTime(JAFA_DATADataSet.勤務票明細Row m, string tittle, int Tani, int iX)
        {
            //// 開始時間と終了時間
            //string sTimeW = m.休憩開始時.Trim() + m.休憩開始分.Trim();
            //string eTimeW = m.休憩終了時.Trim() + m.休憩終了分.Trim();

            //if (sTimeW != string.Empty && eTimeW == string.Empty)
            //{
            //    setErrStatus(eKEH, iX - 1, tittle + "：休憩終了時刻が未入力です");
            //    return false;
            //}

            //if (sTimeW == string.Empty && eTimeW != string.Empty)
            //{
            //    setErrStatus(eKSH, iX - 1, tittle + "：休憩開始時刻が未入力です");
            //    return false;
            //}

            // 始業就業時刻が無記入のとき
            if (m.開始時 == string.Empty && m.開始分 == string.Empty &&
                m.終了時 == string.Empty && m.終了分 == string.Empty)
            {
                if (m.休憩開始時 != string.Empty)
                {
                    setErrStatus(eKSH, iX - 1, tittle + "：始業時刻・終業時刻が未入力で休憩時刻が入力されています");
                    return false;
                }

                if (m.休憩開始分 != string.Empty)
                {
                    setErrStatus(eKSM, iX - 1, tittle + "：始業時刻・終業時刻が未入力で休憩時刻が入力されています");
                    return false;
                }
            }
            
            // 記入のとき
            if (m.休憩開始時 != string.Empty || m.休憩開始分 != string.Empty)
            {
                if (m.休憩開始時 != string.Empty)
                {
                    // 数字範囲、単位チェック
                    if (!checkHourSpan(m.休憩開始時))
                    {
                        setErrStatus(eKSH, iX - 1, tittle + "が正しくありません");
                        return false;
                    }
                }

                if (!checkMinSpan(m.休憩開始分, Tani))
                {
                    setErrStatus(eKSM, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                // 出勤時刻、退勤時刻、休憩時間から実働時間を取得する
                OCRData ocr = new OCRData();
                double wTime = ocr.getWorkTime(m.開始時, m.開始分, m.終了時, m.終了分, m.休憩開始時, m.休憩開始分);

                if (wTime == 0)
                {
                    setErrStatus(eKSH, iX - 1, tittle + "が正しくありません");
                    return false;
                }
            }
            
            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     休憩時間記入チェック </summary>
        /// <param name="obj">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="stKbn">
        ///     勤怠記号の出勤怠区分</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckRestTime(JAFA_OCRDataSet.確定勤務票明細Row m, string tittle, int Tani, int iX)
        {
            //// 開始時間と終了時間
            //string sTimeW = m.休憩開始時.Trim() + m.休憩開始分.Trim();
            //string eTimeW = m.休憩終了時.Trim() + m.休憩終了分.Trim();

            //if (sTimeW != string.Empty && eTimeW == string.Empty)
            //{
            //    setErrStatus(eKEH, iX - 1, tittle + "：休憩終了時刻が未入力です");
            //    return false;
            //}

            //if (sTimeW == string.Empty && eTimeW != string.Empty)
            //{
            //    setErrStatus(eKSH, iX - 1, tittle + "：休憩開始時刻が未入力です");
            //    return false;
            //}

            // 始業就業時刻が無記入のとき
            if (m.開始時 == string.Empty && m.開始分 == string.Empty &&
                m.終了時 == string.Empty && m.終了分 == string.Empty)
            {
                if (m.休憩開始時 != string.Empty)
                {
                    setErrStatus(eKSH, iX - 1, tittle + "：始業時刻・終業時刻が未入力で休憩時刻が入力されています");
                    return false;
                }

                if (m.休憩開始分 != string.Empty)
                {
                    setErrStatus(eKSM, iX - 1, tittle + "：始業時刻・終業時刻が未入力で休憩時刻が入力されています");
                    return false;
                }
            }

            // 記入のとき
            if (m.休憩開始時 != string.Empty || m.休憩開始分 != string.Empty)
            {
                if (m.休憩開始時 != string.Empty)
                {
                    // 数字範囲、単位チェック
                    if (!checkHourSpan(m.休憩開始時))
                    {
                        setErrStatus(eKSH, iX - 1, tittle + "が正しくありません");
                        return false;
                    }
                }

                if (!checkMinSpan(m.休憩開始分, Tani))
                {
                    setErrStatus(eKSM, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                // 出勤時刻、退勤時刻、休憩時間から実働時間を取得する
                OCRData ocr = new OCRData();
                double wTime = ocr.getWorkTime(m.開始時, m.開始分, m.終了時, m.終了分, m.休憩開始時, m.休憩開始分);

                if (wTime == 0)
                {
                    setErrStatus(eKSH, iX - 1, tittle + "が正しくありません");
                    return false;
                }
            }

            return true;
        }

        ///--------------------------------------------------------------------------------
        /// <summary>
        ///     実労働時間チェック </summary>
        /// <param name="m">
        ///     JAFA_DATADataSet.勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///--------------------------------------------------------------------------------
        private bool errCheckWorktime(JAFA_DATADataSet.勤務票明細Row m, string tittle, int Tani, int iX)
        {
            double w = getWorkTime(m.開始時, m.開始分, m.終了時, m.終了分, m.休憩開始時, m.休憩開始分);
            int ws = m.実働時 * 60 + m.実働分;

            if (w != ws)
            {
                int wh = (int)(w / 60);
                int wm = (int)(w % 60);

                setErrStatus(eWH, iX - 1, tittle + "が正しくありません。(" + wh.ToString() + ":" + wm.ToString().PadLeft(2, '0') + ")");
                return false;
            }

            return true;
        }

        ///--------------------------------------------------------------------------------
        /// <summary>
        ///     実労働時間チェック </summary>
        /// <param name="m">
        ///     JAHR_OCRDataSet.確定勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///--------------------------------------------------------------------------------
        private bool errCheckWorktime(JAFA_OCRDataSet.確定勤務票明細Row m, string tittle, int Tani, int iX)
        {
            double w = getWorkTime(m.開始時, m.開始分, m.終了時, m.終了分, m.休憩開始時, m.休憩開始分);
            int ws = m.実働時 * 60 + m.実働分;

            if (w != ws)
            {
                int wh = (int)(w / 60);
                int wm = (int)(w % 60);

                setErrStatus(eWH, iX - 1, tittle + "が正しくありません。(" + wh.ToString() + ":" + wm.ToString().PadLeft(2, '0') + ")");
                return false;
            }

            return true;
        }

        ///--------------------------------------------------------------------------------
        /// <summary>
        ///     出勤区分チェック </summary>
        /// <param name="m">
        ///     JAFA_DATADataSet.勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///--------------------------------------------------------------------------------
        private bool errCheckKinmuKigou(JAFA_DATADataSet.勤務票明細Row m, string tittle, int iX)
        {
            // 出勤区分記入なし
            if (m.出勤区分 == string.Empty)
            {
                if (m.開始時 != string.Empty || m.開始分 != string.Empty || m.終了時 != string.Empty ||
                   m.終了分 != string.Empty || m.休憩開始時 != string.Empty || m.休憩開始分 != string.Empty)
                {
                    setErrStatus(eKintaiKigou, iX - 1, "出勤区分が未入力です");
                    return false;
                }
            }
            
            // 出勤区分記入有り
            if (m.出勤区分 != string.Empty)
            {
                // 数字以外のとき
                if (!Utility.NumericCheck(Utility.NulltoStr(m.出勤区分)))
                {
                    setErrStatus(eKintaiKigou, iX - 1, "出勤区分が正しくありません");
                    return false;
                }

                // 登録済み勤務区分検証
                clsGetMst ms = new clsGetMst();
                string[] kinmu = new string[2];
                kinmu = ms.getKinmuMst(m.出勤区分);

                if (kinmu[0] == global.NOT_FOUND)
                {
                    setErrStatus(eKintaiKigou, iX - 1, "未登録の出勤区分です");
                    return false;
                }
                
                //  出勤区分の実働区分が「0」で時刻が記入されているときNGとする
                if (kinmu[1] == global.FLGOFF)
                {
                    if (m.開始時 != string.Empty)
                    {
                        setErrStatus(eSH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で開始時刻が入力されています");
                        return false;
                    }

                    if (m.開始分 != string.Empty)
                    {
                        setErrStatus(eSM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で開始時刻が入力されています");
                        return false;
                    }

                    if (m.終了時 != string.Empty)
                    {
                        setErrStatus(eEH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で終了時刻が入力されています");
                        return false;
                    }

                    if (m.終了分 != string.Empty)
                    {
                        setErrStatus(eEM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で終了時刻が入力されています");
                        return false;
                    }

                    if (m.休憩開始時 != string.Empty)
                    {
                        setErrStatus(eKSH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で休憩時間が入力されています");
                        return false;
                    }

                    if (m.休憩開始分 != string.Empty)
                    {
                        setErrStatus(eKSM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で休憩時間が入力されています");
                        return false;
                    }
                }

                //  出勤区分の実働区分が「1」で時刻が記入されていないときNGとする
                if (kinmu[1] == global.FLGON)
                {
                    if (m.開始時 == string.Empty)
                    {
                        setErrStatus(eSH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で開始時刻が入力されていません");
                        return false;
                    }

                    if (m.開始分 == string.Empty)
                    {
                        setErrStatus(eSM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で開始時刻が入力されていません");
                        return false;
                    }

                    if (m.終了時 == string.Empty)
                    {
                        setErrStatus(eEH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で終了時刻が入力されていません");
                        return false;
                    }

                    if (m.終了分 == string.Empty)
                    {
                        setErrStatus(eEM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で終了時刻が入力されていません");
                        return false;
                    }
                }
            }
            
            return true;
        }

        ///--------------------------------------------------------------------------------
        /// <summary>
        ///     出勤区分チェック </summary>
        /// <param name="m">
        ///     JAHR_OCRDataSet.確定勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///--------------------------------------------------------------------------------
        private bool errCheckKinmuKigou(JAFA_OCRDataSet.確定勤務票明細Row m, string tittle, int iX)
        {
            // 出勤区分記入なし
            if (m.出勤区分 == string.Empty)
            {
                if (m.開始時 != string.Empty || m.開始分 != string.Empty || m.終了時 != string.Empty ||
                   m.終了分 != string.Empty || m.休憩開始時 != string.Empty || m.休憩開始分 != string.Empty)
                {
                    setErrStatus(eKintaiKigou, iX - 1, "出勤区分が未入力です");
                    return false;
                }
            }

            // 出勤区分記入有り
            if (m.出勤区分 != string.Empty)
            {
                // 数字以外のとき
                if (!Utility.NumericCheck(Utility.NulltoStr(m.出勤区分)))
                {
                    setErrStatus(eKintaiKigou, iX - 1, "出勤区分が正しくありません");
                    return false;
                }

                // 登録済み勤務区分検証
                clsGetMst ms = new clsGetMst();
                string[] kinmu = new string[2];
                kinmu = ms.getKinmuMst(m.出勤区分);

                if (kinmu[0] == global.NOT_FOUND)
                {
                    setErrStatus(eKintaiKigou, iX - 1, "未登録の出勤区分です");
                    return false;
                }

                //  出勤区分の実働区分が「0」で時刻が記入されているときNGとする
                if (kinmu[1] == global.FLGOFF)
                {
                    if (m.開始時 != string.Empty)
                    {
                        setErrStatus(eSH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で開始時刻が入力されています");
                        return false;
                    }

                    if (m.開始分 != string.Empty)
                    {
                        setErrStatus(eSM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で開始時刻が入力されています");
                        return false;
                    }

                    if (m.終了時 != string.Empty)
                    {
                        setErrStatus(eEH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で終了時刻が入力されています");
                        return false;
                    }

                    if (m.終了分 != string.Empty)
                    {
                        setErrStatus(eEM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で終了時刻が入力されています");
                        return false;
                    }

                    if (m.休憩開始時 != string.Empty)
                    {
                        setErrStatus(eKSH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で休憩時間が入力されています");
                        return false;
                    }

                    if (m.休憩開始分 != string.Empty)
                    {
                        setErrStatus(eKSM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で休憩時間が入力されています");
                        return false;
                    }
                }

                //  出勤区分の実働区分が「1」で時刻が記入されていないときNGとする
                if (kinmu[1] == global.FLGON)
                {
                    if (m.開始時 == string.Empty)
                    {
                        setErrStatus(eSH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で開始時刻が入力されていません");
                        return false;
                    }

                    if (m.開始分 == string.Empty)
                    {
                        setErrStatus(eSM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で開始時刻が入力されていません");
                        return false;
                    }

                    if (m.終了時 == string.Empty)
                    {
                        setErrStatus(eEH, iX - 1, "出勤区分が「" + m.出勤区分 + "」で終了時刻が入力されていません");
                        return false;
                    }

                    if (m.終了分 == string.Empty)
                    {
                        setErrStatus(eEM, iX - 1, "出勤区分が「" + m.出勤区分 + "」で終了時刻が入力されていません");
                        return false;
                    }
                }
            }

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     時間記入範囲チェック 0～23の数値 </summary>
        /// <param name="h">
        ///     記入値</param>
        /// <returns>
        ///     正常:true, エラー:false</returns>
        ///------------------------------------------------------------------------------------
        private bool checkHourSpan(string h)
        {
            if (!Utility.NumericCheck(h)) return false;
            else if (int.Parse(h) < 0 || int.Parse(h) > 23) return false;
            else return true;
        }


        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     終了時間記入範囲チェック 0～33の数値 </summary>
        /// <param name="h">
        ///     記入値</param>
        /// <returns>
        ///     正常:true, エラー:false</returns>
        ///------------------------------------------------------------------------------------
        private bool checkEndHourSpan(string h)
        {
            if (!Utility.NumericCheck(h)) return false;
            else if (int.Parse(h) < 0 || int.Parse(h) > 33) return false;
            else return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     分記入範囲チェック：0～59の数値及び記入単位 </summary>
        /// <param name="h">
        ///     記入値</param>
        /// <param name="tani">
        ///     記入単位分</param>
        /// <returns>
        ///     正常:true, エラー:false</returns>
        ///------------------------------------------------------------------------------------
        private bool checkMinSpan(string m, int tani)
        {
            if (!Utility.NumericCheck(m)) return false;
            else if (int.Parse(m) < 0 || int.Parse(m) > 59) return false;
            else if (int.Parse(m) % tani != 0) return false;
            else return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     実働時間を取得する</summary>
        /// <param name="sH">
        ///     開始時</param>
        /// <param name="sM">
        ///     開始分</param>
        /// <param name="eH">
        ///     終了時</param>
        /// <param name="eM">
        ///     終了分</param>
        /// <param name="kSH">
        ///     休憩開始時</param>
        /// <param name="kSM">
        ///     休憩開始分</param>
        ///------------------------------------------------------------------------------------
        public double getWorkTime(string sH, string sM, string eH, string eM, string kSH, string kSM)
        {
            DateTime sTm;
            DateTime eTm;
            DateTime cTm;
            double w = 0;   // 稼働時間

            /*
             * 終業時刻－始業時刻
             */

            // 時刻情報に不備がある場合は０を返す
            if (!Utility.NumericCheck(sH) || !Utility.NumericCheck(sM) || 
                !Utility.NumericCheck(eH) || !Utility.NumericCheck(eM))
                return 0;

            // 開始時刻取得
            if (DateTime.TryParse(Utility.StrtoInt(sH).ToString() + ":" + Utility.StrtoInt(sM).ToString(), out cTm))
            {
                sTm = cTm;

                /*
                 * 終了時刻取得
                 * 24時以降記述のとき、23時以降分は最後に加算する
                 */
                int cEH = Utility.StrtoInt(eH);
                int saEH;
                if (Utility.StrtoInt(eH) > 23)
                {
                    cEH = 23;
                    saEH = Utility.StrtoInt(eH) - 23;
                }
                else
                {
                    cEH = Utility.StrtoInt(eH);
                    saEH = 0;
                }

                if (DateTime.TryParse(cEH.ToString() + ":" + Utility.StrtoInt(eM).ToString(), out cTm))
                {
                    eTm = cTm;

                    // 稼働時間（23時以降記述分を加算する）
                    w = Utility.GetTimeSpan(sTm, eTm).TotalMinutes + (saEH * 60);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

            /*
             * 休憩時間
             */

            double r = Utility.StrtoDouble(Utility.NulltoStr(kSH)) * 60 + 
                       Utility.StrtoDouble(Utility.NulltoStr(kSM));
                        
            // 休憩時間を差し引く
            if (w >= r) w = w - r;
            else w = 0;

            // 値を返す
            return w;
        }

        ///--------------------------------------------------------------
        /// <summary>
        ///     深夜勤務時間を取得する（JA版）</summary>
        /// <param name="m">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="sH">
        ///     開始時</param>
        /// <param name="sM">
        ///     開始分</param>
        /// <param name="eH">
        ///     終了時</param>
        /// <param name="eM">
        ///     終了分</param>
        /// <returns>
        ///     深夜勤務時間</returns>
        /// ------------------------------------------------------------
        public double getShinyaWorkTime_JA(string sH, string sM, string eH, string eM)
        {
            DateTime sTime;
            DateTime eTime;
            DateTime cTm;

            double overTime = 0;  

            double wkShinya = 0;    // 深夜稼働時間

            // 時刻情報に不備がある場合は０を返す
            if (!Utility.NumericCheck(sH) || !Utility.NumericCheck(sM) ||
                !Utility.NumericCheck(eH) || !Utility.NumericCheck(eM))
                return 0;

            // 開始時間を取得
            if (DateTime.TryParse(Utility.StrtoInt(sH).ToString() + ":" + Utility.StrtoInt(sM).ToString(), out cTm))
            {
                sTime = cTm;
            }
            else return 0;

            // 終了時間を取得
            if (Utility.StrtoInt(eH) > 23)
            {
                    // 23時までとそれ以降に分ける
                if (DateTime.TryParse("23:00", out cTm))
                {
                    eTime = cTm;

                    //if (Utility.StrtoInt(eH) > 29)
                    //{
                    //    // 翌日分加算：23時から29時まで
                    //    overTime = 360;
                    //}
                    //else
                    //{
                    //    // 翌日分加算：23時から終了時間（24~29)まで
                    //    overTime = (Utility.StrtoInt(eH) - 23) * 60;
                    //}

                    // 23時以降分加算
                    overTime = (Utility.StrtoInt(eH) - 23) * 60 + Utility.StrtoInt(eM);

                    // ただし29時まで
                    if (Utility.StrtoInt(eH) > 29)
                    {
                        overTime = overTime - ((Utility.StrtoInt(eH) - 29) * 60) - Utility.StrtoInt(eM);
                    }

                    if (Utility.StrtoInt(eH) == 29)
                    {
                        // 翌日分加算：29時まで
                        overTime = overTime - Utility.StrtoInt(eM);
                    }
                }
                else return 0;
            }
            else if (DateTime.TryParse(Utility.StrtoInt(eH).ToString() + ":" + Utility.StrtoInt(eM).ToString(), out cTm))
            {
                eTime = cTm;
            }
            else return 0;

            // 当日内の勤務のと（ＪＡは24時から33時まで記入可能のため、必ず開始時刻より終了時刻が後になる）
            //if (sTime.TimeOfDay < eTime.TimeOfDay)
            //{

            // 早出残業時間を求める
            if (sTime < global.dt0500)  // 開始時刻が午前5時前のとき
            {
                // 早朝時間帯稼働時間
                if (eTime >= global.dt0500)
                {
                    wkShinya += Utility.GetTimeSpan(sTime, global.dt0500).TotalMinutes;
                }
                else
                {
                    wkShinya += Utility.GetTimeSpan(sTime, eTime).TotalMinutes;
                }
            }

            // 終了時刻が22:00以降のとき
            if (eTime >= global.dt2200)
            {
                // 当日分の深夜帯稼働時間を求める
                if (sTime <= global.dt2200)
                {
                    // 出勤時刻が22:00以前のとき深夜開始時刻は22:00とする
                    wkShinya += Utility.GetTimeSpan(global.dt2200, eTime).TotalMinutes;
                }
                else
                {
                    // 出勤時刻が22:00以降のとき深夜開始時刻は出勤時刻とする
                    wkShinya += Utility.GetTimeSpan(sTime, eTime).TotalMinutes;
                }

                // 24時以降の記入分を加算する
                wkShinya += overTime;
            }

            //}

            return wkShinya;
        }

        ///--------------------------------------------------------------
        /// <summary>
        ///     深夜勤務時間を取得する</summary>
        /// <param name="m">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="sH">
        ///     開始時</param>
        /// <param name="sM">
        ///     開始分</param>
        /// <param name="eH">
        ///     終了時</param>
        /// <param name="eM">
        ///     終了分</param>
        /// <returns>
        ///     深夜勤務時間</returns>
        /// ------------------------------------------------------------
        private double getShinyaWorkTime(string sH, string sM, string eH, string eM)
        {
            DateTime sTime;
            DateTime eTime;
            DateTime cTm;

            double wkShinya = 0;    // 深夜稼働時間

            // 時刻情報に不備がある場合は０を返す
            if (!Utility.NumericCheck(sH) || !Utility.NumericCheck(sM) ||
                !Utility.NumericCheck(eH) || !Utility.NumericCheck(eM))
                return 0;

            // 開始時間を取得
            if (DateTime.TryParse(Utility.StrtoInt(sH).ToString() + ":" + Utility.StrtoInt(sM).ToString(), out cTm))
            {
                sTime = cTm;
            }
            else return 0;

            // 終了時間を取得
            if (Utility.StrtoInt(eH) == 24 && Utility.StrtoInt(eM) == 0)
            {
                eTime = global.dt2359;
            }
            else if (DateTime.TryParse(Utility.StrtoInt(eH).ToString() + ":" + Utility.StrtoInt(eM).ToString(), out cTm))
            {
                eTime = cTm;
            }
            else return 0;


            // 当日内の勤務のとき
            if (sTime.TimeOfDay < eTime.TimeOfDay)
            {
                // 早出残業時間を求める
                if (sTime < global.dt0500)  // 開始時刻が午前5時前のとき
                {
                    // 早朝時間帯稼働時間
                    if (eTime >= global.dt0500)
                    {
                        wkShinya += Utility.GetTimeSpan(sTime, global.dt0500).TotalMinutes;
                    }
                    else
                    {
                        wkShinya += Utility.GetTimeSpan(sTime, eTime).TotalMinutes;
                    }
                }

                // 終了時刻が22:00以降のとき
                if (eTime >= global.dt2200)
                {
                    // 当日分の深夜帯稼働時間を求める
                    if (sTime <= global.dt2200)
                    {
                        // 出勤時刻が22:00以前のとき深夜開始時刻は22:00とする
                        wkShinya += Utility.GetTimeSpan(global.dt2200, eTime).TotalMinutes;
                    }
                    else
                    {
                        // 出勤時刻が22:00以降のとき深夜開始時刻は出勤時刻とする
                        wkShinya += Utility.GetTimeSpan(sTime, eTime).TotalMinutes;
                    }

                    // 終了時間が24:00記入のときは23:59までの計算なので稼働時間1分加算する
                    if (Utility.StrtoInt(eH) == 24 && Utility.StrtoInt(eM) == 0)
                        wkShinya += 1;
                }
            }
            else
            {
                // 日付を超えて終了したとき（開始時刻 >= 終了時刻）※2014/10/10 同時刻は翌日の同時刻とみなす

                // 早出残業時間を求める
                if (sTime < global.dt0500)  // 開始時刻が午前5時前のとき
                {
                    wkShinya += Utility.GetTimeSpan(sTime, global.dt0500).TotalMinutes;
                }

                // 当日分の深夜勤務時間（～０：００まで）
                if (sTime <= global.dt2200)
                {
                    // 出勤時刻が22:00以前のとき無条件に120分
                    wkShinya += global.TOUJITSU_SINYATIME;
                }
                else
                {
                    // 出勤時刻が22:00以降のとき出勤時刻から24:00までを求める
                    wkShinya += Utility.GetTimeSpan(sTime, global.dt2359).TotalMinutes + 1;
                }

                // 0:00以降の深夜勤務時間を加算（０：００～終了時刻）
                if (eTime.TimeOfDay > global.dt0500.TimeOfDay)
                {
                    wkShinya += Utility.GetTimeSpan(global.dt0000, global.dt0500).TotalMinutes;
                }
                else
                {
                    wkShinya += Utility.GetTimeSpan(global.dt0000, eTime).TotalMinutes;
                }
            }

            return wkShinya;
        }

        /// -----------------------------------------------------------------------
        /// <summary>
        ///     自分以外で個人番号が同じ勤務票データが存在するか調べる </summary>
        /// <param name="dTbl">
        ///     JSDataSet.勤務票ヘッダDataTable</param>
        /// <param name="sNumber">
        ///     個人番号</param>
        /// <param name="sID">
        ///     勤務票ヘッダID</param>
        /// <returns>
        ///     同番号あり：true, 同番号なし：false</returns>
        /// -----------------------------------------------------------------------
        private bool getSameNumber(JAFA_DATADataSet.勤務票ヘッダDataTable dTbl, int sNumber, string sID)
        {
            var s = dTbl.Where(a => a.社員番号 == sNumber && a.ID != sID);
            if (s.Count() > 0) return false;
            else return true;
        }

        /// <summary>
        ///     「休日出勤」記入と週７日勤務
        /// </summary>
        /// <returns></returns>
        private bool errCheckHolWork(JAFA_DATADataSet dts, string tittle, int iX)
        {
            bool result = true;



            return result;
        }

        ///-------------------------------------------------------------------------
        /// <summary>
        ///     入所日前日以前に勤怠が入力されていたらエラー </summary>
        /// <param name="t">
        ///     JAFA_DATADataSet.勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="dt">
        ///     入所年月日</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///-------------------------------------------------------------------------
        private bool errNDateBeforeWork(JAFA_DATADataSet.勤務票明細Row t, string tittle, int iX, DateTime dt)
        {
            // 入所日前日以前のとき
            if (t.日付 < dt.Day)
            {
                string c = t.出勤区分 + t.開始時 + t.開始分 + t.終了時 + t.終了分 + t.休憩開始時 + t.休憩開始分;

                if (c.Trim() != string.Empty)
                {
                    setErrStatus(eDay, iX - 1, "入所日以前に勤怠が入力されています");
                    return false;
                }
            }

            return true;
        }

        ///-------------------------------------------------------------------------
        /// <summary>
        ///     入所日前日以前に勤怠が入力されていたらエラー </summary>
        /// <param name="t">
        ///     JAFA_DATADataSet.勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="dt">
        ///     入所年月日</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///-------------------------------------------------------------------------
        private bool errNDateBeforeWork(JAFA_OCRDataSet.確定勤務票明細Row t, string tittle, int iX, DateTime dt)
        {
            // 入所日前日以前のとき
            if (t.日付 < dt.Day)
            {
                string c = t.出勤区分 + t.開始時 + t.開始分 + t.終了時 + t.終了分 + t.休憩開始時 + t.休憩開始分;

                if (c.Trim() != string.Empty)
                {
                    setErrStatus(eDay, iX - 1, "入所日以前に勤怠が入力されています");
                    return false;
                }
            }

            return true;
        }
        
        ///-------------------------------------------------------------------------
        /// <summary>
        ///     退職日翌日以降に勤怠が入力されていたらエラー </summary>
        /// <param name="t">
        ///     JAFA_DATADataSet.勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="dt">
        ///     退職年月日</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///-------------------------------------------------------------------------
        private bool errRDateAfterWork(JAFA_DATADataSet.勤務票明細Row t, string tittle, int iX, DateTime dt)
        {
            // 退職日前日以前のとき
            if (dt != global.dtNon && t.日付 > dt.Day)
            {
                string c = t.出勤区分 + t.開始時 + t.開始分 + t.終了時 + t.終了分 + t.休憩開始時 + t.休憩開始分;

                if (c.Trim() != string.Empty)
                {
                    setErrStatus(eDay, iX - 1, "退職日以降に勤怠が入力されています");
                    return false;
                }
            }

            return true;
        }

        ///-------------------------------------------------------------------------
        /// <summary>
        ///     退職日翌日以降に勤怠が入力されていたらエラー </summary>
        /// <param name="t">
        ///     JAFA_DATADataSet.勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="dt">
        ///     退職年月日</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///-------------------------------------------------------------------------
        private bool errRDateAfterWork(JAFA_OCRDataSet.確定勤務票明細Row t, string tittle, int iX, DateTime dt)
        {
            // 退職日前日以前のとき
            if (dt != global.dtNon && t.日付 > dt.Day)
            {
                string c = t.出勤区分 + t.開始時 + t.開始分 + t.終了時 + t.終了分 + t.休憩開始時 + t.休憩開始分;

                if (c.Trim() != string.Empty)
                {
                    setErrStatus(eDay, iX - 1, "退職日以降に勤怠が入力されています");
                    return false;
                }
            }

            return true;
        }
    }
}
