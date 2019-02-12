using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using JAFA_DATA.Common;

namespace JAFA_DATA.OCR
{
    partial class frmPastData
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
        private void showOcrData(int sYY, int sMM, int sID)
        {
            // ビュー読み込み
            //khAdp.Fill(dts.確定ヘッダ);
            //kAdp.Fill(dts.確定データ);

            // 過去勤務票ヘッダテーブル行を取得
            JAFA_OCRDataSet.過去勤務票ヘッダRow r = (JAFA_OCRDataSet.過去勤務票ヘッダRow)dts.過去勤務票ヘッダ.Single(a => a.年 == _sYY && a.月 == _sMM && a.社員番号 == _sID);

            // フォーム初期化
            formInitialize();

            /*
            * ヘッダ情報表示
            */
            // 年月
            txtYear.Text = Utility.EmptytoZero(r.年.ToString());
            txtMonth.Text = Utility.EmptytoZero(r.月.ToString());

            // 交通費等の入力を不可とする ← 可とする 2018/11/11
            //txtKoutsuuhi.ReadOnly = true;
            //txtNittou.ReadOnly = true;
            //txtShukuhakuhi.ReadOnly = true;

            global.ChangeValueStatus = false;   // チェンジバリューステータス
            txtNo.Text = string.Empty;
            global.ChangeValueStatus = true;    // チェンジバリューステータス

            txtNo.Text = Utility.EmptytoZero(r.社員番号.ToString());        // 社員番号
            txtKoutsuuhi.Text = Utility.EmptytoZero(r.交通費.ToString("#,0"));   // 交通費
            txtNittou.Text = Utility.EmptytoZero(r.日当.ToString("#,0"));        // 日当
            txtShukuhakuhi.Text = Utility.EmptytoZero(r.宿泊費.ToString("#,0")); // 宿泊費

            lblSzCode.Text = r.所属コード;   // 2015/07/08
            lblSzName.Text = r.所属名;     // 2015/07/08

            // 過去データ表示
            showItem(r.ヘッダID, dGV);

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
            adpM.FillByHID(dts.過去勤務票明細, hID);   // 2018/11/11
            var h = dts.過去勤務票明細.Where(a => a.ヘッダID == hID).OrderBy(a => a.日付);

            foreach (var t in h)
            {
                /*
                 * 出勤簿明細表示
                 */

                // 画像表示
                if (mRow == 0)
                {
                    //ShowImage(global.pblImagePath + t.画像名.ToString());

                    // openCV版：2018/11/11
                    showImage_openCv(global.pblImagePath + t.画像名.ToString());
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

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     JAメイトOCRデータ表示 </summary>
        /// <param name="sYY">
        ///     年 </param>
        /// <param name="sMM">
        ///     月 </param>
        /// <param name="sID">
        ///     職員番号 </param>
        /// -------------------------------------------------------------------------
        private void showJaMateData(int sYY, int sMM, int sID)
        {
            int yymm = sYY * 100 + sMM;
            int shainID = sID;


            adpJ.FillBySCodeDateSpan(dts.勤怠データ, sID, yymm, yymm);    // 2018/11/11
            foreach (var t in dts.勤怠データ.Where(a => a.対象月度 == yymm && a.対象職員コード == shainID))
            {
                dgvJa[cja, 0].Value = t.普通出勤日数;
                dgvJa[cja, 1].Value = getHourMinute(t.実労働時間);


                // 残業時間はBIG給与計算Pro勤怠データから取得 2019/02/12
                adpB.FillByYYMMSCode(dts.BIG給与計算Pro勤怠データ, sYY, sMM, sID);
                foreach (var m in dts.BIG給与計算Pro勤怠データ)
                {
                    dgvJa[cja, 2].Value = getHourMinute(m.普通残業時間);
                }

                //dgvJa[cja, 2].Value = getHourMinute(t.残業時間);

                dgvJa[cja, 3].Value = getHourMinute(t.深夜時間);

                dgvJa[cja, 4].Value = t.遅刻回数;           // 休日出勤日数挿入 2019/02/12
                dgvJa[cja, 5].Value = t.法定休日出勤日数;    // 以下、1行ずらす 2019/02/12
                dgvJa[cja, 6].Value = t.休日日数;
                dgvJa[cja, 7].Value = t.振替休日日数;
                //dgvJa[cja, 7].Value = t.有給半日;
                dgvJa[cja, 8].Value = t.有給半日 / 0.5;
                dgvJa[cja, 9].Value = t.有給休暇;
                dgvJa[cja, 10].Value = t.欠勤日数;
                dgvJa[cja, 11].Value = t.その他休暇休職合計日数;
                dgvJa[cja, 12].Value = t.結婚休暇日数;
                dgvJa[cja, 13].Value = t.忌引休暇日数;
                dgvJa[cja, 14].Value = t.生理休暇日数;
                dgvJa[cja, 15].Value = t.看護休暇日数;
                dgvJa[cja, 16].Value = t.介護休暇日数;
                dgvJa[cja, 17].Value = t.罹災休暇日数;
                dgvJa[cja, 18].Value = t.隔離休暇日数;
                dgvJa[cja, 19].Value = t.その他の特別休暇日数;
                dgvJa[cja, 20].Value = t.介護休職日数;
                dgvJa[cja, 21].Value = t.産前産後休暇日数;
                dgvJa[cja, 22].Value = t.育児休職日数;
                dgvJa[cja, 23].Value = t.要出勤日数;
                dgvJa[cja, 24].Value = t.有休付与日数;
                dgvJa[cja, 25].Value = t.有休繰越日数;
            }

            dgvJa.CurrentCell = null;
        }

        /// ------------------------------------------------------------------
        /// <summary>
        ///     値から時分標記に変換する </summary>
        /// <param name="sVal">
        ///     数値（分を表す）</param>
        /// <returns>
        ///     文字列：xx時間xx分</returns>
        /// ------------------------------------------------------------------
        private string getHourMinute(int sVal)
        {
            string res = string.Empty;

            int hh = sVal / 60;
            int mm = sVal % 60;

            res = hh.ToString() + ":" + mm.ToString().PadLeft(2, '0');

            return res;
        }
        

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     フォーム表示初期化 </summary>
        /// <param name="sID">
        ///     過去データ表示時のヘッダID</param>
        /// <param name="cIx">
        ///     確定勤務票ヘッダカレントレコードインデックス</param>
        ///------------------------------------------------------------------------------------
        private void formInitialize()
        {
            txtNo.MaxLength = 6;

            // テキストボックス表示色設定
            txtYear.BackColor = Color.White;
            txtMonth.BackColor = Color.White;
            txtNo.BackColor = Color.White;
            txtKoutsuuhi.BackColor = Color.White;
            txtShukuhakuhi.BackColor = Color.White;
            txtNittou.BackColor = Color.White;

            txtYear.ForeColor = Color.Navy;
            txtMonth.ForeColor = Color.Navy;
            txtNo.ForeColor = Color.Navy;

            // 社員情報表示欄
            lblName.Text = string.Empty;
            //lblFuri.Text = string.Empty;
            //lblSyoubi.Text = string.Empty;    // 2018/11/11 コメント化
            lblWdays.Text = string.Empty;

            lblNoImage.Visible = false;

            // ヘッダ情報
            txtYear.ReadOnly = true;
            txtMonth.ReadOnly = true;
            txtNo.ReadOnly = true;

            // 2018/11/11
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 20;
            trackBar1.Value = 0;

            trackBar1.SmallChange = 1;
            trackBar1.LargeChange = 10;
        }
    }
}
