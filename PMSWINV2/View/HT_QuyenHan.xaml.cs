using MTPMSWIN.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// <summary>
    /// Interaction logic for HT_QuyenHan.xaml
    /// </summary>
    public partial class HT_QuyenHan : Window
    {
        private CRUDHandling crud = null;
        private string SQL_NHOMQUYEN = "select * from HT_NHOMQUYEN order by manhom";
        private string nhomquyenID = "";

        private DataTable tblNQCN = null;

        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;
        private const String TABLE_NAME = "HT_NHOMQUYEN_CHUCNANG";
        private const String ID_NAME = "soid";
        private const String CODE_NAME = "";
        private const String CODE_HEADER = "";
        private const String SQL_LOAD_ALL_QHCN = "select nqcn.soid as soid, nqcn.macn as macn, cn.chucnang as chucnang, " + 
                                                 "nqcn.them as them, nqcn.xoa as xoa, nqcn.sua as sua, nqcn.duyet as duyet, nqcn.[in] as [in]" +
                                                  " from HT_NHOMQUYEN_CHUCNANG nqcn left join DM_CHUCNANG cn on cn.macn = nqcn.macn" +
                                                  " where nqcn.manhom='{0}' order by manhom asc";
        private const String SQL_DELETE_QH = "delete from HT_NHOMQUYEN_CHUCNANG where soid = '{0}'";

        public HT_QuyenHan()
        {
            InitializeComponent();
        }

        private void InitTableNhomQuyenChucNang()
        {
            tblNQCN = new DataTable();
            tblNQCN.Columns.Add("soid", typeof(string));
            tblNQCN.Columns.Add("manhom", typeof(string));
            tblNQCN.Columns.Add("macn", typeof(string));
            tblNQCN.Columns.Add("them", typeof(int));
            tblNQCN.Columns.Add("sua", typeof(int));
            tblNQCN.Columns.Add("xoa", typeof(int));
            tblNQCN.Columns.Add("duyet", typeof(int));
            tblNQCN.Columns.Add("in", typeof(int));
            tblNQCN.Columns.Add("nguoilap", typeof(string));
            tblNQCN.Columns.Add("ngaylap", typeof(DateTime));
            tblNQCN.Columns.Add("nguoisua", typeof(string));
            tblNQCN.Columns.Add("ngaysua", typeof(DateTime));
        }

        private void formLoad(object sender, RoutedEventArgs e)
        {
            DataTable otblNhomSP = new MTSQLServer().wRead(SQL_NHOMQUYEN, null, false);
            grdNhomQuyen.ItemsSource = otblNhomSP;
            MTGlobal.SetGridReadOnly(grdNhomQuyen, tblViewGrp, true);
        }

        private void onFocusRowChange(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            nhomquyenID = grdNhomQuyen.GetCellValue(tblViewGrp.FocusedRowHandle, colsoid).ToString();
            crud = new CRUDHandling(grdQuyenHan, tblView, colChucNang, MTROLE, MTButton, TABLE_NAME, ID_NAME, CODE_NAME,
                                     CODE_HEADER, String.Format(SQL_LOAD_ALL_QHCN, nhomquyenID), string.Format(SQL_DELETE_QH,nhomquyenID));

            String err = crud.GridForm_Loaded();
            MTGlobal.SetGridReadOnly(grdQuyenHan, tblView, true);
        }

        private void fAddNhomQuyen()
        {
            dlg_AddNhomQuyen frmAddNQ = new dlg_AddNhomQuyen();
            frmAddNQ.Show();
        }

        private void fAddQuyenHan()
        {
            dlg_AddQuyenHan frmAddQH = new dlg_AddQuyenHan(this.nhomquyenID);
            frmAddQH.Show();
        }

        private void fSaveQuyenHan()
        {
            cmdEdit.Visibility = Visibility.Visible;
            cmdDelete.Visibility = Visibility.Visible;

            int rowNumber = grdQuyenHan.VisibleRowCount;

            if (rowNumber == 0)
            {
                Utils.showMessage("Nhóm quyền chức năng trống. Vui lòng thêm.", "Thông báo");
                return;
            }

            InitTableNhomQuyenChucNang();
            for (int i = 0; i < rowNumber; i++)
            {
                if (grdQuyenHan.GetCellValue(i, "soid") == null)
                {
                    continue;
                }
                string id = grdQuyenHan.GetCellValue(i, "soid").ToString();

                if (id != null)
                {
                    DataRow row = tblNQCN.NewRow();

                    row["soid"] = id;
                    row["manhom"] = nhomquyenID;
                    row["macn"] = grdQuyenHan.GetCellValue(i, "macn").ToString();
                    row["them"] = bool.Parse(grdQuyenHan.GetCellValue(i, "them").ToString()) == true ? 1 : 0;
                    row["sua"] = bool.Parse(grdQuyenHan.GetCellValue(i, "sua").ToString()) == true ? 1 : 0;
                    row["xoa"] = bool.Parse(grdQuyenHan.GetCellValue(i, "xoa").ToString()) == true ? 1 : 0;
                    row["duyet"] = bool.Parse(grdQuyenHan.GetCellValue(i, "duyet").ToString()) == true ? 1 : 0;
                    row["in"] = bool.Parse(grdQuyenHan.GetCellValue(i, "in").ToString()) == true ? 1 : 0;
                    row["nguoisua"] = MTGlobal.MT_USER_LOGIN;
                    row["ngaysua"] = DateTime.Now;

                    tblNQCN.Rows.Add(row);
                }
            }

            this.saveNhomQuyenChucNang();
        }

        private void fRemoveQuyenHan()
        {
            cmdEdit.Visibility = Visibility.Hidden;
            cmdSave.Visibility = Visibility.Hidden;

            if (tblView.GetSelectedRowHandles() == null)
            {
                Utils.showMessage(Utils.SELECT_ROW_TO_DELETE, "Thông báo");
            }
            var row = grdQuyenHan.GetRow(tblView.GetSelectedRowHandles()[0]);
            if (row == null)
            {
                Utils.showMessage(Utils.SELECT_ROW_TO_DELETE, "Thông báo");
            }
            var id = ((System.Data.DataRowView)(row)).Row.ItemArray[0];
            String sql = String.Format(SQL_DELETE_QH, id);
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
            cmdEdit.Visibility = Visibility.Visible;
            cmdSave.Visibility = Visibility.Visible;
        }

        private void fEditQuyenHan()
        {
            crud.cmdEdit_Click();
            cmdSave.Visibility = Visibility.Visible;
            cmdDelete.Visibility = Visibility.Hidden;
        }

        private void cmdThemNQ_Click(object sender, RoutedEventArgs e)
        {
            fAddNhomQuyen();
        }

        private void cmdThemQH_Click(object sender, RoutedEventArgs e)
        {
            fAddQuyenHan();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            fSaveQuyenHan();
        }

        private void saveNhomQuyenChucNang()
        {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[3];
                arrPara[0] = new SqlParameter("@nhomQuyenChucNangTbl", SqlDbType.Structured);
                arrPara[0].Value = tblNQCN;
                arrPara[1] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 50);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[2] = new SqlParameter("@ketqua", SqlDbType.NVarChar, 255);
                arrPara[2].Direction = ParameterDirection.Output;

                int iRs = MTSQLServer.getMTSQLServer().wExec("spHT_AddNhomQuyenChucNang", arrPara);

                if (iRs == -1)
                {
                    Utils.showMessage("Dữ liệu không thể cập nhật. Vui lòng kiểm tra lại.", "Thông báo");
                    //return;
                }
                else
                {
                    Utils.showMessage("Dữ liệu cập nhật thành công.", "Thông báo");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi kết nối cơ sở dữ liệu", "Thông báo");
                return;
            }
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            fRemoveQuyenHan();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            fEditQuyenHan();
        }

        private void grdQuyenHan_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:
                    fAddNhomQuyen();
                    break;
                case Key.F3:
                    fAddQuyenHan();
                    break;
                case Key.F4:
                    fEditQuyenHan();
                    break;
                case Key.F5:
                    fRemoveQuyenHan();
                    break;
                case Key.F6:
                    fSaveQuyenHan();
                    break;
                case Key.F7:
                    fAbort();
                    break;
            }
        }

        private void cmdAbort_Click(object sender, RoutedEventArgs e)
        {
            fAbort();
        }

        private void fAbort()
        {
            crud.cmdAbort_Click();
            cmdDelete.Visibility = Visibility.Visible;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            Utils.fExit();
        }

        private void tblView_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            e.Source.PostEditor();
        }

    }
}