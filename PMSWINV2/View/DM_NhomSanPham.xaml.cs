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

namespace MTPMSWIN.View
{
    
    public partial class DM_NhomSanPham : Window
    { 
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        
        private const String TABLE_NAME = "DM_NHOMSP";
        private const String ID_NAME = "manhomspid";
        private const String CODE_NAME = "manhom";
        private const String CODE_HEADER = "Mã Nhóm";
        private const String SQL_LOAD_ALL_SP = "select * from DM_NHOMSP order by tennhom asc";
        private const String SQL_DELETE_SP = "delete from DM_NHOMSP where manhomspid='{0}'";

        private CRUDHandling crud = null;

        public DM_NhomSanPham()
        {
            InitializeComponent();
            MTButton.cmdAdd = this.cmdAdd;
            MTButton.cmdEdit = this.cmdEdit;
            MTButton.cmdDel = this.cmdDel;
            MTButton.cmdSave = this.cmdSave;
            MTButton.cmdAbort = this.cmdAbort;
            MTGlobal.SetFormatGridControl(grdProductGroup, tblView);

            crud = new CRUDHandling(grdProductGroup, tblView,colManhom, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
            CODE_HEADER, SQL_LOAD_ALL_SP, SQL_DELETE_SP);
        }

        private void GridForm_Loaded(object sender, RoutedEventArgs e)
        {
            
            String err = crud.GridForm_Loaded();
            if (err.Length > 0)
            {
                MessageBox.Show(err, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            crud.cmdAdd_Click();           
        }
        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            crud.cmdEdit_Click();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            String msg = crud.cmdSave_Click();
            if (msg != "")
            {
                MessageBox.Show(msg);
            }
        }

        private void cmdDel_Click(object sender, RoutedEventArgs e)
        {
            String msg = crud.cmdDel_Click();
            if (msg != "")
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MTMain oMain = Application.Current.Windows.OfType<MTMain>().FirstOrDefault();
                if (oMain != null)
                {
                    oMain.onExitTab();
                }
            }
            catch { }
        }
           
        private void tblView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
                
        }

        private void tblView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {
            try
            {
                grdProductGroup.SetCellValue(e.RowHandle, colManhomSPID, MTGlobal.GetNewID());
                grdProductGroup.SetCellValue(e.RowHandle, colNgaylap, DateTime.Now.ToLongDateString());
                grdProductGroup.SetCellValue(e.RowHandle, colNguoilap, MTGlobal.MT_USER_LOGIN);
            }
            catch { }
        }

        private void tblView_InvalidRowException(object sender, DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.Xpf.Grid.ExceptionMode.NoAction; 
        }

        private void tblView_ValidateRow(object sender, DevExpress.Xpf.Grid.GridRowValidationEventArgs e)
        {
            if (e.Row == null) return;
            if (tblView.FocusedColumn.FieldName == colManhom.FieldName.ToString() || tblView.FocusedColumn ==colTenNhom)
            {
                if (e.Value == null || e.Value.ToString() == "") {
                    e.SetError("Vui lòng nhập đủ thông tin...");
                    e.IsValid = false;
                    return;
                }
            }                 
        } 
        private void tblView_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            e.Source.PostEditor();
        }

        private void Grid_PreviewKeyDown_1(object sender, KeyEventArgs e)
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
                    String msgDel = crud.cmdDel_Click();
                    if (msgDel != "")
                    {
                        MessageBox.Show(msgDel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                case Key.F5:
                    String msgSave = crud.cmdSave_Click();
                    if (msgSave != "")
                    {
                        MessageBox.Show(msgSave, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                case Key.Escape:
                    crud.cmdAbort_Click();
                    break;
                case Key.F8:
                    Utils.fExit();
                    break;
            }
        }


    }
}
