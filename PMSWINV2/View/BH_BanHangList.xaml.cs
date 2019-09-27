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
   
    public partial class BH_BanHangList: Window{
     
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;
        
        private DataTable oTblNX;
        private String pPhieuID = "";
        private String pLoaiPhieu = ""; 
        private String pTuNgay = "";
        private String pDenNgay = "";
        private String pCa = "";
        private String pQuay = "";

        DevExpress.Utils.WaitDialogForm Dlg;

        public BH_BanHangList()
        {
            InitializeComponent();           
             
            MTGlobal.SetFormatGridControl(grdPhieu, tblViewList);
            MTGlobal.SetFormatGridControl(grdPhieuCT, tblViewCT,false);
            MTGlobal.SetFormatGridControl(grdPhieuKM, tblViewKM, false);
            MTGlobal.SetGridReadOnly(grdPhieu, tblViewCT, true);

            try
            {
                Dlg= Utils.shwWait();
                pLoaiPhieu = MTGlobal.MT_LOAIP;
                pTuNgay = MTGlobal.MT_TUNGAY;
                pDenNgay = MTGlobal.MT_DENNGAY;
                pCa = MTGlobal.MT_CA;
                pQuay = MTGlobal.MT_QUAY;

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
            try{
                SqlParameter[] arrPara = new SqlParameter[6];
                arrPara[0]=new SqlParameter("@LOAIP",SqlDbType.NVarChar,5);
                arrPara[0].Value=pLoaiPhieu;
                arrPara[1]=new SqlParameter("@TUNGAY",SqlDbType.NVarChar,15);
                arrPara[1].Value=pTuNgay;
                arrPara[2]=new SqlParameter("@DENNGAY",SqlDbType.NVarChar,15);
                arrPara[2].Value=pDenNgay;
                arrPara[3] = new SqlParameter("@CA", SqlDbType.NVarChar, 15);
                arrPara[3].Value = pCa;
                arrPara[4] = new SqlParameter("@QUAY", SqlDbType.NVarChar, 15);
                arrPara[4].Value = pQuay;
                arrPara[5]=new SqlParameter("@NGUOIDUNG",SqlDbType.NVarChar,50);
                arrPara[5].Value=MTGlobal.MT_USER_LOGIN;
                oTblNX = new MTSQLServer().wRead("BH_shwPhieuBHList", arrPara);
                 
                grdPhieu.ItemsSource = oTblNX;
                MTGlobal.SetGridReadOnly(grdPhieu,tblViewList, true);
            }
            catch{ }
        }
        private void BindDataCT(String mPhieuID) {
            try
            {
                String mSql = String.Format("SELECT * FROM BH_PHIEUBHCT where Phieubhid='{0}' AND ISNULL(Quatang,0)=0 order by Phieubhctid ASC",mPhieuID);
                DataTable oTblNXCT = new MTSQLServer().wRead(mSql, null, false);
                grdPhieuCT.ItemsSource = oTblNXCT;
                MTGlobal.SetGridReadOnly(grdPhieuCT, tblViewCT, true);
            }
            catch{ }
        }
        private void BindDataKM(String mPhieuID)
        {
            try
            {
                String mSql = String.Format("SELECT * FROM BH_PHIEUBHCT where Phieubhid='{0}' AND ISNULL(Quatang,0)=1 order by Phieubhctid ASC", mPhieuID);
                DataTable oTblNXCT = new MTSQLServer().wRead(mSql, null, false);
                grdPhieuKM.ItemsSource = oTblNXCT;
                MTGlobal.SetGridReadOnly(grdPhieuKM, tblViewKM, true);
            }
            catch { }
        }

        private void fChitietPhieu(bool isNew=false) {
            try{
                if (tblViewList.FocusedRowHandle < 0){
                    pPhieuID = "";
                }
                else{
                    try
                    {
                        pPhieuID = grdPhieu.GetCellValue(tblViewList.FocusedRowHandle, colPhieubhid).ToString();
                    }
                    catch { }
                }

                if (isNew == false && (pPhieuID == "" || grdPhieu.VisibleRowCount <= 0))
                {
                    isNew = true;
                }
                                
                BH_PhieuBH ofrmPN = new BH_PhieuBH(pPhieuID,this.pLoaiPhieu, this.pTuNgay, this.pDenNgay, MTROLE, isNew); 
                ofrmPN.ShowDialog();
                BindData();
            }
            catch { }        
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
                    if (pPhieuID == "") {
                        Utils.showMessage("Bạn chưa chọn phiếu để in..", "Lưu ý");
                        return;
                    } 
                    new MTReport().rptBH_InBill(pPhieuID);
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
            try
            {
                if (tblViewList.FocusedRowHandle < 0)
                {
                    return;
                }
                pPhieuID = grdPhieu.GetCellValue(tblViewList.FocusedRowHandle, colPhieubhid).ToString();
                if (pPhieuID != "")
                {
                    BindDataCT(pPhieuID);
                    BindDataKM(pPhieuID);
                }
            }
            catch (Exception ex) { ex.Data.Clear(); } 
        } 
        private void grdPhieu_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
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
