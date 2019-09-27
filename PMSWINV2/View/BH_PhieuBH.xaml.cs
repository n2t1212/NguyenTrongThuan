﻿using DevExpress.Xpf.Core;
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

using System.Text.RegularExpressions;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.XtraEditors;

namespace MTPMSWIN.View
{
 
    public partial class BH_PhieuBH : Window
    {
        MTGlobal.MT_ROLE pACT_ROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        private string pPhieuID = "";
        private string pDenNgay = "";
        private string pTuNgay = ""; 
        private String pLoaiPhieu = "";
        private DataTable otblBH = null;
        private DataTable otblBHCT = null;
      
        private bool isEdit = false;
        DevExpress.Utils.WaitDialogForm Dlg;

        private DataTable otblSP = null;

        Timer oTimerClock;
        public BH_PhieuBH(String mPhieuID,String mLoaiPhieu, String mTuNgay, String mDenNgay, MTGlobal.MT_ROLE mActRole, bool isAddNew = false)
        {
            InitializeComponent();         
            this.Title = "LÂP PHIẾU BÁN HÀNG";
            this.pPhieuID = mPhieuID;
            this.pTuNgay = mTuNgay == "" ? DateTime.Now.ToShortDateString() : mTuNgay;
            this.pDenNgay = mDenNgay == "" ? DateTime.Now.ToShortDateString() : mDenNgay;
            this.pLoaiPhieu = mLoaiPhieu;
            this.pACT_ROLE = mActRole;

            this.txtQuay.Text = MTGlobal.MT_QUAY;
            this.txtCa.Text = MTGlobal.MT_CA;
            this.txtThuNgan.Text = MTGlobal.MT_USER_LOGIN_FULLNAME;

            Dlg= Utils.shwWait();
            setUserRight(); 
            if (isAddNew){
                fdoAdd();
            }
            else {
                BindData();
                MTGlobal.SetGridReadOnly(grdPhieuBH,tblView, true);
                setReadOnly(true);
            }
            
            System.Threading.Thread oThreSP = new System.Threading.Thread(LoadSanPham);
            oThreSP.Start();

            oTimerClock = new Timer();
            oTimerClock.Tick += oTimerClock_Tick;
            oTimerClock.Interval = 1000;
            oTimerClock.Enabled = true;

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
            chkKHmoi.IsEnabled = !isReadonly;
            chkVIP.IsEnabled = !isReadonly; 
            txtMadv.IsReadOnly = isReadonly;
            txtTendonvi.IsReadOnly = isReadonly;
            txtDienthoai.IsReadOnly = isReadonly;
            txtDiachi.IsReadOnly = isReadonly;
            txtGhichu.IsReadOnly=isReadonly;

            txtTongCK.IsReadOnly = isReadonly;
            txtTongTTCK.IsReadOnly = isReadonly;
            txtBarCode.IsReadOnly = isReadonly;
            txtBarCodeNo.IsReadOnly = isReadonly;
            txtTientra.IsReadOnly = isReadonly;
            doPrint.IsEnabled = !isReadonly;
            doSaveAdd.IsEnabled = !isReadonly;
            doPrint.IsEnabled = !isReadonly;
            doSaveAdd.IsEnabled = !isReadonly;

        }

        private void LoadSanPham() {
            try
            {
                String mSql = "SELECT Maspid,Manhomspid,Masp,Tensp,Dvt,Quycach,Quydoikgl,Quydoithung,Nhacungcap,Maqrcode,Mabarcode FROM DM_SANPHAM with(nolock) order by Masp asc";
                otblSP = new MTSQLServer().wRead(mSql, null, false);
            }
            catch (Exception ex) { } 
        }

        private void BindData() {
            try{
                txtMadv.Text = "";
                txtTendonvi.Text = "";
                txtDiachi.Text = "";
                txtDienthoai.Text = "";
                txtGhichu.Text = "";

                txtBarCode.Text = "";
                txtBarCodeNo.Text = "";

                txtThanhtien.Text ="";
                txtTongCK.Text = "";
                txtTongTTCK.Text = "";
                txtThanhtoan.Text = "";
                txtTientra.Text = "";
                txtTienthua.Text = "";

                chkKHmoi.IsChecked = false;
                chkVIP.IsChecked = false;

                if (pPhieuID != ""){
                    String mSql = String.Format("SELECT A.* FROM BH_PHIEUBH A WHERE A.Phieubhid='{0}'", pPhieuID);
                    otblBH = new MTSQLServer().wRead(mSql,null,false);
                    if (otblBH != null)
                    {
                        foreach (DataRow vR in otblBH.Rows)
                        {
                            txtSophieu.Text = vR["Sophieu"].ToString();
                            dtpNgaylap.EditValue = vR["Ngayct"].ToString() != "" ? Convert.ToDateTime(vR["Ngayct"].ToString()).ToShortDateString() : "";
                            txtMadv.Text = vR["Makh"].ToString();
                            txtTendonvi.Text = vR["tenkh"].ToString();
                            txtDienthoai.Text = vR["dienthoai"].ToString();
                            txtDiachi.Text = vR["diachi"].ToString() ;
                            txtGhichu.Text = vR["ghichu"].ToString();

                            txtQuay.Text = vR["Quay"].ToString();
                            txtCa.Text = vR["Ca"].ToString();
                            txtThuNgan.Text = vR["Thungan"].ToString();

                            txtThanhtien.Text = vR["Nguyente"].ToString();
                            txtTongCK.Text = vR["Ck"].ToString();
                            txtTongTTCK.Text = vR["TTCk"].ToString();
                            txtThanhtien.Text = vR["Thanhtien"].ToString();
                            txtTientra.Text = vR["Tientra"].ToString();
                            txtTienthua.Text = vR["Tienthoi"].ToString();
                            if (vR["LoaiKH"] != null && vR["LoaiKH"].ToString() == "VIP")
                            {
                                chkVIP.IsChecked = true;
                            }
                            else {
                                chkVIP.IsChecked = false;
                            }
                        }
                    }
                }

                otblBHCT = new MTSQLServer().wRead(String.Format("SELECT * FROM BH_PHIEUBHCT where Phieubhid='{0}' ORDER BY Quatang, Phieubhctid asc", pPhieuID), null, false);
                grdPhieuBH.ItemsSource = otblBHCT;
                MTGlobal.SetFormatGridControl(grdPhieuBH, tblView,false); 
            }
            catch { }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e){
            try{
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }catch { }
        }

        private bool fValidate() {
            if (txtSophieu.Text == "") {
                Utils.showMessage("Số phiếu chưa được khởi tạo..", "Thông báo");
                txtSophieu.Focus();
                return false;
            }
            if (dtpNgaylap.Text == "") {
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

            Double dThanhTienF = 0, dTongTTCKF = 0,dTientra=0;
            Double.TryParse(txtThanhtien.Text, out dThanhTienF);
            Double.TryParse(txtTongTTCK.Text, out dTongTTCKF);
            Double.TryParse(txtTientra.Text, out dTientra);

            if (dThanhTienF <= 0) {
                Utils.showMessage("Số tiền đơn hàng phải >0", "Thông báo");
                txtThanhtien.Focus();
                return false;
            }
            if (dThanhTienF <= 0)
            {
                Utils.showMessage("Bạn chưa nhập số tiền khách trả..", "Thông báo");
                txtTientra.Focus();
                return false;
            }

            tblView.CloseEditor();
            otblBHCT.AcceptChanges();
            if (otblBHCT == null || otblBHCT.Rows.Count == 0) {
                Utils.showMessage("Bạn chưa nhập chi tiết bán hàng.", "Thông báo");
                if (MTGlobal.SetGridReadOnly(grdPhieuBH, tblView, false)){
                    tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                    tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                    tblView.FocusedColumn = grdPhieuBH.Columns.First();
                    tblView.ShowEditor();
                }
                return false;
            } 
            return true;
        }

        private void fThanhTienRow(int iRowHander){
            try{
                String sSL = "0", sDG = "0", sCK = "0", sTTCK = "0";
                sSL = grdPhieuBH.GetCellValue(iRowHander, colSoluong).ToString();
                sDG = grdPhieuBH.GetCellValue(iRowHander, colDonGia).ToString();
                sCK = grdPhieuBH.GetCellValue(iRowHander, colCk).ToString();

                Double dGia, dSL, dCk, dTTCk;
                Double.TryParse(sDG, out dGia);
                Double.TryParse(sSL, out dSL);
                Double.TryParse(sCK, out dCk);

                if (dSL > 0 && dGia > 0)
                {
                    grdPhieuBH.SetCellValue(iRowHander, colNguyente, Math.Round(dSL * dGia, 0, MidpointRounding.AwayFromZero));
                    if (dCk > 0)
                    {
                        dTTCk = Math.Round(dCk * dSL * dGia / 100, 0, MidpointRounding.AwayFromZero);
                        grdPhieuBH.SetCellValue(iRowHander, colTTCk, dTTCk);
                    }
                    sTTCK = grdPhieuBH.GetCellValue(iRowHander, colTTCk).ToString();
                    Double.TryParse(sTTCK, out dTTCk);

                    grdPhieuBH.SetCellValue(iRowHander, colThanhtien, Math.Round(dSL * dGia - dTTCk, 0, MidpointRounding.AwayFromZero));
                }
                else{
                    grdPhieuBH.SetCellValue(iRowHander, colNguyente, 0);
                    grdPhieuBH.SetCellValue(iRowHander, colTTCk, 0);
                    grdPhieuBH.SetCellValue(iRowHander, colThanhtien, 0);
                }
            }
            catch { }
        }

        private void fTinhTong(){
            try{
                try { tblView.PostEditor(); }catch { }
 
                Double dThanhtien = 0,dThanhtienTmp=0, dTongTTCK = 0,dTongTTCKTmp=0;              
                for (int i = 0; i < grdPhieuBH.VisibleRowCount; i++) {
                    int iRowHandle = grdPhieuBH.GetRowHandleByVisibleIndex(i);
                    try
                    {
                        DataRowView vR = (System.Data.DataRowView)grdPhieuBH.GetRow(iRowHandle);
                        if (vR["Quatang"].ToString() != "1" && vR["Quatang"].ToString() != "True")
                        {
                            Double.TryParse(vR.Row["Nguyente"].ToString(), out dThanhtienTmp);
                            Double.TryParse(vR.Row["TTCk"].ToString(), out dTongTTCKTmp);
                            dThanhtien += dThanhtienTmp;
                            dTongTTCK += dTongTTCKTmp;
                        }
                    }
                    catch { }                
                }
                if (dTongTTCK > 0)
                {
                    txtTongCK.Text = "0";
                }
                else {
                    Double dTongCK = 0;
                    Double.TryParse(txtTongCK.Text.Trim(), out dTongCK);
                    dTongTTCK = Math.Round(dThanhtien * dTongCK / 100, 0, MidpointRounding.AwayFromZero);
                }

                txtThanhtien.Text = Math.Round(dThanhtien, 0, MidpointRounding.AwayFromZero).ToString();
                txtTongTTCK.Text = Math.Round(dTongTTCK, 0, MidpointRounding.AwayFromZero).ToString();
                txtThanhtoan.Text = Math.Round(dThanhtien - dTongTTCK, 0, MidpointRounding.AwayFromZero).ToString();                 

            }
            catch { }
        }


        private void fTinhThanhToan(){
            try{
                Double dThanhtien = 0, dCK = 0, dTTCK = 0, dThanhtoan = 0, dTientra = 0, dTienthoi = 0;
                Double.TryParse(txtThanhtien.Text.ToString(), out dThanhtien);
                Double.TryParse(txtTongCK.Text.ToString(), out dCK);
                if (dCK > 0)
                {
                    dTTCK = dThanhtien * dCK / 100;
                }
                else
                {
                    Double.TryParse(txtTongTTCK.Text.ToString(), out dTTCK);
                }
                Double.TryParse(txtTientra.Text.ToString(), out dTientra);

                dThanhtoan = Math.Round(dThanhtien - dTTCK, 0, MidpointRounding.AwayFromZero);
                dTienthoi = Math.Round(dTientra-dThanhtoan, 0, MidpointRounding.AwayFromZero);

                txtTongTTCK.Text = dTTCK.ToString();
                txtThanhtoan.Text = dThanhtoan.ToString();
                txtTienthua.Text = dTienthoi.ToString();
            }
            catch { }
        }


        private void fQuydoithung(int iRowHander){
            try{
                String sMasp = "";
                sMasp = grdPhieuBH.GetCellValue(iRowHander, colMasp).ToString();
                if (otblSP != null && otblSP.Rows.Count > 0)
                {
                    DataRow[] vRow = otblSP.Select(string.Format("Masp='{0}'", sMasp));
                    if (vRow.Length > 0) {
                        Double dSLThung=0, dQDThung = 0, dSLQD = 0;
                        Double.TryParse(vRow[0]["Quydoithung"].ToString(), out dQDThung);
                        Double.TryParse(grdPhieuBH.GetCellValue(iRowHander, colSoThung).ToString(), out dSLThung);
                        dSLQD = Math.Round(dSLThung * dQDThung, 0, MidpointRounding.AwayFromZero);
                        grdPhieuBH.SetCellValue(iRowHander, colSoluong, dSLQD);                        
                    }
                } 
            }
            catch { }
        }
        
        private void fdoAdd() {
            try{
                int mMM = int.Parse(Convert.ToDateTime(pDenNgay).Month.ToString().PadLeft(2, '0'));
                int mYY = int.Parse(Convert.ToDateTime(pDenNgay).Year.ToString());
                pPhieuID = MTGlobal.GetNewID();
                txtSophieu.Text = MTGlobal.GetSophieu(MTGlobal.MT_KYHIEU_CUAHANG, this.pLoaiPhieu, mMM, mYY, MTGlobal.MT_USER_LOGIN).ToString();
                dtpNgaylap.Text = DateTime.Now.ToShortDateString();
               
                BindData();
                if (MTGlobal.SetGridReadOnly(grdPhieuBH,tblView, false))
                {
                    tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                    tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                    tblView.FocusedColumn = grdPhieuBH.Columns.First();
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
            if (MTGlobal.SetGridReadOnly(grdPhieuBH,tblView, false))
            {
                 tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = grdPhieuBH.Columns.First();
                tblView.ShowEditor();
            }
        }

        private void fdoSave() {
            if (fValidate()){
                DataTable tmpBH = new modPhieuBH().dtPhieuBH();
                DataRow vR = tmpBH.NewRow();
                vR["Phieubhid"] = pPhieuID;
                vR["Sophieu"] = txtSophieu.Text.ToUpper();
                vR["Ngayct"] = Convert.ToDateTime(dtpNgaylap.Text);
                vR["Ngayps"] = Convert.ToDateTime(dtpNgaylap.Text);
                vR["Loaiphieu"] = this.pLoaiPhieu;

                vR["Makh"] =txtMadv.Text;
                vR["Tenkh"] = txtTendonvi.Text;
                vR["Dienthoai"] = txtDienthoai.Text;
                vR["Diachi"] = txtDiachi.Text;
                vR["LoaiKH"] = (chkVIP.IsChecked == true ? "VIP" : "NORMAL");
                vR["Khmoi"] = (chkKHmoi.IsChecked == true ? 1 : 0);
                vR["Quay"] = txtQuay.Text;
                vR["Ca"] = txtCa.Text;
                vR["Thungan"] = txtThuNgan.Text;

                vR["Nguyente"] =txtThanhtien.Text;
                vR["Ck"] = txtTongCK.Text==""?"0":txtTongCK.Text;
                vR["TTCk"] = txtTongTTCK.Text == "" ? "0" : txtTongTTCK.Text; 
                vR["Thanhtien"] = txtThanhtoan.Text;

                vR["Tientra"] = txtTientra.Text == "" ? "0" : txtTientra.Text;
                vR["Tienthoi"] =txtTienthua.Text==""?"0":txtTienthua.Text;

                vR["Ghichu"] = txtGhichu.Text;
                vR["Ngaylap"] = DateTime.Now;
                vR["Nguoilap"] = MTGlobal.MT_USER_LOGIN;
                tmpBH.Rows.Add(vR);
                tmpBH.AcceptChanges();
                             
                DataTable tmpBHCT = new modPhieuBH().dtPhieuBHCT();            
                tblView.CommitEditing();
                tblView.CloseEditor();
                otblBHCT.AcceptChanges();
              
                foreach(DataRow sR in otblBHCT.Rows){
                    DataRow cR = tmpBHCT.NewRow();
                    cR["Phieubhctid"] = sR["Phieubhctid"];
                    cR["Phieubhid"] = pPhieuID;
                    cR["Mavach"] = sR["Mavach"];
                    cR["Maspid"] = sR["Maspid"];
                    cR["Masp"] = sR["Masp"];
                    cR["Tensp"] = sR["Tensp"];
                    cR["Dvt"] = sR["Dvt"];
                    cR["Quycach"] = sR["Quycach"];
                    cR["SLThung"] = sR["SLThung"];
                    cR["Soluong"] = sR["Soluong"];
                    cR["Dongia"] = sR["Dongia"];
                    cR["Nguyente"] = sR["Nguyente"];
                    cR["Ck"] = sR["Ck"];
                    cR["TTCk"] = sR["TTCk"];
                    cR["Thanhtien"] = sR["Thanhtien"];
                    cR["Quatang"] = sR["Quatang"]; 
                    cR["Ghiso"] = 1;
                    cR["Ghichu"] = sR["Ghichu"];
                    tmpBHCT.Rows.Add(cR);
                }
                tmpBHCT.AcceptChanges();
                 
                tblView.CommitEditing();
                tblView.CloseEditor();
                otblBHCT.AcceptChanges();
                 
                String Rs = new modPhieuBH().SavePhieuBH(tmpBH, tmpBHCT, MTGlobal.MT_USER_LOGIN);
                if (Rs == "")
                {
                    setReadOnly(true);
                    MTGlobal.SetButtonAction(pACT_ROLE, MTButton, "SAVE");
                    MTGlobal.SetGridReadOnly(grdPhieuBH,tblView, true);
                    isEdit = false;
                }
                else {
                    Utils.showMessage(Rs, "Thông báo");
                }               
            }
        }

       
       
        private void doPrint_Click(object sender, RoutedEventArgs e)
        {
            if (pPhieuID == "") {
                Utils.showMessage("Bạn chưa chọn phiếu để in..", "Lưu ý");
                return;
            }

            Double dTientra = 0;
            Double.TryParse(txtTientra.Text, out dTientra);
            if (dTientra <= 0) {
                Utils.showMessage("Bạn chưa nhập tiền khách trả...", "Lưu ý");
                return;
            }

            if (isEdit) {
                fdoSave();
            }
            new MTReport().rptBH_InBill(pPhieuID);
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
            if (pPhieuID == "")
            {
                Utils.showMessage("Bạn chưa chọn phiếu cần xóa..", "Lưu ý");
                return;
            }
            if (Utils.ConfirmMessage("Bạn có chắc muốn xóa phiếu này?", "Xác nhận"))
            {
                if (new modPhieuBH().DelPhieuBH(pPhieuID))
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
                grdPhieuBH.SetCellValue(e.RowHandle, colPhieubhctid, MTGlobal.GetNewID());
                grdPhieuBH.SetCellValue(e.RowHandle, colPhieubhid,pPhieuID); 
            }
            catch { }
        }

        private void grdPhieuBH_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e){
            try{
                if (isEdit == false) { return; }
                if (e.Key == Key.Enter){
                    if (tblView.FocusedColumn.FieldName == "Masp" && tblView.FocusedColumn.Name == "colMasp"){
                        if (grdPhieuBH.GetFocusedRowCellValue(colMasp) == null || grdPhieuBH.GetFocusedRowCellValue(colMasp).ToString() == "")
                        {
                            tblView.FocusedColumn = colMasp;
                            return;
                        }

                        String sMasp = grdPhieuBH.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString();
                        if (sMasp == null || sMasp == "")
                        {
                            tblView.FocusedColumn = colMasp;
                            return;
                        }

                        if (sMasp != ""){
                            double dSL = 0;
                            try{
                              double.TryParse(grdPhieuBH.GetCellValue(tblView.FocusedRowHandle, colSoluong).ToString(), out dSL);
                              if (dSL == 0) { dSL = 1; }
                            }catch { dSL = 1; }

                            if (otblSP != null && otblSP.Rows.Count > 0){
                                DataRow[] vRow = otblSP.Select(string.Format("Masp='{0}'", sMasp));
                                if (vRow.Length > 0){
                                    grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colTensp, vRow[0]["Tensp"].ToString());
                                    grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colDVT, vRow[0]["Dvt"].ToString());
                                    grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colQC, vRow[0]["Quycach"].ToString());

                                    Double dDGX = MTGlobal.GetDonGia(sMasp, pDenNgay);
                                    grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colDonGia, dDGX);
                                    grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colSoluong, dSL);
                                    if (dDGX > 0){
                                        Double dNguyente = Math.Round(dDGX * dSL, 0, MidpointRounding.AwayFromZero);
                                        grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colNguyente, dNguyente);
                                        grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colThanhtien, dNguyente);
                                    }

                                }else{
                                    dlg_ChonSanPham oChonSP = new dlg_ChonSanPham(otblSP);
                                    oChonSP.ShowDialog();
                                    DataRowView vRw = oChonSP.pRowChon;
                                    if (vRw != null && vRw.Row.ItemArray.Length > 0)
                                    {
                                        grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colTensp, vRw.Row.ItemArray.GetValue(3).ToString());
                                        grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colDVT, vRw.Row.ItemArray.GetValue(4).ToString());
                                        grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colQC, vRw.Row.ItemArray.GetValue(5).ToString());

                                        sMasp = vRw.Row.ItemArray.GetValue(2).ToString().ToUpper();
                                        Double dDGX = MTGlobal.GetDonGia(sMasp, pDenNgay);
                                        grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colDonGia, dDGX);
                                        grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colSoluong, dSL);

                                        if (dDGX > 0)
                                        {
                                            Double dNguyente = Math.Round(dDGX * dSL, 0, MidpointRounding.AwayFromZero);
                                            grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colNguyente, dNguyente);
                                            grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colThanhtien, dNguyente);
                                        }
                                    }
                                    else
                                    {
                                        tblView.FocusedColumn = colMasp;
                                    }
                                }
                            }
                            tblView.CloseEditor();
                            tblView.ShowEditor();
                            grdPhieuBH.SetCellValue(tblView.FocusedRowHandle, colMasp, sMasp);

                        }
                    }
                }
                if (e.Key == Key.Delete)
                {
                    try
                    {
                        if (grdPhieuBH.GetFocusedRowCellValue(colPhieubhctid) == null || grdPhieuBH.GetFocusedRowCellValue(colPhieubhctid).ToString() == "")
                        {
                            e.Handled = true;
                            return;
                        }
                        if (Utils.ConfirmMessage(String.Format("Bạn có chắc muốn xóa {0} không ?", grdPhieuBH.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString()), "Xác nhận xóa") == true)
                        {
                            tblView.DeleteRow(tblView.FocusedRowHandle);
                        }
                    }
                    catch (Exception ex) { ex.Data.Clear(); }
                }
            }
            catch(Exception ex) { Utils.showMessage("Phát sinh lỗi xử lý[KEYDOWN]=>" + ex.Message.ToString(),"Thông báo lỗi");  }
        }
         

        private void tblView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
           // ((TableView)sender).CommitEditing();
            e.Source.PostEditor();     
        }

        private void tblView_CellValueChanged(object sender, CellValueChangedEventArgs e){
            try
            {
                if (isEdit == false) { return; }

                if (tblView.FocusedColumn.FieldName == "Dongia" && e.Column.Name == "colDonGia")
                {
                    if (grdPhieuBH.GetCellValue(e.RowHandle, colDonGia).ToString() != "" && grdPhieuBH.GetCellValue(e.RowHandle, colSoluong).ToString() != "")
                    {
                        fThanhTienRow(e.RowHandle);
                    }
                }
                else if (tblView.FocusedColumn.FieldName == "SLThung" && e.Column.Name == "colSoThung")
                {
                    fQuydoithung(e.RowHandle);
                    fThanhTienRow(e.RowHandle);
                }
                else if (tblView.FocusedColumn.FieldName == "Soluong" && e.Column.Name == "colSoluong")
                {
                    if (grdPhieuBH.GetCellValue(e.RowHandle, colDonGia).ToString() != "" && grdPhieuBH.GetCellValue(e.RowHandle, colSoluong).ToString() != "")
                    {
                        fThanhTienRow(e.RowHandle);
                    }
                }
                else if (tblView.FocusedColumn.FieldName == "Ck" && e.Column.Name == "colCk")
                {
                    if (grdPhieuBH.GetCellValue(e.RowHandle, colDonGia).ToString() != "" && grdPhieuBH.GetCellValue(e.RowHandle, colSoluong).ToString() != "")
                    {
                        fThanhTienRow(e.RowHandle);
                    }
                }
                else if (tblView.FocusedColumn.FieldName == "TTCk" && e.Column.Name == "colTTCk")
                {
                    Double dcolCK = 0;
                    Double.TryParse(grdPhieuBH.GetCellValue(e.RowHandle, colCk).ToString(), out dcolCK);
                    grdPhieuBH.SetCellValue(e.RowHandle, colCk, 0);
                    if (dcolCK <= 0)
                    {
                        fThanhTienRow(e.RowHandle);
                    }
                }
            }
            catch { }
        }
         
        private void tblView_InvalidRowException(object sender, DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText != "")
            {
                Utils.showMessage(e.ErrorText.ToString(), "Lưu ý", "ERR");
            }
            e.ExceptionMode = DevExpress.Xpf.Grid.ExceptionMode.NoAction;      
        }

        private void tblView_ValidateRow(object sender, DevExpress.Xpf.Grid.GridRowValidationEventArgs e)
        {
            try{
                if (e.Row == null) return;
                if (grdPhieuBH.GetCellValue(e.RowHandle, colMasp) == null || grdPhieuBH.GetCellValue(e.RowHandle, colMasp).ToString() == "")
                {
                    e.SetError("Bạn chưa nhập mã hàng hóa..");
                    e.IsValid = false;
                    return;
                }
                double dDG = 0, dSL = 0;
                try{
                    Double.TryParse(grdPhieuBH.GetCellValue(e.RowHandle, colDonGia).ToString(), out dDG);
                    Double.TryParse(grdPhieuBH.GetCellValue(e.RowHandle, colSoluong).ToString(), out dSL);
                }
                catch { }

                String isQuatang = "";
                try{
                    isQuatang = grdPhieuBH.GetCellValue(e.RowHandle, colQuatang).ToString();
                }
                catch { }
                if (isQuatang != "True"){
                    if (dDG <= 0){
                        e.SetError("Bạn chưa nhập đơn giá hàng hóa..");
                        e.IsValid = false;
                        return;
                    }
                    if (dSL <= 0)
                    {
                        e.SetError("Bạn chưa nhập đơn giá hàng hóa..");
                        e.IsValid = false;
                        return;
                    }
                    e.IsValid = true;
                    fTinhTong();
                }
            }
            catch { }
        }

        private void oTimerClock_Tick(object sender, EventArgs e)
        {
            DateTime d=DateTime.Now;
            lblTime.Content ="["+ d.ToShortDateString()+" "+ d.Hour.ToString()+":"+d.Minute.ToString()+":"+d.Second.ToString() +"]";
        }

        #region "KEYDOWN"
        private void Grid_PreviewKeyDown_1(object sender, System.Windows.Input.KeyEventArgs e){
            try{
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
                else if (e.Key == Key.F9)
                {
                    doStock_Click(sender, e);
                }
                else if (e.Key == Key.Q && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    if (isEdit)
                    {
                        txtBarCode.Focus();
                        txtBarCode.SelectAll();
                    }
                }
                else if (e.Key == Key.I && Keyboard.Modifiers == ModifierKeys.Control) {
                    if (isEdit) {
                        grdPhieuBH.Focus();
                        tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                        tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                        tblView.FocusedColumn = colMasp;
                        tblView.ShowEditor();
                    }
                }  
            }
            catch { }
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
                 e.Handled = true;
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); 
            }
        }
        private void chkKHmoi_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
            else if (e.Key == Key.C) {
                   chkKHmoi.IsChecked =!chkKHmoi.IsChecked;
            }
        }

        private void chkVIP_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
            else if (e.Key == Key.C){
                chkVIP.IsChecked = !chkVIP.IsChecked; 
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
        private void txtGhichu_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                grdPhieuBH.Focus();
                tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = colMasp;
                tblView.ShowEditor();
            }          
        }

        private void colQuatang_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tblView.MoveNextCell();
            }
        }

      
    #endregion

        #region "LOST FOCUS"
   
        private void txtMadv_LostFocus(object sender, RoutedEventArgs e){
            /*
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.None)){
                return;
            }*/

            if (isEdit && txtMadv.Text != "" && txtMadv.Text != null && chkKHmoi.IsChecked==false){
                DataTable oTblDV = new MTSQLServer().wRead(String.Format("SELECT Makh,Maloai,Tenkh,Dienthoai,Diachi FROM DM_KHACHHANG where Makh='{0}'", txtMadv.Text.Trim()),null,false);
                if (oTblDV != null && oTblDV.Rows.Count ==1)
                {
                    foreach (DataRow vR in oTblDV.Rows)
                    {
                        txtTendonvi.Text = vR["Tenkh"].ToString();
                        txtDienthoai.Text = vR["Dienthoai"].ToString();
                        txtDiachi.Text = vR["Diachi"].ToString();
                        break;
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
            if (txtMadv.Text == "") { 
                txtTendonvi.Text = "";
                txtDiachi.Text = "";
                txtDienthoai.Text = "";
            }
        }

        private void txtDienthoai_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }
        private void txtDienthoai_LostFocus(object sender, RoutedEventArgs e){
            if (txtMadv.Text == "" && txtTendonvi.Text=="" && chkKHmoi.IsChecked==false) {
                if (isEdit && txtDienthoai.Text != "" && txtDienthoai.Text != null)
                {
                    DataTable oTblDV = new MTSQLServer().wRead(String.Format("SELECT Makh,Maloai,Tenkh,Dienthoai,Diachi FROM DM_KHACHHANG where makh='{0}'", txtMadv.Text.Trim()), null, false);
                    if (oTblDV != null && oTblDV.Rows.Count == 1)
                    {
                        foreach (DataRow vR in oTblDV.Rows)
                        {
                            txtTendonvi.Text = vR["Tenkh"].ToString();
                            txtDienthoai.Text = vR["Dienthoai"].ToString();
                            txtDiachi.Text = vR["Diachi"].ToString();
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
                if (txtMadv.Text == "")
                {
                    txtTendonvi.Text = "";
                    txtDiachi.Text = "";
                    txtDienthoai.Text = "";
                }
            }
        }
       
      #endregion

        private void txtBarCode_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (isEdit == false) { return; }

            if (e.Key == Key.Enter){                
                try{
                    if (txtBarCode.Text.Trim() != "" && txtBarCode.Text.Length > 5)
                    {
                        String mCode = "";
                        Double mDongia = 0;
                        if (txtBarCode.Text.IndexOf("$") > 0){
                            String[] mBarcode = txtBarCode.Text.Split('$');
                            if (mBarcode.Length > 1)
                            {
                                mCode = mBarcode[0];
                                Double.TryParse(mBarcode[1], out mDongia);
                            }
                            else if (mBarcode.Length > 1){
                                mCode = mBarcode[0];
                            }
                        }
                        else
                        {
                            mCode = txtBarCode.Text.Trim();
                        }
                        AddBarCode(mCode, 1, mDongia);

                        txtBarCode.Text = mCode;
                        txtBarCode.SelectionStart = 0;
                        txtBarCode.SelectAll();
                    }
                }
                catch (Exception ex) { Utils.showMessage("Không thể xử lý dữ liệu mã code:" + ex.Message.ToString()); }
            }
        }
         
        private void txtBarCodeNo_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter){
                int iSL = 0;
                if (txtBarCodeNo.Text != ""){
                    int.TryParse(txtBarCodeNo.Text, out iSL);
                }
                if (iSL <= 0) { iSL = 1; }

                AddBarCode(txtBarCode.Text, iSL);
            }
        }

        private bool FindBarcodeExists(String mMasp,int iSL=1, bool isThung=false, DataRow rowSanPham=null) {
            try{
                int mSL = 0;               
                for (int i = 0; i < grdPhieuBH.VisibleRowCount; i++){
                    int iRowHandle = grdPhieuBH.GetRowHandleByVisibleIndex(i);
                    try{
                        DataRowView vR = (System.Data.DataRowView)grdPhieuBH.GetRow(iRowHandle);
                        if (vR == null)
                        {
                            continue;
                        }

                        // SLThung + 1
                        if (isThung)
                        {
                            if (vR.Row["Masp"].ToString().Trim() == mMasp.Trim())
                            {
                                // Set SLThung
                                int.TryParse(vR["SLThung"].ToString(), out mSL);
                                mSL = mSL + iSL;
                                grdPhieuBH.SetCellValue(iRowHandle, colSoThung, mSL);

                                // Set Soluong
                                Double currentSoLuong = 0;
                                Double dQDThung = 1;
                                Double.TryParse(vR["Soluong"].ToString(), out currentSoLuong);

                                if (rowSanPham != null)
                                {
                                    Double.TryParse(rowSanPham["Quydoithung"].ToString(), out dQDThung);
                                }

                                currentSoLuong += Math.Round(dQDThung * iSL, 0, MidpointRounding.AwayFromZero);
                                grdPhieuBH.SetCellValue(iRowHandle, colSoluong, currentSoLuong);

                                // Lay don gia theo don gia thung
                                Double mDonGiaSi = MTGlobal.GetDonGia(vR["Masp"].ToString(), pDenNgay, "SI");
                                Double mDongia = Math.Round(mDonGiaSi / dQDThung, 0, MidpointRounding.AwayFromZero);
                                grdPhieuBH.SetCellValue(iRowHandle, colDonGia, mDongia);

                                fThanhTienRow(iRowHandle);
                                fTinhTong();
                                return true;
                            } 
                        }

                        if (vR.Row["Masp"].ToString().Trim() == mMasp.Trim()) {
                            int.TryParse(vR["Soluong"].ToString(), out mSL);
                            mSL = mSL + iSL;
                            grdPhieuBH.SetCellValue(iRowHandle, colSoluong, mSL);
                            fThanhTienRow(iRowHandle);
                            fTinhTong();
                            return true;
                        } 
                    }
                    catch(Exception ex) 
                    {
                        Utils.showMessage("Lỗi hệ thống: " + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi hệ thống: " + ex.ToString());
                return false; 
            }
            return false;
        }

        private bool RowExists(String mMasp, int iSL = 1){
            try{
                int mSL = 0;
                for (int i = 0; i < grdPhieuBH.VisibleRowCount; i++){
                    int iRowHandle = grdPhieuBH.GetRowHandleByVisibleIndex(i);
                    try{
                        DataRowView vR = (System.Data.DataRowView)grdPhieuBH.GetRow(iRowHandle);
                        if (vR.Row["Masp"].ToString().Trim() == mMasp.Trim())
                        {
                            int.TryParse(vR["Soluong"].ToString(), out mSL);
                            mSL = mSL + iSL;
                            grdPhieuBH.SetCellValue(iRowHandle, colSoluong, mSL);
                            return true;
                        }
                    }catch { }
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi hệ thống: " + ex.ToString());
                return false; 
            }
            return false;
        }

        private void AddBarCode(String mBarcode,int mSL=0,Double mDongia=0) {
            try{
                if (mBarcode != "") {
                    DataRow[] vRow=null;
                    Double dNguyente = 0,dQDThung = 0,dSL = 0,dSLThung=0;                   
                    String mCode = mBarcode.Substring(1, mBarcode.Length - 1);
                    bool isThung = (mBarcode.Substring(0, 1) == "1" || mBarcode.Substring(0, 1) == "A") ? true : false;

                    if (otblSP != null){
                        vRow = otblSP.Select(string.Format("Maqrcode='{0}' or Mabarcode='{1}'", mCode, mCode));
                    }

                    if (vRow != null && vRow.Length > 0){
                        foreach (DataRow vR in vRow){
                            if (vR["Masp"].ToString() != ""){
                                if (FindBarcodeExists(vR["Masp"].ToString(), mSL, isThung, vR) == true)
                                {
                                    return;
                                }
                               
                                if (isThung){
                                        dSLThung = mSL;
                                        if (mDongia <= 0){
                                            mDongia = MTGlobal.GetDonGia(vR["Masp"].ToString(), pDenNgay, "SI");
                                        }
                                        Double.TryParse(vR["Quydoithung"].ToString(), out dQDThung);
                                        dSL = Math.Round(dQDThung * mSL, 0, MidpointRounding.AwayFromZero);
                                        dNguyente = Math.Round(dSLThung * mDongia, 0, MidpointRounding.AwayFromZero);
                                        if (dSL > 0)
                                        {
                                            mDongia = Math.Round(mDongia / dSL, 0, MidpointRounding.AwayFromZero);
                                        }
                                 }else{
                                        dSLThung = 0;
                                        dSL = mSL;
                                        if (mDongia <= 0)
                                        {
                                            mDongia = MTGlobal.GetDonGia(vR["Masp"].ToString(), pDenNgay);
                                        }
                                        dNguyente = Math.Round(dSL * mDongia, 0, MidpointRounding.AwayFromZero);                                         
                                    }
                                
                                DataRow vRBH = otblBHCT.NewRow();
                                vRBH["Phieubhctid"] = MTGlobal.GetNewID();
                                vRBH["Phieubhid"] = pPhieuID;
                                vRBH["Masp"] = vR["masp"];
                                vRBH["Tensp"] = vR["tensp"];
                                vRBH["Dvt"] = vR["dvt"];
                                vRBH["Quycach"] = vR["quycach"];
                                vRBH["SLThung"] =dSLThung;
                                vRBH["Soluong"] = dSL;
                                vRBH["Dongia"] = mDongia;
                                vRBH["Nguyente"] = dNguyente;
                                vRBH["Thanhtien"] = dNguyente;
                                vRBH["QDThung"] = dQDThung;
                                otblBHCT.Rows.Add(vRBH);
                                                                
                                txtBarCode.SelectAll();
                                break;
                            }
                        }
                        otblBHCT.AcceptChanges();
                        grdPhieuBH.RefreshData();
                        fTinhTong();
                    }
                    else {
                        DataTable oTblBarcode = new MTSQLServer().wRead(String.Format("select top 1 Maspid,Masp,Tensp,Dvt,Quycach,Quydoithung from DM_SANPHAM where Mabarcode='{0}' OR Maqrcode='{1}'", mBarcode,mBarcode), null, false);
                        if (oTblBarcode != null && oTblBarcode.Rows.Count >0){
                            foreach (DataRow vR in oTblBarcode.Rows) {
                                if (vR["Masp"].ToString() != ""){
                                    if (FindBarcodeExists(vR["Masp"].ToString(), mSL,isThung) == true)
                                    {
                                        return;
                                    }

                                    if (isThung){
                                        dSLThung = mSL;
                                        if (mDongia <= 0)
                                        {
                                            mDongia = MTGlobal.GetDonGia(vR["Masp"].ToString(), pDenNgay, "SI");
                                        }
                                        Double.TryParse(vR["Quydoithung"].ToString(), out dQDThung);
                                        dSL = Math.Round(dQDThung * mSL, 0, MidpointRounding.AwayFromZero);
                                        dNguyente = Math.Round(dSLThung * mDongia, 0, MidpointRounding.AwayFromZero);
                                        if (dSL > 0)
                                        {
                                            mDongia = Math.Round(mDongia / dSL, 0, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                    else
                                    {
                                        dSLThung = 0;
                                        dSL = mSL;
                                        if (mDongia <= 0)
                                        {
                                            mDongia = MTGlobal.GetDonGia(vR["Masp"].ToString(), pDenNgay);
                                        }
                                        dNguyente = Math.Round(dSL * mDongia, 0, MidpointRounding.AwayFromZero);
                                    }
                                     
                                    DataRow vRBH = otblBHCT.NewRow();
                                    vRBH["Phieubhctid"] = MTGlobal.GetNewID();
                                    vRBH["Phieubhid"] = pPhieuID;
                                    vRBH["Mavach"] =mBarcode;
                                    vRBH["Masp"] = vR["Masp"];
                                    vRBH["Tensp"] = vR["Tensp"];
                                    vRBH["Dvt"] = vR["Dvt"];
                                    vRBH["Quycach"] = vR["Quycach"];
                                    vRBH["SLThung"] = dSLThung;
                                    vRBH["Soluong"] =dSL;
                                    vRBH["Dongia"] = mDongia;
                                    vRBH["Nguyente"] = dNguyente;
                                    vRBH["Thanhtien"] = dNguyente;
                                    otblBHCT.Rows.Add(vRBH);

                                    txtBarCode.SelectAll();
                                    break;
                                }
                            }
                            otblBHCT.AcceptChanges();
                            grdPhieuBH.RefreshData();
                            fTinhTong();
                        }                   
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi hệ thống: " + ex.ToString());
            }
        }

        private void txtCK_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
        private void txtTTCK_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
        private void txtTientra_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
        private void txtTientra_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            fTinhThanhToan();
        }
        private void txtTongCK_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            fTinhThanhToan();
        }
        private void txtTongTTCK_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            fTinhThanhToan();
        }

        private void cmdDelRow_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pACT_ROLE.isDel)
                {
                    String mMasp = grdPhieuBH.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString();
                    if (Utils.ConfirmMessage(String.Format("Bạn có chắc muốn xóa dòng {0}", mMasp), "Xác nhận", "N"))
                    {
                        tblView.DeleteRow(tblView.FocusedRowHandle);
                    }
                }
                else {
                    Utils.showMessage("Bạn không có quyền xóa mẫu tin...", "Lưu ý");
                }
            }
            catch { }            
        }

        private void txtThanhtoan_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                Double dThanhtoan = 0;
                Double dTientra = 0;
                Double.TryParse(txtThanhtoan.Text, out dThanhtoan);
                Double.TryParse(txtTientra.Text, out dTientra);
                txtTienthua.Text = Math.Round(dThanhtoan - dTientra, 0, MidpointRounding.AwayFromZero).ToString();
            }
            catch { }
        }

        private void txtBarCode_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBarCode.SelectAll();
        }
         
        private void chkThung_Click(object sender, RoutedEventArgs e)
        {
            txtBarCode.Focus();
            txtBarCode.SelectAll();
        }

        private void doStock_Click(object sender, RoutedEventArgs e){
            if (pDenNgay == "") {
                Utils.showMessage("Không xác định được ngày kết tồn kho..", "Thông báo");
                return;
            }

            DataTable oTblSPC = new DataTable();
            oTblSPC.Columns.Add("Maspid", typeof(System.String));
            oTblSPC.Columns.Add("Masp", typeof(System.String));

            dlg_Tonkho oTonkho = new dlg_Tonkho(oTblSPC, pDenNgay);
            oTonkho.Topmost = true;
            oTonkho.ShowDialog();
        }
    }
}