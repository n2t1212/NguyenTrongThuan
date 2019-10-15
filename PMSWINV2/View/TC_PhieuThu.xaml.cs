using MTPMSWIN.Model;
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

namespace MTPMSWIN.View
{
    public partial class TC_PhieuThu : Window
    {
        MTGlobal.MT_ROLE pACT_ROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        private string pPhieuTCID = "";
        private string pDenNgay = "";
        private string pTuNgay = "";
        private string pLoaiPhieu = "";

        private DataTable otblPTC = null;
        private DataTable otblPTCCT = null;
        private bool isEdit = false;
        DevExpress.Utils.WaitDialogForm Dlg;

        public TC_PhieuThu()
        {
            InitializeComponent();
        }

        public TC_PhieuThu(String mPhieuTCID, String mLoaiPhieu, String mTuNgay, String mDenNgay,
            MTGlobal.MT_ROLE mActRole, bool isAddNew = false)
        {
            InitializeComponent();
            this.lblTitle.Content = "LẬP PHIẾU THU";
            this.pPhieuTCID = mPhieuTCID;
            this.pTuNgay = mTuNgay == "" ? DateTime.Now.ToShortDateString() : mTuNgay;
            this.pDenNgay = mDenNgay == "" ? DateTime.Now.ToShortDateString() : mDenNgay;
            this.pLoaiPhieu = mLoaiPhieu;
            this.pACT_ROLE = mActRole;

            Dlg = Utils.shwWait();

            setUserRight();
            if (isAddNew)
            {
                fdoAdd();
            }
            else
            {
                //BindData();
                //MTGlobal.SetGridReadOnly(grdPhieuNhap, tblView, true);
                setReadOnly(true);
            }

            Utils.closeWait(Dlg);
        }

        private void BindData()
        {
            otblPTCCT = new MTSQLServer().wRead(String.Format("SELECT * FROM TC_PHIEUTCCT where Phieutcid='{0}' ORDER BY Phieutcctid asc", pPhieuTCID), null, false);
            grdPhieuThuCT.ItemsSource = otblPTCCT;
            MTGlobal.SetFormatGridControl(grdPhieuThuCT, tblView, false);
        }

        private void fdoAdd()
        {
            try
            {
                int mMM = int.Parse(Convert.ToDateTime(pDenNgay).Month.ToString().PadLeft(2, '0'));
                int mYY = int.Parse(Convert.ToDateTime(pDenNgay).Year.ToString());
                pPhieuTCID = MTGlobal.GetNewID();
                txtSoPhieu.Text = MTGlobal.GetSophieu(MTGlobal.MT_KYHIEU_CUAHANG, this.pLoaiPhieu, mMM, mYY, MTGlobal.MT_USER_LOGIN).ToString();
                txtNgayCT.Text = DateTime.Now.ToShortDateString();
                txtNgayPS.Text = DateTime.Now.ToShortDateString();

                BindData();
                if (MTGlobal.SetGridReadOnly(grdPhieuThuCT, tblView, false))
                {
                    tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                    tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                    tblView.FocusedColumn = grdPhieuThuCT.Columns.First();
                    tblView.ShowEditor();
                }

                setReadOnly(false);
                MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "ADD");
                txtSoPhieu.Focus();
                isEdit = true;
            }
            catch (Exception ex) { Utils.showMessage(ex.Message.ToString(), "Thông báo lỗi"); }
        }

        private void setUserRight()
        {
            this.doAdd.IsEnabled = pACT_ROLE.isAdd;
            this.doEdit.IsEnabled = pACT_ROLE.isEdit;
            this.doDel.IsEnabled = pACT_ROLE.isDel;
            MTButton.cmdAdd = this.doAdd;
            MTButton.cmdEdit = this.doEdit;
            MTButton.cmdDel = this.doDel;
            MTButton.cmdSave = this.doSave;
            MTButton.cmdAbort = this.doAbort;

            MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "INIT");
        }

        private void setReadOnly(bool isReadonly = false)
        {
            
        }

        private void doPrint_Click(object sender, RoutedEventArgs e)
        {
            if (pPhieuTCID == "")
            {
                Utils.showMessage("Bạn chưa chọn phiếu để in...", "Lưu ý");
            }
           // new MTReport().rptNX_Phieunhap(pPhieuTCID);
        }
        private void doAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fdoAdd();
            }
            catch (Exception ex) { Utils.showMessage(ex.Message.ToString(), "Thông báo lỗi"); }
        }

        private void doEdit_Click(object sender, RoutedEventArgs e)
        {
            //fdoEdit();
        }
        private void doSave_Click(object sender, RoutedEventArgs e)
        {
           // fdoSave();
        }
        private void doDel_Click(object sender, RoutedEventArgs e)
        {
            if (pPhieuTCID == "")
            {
                Utils.showMessage("Bạn chưa chọn phiếu cần xóa..", "Lưu ý");
                return;
            }
            if (Utils.ConfirmMessage("Bạn có chắc muốn xóa phiếu này?", "Xác nhận"))
            {
                if (new modPhieuNX().DelPhieuNX(pPhieuTCID))
                {
                    pPhieuTCID = "";
                    BindData();
                    setReadOnly(true);
                    MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "ABORT");
                }
                else
                {
                    Utils.showMessage("Không thể xóa phiếu, vui lòng kiểm tra lại", "Thông báo");
                }

            }
        }
        private void doAbort_Click(object sender, RoutedEventArgs e)
        {
            BindData();
            setReadOnly(true);
            MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "ABORT");
        }

        private void doExit_Click(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread vThread = new System.Threading.Thread(() =>
            {
                try
                {
                    MTSynData.doSyncPost();
                }
                catch { }
            });
            vThread.Start();
            this.Close();
        }

        private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread vThread = new System.Threading.Thread(() =>
            {
                try
                {
                    MTSynData.doSyncPost();
                }
                catch { }
            });
            vThread.Start();
            this.Close();
        }

        private void Grid_PreviewKeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                doAdd_Click(sender, e);
            }
            else if (e.Key == Key.F3)
            {
                doEdit_Click(sender, e);
            }
            else if (e.Key == Key.F5)
            {
                doSave_Click(sender, e);
            }
            else if (e.Key == Key.F7)
            {
                doPrint_Click(sender, e);
            }
            else if (e.Key == Key.F8)
            {
                doExit_Click(sender, e);
            }
            else if (e.Key == Key.Escape)
            {
                doAbort_Click(sender, e);
            }

            if (e.Key == Key.Enter)
            {
            }
        }

        private void txtMaDT_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtHoTen_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtDiaChi_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtMaLD_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                String maLD = txtMaLD.Text.ToString().Trim();

                if (maLD.Length == 0)
                {
                    txtMaLD.Focus();
                    return;
                }
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void TextEdit_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                
            }
        }

        private void txtGhiChu_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtNgayCT_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtSoCT_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtNgayPS_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtNgayPS.Text == "")
                {
                    txtNgayPS.Focus();
                    return;
                }
                e.Handled = true;
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void cbNguyenTe_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtTyGia_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtMaLD_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (isEdit && txtMaLD.Text != "" && txtMaLD.Text != null)
            {
                String mLydo = new MTSQLServer().getNameByValue("SELECT Lydo FROM DM_LYDO", "Mald", txtMaLD.Text.Trim(), "Lydo");
                if (mLydo == "" || mLydo == null)
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("LYDO");
                    oDM.ShowDialog();
                    txtMaLD.Text = oDM.pMaso;
                    txtLD.Text = oDM.pGiaTriChon;
                }
                else
                {
                    txtLD.Text = mLydo;
                }
            }
            if (txtMaLD.Text == "") { txtLD.Text = ""; }
        }

        private void txtMaDT_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (isEdit && txtMaDT.Text != "" && txtMaDT.Text != null)
            {
                String mTENKH = new MTSQLServer().getNameByValue("SELECT Tenkh FROM DM_KHACHHANG", "Makh", txtMaDT.Text.Trim(), "Tenkh");
                if (mTENKH == "" || mTENKH == null)
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("DV");
                    oDM.ShowDialog();
                    txtMaDT.Text = oDM.pMaso;
                    txtTenDoiTuong.Text = oDM.pGiaTriChon;
                    txtHoTen.Text = oDM.pGiaTriChon;
                }
                else
                {
                    txtTenDoiTuong.Text = mTENKH;
                }
            }
            if (txtMaDT.Text == "") { txtTenDoiTuong.Text = ""; }
        }

        private void txtTenDoiTuong_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtLD_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

    }
}
