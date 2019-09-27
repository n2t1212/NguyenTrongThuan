using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using MTPMSWIN.Model;
using System.Data.SqlClient;

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for dlg_ViewSanPhamImport.xaml
    /// </summary>
    public partial class dlg_ImportSanPham : Window
    {
        private const string PROVIDER = "Provider=Microsoft.ACE.OLEDB.12.0;";
        private const string EXTENDED_PROPERTIES = "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;';";
        private const string MOI = "MOI";

        private DataTable sanPham;
        private DataTable excelContent;

        public string manhomspid = "";
        private string fileName = "";
        private string loai = "";

        public dlg_ImportSanPham()
        {
            InitializeComponent();
        }

        public dlg_ImportSanPham(string mNhomspid)
        {
            InitializeComponent();
            this.manhomspid = mNhomspid;
        }

        private void loadForm(object sender, RoutedEventArgs e)
        {
            txtFileName.IsReadOnly = true;
            initSanPhamDataTable();
            cmdAccept.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = openFileDlg.FileName;
            }
            txtFileName.Text = fileName;

            if (Utils.validateFileType(fileName, Utils.EXCEL_TYPE))
            {
                cmdAccept.IsEnabled = true;
                excelContent = ReadDataFromExcelFile();
                viewsanPham();
            }
            else
            {
                Utils.showMessage(Utils.ERR_FILE_FORMAT(Utils.EXCEL_EXTENSION_1), "Thông báo");
                txtFileName.Text = "";
                return;
            }
        }

        private void viewsanPham()
        {
            convertExcelContent2sanPhamDataTable();
            grdSanPham.ItemsSource = sanPham;
        }

        private void initSanPhamDataTable()
        {
            sanPham = new DataTable();
            sanPham.Columns.Add("spid", typeof(string));
            sanPham.Columns.Add("manhomspid", typeof(string));
            sanPham.Columns.Add("masp", typeof(string));
            sanPham.Columns.Add("tensp", typeof(string));
            sanPham.Columns.Add("dvt", typeof(string));
            sanPham.Columns.Add("quycach", typeof(string));
            sanPham.Columns.Add("quydoikgl", typeof(double));
            sanPham.Columns.Add("quydoithung", typeof(double));
            sanPham.Columns.Add("nhacungcap", typeof(string));
            sanPham.Columns.Add("mabarcode", typeof(string));
            sanPham.Columns.Add("maqrcode", typeof(string));
            sanPham.Columns.Add("ngaylap", typeof(DateTime));
            sanPham.Columns.Add("nguoilap", typeof(string));
            sanPham.Columns.Add("ngaysua", typeof(DateTime));
            sanPham.Columns.Add("nguoisua", typeof(string));
        }

        private DataTable ReadDataFromExcelFile()
        {
            string connectionString = PROVIDER + "Data Source=" + fileName.Trim() + ";" + EXTENDED_PROPERTIES;
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {
                oledbConn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [SAN_PHAM$]", oledbConn);
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                oleda.Fill(ds);

                data = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Utils.showMessage("Có lỗi trong quá trình xử lý", "Thông báo");
            }
            finally
            {
                // Đóng chuỗi kết nối
                oledbConn.Close();
            }
            return data;
        }

        private DataTable getProductByCode(string code)
        {
            string mSQL = "select * from DM_SANPHAM";
            if (code.Length > 0)
            {
                mSQL += string.Format(" where UPPER(Masp) = '{0}' ", code.ToString().ToUpper());
            }

            DataTable dt = MTSQLServer.getMTSQLServer().wRead(mSQL, null, false);
            return dt;
        }

        private void convertExcelContent2sanPhamDataTable()
        {
            DataRow rw;
            try
            {
                for (int i = 2; i < excelContent.Rows.Count; i++)
                {
                    rw = sanPham.NewRow();
                    if (excelContent.Rows[i][0] == null || excelContent.Rows[i][0].ToString() == "")
                    {
                        break;
                    }
                    string spid = "";
                    string tensp = "";
                    rw["manhomspid"] = manhomspid;
                    string masp = excelContent.Rows[i][0] == DBNull.Value ? "" :Utils.convertToUnsignedString(excelContent.Rows[i][0].ToString());
                    DataTable product = this.getProductByCode(masp);
                    if (product.Rows.Count == 1)
                    {
                        spid = product.Rows[0]["Maspid"] == DBNull.Value ? "" : product.Rows[0]["Maspid"].ToString();
                        tensp = product.Rows[0]["Tensp"] == DBNull.Value ? "" : product.Rows[0]["Tensp"].ToString();
                    }
                    else
                    {
                        spid = MTGlobal.GetNewID();
                        tensp = excelContent.Rows[i][1] == DBNull.Value ? "" : excelContent.Rows[i][1].ToString();
                    }
                    rw["spid"] = spid;
                    rw["masp"] = masp;
                    rw["tensp"] = tensp;
                    rw["dvt"] = excelContent.Rows[i][2] == DBNull.Value ? "" : excelContent.Rows[i][2].ToString();
                    rw["quycach"] = excelContent.Rows[i][3] == DBNull.Value ? "" : excelContent.Rows[i][3].ToString();
                    rw["quydoikgl"] = excelContent.Rows[i][4] == DBNull.Value ? 0 : double.Parse(excelContent.Rows[i][4].ToString());
                    rw["quydoithung"] = excelContent.Rows[i][5] == DBNull.Value ? 1 : double.Parse(excelContent.Rows[i][5].ToString());
                    rw["nhacungcap"] = excelContent.Rows[i][6] == DBNull.Value ? "" : excelContent.Rows[i][6].ToString();
                    rw["ngaylap"] = DateTime.Now;
                    rw["nguoilap"] = MTGlobal.MT_USER_LOGIN;
                    rw["mabarcode"] = "";
                    rw["maqrcode"] = masp;

                    sanPham.Rows.Add(rw);
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Vui lòng kiểm tra nội dung file", "Thông báo");
                return;
            }
        }


        public void SaveSanPham()
        {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0] = new SqlParameter("@tblSanPham", SqlDbType.Structured);
                arrPara[0].Value = sanPham;
                arrPara[1] = new SqlParameter("@Nhomid", SqlDbType.NVarChar, 50);
                arrPara[1].Value = manhomspid;
                arrPara[2] = new SqlParameter("@Loai", SqlDbType.NVarChar, 10);
                arrPara[2].Value = loai;
                arrPara[3] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
                arrPara[3].Value = MTGlobal.MT_USER_LOGIN;
                int iRs = MTSQLServer.getMTSQLServer().wExec("spDM_ImportSanPham", arrPara);

                if (iRs == -1)
                {
                    Utils.showMessage("Dữ liệu không thể cập nhật. Vui lòng kiểm tra lại.", "Thông báo");
                    //return;
                }
                else
                {
                    Utils.showMessage("Dữ liệu cập nhật thành công.", "Thông báo");
                    txtFileName.Text = "";
                    initSanPhamDataTable();
                    grdSanPham.ItemsSource = sanPham;
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi kết nối cơ sở dữ liệu", "Lỗi");
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

        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            if (chbxAddNew.IsChecked == true)
            {
                loai = MOI;
            }
            SaveSanPham();
            cmdAccept.IsEnabled = false;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
