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
 
    public partial class NX_PhieuNhap : Window
    {
        MTGlobal.MT_ROLE pACT_ROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        private string pPhieuNXID = "";
        private string pDenNgay = "";
        private string pTuNgay = "";
        private string pLoaiPhieu = "";

        private DataTable otblPNX = null;
        private DataTable otblPNXCT = null;
        private bool isEdit = false;
        DevExpress.Utils.WaitDialogForm Dlg;

        private DataTable otblSP = null;
        

        public NX_PhieuNhap(String mPhieuNXID,String mLoaiPhieu,String mTuNgay, String mDenNgay,MTGlobal.MT_ROLE mActRole,bool isAddNew=false)
        {
            InitializeComponent();         
            
            this.lblTitle.Content = "LÂP PHIẾU NHẬP";
            this.pPhieuNXID = mPhieuNXID;
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
                MTGlobal.SetGridReadOnly(grdPhieuNhap,tblView, true);
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
            txtSoctgoc.IsReadOnly = isReadonly ;
            cbNguyente.IsReadOnly = isReadonly;
            txtTygia.IsReadOnly = isReadonly;
            txtMakho.IsReadOnly = isReadonly;
            txtMadv.IsReadOnly = isReadonly;
            txtMald.IsReadOnly = isReadonly;
            txtGhichu.IsReadOnly=isReadonly;
        }

        private void LoadSanPham() {
            try
            {
                String mSql = "SELECT Maspid,Manhomspid,Masp,Tensp,Dvt,Quycach,Quydoikgl,Quydoithung,Nhacungcap FROM DM_SANPHAM with(nolock) order by Masp asc";
                otblSP = new MTSQLServer().wRead(mSql,null,false);
            }
            catch (Exception ex) { } 
        }

        private void BindData() {
            try
            {
                cbNguyente.EditValue = "VND";
                txtTygia.Text = "1";
                txtMakho.Text = "";
                txtTenkho.Text = "";
                txtMadv.Text = "";
                txtTendonvi.Text = "";
                txtMald.Text = "";
                txtLydo.Text = "";
                txtNguoigiao.Text = "";
                txtGhichu.Text = "";

                if (pPhieuNXID != "")
                {
                    String mSql = String.Format("SELECT A.*,B.Tenkho,C.Tenkh,D.Lydo FROM NX_PHIEUNX A LEFT JOIN DM_KHO B ON A.Makho=B.Makho "
                                                 + "LEFT JOIN DM_KHACHHANG C ON A.Makh=c.Makh  LEFT JOIN DM_LYDO D ON A.Mald=d.Mald "+
                                                   " WHERE Phieunxid='{0}'", pPhieuNXID);

                    otblPNX = new MTSQLServer().wRead(mSql,null,false);
                    if (otblPNX != null)
                    {
                        foreach (DataRow vR in otblPNX.Rows)
                        {
                            txtSophieu.Text = vR["Sophieu"].ToString(); 
                            dtpNgaylap.EditValue = vR["Ngayct"].ToString()!=""?Convert.ToDateTime(vR["Ngayct"].ToString()).ToShortDateString():"";
                            dtpNgayPS.EditValue = vR["Ngayps"].ToString() != "" ? Convert.ToDateTime(vR["Ngayps"].ToString()).ToShortDateString() : "";
                            txtSoctgoc.Text = vR["Soctgoc"].ToString();
                            cbNguyente.EditValue = vR["Loaitien"].ToString();
                            txtTygia.Text = vR["Tygia"].ToString();
                            txtMakho.Text = vR["Makho"].ToString();
                            txtTenkho.Text = vR["Tenkho"].ToString();
                            txtMadv.Text = vR["Makh"].ToString();
                            txtTendonvi.Text = vR["Tenkh"].ToString();
                            txtMald.Text = vR["Mald"].ToString();
                            txtLydo.Text = vR["Lydo"].ToString();
                            txtNguoigiao.Text = vR["Giaonhan"].ToString();
                            txtGhichu.Text = vR["Ghichu"].ToString();
                        }
                    }
                     
                }

                otblPNXCT = new MTSQLServer().wRead(String.Format("SELECT * FROM NX_PHIEUNXCT where phieunxid='{0}' ORDER BY Phieunxctid asc", pPhieuNXID), null, false);
                grdPhieuNhap.ItemsSource = otblPNXCT;
                MTGlobal.SetFormatGridControl(grdPhieuNhap, tblView,false);
                
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
            /*
            if (txtMadv.Text == "")
            {
                Utils.showMessage("Bạn chưa nhập mã khách hàng..", "Thông báo");
                txtMadv.Focus();
                return false;
            }*/

            if (txtMakho.Text == "")
            {
                Utils.showMessage("Bạn chưa chọn kho hàng..", "Thông báo");
                txtMakho.Focus();
                return false;
            }
            if (txtNguoigiao.Text == "") {
                Utils.showMessage("Bạn chưa nhập người giao hàng..", "Thông báo");
                txtNguoigiao.Focus();
                return false;
            }
            if (grdPhieuNhap.VisibleRowCount <= 0)
            {
                Utils.showMessage("Bạn chưa nhập chi tiết hàng hóa..", "Thông báo");
                tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = grdPhieuNhap.Columns.First();
                tblView.ShowEditor();
                return false;
            }
            return true;
        }

        private void fdoAdd() {
            try{
                int mMM = int.Parse(Convert.ToDateTime(pDenNgay).Month.ToString().PadLeft(2, '0'));
                int mYY = int.Parse(Convert.ToDateTime(pDenNgay).Year.ToString());
                pPhieuNXID = MTGlobal.GetNewID();
                txtSophieu.Text = MTGlobal.GetSophieu(MTGlobal.MT_KYHIEU_CUAHANG,this.pLoaiPhieu, mMM, mYY, MTGlobal.MT_USER_LOGIN).ToString();
                dtpNgaylap.Text = DateTime.Now.ToShortDateString();
                dtpNgayPS.Text = DateTime.Now.ToShortDateString();                
              
                BindData();
                if (MTGlobal.SetGridReadOnly(grdPhieuNhap,tblView, false))
                {
                    tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                    tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                    tblView.FocusedColumn = grdPhieuNhap.Columns.First();
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
            if (MTGlobal.SetGridReadOnly(grdPhieuNhap,tblView, false))
            {
                tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = grdPhieuNhap.Columns.First();
                tblView.ShowEditor();
            }
        }
        private void fdoSave() {
            if (fValidate()){
                DataTable tmpPN = new modPhieuNX().dtPhieuNX();
                DataRow vR = tmpPN.NewRow();
                vR["Phieunxid"] = pPhieuNXID;
                vR["Sophieu"] = txtSophieu.Text.ToUpper();
                vR["Loaiphieu"] =this.pLoaiPhieu;
                vR["Makho"] = txtMakho.Text;
                vR["Mald"] = txtMald.Text;
                vR["Makh"] = txtMadv.Text;
                vR["Ngayct"] = Convert.ToDateTime(dtpNgaylap.Text);
                vR["Ngayps"] =Convert.ToDateTime(dtpNgayPS.Text);
                vR["Soctgoc"] = txtSoctgoc.Text;
                vR["Loaitien"] = cbNguyente.Text.ToString();
                vR["Tygia"] = txtTygia.Text.Length == 0 ? 0 : Double.Parse(txtTygia.Text);
                vR["Nguyente"] = 0;
                vR["Vat"] = 0;
                vR["TTVat"] = 0;
                vR["Thanhtien"] = 0;
                vR["Ghichu"] = txtGhichu.Text;
                vR["Giaonhan"] = txtNguoigiao.Text.Trim();
                vR["Ngaylap"] = DateTime.Now;
                vR["Nguoilap"] = MTGlobal.MT_USER_LOGIN;
                tmpPN.Rows.Add(vR);
                tmpPN.AcceptChanges();

                DataTable tmpPNCT = new modPhieuNX().dtPhieuNXCT();
                var filteredRows = tblView.Grid.DataController.GetAllFilteredAndSortedRows();
                if (filteredRows.Count == 0)
                {
                    return;
                }

                tblView.CommitEditing();
                tblView.CloseEditor();
                otblPNXCT.AcceptChanges();
                //for (int i = 0; i < filteredRows.Count; i++){
                foreach(DataRow sR in otblPNXCT.Rows){
                    //var row = filteredRows[i];
                    DataRow cR = tmpPNCT.NewRow();
                    cR["Phieunxctid"] = sR["Phieunxctid"];
                    cR["Phieunxid"] = pPhieuNXID;
                    cR["Maspid"] = sR["Maspid"];
                    cR["Masp"] = sR["Masp"];
                    cR["Tensp"] =sR["Tensp"];
                    cR["Quycach"] = sR["Quycach"];
                    cR["Dvt"] = sR["Dvt"];
                    cR["SLThung"] = sR["SLThung"];
                    cR["Soluong"] = sR["Soluong"];
                    cR["Dongia"] = sR["Dongia"];
                    cR["Nguyente"] = sR["Nguyente"];
                    cR["Vat"] = sR["Vat"];
                    cR["TTVat"] =sR["TTVat"];
                    cR["Thanhtien"] =sR["Thanhtien"];
                    cR["Songaychono"] = 0;
                    cR["Ghiso"] = 1;
                    cR["Ghichu"] = sR["Ghichu"];
                    tmpPNCT.Rows.Add(cR);
                }
                tmpPNCT.AcceptChanges();
                 
                String Rs=new modPhieuNX().SavePhieuNX(tmpPN,tmpPNCT);
                if (Rs == "")
                {
                    setReadOnly(true);
                    MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "SAVE");
                    MTGlobal.SetGridReadOnly(grdPhieuNhap,tblView, true);
                    isEdit = false;
                }
                else {
                    Utils.showMessage(Rs, "Thông báo");
                }               
            }
        }

        private void fThanhTien(int iRowHander) { 
            try{
                String sSL = "0", sDG = "0", sVAT = "0";

                sSL = grdPhieuNhap.GetCellValue(iRowHander, colSoLuong).ToString();
                sDG = grdPhieuNhap.GetCellValue(iRowHander, colDonGia).ToString();
                sVAT = grdPhieuNhap.GetCellValue(iRowHander, colvat).ToString();
                
                Double dGia, dSL,dVAT;
                Double.TryParse(sDG, out dGia);
                Double.TryParse(sSL, out dSL);
                Double.TryParse(sVAT, out dVAT);

                if (dSL > 0 && dGia > 0)
                {
                    grdPhieuNhap.SetCellValue(iRowHander, colnguyente, Math.Round(dSL * dGia, 0, MidpointRounding.AwayFromZero));
                    if (dVAT > 0)
                    {
                        grdPhieuNhap.SetCellValue(iRowHander, colttvat, Math.Round((dSL * dGia * dVAT) / 100, 0, MidpointRounding.AwayFromZero));
                    }
                    grdPhieuNhap.SetCellValue(iRowHander, colThanhtien, Math.Round((dSL * dGia) + ((dSL * dGia * dVAT) / 100), 0, MidpointRounding.AwayFromZero));
                }
                else {
                    grdPhieuNhap.SetCellValue(iRowHander, colnguyente, 0);
                    grdPhieuNhap.SetCellValue(iRowHander, colttvat, 0);
                    grdPhieuNhap.SetCellValue(iRowHander, colThanhtien, 0);
                }

            }catch{}           
        }

        private void fQuydoithung(int iRowHander){
            try{
                String sMasp = "";
                sMasp = grdPhieuNhap.GetCellValue(iRowHander, colMasp).ToString();
                if (otblSP != null && otblSP.Rows.Count > 0)
                {
                    DataRow[] vRow = otblSP.Select(string.Format("Masp='{0}'", sMasp));
                    if (vRow.Length > 0)
                    {
                        Double dSLThung = 0, dQDThung = 0, dSLQD = 0;
                        Double.TryParse(vRow[0]["Quydoithung"].ToString(), out dQDThung);
                        Double.TryParse(grdPhieuNhap.GetCellValue(iRowHander,colSoThung).ToString(), out dSLThung);
                        dSLQD = Math.Round(dSLThung * dQDThung, 0, MidpointRounding.AwayFromZero);
                        grdPhieuNhap.SetCellValue(iRowHander, colSoLuong, dSLQD);
                    }
                }
            }
            catch { }
        }


        private void doPrint_Click(object sender, RoutedEventArgs e)
        {
            if (pPhieuNXID == "") {
                Utils.showMessage("Bạn chưa chọn phiếu để in...", "Lưu ý");
            }
            new MTReport().rptNX_Phieunhap(pPhieuNXID);
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
            if (pPhieuNXID == "")
            {
                Utils.showMessage("Bạn chưa chọn phiếu cần xóa..", "Lưu ý");
                return;
            }
            if (Utils.ConfirmMessage("Bạn có chắc muốn xóa phiếu này?", "Xác nhận"))
            {
                if (new modPhieuNX().DelPhieuNX(pPhieuNXID))
                {
                    pPhieuNXID = "";
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

        private void tblView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
        }

        private void tblView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e){ 
            try{
                grdPhieuNhap.SetCellValue(e.RowHandle, colPhieuNXCTID, MTGlobal.GetNewID());
                grdPhieuNhap.SetCellValue(e.RowHandle, colPhieuNXID,pPhieuNXID); 
            }
            catch { }
        }


        private void grdPhieuNhap_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (isEdit == false) { return; }
            if (e.Key == Key.Enter)
            {
                if (tblView.FocusedColumn.FieldName == "Masp" && tblView.FocusedColumn.Name == "colMasp")
                {
                    if (grdPhieuNhap.GetFocusedRowCellValue(colMasp) == null || grdPhieuNhap.GetFocusedRowCellValue(colMasp).ToString() == "")
                    {
                        tblView.FocusedColumn = colMasp;
                        return;
                    }

                    String sMasp = grdPhieuNhap.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString();
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
                            double.TryParse(grdPhieuNhap.GetCellValue(tblView.FocusedRowHandle, colSoLuong).ToString(), out dSL);
                            if (dSL == 0) { dSL = 1; }
                        }
                        catch { dSL = 1; }

                        if (otblSP != null && otblSP.Rows.Count > 0)
                        {
                            DataRow[] vRow = otblSP.Select(string.Format("Masp='{0}'", sMasp));
                            if (vRow.Length > 0)
                            {
                                grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colSanpham, vRow[0]["Tensp"].ToString());
                                grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colDVT, vRow[0]["Dvt"].ToString());
                                grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colQC, vRow[0]["Quycach"].ToString()); 
                                grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colSoLuong, dSL);

                            }
                            else
                            {
                                dlg_ChonSanPham oChonSP = new dlg_ChonSanPham(otblSP);
                                oChonSP.ShowDialog();
                                DataRowView vRw = oChonSP.pRowChon;
                                if (vRw != null && vRw.Row.ItemArray.Length > 0)
                                {
                                    grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colSanpham, vRw.Row.ItemArray.GetValue(3).ToString());
                                    grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colDVT, vRw.Row.ItemArray.GetValue(4).ToString());
                                    grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colQC, vRw.Row.ItemArray.GetValue(5).ToString());
                                    
                                    grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colSoLuong, dSL);
                                    sMasp = vRw.Row.ItemArray.GetValue(2).ToString().ToUpper();
                                }
                                else
                                {
                                    tblView.FocusedColumn = colMasp;
                                }
                            }
                        }
                        tblView.CloseEditor();
                        tblView.ShowEditor();           
                        grdPhieuNhap.SetCellValue(tblView.FocusedRowHandle, colMasp, sMasp);
                        //tblView.PostEditor();
                    }
                }
            }
            if (e.Key == Key.Delete)
            {
                try
                {
                    if (grdPhieuNhap.GetFocusedRowCellValue(colPhieuNXCTID) == null || grdPhieuNhap.GetFocusedRowCellValue(colPhieuNXCTID).ToString() == "")
                    {
                        e.Handled = true;
                        return;
                    }
                    if (Utils.ConfirmMessage(String.Format("Bạn có chắc muốn xóa {0} không ?", grdPhieuNhap.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString()), "Xác nhận xóa") == true)
                    {
                        tblView.DeleteRow(tblView.FocusedRowHandle);
                    }
                }
                catch (Exception ex) { ex.Data.Clear(); }
            }
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
                if (grdPhieuNhap.GetCellValue(e.RowHandle, colDonGia).ToString() != "") {
                    fThanhTien(e.RowHandle);
                }
            }else if (tblView.FocusedColumn.FieldName == "Soluong" && e.Column.Name == "colSoLuong")
            {
                //string value = ((DevExpress.Xpf.Grid.CellValueEventArgs)(e)).Value.ToString();
                if (grdPhieuNhap.GetCellValue(e.RowHandle, colDonGia).ToString() != "" && grdPhieuNhap.GetCellValue(e.RowHandle, colSoLuong).ToString() != "")
                {
                    fThanhTien(e.RowHandle);
                }
            }else if (tblView.FocusedColumn.FieldName == "Dongia" && e.Column.Name == "colDonGia")
            {
                if (grdPhieuNhap.GetCellValue(e.RowHandle, colDonGia).ToString() != "" && grdPhieuNhap.GetCellValue(e.RowHandle, colSoLuong).ToString() != "")
                {
                    fThanhTien(e.RowHandle);
                }
            }
          
            else if (tblView.FocusedColumn.FieldName == "Vat" && e.Column.Name =="colvat")
            {                 
                if (grdPhieuNhap.GetCellValue(e.RowHandle, colDonGia).ToString() != "" && grdPhieuNhap.GetCellValue(e.RowHandle, colSoLuong).ToString() != "")
                {
                    fThanhTien(e.RowHandle);
                }
            }       
           
        }
         

        private void tblView_InvalidRowException(object sender, DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText!="")
            {
                Utils.showMessage(e.ErrorText.ToString(), "Lưu ý", "ERR");
            }
            e.ExceptionMode = DevExpress.Xpf.Grid.ExceptionMode.NoAction;            
        }

        private void tblView_ValidateRow(object sender, DevExpress.Xpf.Grid.GridRowValidationEventArgs e)
        {
            if (e.Row == null) return;
            if (grdPhieuNhap.GetCellValue(e.RowHandle, colMasp) == null || grdPhieuNhap.GetCellValue(e.RowHandle, colMasp).ToString()=="")
            {
                e.SetError("Bạn chưa nhập mã hàng hóa..");
                e.IsValid = false;
                return;
            }

            double dDG = 0,dSL=0;            
            try
            {
                Double.TryParse(grdPhieuNhap.GetCellValue(e.RowHandle, colDonGia).ToString(), out dDG);
                Double.TryParse(grdPhieuNhap.GetCellValue(e.RowHandle, colSoLuong).ToString(), out dSL);
            }
            catch { }
            if (dDG <= 0) {
                e.SetError("Bạn chưa nhập đơn giá hàng hóa..");
                e.IsValid = false;
                return;
            }
            if (dSL <= 0) {
                e.SetError("Bạn chưa nhập đơn giá hàng hóa..");
                e.IsValid = false;
                return;
            }

            double dVAT = 0, dTTVAT = 0, dNguyente = 0, dNguyenteRow = 0, dTTVatRow = 0, dThanhtienRow = 0;
            try
            {
                Double.TryParse(grdPhieuNhap.GetCellValue(e.RowHandle, colvat).ToString(), out dVAT);
                dNguyente = Math.Round(dSL * dDG, 0, MidpointRounding.AwayFromZero);
                if (dVAT > 0)
                {
                    dTTVAT = Math.Round(dNguyente * dVAT / 100, 0, MidpointRounding.AwayFromZero);
                }

                Double.TryParse(grdPhieuNhap.GetCellValue(e.RowHandle, colnguyente).ToString(), out dNguyenteRow);
                Double.TryParse(grdPhieuNhap.GetCellValue(e.RowHandle, colttvat).ToString(), out dTTVatRow);
                Double.TryParse(grdPhieuNhap.GetCellValue(e.RowHandle, colThanhtien).ToString(), out dThanhtienRow);

                if (dNguyente != Math.Round(dNguyenteRow, 0, MidpointRounding.AwayFromZero)) {
                    e.SetError("Số tiền nguyên tệ không đúng.");
                    e.IsValid = false;
                    return;
                }

                if (dTTVAT != Math.Round(dTTVatRow, 0, MidpointRounding.AwayFromZero))
                {
                    e.SetError("Số tiền (VAT) không đúng.");
                    e.IsValid = false;
                    return;
                }
                if ((dNguyente+dTTVAT) != Math.Round(dThanhtienRow, 0, MidpointRounding.AwayFromZero))
                {
                    e.SetError("Tổng thành tiền không đúng.");
                    e.IsValid = false;
                    return;
                }
                
            }
            catch { } 
             
            //string productCode = ((System.Data.DataRowView)(e.Value)).Row.ItemArray[3].ToString();
           
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
           
            if (e.Key == Key.Enter)
            {
                /* 
                var ue = e.OriginalSource as FrameworkElement;
                
                 * var control =((DependencyObject)e.OriginalSource);                    
                    switch (""){                       
                        case "dtpNgaylap":
                            if (dtpNgaylap.Text == "") {
                                dtpNgaylap.ValidationError.ErrorContent.Equals("Bạn chưa nhập ngày chứng từ");
                                dtpNgaylap.ShowError = true;
                                dtpNgaylap.Focus();
                                return;
                            }
                            break;
                    }
                     e.Handled = true;
                      ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); 
               */

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
    
        private void txtSoctgoc_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void cbNguyente_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtTygia_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtMakho_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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

        void txtMald_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            } 
        }
        private void txtNguoinhan_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
                grdPhieuNhap.Focus();
                tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = colMasp;
                tblView.ShowEditor();
            }          
        } 
    #endregion

        #region "LOST FOCUS"

        bool isModified = false;
        private void txtMakho_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (e.UpdateSource == DevExpress.Xpf.Editors.Validation.Native.UpdateEditorSource.TextInput)
            {
                isModified = true;
            }
        }

        private void txtMakho_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isEdit && isModified && txtMakho.Text != "" && txtMakho.Text != null)
            {
                String mTenKho = new MTSQLServer().getNameByValue("SELECT Tenkho FROM DM_KHO", "Makho", txtMakho.Text.Trim(), "Tenkho");
                if (mTenKho == "" || mTenKho == null)
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("KHO");
                    oDM.ShowDialog();
                    txtMakho.Text = oDM.pMaso;
                    txtTenkho.Text = oDM.pGiaTriChon;
                }
                else
                {
                    txtTenkho.Text = mTenKho;
                }
            }
            if (txtMakho.Text == "") { txtTenkho.Text = ""; }
        }

        private void txtMadv_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isEdit && txtMadv.Text != "" && txtMadv.Text != null)
            {
                String mTENKH = new MTSQLServer().getNameByValue("SELECT Tenkh FROM DM_KHACHHANG", "Makh", txtMadv.Text.Trim(), "Tenkh");
                if (mTENKH == "" || mTENKH == null)
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("DV");
                    oDM.ShowDialog();
                    txtMadv.Text = oDM.pMaso;
                    txtTendonvi.Text = oDM.pGiaTriChon;
                }
                else
                {
                    txtTendonvi.Text = mTENKH;
                }
            }
            if (txtMadv.Text == "") { txtTendonvi.Text = ""; }
        }

        private void txtMald_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isEdit && txtMald.Text != "" && txtMald.Text != null)
            {
                String mLydo = new MTSQLServer().getNameByValue("SELECT Lydo FROM DM_LYDO", "Mald", txtMald.Text.Trim(), "Lydo");
                if (mLydo == "" || mLydo == null)
                {
                    dlg_ChonDM oDM = new dlg_ChonDM("LYDO");
                    oDM.ShowDialog();
                    txtMald.Text = oDM.pMaso;
                    txtLydo.Text = oDM.pGiaTriChon;
                }
                else
                {
                    txtLydo.Text = mLydo;
                }
            }
            if (txtMald.Text == "") { txtLydo.Text = ""; }
        }
      #endregion

      

     
     
         
         
    }
}
