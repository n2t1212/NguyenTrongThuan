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
    
    public partial class HT_ThamSo : Window
    { 
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;
                
        private const String TABLE_NAME = "HT_PARA";
        private const String ID_NAME = "PARA_NAME";
        private const String CODE_NAME = "PARA_NAME";
        private const String CODE_HEADER = "Tham Số";
        private const String SQL_LOAD_ALL_SP = "select * from HT_PARA order by PARA_NAME asc";
        private const String SQL_DELETE_SP = "delete from HT_PARA where PARA_NAME='{0}'";

        private CRUDHandling crud = null;

        public HT_ThamSo(){
            InitializeComponent();
            MTButton.cmdAdd = this.cmdAdd;
            MTButton.cmdEdit = this.cmdEdit;
            MTButton.cmdDel = this.cmdDel;
            MTButton.cmdSave = this.cmdSave;
            MTButton.cmdAbort = this.cmdAbort;
            MTGlobal.SetFormatGridControl(grdPara, tblView);

            crud = new CRUDHandling(grdPara, tblView, colParaName, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
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
            Utils.fExit();
        }
           
        private void tblView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
                
        }

        private void tblView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {
            try
            {
                //grdProductGroup.SetCellValue(e.RowHandle, colManhomSPID, MTGlobal.GetNewID()); 
                
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
            if (tblView.FocusedColumn.FieldName == colParaName.FieldName.ToString() || tblView.FocusedColumn ==colParaName)
            {
                if (e.Value == null || e.Value.ToString() == "") {
                    e.SetError("Vui lòng nhập đủ thông tin...");
                    e.IsValid = false;
                    return;
                }
            }
            if (tblView.FocusedColumn.FieldName == colParaVal.FieldName.ToString() || tblView.FocusedColumn == colParaVal)
            {
                if (e.Value == null || e.Value.ToString() == "")
                {
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
                    String msg = crud.cmdDel_Click();
                    if (msg != "")
                    {
                        MessageBox.Show(msg, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
