using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAHR_OCR.Common;

namespace RELOCLUB_QR.QR
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
            GridViewSetting(dg);
            GridViewSetting2(dg2);

            // データグリッドビューにデータを表示します
            GridViewShow(dg);
            GridViewShow2(dg2);

            lblNoImage.Visible = false;
            button1.Enabled = false;

            // 拡大・縮小ボタンを非アクティブにする
            btnPlus.Enabled = false;
            btnMinus.Enabled = false;
        }

        RELODataSet dts = new RELODataSet();
        RELODataSetTableAdapters.NGTableAdapter adp = new RELODataSetTableAdapters.NGTableAdapter();
        
        //カラム定義
        string C_Date = "col1";
        string C_dNum = "col2";
        string C_fNum = "col3";
        string C_Img = "col4";
        string C_ID = "col5";

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
                tempDGV.Height = 221;

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
        private void GridViewShow(DataGridView tg)
        {
            try
            {
                int r =0;

                adp.Fill(dts.NG);
                var s = dts.NG.OrderBy(a => a.画像名).ThenBy(a => a.枚目).ThenBy(a => a.番号);

                foreach (var t in s)
                {
                    tg.Rows.Add();
                    tg[C_Date, r].Value = t.画像名.Substring(0, 4) + "/" + t.画像名.Substring(4, 2) + "/" + t.画像名.Substring(6, 2) + " " +
                                          t.画像名.Substring(8, 2) + ":" + t.画像名.Substring(10, 2) + ":" + t.画像名.Substring(12, 2);
                    tg[C_dNum, r].Value = t.枚目.ToString() + "/" + t.読込枚数.ToString();
                    tg[C_fNum, r].Value = t.番号.ToString();
                    tg[C_Img, r].Value = t.画像名.ToString();
                    tg[C_ID, r].Value = t.ID.ToString();

                    r++;
                }

                tg.CurrentCell = null;

                if (r > 0)
                {
                    label1.Text = "【ＱＲコード読み取りエラー : " + r.ToString() + "件】";
                }
                else
                {
                    label1.Text = "【ＱＲコード読み取りエラー】";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                string uPath = Properties.Settings.Default.unMatchPath;

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
                    label2.Text = "【アンマッチ画像 : " + r.ToString() + "件】";
                }
                else
                {
                    label2.Text = "【アンマッチ画像】";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            //修正画面へ組み入れた画像フォームの表示    
            //画像の出力が無い場合は、画像表示をしない。
            if (tempImgName == string.Empty)
            {
                leadImg.Visible = false;
                lblNoImage.Visible = false;
                global.pblImagePath = string.Empty;
                return;
            }

            //画像ファイルがあるとき表示
            if (System.IO.File.Exists(tempImgName))
            {
                lblNoImage.Visible = false;
                leadImg.Visible = true;

                // 画像操作ボタン
                btnPlus.Enabled = true;
                btnMinus.Enabled = true;

                //画像ロード
                Leadtools.Codecs.RasterCodecs.Startup();
                Leadtools.Codecs.RasterCodecs cs = new Leadtools.Codecs.RasterCodecs();

                // 描画時に使用される速度、品質、およびスタイルを制御します。 
                Leadtools.RasterPaintProperties prop = new Leadtools.RasterPaintProperties();
                prop = Leadtools.RasterPaintProperties.Default;
                prop.PaintDisplayMode = Leadtools.RasterPaintDisplayModeFlags.Resample;
                leadImg.PaintProperties = prop;

                leadImg.Image = cs.Load(tempImgName, 0, Leadtools.Codecs.CodecsLoadByteOrder.BgrOrGray, 1, 1);

                //画像表示倍率設定
                if (global.miMdlZoomRate == 0f)
                {
                    leadImg.ScaleFactor *= global.ZOOM_RATE;
                }
                else
                {
                    leadImg.ScaleFactor *= global.miMdlZoomRate;
                }

                //画像のマウスによる移動を可能とする
                leadImg.InteractiveMode = Leadtools.WinForms.RasterViewerInteractiveMode.Pan;

                // グレースケールに変換
                Leadtools.ImageProcessing.GrayscaleCommand grayScaleCommand = new Leadtools.ImageProcessing.GrayscaleCommand();
                grayScaleCommand.BitsPerPixel = 8;
                grayScaleCommand.Run(leadImg.Image);
                leadImg.Refresh();

                cs.Dispose();
                Leadtools.Codecs.RasterCodecs.Shutdown();

                // 拡大・縮小ボタンをアクティブにする
                btnPlus.Enabled = true;
                btnMinus.Enabled = true;
            }
            else
            {
                //画像ファイルがないとき
                lblNoImage.Visible = true;

                // 画像操作ボタン
                btnPlus.Enabled = false;
                btnMinus.Enabled = false;

                leadImg.Visible = false;

                // 拡大・縮小ボタンを非アクティブにする
                btnPlus.Enabled = false;
                btnMinus.Enabled = false;
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (leadImg.ScaleFactor < global.ZOOM_MAX)
            {
                leadImg.ScaleFactor += global.ZOOM_STEP;
            }
            global.miMdlZoomRate = (float)leadImg.ScaleFactor;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (leadImg.ScaleFactor > global.ZOOM_MIN)
            {
                leadImg.ScaleFactor -= global.ZOOM_STEP;
            }
            global.miMdlZoomRate = (float)leadImg.ScaleFactor;
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowImage(Properties.Settings.Default.ngPath + dg[C_Img, e.RowIndex].Value);
                button1.Enabled = true;
                dg2.CurrentCell = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count > 0)
            {
                // NGデータ削除
                ngDataDelete(dg.SelectedRows[0].Index);
            }
            else if (dg2.SelectedRows.Count > 0)
            {
                // アンマッチ画像削除
                unImageDelete(dg2[C_Img, dg2.SelectedRows[0].Index].Value.ToString());
            }
        }

        /// ------------------------------------------------------
        /// <summary>
        ///     ＮＧデータ削除 </summary>
        /// <param name="row">
        ///     対象の行インデックス</param>
        /// ------------------------------------------------------
        private void ngDataDelete(int row)
        {
            if (dg.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("選択されているＮＧデータを削除します。よろしいですか", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    int ids = int.Parse(dg[C_ID, row].Value.ToString());
                    adp.Fill(dts.NG);
                    RELODataSet.NGRow r = dts.NG.FindByID(ids);
                    r.Delete();
                    adp.Update(dts.NG);

                    // データ再表示
                    dg.Rows.Clear();
                    GridViewShow(dg);

                    // 画像表示初期化
                    leadImg.Visible = false;
                }
            }
        }

        /// --------------------------------------------------------
        /// <summary>
        ///     アンマッチ画像の削除 </summary>
        /// <param name="uFile">
        ///     削除する画像パス</param>
        /// --------------------------------------------------------
        private void unImageDelete(string uFile)
        {
            if (MessageBox.Show("選択されているアンマッチ画像を削除します。よろしいですか", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                System.IO.File.Delete(uFile);

                // データ再表示
                dg2.Rows.Clear();
                GridViewShow2(dg2);

                // 画像表示初期化
                leadImg.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // データ削除済みの画像の削除
            ngtifDelete();

            // クローズ
            this.Close();
        }

        private void frmNG_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        /// ---------------------------------------------------------------------------
        /// <summary>
        ///     該当するNGデータが存在しないNG画像を削除する </summary>
        /// ---------------------------------------------------------------------------
        private void ngtifDelete()
        {
            RELODataSet dts = new RELODataSet();
            RELODataSetTableAdapters.NGTableAdapter adp = new RELODataSetTableAdapters.NGTableAdapter();
            adp.Fill(dts.NG);

            foreach (var file in System.IO.Directory.GetFiles(Properties.Settings.Default.ngPath, "*.tif"))
            {
                string f = System.IO.Path.GetFileName(file);

                // NGデータがないとき
                if (!dts.NG.Any(a => a.画像名 == f))
                {
                    // 対象画像を削除する
                    System.IO.File.Delete(file);
                }
            }
        }

        private void dg2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowImage(dg2[C_Img, e.RowIndex].Value.ToString());
                button1.Enabled = true;
                dg.CurrentCell = null;
            }
        }
    }
}
