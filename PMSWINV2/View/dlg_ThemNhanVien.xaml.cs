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
    /// <summary>
    /// Interaction logic for dlg_ThemNhanVien.xaml
    /// </summary>
    public partial class dlg_ThemNhanVien : Window
    {
        private string maBoPhan = "";
        private string tenBoPhan = "";
        private DataTable nhanVienDT;
        public dlg_ThemNhanVien()
        {
            InitializeComponent();
        }

        public dlg_ThemNhanVien(string mabophan, string tenbophan)
        {
            InitializeComponent();
            this.maBoPhan = mabophan;
            this.tenBoPhan = tenbophan;
            lblBoPhan.Content = tenbophan;
            txtMaNV.Focus();
        }

        private void createNhanVienDatatable()
        {
            nhanVienDT = new DataTable();
            nhanVienDT.Columns.Add("Manvid", typeof(string));
            nhanVienDT.Columns.Add("Mabp", typeof(string));
            nhanVienDT.Columns.Add("Macv", typeof(string));
            nhanVienDT.Columns.Add("Manv", typeof(string));
            nhanVienDT.Columns.Add("Tennv", typeof(string));
            nhanVienDT.Columns.Add("Ngaysinh", typeof(DateTime));
            nhanVienDT.Columns.Add("Diachi", typeof(string));
            nhanVienDT.Columns.Add("Dienthoai", typeof(string));
            nhanVienDT.Columns.Add("Email", typeof(string));
            nhanVienDT.Columns.Add("Ngaythuviec", typeof(DateTime));
            nhanVienDT.Columns.Add("Ngaychinhthuc", typeof(DateTime));
            nhanVienDT.Columns.Add("Ngaylap", typeof(DateTime));
            nhanVienDT.Columns.Add("Nguoilap", typeof(string));
            nhanVienDT.Columns.Add("Ngaysua", typeof(DateTime));
            nhanVienDT.Columns.Add("Nguoisua", typeof(string));
        }

        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            string manv = txtMaNV.Text.ToString();
            string tennv = txtHoTen.Text.ToString();
            string sdt = txtSDT.Text.ToString();
            string diachi = txtDiaChi.Text.ToString();
            string ngaysinh = txtNgaySinh.Text.ToString();
            string email = txtEmail.Text.ToString();
            string ngayThuViec = txtNgayThuViec.Text.ToString();
            string ngayChinhThuc = txtNgayChinhThuc.Text.ToString();

            string err = ValidateHelper.validateFieldNotEmpty("Mã nhân viên", manv);

            if (err.Length == 0)
            {
                err = ValidateHelper.validateFieldNotEmpty("Họ tên", tennv);

                if (err.Length == 0)
                {
                    err = ValidateHelper.validateFieldIsNumber("Số điện thoại", sdt);

                    if(err.Length > 0)
                    {
                        Utils.showMessage(err, "Thông báo");
                        txtSDT.Focus();
                        return;
                    }
                }
                else
                {
                    Utils.showMessage(err, "Thông báo");
                    txtHoTen.Focus();
                    return;
                }
            } 
            else
            {
                Utils.showMessage(err, "Thông báo");
                txtMaNV.Focus();
                return;
            }

            createNhanVienDatatable();
            DataRow rw = nhanVienDT.NewRow();
            rw["Manvid"] = MTGlobal.GetNewID();
            rw["Mabp"] = maBoPhan;
            rw["Macv"] = "";
            rw["Manv"] = Utils.convertToUnsignedString(manv);
            rw["Tennv"] = tennv;
            rw["Ngaysinh"] = DateTime.Parse(ngaysinh.ToString());
            rw["Diachi"] = diachi;
            rw["Dienthoai"] = sdt;
            rw["Email"] = email;
            rw["Ngaythuviec"] = DateTime.Parse(ngayThuViec.ToString());
            rw["Ngaychinhthuc"] = DateTime.Parse(ngayChinhThuc.ToString());
            rw["Ngaylap"] = DateTime.Now;
            rw["Nguoilap"] = MTGlobal.MT_USER_LOGIN;
            rw["Nguoisua"] = "";

            nhanVienDT.Rows.Add(rw);

            saveNhanVien();
            this.Close();
        }

        private void saveNhanVien()
        {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[3];
                arrPara[0] = new SqlParameter("@nhanvien", SqlDbType.Structured);
                arrPara[0].Value = nhanVienDT;
                arrPara[1] = new SqlParameter("@mabophan", SqlDbType.NVarChar, 50);
                arrPara[1].Value = maBoPhan;
                arrPara[2] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 10);
                arrPara[2].Value = MTGlobal.MT_USER_LOGIN;
                int iRs = MTSQLServer.getMTSQLServer().wExec("spDM_AddNhanVien", arrPara);

                if (iRs == -1)
                {
                    Utils.showMessage("Dữ liệu không thể cập nhật. Vui lòng kiểm tra lại.", "Thông báo");
                    //return;
                }
                else
                {
                    Utils.showMessage("Dữ liệu cập nhật thành công.", "Thông báo");
                    
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

        private void maNhanVien_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void hoTen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void sdt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void diaChi_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void email_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void ngaySinh_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void ngayThuVien_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
