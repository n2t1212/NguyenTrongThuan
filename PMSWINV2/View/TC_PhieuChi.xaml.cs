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
    public partial class TC_PhieuChi : Window
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

        // TK nợ, TK có
        private String TKNo = "";
        private String TKCo = "";

        public TC_PhieuChi()
        {
            InitializeComponent();
        }

        public TC_PhieuChi(String mPhieuTCID, String mLoaiPhieu, String mTuNgay, String mDenNgay,
            MTGlobal.MT_ROLE mActRole, bool isAddNew = false)
        {
            InitializeComponent();
            this.lblTitle.Content = "LẬP PHIẾU CHI";
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
                BindData();
                MTGlobal.SetGridReadOnly(grdPhieuThuCT, tblView, true);
                setReadOnly(true);
            }

            Utils.closeWait(Dlg);
        }

        private void BindData()
        {
            cbNguyenTe.EditValue = "VND";
            txtMaDT.Text = "";
            txtTenDoiTuong.Text = "";
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtMaLD.Text = "";
            txtLD.Text = "";
            txtManv.Text = "";
            txtTennv.Text = "";
            txtGhiChu.Text = "";

            if (pPhieuTCID != "")
            {
                String mSql = String.Format("select A.*, B.Tenkh, B.Diachi, C.Lydo, C.TKNo, C.TKCo, D.Tennv from TC_PHIEUTC A left join DM_KHACHHANG B on A.Madt = B.Makh left join DM_LYDO C on A.Mald = C.Mald left join DM_NHANVIEN D on A.Mant = D.Manv where A.Phieutcid = '{0}'", pPhieuTCID);

                otblPTC = new MTSQLServer().wRead(mSql, null, false);

                if (otblPTC != null)
                {
                    foreach (DataRow vR in otblPTC.Rows)
                    {
                        txtSoPhieu.Text = vR["Sophieu"].ToString();
                        txtMaDT.Text = vR["Madt"].ToString();
                        txtTenDoiTuong.Text = vR["Tenkh"].ToString();
                        txtDiaChi.Text = vR["Diachi"].ToString();
                        txtHoTen.Text = vR["Hoten"].ToString();
                        txtMaLD.Text = vR["Mald"].ToString();
                        txtLD.Text = vR["Lydo"].ToString();
                        txtManv.Text = vR["Mant"].ToString();
                        txtTennv.Text = vR["Tennv"].ToString();
                        txtGhiChu.Text = vR["Ghichu"].ToString();
                        txtSoCT.Text = vR["Soctgoc"].ToString();
                        txtNgayCT.EditValue = vR["Ngayct"].ToString() != "" ? Convert.ToDateTime(vR["Ngayct"].ToString()).ToShortDateString() : "";
                        txtNgayPS.EditValue = vR["Ngayps"].ToString() != "" ? Convert.ToDateTime(vR["Ngayps"].ToString()).ToShortDateString() : "";
                        cbNguyenTe.EditValue = vR["Loaitien"].ToString();
                        txtTyGia.Text = vR["Tygia"].ToString();

                        TKNo = vR["TKNo"].ToString();
                        TKCo = vR["TKCo"].ToString();
                    }
                }
            }

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

        private void fdoEdit()
        {
            setReadOnly(false);
            MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "EDIT");
            isEdit = true;
            txtMaDT.Focus();
            if (MTGlobal.SetGridReadOnly(grdPhieuThuCT, tblView, false))
            {
                tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = grdPhieuThuCT.Columns.First();
                tblView.ShowEditor();
            }
        }

        private void fdoSave()
        {
            Double soTien = 0;
            if (fValidate())
            {
                DataTable tmpTCCT = new modPhieuTC().dtPhieuTCCT();
                var filteredRows = tblView.Grid.DataController.GetAllFilteredAndSortedRows();
                if (filteredRows.Count == 0)
                {
                    return;
                }

                tblView.CommitEditing();
                tblView.CloseEditor();
                otblPTCCT.AcceptChanges();

                foreach (DataRow sR in otblPTCCT.Rows)
                {
                    DataRow cR = tmpTCCT.NewRow();
                    cR["Phieutcctid"] = sR["Phieutcctid"];
                    cR["Phieutcid"] = pPhieuTCID;
                    cR["Macp"] = sR["Macp"];
                    cR["Madt"] = sR["Madt"];
                    cR["Matk"] = sR["Matk"];
                    cR["TKNo"] = sR["TKNo"];
                    cR["TKCo"] = sR["TKNo"];
                    cR["Nguyente"] = sR["Nguyente"];
                    cR["Thanhtien"] = sR["Thanhtien"];
                    cR["Diengiai"] = sR["Diengiai"];
                    cR["SoHD"] = sR["SoHD"];
                    cR["NChono"] = sR["NChono"];

                    soTien += Convert.ToDouble(sR["Thanhtien"]);

                    tmpTCCT.Rows.Add(cR);
                }
                tmpTCCT.AcceptChanges();

                DataTable tmpTC = new modPhieuTC().dtPhieuTC();
                DataRow vR = tmpTC.NewRow();
                vR["Phieutcid"] = pPhieuTCID;
                vR["Mant"] = "";
                vR["Madt"] = txtMaDT.Text.ToUpper();
                vR["Loaiphieu"] = "PC";
                vR["Sophieu"] = txtSoPhieu.Text;
                vR["Ngayct"] = Convert.ToDateTime(txtNgayCT.Text);
                vR["Ngayps"] = Convert.ToDateTime(txtNgayCT.Text);
                vR["Tygia"] = txtTyGia.Text.Length == 0 ? 0 : Double.Parse(txtTyGia.Text);
                vR["Soctgoc"] = txtSoCT.Text;
                vR["TKDu"] = "";
                vR["Nguyente"] = 0;
                vR["Sotien"] = soTien;
                vR["Hoten"] = txtHoTen.Text;
                vR["Nguoilap"] = txtManv.Text;
                vR["Ngaylap"] = DateTime.Now;
                vR["Nguoisua"] = "";
                vR["Ghiso"] = 1;
                vR["Mald"] = txtMaLD.Text;
                vR["Ghichu"] = txtGhiChu.Text;
                vR["Loaitien"] = cbNguyenTe.EditValue;

                tmpTC.Rows.Add(vR);
                tmpTC.AcceptChanges();

                String Rs = new modPhieuTC().SavePhieuTC(tmpTC, tmpTCCT);
                if (Rs == "")
                {
                    setReadOnly(true);
                    MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "SAVE");
                    MTGlobal.SetGridReadOnly(grdPhieuThuCT, tblView, true);
                    isEdit = false;
                }
                else
                {
                    Utils.showMessage(Rs, "Thông báo");
                }
            }
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
            fdoEdit();
        }
        private void doSave_Click(object sender, RoutedEventArgs e)
        {
            fdoSave();
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
                    txtDiaChi.Text = oDM.pDiaChi;
                }
                else
                {
                    txtTenDoiTuong.Text = mTENKH;
                    txtHoTen.Text = mTENKH;
                    txtDiaChi.Text = new MTSQLServer().getNameByValue("SELECT Diachi FROM DM_KHACHHANG", "Makh", txtMaDT.Text.Trim(), "Diachi");
                }

                txtDiaChi.Focus();
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
                    TKNo = oDM.pTKNo;
                    TKCo = oDM.pTKCo;
                }
                else
                {
                    txtLD.Text = mLydo;
                }
            }
            if (txtMaLD.Text == "") { txtLD.Text = ""; }
        }

        private void txtManv_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtManv_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (isEdit && txtManv.Text != "" && txtMaLD.Text != null)
            {
                String mManv = new MTSQLServer().getNameByValue("SELECT Manv FROM DM_NHANVIEN", "Manv", txtManv.Text.Trim(), "Tennv");
                if (mManv == "" || mManv == null)
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("NHANVIEN");
                    oDM.ShowDialog();
                    txtManv.Text = oDM.pMaso;
                    txtTennv.Text = oDM.pGiaTriChon;
                }
                else
                {
                    txtTennv.Text = mManv;
                }
            }
            if (txtManv.Text == "") { txtTennv.Text = ""; }
        }

        private void txtTennv_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void tblView_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tblView.FocusedColumn.FieldName == "TKNo" && tblView.FocusedColumn.Name == "colTKNo")
                {
                    if (grdPhieuThuCT.GetFocusedRowCellValue(colTKNo) == null || grdPhieuThuCT.GetFocusedRowCellValue(colTKNo).ToString() == "")
                    {
                        tblView.FocusedColumn = colTKNo;
                        return;
                    }
                }

                if (tblView.FocusedColumn.FieldName == "TKCo" && tblView.FocusedColumn.Name == "colTKCo")
                {
                    if (grdPhieuThuCT.GetFocusedRowCellValue(colTKCo) == null || grdPhieuThuCT.GetFocusedRowCellValue(colTKCo).ToString() == "")
                    {
                        tblView.FocusedColumn = colTKCo;
                        return;
                    }
                }

                if (tblView.FocusedColumn.FieldName == "Diengiai" && tblView.FocusedColumn.Name == "colDiengiai")
                {
                    grdPhieuThuCT.SetFocusedRowCellValue(colTKNo, TKNo);
                    grdPhieuThuCT.SetFocusedRowCellValue(colTKCo, TKCo);
                    tblView.FocusedColumn = colTKCo;
                }
            }
        }

        private void tblView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {
            grdPhieuThuCT.SetCellValue(e.RowHandle, colPhieutcctid, MTGlobal.GetNewID());
        }

        private bool fValidate()
        {
            if (txtMaDT.Text == "")
            {
                Utils.showMessage("Đối tượng không được bỏ trống..", "Thông báo");
                txtMaDT.Focus();
                return false;
            }

            if (txtMaLD.Text == "")
            {
                Utils.showMessage("Lý do không được bỏ trống..", "Thông báo");
                txtMaLD.Focus();
                return false;
            }

            if (txtSoPhieu.Text == "")
            {
                Utils.showMessage("Số phiếu chưa được khởi tạo..", "Thông báo");
                txtSoPhieu.Focus();
                return false;
            }

            if (txtNgayCT.Text == "")
            {
                Utils.showMessage("Ngày chứng từ không được bỏ trống..", "Thông báo");
                txtNgayCT.Focus();
                return false;
            }

            if (txtNgayPS.Text == "")
            {
                Utils.showMessage("Ngày hoạch toán không được bỏ trống..", "Thông báo");
                txtNgayPS.Focus();
                return false;
            }

            return true;
        }

        private void btnAddNV_Click(object sender, RoutedEventArgs e)
        {
            dlg_ThemNhanVien frmAddNV = new dlg_ThemNhanVien();
            frmAddNV.ShowDialog();

            String maNV = frmAddNV.maNV;
            String tenNV = frmAddNV.tenNV;

            if (maNV.Length > 0 && tenNV.Length > 0)
            {
                txtManv.Text = maNV;
                txtTennv.Text = tenNV;
            }
            else
            {
                txtManv.Focus();
            }
        }

        private void btnAddDT_Click(object sender, RoutedEventArgs e)
        {
            dlg_AddKhachHang frmAddKH = new dlg_AddKhachHang();
            frmAddKH.ShowDialog();

            String maDT = frmAddKH.maKH;
            String tenDT = frmAddKH.tenKH;

            if (maDT.Length > 0 && maDT.Length > 0)
            {
                txtMaDT.Text = maDT;
                txtTenDoiTuong.Text = tenDT;
                txtHoTen.Text = tenDT;
                txtDiaChi.Text = frmAddKH.DiaChi;
            }
            else
            {
                txtMaDT.Focus();
            }
        }
    }
}
