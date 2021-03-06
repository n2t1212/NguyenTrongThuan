﻿using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MTPMSWIN.View
{
 
    public partial class DM_BangGia: Window
    {
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        // DM san pham
        private const String TABLE_NAME = "DM_BANGGIA_CHITIET";
        private const String ID_NAME = "Banggiactid";
        private const String CODE_NAME = "Masp";
        private const String CODE_HEADER = "Mã SP";
        private const String SQL_LOAD_ALL_BGCT = "select bgct.* from DM_BANGGIA_CHITIET bgct where bgct.Banggiaid='{0}' order by bgct.Masp asc";
        private const String SQL_DELETE_BGCT = "delete from DM_BANGGIA_CHITIET where Banggiactid='{0}'" +
            " and (select count(*) from BH_PHIEUBHCT b where Maspid = b.Maspid) = 0" +
            " and (select count(*) from NX_PHIEUNXCT c where Maspid = c.Maspid) = 0" +
            " and (select count(*) from NX_CHANHXECT d where Maspid = d.Maspid) = 0";
        private string SQL_PRODUCT_GROUP = "select * from DM_BANGGIA order by Banggia asc";
        private CRUDHandling crud = null;
        private CRUDHandling crud_BangGia = null;
        private String mBangGiaId="";
        private DevExpress.Utils.WaitDialogForm Dlg =null;
        private DataTable otblSP = null;

        private void LoadSanPham()
        {
            try
            {
                String mSql = "SELECT Maspid,Manhomspid,Masp,Tensp,Dvt,Quycach,Quydoikgl,Quydoithung,Nhacungcap FROM DM_SANPHAM with(nolock) order by Masp asc";
                otblSP = new MTSQLServer().wRead(mSql, null, false);
            }
            catch (Exception ex) { }
        }

        public DM_BangGia()
        {
            InitializeComponent();
            MTButton.cmdAdd = this.cmdAdd;
            MTButton.cmdEdit = this.cmdEdit;
            MTButton.cmdDel = this.cmdDel;
            MTButton.cmdSave = this.cmdSave;
            MTButton.cmdAbort = this.cmdAbort;
            MTGlobal.SetFormatGridControl(grdBangGiaCT, tblViewCT);
            MTGlobal.SetFormatGridControl(grdBangGiaCT, tblViewBG);
            

            Dlg = Utils.shwWait();

            //LOAD PRODUCT GROUP
            //DataTable otblNhomSP = new MTSQLServer().wRead(SQL_PRODUCT_GROUP, null, false);
            //grdBangGia.ItemsSource = otblNhomSP;            
            crud_BangGia = new CRUDHandling(grdBangGia, tblViewBG, colBanggia, MTROLE, MTButton, "DM_BANGGIA", "Banggiaid", CODE_NAME,
                                     CODE_HEADER, SQL_PRODUCT_GROUP, "");

            String err = crud_BangGia.GridForm_Loaded();
            MTGlobal.SetGridReadOnly(grdBangGia, tblViewBG, true);

            crud = new CRUDHandling(grdBangGiaCT, tblViewCT, colMasp, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
            CODE_HEADER, String.Format(SQL_LOAD_ALL_BGCT, mBangGiaId), SQL_DELETE_BGCT);

            System.Threading.Thread oThreSP = new System.Threading.Thread(LoadSanPham);
            oThreSP.Start();

            Utils.closeWait(Dlg);
        }

        private void GridForm_Loaded(object sender, RoutedEventArgs e)
        {
           
            String err = crud.GridForm_Loaded();
            if (err.Length > 0)
            {
                //System.Windows.MessageBox.Show(err, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                Utils.showMessage("Có lỗi trong quá trình xử lý", "Thông báo");
            }

        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (mBangGiaId == "") { 
                System.Windows.MessageBox.Show("Bạn chưa chọn nhóm sản phẩm..","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }

            crud.cmdAdd_Click();
            tblViewCT.FocusedColumn = colMasp;
        }
        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            crud.cmdEdit_Click();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            string msg = crud.cmdSave_Click();

            if (msg != "")
            {
                Utils.showMessage(msg, "Thông báo");
            }
        }

        private void cmdDel_Click(object sender, RoutedEventArgs e)
        {
            String msg = crud.cmdDel_Click();
            if (msg != "")
            {
                Utils.showMessage(msg, "Thông báo");
            }
        }

        private void cmdAbort_Click(object sender, RoutedEventArgs e)
        {
            crud.cmdAbort_Click();
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MTMain oMain = System.Windows.Application.Current.Windows.OfType<MTMain>().FirstOrDefault();
                if (oMain != null)
                {
                    oMain.onExitTab();
                }
            }
            catch { }
        }

        private void tblViewGrp_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            try
            {
                mBangGiaId = grdBangGia.GetCellValue(tblViewBG.FocusedRowHandle, colBanggiaID).ToString();
                crud = new CRUDHandling(grdBangGiaCT, tblViewCT, colMasp, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
                                         CODE_HEADER, String.Format(SQL_LOAD_ALL_BGCT, mBangGiaId), SQL_DELETE_BGCT);

                String err = crud.GridForm_Loaded();
                MTGlobal.SetGridReadOnly(grdBangGiaCT, tblViewCT, true);
            }
            catch { }
          
        }

        private void tblView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
                
        }

        private void tblView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {
            try{
                grdBangGiaCT.SetCellValue(e.RowHandle, colBanggiactid, MTGlobal.GetNewID());
                grdBangGiaCT.SetCellValue(e.RowHandle, colBanggiaid2, mBangGiaId);
            }
            catch { }
        }

        private void tblView_InvalidRowException(object sender, DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.Xpf.Grid.ExceptionMode.NoAction; 
        }

        private void tblView_ValidateRow(object sender, DevExpress.Xpf.Grid.GridRowValidationEventArgs e)
        {
            if (MTGlobal.MT_CURRENT_ACTION != "ADD" && MTGlobal.MT_CURRENT_ACTION != "EDIT") {
                return;
            }
            if (tblViewCT.FocusedColumn.FieldName == colMasp.FieldName.ToString() || tblViewCT.FocusedColumn == colTenSP)
            {
                if (e.Value == null || e.Value.ToString() == "") {
                    e.SetError("Thông tin không được bỏ trống...");
                    e.IsValid = false;
                    return;
                }
               
            }       
        } 

        private void tblView_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            e.Source.PostEditor();
        }

        private void doImport(object sender, RoutedEventArgs e)
        {
            if (mBangGiaId.Length == 0)
            {
                Utils.showMessage("Vui lòng chọn bảng giá", "Thông báo");
                return;
            }

            dlg_ImportBanggia importView = new dlg_ImportBanggia(mBangGiaId);
            importView.Show();

        }

        private void cmdDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog fdl = new FolderBrowserDialog();
                if (fdl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.Copy(@"Bieu_mau_gia_san_pham.xlsx", fdl.SelectedPath + @"\Bieu_mau_gia_san_pham.xlsx");
                    Utils.showMessage("Đã tải file mẫu thành công.", "Thông báo");
                    return;
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi tải file. Vui lòng kiểm tra đường dẫn", "Thông báo");
                return;
            }
           
        }

        //private void tblViewCT_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        //{

        //}

        private void tblViewCT_PreviewKeyDown1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:
                    crud.cmdAdd_Click();
                    break;

                case Key.F3:
                    crud.cmdEdit_Click();
                    break;
                case Key.F4:
                    String msg = crud.cmdDel_Click();
                    if (msg != "")
                    {
                        Utils.showMessage(msg, "Thông báo");
                    }
                    break;
                case Key.F5:
                    String msgSave = crud.cmdSave_Click();
                    if (msgSave != "")
                    {
                        Utils.showMessage(msgSave, "Thông báo");
                    }
                    break;
                case Key.Escape:
                    crud.cmdAbort_Click();
                    break;
                case Key.F8:
                    Utils.fExit();
                    break;
            }

            if (e.Key == Key.Enter)
            {
                if (tblViewCT.FocusedColumn.FieldName == "Masp" && tblViewCT.FocusedColumn.Name == "colMasp")
                {
                    if (grdBangGiaCT.GetFocusedRowCellValue(colMasp) == null || grdBangGiaCT.GetFocusedRowCellValue(colMasp).ToString() == "")
                    {
                        tblViewCT.FocusedColumn = colMasp;
                        return;
                    }

                    String sMasp = grdBangGiaCT.GetCellValue(tblViewCT.FocusedRowHandle, colMasp).ToString();
                    if (sMasp == null || sMasp == "")
                    {
                        tblViewCT.FocusedColumn = colMasp;
                        return;
                    }

                    if (sMasp != "")
                    {
                        if (otblSP != null && otblSP.Rows.Count > 0)
                        {
                            DataRow[] vRow = otblSP.Select(string.Format("Masp='{0}'", sMasp));

                            if (vRow.Length > 0)
                            {
                                string errDuplicateSP = ValidateHelper.checkCodeValueNotDuplicate(TABLE_NAME, CODE_NAME, sMasp, "Mã SP");

                                if (errDuplicateSP.Length > 0)
                                {
                                    tblViewCT.FocusedColumn = colMasp;
                                    Utils.showMessage(errDuplicateSP, "Thông báo");
                                    return;
                                }

                                grdBangGiaCT.SetCellValue(tblViewCT.FocusedRowHandle, colMaspid, vRow[0]["Maspid"].ToString());
                                grdBangGiaCT.SetCellValue(tblViewCT.FocusedRowHandle, colTenSP, vRow[0]["Tensp"].ToString());
                            }
                            else
                            {
                                dlg_ChonSanPham oChonSP = new dlg_ChonSanPham(otblSP);
                                oChonSP.ShowDialog();
                                DataRowView vRw = oChonSP.pRowChon;
                                if (vRw != null && vRw.Row.ItemArray.Length > 0)
                                {
                                    string errDuplicateSP = ValidateHelper.checkCodeValueNotDuplicate(TABLE_NAME, CODE_NAME, vRw.Row.ItemArray.GetValue(2).ToString(), "Mã SP");

                                    if (errDuplicateSP.Length > 0)
                                    {
                                        Utils.showMessage(errDuplicateSP, "Thông báo");
                                        return;
                                    }

                                    grdBangGiaCT.SetCellValue(tblViewCT.FocusedRowHandle, colMaspid, vRw.Row.ItemArray.GetValue(0).ToString());
                                    grdBangGiaCT.SetCellValue(tblViewCT.FocusedRowHandle, colTenSP, vRw.Row.ItemArray.GetValue(3).ToString());
                                    sMasp = vRw.Row.ItemArray.GetValue(2).ToString().ToUpper();
                                }
                                else
                                {
                                    tblViewCT.FocusedColumn = colMasp;
                                }
                            }
                            tblViewCT.FocusedColumn = colTenSP;
                        }
                        tblViewCT.CloseEditor();
                        tblViewCT.ShowEditor();
                        grdBangGiaCT.SetCellValue(tblViewCT.FocusedRowHandle, colMasp, sMasp);
                        //tblView.PostEditor();
                    }
                }
            }

            if (e.Key == Key.Delete)
            {
                try
                {
                    if (grdBangGiaCT.GetFocusedRowCellValue(colMaspid) == null || grdBangGiaCT.GetFocusedRowCellValue(colMaspid).ToString() == "")
                    {
                        e.Handled = true;
                        return;
                    }
                    if (Utils.ConfirmMessage(String.Format("Bạn có chắc muốn xóa {0} không ?", grdBangGiaCT.GetCellValue(tblViewCT.FocusedRowHandle, colMasp).ToString()), "Xác nhận xóa") == true)
                    {
                        tblViewCT.DeleteRow(tblViewCT.FocusedRowHandle);
                    }
                }
                catch (Exception ex) { ex.Data.Clear(); }
            }
        }
    }
}
