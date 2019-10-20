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
    public partial class dlg_AddKhachHang : Window
    {

        public string maKH = "";
        public string tenKH = "";
        public string DiaChi = "";

        private DataTable KhachHangDT;

        private void createKhachHangDatatable()
        {
            KhachHangDT = new DataTable();
            KhachHangDT.Columns.Add("Makhid", typeof(String));
            KhachHangDT.Columns.Add("Makh", typeof(String));
            KhachHangDT.Columns.Add("Maloai", typeof(String));
            KhachHangDT.Columns.Add("Tenkh", typeof(String));
            KhachHangDT.Columns.Add("Dienthoai", typeof(String));
            KhachHangDT.Columns.Add("Diachi", typeof(String));
            KhachHangDT.Columns.Add("Xa", typeof(String));
            KhachHangDT.Columns.Add("Huyen", typeof(String));
            KhachHangDT.Columns.Add("Tinh", typeof(String));
            KhachHangDT.Columns.Add("Email", typeof(String));
            KhachHangDT.Columns.Add("Mst", typeof(String));
            KhachHangDT.Columns.Add("Sinhnhat", typeof(DateTime));
            KhachHangDT.Columns.Add("Mathe", typeof(String));
            KhachHangDT.Columns.Add("Ngaycap", typeof(DateTime));
            KhachHangDT.Columns.Add("Ngayhethan", typeof(DateTime));
            KhachHangDT.Columns.Add("Nguon", typeof(String));
            KhachHangDT.Columns.Add("Ghichu", typeof(String));
        }

        public dlg_AddKhachHang()
        {
            InitializeComponent();
        }

        public void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            string maKh = txtMakh.Text.ToString().Trim().ToUpper();
            string maLoai = txtMaloai.Text.ToString().Trim().ToUpper();
            string tenKh = txtTenkh.Text.ToString().Trim();
            string soDt = txtSodt.Text.ToString();
            string diaChi = txtDiachi.Text.ToString();
            string email = txtEmail.Text.ToString();

            string err = ValidateHelper.validateFieldNotEmpty("Mã khách hàng", maKh);
            if (err.Length == 0)
            {
                string sql = string.Format("select * from DM_KHACHHANG where Makh='{0}'", maKh);

                DataTable KH = new MTSQLServer().wRead(sql, null, false);
                if (KH.Rows.Count > 0)
                {
                    err = "Mã khách hàng đã tồn tại";
                    Utils.showMessage(err, "Thông báo");
                    txtMakh.Focus();
                    return;
                }
                else
                {
                    err = ValidateHelper.validateFieldNotEmpty("Họ tên", tenKH);

                    if (err.Length == 0)
                    {
                        err = ValidateHelper.validateFieldIsNumber("Số điện thoại", soDt);

                        if (err.Length > 0)
                        {
                            Utils.showMessage(err, "Thông báo");
                            txtSodt.Focus();
                            return;
                        }
                        else
                        {
                            Utils.showMessage(err, "Thông báo");
                            txtTenkh.Focus();
                            return;
                        }
                    }
                }
            }  
            else
            {
                Utils.showMessage(err, "Thông báo");
                txtMakh.Focus();
                return;
            }

            createKhachHangDatatable();
            DataRow rw = KhachHangDT.NewRow();
            rw["Makhid"] = MTGlobal.GetNewID();
            rw["Makh"] = maKh;
            rw["Maloai"] = maLoai;
            rw["Tenkh"] = tenKh;
            rw["Dienthoai"] = soDt;
            rw["Diachi"] = diaChi;
            rw["Email"] = txtEmail.Text;
            rw["Mst"] = txtMST.Text;

            if(txtSinhnhat.Text.Trim().Length > 0)
            {
                rw["Sinhnhat"] = DateTime.Parse(txtSinhnhat.Text.ToString());
            }
            
            rw["Mathe"] = txtMathe.Text;

            if(txtNgaycap.Text.Trim().Length > 0)
            {
                rw["Ngaycap"] = DateTime.Parse(txtNgaycap.Text.ToString());
            }

            if(txtNgayhethan.Text.Trim().Length > 0)
            {
                rw["Ngayhethan"] = DateTime.Parse(txtNgayhethan.Text.ToString());
            }
            rw["Nguon"] = "";
            rw["Ghichu"] = txtGhichu.Text;

            KhachHangDT.Rows.Add(rw);

            if (saveKhachHang())
            {
                maKH = Utils.convertToUnsignedString(maKh).ToUpper();
                tenKH = tenKh;
                DiaChi = diaChi;
            }
            this.Close();
        }

        private bool saveKhachHang()
        {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[2];
                arrPara[0] = new SqlParameter("@khachhang", SqlDbType.Structured);
                arrPara[0].Value = KhachHangDT;
                arrPara[1] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 10);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                int iRs = MTSQLServer.getMTSQLServer().wExec("spDM_AddKhachHang", arrPara);

                if (iRs == -1)
                {
                    Utils.showMessage("Dữ liệu không thể cập nhật. Vui lòng kiểm tra lại.", "Thông báo");
                    return false;
                }
                else
                {
                    Utils.showMessage("Dữ liệu cập nhật thành công.", "Thông báo");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi kết nối cơ sở dữ liệu", "Thông báo");
                return false;
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

        private void txtMakh_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtMaloai_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtMaloai.Text != "" && txtMaloai.Text.Length > 10)
                {
                    txtMaloai.Text = "";
                    txtMaloai.Focus();
                    return;
                }
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtTenkh_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtSodt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtDiachi_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtEmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtMST_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtSinhnhat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtMathe_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtNgaycap_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtNgayhethan_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtGhichu_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
