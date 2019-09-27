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
using MTPMSWIN.Model;

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for dlg_ViewBangGiaImport.xaml
    /// </summary>
    public partial class dlg_ImportBanggia : Window
    {
        private const string PROVIDER = "Provider=Microsoft.ACE.OLEDB.12.0;";
        private const string EXTENDED_PROPERTIES = "Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";
        private const string MOI = "MOI";

        private DataTable bangGiaCT;
        private DataTable excelContent;

        public string manhomspid = "";
        private string fileName = "";
        private string loai = "";

        public dlg_ImportBanggia()
        {
            InitializeComponent();
        }

        public dlg_ImportBanggia(string manhomid)
        {
            InitializeComponent();
            this.manhomspid = manhomid;
        }

        private void loadForm(object sender, RoutedEventArgs e)
        {
            txtFileName.IsReadOnly = true;
            initBangGiaCTDataTable();
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
                viewBangGiaCT();
            }
            else
            {
                Utils.showMessage(Utils.ERR_FILE_FORMAT(Utils.EXCEL_EXTENSION_1), "Thông báo");
                txtFileName.Text = "";
                return;
            }
        }

        private void viewBangGiaCT()
        {
            convertExcelContent2BangGiaCTDataTable();
            grdBangGiaCT.ItemsSource = bangGiaCT;
        }

        private void initBangGiaCTDataTable()
        {
            bangGiaCT = new DataTable();
            bangGiaCT.Columns.Add("banggiactid", typeof(string));
            bangGiaCT.Columns.Add("banggiaid", typeof(string));
            bangGiaCT.Columns.Add("mavung", typeof(string));
            bangGiaCT.Columns.Add("spid", typeof(string));
            bangGiaCT.Columns.Add("masp", typeof(string));
            bangGiaCT.Columns.Add("tensp", typeof(string));
            bangGiaCT.Columns.Add("giagoc", typeof(double));
            bangGiaCT.Columns.Add("giasi", typeof(double));
            bangGiaCT.Columns.Add("giale", typeof(double));
            bangGiaCT.Columns.Add("ghichu", typeof(string));

        }

        private DataTable ReadDataFromExcelFile()
        {
            string connectionString = PROVIDER + "Data Source=" + fileName.Trim() + ";" + EXTENDED_PROPERTIES;
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {
                oledbConn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [GIA_SP$]", oledbConn);
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

        private void convertExcelContent2BangGiaCTDataTable()
        {
            try
            {
                DataRow rw;
                for (int i = 2; i < excelContent.Rows.Count; i++)
                {
                    rw = bangGiaCT.NewRow();
                    if (excelContent.Rows[i][0] == null || excelContent.Rows[i][0].ToString() == "")
                    {
                        break;
                    }
                    rw["banggiactid"] = MTGlobal.GetNewID();
                    rw["banggiaid"] = manhomspid;
                    rw["mavung"] = excelContent.Rows[i][0] == DBNull.Value ? "" : excelContent.Rows[i][0].ToString();
                    string masp = excelContent.Rows[i][1] == DBNull.Value ? "" : Utils.convertToUnsignedString(excelContent.Rows[i][1].ToString());
                    DataTable product = this.getProductByCode(masp);
                    if (product.Rows.Count < 1)
                    {
                        continue;
                    }
                    string spid = product.Rows[0]["Maspid"] == DBNull.Value ? "" : product.Rows[0]["Maspid"].ToString();
                    rw["spid"] = spid;
                    rw["masp"] = masp;
                    rw["tensp"] = product.Rows[0]["Tensp"] == DBNull.Value ? "" : product.Rows[0]["Tensp"].ToString();
                    rw["giagoc"] = excelContent.Rows[i][2] == DBNull.Value ? 0 : double.Parse(excelContent.Rows[i][2].ToString());
                    rw["giasi"] = excelContent.Rows[i][3] == DBNull.Value ? 0 : double.Parse(excelContent.Rows[i][3].ToString());
                    rw["giale"] = excelContent.Rows[i][4] == DBNull.Value ? 0 : double.Parse(excelContent.Rows[i][4].ToString());
                    rw["ghichu"] = excelContent.Rows[i][5] == DBNull.Value ? "" : excelContent.Rows[i][5].ToString();

                    bangGiaCT.Rows.Add(rw);
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Vui lòng kiểm tra nội dung file", "Thông báo");
                return;
            }
        }


        public void SaveBangGia()
        {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0] = new SqlParameter("@tblBanggiaCT", SqlDbType.Structured);
                arrPara[0].Value = bangGiaCT;
                arrPara[1] = new SqlParameter("@Banggiaid", SqlDbType.NVarChar, 50);
                arrPara[1].Value = manhomspid;
                arrPara[2] = new SqlParameter("@Loai", SqlDbType.NVarChar, 10);
                arrPara[2].Value = loai;
                arrPara[3] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
                arrPara[3].Value = MTGlobal.MT_USER_LOGIN;
                int iRs = MTSQLServer.getMTSQLServer().wExec("spDM_ImportBanggia", arrPara);

                if (iRs == -1)
                {
                    Utils.showMessage("Dữ liệu không thể cập nhật. Vui lòng kiểm tra lại.", "Thông báo");
                    //return;
                }
                else
                {
                    Utils.showMessage("Dữ liệu cập nhật thành công.", "Thông báo");
                    txtFileName.Text = "";
                    initBangGiaCTDataTable();
                    grdBangGiaCT.ItemsSource = bangGiaCT;
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi kết nối cơ sở dữ liệu", "Lỗi", "ERR");
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
            SaveBangGia();
            cmdAccept.IsEnabled = false;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
