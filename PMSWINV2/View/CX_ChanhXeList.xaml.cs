using System;
using System.Collections.Generic;
using System.Data; 
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MTPMSWIN.Model;
using DevExpress.Xpf.Grid;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace MTPMSWIN.View
{
   
    public partial class CX_ChanhXeList : Window{
     
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;
        
        private DataTable oTblCX;
        private String pPhieuID = "";
        private String pLoaiPhieu = ""; 
        private String pTuNgay = "";
        private String pDenNgay = "";
        DevExpress.Utils.WaitDialogForm Dlg;

        public CX_ChanhXeList()
        {
            InitializeComponent();           
             
            MTGlobal.SetFormatGridControl(grdPhieu, tblViewList,true);
            MTGlobal.SetFormatGridControl(grdPhieuCT, tblViewCT,false);
            Dlg = Utils.shwWait();
            try
            {              
                pLoaiPhieu = MTGlobal.MT_LOAIP;
                pTuNgay = MTGlobal.MT_TUNGAY;
                pDenNgay = MTGlobal.MT_DENNGAY;

                setUserRight();
                BindData();
            }
            catch { }
            Utils.closeWait(Dlg);
        }
         
        private void setUserRight() {
            MTROLE.isAdd = true;
            MTROLE.isEdit = true;
            MTROLE.isDel = true;
            MTButton.cmdAdd = this.cmdAdd;       
        }
       
        private void BindData() {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0]=new SqlParameter("@LOAIP",SqlDbType.NVarChar,5);
                arrPara[0].Value=pLoaiPhieu;
                arrPara[1]=new SqlParameter("@TUNGAY",SqlDbType.NVarChar,15);
                arrPara[1].Value=pTuNgay;
                arrPara[2]=new SqlParameter("@DENNGAY",SqlDbType.NVarChar,15);
                arrPara[2].Value=pDenNgay;
                arrPara[3]=new SqlParameter("@NGUOIDUNG",SqlDbType.NVarChar,50);
                arrPara[3].Value=MTGlobal.MT_USER_LOGIN;
                oTblCX = new MTSQLServer().wRead("NX_shwChanhxeList", arrPara);

                grdPhieu.ItemsSource = oTblCX;
                MTGlobal.SetGridReadOnly(grdPhieu, tblViewList, true);
            }
            catch{ }
        }
        private void BindDataCT(String mPhieuID) {
            try
            {
                String mSql = String.Format("SELECT A.*,B.Tensp,B.Dvt,B.Quycach FROM NX_CHANHXECT A LEFT JOIN DM_SANPHAM B ON A.Masp=B.Masp WHERE Chanhxeid='{0}' order by Chanhxectid asc", mPhieuID);
                DataTable oTblNXCT = new MTSQLServer().wRead(mSql, null, false);
                grdPhieuCT.ItemsSource = oTblNXCT;
                MTGlobal.SetGridReadOnly(grdPhieuCT, tblViewCT, true);
            }
            catch{ }
        }

        private void fChitietPhieu(bool isNew=false) {
            try{ 
                if (tblViewList.FocusedRowHandle < 0){
                    pPhieuID = "";
                }
                else
                {
                    try
                    {
                        pPhieuID = grdPhieu.GetCellValue(tblViewList.FocusedRowHandle, colChanhxeid).ToString();
                    }
                    catch { }
                }                 
                if (isNew == false && (pPhieuID == "" || grdPhieu.VisibleRowCount <= 0))
                 {
                   isNew = true;
                 }

                CX_PhieuCX ofrmCX = new CX_PhieuCX(pPhieuID, this.pLoaiPhieu, this.pTuNgay, this.pDenNgay, MTROLE, isNew);
                ofrmCX.ShowDialog();
                BindData(); 
            }
            catch (Exception ex) { Utils.showMessage(ex.Message.ToString(), "Thông báo lỗi"); }
        }

        private void Grid_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:
                    cmdAdd_Click(sender, e);
                    break;

                case Key.F6:
                    fChitietPhieu();
                    break;

                case Key.F7:     
                      if (pPhieuID == "")
                        {
                            Utils.showMessage("Bạn chưa chọn phiếu để in...", "Lưu ý");
                        }
                        new MTReport().rptNX_Chanhxe(pPhieuID);
                    break;

                case Key.F8:
                    try
                    {
                        MTMain oMain = Application.Current.Windows.OfType<MTMain>().FirstOrDefault();
                        if (oMain != null)
                        {
                            oMain.onExitTab();
                        }
                    }
                    catch { }
                    break;
            }
        }
      

        private void tblViewList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try{
                if (tblViewList.FocusedRowHandle < 0) {
                    return;
                }
                pPhieuID = grdPhieu.GetCellValue(tblViewList.FocusedRowHandle, colChanhxeid).ToString();
                if (pPhieuID != "")
                {
                    BindDataCT(pPhieuID);
                }
            }
            catch (Exception ex) { ex.Data.Clear(); }
        }
       
      
        private void grdPhieu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            fChitietPhieu();
        }
        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            fChitietPhieu(true);
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
         
       
    }
}
