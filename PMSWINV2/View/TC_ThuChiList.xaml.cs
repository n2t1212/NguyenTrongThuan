using MTPMSWIN.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace MTPMSWIN.View
{
    public partial class TC_ThuChiList : Window
    {
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        private String pPhieuID = "";
        private String pLoaiPhieu = ""; 
        private String pTuNgay = "";
        private String pDenNgay = "";
        DevExpress.Utils.WaitDialogForm Dlg;

        private DataTable oTblTC = null;

        public TC_ThuChiList()
        {
            InitializeComponent();
            MTGlobal.SetFormatGridControl(grdPhieuTC, tblViewTCList, true);
            MTGlobal.SetFormatGridControl(grdPhieuTCCT, tblViewTCCT, false);

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

        private void setUserRight()
        {
            MTROLE.isAdd = true;
            MTROLE.isEdit = true;
            MTROLE.isDel = true;
            MTButton.cmdAdd = this.cmdAdd;
        }

        private void BindData()
        {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0] = new SqlParameter("@LOAIP", SqlDbType.NVarChar, 5);
                arrPara[0].Value = pLoaiPhieu;
                arrPara[1] = new SqlParameter("@TUNGAY", SqlDbType.NVarChar, 15);
                arrPara[1].Value = pTuNgay;
                arrPara[2] = new SqlParameter("@DENNGAY", SqlDbType.NVarChar, 15);
                arrPara[2].Value = pDenNgay;
                arrPara[3] = new SqlParameter("@NGUOIDUNG", SqlDbType.NVarChar, 50);
                arrPara[3].Value = MTGlobal.MT_USER_LOGIN;
                oTblTC = new MTSQLServer().wRead("NX_shwPhieuTCList", arrPara);

                grdPhieuTC.ItemsSource = oTblTC;
                MTGlobal.SetGridReadOnly(grdPhieuTC, tblViewTCList, true);
            }
            catch { }
        }

        private void BindDataCT(String mPhieuID)
        {
            try
            {
                String mSql = String.Format("SELECT * FROM NX_PHIEUTCCT where phieutcid='{0}' ORDER BY Phieutcctid asc", mPhieuID);
                DataTable oTblNXCT = new MTSQLServer().wRead(mSql, null, false);
                grdPhieuTCCT.ItemsSource = oTblNXCT;
                MTGlobal.SetGridReadOnly(grdPhieuTCCT, tblViewTCCT, true);
            }
            catch { }
        }

        private void fChitietPhieu(bool isNew = false)
        {
            try
            {
                if (tblViewTCList.FocusedRowHandle < 0)
                {
                    pPhieuID = "";
                }
                else
                {
                    try
                    {
                        pPhieuID = grdPhieuTC.GetCellValue(tblViewTCList.FocusedRowHandle, colPhieutcid).ToString();
                    }
                    catch { }
                }

                if (isNew == false && (pPhieuID == "" || grdPhieuTC.VisibleRowCount <= 0))
                {
                    isNew = true;
                }


                if (pLoaiPhieu == MTGlobal.PT)
                {
                    TC_PhieuThu ofrmPT = new TC_PhieuThu(pPhieuID, this.pLoaiPhieu, this.pTuNgay, this.pDenNgay, MTROLE, isNew);
                    ofrmPT.ShowDialog();
                    BindData();
                }
                else if (pLoaiPhieu == MTGlobal.PC)
                {
                    //TC_PhieuChi ofrmPC = new TC_PhieuChi(pPhieuID, this.pLoaiPhieu, this.pTuNgay, this.pDenNgay, MTROLE, isNew);
                    //ofrmPC.ShowDialog();
                    //BindData();
                }
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
                    if (pLoaiPhieu == MTGlobal.PT)
                    {
                        
                    }
                    else
                    {
                        //
                    }
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

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            fChitietPhieu(true);
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tblViewTCList_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (tblViewTCList.FocusedRowHandle < 0)
                {
                    return;
                }
                pPhieuID = grdPhieuTC.GetCellValue(tblViewTCList.FocusedRowHandle, colPhieutcid).ToString();
                if (pPhieuID != "")
                {
                    BindDataCT(pPhieuID);
                }
            }
            catch (Exception ex) { ex.Data.Clear(); }
        }
    }
}
