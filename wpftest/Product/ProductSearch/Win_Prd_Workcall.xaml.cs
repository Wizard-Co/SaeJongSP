﻿using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WizMes_SaeJongSP.PopUp;
using WizMes_SaeJongSP.PopUP;
using WPF.MDI;

namespace WizMes_SaeJongSP
{
    /// <summary>
    /// Win_Prd_ProcessResult_Q.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Win_Prd_Workcall : UserControl
    {
        int rowNum = 0;
        Lib lib = new Lib();
        Win_Prd_Workcall_CodeView Call = new Win_Prd_Workcall_CodeView();

        public Win_Prd_Workcall()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetComboBox();
            chkDateSrh.IsChecked = true;

            dtpSDate.SelectedDate = DateTime.Today;
            dtpEDate.SelectedDate = DateTime.Today;
        }

        #region Header 부분 - 검색조건

        // 일자
        private void lblDateSrh_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (chkDateSrh.IsChecked == true)
            {
                chkDateSrh.IsChecked = false;
            }
            else
            {
                chkDateSrh.IsChecked = true;
            }
        }
        private void chkDateSrh_Checked(object sender, RoutedEventArgs e)
        {
            chkDateSrh.IsChecked = true;
            dtpSDate.IsEnabled = true;
            dtpEDate.IsEnabled = true;

            btnYesterDay.IsEnabled = true;
            btnToday.IsEnabled = true;
            btnLastMonth.IsEnabled = true;
            btnThisMonth.IsEnabled = true;
        }
        private void chkDateSrh_Unchecked(object sender, RoutedEventArgs e)
        {
            chkDateSrh.IsChecked = false;
            dtpSDate.IsEnabled = false;
            dtpEDate.IsEnabled = false;

            btnYesterDay.IsEnabled = false;
            btnToday.IsEnabled = false;
            btnLastMonth.IsEnabled = false;
            btnThisMonth.IsEnabled = false;
        }

        // 전일 금일 전월 금월 버튼
        //전일
        private void btnYesterDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime[] SearchDate = lib.BringLastDayDateTimeContinue(dtpEDate.SelectedDate.Value);

            dtpSDate.SelectedDate = SearchDate[0];
            dtpEDate.SelectedDate = SearchDate[1];
        }

        //금일
        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            dtpSDate.SelectedDate = DateTime.Today;
            dtpEDate.SelectedDate = DateTime.Today;
        }

        // 전월 버튼 클릭 이벤트
        private void btnLastMonth_Click(object sender, RoutedEventArgs e)
        {
            DateTime[] SearchDate = lib.BringLastMonthContinue(dtpSDate.SelectedDate.Value);

            dtpSDate.SelectedDate = SearchDate[0];
            dtpEDate.SelectedDate = SearchDate[1];
        }

        // 금월 버튼 클릭 이벤트
        private void btnThisMonth_Click(object sender, RoutedEventArgs e)
        {
            dtpSDate.SelectedDate = lib.BringThisMonthDatetimeList()[0];
            dtpEDate.SelectedDate = lib.BringThisMonthDatetimeList()[1];
        }

        // 공정 검색
        private void lblProcess_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (chkProcess.IsChecked == true)
            {
                chkProcess.IsChecked = false;
            }
            else
            {
                chkProcess.IsChecked = true;
            }
        }
        private void chkProcess_Checked(object sender, RoutedEventArgs e)
        {
            chkProcess.IsChecked = true;
            cboProcess.IsEnabled = true;
        }
        private void chkProcess_Unchecked(object sender, RoutedEventArgs e)
        {
            chkProcess.IsChecked = false;
            cboProcess.IsEnabled = false;
        }

        // 호출자 검색
        private void lblCallPerson_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (chkCallPerson.IsChecked == true)
            {
                chkCallPerson.IsChecked = false;
            }
            else
            {
                chkCallPerson.IsChecked = true;
            }
        }
        private void chkCallPerson_Checked(object sender, RoutedEventArgs e)
        {
            chkCallPerson.IsChecked = true;
            txtCallPerson.IsEnabled = true;
            btnPfCallPerson.IsEnabled = true;
        }
        private void chkCallPerson_Unchecked(object sender, RoutedEventArgs e)
        {
            chkCallPerson.IsChecked = false;
            txtCallPerson.IsEnabled = false;
            btnPfCallPerson.IsEnabled = false;
        }
        private void txtCallPerson_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainWindow.pf.ReturnCode(txtCallPerson, (int)Defind_CodeFind.DCF_PERSON, "");
            }
        }
        private void btnPfCallPerson_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.pf.ReturnCode(txtCallPerson, (int)Defind_CodeFind.DCF_PERSON, "");
        }

        //호기
        private void lblMachine_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (chkMachine.IsChecked == true)
            {
                chkMachine.IsChecked = false;
            }
            else
            {
                chkMachine.IsChecked = true;
            }
        }

        private void chkMachine_Checked(object sender, RoutedEventArgs e)
        {
            chkMachine.IsChecked = true;
            cboMachine.IsEnabled = true;
        }

        private void chkMachine_Unchecked(object sender, RoutedEventArgs e)
        {
            chkMachine.IsChecked = false;
            cboMachine.IsEnabled = false;

        }

        //응대상태
        private void lblRespondState_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (chkRespondState.IsChecked == true)
            {
                chkRespondState.IsChecked = false;
            }
            else
            {
                chkRespondState.IsChecked = true;
            }
        }

        private void chkRespondState_Checked(object sender, RoutedEventArgs e)
        {
            chkRespondState.IsChecked = true;
            cboRespondState.IsEnabled = true;
        }

        private void chkRespondState_Unchecked(object sender, RoutedEventArgs e)
        {
            chkRespondState.IsChecked = false;
            cboRespondState.IsEnabled = false;
        }

        #endregion // Header 부분 - 검색조건

        #region Header 부분 - 오른쪽 버튼 모음 (검색, 닫기, 엑셀)

        // 검색버튼
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //검색버튼 비활성화
            btnSearch.IsEnabled = false;

            Dispatcher.BeginInvoke(new Action(() =>

            {
                Thread.Sleep(2000);

                //로직
                using (Loading lw = new Loading(re_search))
                {
                    lw.ShowDialog();
                }

            }), System.Windows.Threading.DispatcherPriority.Background);



            Dispatcher.BeginInvoke(new Action(() =>

            {
                btnSearch.IsEnabled = true;

            }), System.Windows.Threading.DispatcherPriority.Background);
        }


        // 닫기버튼
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Lib.Instance.ChildMenuClose(this.ToString());
        }

        //삭제
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string OutMessage = string.Empty;

            MessageBoxResult MesResult = MessageBox.Show("선택하신 항목을 선택자료를 삭제하시겠습니까?", "확인", MessageBoxButton.YesNo);
            switch (MesResult)
            {
                case MessageBoxResult.Yes:

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("InstID", Call.Num);
                    sqlParameter.Add("OutMessage", "");

                    Dictionary<string, int> outputParam = new Dictionary<string, int>();
                    outputParam.Add("OutMessage", 200);

                    Dictionary<string, string> dicResult = DataStore.Instance.ExecuteProcedureOutputNoTran("xp_prd_dWorkCall", sqlParameter, outputParam, true);
                    string result = dicResult["OutMessage"];

                    if (result.Equals(""))
                    {
                        using (Loading lw = new Loading(FillGrid))
                        {
                            lw.ShowDialog();
                        }

                        dgdMain.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show(result);
                    }
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }

        // 엑셀버튼
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = null;
            string Name = string.Empty;

            string[] lst = new string[2];
            lst[0] = "일 생산 집계";
            lst[1] = dgdMain.Name;

            ExportExcelxaml ExpExc = new ExportExcelxaml(lst);

            ExpExc.ShowDialog();

            if (ExpExc.DialogResult.HasValue)
            {
                if (ExpExc.choice.Equals(dgdMain.Name))
                {
                    if (ExpExc.Check.Equals("Y"))
                        dt = Lib.Instance.DataGridToDTinHidden(dgdMain);
                    else
                        dt = Lib.Instance.DataGirdToDataTable(dgdMain);

                    Name = dgdMain.Name;

                    if (Lib.Instance.GenerateExcel(dt, Name))
                        Lib.Instance.excel.Visible = true;
                    else
                        return;
                }
                else
                {
                    if (dt != null)
                    {
                        dt.Clear();
                    }
                }
            }
        }

        #endregion // Header 부분 - 오른쪽 버튼 모음 (검색, 닫기, 엑셀)

        // 메인 데이터그리드 체크박스 이벤트
        private void chkC_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkSender = sender as CheckBox;
            var Main = chkSender.DataContext as Win_prd_PlanInputView_U_CodeView;
            if (Main != null)
            {
                Main.IsCheck = true;
            }
        }

        private void chkC_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chkSender = sender as CheckBox;
            var Main = chkSender.DataContext as Win_prd_PlanInputView_U_CodeView;
            if (Main != null)
            {
                Main.IsCheck = false;
            }
        }

        void re_search()
        {
            FillGrid();

            if (dgdMain.Items.Count > 1)
            {
                dgdMain.SelectedIndex = rowNum;
            }
            else
            {
                MessageBox.Show("조회된 데이터가 없습니다.");
                return;
            }
        }

        #region 콤보박스 

        //공정 
        private void SetComboBox() {

            //공정 콤보박스 
            ObservableCollection<CodeView> ovcProcess = ComboBoxUtil.Instance.GetWorkProcess(0,"");
            this.cboProcess.ItemsSource = ovcProcess;
            this.cboProcess.DisplayMemberPath = "code_name";
            this.cboProcess.SelectedValuePath = "code_id";

            //호기 콤보박스 
            ObservableCollection<CodeView> ovcMachine = ComboBoxUtil.Instance.GetMachine("");
            this.cboMachine.ItemsSource = ovcMachine;
            this.cboMachine.DisplayMemberPath = "code_name";
            this.cboMachine.SelectedValuePath = "code_id";

            //응대상태 콤보박스 
            List<string[]> listResStates = new List<string[]>();
            string[] YN01 = new string[] { "Y", "Y" };
            string[] YN02 = new string[] { "N", "N" };
            listResStates.Add(YN01);
            listResStates.Add(YN02);

            ObservableCollection<CodeView> ovcYN = ComboBoxUtil.Instance.Direct_SetComboBox(listResStates);
            this.cboRespondState.ItemsSource = ovcYN;
            this.cboRespondState.DisplayMemberPath = "code_name";
            this.cboRespondState.SelectedValuePath = "code_id";

            //호출사유 콤보박스 
            ObservableCollection<CodeView> ovcCallReason = ComboBoxUtil.Instance.GetCMCode_SetComboBox("PRDCallReason", "");
            this.cboCallReason.ItemsSource = ovcCallReason;
            this.cboCallReason.DisplayMemberPath = "code_name";
            this.cboCallReason.SelectedValuePath = "code_id";


        }

        #endregion

        #region 조회 메서드

        private void FillGrid()
        {
            
            if (dgdMain.Items.Count > 0)
            {
                dgdMain.Items.Clear();
            }

            if (dgdSum.Items.Count > 0)
            {
                dgdSum.Items.Clear();
            }

            var Sum = new Win_Prd_Workcall_Sum_CodeView();
            string str = string.Empty;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("chkDate", chkDateSrh.IsChecked == true? 1: 0);
                sqlParameter.Add("SDate", dtpSDate.SelectedDate != null ? dtpSDate.SelectedDate.Value.ToString("yyyyMMdd") : "");
                sqlParameter.Add("EDate", dtpEDate.SelectedDate != null ? dtpEDate.SelectedDate.Value.ToString("yyyyMMdd") : "");
                sqlParameter.Add("chkProcess", chkProcess.IsChecked == true ? 1: 0);
                sqlParameter.Add("ProcessID", chkProcess.IsChecked == true && cboProcess.SelectedValue != null ? cboProcess.SelectedValue.ToString() : "");
                sqlParameter.Add("chkMachine", chkMachine.IsChecked == true ? 1: 0);
                sqlParameter.Add("MachineID", chkMachine.IsChecked == true && cboMachine.SelectedValue != null ? cboMachine.SelectedValue.ToString() : "");
                sqlParameter.Add("chkRespondState", chkRespondState.IsChecked == true ? 1 : 0) ;
                sqlParameter.Add("RespondState", chkRespondState.IsChecked == true && cboRespondState.SelectedValue != null ? cboRespondState.SelectedValue.ToString() : "");
                sqlParameter.Add("chkCallPerson", chkCallPerson.IsChecked == true ? 1: 0);
                sqlParameter.Add("CallPersonID", chkCallPerson.IsChecked == true && txtCallPerson.Tag != null ? txtCallPerson.Tag.ToString() : "");
                sqlParameter.Add("chkCallReason", chkCallReason.IsChecked == true ? 1: 0);
                sqlParameter.Add("CallReason", chkCallReason.IsChecked == true && cboCallReason.SelectedValue != null ? cboCallReason.SelectedValue.ToString() : "");

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_prd_sWorkCall", sqlParameter, false); //2021-05-19
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    int i = 0;
                    int Res = 0;

                    if (dt.Rows.Count > 0)
                    {
                        DataRowCollection drc = dt.Rows;

                        foreach (DataRow dr in drc)
                        {
                            i++;
                            if (dr["RespondAbleYN"].ToString() != null)
                            {
                                Res++;
                            } 

                            var WinR = new Win_Prd_Workcall_CodeView()
                            {
                                Num = i.ToString(),

                                CallDate = dr["CallDate"].ToString(),
                                ProcessID = dr["processID"].ToString(),
                                Process = dr["Process"].ToString(),
                                MachineID = dr["MachineID"].ToString(),
                                Machine = dr["Machine"].ToString(),
                                CallPersonID = dr["CallPersonID"].ToString(),
                                CallPersonName = dr["CallPersonName"].ToString(),
                                CallTime = Lib.Instance.SixLengthTime(dr["CallTime"].ToString()),
                                CallReasonCode = dr["CallReasonCode"].ToString(),
                                CallReason = dr["CallReason"].ToString(),
                                RepondPersonID = dr["RepondPersonID"].ToString(),
                                RepondPersonName = dr["RepondPersonName"].ToString(),
                                

                            };

                            if (dr["RespondAbleYN"].ToString().Trim().Equals("Y"))
                            {
                                WinR.RespondAbleYN = "정상처리";
                            }
                            else
                            {
                                WinR.RespondAbleYN = "처리불가";
                            }

                            dgdMain.Items.Add(WinR);

                            Sum.SumCount = i.ToString();
                            Sum.SumCallCount += i.ToString();
                            Sum.SumResCount = Res.ToString();


                        }
                        dgdSum.Items.Add(Sum);

                    }

                    //tblCnt.Text = " ▶ 검색 결과 : " + (dt.Rows.Count - 1) + " 건";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("오류 발생, 오류 내용 : " + ex.ToString());
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }

        #endregion // 조회 메서드

        #region 기타 메서드 모음

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN1(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatN2(object obj)
        {
            return string.Format("{0:N2}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatNDigit(object obj, int digit)
        {
            return string.Format("{0:N" + digit + "}", obj);
        }

        // 데이터피커 포맷으로 변경
        private string DatePickerFormat(string str)
        {
            string result = "";

            if (str.Length == 8)
            {
                if (!str.Trim().Equals(""))
                {
                    result = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
                }
            }

            return result;
        }

        // 시간 형식 6글자라면! 11:11:11
        private string DateTimeFormat(string str)
        {
            str = str.Replace(":", "").Trim();

            if (str.Length == 6)
            {
                string Hour = str.Substring(0, 2);
                string Min = str.Substring(2, 2);
                string Sec = str.Substring(4, 2);

                str = Hour + ":" + Min + ":" + Sec;
            }

            return str;
        }

        // 시간 분 → 11:12 형식으로 변환
        private string DateTimeMinToTime(string str)
        {
            str = str.Replace(":", "").Trim();

            int num = 0;
            if (int.TryParse(str, out num) == true)
            {
                string hour = (num / 60).ToString();
                string min = (num % 60).ToString();

                if (min.Length == 1)
                {
                    min = "0" + min;
                }

                str = hour + ":" + min;
            }

            return str;
        }

        private string DateTimeFormat2(string str)
        {
            if (str == null) { return ""; }

            string result = str;

            str = str.Replace(":", "").Replace("/", "").Replace("-", "").Trim();

            if (str.Length == 14)
            {
                string Date = DatePickerFormat(str.Substring(0, 8));
                string Time = DateTimeFormat(str.Substring(8, 6));

                result = Date + " " + Time;
            }

            return result;
        }

        // Int로 변환
        private int ConvertInt(string str)
        {
            int result = 0;
            int chkInt = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "");

                if (Int32.TryParse(str, out chkInt) == true)
                {
                    result = Int32.Parse(str);
                }
            }

            return result;
        }

        // 소수로 변환 가능한지 체크 이벤트
        private bool CheckConvertDouble(string str)
        {
            bool flag = false;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                if (Double.TryParse(str, out chkDouble) == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        // 숫자로 변환 가능한지 체크 이벤트
        private bool CheckConvertInt(string str)
        {
            bool flag = false;
            int chkInt = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Trim().Replace(",", "");

                if (Int32.TryParse(str, out chkInt) == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        // 소수로 변환
        private double ConvertDouble(string str)
        {
            double result = 0;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "").Replace(":", "");

                if (Double.TryParse(str, out chkDouble) == true)
                {
                    result = Double.Parse(str);
                }
            }

            return result;
        }


        #endregion

     
        
    }

    #region 메인그리드 코드뷰

    class Win_Prd_Workcall_CodeView : BaseView
    {
        public string Num { get; set; }
        public bool chkData { get; set; }

        public string cls { get; set; }
        public string CallDate { get; set; }
        public string ProcessID { get; set; }
        public string Process { get; set; }
        public string MachineID { get; set; }
        public string Machine { get; set; }
        public string CallPersonID { get; set; }
        public string CallPersonName { get; set; }
        public string CallTime { get; set; }
        public string CallReasonCode { get; set; }
        public string CallReason { get; set; }
        public string RepondPersonID { get; set; }
        public string RepondPersonName { get; set; }
        public string RespondAbleYN { get; set; }

      
    }

    class Win_Prd_Workcall_Sum_CodeView
    {
        public string SumCount { get; set; }        //합계
        public string SumCallCount { get; set; }    //호출합계
        public string SumResCount { get; set; }     //응답합계
    }

    #endregion
}