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
 
    public partial class dlg_ChonSanPham: Window
    {       
        public DataRow RowSP = null;
        private String pLoaiDM = "";

        public System.Data.DataRowView pRowChon;
        public String pGiaTriChon = "";
        public String pMaso = "";
        public String pFieldNameReturn="";

        public dlg_ChonSanPham(DataTable mTblSP)
        {
            InitializeComponent();

            MTGlobal.SetFormatGridControl(grdChonDM, tblView);
            grdChonDM.View.NavigationStyle = DevExpress.Xpf.Grid.GridViewNavigationStyle.Row;
          
            setDataSource(mTblSP);                       
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void setDataSource(DataTable oTblSrc) {
            try{ 
                if (oTblSrc == null) {
                    Utils.showMessage("Không thể đọc dữ liệu cần chọn...", "Thông báo");
                    return;
                }
                grdChonDM.ItemsSource = oTblSrc;
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
