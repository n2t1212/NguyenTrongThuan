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
    /// Interaction logic for dlg_ThemNguoiDung.xaml
    /// </summary>
    public partial class dlg_NguoiDung : Window
    {
        private string maBoPhan = "";
        private string maNhomQuyen;
        private DataTable nguoiDungDT;
        List<DM_NhanVien> dsNhanVien;
        List<HT_NhomQuyen> dsNhomQuyen;
        private string id = "";
        private string oldPassword = "";

        private bool isEditPassword = false;

        SqlDataAdapter SQLAdaptor = null;
        DataSet DSetMain = null;

        public dlg_NguoiDung()
        {
            InitializeComponent();
        }

        public dlg_NguoiDung(string mabophan, string id)
        {
            InitializeComponent();
            this.maBoPhan = mabophan;
            initComboboxNhomQuyen();
            
            if (id != "")
            {
                lblTitle.Content = "CHỈNH SỬA NGƯỜI DÙNG";
                this.id = id;
                initComboboxNhanVien(true);
                initInfoToEdit(this.id);
                cbxNhanVien.IsReadOnly = true;
            }
            else
            {
                lblTitle.Content = "THÊM NGƯỜI DÙNG";
                initComboboxNhanVien(false);
            }
            
            cbxNhanVien.Focus();
        }

        private DataTable getNhomQuyenByIdUser(string idUser)
        {
            string sql = string.Format("select nq.* from HT_NHOMQUYEN nq left join HT_QUYENHAN qh on nq.soid = qh.soid_nhomquyen where qh.soid_nguoidung = '{0}'", id);
            SQLAdaptor = MTSQLServer.getMTSQLServer().wAdapter(sql, null, false);
            DSetMain = new DataSet();
            SQLAdaptor.Fill(DSetMain, "HT_NHOMQUYEN");
            DataTable dt = DSetMain.Tables["HT_NHOMQUYEN"];
            return dt;
        }

        private void initInfoToEdit(string id)
        {
            string sql = string.Format("select * from HT_NGUOIDUNG where soid = '{0}'", id);
            SQLAdaptor = MTSQLServer.getMTSQLServer().wAdapter(sql, null, false);
            DSetMain = new DataSet();
            SQLAdaptor.Fill(DSetMain, "HT_NGUOIDUNG");
            DataTable dt = DSetMain.Tables["HT_NGUOIDUNG"];
            int rowNumber = dt.Rows.Count;
            if (rowNumber == 1)
            {
                string manv = dt.Rows[0].ItemArray[1].ToString();
                string tk = dt.Rows[0].ItemArray[2].ToString();
                string mk = dt.Rows[0].ItemArray[3].ToString();
                oldPassword = mk;
                string isSync = dt.Rows[0][10].ToString();
                string kyhieu = dt.Rows[0].ItemArray[11].ToString();

                for(int i = 0; i < dsNhanVien.Count; i++)
                {
                    if(dsNhanVien[i].Manv.ToLower() == manv.ToLower())
                    {
                        cbxNhanVien.SelectedItem = dsNhanVien[i];
                        break;
                    }
                }

                txtUsername.Text = tk;
                txtPassword.Text = mk;
                txtRepeatPassword.Text = mk;
                txtKyHieu.Text = kyhieu;
                if (isSync.ToLower() == "true")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }

                DataTable nhomQuyenDtb = this.getNhomQuyenByIdUser(id);

                int size = nhomQuyenDtb.Rows.Count;

                if (size == 1)
                {
                    string maNhom = nhomQuyenDtb.Rows[0][1].ToString();
                    for (int i = 0; i < dsNhomQuyen.Count; i++)
                    {
                        if (dsNhomQuyen[i].manhom == maNhom)
                        {
                            cbxNhomQuyen.SelectedItem = dsNhomQuyen[i];
                            break;
                        }
                    }
                }
                
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

        private void loadForm(object sender, RoutedEventArgs e)
        {
            
        }

        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            if (cbxNhanVien.SelectedItem == null)
            {
                Utils.showMessage("Vui lòng chọn nhân viên", "Thông báo");
                cbxNhanVien.Focus();
                return;
            }

            string manv = ((MTPMSWIN.View.dlg_NguoiDung.DM_NhanVien)(cbxNhanVien.SelectedItemValue)).Manv;
            string hoten = ((MTPMSWIN.View.dlg_NguoiDung.DM_NhanVien)(cbxNhanVien.SelectedItemValue)).Tennv;
            string taikhoan = txtUsername.Text.ToString().Trim();
            string matkhau = txtPassword.Text.ToString().Trim();
            string repeatMatKhau = txtRepeatPassword.Text.ToString().Trim();
            string kyhieu = Utils.convertToUnsignedString(txtKyHieu.Text);

            if (taikhoan == "")
            {
                Utils.showMessage("Tài khoản không được để trống", "Thông báo");
                txtUsername.Focus();
                return;
            }

            if (matkhau == "")
            {
                Utils.showMessage("Mật khẩu không được để trống", "Thông báo");
                txtPassword.Focus();
                return;
            }

            if (matkhau != repeatMatKhau)
            {
                Utils.showMessage("Nhập lại mật khẩu không đúng", "Thông báo");
                txtRepeatPassword.Focus();
                return;
            }

            if (cbxNhomQuyen.SelectedItem == null)
            {
                Utils.showMessage("Vui lòng chọn nhóm chức năng", "Thông báo");
                txtRepeatPassword.Focus();
                return;
            }

            maNhomQuyen = ((MTPMSWIN.View.dlg_NguoiDung.HT_NhomQuyen)(cbxNhomQuyen.SelectedItemValue)).manhom;

            createNguoiDungDataTable();

            DataRow rw = nguoiDungDT.NewRow();
            if (this.id.Length == 0)
            {
                this.id = MTGlobal.GetNewID();
            }
            rw["soid"] = this.id;
            rw["manv"] = manv;
            rw["taikhoan"] = Utils.convertToUnsignedString(taikhoan);
            if (isEditPassword)
            {
                rw["matkhau"] = MTGlobal.HashMD5(matkhau);
            }
            else
            {
                rw["matkhau"] = matkhau;
            }
            
            rw["hoten"] = hoten;
            rw["nguoilap"] = MTGlobal.MT_USER_LOGIN;
            rw["ngaylap"] = DateTime.Now;
            rw["isync"] = chkActive.IsChecked == true ? 1: 0;
            rw["kyhieu"] = kyhieu;
            nguoiDungDT.Rows.Add(rw);

            SaveNguoiDung();
            this.Close();
        }

        private void initComboboxNhanVien(bool isEdit)
        {
            string sql = "select * from DM_NHANVIEN where Mabp = '{0}' ";
            if (!isEdit)
            {
                sql += "and Manv not in (select manv from HT_NGUOIDUNG) ";
            }

            sql += "order by Manv";

            string sqlGetNV = string.Format(sql, maBoPhan);

            SQLAdaptor = MTSQLServer.getMTSQLServer().wAdapter(sqlGetNV, null, false);
            DSetMain = new DataSet();
            SQLAdaptor.Fill(DSetMain, "DM_NHANVIEN");
            DataTable dt = DSetMain.Tables["DM_NHANVIEN"];
            int rowNumber = dt.Rows.Count;
            dsNhanVien = new List<DM_NhanVien>();
            if (rowNumber > 0)
            {
                for (int i = 0; i < rowNumber; i++)
                {
                    string Manv = dt.Rows[i]["Manv"].ToString();
                    string Tennv = dt.Rows[i]["Tennv"].ToString();

                    dsNhanVien.Add(new DM_NhanVien() { Manv = Manv, Tennv = Tennv});
                }
            }

            cbxNhanVien.ItemsSource = dsNhanVien;

        }

        private void initComboboxNhomQuyen()
        {
            string sql = string.Format("select * from HT_NHOMQUYEN");
            SQLAdaptor = MTSQLServer.getMTSQLServer().wAdapter(sql, null, false);
            DSetMain = new DataSet();
            SQLAdaptor.Fill(DSetMain, "HT_NHOMQUYEN");
            DataTable dt = DSetMain.Tables["HT_NHOMQUYEN"];
            int rowNumber = dt.Rows.Count;

            dsNhomQuyen = new List<HT_NhomQuyen>();

            if (rowNumber > 0)
            {
                for (int i = 0; i < rowNumber; i++)
                {
                    string maNhom = dt.Rows[i]["manhom"].ToString();
                    string tenNhom = dt.Rows[i]["tennhom"].ToString();

                    dsNhomQuyen.Add(new HT_NhomQuyen() { manhom = maNhom, tennhom = tenNhom });
                }
            }

            cbxNhomQuyen.ItemsSource = dsNhomQuyen;

        }

        class DM_NhanVien
        {
            public string Manv { get; set; }
            public string Tennv { get; set; }
        }

        class HT_NhomQuyen
        {
            public string manhom { get; set; }
            public string tennhom { get; set; }
        }

        private void createNguoiDungDataTable()
        {
            nguoiDungDT = new DataTable();
            nguoiDungDT.Columns.Add("soid", typeof(string));
            nguoiDungDT.Columns.Add("manv", typeof(string));
            nguoiDungDT.Columns.Add("taikhoan", typeof(string));
            nguoiDungDT.Columns.Add("matkhau", typeof(string));
            nguoiDungDT.Columns.Add("hoten", typeof(string));
            nguoiDungDT.Columns.Add("nguoilap", typeof(string));
            nguoiDungDT.Columns.Add("ngaylap", typeof(DateTime));
            nguoiDungDT.Columns.Add("nguoisua", typeof(string));
            nguoiDungDT.Columns.Add("ngaysua", typeof(DateTime));
            nguoiDungDT.Columns.Add("ngaysync", typeof(DateTime));
            nguoiDungDT.Columns.Add("isync", typeof(int));
            nguoiDungDT.Columns.Add("kyhieu",typeof(string));
        }

        public void SaveNguoiDung()
        {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[3];
                arrPara[0] = new SqlParameter("@nguoidungDT", SqlDbType.Structured);
                arrPara[0].Value = nguoiDungDT;
                arrPara[1] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 50);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[2] = new SqlParameter("@maNhomQuyen", SqlDbType.NVarChar, 50);
                arrPara[2].Value = maNhomQuyen;

                int iRs = MTSQLServer.getMTSQLServer().wExec("spHT_AddNguoiDung", arrPara);

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

        private void nhanVien_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void matKhau_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }

        }

        private void taikhoan_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void nhapLaiMK_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtPassword_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if ((e.NewValue.ToString().Trim() != oldPassword && oldPassword != "") || oldPassword == "")
            {
                isEditPassword = true;
            }
        }
    }
}
