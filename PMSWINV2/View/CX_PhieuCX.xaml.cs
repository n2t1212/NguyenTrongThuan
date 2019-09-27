using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.XtraEditors;
using MTPMSWIN.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MTPMSWIN.View
{
 
    public partial class CX_PhieuCX : Window
    {
        MTGlobal.MT_ROLE pACT_ROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        private string pPhieuID = "";
        private string pDenNgay = "";
        private string pTuNgay = "";
        private string pLoaiPhieu = "PN";

        private DataTable otblCX = null;
        private DataTable otblCXCT = null;
        private bool isEdit = false;
        DevExpress.Utils.WaitDialogForm Dlg;

        private DataTable otblSP = null;

        public CX_PhieuCX()
        {
            InitializeComponent(); 
        }

        public CX_PhieuCX(String mPhieuID, String mLoaiPhieu, String mTuNgay, String mDenNgay, MTGlobal.MT_ROLE mActRole, bool isAddNew = false)
        {
            InitializeComponent();         
            
            this.lblTitle.Content = "LÂP PHIẾU GỬI CHÀNH";
            this.pPhieuID = mPhieuID;
            this.pTuNgay = mTuNgay == "" ? DateTime.Now.ToShortDateString() : mTuNgay;
            this.pDenNgay = mDenNgay == "" ? DateTime.Now.ToShortDateString() : mDenNgay;
            this.pLoaiPhieu = mLoaiPhieu;
            this.pACT_ROLE = mActRole;

            Dlg = Utils.shwWait();
            setUserRight(); 
            if (isAddNew){
                fdoAdd();
            }
            else {
                BindData();
                MTGlobal.SetGridReadOnly(grdPhieu,tblView, true);
                setReadOnly(true);
            }            
            System.Threading.Thread oThreSP = new System.Threading.Thread(LoadSanPham);
            oThreSP.Start();

            Utils.closeWait(Dlg);
        }
                
        private void setUserRight() {
            this.doAdd.IsEnabled = pACT_ROLE.isAdd;
            this.doEdit.IsEnabled=pACT_ROLE.isEdit;
            this.doDel.IsEnabled=pACT_ROLE.isDel;
            MTButton.cmdAdd = this.doAdd;
            MTButton.cmdEdit = this.doEdit;
            MTButton.cmdDel = this.doDel;
            MTButton.cmdSave = this.doSave;
            MTButton.cmdAbort = this.doAbort;
            MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "INIT");
        }

        private void setReadOnly(bool isReadonly=false) {
            dtpNgaylap.IsReadOnly = isReadonly;
            dtpNgayPS.IsReadOnly = isReadonly;

            txtTrongluong.IsReadOnly = isReadonly;
            txtChiphi.IsReadOnly = isReadonly;
            cbThanhtoan.IsReadOnly = isReadonly;
            txtMadv.IsReadOnly = isReadonly;
            txtTendonvi.IsReadOnly = isReadonly;
            txtDienthoai.IsReadOnly = isReadonly;
            txtDiachi.IsReadOnly = isReadonly;
            txtSoxe.IsReadOnly = isReadonly;
            txtLoaixe.IsReadOnly = isReadonly;
            txtTaixe.IsReadOnly = isReadonly;
            txtDienthoaiXe.IsReadOnly = isReadonly;
            txtGhichu.IsReadOnly = isReadonly;
        }

        private void LoadSanPham() {
            try
            {
                String mSql = "SELECT Maspid,Manhomspid,Masp,Tensp,Dvt,Quycach,Quydoikgl,Quydoithung,Nhacungcap FROM DM_SANPHAM with(nolock) order by Masp asc";
                otblSP = new MTSQLServer().wRead(mSql, null, false);
            }
            catch (Exception ex) { } 
        }

        private void BindData(bool isDetailOnly=false) {
            try{
                if (isDetailOnly){
                    otblCXCT = new MTSQLServer().wRead(String.Format("SELECT A.*,B.Tensp,B.Dvt,B.Quycach FROM NX_CHANHXECT A LEFT JOIN DM_SANPHAM B ON A.Masp=B.Masp WHERE Chanhxeid='{0}' ORDER BY Chanhxectid ASC", pPhieuID), null, false);
                    grdPhieu.ItemsSource = otblCXCT;
                    MTGlobal.SetFormatGridControl(grdPhieu, tblView, false);
                }
                else
                {
                    txtTrongluong.Text = "";
                    txtChiphi.Text = "";
                    cbThanhtoan.Text = "";
                    txtMadv.Text = "";
                    txtTendonvi.Text = "";
                    txtDienthoai.Text = "";
                    txtDiachi.Text = "";
                    txtSoxe.Text = "";
                    txtLoaixe.Text = "";
                    txtTaixe.Text = "";
                    txtDienthoaiXe.Text = "";
                    txtGhichu.Text = "";
                    if (pPhieuID != "")
                    {
                        String mSql = String.Format("SELECT * FROM NX_CHANHXE WHERE Chanhxeid='{0}'", pPhieuID);
                        otblCX = new MTSQLServer().wRead(mSql, null, false);
                        if (otblCX != null)
                        {
                            foreach (DataRow vR in otblCX.Rows)
                            {
                                txtSophieu.Text = vR["Sophieu"].ToString();
                                dtpNgaylap.EditValue = vR["Ngayct"].ToString() != "" ? Convert.ToDateTime(vR["Ngayct"].ToString()).ToShortDateString() : "";
                                dtpNgayPS.EditValue = vR["Ngayps"].ToString() != "" ? Convert.ToDateTime(vR["Ngayps"].ToString()).ToShortDateString() : "";

                                txtTrongluong.Text = vR["Trongluong"].ToString();
                                txtChiphi.Text = vR["Chiphi"].ToString();
                                cbThanhtoan.Text = vR["HTThanhtoan"].ToString();
                                txtMadv.Text = vR["Makh"].ToString();
                                txtTendonvi.Text = vR["Tenkh"].ToString();
                                txtDienthoai.Text = vR["Dienthoai"].ToString();
                                txtDiachi.Text = vR["Diachi"].ToString();
                                txtSoxe.Text = vR["Soxe"].ToString();
                                txtLoaixe.Text = vR["Loaixe"].ToString();
                                txtTaixe.Text = vR["Taixe"].ToString();
                                txtDienthoaiXe.Text = vR["Dienthoaixe"].ToString();
                                txtGhichu.Text = vR["Ghichu"].ToString();
                            }
                        }

                    }

                    otblCXCT = new MTSQLServer().wRead(String.Format("SELECT A.*,B.Tensp,B.Dvt,B.Quycach FROM NX_CHANHXECT A LEFT JOIN DM_SANPHAM B ON A.Masp=B.Masp WHERE Chanhxeid='{0}' ORDER BY Chanhxectid ASC", pPhieuID), null, false);
                    grdPhieu.ItemsSource = otblCXCT;
                    MTGlobal.SetFormatGridControl(grdPhieu, tblView, false);
                }
            }
            catch { }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch { }
        }

        private void fQuydoithung(int iRowHander){
            try{
                String sMasp = "";
                sMasp = grdPhieu.GetCellValue(iRowHander, colMasp).ToString();
                if (otblSP != null && otblSP.Rows.Count > 0)
                {
                    DataRow[] vRow = otblSP.Select(string.Format("Masp='{0}'", sMasp));
                    if (vRow.Length > 0)
                    {
                        Double dSLThung = 0, dQDThung = 0, dSLQD = 0;
                        Double.TryParse(vRow[0]["Quydoithung"].ToString(), out dQDThung);
                        Double.TryParse(grdPhieu.GetCellValue(iRowHander, colSoThung).ToString(), out dSLThung);
                        dSLQD = Math.Round(dSLThung * dQDThung, 0, MidpointRounding.AwayFromZero);
                        grdPhieu.SetCellValue(iRowHander, colSoLuong, dSLQD);
                    }
                }
            }
            catch { }
        }

        private bool fValidate() {
            if (txtSophieu.Text == "")
            {
                Utils.showMessage("Số phiếu chưa được khởi tạo..", "Thông báo");
                txtSophieu.Focus();
                return false;
            }
            if (dtpNgaylap.Text == "")
            {
                Utils.showMessage("Ngày chứng từ không thể bỏ trống..", "Thông báo");
                dtpNgaylap.Focus();
                return false;
            }
            if (txtMadv.Text == "")
            {
                Utils.showMessage("Bạn chưa nhập mã khách hàng..", "Thông báo");
                txtMadv.Focus();
                return false;
            }

            if (txtChiphi.Text == "" || txtChiphi.Text=="0")
            {
                Utils.showMessage("Bạn chưa nhập chi phí gửi..", "Thông báo");
                txtChiphi.Focus();
                return false;
            }

            if (txtSoxe.Text == "")
            {
                Utils.showMessage("Bạn chưa nhập mã khách hàng..", "Thông báo");
                txtSoxe.Focus();
                return false;
            }
            if (grdPhieu.VisibleRowCount <= 0) {
                Utils.showMessage("Bạn chưa nhập chi tiết hàng hóa..", "Thông báo");
                tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = grdPhieu.Columns.First();
                tblView.ShowEditor();
                return false;
            }
            return true;
        }

        private void fdoAdd() {
            try{
                int mMM = int.Parse(Convert.ToDateTime(pDenNgay).Month.ToString().PadLeft(2, '0'));
                int mYY = int.Parse(Convert.ToDateTime(pDenNgay).Year.ToString());
                pPhieuID = MTGlobal.GetNewID();
                txtSophieu.Text = MTGlobal.GetSophieu(MTGlobal.MT_KYHIEU_CUAHANG, pLoaiPhieu, mMM, mYY, MTGlobal.MT_USER_LOGIN).ToString();
                dtpNgaylap.Text = DateTime.Now.ToShortDateString();
                dtpNgayPS.Text = DateTime.Now.ToShortDateString();                
              
                BindData();
                if (MTGlobal.SetGridReadOnly(grdPhieu,tblView, false))
                {
                    tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                    tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                    tblView.FocusedColumn = grdPhieu.Columns.First();
                    tblView.ShowEditor();
                }

                setReadOnly(false);
                MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "ADD");
                txtSophieu.Focus();
                isEdit = true;
            }
            catch (Exception ex) { Utils.showMessage(ex.Message.ToString(), "Thông báo lỗi"); }
        }

        private void fdoEdit() {
            setReadOnly(false);
            MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "EDIT");
            isEdit = true;
            txtSophieu.Focus();
            if (MTGlobal.SetGridReadOnly(grdPhieu,tblView, false))
            {
                tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = grdPhieu.Columns.First();
                tblView.ShowEditor();
            }
        }
        private void fdoSave() {
            if (fValidate()){
                DataTable tmpCX = new modChanhxe().dtPhieu();
                DataRow vR = tmpCX.NewRow();

                Double dTrongluong = 0, dChiphi = 0;
                Double.TryParse(txtTrongluong.Text, out dTrongluong);
                Double.TryParse(txtChiphi.Text, out dChiphi);

                vR["Chanhxeid"] = pPhieuID;
                vR["Sophieu"] = txtSophieu.Text.ToUpper();
                vR["Loaiphieu"] = this.pLoaiPhieu;
                vR["Ngayct"] = Convert.ToDateTime(dtpNgaylap.Text);
                vR["Ngayps"] = Convert.ToDateTime(dtpNgayPS.Text);

                vR["Makh"] =txtMadv.Text;
                vR["Tenkh"] = txtTendonvi.Text;
                vR["Diachi"] = txtDiachi.Text;
                vR["Dienthoai"] = txtDienthoai.Text;

                vR["Soxe"] = txtSoxe.Text;
                vR["Loaixe"] = txtLoaixe.Text;
                vR["Taixe"] = txtTaixe.Text;
                vR["Dienthoaixe"] = txtDienthoaiXe.Text;

                vR["Trongluong"] = Math.Round(dTrongluong,0,MidpointRounding.AwayFromZero);
                vR["Chiphi"] = Math.Round(dChiphi, 0, MidpointRounding.AwayFromZero) ;
                vR["HTThanhtoan"] = cbThanhtoan.Text;
                vR["Trangthai"] = 0;
                vR["Ghichu"] = txtGhichu.Text;

                tmpCX.Rows.Add(vR);
                tmpCX.AcceptChanges();

                DataTable tmpCXCT = new modChanhxe().dtPhieuCT();
                tblView.CommitEditing();
                tblView.CloseEditor();
                otblCXCT.AcceptChanges();                
                foreach(DataRow sR in otblCXCT.Rows){                  
                    DataRow cR = tmpCXCT.NewRow();
                    cR["Chanhxectid"] = sR["Chanhxectid"];
                    cR["Chanhxeid"] = pPhieuID;
                    cR["Maspid"] = sR["Maspid"];
                    cR["Masp"] = sR["Masp"];
                    cR["SLThung"] = sR["SLThung"];
                    cR["Soluong"] = sR["Soluong"];
                    cR["Ghichu"] = sR["Ghichu"];                 
                    tmpCXCT.Rows.Add(cR);
                }
                tmpCXCT.AcceptChanges();

                String Rs = new modChanhxe().SavePhieu(tmpCX, tmpCXCT);
                if (Rs == ""){
                    BindData(true);
                    setReadOnly(true);
                    MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "SAVE");
                    MTGlobal.SetGridReadOnly(grdPhieu,tblView, true);                  
                    isEdit = false;
                }else {
                    Utils.showMessage(Rs, "Thông báo");
                }               
            }
        }

        
        private void doPrint_Click(object sender, RoutedEventArgs e)
        {
            if (pPhieuID == "")
            {
                Utils.showMessage("Bạn chưa chọn phiếu để in...", "Lưu ý");
            }
            new MTReport().rptNX_Chanhxe(pPhieuID);
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
            if (pPhieuID == ""){
                Utils.showMessage("Bạn chưa chọn phiếu cần xóa..", "Lưu ý");
                return;
            }
            if (Utils.ConfirmMessage("Bạn có chắc muốn xóa phiếu này?", "Xác nhận"))
            {
                if (new modChanhxe().DelPhieu(pPhieuID))
                {
                    pPhieuID = "";
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
            this.Close();
        }

        private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tblView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
        }

        private void tblView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e){ 
            try{
                grdPhieu.SetCellValue(e.RowHandle, colChanhxectid, MTGlobal.GetNewID());
                grdPhieu.SetCellValue(e.RowHandle, colChanhxeid,pPhieuID); 
            }
            catch { }
        }
                
        private void tblView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            e.Source.PostEditor();           
        }

         
        private void tblView_CellValueChanged(object sender, CellValueChangedEventArgs e){             
            if (isEdit == false) { return; }
            if (tblView.FocusedColumn.FieldName == "SLThung" && e.Column.Name == "colSoThung")
            {
                fQuydoithung(e.RowHandle);
            }
        }
         
        private void tblView_InvalidRowException(object sender, DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText!="")
            {
                Utils.showMessage(e.ErrorText, "Lưu ý", "ERR");
            }
            e.ExceptionMode = DevExpress.Xpf.Grid.ExceptionMode.NoAction;            
        }

        private void tblView_ValidateRow(object sender, DevExpress.Xpf.Grid.GridRowValidationEventArgs e)
        {
            if (e.Row == null) return;
            if (grdPhieu.GetCellValue(e.RowHandle, colMasp) == null || grdPhieu.GetCellValue(e.RowHandle, colMasp).ToString()=="")
            {
                e.SetError("Bạn chưa nhập mã hàng hóa..");
                e.IsValid = false;
                return;
            }

            double dSL=0;            
            try{              
                Double.TryParse(grdPhieu.GetCellValue(e.RowHandle, colSoLuong).ToString(), out dSL);
            }
            catch { }
            if (dSL <= 0) {
                e.SetError("Bạn chưa nhập đơn giá hàng hóa..");
                e.IsValid = false;
                return;
            }  
        }



        #region "KEYDOWN"
        private void Grid_PreviewKeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F2) {
                doAdd_Click(sender, e);
            }
            else if (e.Key == Key.F3) {
                doEdit_Click(sender, e);
            }
            else if (e.Key == Key.F5) {
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
            else if (e.Key == Key.Escape) {
                doAbort_Click(sender, e);
            } 
        }
     
        private void txtSophieu_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtSophieu.Text != "")
                {
                    e.Handled = true;
                    (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); 
                }
            }
          
        }
        private void dtpNgaylap_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (dtpNgaylap.Text == ""){                 
                    dtpNgaylap.Focus();
                    return;
                }
                dtpNgayPS.Text = Convert.ToDateTime(dtpNgaylap.Text).ToShortDateString();
                e.Handled = true;
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); 
            }
        }
        private void dtpNgayPS_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (dtpNgayPS.Text == "")
                {
                    dtpNgayPS.Focus();
                    return;
                }
                else if (dtpNgayPS.Text != "" && dtpNgaylap.Text != "")
                {
                    System.Globalization.CultureInfo oCul = new System.Globalization.CultureInfo("ur-PK");
                    if (Convert.ToDateTime(dtpNgaylap.Text) < Convert.ToDateTime(dtpNgayPS.Text))
                    {
                        Utils.showMessage("Ngày phát sinh không thể sau ngày lập..", "Lưu ý");
                        dtpNgayPS.Focus();
                    }
                }
                e.Handled = true;
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); 
            }
        }


        private void txtTrongluong_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtChiphi_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void cbThanhtoan_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtMadv_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
        private void txtTendonvi_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtDienthoai_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtDiachi_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtSoxe_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            } 
        }
        private void txtLoaixe_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            } 
        }
        
        private void txtTaixe_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            } 
        }
        private void txtDienthoaiXe_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            } 
        }

        private void txtGhichu_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                if (isEdit == true)
                {
                    grdPhieu.Focus();
                    tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                    tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                    tblView.FocusedColumn = colMasp;
                    tblView.ShowEditor();
                }
            }          
        }

        private void txtDienthoai_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }

        private void txtDienthoaiXe_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }
    
    #endregion

        #region "LOST FOCUS"

        private void txtSoxe_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isEdit && txtSoxe.Text != null && txtSoxe.Text != ""){
                DataTable otblXe = new MTSQLServer().wRead(String.Format("SELECT TOP 1 Soxe,Loaixe,Taixe,Dienthoai FROM DM_XE  WHERE Soxe='{0}'",txtSoxe.Text),null,false );
                if (otblXe !=null && otblXe.Rows.Count>0)
                {
                    foreach (DataRow vR in otblXe.Rows) {
                        txtSoxe.Text = vR["Soxe"].ToString();
                        txtLoaixe.Text = vR["Loaixe"].ToString();
                        txtTaixe.Text = vR["Taixe"].ToString();
                        txtDienthoaiXe.Text = vR["Dienthoai"].ToString();
                        return;
                    }
                }else{
                    dlg_ChonDM oDM = new dlg_ChonDM("XE");
                    oDM.ShowDialog();
                    DataRowView vRw = oDM.pRowChon;
                    if (vRw != null && vRw.Row.ItemArray.Length > 0)
                    {
                        txtSoxe.Text = vRw.Row.ItemArray.GetValue(1).ToString().ToUpper();
                        txtLoaixe.Text = vRw.Row.ItemArray.GetValue(2).ToString().ToUpper();
                        txtTaixe.Text = vRw.Row.ItemArray.GetValue(3).ToString().ToUpper();
                        txtDienthoaiXe.Text = vRw.Row.ItemArray.GetValue(4).ToString().ToUpper();                        
                    } 
                } 
            }
            if (txtSoxe.Text == "" && txtDienthoaiXe.Text == "") { txtLoaixe.Text = ""; txtTaixe.Text = ""; }            
        }
        private void txtDienthoaiXe_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSoxe.Text != "") { return; }

            if (isEdit && txtDienthoaiXe.Text != null && txtDienthoaiXe.Text != "")
            {
                DataTable otblXe = new MTSQLServer().wRead(String.Format("SELECT TOP 1 Soxe,Loaixe,Taixe,Dienthoai FROM DM_XE  WHERE Soxe='{0}'", txtSoxe.Text), null, false);
                if (otblXe != null && otblXe.Rows.Count > 0)
                {
                    foreach (DataRow vR in otblXe.Rows)
                    {
                        txtSoxe.Text = vR["Soxe"].ToString();
                        txtLoaixe.Text = vR["Loaixe"].ToString();
                        txtTaixe.Text = vR["Taixe"].ToString();
                        txtDienthoaiXe.Text = vR["Dienthoai"].ToString();
                        return;
                    }
                }
                else
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("XE");
                    oDM.ShowDialog();
                    DataRowView vRw = oDM.pRowChon;
                    if (vRw != null && vRw.Row.ItemArray.Length > 0)
                    {
                        txtSoxe.Text = vRw.Row.ItemArray.GetValue(1).ToString().ToUpper();
                        txtLoaixe.Text = vRw.Row.ItemArray.GetValue(2).ToString().ToUpper();
                        txtTaixe.Text = vRw.Row.ItemArray.GetValue(3).ToString().ToUpper();
                        txtDienthoaiXe.Text = vRw.Row.ItemArray.GetValue(4).ToString().ToUpper();
                    }
                }
            }
            if (txtSoxe.Text == "" && txtDienthoaiXe.Text == "") { txtLoaixe.Text = ""; txtTaixe.Text = ""; }   
        }
         
        private void txtMadv_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isEdit && txtMadv.Text != "" && txtMadv.Text != null)
            {
                DataTable otblDV = new MTSQLServer().wRead(String.Format("SELECT TOP 1 Makh,Tenkh,Dienthoai,Diachi FROM DM_KHACHHANG WHERE makh='{0}'", txtSoxe.Text), null, false);
                if (otblDV != null && otblDV.Rows.Count > 0)
                {
                    foreach (DataRow vR in otblDV.Rows)
                    {
                        txtMadv.Text = vR["Makh"].ToString();
                        txtTendonvi.Text = vR["Tenkh"].ToString();
                        txtDienthoai.Text = vR["Dienthoai"].ToString();
                        txtDiachi.Text = vR["Diachi"].ToString();
                        return;
                    }
                }
                else
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("DV");
                    oDM.ShowDialog();
                    DataRowView vRw = oDM.pRowChon;
                    if (vRw != null && vRw.Row.ItemArray.Length > 0){                        
                        txtMadv.Text = vRw.Row.ItemArray.GetValue(1).ToString().ToUpper();
                        txtTendonvi.Text = vRw.Row.ItemArray.GetValue(2).ToString().ToUpper();
                        txtDienthoai.Text = vRw.Row.ItemArray.GetValue(3).ToString().ToUpper();
                        txtDiachi.Text = vRw.Row.ItemArray.GetValue(4).ToString().ToUpper();
                    }
                } 
            }
            if (txtMadv.Text == "" && txtDienthoai.Text=="") { txtTendonvi.Text = ""; txtDiachi.Text = ""; }
        }
        private void txtDienthoai_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtMadv.Text != "") { return; }

            if (isEdit && txtMadv.Text != "" && txtMadv.Text != null)
            {
                DataTable otblDV = new MTSQLServer().wRead(String.Format("SELECT TOP 1 Makh,Tenkh,Dienthoai,Diachi FROM DM_KHACHHANG WHERE makh='{0}'", txtSoxe.Text), null, false);
                if (otblDV != null && otblDV.Rows.Count > 0)
                {
                    foreach (DataRow vR in otblDV.Rows)
                    {
                        txtMadv.Text = vR["Makh"].ToString();
                        txtTendonvi.Text = vR["Tenkh"].ToString();
                        txtDienthoai.Text = vR["Dienthoai"].ToString();
                        txtDiachi.Text = vR["Diachi"].ToString();
                        return;
                    }
                }
                else
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("DV");
                    oDM.ShowDialog();
                    DataRowView vRw = oDM.pRowChon;
                    if (vRw != null && vRw.Row.ItemArray.Length > 0)
                    {
                        txtMadv.Text = vRw.Row.ItemArray.GetValue(1).ToString().ToUpper();
                        txtTendonvi.Text = vRw.Row.ItemArray.GetValue(2).ToString().ToUpper();
                        txtDienthoai.Text = vRw.Row.ItemArray.GetValue(3).ToString().ToUpper();
                        txtDiachi.Text = vRw.Row.ItemArray.GetValue(4).ToString().ToUpper();
                    }
                }
            }
            if (txtMadv.Text == "" && txtDienthoai.Text=="") { txtTendonvi.Text = ""; txtDiachi.Text = ""; }
        }

      #endregion

        private void grdPhieu_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (isEdit == false) { return; }
            if (e.Key == Key.Enter){
                if (tblView.FocusedColumn.FieldName == "Masp" && tblView.FocusedColumn.Name == "colMasp")
                {
                    if (grdPhieu.GetFocusedRowCellValue(colMasp) == null || grdPhieu.GetFocusedRowCellValue(colMasp).ToString() == "")
                    {
                        tblView.FocusedColumn = colMasp;
                        return;
                    }

                    String sMasp = grdPhieu.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString();
                    if (sMasp == null || sMasp == "")
                    {
                        tblView.FocusedColumn = colMasp;
                        return;
                    }

                    if (sMasp != "")
                    {
                        double dSL = 0;
                        try
                        {
                            double.TryParse(grdPhieu.GetCellValue(tblView.FocusedRowHandle, colSoLuong).ToString(), out dSL);
                            if (dSL == 0) { dSL = 1; }
                        }
                        catch { dSL = 1; }

                        if (otblSP != null && otblSP.Rows.Count > 0)
                        {
                            DataRow[] vRow = otblSP.Select(string.Format("Masp='{0}'", sMasp));
                            if (vRow.Length > 0)
                            {
                                grdPhieu.SetCellValue(tblView.FocusedRowHandle, colSanpham, vRow[0]["Tensp"].ToString());
                                grdPhieu.SetCellValue(tblView.FocusedRowHandle, colDVT, vRow[0]["Dvt"].ToString());
                                grdPhieu.SetCellValue(tblView.FocusedRowHandle, colQC, vRow[0]["Quycach"].ToString());
                                grdPhieu.SetCellValue(tblView.FocusedRowHandle, colSoLuong, dSL);
                            }
                            else
                            {
                                dlg_ChonSanPham oChonSP = new dlg_ChonSanPham(otblSP);
                                oChonSP.ShowDialog();
                                DataRowView vRw = oChonSP.pRowChon;
                                if (vRw != null && vRw.Row.ItemArray.Length > 0)
                                {
                                    grdPhieu.SetCellValue(tblView.FocusedRowHandle, colSanpham, vRw.Row.ItemArray.GetValue(3).ToString());
                                    grdPhieu.SetCellValue(tblView.FocusedRowHandle, colDVT, vRw.Row.ItemArray.GetValue(4).ToString());
                                    grdPhieu.SetCellValue(tblView.FocusedRowHandle, colQC, vRw.Row.ItemArray.GetValue(5).ToString());
                                    sMasp = vRw.Row.ItemArray.GetValue(2).ToString().ToUpper();
                                    grdPhieu.SetCellValue(tblView.FocusedRowHandle, colSoLuong, dSL);
                                }
                                else
                                {
                                    tblView.FocusedColumn = colMasp;
                                }
                            }
                        }

                        tblView.CloseEditor();
                        tblView.ShowEditor();                        
                        grdPhieu.SetCellValue(tblView.FocusedRowHandle, colMasp, sMasp);
                       //tblView.PostEditor();
                    }
                }
            }

            if (e.Key == Key.Delete)
            {
                try
                {
                    if (grdPhieu.GetFocusedRowCellValue(colChanhxectid) == null || grdPhieu.GetFocusedRowCellValue(colChanhxectid).ToString() == "")
                    {
                        e.Handled = true;
                        return;
                    }
                    if (Utils.ConfirmMessage(String.Format("Bạn có chắc muốn xóa {0} không ?", grdPhieu.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString()), "Xác nhận xóa") == true)
                    {
                        tblView.DeleteRow(tblView.FocusedRowHandle);
                    }
                }
                catch (Exception ex) { ex.Data.Clear(); }
            }
        }

        

     
    }
}
