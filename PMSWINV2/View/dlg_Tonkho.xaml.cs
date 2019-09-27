using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using System.Data;
using System.Data.SqlClient;

using MTPMSWIN.Model;


namespace MTPMSWIN.View
{
 
    public partial class dlg_Tonkho: Window
    {       
        public DataRow RowSP = null;
        private String pLoaiDM = "";

        public System.Data.DataRowView pRowChon;
        public String pGiaTriChon = "";
        public String pMaso = "";
        public String pFieldNameReturn="";
        private String pDenngay = "";
        public dlg_Tonkho(DataTable mTblSP,String mDenngay="")
        {
            InitializeComponent();

            MTGlobal.SetFormatGridControl(grdChonDM, tblView);
            grdChonDM.View.NavigationStyle = DevExpress.Xpf.Grid.GridViewNavigationStyle.Row;
            this.pDenngay = mDenngay;

            setDataSource(mTblSP);                       
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void setDataSource(DataTable oTblSP) {
            try{

                SqlParameter[] arrPara = new SqlParameter[3];
                arrPara[0] = new SqlParameter("@tblSP", SqlDbType.Structured);
                arrPara[0].Value = oTblSP;
                arrPara[1] = new SqlParameter("@Denngay", SqlDbType.NVarChar,15);
                arrPara[1].Value =this.pDenngay;
                arrPara[2] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar,50);
                arrPara[2].Value = MTGlobal.MT_USER_LOGIN;

                DataTable oTblHHTK = new MTSQLServer().wRead("spHH_Xemtonkho", arrPara);
                grdChonDM.ItemsSource = oTblHHTK;
                if (grdChonDM.VisibleRowCount > 0) {
                    grdChonDM.Focus();
                   colMasp.Focus();
                } 
            }
            catch (Exception ex) { }
        }     
                
        private void cmdAccept_Click(object sender, RoutedEventArgs e){
            try {
                if (pFieldNameReturn != "")
                {
                    pGiaTriChon = grdChonDM.GetCellValue(tblView.FocusedRowHandle, pFieldNameReturn).ToString();
                    pMaso = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString();                    
                }
                else {
                    pGiaTriChon = grdChonDM.GetCellValue(tblView.FocusedRowHandle,colTenSP).ToString();
                    pMaso = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString();
                }
                pRowChon = (System.Data.DataRowView)grdChonDM.GetRow(tblView.FocusedRowHandle);
                this.Close();
            }catch{}
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
          
            this.Close();
        }

        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
             
            this.Close();
        }

        private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        } 

        private void Grid_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                cmdAccept_Click(sender, e);
            }
        }

      
    }
}
