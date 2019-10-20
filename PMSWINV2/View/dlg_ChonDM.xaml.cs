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
 
    public partial class dlg_ChonDM: Window
    {
        DataTable oTblSrc = null;
        public DataRow RowSP = null;
        private String pLoaiDM = "";

        public System.Data.DataRowView pRowChon;
        public String pGiaTriChon = "";
        public String pMaso = "";
        public String pFieldNameReturn="";

        // Tài khoản nợ, có
        public String pTKNo = "";
        public String pTKCo = "";

        // Địa chỉ KH
        public String pDiaChi = "";

        public dlg_ChonDM(String mLoaiDM="",String mFieldName ="")
        {
            InitializeComponent();

            MTGlobal.SetFormatGridControl(grdChonDM, tblView);
            grdChonDM.View.NavigationStyle = DevExpress.Xpf.Grid.GridViewNavigationStyle.Row;
            pLoaiDM = mLoaiDM;
            pFieldNameReturn = mFieldName;
            setDataSource();                       
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void setDataSource() {
            try{
                
               for(int i=0;i<=grdChonDM.Columns.Count-1;i++){
                  grdChonDM.Columns[i].Visible=false;
               }

                String mSql = "";
                switch (pLoaiDM) { 
                    case "KHO":                        
                        mSql = "SELECT Khoid,Makho,Tenkho,Diachi,Thukho FROM DM_KHO with(nolock) order by Makho asc";
                        oTblSrc=new MTSQLServer().wRead(mSql,null,false);
                        colID.FieldName = "Khoid"; 
                        colMaso.FieldName="Makho";                    
                        colMaso.Header = "Mã kho";
                        colMaso.Width = 100;

                        colTen.FieldName="Tenkho";
                        colTen.Header = "Tên Kho";
                        colTen.Width = 200;

                        colCol1.FieldName="Diachi";
                        colCol1.Header = "Địa chỉ";
                        colCol1.Width = 200;
                        for(int i=1;i<=3;i++){
                         grdChonDM.Columns[i].Visible=true; 
                        }
                        lblTitle.Content = "DANH MỤC KHO";
                        break;

                    case "LYDO":
                        mSql = "SELECT Mald, Lydo, TKNo, TKCo FROM DM_LYDO with(nolock) order by Mald asc";
                        oTblSrc=new MTSQLServer().wRead(mSql,null,false);
                        colMaso.FieldName="Mald";
                        colMaso.Header = "Mã LD";
                        colMaso.Width = 100;
                        colTen.FieldName="Lydo";
                        colTen.Header = "Lý do";
                        colTen.Width = 200;

                        colCol1.FieldName="TKNo";
                        colCol1.Width = 100;
                        colCol1.Header = "TK Nợ";

                        colCol2.FieldName="TKCo";
                        colCol2.Width = 100;
                        colCol2.Header = "TKCo";

                        colMaso.Visible=true;
                        colTen.Visible=true;
                        colCol1.Visible = true;
                        colCol2.Visible = true;
                        lblTitle.Content = "DANH MỤC LÝ DO";
                        break;

                    case "DV":
                       mSql = "SELECT Makhid,Makh,Tenkh,Dienthoai,Diachi,Maloai FROM DM_KHACHHANG with(nolock) order by Makh asc";
                       oTblSrc=new MTSQLServer().wRead(mSql,null,false);
                       grdChonDM.Columns[0].FieldName="Makhid,"; 
                       colMaso.FieldName="Makh";
                       colMaso.Width=80;
                       colMaso.Header= "Mã KH";
                       
                       colTen.FieldName="Tenkh";
                       colTen.Width=200;
                       colTen.Header= "Khách Hàng";

                       colCol1.FieldName="Dienthoai";
                       colCol1.Width = 90;
                       colCol1.Header = "Điện thoại";

                       colCol2.FieldName="Diachi";
                       colCol2.Width = 200;
                       colCol2.Header = "Địa Chỉ";

                       colCol3.FieldName="Loaikh";
                       colCol3.Width = 80;
                       colCol3.Header = "Loại";

                       colMaso.Visible=true;
                       colTen.Visible=true;
                       colCol1.Visible = true;
                       colCol2.Visible = true;
                       colCol3.Visible = true;
                       lblTitle.Content = "DANH MỤC KHÁCH HÀNG";
                       break;

                    case "XE":
                       mSql = "SELECT Xeid,Soxe,Loaixe,Taixe,Dienthoai FROM DM_XE with(nolock) order by Soxe asc";
                       oTblSrc = new MTSQLServer().wRead(mSql, null, false);
                       grdChonDM.Columns[0].FieldName = "Xeid";
                       colMaso.FieldName = "Soxe";
                       colMaso.Width = 120;
                       colMaso.Header = "Số xe";

                       colTen.FieldName = "Loaixe";
                       colTen.Width = 150;
                       colTen.Header = "Loại xe";

                       colCol1.FieldName = "Taixe";
                       colCol1.Width = 200;
                       colCol1.Header = "Tài xế";

                       colCol2.FieldName = "Dienthoai";
                       colCol2.Width =120;
                       colCol2.Header = "Điện thoại";
                         
                       colMaso.Visible = true;
                       colTen.Visible = true;
                       colCol1.Visible = true;
                       colCol2.Visible = true;                     
                       lblTitle.Content = "DANH MỤC XE";
                       break;
                    case "NHANVIEN":
                       mSql = "SELECT Manvid,Manv,Tennv FROM DM_NHANVIEN with(nolock) order by Manv asc";
                       oTblSrc = new MTSQLServer().wRead(mSql, null, false);
                       grdChonDM.Columns[0].FieldName = "Manvid";
                       colMaso.FieldName = "Manv";
                       colMaso.Width = 120;
                       colMaso.Header = "Mã nhân viên";

                       colTen.FieldName = "Tennv";
                       colTen.Width = 200;
                       colTen.Header = "Tên nhân viên";
                         
                       colMaso.Visible = true;
                       colTen.Visible = true;                  
                       lblTitle.Content = "DANH MỤC NHÂN VIÊN";
                       break;
                }
 
                if (oTblSrc == null) {
                    Utils.showMessage("Không thể đọc dữ liệu cần chọn...", "Thông báo");
                    return;
                }
                grdChonDM.ItemsSource = this.oTblSrc;
                if (grdChonDM.VisibleRowCount > 0) {
                    grdChonDM.Focus();
                   colMaso.Focus();
                } 
            }
            catch (Exception ex) { }
        }
       
        private void cmdAccept_Click(object sender, RoutedEventArgs e){
            try {
                if (pFieldNameReturn != "")
                {
                    pGiaTriChon = grdChonDM.GetCellValue(tblView.FocusedRowHandle, pFieldNameReturn).ToString();
                    pMaso = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colMaso).ToString();

                    if (pLoaiDM == "LYDO")
                    {
                        pTKNo = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol1).ToString();
                        pTKCo = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol2).ToString();
                    }

                    if (pLoaiDM == "DV")
                    {
                        pDiaChi = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol2).ToString();
                    }
                }
                else {
                    pGiaTriChon = grdChonDM.GetCellValue(tblView.FocusedRowHandle,colTen).ToString();
                    if (pLoaiDM == "LYDO")
                    {
                        pTKNo = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol1).ToString();
                        pTKCo = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol2).ToString();
                    }

                    if (pLoaiDM == "DV")
                    {
                        pDiaChi = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol2).ToString();
                    }
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
 

       
        private void tblView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            pGiaTriChon = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colTen).ToString();
            pMaso = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colMaso).ToString();
            if (pLoaiDM == "LYDO")
            {
                pTKNo = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol1).ToString();
                pTKCo = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol2).ToString();
            }

            if (pLoaiDM == "DV")
            {
                pDiaChi = grdChonDM.GetCellValue(tblView.FocusedRowHandle, colCol2).ToString();
            }
        }

        private void Grid_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                cmdAccept_Click(sender, e);
            }else if(e.Key==Key.Escape){
                cmdExit_Click(sender, e);
            }
        }

        private void tblView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            cmdAccept_Click(sender, e);
        }

      
    }
}
