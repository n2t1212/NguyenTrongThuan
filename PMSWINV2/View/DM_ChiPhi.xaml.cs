using MTPMSWIN.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MTPMSWIN.View
{
    public partial class DM_ChiPhi : Window
    {
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;


        private const String TABLE_NAME = "DM_CHIPHI";
        private const String ID_NAME = "Macp";
        private const String CODE_NAME = "Macp";
        private const String CODE_HEADER = "Mã chi phí";
        private const String SQL_LOAD_ALL_CP = "select * from DM_CHIPHI order by Macp asc";
        private const String SQL_DELETE_CP = "delete from DM_CHIPHI where Macp='{0}'";

        private CRUDHandling crud = null;

        public DM_ChiPhi()
        {
            InitializeComponent();
            MTButton.cmdAdd = this.cmdAdd;
            MTButton.cmdEdit = this.cmdEdit;
            MTButton.cmdDel = this.cmdDel;
            MTButton.cmdSave = this.cmdSave;
            MTButton.cmdAbort = this.cmdAbort;
            MTGlobal.SetFormatGridControl(grdChiPhi, tblView);

            crud = new CRUDHandling(grdChiPhi, tblView, colMaChiPhi, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
            CODE_HEADER, SQL_LOAD_ALL_CP, SQL_DELETE_CP);
        }

        private void grdChiPhi_Loaded(object sender, RoutedEventArgs e)
        {
            String err = crud.GridForm_Loaded();
            if (err.Length > 0)
            {
                MessageBox.Show(err, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                MessageBox.Show(msg, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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


        private void tblView_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            e.Source.PostEditor();
        }

        private void tblView_InvalidRowException(object sender, DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.Xpf.Grid.ExceptionMode.NoAction;
        }

        private void tblView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

        }

        private void tblView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {
            try
            {
                grdChiPhi.SetCellValue(e.RowHandle, colMaChiPhi, MTGlobal.GetNewID());

            }
            catch { }
        }

        private void tblView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tblView.FocusedColumn.FieldName == "Matk" && tblView.FocusedColumn.Name == "colMaChiPhi")
                {
                    if (grdChiPhi.GetFocusedRowCellValue(colMaChiPhi) == null || grdChiPhi.GetFocusedRowCellValue(colMaChiPhi).ToString() == "")
                    {
                        tblView.FocusedColumn = colMaChiPhi;
                        return;
                    }

                    string maCp = grdChiPhi.GetCellValue(tblView.FocusedRowHandle, colMaChiPhi).ToString();

                    string errDuplicateCP = ValidateHelper.checkCodeValueNotDuplicate(TABLE_NAME, CODE_NAME, maCp, "Mã chi phí");

                    if (errDuplicateCP.Length > 0)
                    {
                        Utils.showMessage(errDuplicateCP, "Thông báo");
                        tblView.FocusedColumn = colMaChiPhi;
                        return;
                    }
                }
            }
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
