using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAFA_DATA.Common;
using OpenCvSharp;

namespace JAFA_DATA.OCR
{
    public partial class frmNG : Form
    {
        public frmNG()
        {
            InitializeComponent();
        }

        private void frmNG_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // データグリッド定義
            GridViewSetting2(dg2);

            // データグリッドビューにデータを表示します
            GridViewShow2(dg2);

            lblNoImage.Visible = false;
            button1.Enabled = false;
            button3.Enabled = false;
            trackBar1.Enabled = false;
        }

        // openCvSharp 関連　2018/10/23
        const float B_WIDTH = 0.35f;
        const float B_HEIGHT = 0.35f;
        const float A_WIDTH = 0.05f;
        const float A_HEIGHT = 0.05f;

        float n_width = 0f;
        float n_height = 0f;

        Mat mMat = new Mat();

        //カラム定義
        string C_Date = "col1";
        string C_dNum = "col2";
        string C_fNum = "col3";
        string C_Img = "col4";
        string C_ID = "col5";

        // 表示中画像名
        string dImg = string.Empty;

        /// <summary>
        /// データグリッドビューの定義を行います
        /// </summary>
        /// <param name="tempDGV">データグリッドビューオブジェクト</param>
        private void GridViewSetting(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更する

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", (float)9.5, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", (float)9.5, FontStyle.Regular);

                // 行の高さ
                tempDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                tempDGV.ColumnHeadersHeight = 20;
                tempDGV.RowTemplate.Height = 20;

                // 全体の高さ
                //tempDGV.Height = 574;
                tempDGV.Height = 321;

                // 奇数行の色
                tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

                //各列幅指定
                tempDGV.Columns.Add(C_Date, "処理日時");
                tempDGV.Columns.Add(C_dNum, "枚目");
                tempDGV.Columns.Add(C_fNum, "位置");
                tempDGV.Columns.Add(C_Img, "画像名");
                tempDGV.Columns.Add(C_ID, "ID");

                tempDGV.Columns[C_Date].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tempDGV.Columns[C_dNum].Width = 80;
                tempDGV.Columns[C_fNum].Width = 80;
                tempDGV.Columns[C_Img].Visible = false;
                tempDGV.Columns[C_ID].Visible = false;

                tempDGV.Columns[C_Date].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                tempDGV.Columns[C_dNum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                tempDGV.Columns[C_fNum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = false;

                // 編集不可とする
                tempDGV.ReadOnly = true;

                // 追加行表示しない
                tempDGV.AllowUserToAddRows = false;

                // データグリッドビューから行削除を禁止する
                tempDGV.AllowUserToDeleteRows = false;

                // 手動による列移動の禁止
                tempDGV.AllowUserToOrderColumns = false;

                // 列サイズ変更可
                tempDGV.AllowUserToResizeColumns = true;

                // 行サイズ変更禁止
                tempDGV.AllowUserToResizeRows = false;

                // 行ヘッダーの自動調節
                //tempDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                //TAB動作
                tempDGV.StandardTab = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// データグリッドビューの定義を行います
        /// </summary>
        /// <param name="tempDGV">データグリッドビューオブジェクト</param>
        private void GridViewSetting2(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更する

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", (float)9.5, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", (float)9.5, FontStyle.Regular);

                // 行の高さ
                tempDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                tempDGV.ColumnHeadersHeight = 20;
                tempDGV.RowTemplate.Height = 20;

                // 全体の高さ
                //tempDGV.Height = 574;
                tempDGV.Height = 562;

                // 奇数行の色
                tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

                //各列幅指定
                tempDGV.Columns.Add(C_Date, "処理日時");
                tempDGV.Columns.Add(C_Img, "画像名");
                tempDGV.Columns.Add(C_ID, "ID");

                tempDGV.Columns[C_Date].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tempDGV.Columns[C_Img].Visible = false;
                tempDGV.Columns[C_ID].Visible = false;

                tempDGV.Columns[C_Date].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = false;

                // 編集不可とする
                tempDGV.ReadOnly = true;

                // 追加行表示しない
                tempDGV.AllowUserToAddRows = false;

                // データグリッドビューから行削除を禁止する
                tempDGV.AllowUserToDeleteRows = false;

                // 手動による列移動の禁止
                tempDGV.AllowUserToOrderColumns = false;

                // 列サイズ変更可
                tempDGV.AllowUserToResizeColumns = true;

                // 行サイズ変更禁止
                tempDGV.AllowUserToResizeRows = false;

                // 行ヘッダーの自動調節
                //tempDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                //TAB動作
                tempDGV.StandardTab = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        ///     データグリッドビューにデータを表示します </summary>
        /// <param name="tempGrid">
        ///     データグリッドビューオブジェクト名</param>
        /// ----------------------------------------------------------------------------
        private void GridViewShow2(DataGridView tg)
        {
            try
            {
                int r = 0;

                string uPath = Properties.Settings.Default.ngPath;

                foreach (string files in System.IO.Directory.GetFiles(uPath, "*.tif"))
                {
                    string fName = System.IO.Path.GetFileName(files);
 
                    tg.Rows.Add();
                    tg[C_Date, r].Value = fName.Substring(0, 4) + "/" + fName.Substring(4, 2) + "/" + fName.Substring(6, 2) + " " +
                                          fName.Substring(8, 2) + ":" + fName.Substring(10, 2) + ":" + fName.Substring(12, 2);
                    tg[C_Img, r].Value = files;

                    r++;
                }

                tg.CurrentCell = null;

                if (r > 0)
                {
                    label1.Text = "【" + r.ToString() + "件】";
                }
                else
                {
                    label1.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        ///-----------------------------------------------------------
        /// <summary>
        ///     画像表示 openCV：2018/10/24 </summary>
        /// <param name="img">
        ///     表示画像ファイル名</param>
        ///-----------------------------------------------------------
        private void showImage_openCv(string img)
        {
            n_width = B_WIDTH;
            n_height = B_HEIGHT;

            imgShow(img, n_width, n_height);

            trackBar1.Value = 0;
        }

        ///---------------------------------------------------------
        /// <summary>
        ///     画像表示メイン openCV : 2018/10/24 </summary>
        /// <param name="mImg">
        ///     Mat形式イメージ</param>
        /// <param name="w">
        ///     width</param>
        /// <param name="h">
        ///     height</param>
        ///---------------------------------------------------------
        private void imgShow(string filePath, float w, float h)
        {
            mMat = new Mat(filePath, ImreadModes.GrayScale);
            Bitmap bt = MatToBitmap(mMat);

            // Bitmap を生成
            Bitmap canvas = new Bitmap((int)(bt.Width * w), (int)(bt.Height * h));

            Graphics g = Graphics.FromImage(canvas);

            g.DrawImage(bt, 0, 0, bt.Width * w, bt.Height * h);

            //メモリクリア
            bt.Dispose();
            g.Dispose();

            pictureBox1.Image = canvas;
        }

        ///---------------------------------------------------------
        /// <summary>
        ///     画像表示メイン openCV : 2018/10/24 </summary>
        /// <param name="mImg">
        ///     Mat形式イメージ</param>
        /// <param name="w">
        ///     width</param>
        /// <param name="h">
        ///     height</param>
        ///---------------------------------------------------------
        private void imgShow(Mat mImg, float w, float h)
        {
            int cWidth = 0;
            int cHeight = 0;

            Bitmap bt = MatToBitmap(mImg);

            // Bitmapサイズ
            if (panel1.Width < (bt.Width * w) || panel1.Height < (bt.Height * h))
            {
                cWidth = (int)(bt.Width * w);
                cHeight = (int)(bt.Height * h);
            }
            else
            {
                cWidth = panel1.Width;
                cHeight = panel1.Height;
            }

            // Bitmap を生成
            Bitmap canvas = new Bitmap(cWidth, cHeight);

            // ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            // 画像をcanvasの座標(0, 0)の位置に指定のサイズで描画する
            g.DrawImage(bt, 0, 0, bt.Width * w, bt.Height * h);

            //メモリクリア
            bt.Dispose();
            g.Dispose();

            // PictureBox1に表示する
            pictureBox1.Image = canvas;
        }


        // GUI上に画像を表示するには、OpenCV上で扱うMat形式をBitmap形式に変換する必要がある
        public static Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }





        /// ------------------------------------------------------------------------------
        /// <summary>
        ///     伝票画像表示 </summary>
        /// <param name="iX">
        ///     現在の伝票</param>
        /// <param name="tempImgName">
        ///     画像名</param>
        /// ------------------------------------------------------------------------------
        public void ShowImage(string tempImgName)
        {
            ////修正画面へ組み入れた画像フォームの表示    
            ////画像の出力が無い場合は、画像表示をしない。
            //if (tempImgName == string.Empty)
            //{
            //    leadImg.Visible = false;
            //    lblNoImage.Visible = false;
            //    global.pblImagePath = string.Empty;
            //    return;
            //}

            ////画像ファイルがあるとき表示
            //if (System.IO.File.Exists(tempImgName))
            //{
            //    lblNoImage.Visible = false;
            //    leadImg.Visible = true;

            //    // 画像操作ボタン
            //    btnPlus.Enabled = true;
            //    btnMinus.Enabled = true;

            //    //画像ロード
            //    Leadtools.Codecs.RasterCodecs.Startup();
            //    Leadtools.Codecs.RasterCodecs cs = new Leadtools.Codecs.RasterCodecs();

            //    // 描画時に使用される速度、品質、およびスタイルを制御します。 
            //    Leadtools.RasterPaintProperties prop = new Leadtools.RasterPaintProperties();
            //    prop = Leadtools.RasterPaintProperties.Default;
            //    prop.PaintDisplayMode = Leadtools.RasterPaintDisplayModeFlags.Resample;
            //    leadImg.PaintProperties = prop;

            //    leadImg.Image = cs.Load(tempImgName, 0, Leadtools.Codecs.CodecsLoadByteOrder.BgrOrGray, 1, 1);

            //    //画像表示倍率設定
            //    if (global.miMdlZoomRate == 0f)
            //    {
            //        leadImg.ScaleFactor *= global.ZOOM_RATE;
            //    }
            //    else
            //    {
            //        leadImg.ScaleFactor *= global.miMdlZoomRate;
            //    }

            //    //画像のマウスによる移動を可能とする
            //    leadImg.InteractiveMode = Leadtools.WinForms.RasterViewerInteractiveMode.Pan;

            //    // グレースケールに変換
            //    Leadtools.ImageProcessing.GrayscaleCommand grayScaleCommand = new Leadtools.ImageProcessing.GrayscaleCommand();
            //    grayScaleCommand.BitsPerPixel = 8;
            //    grayScaleCommand.Run(leadImg.Image);
            //    leadImg.Refresh();

            //    cs.Dispose();
            //    Leadtools.Codecs.RasterCodecs.Shutdown();

            //    // 拡大・縮小ボタンをアクティブにする
            //    btnPlus.Enabled = true;
            //    btnMinus.Enabled = true;
            //}
            //else
            //{
            //    //画像ファイルがないとき
            //    lblNoImage.Visible = true;

            //    // 画像操作ボタン
            //    btnPlus.Enabled = false;
            //    btnMinus.Enabled = false;

            //    leadImg.Visible = false;

            //    // 拡大・縮小ボタンを非アクティブにする
            //    btnPlus.Enabled = false;
            //    btnMinus.Enabled = false;
            //}
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            //if (leadImg.ScaleFactor < global.ZOOM_MAX)
            //{
            //    leadImg.ScaleFactor += global.ZOOM_STEP;
            //}
            //global.miMdlZoomRate = (float)leadImg.ScaleFactor;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            //if (leadImg.ScaleFactor > global.ZOOM_MIN)
            //{
            //    leadImg.ScaleFactor -= global.ZOOM_STEP;
            //}
            //global.miMdlZoomRate = (float)leadImg.ScaleFactor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // アンマッチ画像削除
            unImageDelete(dg2[C_Img, dg2.SelectedRows[0].Index].Value.ToString());
        }

        /// --------------------------------------------------------
        /// <summary>
        ///     アンマッチ画像の削除 </summary>
        /// <param name="uFile">
        ///     削除する画像パス</param>
        /// --------------------------------------------------------
        private void unImageDelete(string uFile)
        {
            if (MessageBox.Show("選択されているNG画像を削除します。よろしいですか", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                System.IO.File.Delete(uFile);

                // データ再表示
                dg2.Rows.Clear();
                GridViewShow2(dg2);

                // 画像表示初期化
                dispClear();
            }
        }

        private void dispClear()
        {
            // 画像表示初期化
            //leadImg.Visible = false;

            pictureBox1.Image = null;
            button1.Enabled = false;
            button3.Enabled = false;
            trackBar1.Enabled = false;
            dImg = string.Empty;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // クローズ
            this.Close();
        }

        private void frmNG_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
        
        private void dg2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 2018/11/12 コメント化
                //ShowImage(dg2[C_Img, e.RowIndex].Value.ToString());
                //button1.Enabled = true;
                //button3.Enabled = true;
                //btnPlus.Enabled = true;
                //btnMinus.Enabled = true;
                //dImg = dg2[C_Img, e.RowIndex].Value.ToString();


                // openCV:2018/10/24
                showImage_openCv(dg2[C_Img, e.RowIndex].Value.ToString());

                button1.Enabled = true;
                button3.Enabled = true;
                trackBar1.Enabled = true;

                dImg = dg2[C_Img, e.RowIndex].Value.ToString();
            }
        }

        private bool reNewData()
        {
            if (MessageBox.Show("選択されている画像を出勤簿データに再生します。よろしいですか", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return false;
            }

            frmNGReNew frm = new frmNGReNew();
            frm.ShowDialog();
            // 前半後半区分を取得
            int _zKKbn = frm.zkKbn;
            frm.Dispose();

            // 前半後半区分のとき終了
            if (frm.zkKbn == 0)
            {
                return false;
            }

            _zKKbn--;

            // 出力配列
            string[] arrayCsv = null;

            StringBuilder sb = new StringBuilder();
            int cnt = 1;

            sb.Clear();
            sb.Append("*," + _zKKbn.ToString() + "," + _zKKbn.ToString() + ",");
            sb.Append(System.IO.Path.GetFileName(dImg) + "," + global.cnfYear.ToString() + "," + global.cnfMonth.ToString() + ",,,,");

            // 配列にセット
            Array.Resize(ref arrayCsv, cnt);        // 配列のサイズ拡張
            arrayCsv[cnt - 1] = sb.ToString();      // 文字列のセット

            for (int i = 0; i < 16; i++)
            {
                cnt++;
                sb.Clear();
                sb.Append(",,,,,,,0");

                // 配列にセット
                Array.Resize(ref arrayCsv, cnt);        // 配列のサイズ拡張
                arrayCsv[cnt - 1] = sb.ToString();      // 文字列のセット
            }

            // CSVファイル出力
            Utility.csvFileWrite(Properties.Settings.Default.dataPath, arrayCsv, System.IO.Path.GetFileNameWithoutExtension(dImg));

            // NG画像を移動
            System.IO.File.Move(dImg, Properties.Settings.Default.dataPath + System.IO.Path.GetFileName(dImg));

            // メッセージ
            MessageBox.Show("画像の出勤簿データ再生処理が終了しました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (reNewData())
            {
                // データ再表示
                dg2.Rows.Clear();
                GridViewShow2(dg2);

                // 画像表示初期化
                dispClear();
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            n_width = B_WIDTH + (float)trackBar1.Value * 0.05f;
            n_height = B_HEIGHT + (float)trackBar1.Value * 0.05f;
            
            imgShow(mMat, n_width, n_height);
        }
    }
}
