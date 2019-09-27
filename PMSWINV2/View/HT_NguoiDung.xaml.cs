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

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for HT_NguoiDung.xaml
    /// </summary>
    public partial class HT_NguoiDung : Window
    {
        private const string NEW = "";
        private const String TABLE_NAME = "HT_NGUOIDUNG";
        private const String ID_NAME = "soid";
        private const String CODE_NAME = "manv";
        private const String CODE_HEADER = "Mã nhân viên";
        private const String SQL_LOAD_ALL_ND = "select nd.*, bp.tenbophan as tenbophan, nq.tennhom as tennhomquyen from HT_NGUOIDUNG nd left join DM_NHANVIEN nv" + 
                                                        " on nd.manv = nv.Manv left join DM_BOPHAN bp on nv.Mabp = bp.mabophan left join HT_QUYENHAN qh " +
                                                        " on qh.soid_nguoidung = nd.soid left join HT_NHOMQUYEN nq on nq.soid = qh.soid_nhomquyen " + 
                                                        " where bp.mabophan='{0}' order by nd.manv";
        private const String SQL_DELETE_ND = "delete from HT_NGUOIDUNG where soid='{0}'";
        private CRUDHandling crud = null;

        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        private string maBoPhan = "";
        private string tenBoPhan = "";

        public HT_NguoiDung()
        {
            InitializeComponent();
            grid.ItemsSource = new ListDM_BoPhan().GetListBoPhan();
            treeListView1.ExpandAllNodes();

            MTGlobal.SetFormatGridControl(grdNguoiDung, tblView);
            MTGlobal.SetFormatGridControl(grdNguoiDung, tblView);
            crud = new CRUDHandling(grdNguoiDung, tblView, colMaNV, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
            CODE_HEADER, String.Format(SQL_LOAD_ALL_ND, maBoPhan), SQL_DELETE_ND);
        }

        private void loadForm(object sender, RoutedEventArgs e)
        {
            String err = crud.GridForm_Loaded();
            if (err.Length > 0)
            {
                MessageBox.Show(err, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tblView_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            e.Source.PostEditor();
        }

        private void getUserInfo(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            maBoPhan = ((MTPMSWIN.Model.DM_BoPhan)(e.NewRow)).mabophan;
            tenBoPhan = ((MTPMSWIN.Model.DM_BoPhan)(e.NewRow)).tenbophan;
            crud = new CRUDHandling(grdNguoiDung, tblView, colMaNV, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
            CODE_HEADER, String.Format(SQL_LOAD_ALL_ND, maBoPhan), SQL_DELETE_ND);
            crud.GridForm_Loaded();

        }

        private void fAddStaff()
        {
            dlg_ThemNhanVien addNV = new dlg_ThemNhanVien(maBoPhan, tenBoPhan);
            addNV.Show();
        }

        private void fAddUser()
        {
            dlg_NguoiDung addND = new dlg_NguoiDung(maBoPhan, NEW);
            addND.Show();
        }

        private void fRemoveUser()
        {
            if (grdNguoiDung.VisibleRowCount < 1)
            {
                Utils.showMessage("Danh sách người dùng trống", "Thông báo");
                return;
            }
            string soid = grdNguoiDung.GetFocusedRowCellValue("soid").ToString();

            string sql = String.Format(SQL_DELETE_ND, soid);
            try
            {
                int result = MTSQLServer.getMTSQLServer().wExec(sql, null, false);
                if (result == 1)
                {
                    crud.GridForm_Loaded();
                    Utils.showMessage(Utils.SAVE_DB_OK, "Thông báo");
                }
                else
                {
                    Utils.showMessage(Utils.ERR_UPDATE_DB, "Thông báo");
                }

            }
            catch (Exception ex) { }
        }

        private void cmdThemNV_Click(object sender, RoutedEventArgs e)
        {
            fAddStaff();
        }

        private void cmdThemND_Click_1(object sender, RoutedEventArgs e)
        {
            fAddUser();
        }

        private void onEditNguoiDung(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {
            string id = grdNguoiDung.GetCellValue(e.HitInfo.RowHandle, grdNguoiDung.Columns[0]).ToString();
            dlg_NguoiDung addND = new dlg_NguoiDung(maBoPhan, id);
            
            addND.Show();
        }

        private void cmdXoaND_Click(object sender, RoutedEventArgs e)
        {
            fRemoveUser();
        }

        private void Grid_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:
                    fAddStaff();
                    break;
                case Key.F3:
                    fAddUser();
                    break;
                case Key.F4:
                    fRemoveUser();
                    break;
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            Utils.fExit();
        }
       
    }
}
