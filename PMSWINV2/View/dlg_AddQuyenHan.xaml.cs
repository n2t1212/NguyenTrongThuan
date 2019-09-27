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
using System.Data.SqlClient;
using System.Data;

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for dlg_AddQuyenHan.xaml
    /// </summary>
    public partial class dlg_AddQuyenHan : Window
    {
        SqlDataAdapter SQLAdaptor = null;
        DataSet DSetMain = null;
        private string nhomQuyenId;
        private DataTable tblSrc = null;
        private DataTable tblNQCN = null;
        private DataTable tblSelected = null;

        public dlg_AddQuyenHan()
        {
            InitializeComponent();
        }

        public dlg_AddQuyenHan(string nhomQuyenId)
        {
            InitializeComponent();
            this.nhomQuyenId = nhomQuyenId;
            initGridChucNang();
            InitTableNhomQuyenChucNang();
            InitTableSelected();

            MTGlobal.SetFormatGridControl(grdChucNang, tblChucNang);
            MTGlobal.SetFormatGridControl(grdNhomQuyenChucNang, tblNhomQuyenChucNang);
            tblChucNang.MultiSelectMode = DevExpress.Xpf.Grid.TableViewSelectMode.Row;
            tblNhomQuyenChucNang.MultiSelectMode = DevExpress.Xpf.Grid.TableViewSelectMode.Row;
            MTGlobal.SetGridReadOnly(grdNhomQuyenChucNang, tblNhomQuyenChucNang, false);
        }

        private void InitTableSelected()
        {
            tblSelected = new DataTable();
            tblSelected.Columns.Add("macn", typeof(string));
            tblSelected.Columns.Add("tenchucnang", typeof(string));
            tblSelected.Columns.Add("them", typeof(int));
            tblSelected.Columns.Add("sua", typeof(int));
            tblSelected.Columns.Add("xoa", typeof(int));
            tblSelected.Columns.Add("duyet", typeof(int));
            tblSelected.Columns.Add("in", typeof(int));
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

        private void convertTableSelectedToTableNQCN()
        {
            int loop = 0;
            foreach (DataRow rw in tblSelected.Rows)
            {
                DataRow row = tblNQCN.NewRow();
                row["soid"] = MTGlobal.GetNewID() + loop;
                row["manhom"] = nhomQuyenId;
                row["macn"] = rw["macn"];
                row["them"] = rw["them"];
                row["sua"] = rw["sua"];
                row["xoa"] = rw["xoa"];
                row["duyet"] = rw["duyet"];
                row["in"] = rw["in"];
                row["nguoilap"] = MTGlobal.MT_USER_LOGIN;
                row["ngaylap"] = DateTime.Now;

                tblNQCN.Rows.Add(row);
                loop++;
            }
        }

        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            if (tblSelected.Rows.Count == 0)
            {
                Utils.showMessage("Vui lòng chọn chức năng", "Thông báo", "MSG");
                return;
            }

            this.convertTableSelectedToTableNQCN();

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

        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void initGridChucNang()
        {
            string sql = string.Format("select cn.* from DM_CHUCNANG cn where cn.macn not in (select nqcn.macn from HT_NHOMQUYEN_CHUCNANG nqcn where nqcn.manhom = '{0}')", this.nhomQuyenId);
            SQLAdaptor = MTSQLServer.getMTSQLServer().wAdapter(sql, null, false);
            DSetMain = new DataSet();
            SQLAdaptor.Fill(DSetMain, "DM_CHUCNANG");
            tblSrc = DSetMain.Tables["DM_CHUCNANG"];
            grdChucNang.ItemsSource = tblSrc;
            
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < grdChucNang.VisibleRowCount; i++)
                {
                    if(tblChucNang.IsRowSelected(i))
                    {
                        DataRow rw = tblSelected.NewRow();
                        rw["macn"] = grdChucNang.GetCellValue(i, "macn");
                        rw["tenchucnang"] = grdChucNang.GetCellValue(i, "chucnang");
                        rw["them"] = 1;
                        rw["sua"] = 1;
                        rw["xoa"] = 1;
                        rw["duyet"] = 1;
                        rw["in"] = 1;

                        tblSelected.Rows.Add(rw);
                        tblChucNang.DeleteRow(i);
                    }
                }

                if (grdChucNang.VisibleRowCount > 0)
                {
                    tblChucNang.SelectRow(0);
                }

                tblSelected.AcceptChanges();
                grdNhomQuyenChucNang.ItemsSource = tblSelected;
                tblSrc.AcceptChanges();
                grdChucNang.RefreshData();
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdAddAll_Click(object sender, RoutedEventArgs e)
        {
            if (tblSrc != null && tblSrc.Rows.Count > 0)
            {
                foreach(DataRow row in tblSrc.Rows)
                {
                    DataRow rw = tblSelected.NewRow();
                    rw["macn"] = row["macn"];
                    rw["tenchucnang"] = row["chucnang"];
                    rw["them"] = 1;
                    rw["sua"] = 1;
                    rw["xoa"] = 1;
                    rw["duyet"] = 1;
                    rw["in"] = 1;

                    tblSelected.Rows.Add(rw);
                }

                tblSelected.AcceptChanges();
                tblSrc.Rows.Clear();
                grdChucNang.ItemsSource = tblSrc;
                grdNhomQuyenChucNang.ItemsSource = tblSelected;
            }
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < grdNhomQuyenChucNang.VisibleRowCount; i++)
                {
                    if (tblNhomQuyenChucNang.IsRowSelected(i))
                    {
                        DataRow rw = tblSrc.NewRow();
                        rw["macn"] = grdNhomQuyenChucNang.GetCellValue(i, "macn");
                        rw["chucnang"] = grdNhomQuyenChucNang.GetCellValue(i, "tenchucnang");
                        

                        tblSrc.Rows.Add(rw);
                        tblNhomQuyenChucNang.DeleteRow(i);
                    }
                }

                if (grdNhomQuyenChucNang.VisibleRowCount > 0)
                {
                    tblNhomQuyenChucNang.SelectRow(0);
                }

                tblSrc.AcceptChanges();
                grdChucNang.ItemsSource = tblSrc;
                tblSelected.AcceptChanges();
                grdNhomQuyenChucNang.RefreshData();
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            if (tblSelected != null && tblSelected.Rows.Count > 0)
            {
                foreach (DataRow row in tblSelected.Rows)
                {
                    DataRow rw = tblSrc.NewRow();
                    rw["macn"] = row["macn"];
                    rw["chucnang"] = row["tenchucnang"];


                    tblSrc.Rows.Add(rw);
                }

                tblSrc.AcceptChanges();
                tblSelected.Rows.Clear();
                grdChucNang.ItemsSource = tblSrc;
                grdNhomQuyenChucNang.ItemsSource = tblSelected;
            }
        }

        
    }
}
