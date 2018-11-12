using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using JAFA_DATA.Common;
using Excel = Microsoft.Office.Interop.Excel;
using OpenCvSharp;

namespace JAFA_DATA.OCR
{
	public partial class frmPastData : Form
	{
		/// ------------------------------------------------------------
		/// <summary>
		///     コンストラクタ </summary>
		/// <param name="sID">
		///     処理モード</param>
		/// ------------------------------------------------------------
		public frmPastData(int sYY, int sMM, int sID)
		{
			InitializeComponent();

			_sYY = sYY;
			_sMM = sMM;
			_sID = sID;

			// 画像パス取得
			global.pblImagePath = Properties.Settings.Default.tifPath;

			// データを読み込む
			//adpH.Fill(dts.過去勤務票ヘッダ);  // 2018/11/11 コメント化
			//adpM.Fill(dts.過去勤務票明細);   // 2018/11/11 コメント化

			adpH.FillByYYMMSCode(dts.過去勤務票ヘッダ, sYY, sMM, sID);

			sAdp.Fill(dts.出勤区分);

			//adpJ.Fill(dts.勤怠データ);  // 2018/11/11 コメント化
		}

		// openCvSharp 関連　2018/10/26
		const float B_WIDTH = 0.35f;
		const float B_HEIGHT = 0.35f;
		const float A_WIDTH = 0.05f;
		const float A_HEIGHT = 0.05f;

		float n_width = 0f;
		float n_height = 0f;

		Mat mMat = new Mat();

		int _sYY = 0;
		int _sMM = 0;
		int _sID = 0;

		// データアダプターオブジェクト
		JAFA_OCRDataSetTableAdapters.TableAdapterManager adpMn = new JAFA_OCRDataSetTableAdapters.TableAdapterManager();
		JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter adpH = new JAFA_OCRDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
		JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter adpM = new JAFA_OCRDataSetTableAdapters.過去勤務票明細TableAdapter();
		JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter sAdp = new JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter();
		JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter adpJ = new JAFA_OCRDataSetTableAdapters.勤怠データTableAdapter();
   
		// データセットオブジェクト
		JAFA_OCRDataSet dts = new JAFA_OCRDataSet();
		
		/// <summary>
		///     カレントデータRowsインデックス</summary>
		int cI = 0;

		// 社員マスターより取得した所属コード
		string mSzCode = string.Empty;

		#region 終了ステータス定数
		const string END_BUTTON = "btn";
		const string END_MAKEDATA = "data";
		const string END_CONTOROL = "close";
		const string END_NODATA = "non Data";
		#endregion

		string sDBNM = string.Empty;                // データベース名

		string _PCADBName = string.Empty;           // 会社領域データベース識別番号
		string _PCAComNo = string.Empty;            // 会社番号
		string _PCAComName = string.Empty;          // 会社名

		// dataGridView1_CellEnterステータス
		bool gridViewCellEnterStatus = true;

		private void frmCorrect_Load(object sender, EventArgs e)
		{
			this.pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

			// フォーム最大値
			Utility.WindowsMaxSize(this, this.Width, this.Height);

			// フォーム最小値
			Utility.WindowsMinSize(this, this.Width, this.Height);

			////元号を取得 2018/11/11 コメント化
			//label1.Text = Properties.Settings.Default.gengou;
			
			// データテーブル件数カウント
			if (dts.過去勤務票ヘッダ.Count == 0)
			{
				MessageBox.Show("対象となる過去データがありません", "過去出勤簿データ表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				//終了処理
				Environment.Exit(0);
			}

			// キャプション
			this.Text = "過去出勤簿データ表示";

			// グリッドビュー定義
			GridviewSet gs = new GridviewSet();
			gs.Setting_Shain(dGV);
			gs.Setting_JAOcrMate(dgvJa);

			// 画面表示
			showOcrData(_sYY, _sMM, _sID);
			showJaMateData(_sYY, _sMM, _sID);
			
			// tagを初期化
			this.Tag = string.Empty;
		}

		#region データグリッドビューカラム定義
		private static string cDay = "col1";        // 日付
		private static string cWeek = "col2";       // 曜日
		private static string cKintai1 = "col3";    // 勤怠記号
		private static string cKName = "col22";     // 出勤区分名称
		private static string cSH = "col4";         // 開始時
		private static string cSE = "col5";
		private static string cSM = "col6";         // 開始分
		private static string cEH = "col7";         // 終了時
		private static string cEE = "col8";
		private static string cEM = "col9";         // 終了分
		private static string cKSH = "col10";       // 休憩開始時
		private static string cKSE = "col11";
		private static string cKSM = "col12";       // 休憩開始分
		private static string cWH = "col16";        // 実労働時間時
		private static string cWE = "col17";
		private static string cWM = "col18";        // 実労働時間分
		private static string cTeisei = "col21";    // 訂正
		private static string cID = "colID";
		private static string cImg = "colImg";      // 画像名
		private static string cjaH = "cHaed";       // 集計タイトル
		private static string cja = "cData";        // 集計データ
		#endregion

		///----------------------------------------------------------------------------
		/// <summary>
		///     データグリッドビュークラス </summary>
		///----------------------------------------------------------------------------
		private class GridviewSet
		{
			///----------------------------------------------------------------------------
			/// <summary>
			///     データグリッドビューの定義を行います</summary> 
			/// <param name="gv">
			///     データグリッドビューオブジェクト</param>
			///----------------------------------------------------------------------------
			public void Setting_Shain(DataGridView gv)
			{
				try
				{
					// データグリッドビューの基本設定
					setGridView_Properties(gv);

					// Tagをセット
					//gv.Tag = global.SHAIN_ID;

					// カラムコレクションを空にします
					gv.Columns.Clear();

					// 行数をクリア            
					gv.Rows.Clear();                                       

					//各列幅指定
					gv.Columns.Add(cDay, "日");
					gv.Columns.Add(cWeek, "曜");
					gv.Columns.Add(cKintai1, "区分");
					gv.Columns.Add(cKName, "出勤区分");
					gv.Columns.Add(cSH, "始");
					gv.Columns.Add(cSE, "");
					gv.Columns.Add(cSM, "業");
					gv.Columns.Add(cEH, "終");
					gv.Columns.Add(cEE, "");
					gv.Columns.Add(cEM, "業");
					gv.Columns.Add(cKSH, "休");
					gv.Columns.Add(cKSE, "");
					gv.Columns.Add(cKSM, "憩");
					gv.Columns.Add(cWH, "実");
					gv.Columns.Add(cWE, "");
					gv.Columns.Add(cWM, "労");

					DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
					gv.Columns.Add(column);
					gv.Columns[16].Name = cTeisei;
					gv.Columns[16].HeaderText = "訂正";

					gv.Columns.Add(cID, "");   // 明細ID
					gv.Columns[cID].Visible = false;

					gv.Columns.Add(cImg, "");   // 画像名
					gv.Columns[cImg].Visible = false;

					// 出勤区分は非表示
					//gv.Columns[cKintai1].Visible = false;

					//gv.ReadOnly = true;

					foreach (DataGridViewColumn c in gv.Columns)
					{
						// 幅                       
						if (c.Name == cSE || c.Name == cEE || c.Name == cKSE || c.Name == cWE)
						{
							c.Width = 10;
						}
						else if (c.Name == cTeisei)
						{
							c.Width = 40;
						}
						else
						{
							c.Width = 28;
						}

						gv.Columns[cKName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				 
						// 表示位置
						if (c.Index < 3 || c.Name == cSE || c.Name == cEE || c.Name == cKSE || 
							c.Name == cWE || c.Name == cTeisei)
							c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
						else c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

						if (c.Name == cSH || c.Name == cEH || c.Name == cKSH || c.Name == cWH) 
							c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

						if (c.Name == cKName || c.Name == cSM || c.Name == cEM || c.Name == cKSM || c.Name == cWM) 
							c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;

						// 編集可否
						if (c.Index < 2 || c.Name == cSE || c.Name == cEE || c.Name == cKSE || c.Name == cWE)
							c.ReadOnly = true;
						else c.ReadOnly = false;

						// 区切り文字
						if (c.Name == cSE || c.Name == cEE || c.Name == cKSE || c.Name == cWE)
							c.DefaultCellStyle.Font = new Font("ＭＳＰゴシック", 8, FontStyle.Regular);

						// 入力可能桁数
						if (c.Name == cKSH)
						{
							DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)c;
							col.MaxInputLength = 1;
						}
						else if (c.Name != cTeisei)
						{
							DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)c;
							col.MaxInputLength = 2;
						}

						// ソート禁止
						c.SortMode = DataGridViewColumnSortMode.NotSortable;
					}
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			///----------------------------------------------------------------------------
			/// <summary>
			///     データグリッドビューの定義を行います</summary> 
			/// <param name="gv">
			///     データグリッドビューオブジェクト</param>
			///----------------------------------------------------------------------------
			public void Setting_JAOcrMate(DataGridView gv)
			{
				try
				{
					// 列スタイルを変更する
					gv.EnableHeadersVisualStyles = false;

					// 列ヘッダー表示位置指定
					gv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

					// 列ヘッダーフォント指定
					gv.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

					// データフォント指定
					gv.DefaultCellStyle.Font = new Font("Meiryo UI", (Single)9, FontStyle.Regular);
					//gv.DefaultCellStyle.Font = new Font("ＭＳＰゴシック", (Single)10, FontStyle.Regular);

					// 行の高さ
					gv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
					gv.ColumnHeadersHeight = 18;
					gv.RowTemplate.Height = 18;

					// 全体の高さ
					gv.Height = 578;

					// 全体の幅
					gv.Width = 142;

					// 奇数行の色
					//gv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

					//テキストカラーの設定
					//gv.RowsDefaultCellStyle.ForeColor = Color.Black;
					//gv.DefaultCellStyle.SelectionBackColor = Color.Empty;
					//gv.DefaultCellStyle.SelectionForeColor = Color.Navy;

					// 行ヘッダを表示しない
					gv.RowHeadersVisible = false;

					// 選択モード
					gv.SelectionMode = DataGridViewSelectionMode.CellSelect;
					gv.MultiSelect = false;

					// データグリッドビュー編集可
					gv.ReadOnly = false;

					// 追加行表示しない
					gv.AllowUserToAddRows = false;

					// データグリッドビューから行削除を禁止する
					gv.AllowUserToDeleteRows = false;

					// 手動による列移動の禁止
					gv.AllowUserToOrderColumns = false;

					// 列サイズ変更不可
					gv.AllowUserToResizeColumns = false;

					// 行サイズ変更禁止
					gv.AllowUserToResizeRows = false;

					// 行ヘッダーの自動調節
					//gv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

					//TAB動作
					gv.StandardTab = false;

					// 編集モード
					gv.EditMode = DataGridViewEditMode.EditOnEnter;

					// Tagをセット
					//gv.Tag = global.SHAIN_ID;

					// カラムコレクションを空にします
					gv.Columns.Clear();

					//各列幅指定
					gv.Columns.Add(cjaH, "");
					gv.Columns.Add(cja, "");
					gv.Columns[0].Width = 90;
					gv.Columns[0].DefaultCellStyle.BackColor = SystemColors.ButtonFace;

					gv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					gv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					gv.Columns[1].DefaultCellStyle.ForeColor = Color.Navy;
					gv.Columns[1].DefaultCellStyle.Font = new Font("Meiryo UI", (Single)9, FontStyle.Regular);


					// 行数を設定          
					gv.Rows.Add(31);

					// 行ヘッダ
					gv[0, 0].Value = "普通出勤";
					gv[0, 1].Value = "実労働時間";
					gv[0, 2].Value = "残業時間";
					gv[0, 3].Value = "深夜時間";
					gv[0, 4].Value = "法定休日出勤";
					gv[0, 5].Value = "休日";
					gv[0, 6].Value = "振替休日";
					gv[0, 7].Value = "有休半日・回";
					gv[0, 8].Value = "有休・回";
					gv[0, 9].Value = "欠勤";
					gv[0, 10].Value = "その他休暇合計";
					gv[0, 11].Value = "結婚休暇";
					gv[0, 12].Value = "忌引休暇";
					gv[0, 13].Value = "生理休暇";
					gv[0, 14].Value = "看護休暇";
					gv[0, 15].Value = "介護休暇";
					gv[0, 16].Value = "罹災休暇";
					gv[0, 17].Value = "隔離休暇";
					gv[0, 18].Value = "他特休";
					gv[0, 19].Value = "介護休職";
					gv[0, 20].Value = "産前産後";
					gv[0, 21].Value = "育児休職";
					gv[0, 22].Value = "要出勤日数";
					gv[0, 23].Value = "有休付与";
					gv[0, 24].Value = "有休繰越";

					// 編集の可否 : データ欄は編集可能とする
					//gv.ReadOnly = true;

					foreach (DataGridViewColumn  c in gv.Columns)
					{
						if (c.Name == cja)
						{
							c.ReadOnly = false;

							// 入力可能文字桁数
							DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)c;
							col.MaxInputLength = 2;
						}
						else
						{
							c.ReadOnly = true;
						}
					}
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			///----------------------------------------------------------------------------
			/// <summary>
			///     データグリッドビュー基本設定</summary>
			/// <param name="gv">
			///     データグリッドビューオブジェクト</param>
			///----------------------------------------------------------------------------
			private void setGridView_Properties(DataGridView gv)
			{
				// 列スタイルを変更する
				gv.EnableHeadersVisualStyles = false;

				// 列ヘッダー表示位置指定
				gv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

				// 列ヘッダーフォント指定
				gv.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

				// データフォント指定
				gv.DefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);
				//gv.DefaultCellStyle.Font = new Font("ＭＳＰゴシック", (Single)10, FontStyle.Regular);

				// 行の高さ
				gv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
				gv.ColumnHeadersHeight = 18;
				gv.RowTemplate.Height = 18;

				// 全体の高さ
				gv.Height = 578;

				// 全体の幅
				gv.Width = 455;

				// 奇数行の色
				//gv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

				//テキストカラーの設定
				gv.RowsDefaultCellStyle.ForeColor = Color.Navy;       
				gv.DefaultCellStyle.SelectionBackColor = Color.Empty;
				gv.DefaultCellStyle.SelectionForeColor = Color.Navy;

				// 行ヘッダを表示しない
				gv.RowHeadersVisible = false;

				// 選択モード
				gv.SelectionMode = DataGridViewSelectionMode.CellSelect;
				gv.MultiSelect = false;

				// データグリッドビュー編集可
				gv.ReadOnly = false;

				// 追加行表示しない
				gv.AllowUserToAddRows = false;

				// データグリッドビューから行削除を禁止する
				gv.AllowUserToDeleteRows = false;

				// 手動による列移動の禁止
				gv.AllowUserToOrderColumns = false;

				// 列サイズ変更不可
				gv.AllowUserToResizeColumns = false;

				// 行サイズ変更禁止
				gv.AllowUserToResizeRows = false;

				// 行ヘッダーの自動調節
				//gv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

				//TAB動作
				gv.StandardTab = false;

				// 編集モード
				gv.EditMode = DataGridViewEditMode.EditOnEnter;
			}
		}

		private void txtNo_TextChanged(object sender, EventArgs e)
		{
			// チェンジバリューステータス
			if (!global.ChangeValueStatus) return;

			// 社員番号のとき
			lblName.Text = string.Empty;

			if (txtNo.Text != string.Empty)
			{
				string[] sName = new string[6];

				clsGetMst ms = new clsGetMst();
				sName = ms.getKojinMst(Utility.StrtoInt(txtNo.Text));
				lblName.Text = sName[0];
				//lblFuri.Text = sName[1];
				//lblSyoubi.Text = sName[4];　// 2018/11/11 コメント化
				lblWdays.Text = sName[5];
			}
		}


		private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				//イベントハンドラが複数回追加されてしまうので最初に削除する
				e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
				//e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress2);

				//イベントハンドラを追加する
				e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
			}
		}

		void Control_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '\t' &&
				 e.KeyChar != ':' && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		void Control_KeyPress2(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
				e.KeyChar == '\b' || e.KeyChar == '\t')
				e.Handled = false;
			else e.Handled = true;
		}

		/// -------------------------------------------------------------------
		/// <summary>
		///     曜日をセットする </summary>
		/// <param name="tempRow">
		///     MultiRowのindex</param>
		/// -------------------------------------------------------------------
		private void YoubiSet(int tempRow)
		{
			string sDate;
			DateTime eDate;
			Boolean bYear = false;
			Boolean bMonth = false;

			//年月を確認
			if (txtYear.Text != string.Empty)
			{
				if (Utility.NumericCheck(txtYear.Text))
				{
					if (int.Parse(txtYear.Text) > 0)
					{
						bYear = true;
					}
				}
			}

			if (txtMonth.Text != string.Empty)
			{
				if (Utility.NumericCheck(txtMonth.Text))
				{
					if (int.Parse(txtMonth.Text) >= 1 && int.Parse(txtMonth.Text) <= 12)
					{
						for (int i = 0; i < global._MonthMULTIGYO; i++)
						{
							bMonth = true;
						}
					}
				}
			}

			//年月の値がfalseのときは曜日セットは行わずに終了する
			if (bYear == false || bMonth == false) return;

			//行の色を初期化
			dGV.Rows[tempRow].DefaultCellStyle.BackColor = Color.Empty;

			//Nullか？
			dGV[cWeek, tempRow].Value = string.Empty;
			if (dGV[cDay, tempRow].Value != null) 
			{
				if (dGV[cDay, tempRow].Value.ToString() != string.Empty)
				{
					if (Utility.NumericCheck(dGV[cDay, tempRow].Value.ToString()))
					{
						{
							sDate = Utility.StrtoInt(txtYear.Text).ToString() + "/" + 
									Utility.EmptytoZero(txtMonth.Text) + "/" +
									Utility.EmptytoZero(dGV[cDay, tempRow].Value.ToString());
							
							// 存在する日付と認識された場合、曜日を表示する
							if (DateTime.TryParse(sDate, out eDate))
							{
								dGV[cWeek, tempRow].Value = ("日月火水木金土").Substring(int.Parse(eDate.DayOfWeek.ToString("d")), 1);

								// 休日背景色設定・日曜日
								if (dGV[cWeek, tempRow].Value.ToString() == "日")
									dGV.Rows[tempRow].DefaultCellStyle.BackColor = Color.MistyRose;

								// 時刻区切り文字
								dGV[cSE, tempRow].Value = ":";
								dGV[cEE, tempRow].Value = ":";
								dGV[cKSE, tempRow].Value = ":";
								dGV[cWE, tempRow].Value = ":";
							}
						}
					}
				}
			 }
		}

		private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (!global.ChangeValueStatus) return;

			if (e.RowIndex < 0) return;

			string colName = dGV.Columns[e.ColumnIndex].Name;

			// 日付
			if (colName == cDay)
			{
				// 曜日を表示します
				YoubiSet(e.RowIndex);
			}
			
			// 出勤日数
			//txtShukkinTl.Text = getWorkDays(_YakushokuType);

			//// 休日チェック
			//if (colName == cKyuka || colName == cCheck)
			//{
			//    // 休日行背景色設定
			//    if (dGV[cKyuka, e.RowIndex].Value.ToString() == "True")
			//        dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
			//    else dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
			//}

			// 勤怠記号
			if (colName == cKintai1)
			{
				global.ChangeValueStatus = false;

                if (dGV[cKintai1, e.RowIndex].Value != null)
                {
                    if (dts.出勤区分.Any(a => a.ID == Utility.StrtoInt(dGV[cKintai1, e.RowIndex].Value.ToString())))
                    {
                        JAFA_OCRDataSet.出勤区分Row r = dts.出勤区分.Single(a => a.ID == Utility.StrtoInt(dGV[cKintai1, e.RowIndex].Value.ToString()));
                        dGV[cKName, e.RowIndex].Value = r.名称;
                    }
                    else
                    {
                        dGV[cKName, e.RowIndex].Value = string.Empty;
                    }
                }
                else
                {
                    dGV[cKName, e.RowIndex].Value = string.Empty;
                }

                global.ChangeValueStatus = true;
			}

			// 出勤時刻、退勤時刻
			if (colName == cSH || colName == cSM || colName == cEH || colName == cEM || 
				colName == cKSH || colName == cKSM)
			{
				// 実働時間を計算して表示する
				if (dGV[cSH, e.RowIndex].Value != null && dGV[cSM, e.RowIndex].Value != null &&
					dGV[cEH, e.RowIndex].Value != null && dGV[cEM, e.RowIndex].Value != null)
				{
					if (dGV[cSH, e.RowIndex].Value.ToString() != string.Empty &&
						dGV[cSM, e.RowIndex].Value.ToString() != string.Empty &&
						dGV[cEH, e.RowIndex].Value.ToString() != string.Empty &&
						dGV[cEM, e.RowIndex].Value.ToString() != string.Empty)
					{
						// 出勤時刻、退勤時刻、休憩時間から実働時間を取得する
						OCRData ocr = new OCRData();
						double wTime = ocr.getWorkTime(dGV[cSH, e.RowIndex].Value.ToString(),
							dGV[cSM, e.RowIndex].Value.ToString(), 
							dGV[cEH, e.RowIndex].Value.ToString(), 
							dGV[cEM, e.RowIndex].Value.ToString(),
							Utility.NulltoStr(dGV[cKSH, e.RowIndex].Value),
							Utility.NulltoStr(dGV[cKSM, e.RowIndex].Value));

						// 実働時間
						if (wTime >= 0)
						{
							double wTimeH = Math.Floor(wTime / 60);
							double wTimeM = wTime % 60;

							// ChangeValueイベントを発生させない
							global.ChangeValueStatus = false;
							dGV[cWH, e.RowIndex].Value = wTimeH.ToString();
							dGV[cWM, e.RowIndex].Value = wTimeM.ToString().PadLeft(2, '0');
							global.ChangeValueStatus = true;
						}
						else
						{
							// ChangeValueイベントを発生させない
							dGV[cWH, e.RowIndex].Value = string.Empty;
							dGV[cWM, e.RowIndex].Value = string.Empty;
							global.ChangeValueStatus = true;
						}
					}
					else
					{
						// ChangeValueイベントを発生させない
						global.ChangeValueStatus = false;
						dGV[cWH, e.RowIndex].Value = string.Empty;
						dGV[cWM, e.RowIndex].Value = string.Empty;
						global.ChangeValueStatus = true;
					}
				}
				else
				{
					// ChangeValueイベントを発生させない
					global.ChangeValueStatus = false;
					dGV[cWH, e.RowIndex].Value = string.Empty;
					dGV[cWM, e.RowIndex].Value = string.Empty;
					global.ChangeValueStatus = true;
				}
			}

			// 訂正チェック
			if (colName == cTeisei)
			{
				if (dGV[cTeisei, e.RowIndex].Value.ToString() == "True")
				{
					dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;
				}
				else
				{
					// 曜日を表示します（日曜日は色表示のため）
					YoubiSet(e.RowIndex);
				}
			}
		}
		
		/// ----------------------------------------------------------------------------------------------------
		/// <summary>
		///     空白以外のとき、指定された文字数になるまで左側に０を埋めこみ、右寄せした文字列を返す
		/// </summary>
		/// <param name="tm">
		///     文字列</param>
		/// <param name="len">
		///     文字列の長さ</param>
		/// <returns>
		///     文字列</returns>
		/// ----------------------------------------------------------------------------------------------------
		private string timeVal(object tm, int len)
		{
			string t = Utility.NulltoStr(tm);
			if (t != string.Empty) return t.PadLeft(len, '0');
			else return t;
		}

		/// ----------------------------------------------------------------------------------------------------
		/// <summary>
		///     空白以外のとき、先頭文字が０のとき先頭文字を削除した文字列を返す　
		///     先頭文字が０以外のときはそのまま返す
		/// </summary>
		/// <param name="tm">
		///     文字列</param>
		/// <returns>
		///     文字列</returns>
		/// ----------------------------------------------------------------------------------------------------
		private string timeValH(object tm)
		{
			string t = Utility.NulltoStr(tm);

			if (t != string.Empty)
			{
				t = t.PadLeft(2, '0');
				if (t.Substring(0, 1) == "0")
				{
					t = t.Substring(1, 1);
				}
			}

			return t;
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///     Bool値を数値に変換する </summary>
		/// <param name="b">
		///     True or False</param>
		/// <returns>
		///     true:1, false:0</returns>
		/// ------------------------------------------------------------------------------------
		private int booltoFlg(string b)
		{
			if (b == "True") return global.flgOn;
			else return global.flgOff;
		}


		private void btnRtn_Click(object sender, EventArgs e)
		{
			// エラーチェック
			//if (!errCheck())
			//{
			//    return;
			//}


			// フォームを閉じる
			this.Tag = END_BUTTON;
			this.Close();
		}

		private void frmCorrect_FormClosing(object sender, FormClosingEventArgs e)
		{
			int yy = Utility.StrtoInt(txtYear.Text);
			int mm = Utility.StrtoInt(txtMonth.Text);
			int sNum = Utility.StrtoInt(txtNo.Text);

			// 過去勤怠ヘッダデータ、明細データ更新
			curDataUpDate();

            // 勤怠データ更新
			jaOCRDataUpdate(yy, mm, sNum);

			// 解放する
			this.Dispose();
		}

		/// ----------------------------------------------------------------------------
		/// <summary>
		///     過去勤怠データ更新  </summary>
		/// <param name="sYY">
		///     年（西暦） </param>
		/// <param name="sMM">
		///     月 </param>
		/// <param name="sID">
		///     職員コード</param>
		/// ----------------------------------------------------------------------------
		private void jaOCRDataUpdate(int sYY, int sMM, int sID)
		{            
			string yymm = sYY.ToString() + sMM.ToString().PadLeft(2, '0');
			string shainID = sID.ToString();
			
			JAFA_OCRDataSet.勤怠データRow t = dts.勤怠データ.Single(a => a.対象月度 == yymm && a.対象職員コード == shainID);

			t.普通出勤日数 = Utility.StrtoDouble(Utility.NulltoStr(dgvJa[cja, 0].Value));

			t.実労働時間 = getWorkMinute(Utility.NulltoStr(dgvJa[cja, 1].Value));
			t.残業時間 = getWorkMinute(Utility.NulltoStr(dgvJa[cja, 2].Value));
			t.深夜時間 = getWorkMinute(Utility.NulltoStr(dgvJa[cja, 3].Value));

			t.法定休日出勤日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 4].Value));
			t.休日日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 5].Value));
			t.振替休日日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 6].Value));
			t.有給半日 = Utility.StrtoDouble(Utility.NulltoStr(dgvJa[cja, 7].Value)) * 0.5;
			t.有給休暇 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 8].Value));
			t.欠勤日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 9].Value));
			t.その他休暇休職合計日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 10].Value));
			t.結婚休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 11].Value));
			t.忌引休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 12].Value));
			t.生理休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 13].Value));
			t.看護休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 14].Value));
			t.介護休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 15].Value));
			t.罹災休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 16].Value));
			t.隔離休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 17].Value));
			t.その他の特別休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 18].Value));
			t.介護休職日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 19].Value));
			t.産前産後休暇日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 20].Value));
			t.育児休職日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 21].Value));
			t.要出勤日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 22].Value));
			t.有休付与日数 = Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, 23].Value));
			t.有休繰越日数 = Utility.StrtoDouble(Utility.NulltoStr(dgvJa[cja, 24].Value));

			adpJ.Update(dts.勤怠データ);
		}




		/// ---------------------------------------------------------------------
		/// <summary>
		///     MDBファイルを最適化する </summary>
		/// ---------------------------------------------------------------------
		private void mdbCompact()
		{
			try
			{
				JRO.JetEngine jro = new JRO.JetEngine();
				string OldDb = Properties.Settings.Default.mdbOlePath;
				string NewDb = Properties.Settings.Default.mdbPathTemp;

				jro.CompactDatabase(OldDb, NewDb);

				//今までのバックアップファイルを削除する
				System.IO.File.Delete(Properties.Settings.Default.mdbPath + global.MDBBACK);

				//今までのファイルをバックアップとする
				System.IO.File.Move(Properties.Settings.Default.mdbPath + global.MDBFILE, Properties.Settings.Default.mdbPath + global.MDBBACK);

				//一時ファイルをMDBファイルとする
				System.IO.File.Move(Properties.Settings.Default.mdbPath + global.MDBTEMP, Properties.Settings.Default.mdbPath + global.MDBFILE);
			}
			catch (Exception e)
			{
				MessageBox.Show("MDB最適化中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
			}
		}
		
		private void btnPlus_Click(object sender, EventArgs e)
		{
			// 2018/11/11 コメント化
			//if (leadImg.ScaleFactor < global.ZOOM_MAX)
			//{
			//    leadImg.ScaleFactor += global.ZOOM_STEP;
			//}
			//global.miMdlZoomRate = (float)leadImg.ScaleFactor;
		}

		private void btnMinus_Click(object sender, EventArgs e)
		{
			// 2018/11/11 コメント化
			//if (leadImg.ScaleFactor > global.ZOOM_MIN)
			//{
			//    leadImg.ScaleFactor -= global.ZOOM_STEP;
			//}
			//global.miMdlZoomRate = (float)leadImg.ScaleFactor;
		}

		private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			//if (e.RowIndex < 0) return;

			string colName = dGV.Columns[e.ColumnIndex].Name;

			if (colName == cSH || colName == cSE || colName == cEH || colName == cEE ||
				colName == cKSH || colName == cKSE || colName == cWH || colName == cWE)
			{
				e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
			}
		}

		private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			string colName = dGV.Columns[dGV.CurrentCell.ColumnIndex].Name;

			if (colName == cTeisei)
			{
				if (dGV.IsCurrentCellDirty)
				{
					dGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
					dGV.RefreshEdit();
				}
			}
		}

		private void dataGridView1_CellEnter_1(object sender, DataGridViewCellEventArgs e)
		{
			// エラー表示時には処理を行わない
			if (!gridViewCellEnterStatus) return;
 
			string ColH = string.Empty;
			string ColM = dGV.Columns[dGV.CurrentCell.ColumnIndex].Name;

			// 開始時間または終了時間を判断
			if (ColM == cSM)            // 開始時刻
			{
				ColH = cSH;
			}
			else if (ColM == cEM)       // 終了時刻
			{
				ColH = cEH;
			}
			else if (ColM == cKSM)      // 休憩開始時刻
			{
				ColH = cKSH;
			}
			else
			{
				return;
			}

			// 時が入力済みで分が未入力のとき分に"00"を表示します
			if (dGV[ColH, dGV.CurrentRow.Index].Value != null)
			{
				if (dGV[ColH, dGV.CurrentRow.Index].Value.ToString().Trim() != string.Empty)
				{
					if (dGV[ColM, dGV.CurrentRow.Index].Value == null)
					{
						dGV[ColM, dGV.CurrentRow.Index].Value = "00";
					}
					else if (dGV[ColM, dGV.CurrentRow.Index].Value.ToString().Trim() == string.Empty)
					{
						dGV[ColM, dGV.CurrentRow.Index].Value = "00";
					}
				}
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
			// 2018/11/11 コメント化
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
			//if (File.Exists(tempImgName))
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
			//    //global.pblImagePath = tempImgName;
			//}
			//else
			//{
			//    //画像ファイルがないとき
			//    lblNoImage.Visible = true;

			//    // 画像操作ボタン
			//    btnPlus.Enabled = false;
			//    btnMinus.Enabled = false;

			//    leadImg.Visible = false;
			//    //global.pblImagePath = string.Empty;
			//}
		}

		private void leadImg_MouseLeave(object sender, EventArgs e)
		{
			this.Cursor = Cursors.Default;
		}

		private void leadImg_MouseMove(object sender, MouseEventArgs e)
		{
			this.Cursor = Cursors.Hand;
		}

		private void dGV_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			// 画像表示
			ShowImage(global.pblImagePath + Utility.NulltoStr(dGV[cImg, dGV.CurrentRow.Index].Value).ToString());
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("出勤簿内容を印刷します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}

			// 印刷
			sReport(Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text), Utility.StrtoInt(txtNo.Text)); 
		}


		/// ---------------------------------------------------------------------------
		/// <summary>
		///     過去出勤簿データ印刷 </summary>
		/// <param name="sYY">
		///     対象年（西暦）</param>
		/// <param name="sMM">
		///     対象月 </param>
		/// <param name="sID">
		///     社員番号</param>
		/// ---------------------------------------------------------------------------
		private void sReport(int sYY, int sMM, int sID)
		{
			// 出勤区分データセット
			JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter sAdp = new JAFA_OCRDataSetTableAdapters.出勤区分TableAdapter();
			sAdp.Fill(dts.出勤区分);

			int sRow = 5;   // 明細印刷開始行

			try
			{
				//マウスポインタを待機にする
				this.Cursor = Cursors.WaitCursor;

				string sAppPath = System.AppDomain.CurrentDomain.BaseDirectory;

				Excel.Application oXls = new Excel.Application();

				// 勤務報告書テンプレートシート
				string xlsFile = System.IO.Directory.GetCurrentDirectory() + @"\" + "出勤簿印刷Temp.xlsx";
				Excel.Workbook oXlsBook = (Excel.Workbook)(oXls.Workbooks.Open(xlsFile,
												   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
												   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
												   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
												   Type.Missing, Type.Missing));

				Excel.Worksheet oxlsSheet = (Excel.Worksheet)oXlsBook.Sheets[1];

				Excel.Range[] rng = new Microsoft.Office.Interop.Excel.Range[2];

				string Category = string.Empty;

				try
				{
					// 年月
					oxlsSheet.Cells[2, 2] = sYY.ToString() + "年" + sMM.ToString() + "月";

					// 職員コード・氏名
					oxlsSheet.Cells[2, 6] = txtNo.Text;
					oxlsSheet.Cells[2, 7] = lblName.Text;

					// 所属コード・所属名
					oxlsSheet.Cells[2, 9] = lblSzCode.Text;
					oxlsSheet.Cells[2, 10] = lblSzName.Text;
					
					// 出勤簿のグリッドを順番に読む
					for (int i = 0; i < dGV.RowCount; i++)
					{
						if (dGV[cDay, i].Value != null) // 日付がnullの行はネグる 2015/08/27
						{
							oxlsSheet.Cells[i + sRow, 2] = dGV[cDay, i].Value.ToString();
							oxlsSheet.Cells[i + sRow, 3] = dGV[cWeek, i].Value.ToString();
							oxlsSheet.Cells[i + sRow, 4] = dGV[cKintai1, i].Value.ToString();

							// 出勤区分名称
							var sKbn = dts.出勤区分.Where(a => a.ID == Utility.StrtoInt(dGV[cKintai1, i].Value.ToString()));
							foreach (var item in sKbn)
							{
								oxlsSheet.Cells[i + sRow, 5] = item.名称;
							}

							oxlsSheet.Cells[i + sRow, 6] = setCellTime(dGV[cSH, i].Value.ToString(), dGV[cSM, i].Value.ToString());
							oxlsSheet.Cells[i + sRow, 7] = setCellTime(dGV[cEH, i].Value.ToString(), dGV[cEM, i].Value.ToString());
							oxlsSheet.Cells[i + sRow, 8] = setCellTime(dGV[cKSH, i].Value.ToString(), dGV[cKSM, i].Value.ToString());
							oxlsSheet.Cells[i + sRow, 9] = setCellTime(dGV[cWH, i].Value.ToString(), dGV[cWM, i].Value.ToString());
						}
					}

					// JAメイトOCRデータのグリッドを順番に読む
					for (int i = 0; i < 25; i++)
					{
						oxlsSheet.Cells[i + 5, 12] = dgvJa[1, i].Value.ToString();
					}

					oxlsSheet.Cells[37, 4] = txtKoutsuuhi.Text;         // 交通費
					oxlsSheet.Cells[38, 4] = txtNittou.Text;            // 日当
					oxlsSheet.Cells[39, 4] = txtShukuhakuhi.Text;       // 宿泊費

					//マウスポインタを元に戻す
					this.Cursor = Cursors.Default;

					//印刷
					oXlsBook.PrintOut();

					// ウィンドウを非表示にする
					oXls.Visible = false;

					//保存処理
					oXls.DisplayAlerts = false;

					//Bookをクローズ
					oXlsBook.Close(Type.Missing, Type.Missing, Type.Missing);

					//Excelを終了
					oXls.Quit();
				}

				catch (Exception e)
				{
					MessageBox.Show(e.Message, "印刷処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					// ウィンドウを非表示にする
					oXls.Visible = false;

					//保存処理
					oXls.DisplayAlerts = false;

					//Bookをクローズ
					oXlsBook.Close(Type.Missing, Type.Missing, Type.Missing);

					//Excelを終了
					oXls.Quit();
				}

				finally
				{
					// COM オブジェクトの参照カウントを解放する 
					System.Runtime.InteropServices.Marshal.ReleaseComObject(oxlsSheet);
					System.Runtime.InteropServices.Marshal.ReleaseComObject(oXlsBook);
					System.Runtime.InteropServices.Marshal.ReleaseComObject(oXls);

					//マウスポインタを元に戻す
					this.Cursor = Cursors.Default;
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "印刷処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			//マウスポインタを元に戻す
			this.Cursor = Cursors.Default;
		}

		/// ----------------------------------------------------------------
		/// <summary>
		///     セルに渡す時刻、時間文字列 </summary>
		/// <param name="hh">
		///     時</param>
		/// <param name="mm">
		///     分</param>
		/// <returns>
		///     印字文字列</returns>
		/// ----------------------------------------------------------------
		private string setCellTime(string hh, string mm)
		{
			string rtn = string.Empty;

			if (hh != string.Empty || mm != string.Empty)
			{
				rtn = hh.PadLeft(2, ' ') + ":" + mm.PadLeft(2, '0');
			}

			return rtn;
		}

		private void dGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dgvJa_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				//イベントハンドラが複数回追加されてしまうので最初に削除する
				e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
				//e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress2);

				//イベントハンドラを追加する
				e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
			}
		}

		private void dgvJa_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			// 入力可能文字桁数をコントロールする
			if (e.ColumnIndex == 1)
			{
				DataGridViewColumn c = (DataGridViewColumn)dgvJa.Columns[e.ColumnIndex];
				DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)c;

				// 実働時間、残業時間、深夜時間
				if (e.RowIndex == 1 || e.RowIndex == 2 || e.RowIndex == 3)
				{
					col.MaxInputLength = 6;
				}
				else if (e.RowIndex < 24)
				{
					// 日数項目
					col.MaxInputLength = 2;
				}
				else if (e.RowIndex == 24)
				{
					// 有休繰越日数
					col.MaxInputLength = 4;
				}
			}
		}

		private bool errCheck()
		{
			bool rtn = true;

			for (int i = 0; i < dgvJa.RowCount; i++)
			{
				if (dgvJa[cjaH, i].Value == null)
				{
					continue;
				}

				// 実働時間、残業時間、深夜時間
				if (i == 1 || i == 2 || i == 3)
				{
					if (!checkTmFormat(Utility.NulltoStr(dgvJa[cja, i].Value), i))
					{
						return false;
					}
				}
				else if (i > 3 && i < 24)
				{
					if (Utility.StrtoInt(Utility.NulltoStr(dgvJa[cja, i].Value)) > 31)
					{
						MessageBox.Show(dgvJa[cjaH, i].Value.ToString() + "の値が正しくありません", "日数エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						dgvJa.CurrentCell = dgvJa[cja, i];
						return false;
					}
				}
				else if (i == 24)
				{
					double d = 0;
					if (!double.TryParse(dgvJa[cja, i].Value.ToString(), out d))
					{
						MessageBox.Show(dgvJa[cjaH, i].Value.ToString() + "の値が正しくありません", "有休繰越日数エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						dgvJa.CurrentCell = dgvJa[cja, i];
						return false;
					}
				}
			}

			return rtn;
		}
		
		/// ----------------------------------------------------------------------------
		/// <summary>
		///     実働時間、残業時間、深夜時間の入力値チェック  </summary>
		/// <param name="sVal">
		///     実働時間、残業時間、深夜時間の入力値</param>
		/// <param name="i">
		///     行インデックス</param>
		/// <returns>
		///     true:エラーなし, false:エラーあり </returns>
		/// ----------------------------------------------------------------------------
		private bool checkTmFormat(string sVal, int i)
		{
			bool rtn = true;

			if (sVal.Contains("."))
			{
				MessageBox.Show(dgvJa[cjaH, i].Value.ToString() + "は  XX:XX形式で入力してください", "時間エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				dgvJa.CurrentCell = dgvJa[cja, i];
				return false;
			}

			if (!sVal.Contains(":"))
			{
				MessageBox.Show(dgvJa[cjaH, i].Value.ToString() + "は  XX:XX形式で入力してください", "時間エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				dgvJa.CurrentCell = dgvJa[cja, i];
				return false;
			}
			else
			{
				string [] s = sVal.Split(':');

				if (s.Length != 2)
				{
					MessageBox.Show(dgvJa[cjaH, i].Value.ToString() + "は  XX:XX形式で入力してください", "時間エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					dgvJa.CurrentCell = dgvJa[cja, i];
					return false;
				}
				else
				{
					if (Utility.NulltoStr(s[0]) == string.Empty || Utility.NulltoStr(s[1]) == string.Empty)
					{
						MessageBox.Show(dgvJa[cjaH, i].Value.ToString() + "は  XX:XX形式で入力してください", "時間エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						dgvJa.CurrentCell = dgvJa[cja, i];
						return false;
					}

					if (Utility.StrtoInt(Utility.NulltoStr(s[1])) >= 60)
					{
						MessageBox.Show(dgvJa[cjaH, i].Value.ToString() + "の分を正しく入力してください", "時間エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						dgvJa.CurrentCell = dgvJa[cja, i];
						return false;
					}
				}
			}

			return rtn;

		}

		/// --------------------------------------------------------------------
		/// <summary>
		///      XX:XX形式から分に変換する </summary>
		/// <param name="sVal">
		///     実働時間、残業時間、深夜時間の入力値 </param>
		/// <returns>
		///     時間（分）</returns>
		/// --------------------------------------------------------------------
		private int getWorkMinute(string sVal)
		{
			string[] s = sVal.Split(':');

			int m = Utility.StrtoInt(s[0]) * 60 + Utility.StrtoInt(s[1]);

			return m;
		}

		private void trackBar1_ValueChanged(object sender, EventArgs e)
		{
			n_width = B_WIDTH + (float)trackBar1.Value * 0.05f;
			n_height = B_HEIGHT + (float)trackBar1.Value * 0.05f;

			imgShow(mMat, n_width, n_height);
		}

		///-----------------------------------------------------------------------------------
		/// <summary>
		///     カレントデータを更新する：2018/11/11</summary>
		/// <param name="iX">
		///     カレントレコードのインデックス</param>
		///-----------------------------------------------------------------------------------
		private void curDataUpDate()
		{
			// エラーメッセージ
			string errMsg = "出勤簿テーブル更新";

			try
			{
				// 過去勤務票ヘッダテーブル行を取得
				JAFA_OCRDataSet.過去勤務票ヘッダRow r = (JAFA_OCRDataSet.過去勤務票ヘッダRow)dts.過去勤務票ヘッダ.Single(a => a.年 == _sYY && a.月 == _sMM && a.社員番号 == _sID);

                // 過去勤務票ヘッダテーブルセット更新
                r.年 = Utility.StrtoInt(txtYear.Text);
				r.月 = Utility.StrtoInt(txtMonth.Text);
				r.社員番号 = Utility.StrtoInt(txtNo.Text);
				r.社員名 = Utility.NulltoStr(lblName.Text);
				r.交通費 = Utility.StrtoInt(txtKoutsuuhi.Text.Replace(",", ""));
				r.日当 = Utility.StrtoInt(txtNittou.Text.Replace(",", ""));
				r.宿泊費 = Utility.StrtoInt(txtShukuhakuhi.Text.Replace(",", ""));
				r.更新年月日 = DateTime.Now;

				// データベース更新
				adpH.Update(dts.過去勤務票ヘッダ);

                // 過去勤務票明細テーブルセット更新
                for (int i = 0; i < global._MonthMULTIGYO; i++)
				{
					// 存在する日付か検証
					if (Utility.NulltoStr(dGV[cWeek, i].Value) != string.Empty)
					{
						JAFA_OCRDataSet.過去勤務票明細Row m = (JAFA_OCRDataSet.過去勤務票明細Row)dts.過去勤務票明細.FindByID(int.Parse(dGV[cID, i].Value.ToString()));

						m.出勤区分 = Utility.NulltoStr(dGV[cKintai1, i].Value);
						m.開始時 = timeValH(dGV[cSH, i].Value);
						m.開始分 = timeVal(dGV[cSM, i].Value, 2);
						m.終了時 = timeValH(dGV[cEH, i].Value);
						m.終了分 = timeVal(dGV[cEM, i].Value, 2);
						m.休憩開始時 = timeValH(dGV[cKSH, i].Value);
						m.休憩開始分 = timeVal(dGV[cKSM, i].Value, 2);
						m.実働時 = Utility.StrtoInt(timeValH(dGV[cWH, i].Value));
						m.実働分 = Utility.StrtoInt(timeVal(dGV[cWM, i].Value, 2));

						if (dGV[cTeisei, i].Value.ToString() == "True")
						{
							m.訂正 = global.flgOn;
						}
						else
						{
							m.訂正 = global.flgOff;
						}

						m.更新年月日 = DateTime.Now;
					}
				}

				// データベース更新
				adpM.Update(dts.過去勤務票明細);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, errMsg, MessageBoxButtons.OK);
			}
			finally
			{
			}
		}
	}
}
