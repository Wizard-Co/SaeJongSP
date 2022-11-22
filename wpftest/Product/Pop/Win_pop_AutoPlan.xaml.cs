using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WizMes_SaeJongSP.PopUp
{
    /// <summary>
    /// RheoChoice.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Win_pop_AutoPlan : Window
    {
        int rowNum = 0;

        public string InstID = "";

        Lib lib = new Lib();

        public Win_prd_PlanInputAuto AutoPlan = new Win_prd_PlanInputAuto();

        public List<Win_prd_PlanInputAuto_CodeView> lstAutoPlan = new List<Win_prd_PlanInputAuto_CodeView>();



        public Win_pop_AutoPlan()
        {
            InitializeComponent();
        }

        public Win_pop_AutoPlan(List<Win_prd_PlanInputAuto_CodeView> lstAutoPlan)
        {
            InitializeComponent();

            this.lstAutoPlan = lstAutoPlan;
        }

        public Win_pop_AutoPlan(string InstID)
        {
            InitializeComponent();

            this.InstID = InstID;

        }

        // 콤보박스셋팅
        private void ComboBoxSetting()
        {
            

        }

        private void MoveSub_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxSetting();

            FillGrid();
        }

        #region 주요 버튼 이벤트 - 확인, 닫기, 검색

        public List<Win_mtr_LotStockControl_U_CodeView> lstLotStock = new List<Win_mtr_LotStockControl_U_CodeView>();

        //확인
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgdMain.Items.Count; i++)
            {
                var main = dgdMain.Items[i] as Win_mtr_LotStockControl_U_CodeView;

                if (main != null && main.Chk == true)
                {
                    lstLotStock.Add(main);

                }

            }

            this.DialogResult = true;

        }

        //닫기
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //검색
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>

            {
                Thread.Sleep(2000);

                //로직
                re_Search(rowNum);

            }), System.Windows.Threading.DispatcherPriority.Background);



            Dispatcher.BeginInvoke(new Action(() =>

            {

            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        #endregion // 주요 버튼 이벤트


        #region Header 부분 - 검색조건


       
      

        #endregion // Header 부분 - 검색조건

        #region 주요 메서드 모음

        private void re_Search(int rowNum)
        {
            FillGrid();

            if (dgdMain.Items.Count > 0)
            {
                dgdMain.SelectedIndex = rowNum;
            }
            else
            {
                this.DataContext = null;
                MessageBox.Show("조회된 데이터가 없습니다.");
                return;
            }
        }

        #region 조회
        private void FillGrid()
        {
            if (dgdMain.Items.Count > 0)
            {
                dgdMain.Items.Clear();
            }

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("ChkDate",0);
                sqlParameter.Add("SDate", "");
                sqlParameter.Add("EDate", "");
                sqlParameter.Add("ChkCustomID",  0);
                sqlParameter.Add("CustomID", "");
                sqlParameter.Add("ChkArticleID", 0);
                sqlParameter.Add("ArticleID", "");
                sqlParameter.Add("ChkOrder",  0);
                sqlParameter.Add("Order",  "");
                sqlParameter.Add("ChkIncPlComplete",  0);
                sqlParameter.Add("ChkCloseClss", 0);
                sqlParameter.Add("ChkBuyerArticleNo",  0);
                sqlParameter.Add("BuyerArticleNoID", "");


                DataSet ds = DataStore.Instance.ProcedureToDataSet_LogWrite("xp_AutoPlan_sOrder", sqlParameter, true, "R");

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    int i = 0;

                    if (dt.Rows.Count > 0)
                    {
                        DataRowCollection drc = dt.Rows;

                        foreach (DataRow dr in drc)
                        {
                            i++;

                            var AutoPlan = new Win_prd_PlanInputAuto_CodeView()
                            {
                            


                            };

                            //dgdMain.Items.Add();
                        }
                        //tbkCount.Text = "▶ 검색결과 : " + i.ToString() + " 건";
                    }
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
        #endregion



        #endregion

        #region 생산계획편성
        //
        private void btnAutoPlan_Click()
        {

            try
            {
                // 대상조회된거 선택한 셀 정보들 넣기 
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();
                sqlParameter.Add("InstID", ""); 
                sqlParameter.Add("InstDate", MainWindow.CurrentUser);
                sqlParameter.Add("OrderID", MainWindow.CurrentUser);
                sqlParameter.Add("OrderSeq", MainWindow.CurrentUser);
                sqlParameter.Add("InstRoll", MainWindow.CurrentUser);
                sqlParameter.Add("InstQty", MainWindow.CurrentUser);
                sqlParameter.Add("ExpectDate", MainWindow.CurrentUser);
                sqlParameter.Add("PersonID", MainWindow.CurrentUser);
                sqlParameter.Add("Remark", MainWindow.CurrentUser);
                sqlParameter.Add("MtrExceptYN", MainWindow.CurrentUser);
                sqlParameter.Add("OutwareExceptYN", MainWindow.CurrentUser);
                sqlParameter.Add("CreateUserID", MainWindow.CurrentUser);
                sqlParameter.Add("AutoPlanYN", MainWindow.CurrentUser);

                string[] result = DataStore.Instance.ExecuteProcedure("xp_PlanInput_iAutoPlan", sqlParameter, false);

                if (result[0].Equals("success"))
                {
                    //MessageBox.Show("성공ㅇㅅㅇ");
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
        #endregion

        #region 유효성 검사

        private bool CheckData()
        {
            bool flag = true;

            return flag;
        }

        #endregion

        #region 데이터 그리드 체크박스 이벤트

        // 팝업창 체크박스 이벤트
        private void CHK_Click_Sub(object sender, RoutedEventArgs e)
        {
            //CheckBox chkSender = sender as CheckBox;
            //var MoveSub = chkSender.DataContext as Win_mtr_Move_U_CodeViewSub;

            //if (MoveSub != null)
            //{
            //    if (chkSender.IsChecked == true)
            //    {
            //        MoveSub.Chk = true;
            //        MoveSub.FontColor = true;

            //        if (ovcMoveSub.Contains(MoveSub) == false)
            //        {
            //            ovcMoveSub.Add(MoveSub);
            //        }
            //    }
            //    else
            //    {
            //        MoveSub.Chk = false;
            //        MoveSub.FontColor = false;

            //        if (ovcMoveSub.Contains(MoveSub) == true)
            //        {
            //            ovcMoveSub.Remove(MoveSub);
            //        }
            //    }
            //}
        }

        #endregion // 데이터 그리드 체크박스 이벤트

        #region 전체 선택 체크박스 이벤트

        // 전체 선택 체크박스 체크 이벤트
        private void AllCheck_Checked(object sender, RoutedEventArgs e)
        {
            //ovcMoveSub.Clear();

            //if (dgdMain.Visibility == Visibility.Visible)
            //{
            //    for (int i = 0; i < dgdMain.Items.Count; i++)
            //    {
            //        var MoveSub = dgdMain.Items[i] as Win_mtr_Move_U_CodeViewSub;
            //        MoveSub.Chk = true;
            //        MoveSub.FontColor = true;

            //        ovcMoveSub.Add(MoveSub);
            //    }
            //}
        }

        // 전체 선택 체크박스 언체크 이벤트
        private void AllCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            //ovcMoveSub.Clear();

            //if (dgdMain.Visibility == Visibility.Visible)
            //{
            //    for (int i = 0; i < dgdMain.Items.Count; i++)
            //    {
            //        var MoveSub = dgdMain.Items[i] as Win_mtr_Move_U_CodeViewSub;
            //        MoveSub.Chk = false;
            //        MoveSub.FontColor = false;
            //    }
            //}
        }

        #endregion // 전체 선택 체크박스 이벤트

        #region 기타 메서드

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
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
                str = str.Replace(",", "");

                if (Double.TryParse(str, out chkDouble) == true)
                {
                    result = Double.Parse(str);
                }
            }

            return result;
        }






        #endregion // 기타 메서드

        // 메인 그리드 더블클릭시 선택한걸로!!
        private void dgdMain_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ClickCount == 2)
            //{
            //    btnConfirm_Click(null, null);
            //}
        }

        private void chkReq_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chkSender = sender as CheckBox;
            var LotStock = chkSender.DataContext as Win_prd_PlanInputAuto_CodeView;

            if (LotStock != null)
            {
                //if (chkSender.IsChecked == true)
                //{
                //    LotStock.Chk = true;
                //}
                //else
                //{
                //    LotStock.Chk = false;
                //}

            }
        }


        //2021-05-29(2021-07-12 해제도 추가)
        private void BtnAllChoice_Click(object sender, RoutedEventArgs e)
        {
            if (dgdMain.Items.Count > 0)
            {
                foreach (Win_mtr_LotStockControl_U_CodeView Silsadata in dgdMain.Items)
                {

                    if (Silsadata != null && Silsadata.Chk == false)
                    {
                        Silsadata.Chk = true;
                    }
                    else
                    {
                        Silsadata.Chk = false;
                    }

                }

                dgdMain.Items.Refresh();
            }
        }


     

    }


}
