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
    /// Interaction logic for dlg_AddNhomQuyen.xaml
    /// </summary>
    public partial class dlg_AddNhomQuyen : Window
    {
        public dlg_AddNhomQuyen()
        {
            InitializeComponent();
        }

        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            string manhom = txtMaNhomQuyen.Text.ToString().Trim();
            string tennhom = txtTenNhomQuyen.Text.ToString().Trim();

            if (manhom.Length == 0)
            {
                Utils.showMessage("Mã nhóm không được để trống", "", "Thông báo");
                txtMaNhomQuyen.Focus();
                return;
            }

            if (tennhom.Length == 0)
            {
                Utils.showMessage("Tên nhóm không được để trống", "", "Thông báo");
                txtTenNhomQuyen.Focus();
                return;
            }

            DataTable dtNhomQuyen = new DataTable();

            dtNhomQuyen.Columns.Add("soid", typeof(string));
            dtNhomQuyen.Columns.Add("manhom", typeof(string));
            dtNhomQuyen.Columns.Add("tennhom", typeof(string));
            dtNhomQuyen.Columns.Add("nguoilap", typeof(string));
            dtNhomQuyen.Columns.Add("ngaylap", typeof(DateTime));
            dtNhomQuyen.Columns.Add("nguoisua", typeof(string));
            dtNhomQuyen.Columns.Add("ngaysua", typeof(DateTime));
            dtNhomQuyen.Columns.Add("ngaysync", typeof(DateTime));

            DataRow rw = dtNhomQuyen.NewRow();
            rw["soid"] = MTGlobal.GetNewID();
            rw["manhom"] = manhom;
            rw["tennhom"] = tennhom;
            rw["nguoilap"] = MTGlobal.MT_USER_LOGIN;
            rw["ngaylap"] = DateTime.Now;

            dtNhomQuyen.Rows.Add(rw);

            try
            {
                SqlParameter[] arrPara = new SqlParameter[2];
                arrPara[0] = new SqlParameter("@NhomQuyenTbl", SqlDbType.Structured);
                arrPara[0].Value = dtNhomQuyen;
                arrPara[1] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 50);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;

                int iRs = MTSQLServer.getMTSQLServer().wExec("spHT_AddNhomQuyen", arrPara);

                if (iRs == -1)
                {
                    Utils.showMessage("Dữ liệu không thể cập nhật. Vui lòng kiểm tra lại.", "Thông báo", "MSG");
                    //return;
                }
                else
                {
                    Utils.showMessage("Dữ liệu cập nhật thành công.", "Thông báo", "MSG");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi kết nối cơ sở dữ liệu", "Thông báo", "MSG");
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

        private void txtMaNhomQuyen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtTenNhomQuyen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
