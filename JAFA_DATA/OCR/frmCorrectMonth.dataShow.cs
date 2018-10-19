using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using JAFA_DATA.Common;

namespace JAFA_DATA.OCR
{
    partial class frmCorrectMonth
    {
        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     確定勤務票ヘッダと確定勤務票明細のデータセットにデータを読み込む </summary>
        ///------------------------------------------------------------------------------------
        private void getDataSet()
        {
            adpMn.確定勤務票ヘッダTableAdapter.Fill(dts.確定勤務票ヘッダ);
            adpMn.確定勤務票明細TableAdapter.Fill(dts.確定勤務票明細);
        }
        
        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     データを画面に表示します </summary>
        /// <param name="iX">
        ///     ヘッダデータインデックス</param>
        ///------------------------------------------------------------------------------------
        private void showOcrData(int iX)
        {
            // ビュー読み込み
            //khAdp.Fill(dts.確定ヘッダ);
            //kAdp.Fill(dts.確定データ);

            // 確定勤務票ヘッダテーブル行を取得
            JAFA_OCRDataSet.確定勤務票ヘッダRow r = (JAFA_OCRDataSet.確定勤務票ヘッダRow)dts.確定勤務票ヘッダ.Rows[iX];

            // フォーム初期化
            formInitialize(iX);

            /*
            * ヘッダ情報表示
            */
            // 年月
            txtYear.Text = Utility.EmptytoZero(r.年.ToString());
            txtMonth.Text = Utility.EmptytoZero(r.月.ToString());

            lblZenKou.Text = string.Empty;

            // 交通費等の入力を可能とする
            txtKoutsuuhi.Enabled = true;
            txtNittou.Enabled = true;
            txtShukuhakuhi.Enabled = true;

            global.ChangeValueStatus = false;   // チェンジバリューステータス
            txtNo.Text = string.Empty;
            global.ChangeValueStatus = true;    // チェンジバリューステータス

            txtNo.Text = Utility.EmptytoZero(r.社員番号.ToString());        // 社員番号
            txtKoutsuuhi.Text = Utility.EmptytoZero(r.交通費.ToString("#,0"));   // 交通費
            txtNittou.Text = Utility.EmptytoZero(r.日当.ToString("#,0"));        // 日当
            txtShukuhakuhi.Text = Utility.EmptytoZero(r.宿泊費.ToString("#,0")); // 宿泊費

            /*
             * 確定データ表示
             */
            showItem(r.ヘッダID, dGV);
     
            // エラー情報表示初期化
            lblErrMsg.Visible = false;
            lblErrMsg.Text = string.Empty;

            //// 画像表示
            //ShowImage(global.pblImagePath + r.画像名.ToString());                        
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     確定勤怠データ表示 </summary>
        /// <param name="hID">
        ///     ヘッダID</param>
        /// <param name="dGV">
        ///     データグリッドビューオブジェクト</param>
        ///------------------------------------------------------------------------------------
        private void showItem(string hID, DataGridView dGV)
        {
            // グリッドビュー行数を設定して表示色を初期化
            dGV.Rows.Clear();
            dGV.RowCount = global._MonthMULTIGYO;
            for (int i = 0; i < global._MonthMULTIGYO; i++)
            {
                dGV.Rows[i].DefaultCellStyle.BackColor = Color.FromName("Control");
                dGV.Rows[i].ReadOnly = true;    // 初期設定は編集不可とする
            }

            // 行インデックス初期化
            int mRow = 0;

            // 日別勤務実績表示
            var h = dts.確定勤務票明細.Where(a => a.ヘッダID == hID).OrderBy(a => a.日付);

            foreach (var t in h)
            {
                /*
                 * 出勤簿明細表示
                 */

                // 画像表示
                if (mRow == 0)
                {
                    ShowImage(global.pblImagePath + t.画像名.ToString());
                }

                // 表示色を初期化
                dGV.Rows[mRow].DefaultCellStyle.BackColor = Color.Empty;

                // 編集を可能とする
                dGV.Rows[mRow].ReadOnly = false;

                dGV[cDay, mRow].Value = t.日付;
                YoubiSet(mRow);

                dGV[cKintai1, mRow].Value = t.出勤区分;
                dGV[cSH, mRow].Value = t.開始時;
                dGV[cSM, mRow].Value = t.開始分;
                dGV[cEH, mRow].Value = t.終了時;
                dGV[cEM, mRow].Value = t.終了分;
                dGV[cKSH, mRow].Value = t.休憩開始時;
                dGV[cKSM, mRow].Value = t.休憩開始分;
                //dGV[cWH, mRow].Value = t.実働時;
                //dGV[cWM, mRow].Value = t.実働分;

                // 訂正チェック
                if (t.訂正 == global.flgOn)
                {
                    dGV[cTeisei, mRow].Value = true;
                }
                else
                {
                    dGV[cTeisei, mRow].Value = false;
                }

                // 明細ＩＤ
                dGV[cID, mRow].Value = t.ID.ToString();

                // 画像名
                dGV[cImg, mRow].Value = t.画像名;
                
                // 行インデックス加算
                mRow++;
            }

            //カレントセル選択状態としない
            dGV.CurrentCell = null;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     画像を表示する </summary>
        /// <param name="pic">
        ///     pictureBoxオブジェクト</param>
        /// <param name="imgName">
        ///     イメージファイルパス</param>
        /// <param name="fX">
        ///     X方向のスケールファクター</param>
        /// <param name="fY">
        ///     Y方向のスケールファクター</param>
        ///------------------------------------------------------------------------------------
        private void ImageGraphicsPaint(PictureBox pic, string imgName, float fX, float fY, int RectDest, int RectSrc)
        {
            Image _img = Image.FromFile(imgName);
            Graphics g = Graphics.FromImage(pic.Image);

            // 各変換設定値のリセット
            g.ResetTransform();

            // X軸とY軸の拡大率の設定
            g.ScaleTransform(fX, fY);

            // 画像を表示する
            g.DrawImage(_img, RectDest, RectSrc);

            // 現在の倍率,座標を保持する
            global.ZOOM_NOW = fX;
            global.RECTD_NOW = RectDest;
            global.RECTS_NOW = RectSrc;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     フォーム表示初期化 </summary>
        /// <param name="sID">
        ///     過去データ表示時のヘッダID</param>
        /// <param name="cIx">
        ///     確定勤務票ヘッダカレントレコードインデックス</param>
        ///------------------------------------------------------------------------------------
        private void formInitialize(int cIx)
        {
            txtNo.MaxLength = 6;

            // テキストボックス表示色設定
            txtYear.BackColor = Color.White;
            txtMonth.BackColor = Color.White;
            txtNo.BackColor = Color.White;

            txtYear.ForeColor = Color.Navy;
            txtMonth.ForeColor = Color.Navy;
            txtNo.ForeColor = Color.Navy;

            // 社員情報表示欄
            lblName.Text = string.Empty;
            lblFuri.Text = string.Empty;
            lblSyoubi.Text = string.Empty;
            lblWdays.Text = string.Empty;

            lblNoImage.Visible = false;

            // 勤務票データ編集のとき
            // ヘッダ情報
            txtYear.ReadOnly = false;
            txtMonth.ReadOnly = false;
            txtNo.ReadOnly = false;
                
            // スクロールバー設定
            hScrollBar1.Enabled = true;
            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = dts.確定勤務票ヘッダ.Count - 1;
            hScrollBar1.Value = cIx;
            hScrollBar1.LargeChange = 1;
            hScrollBar1.SmallChange = 1;

            //移動ボタン制御
            btnFirst.Enabled = true;
            btnNext.Enabled = true;
            btnBefore.Enabled = true;
            btnEnd.Enabled = true;

            //最初のレコード
            if (cIx == 0)
            {
                btnBefore.Enabled = false;
                btnFirst.Enabled = false;
            }

            //最終レコード
            if ((cIx + 1) == dts.確定勤務票ヘッダ.Count)
            {
                btnNext.Enabled = false;
                btnEnd.Enabled = false;
            }

            // その他のボタンを有効とする
            btnErrCheck.Visible = true;
            btnDataMake.Visible = true;
            btnDel.Visible = true;

            ////エラー情報表示
            //ErrShow();

            //データ数表示
            lblPage.Text = " (" + (cI + 1).ToString() + "/" + dts.確定勤務票ヘッダ.Count.ToString() + ")";
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     エラー表示 </summary>
        /// <param name="ocr">
        ///     OCRDATAクラス</param>
        ///------------------------------------------------------------------------------------
        private void ErrShow(OCRData ocr)
        {
            if (ocr._errNumber != ocr.eNothing)
            {
                // グリッドビューCellEnterイベント処理は実行しない
                gridViewCellEnterStatus = false;

                lblErrMsg.Visible = true;
                lblErrMsg.Text = ocr._errMsg;

                // 対象年月
                if (ocr._errNumber == ocr.eYearMonth)
                {
                    txtYear.BackColor = Color.Yellow;
                    txtMonth.BackColor = Color.Yellow;
                    txtYear.Focus();
                }

                // 対象月
                if (ocr._errNumber == ocr.eMonth)
                {
                    txtMonth.BackColor = Color.Yellow;
                    txtMonth.Focus();
                }

                // 個人番号
                if (ocr._errNumber == ocr.eShainNo)
                {
                    txtNo.BackColor = Color.Yellow;
                    txtNo.Focus();
                }

                // 日
                if (ocr._errNumber == ocr.eDay)
                {
                    dGV[cDay, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cDay, ocr._errRow];
                }

                // 勤怠記号
                if (ocr._errNumber == ocr.eKintaiKigou)
                {
                    dGV[cKintai1, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cKintai1, ocr._errRow];
                }

                // 週間勤怠記号
                if (ocr._errNumber == ocr.eWeek)
                {
                    int rCnt = 0;
                    for (int i = ocr._errRow; rCnt < 7; i--)
                    {                        
                        dGV[cKintai1, i].Style.BackColor = Color.Yellow;

                        if (i == 0)
                        {
                            break;
                        }

                        rCnt++;
                    }
                }

                // 開始時
                if (ocr._errNumber == ocr.eSH)
                {
                    dGV[cSH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cSH, ocr._errRow];
                }

                // 開始分
                if (ocr._errNumber == ocr.eSM)
                {
                    dGV[cSM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cSM, ocr._errRow];
                }

                // 終了時
                if (ocr._errNumber == ocr.eEH)
                {
                    dGV[cEH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cEH, ocr._errRow];
                }

                // 終了分
                if (ocr._errNumber == ocr.eEM)
                {
                    dGV[cEM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cEM, ocr._errRow];
                }

                // 休憩開始時
                if (ocr._errNumber == ocr.eKSH)
                {
                    dGV[cKSH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cKSH, ocr._errRow];
                }

                // 休憩開始分
                if (ocr._errNumber == ocr.eKSM)
                {
                    dGV[cKSM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cKSM, ocr._errRow];
                }

                // 実労働時間・時
                if (ocr._errNumber == ocr.eWH)
                {
                    dGV[cWH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cWH, ocr._errRow];
                }

                // 実労働時間・分
                if (ocr._errNumber == ocr.eWM)
                {
                    dGV[cWM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cWM, ocr._errRow];
                }
                                
                // グリッドビューCellEnterイベントステータスを戻す
                gridViewCellEnterStatus = true;

            }
        }
    }
}
