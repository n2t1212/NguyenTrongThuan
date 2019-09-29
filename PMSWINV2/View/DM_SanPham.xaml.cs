using System;
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
 

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Printing;
using System.Windows.Forms;
using DevExpress.Utils;

namespace MTPMSWIN.View
{
   
    public partial class DM_SanPham : Window
    {
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;  
        private const String TABLE_NAME = "DM_SANPHAM";
        private const String ID_NAME = "Maspid";
        private const String CODE_NAME = "Masp";
        private const String CODE_HEADER = "Mã sản phẩm";
        private const String SQL_LOAD_ALL_SP = "select * from DM_SANPHAM where Manhomspid='{0}' order by Masp asc";
        private const String SQL_DELETE_SP = "delete from DM_SANPHAM where Maspid='{0}'" +
            " and Maspid not in (select Maspid from BH_PHIEUBHCT)" +
            " and Maspid not in (select Maspid from NX_PHIEUNXCT)" +
            " and Maspid not in (select Maspid from NX_CHANHXECT)";
        private string SQL_PRODUCT_GROUP = "select * from DM_NHOMSP order by Tennhom asc";
        private CRUDHandling crud = null;

        private String mNhomspid="";
        private DevExpress.Utils.WaitDialogForm Dlg =null;


        public DM_SanPham()
        {
            InitializeComponent();
            MTButton.cmdAdd = this.cmdAdd;
            MTButton.cmdEdit = this.cmdEdit;
            MTButton.cmdDel = this.cmdDel;
            MTButton.cmdSave = this.cmdSave;
            MTButton.cmdAbort = this.cmdAbort;
            MTGlobal.SetFormatGridControl(grdProduct, tblView);
            MTGlobal.SetFormatGridControl(grdProductGroup, tblViewGrp);
            crud = new CRUDHandling(grdProduct, tblView, colMasp, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
            CODE_HEADER, String.Format(SQL_LOAD_ALL_SP,mNhomspid), SQL_DELETE_SP);

            //LOAD PRODUCT GROUP
            DataTable otblNhomSP = new MTSQLServer().wRead(SQL_PRODUCT_GROUP, null, false);
            grdProductGroup.ItemsSource = otblNhomSP;            
            MTGlobal.SetGridReadOnly(grdProductGroup,tblViewGrp, true);
        }

        private void GridForm_Loaded(object sender, RoutedEventArgs e)
        {
           
            String err = crud.GridForm_Loaded();
            if (err.Length > 0)
            {
                //System.Windows.MessageBox.Show(err, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                Utils.showMessage(err, "Thông báo");
            }
        }

        private void DisableEditCode()
        {
            colMasp.AllowEditing = DefaultBoolean.False;
            colMabarcode.AllowEditing = DefaultBoolean.False;
            colMaQr.AllowEditing = DefaultBoolean.False;
        }

        private void EnableEditCode()
        {
            colMasp.AllowEditing = DefaultBoolean.True;
            colMabarcode.AllowEditing = DefaultBoolean.True;
            colMaQr.AllowEditing = DefaultBoolean.True;
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (mNhomspid == "") { 
                System.Windows.MessageBox.Show("Bạn chưa chọn nhóm sản phẩm..","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }

            crud.cmdAdd_Click();
            
            DisableEditCode();

            colMasp.AllowEditing = DefaultBoolean.True;
            
        }
        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            crud.cmdEdit_Click();
            DisableEditCode();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            DisableEditCode();
            String msg = crud.cmdSave_Click();
            if (msg != "")
            {
                //System.Windows.MessageBox.Show(msg);
                Utils.showMessage(msg, "Thông báo");
            }
        }

        private void cmdDel_Click(object sender, RoutedEventArgs e)
        {
            String msg = crud.cmdDel_Click();
            if (msg != "")
            {
                //System.Windows.MessageBox.Show(msg);
                Utils.showMessage("Sản phẩm đang được sử dụng. Không được xóa sản phẩm.", "Thông báo");
            }
        }

        private void cmdAbort_Click(object sender, RoutedEventArgs e)
        {
            DisableEditCode();
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

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            new MTReport().rptDM_SanPham(); 
        }


        private void tblViewGrp_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            try
            {
                mNhomspid = grdProductGroup.GetCellValue(tblViewGrp.FocusedRowHandle, colManhomSPID1).ToString();
                crud = new CRUDHandling(grdProduct, tblView, colMasp, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
                                         CODE_HEADER, String.Format(SQL_LOAD_ALL_SP, mNhomspid), SQL_DELETE_SP);

                String err = crud.GridForm_Loaded();
                MTGlobal.SetGridReadOnly(grdProduct,tblView, true);
            }
            catch { }
          
        }

        private void tblView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
                
        }

        private void tblView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {
            try{
                grdProduct.SetCellValue(e.RowHandle, colSPID, MTGlobal.GetNewID());
                grdProduct.SetCellValue(e.RowHandle, colManhomSPID, mNhomspid);
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
            if (tblView.FocusedColumn.FieldName == colMasp.FieldName.ToString() || tblView.FocusedColumn == colTenSP)
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
            if (mNhomspid.Length == 0)
            {
                Utils.showMessage("Vui lòng chọn nhóm sản phẩm", "Thông báo");
                return;
            }

            dlg_ImportSanPham importSP = new dlg_ImportSanPham(mNhomspid);
            importSP.Show();
        }
		
        private void cmdInBarcode_Click(object sender, RoutedEventArgs e){
            try{
                dlg_ChonInmavach dlgChon = new dlg_ChonInmavach(); 
                dlgChon.ShowDialog();
                if(dlgChon.isChon){
                  
                  DataTable otblSP=new DataTable();      
                  otblSP.Columns.Add("Maspid", typeof(String));
                  otblSP.Columns.Add("Masp", typeof(String));
                  otblSP.Columns.Add("Soluong", typeof(int));
                  if(dlgChon.otblChon!=null){
                     foreach(DataRow vRow in dlgChon.otblChon.Rows){
                       DataRow vR=otblSP.NewRow();
                         vR["Maspid"]=vRow["Maspid"];
                         vR["Masp"]=vRow["Masp"];
                         vR["Soluong"] = vRow["Soluong"];
                         otblSP.Rows.Add(vR);
                     }
                      otblSP.AcceptChanges();
                  }

                  if (System.Windows.MessageBox.Show("Bạn muốn in mẫu 4 tem/dòng ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                  {
                      new MTReport().rptInMaVach(otblSP, dlgChon.isDongia, dlgChon.isMaQR, "25_15");
                  }
                  else {
                      new MTReport().rptInMaVach(otblSP, dlgChon.isDongia, dlgChon.isMaQR, "35_22");
                  }                 
                 
                }
               
            }
            catch (Exception ex) {Utils.showMessage("không thể in mã vạch: "+ex.Message.ToString(),"In mã vạch"); }             
                
        }

        private void cmdDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog fdl = new FolderBrowserDialog();
                if (fdl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.File.Exists(fdl.SelectedPath + @"\Bieu_mau_san_pham.xlsx")){
                        Utils.showMessage("File mẫu đang tồn tại trong thư mục. Vui lòng kiểm tra lại..", "Thông báo");
                    }else{
                        System.IO.File.Copy(@"Bieu_mau_san_pham.xlsx", fdl.SelectedPath + @"\Bieu_mau_san_pham.xlsx");
                        Utils.showMessage("Đã tải file mẫu thành công.", "Thông báo");
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi tải file. Vui lòng kiểm tra đường dẫn", "Thông báo");
                return;
            }
        }

        private void Grid_PreviewKeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
             switch (e.Key)
            {
                case Key.F2:
                    crud.cmdAdd_Click();
                    DisableEditCode();
                    colMasp.AllowEditing = DefaultBoolean.True;
                    break;

                case Key.F3:
                    crud.cmdEdit_Click();
                    break;
                case Key.F4:
                    String msgDel = crud.cmdDel_Click();
                    if (msgDel != "")
                    {
                        Utils.showMessage("Sản phẩm đang được sử dụng. Không được xóa sản phẩm.", "Thông báo");
                    }
                    break;
                case Key.F5:
                    DisableEditCode();
                    String msgSave = crud.cmdSave_Click();
                    if (msgSave != "")
                    {
                        Utils.showMessage(msgSave, "Thông báo");
                    }
                    break;
                case Key.Escape:
                    crud.cmdAbort_Click();
                    DisableEditCode();
                    break;
                case Key.F8:
                    Utils.fExit();
                    break;
            }
        }

        private void grdProduct_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Press CTRL + E to edit masp, barcode and QR code
            if (e.Key == Key.E && Keyboard.Modifiers == ModifierKeys.Control)
            {
                EnableEditCode();
            }

            // Press CTRL + U to disable edit masp, barcode and QR code
            if (e.Key == Key.U && Keyboard.Modifiers == ModifierKeys.Control)
            {
                DisableEditCode();
            }

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (tblView.FocusedColumn.FieldName == "Masp" && tblView.FocusedColumn.Name == "colMasp")
                {
                    if (grdProduct.GetFocusedRowCellValue(colMasp) == null || grdProduct.GetFocusedRowCellValue(colMasp).ToString() == "")
                    {
                        tblView.FocusedColumn = colMasp;
                        return;
                    }

                    String sMasp = grdProduct.GetCellValue(tblView.FocusedRowHandle, colMasp).ToString();
                    if (sMasp == null || sMasp == "")
                    {
                        tblView.FocusedColumn = colMasp;
                        return;
                    }

                    string errDuplicateSP = ValidateHelper.checkCodeValueNotDuplicate(TABLE_NAME, CODE_NAME, sMasp, "Mã SP");

                    if (errDuplicateSP.Length > 0)
                    {
                        tblView.FocusedColumn = colMasp;
                        Utils.showMessage(errDuplicateSP, "Thông báo");
                        return;
                    }

                    grdProduct.SetCellValue(tblView.FocusedRowHandle, colMabarcode, sMasp);
                    grdProduct.SetCellValue(tblView.FocusedRowHandle, colMaQr, sMasp);
                }
            }

        }

    }
}
