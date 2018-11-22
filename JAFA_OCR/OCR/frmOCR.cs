﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Leadtools;
using Leadtools.Codecs;
using System.Data.OleDb;
using JAFA_OCR.Common;
using System.IO;

namespace JAFA_OCR.OCR
{
    public partial class frmOCR : Form
    {
        public frmOCR()
        {
            InitializeComponent();
        }
        
        string _outPC = string.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("出力先ＰＣを選択してください", "出力先ＰＣ未選択", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // 20181122
            // 入力画像があるか？
            var tifCnt = System.IO.Directory.GetFiles(Properties.Settings.Default.scanPath, "*.tif").Count();

            if (tifCnt == 0)
            {
                MessageBox.Show("出勤簿画像がありません", "画像なし", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("OCR認識を実行します。" + Environment.NewLine + "よろしいですか", "OCR実行確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;

            string jobname = Properties.Settings.Default.wrHands_JobName;
            string msg = string.Empty;

            // 出力先ＰＣ登録名取得
            _outPC = comboBox1.Text;

            this.Hide();

            // 20181122

            if (Properties.Settings.Default.ocrStatus == 0)
            {
                // マルチTiff画像をシングルtifに分解する：LeadTools(SCANフォルダ → TRAYフォルダ)
                if (!MultiTif(Properties.Settings.Default.scanPath, Properties.Settings.Default.trayPath))
                {
                    this.Show();
                    return;
                }
            }
            else if (Properties.Settings.Default.ocrStatus == 1)
            {
                // 20181122
                // マルチTiff画像をシングルtifに分解する(SCANフォルダ → TRAYフォルダ)：2018/10/23
                if (!MultiTif_New(Properties.Settings.Default.scanPath, Properties.Settings.Default.trayPath))
                {
                    this.Show();
                    return;
                }
            }


            // 帳票ライブラリV8.0.3によるOCR認識実行
            wrhs803LibOCR(jobname);

            // フォームを閉じる
            this.Close();
        }

        ///----------------------------------------------------------------
        /// <summary>
        ///     帳票認識ライブラリ V8.0.3 による認識処理実行
        /// </summary>
        ///----------------------------------------------------------------
        private void wrhs803LibOCR(string jobName)
        {
            // ファイル名のタイムスタンプを設定
            string fnm = string.Format("{0:0000}", DateTime.Today.Year) +
                         string.Format("{0:00}", DateTime.Today.Month) +
                         string.Format("{0:00}", DateTime.Today.Day) +
                         string.Format("{0:00}", DateTime.Now.Hour) +
                         string.Format("{0:00}", DateTime.Now.Minute) +
                         string.Format("{0:00}", DateTime.Now.Second);

            int sNum = 0;
            int sOK = 0;
            int sNG = 0;
            int ret = 0;
            string newFileName = string.Empty;

            try
            {
                // オーナーフォームを無効にする
                this.Enabled = false;

                // プログレスバーを表示する
                frmPrg frmP = new frmPrg();
                frmP.Owner = this;
                frmP.Show();

                // 処理する画像数を取得
                int t = System.IO.Directory.GetFiles(Properties.Settings.Default.trayPath, "*.tif").Count();

                // 順番に認識処理を実行
                foreach (string files in System.IO.Directory.GetFiles(Properties.Settings.Default.trayPath, "*.tif"))
                {
                    // 画像数カウント
                    sNum++;

                    // プログレス表示
                    frmP.Text = "OCR認識中です ... " + sNum.ToString() + "/" + t.ToString();
                    frmP.progressValue = sNum * 100 / t;
                    frmP.ProgressStep();

                    // 標準パターンの読み込み
                    ret = FormRecog.OcrPatternLoad(Properties.Settings.Default.ocrPatternLoadPath);
                    
                    // パターン読み込みに成功したとき
                    if (ret > 0)
                    {
                        // 帳票認識ライブラリの制御内容を設定
                        FormRecog.OcrSetStatus(5, 0);   // 強制認識制御（強制認識しない）

                        // 認識結果出力イメージファイル
                        StringBuilder outimage = new StringBuilder(256);
                        outimage.Append(Properties.Settings.Default.wrOutPath + System.IO.Path.GetFileName(files));

                        // 認識結果出力テキストファイル
                        StringBuilder outtext = new StringBuilder(256);
                        outtext.Append(Properties.Settings.Default.wrOutPath + System.IO.Path.GetFileNameWithoutExtension(files) + ".csv");

                        // 認識結果 構造体
                        FormRecog.FORM_RECOG_DATA dt = new FormRecog.FORM_RECOG_DATA();

                        // 認識処理を開始
                        ret = FormRecog.OcrFormRecogStart(jobName, files, outimage, outtext, ref dt, false, false);

                        // 認識成功のとき
                        if (ret > 0)
                        {
                            // 認識結果のメモリ解放
                            ret = FormRecog.OcrFormStructFree(ref dt);

                            // 認識終了
                            ret = FormRecog.OcrFormRecogEnd();

                            // PC毎の出力先フォルダがなければ作成する
                            string rPath = Properties.Settings.Default.pcPath + _outPC + @"\";
                            if (System.IO.Directory.Exists(rPath) == false)
                                System.IO.Directory.CreateDirectory(rPath);

                            // 出力されたイメージファイルとテキストファイルのリネーム処理を行います
                            // READフォルダ → DATAフォルダ
                            string inCsvFile = Properties.Settings.Default.wrOutPath +
                                               Properties.Settings.Default.wrReaderOutFile;

                            // 新ファイル名
                            newFileName = rPath + fnm + sNum.ToString().PadLeft(3, '0');

                            wrhOutFileRename(inCsvFile, newFileName);

                            // カウント
                            sOK++;
                        }
                        else
                        {
                            //MessageBox.Show("OCR認識開始に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            // NGフォルダがあるか？なければ作成する（TIFフォルダ）
                            if (!System.IO.Directory.Exists(Properties.Settings.Default.ngPath))
                            {
                                System.IO.Directory.CreateDirectory(Properties.Settings.Default.ngPath);
                            }

                            // 移動先NGフォルダパス
                            string toImg = Properties.Settings.Default.ngPath + System.IO.Path.GetFileName(files);

                            // 同名ファイルが既に登録済みのときは削除する
                            if (System.IO.File.Exists(toImg)) System.IO.File.Delete(toImg);

                            // NG画像をコピーする
                            if (System.IO.File.Exists(files)) System.IO.File.Copy(files, toImg);

                            // NGカウント
                            sNG++;
                        }
                    }
                    else
                    {
                        MessageBox.Show("OCR標準パターンの読み込みに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                // いったんオーナーをアクティブにする
                this.Activate();

                // 進行状況ダイアログを閉じる
                frmP.Close();

                // オーナーのフォームを有効に戻す
                this.Enabled = true;

                // 終了表示
                string msg = sNum.ToString() + "件のOCR認識処理を行いました" + Environment.NewLine + Environment.NewLine;
                msg += "OK件数 ： " + sOK.ToString() + Environment.NewLine;
                msg += "NG件数 ： " + sNG.ToString();
                MessageBox.Show(msg, "OCR認識結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                // TRAYフォルダの全てのtifファイルを削除します
                foreach (var files in System.IO.Directory.GetFiles(Properties.Settings.Default.trayPath, "*.tif"))
                {
                    System.IO.File.Delete(files);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        // OCRファイル名連番 : WinReader
        int dNo = 0;
        
        /// -----------------------------------------------------------------
        /// <summary>
        ///     CSVファイルと画像ファイルの名前を日付スタンプに変更する </summary>
        /// <param name="readFilePath">
        ///     入力CSVファイル名(フルパス）</param>
        /// <param name="newFnm">
        ///     新ファイル名（フルパス・但し拡張子なし）</param>
        /// -----------------------------------------------------------------
        private void wrhOutFileRename(string readFilePath, string newFnm)
        {
            string imgName = string.Empty;      // 画像ファイル名
            string[] stArrayData;               // CSVファイルを１行単位で格納する配列
            string inFilePath = string.Empty;   // ＯＣＲ認識モードごとの入力ファイル名

            // CSVデータの存在を確認します
            if (!System.IO.File.Exists(readFilePath)) return;

            // StreamReader の新しいインスタンスを生成する
            //入力ファイル
            System.IO.StreamReader inFile = new System.IO.StreamReader(readFilePath, Encoding.Default);

            // 読み込んだ結果をすべて格納するための変数を宣言する
            string stResult = string.Empty;
            string stBuffer;

            // 読み込みできる文字がなくなるまで繰り返す
            while (inFile.Peek() >= 0)
            {
                // ファイルを 1 行ずつ読み込む
                stBuffer = inFile.ReadLine();

                // カンマ区切りで分割して配列に格納する
                stArrayData = stBuffer.Split(',');

                //先頭に「*」か「#」があったらヘッダー情報
                if ((stArrayData[0] == "*"))
                {
                    //文字列バッファをクリア
                    stResult = string.Empty;

                    // 文字列再構成（画像ファイル名を変更する）
                    stBuffer = string.Empty;
                    for (int i = 0; i < stArrayData.Length; i++)
                    {
                        if (stBuffer != string.Empty)
                        {
                            stBuffer += ",";
                        }

                        // 画像ファイル名を変更する
                        if (i == 3)
                        {
                            stArrayData[i] = System.IO.Path.GetFileName(newFnm) + ".tif"; // 画像ファイル名を変更
                        }

                        // フィールド結合
                        string sta = stArrayData[i].Trim();
                        stBuffer += sta;
                    }
                }

                // 読み込んだものを追加で格納する
                stResult += (stBuffer + Environment.NewLine);
            }

            // CSVファイル書き出し
            System.IO.StreamWriter outFile = new System.IO.StreamWriter(newFnm + ".csv",
                                                    false, System.Text.Encoding.GetEncoding(932));
            outFile.Write(stResult);

            // 出力ファイルを閉じる
            outFile.Close();

            // 入力ファイルを閉じる
            inFile.Close();

            // 入力ファイル削除 : "txtout.csv"
            string inPath = System.IO.Path.GetDirectoryName(readFilePath);
            Utility.FileDelete(inPath, Properties.Settings.Default.wrReaderOutFile);

            // 画像ファイルをリネーム
            System.IO.File.Move(Properties.Settings.Default.wrOutPath + "WRH00001.tif", newFnm + ".tif");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOCR_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        ///------------------------------------------------------------------------------
        /// <summary>
        ///     マルチフレームの画像ファイルを頁ごとに分割する </summary>
        /// <param name="InPath">
        ///     画像ファイル入力パス</param>
        /// <param name="outPath">
        ///     分割後出力パス</param>
        /// <returns>
        ///     true:分割を実施, false:分割ファイルなし</returns>
        ///------------------------------------------------------------------------------
        private bool MultiTif(string InPath, string outPath)
        {
            //スキャン出力画像を確認
            if (System.IO.Directory.GetFiles(InPath, "*.tif").Count() == 0)
            {
                MessageBox.Show("ＯＣＲ変換処理対象の画像ファイルが指定フォルダ " + InPath + " に存在しません", "スキャン画像確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // 出力先フォルダがなければ作成する
            if (System.IO.Directory.Exists(outPath) == false)
            {
                System.IO.Directory.CreateDirectory(outPath);
            }

            // 出力先フォルダ内の全てのファイルを削除する（通常ファイルは存在しないが例外処理などで残ってしまった場合に備えて念のため）
            foreach (string files in System.IO.Directory.GetFiles(outPath, "*"))
            {
                System.IO.File.Delete(files);
            }

            RasterCodecs.Startup();
            RasterCodecs cs = new RasterCodecs();

            int _pageCount = 0;
            string fnm = string.Empty;

            // マルチTIFを分解して画像ファイルをTRAYフォルダへ保存する
            foreach (string files in System.IO.Directory.GetFiles(InPath, "*.tif"))
            {
                // 画像読み出す
                RasterImage leadImg = cs.Load(files, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);

                // 頁数を取得
                int _fd_count = leadImg.PageCount;

                // 頁ごとに読み出す
                for (int i = 1; i <= _fd_count; i++)
                {
                    // ファイル名（日付時間部分）
                    string fName = string.Format("{0:0000}", DateTime.Today.Year) +
                            string.Format("{0:00}", DateTime.Today.Month) +
                            string.Format("{0:00}", DateTime.Today.Day) +
                            string.Format("{0:00}", DateTime.Now.Hour) +
                            string.Format("{0:00}", DateTime.Now.Minute) +
                            string.Format("{0:00}", DateTime.Now.Second);

                    // ファイル名設定
                    _pageCount++;
                    fnm = outPath + fName + string.Format("{0:000}", _pageCount) + ".tif";

                    // 画像保存
                    cs.Save(leadImg, fnm, RasterImageFormat.Tif, 0, i, i, 1, CodecsSavePageMode.Insert);
                }
            }

            // InPathフォルダの全てのtifファイルを削除する
            foreach (var files in System.IO.Directory.GetFiles(InPath, "*.tif"))
            {
                System.IO.File.Delete(files);
            }

            return true;
        }

        ///------------------------------------------------------------------------------
        /// <summary>
        ///     マルチフレームの画像ファイルを頁ごとに分割する：OpenCVバージョン</summary>
        /// <param name="InPath">
        ///     画像ファイル入力パス</param>
        /// <param name="outPath">
        ///     分割後出力パス</param>
        /// <returns>
        ///     true:分割を実施, false:分割ファイルなし</returns>
        ///------------------------------------------------------------------------------
        private bool MultiTif_New(string InPath, string outPath)
        {
            //スキャン出力画像を確認
            if (System.IO.Directory.GetFiles(InPath, "*.tif").Count() == 0)
            {
                MessageBox.Show("ＯＣＲ変換処理対象の画像ファイルが指定フォルダ " + InPath + " に存在しません", "スキャン画像確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // 出力先フォルダがなければ作成する
            if (System.IO.Directory.Exists(outPath) == false)
            {
                System.IO.Directory.CreateDirectory(outPath);
            }

            // 出力先フォルダ内の全てのファイルを削除する（通常ファイルは存在しないが例外処理などで残ってしまった場合に備えて念のため）
            foreach (string files in System.IO.Directory.GetFiles(outPath, "*"))
            {
                System.IO.File.Delete(files);
            }

            int _pageCount = 0;
            string fnm = string.Empty;

            // マルチTIFを分解して画像ファイルをTRAYフォルダへ保存する
            foreach (string files in System.IO.Directory.GetFiles(InPath, "*.tif"))
            {
                //TIFFのImageCodecInfoを取得する
                ImageCodecInfo ici = GetEncoderInfo("image/tiff");

                if (ici == null)
                {
                    return false;
                }

                using (FileStream tifFS = new FileStream(files, FileMode.Open, FileAccess.Read))
                {
                    Image gim = Image.FromStream(tifFS);

                    FrameDimension gfd = new FrameDimension(gim.FrameDimensionsList[0]);

                    //全体のページ数を得る
                    int pageCount = gim.GetFrameCount(gfd);

                    for (int i = 0; i < pageCount; i++)
                    {
                        gim.SelectActiveFrame(gfd, i);

                        // ファイル名（日付時間部分）
                        string fName = string.Format("{0:0000}", DateTime.Today.Year) + string.Format("{0:00}", DateTime.Today.Month) +
                                string.Format("{0:00}", DateTime.Today.Day) + string.Format("{0:00}", DateTime.Now.Hour) +
                                string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second);

                        _pageCount++;

                        // ファイル名設定
                        fnm = outPath + fName + string.Format("{0:000}", _pageCount) + ".tif";

                        EncoderParameters ep = null;
                        
                        // マルチTIFFではなく、1枚だけ保存する
                        // 圧縮方法を指定する
                        ep = new EncoderParameters(1);
                        ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)EncoderValue.CompressionCCITT4);
                            
                        // 画像保存
                        gim.Save(fnm, ici, ep);
                        ep.Dispose();
                    }
                }
            }

            // InPathフォルダの全てのtifファイルを削除する
            foreach (var files in System.IO.Directory.GetFiles(InPath, "*.tif"))
            {
                System.IO.File.Delete(files);
            }

            return true;
        }

        //MimeTypeで指定されたImageCodecInfoを探して返す
        private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mineType)
        {
            //GDI+ に組み込まれたイメージ エンコーダに関する情報をすべて取得
            System.Drawing.Imaging.ImageCodecInfo[] encs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            //指定されたMimeTypeを探して見つかれば返す
            foreach (System.Drawing.Imaging.ImageCodecInfo enc in encs)
            {
                if (enc.MimeType == mineType)
                {
                    return enc;
                }
            }
            return null;
        }

        //ImageFormatで指定されたImageCodecInfoを探して返す
        private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(System.Drawing.Imaging.ImageFormat f)
        {
            System.Drawing.Imaging.ImageCodecInfo[] encs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();

            foreach (System.Drawing.Imaging.ImageCodecInfo enc in encs)
            {
                if (enc.FormatID == f.Guid)
                {
                    return enc;
                }
            }

            return null;
        }

        private void showMultiTiff(string tiffFileName)
        {
            FileStream tifFS = new FileStream(tiffFileName, FileMode.Open, FileAccess.Read);
            Image gim = Image.FromStream(tifFS);
            FrameDimension gfd = new FrameDimension(gim.FrameDimensionsList[0]);

            //全体のページ数を得る
            int pageCount = gim.GetFrameCount(gfd); 
            
            for (int i = 0; i < pageCount; i++)
            {
                gim.SelectActiveFrame(gfd, i);

                gim.Save(@"C:\multitif\0902" + i.ToString().PadLeft(3, '0') + ".tif", ImageFormat.Tiff);
            }

            MessageBox.Show("finish!!");
        }

        private void frmOCR_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最少サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // コンボボックス
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // コンボボックス
            loadOutPcMst();
        }

        ///---------------------------------------------------
        /// <summary>
        ///     出力先ＰＣコンボボックスへロードする</summary>
        /// 
        ///---------------------------------------------------
        private void loadOutPcMst()
        {
            JAFA_OCRDataSet dts = new JAFA_OCRDataSet();           
            JAFA_OCRDataSetTableAdapters.出力先マスターTableAdapter adp = new JAFA_OCRDataSetTableAdapters.出力先マスターTableAdapter();

            adp.Fill(dts.出力先マスター);

            var s = dts.出力先マスター.OrderBy(a => a.ID);

            foreach (var t in s)
            {
                comboBox1.Items.Add(t.登録名); 
            }
        }
    }
}
