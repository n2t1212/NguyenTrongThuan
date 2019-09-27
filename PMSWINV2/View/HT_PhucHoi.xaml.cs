using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for HT_PhucHoi.xaml
    /// </summary>
    public partial class HT_PhucHoi : Window
    {
        private string restoreFile = "";
        private SqlConnection testSQLConnection;

        private string SERVER = "";
        private string USERNAME = "";
        private string PASSWORD = "";
        private string DBNAME = "";

        public HT_PhucHoi()
        {
            InitializeComponent();
            btnRestore.IsEnabled = false;
            initServerCombobox();
        }

        private void formLoad(object sender, RoutedEventArgs e)
        {
            //initBackupInfo();
        }

        private void btnSelectPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                restoreFile = ofd.FileName;
                txtPathRestore.Text = restoreFile;
                btnRestore.IsEnabled = true;
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (txtDatabase.Text.ToString() == null)
            {
                Utils.showMessage("Vui lòng chọn CSDL cần phục hồi", "Thông báo");
                txtPathRestore.Text = "";
                btnRestore.IsEnabled = false;
                txtDatabase.Focus();
                return;
            }

            DBNAME = txtDatabase.Text.ToString().Trim();

            if (Utils.isDatabaseExisted(DBNAME))
            {
                Utils.showMessage("CSDL đã tồn tại. Vui lòng chọn tên CSDL khác.","Thông báo");
                txtPathRestore.Text = "";
                btnRestore.IsEnabled = false;
                txtDatabase.Focus();
                return;
            }

            if (!Utils.validateFileType(restoreFile, Utils.BACKUP_SQL_TYPE))
            {
                Utils.showMessage(Utils.ERR_FILE_FORMAT(Utils.BACKUP_SQL_EXTENSION), "Thông báo");
                txtPathRestore.Text = "";
                btnRestore.IsEnabled = false;
                return;
            }

            try {
                if (MTGlobal.MT_Cnn.State == ConnectionState.Closed)
                {
                    MTGlobal.MT_Cnn.Open();
                }

                btnRestore.IsEnabled = false;

                string testConnectionStr = string.Format(@"Data Source={0};Persist Security Info=True;User ID={1};Password={2};Max Pool Size=2000", SERVER, USERNAME, PASSWORD);

                if (!isConnection(testConnectionStr))
                {
                    Utils.showMessage("Tài khoản hoặc mật khẩu không đúng. Vui lòng kiểm tra lại", "Thông báo");
                    return;
                }

                if (Utils.createAndRestoreDB(SERVER, USERNAME, PASSWORD, DBNAME, restoreFile))
                {
                    Utils.showMessage("Phục hồi dữ liệu thành công", "Thông báo");
                }
                else
                {
                    Utils.showMessage("Không thể phục hồi dữ liệu. Vui lòng kiểm tra lại", "Thông báo");
                }
                
            }
            catch (Exception ex)
            {
                Utils.showMessage("Có lỗi trong quá trình phục hồi dữ liệu", "Thông báo");
            }
        }

        private void initBackupInfo()
        {
            txtUsername.Text = MTGlobal.MT_SQL_USER;
            txtPassword.Text = MTGlobal.MT_SQL_PASS;
        }

        private void btnTestConnect_Click(object sender, RoutedEventArgs e)
        {
            if (((MTPMSWIN.View.HT_PhucHoi.ServerSQL)(cbcServer.SelectedItem)) == null)
            {
                Utils.showMessage("Vui lòng chọn server", "Thông báo");
                cbcServer.Focus();
                return;
            }
            SERVER = ((MTPMSWIN.View.HT_PhucHoi.ServerSQL)(cbcServer.SelectedItem)).name;
            USERNAME = txtUsername.Text.ToString().Trim();
            PASSWORD = txtPassword.Text.ToString().Trim();

            if (USERNAME.Length == 0)
            {
                Utils.showMessage("Username không được bỏ trống", "Thông báo");
                return;
            }

            if (PASSWORD.Length == 0)
            {
                Utils.showMessage("Password không được bỏ trống", "Thông báo");
                return;
            }

            //string testConnectionStr = string.Format(@"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Max Pool Size=2000", SERVER, MTGlobal.MT_DBNAME, USERNAME, PASSWORD);
            string testConnectionStr = string.Format(@"Data Source={0};Persist Security Info=True;User ID={1};Password={2};Max Pool Size=2000", SERVER, USERNAME, PASSWORD);

            if (isConnection(testConnectionStr))
            {
                showRestoreFunction();
                btnTestConnect.Visibility = Visibility.Hidden;
                Utils.showMessage("Kết nối thành công", "Thông báo");
                return;
            }

            txtDatabase.Focus();
        }

        private bool isConnection(string connectionStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionStr))
                {
                    conn.Open(); // throws if invalid
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Không thể kết nối server. Vui lòng kiểm tra lại thông tin", "Thông báo");
                return false;
            }
        }

        

        private void initServerCombobox()
        {
            List<string> lstServer  = this.ListLocalSqlInstances().ToList();
            int count = lstServer.Count;
            List<ServerSQL> dsServer = new List<ServerSQL>();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    dsServer.Add(new ServerSQL() { name = lstServer[i] });
                }
            }

            cbcServer.ItemsSource = dsServer;
        }

        public class DatabaseSystem
        {
            public string name { get; set; }
        }

        public class ServerSQL
        {
            public string name { get; set; }
        }

        private void showRestoreFunction()
        {
            lblDataBase.Visibility = Visibility.Visible;
            txtDatabase.Visibility = Visibility.Visible;
            lblChonFile.Visibility = Visibility.Visible;
            txtPathRestore.Visibility = Visibility.Visible;
            btnSelectPath.Visibility = Visibility.Visible;
            btnRestore.Visibility = Visibility.Visible;
        }

        private IEnumerable<string> ListLocalSqlInstances()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    foreach (string item in ListLocalSqlInstances(hive))
                    {
                        yield return item;
                    }
                }

                using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                    foreach (string item in ListLocalSqlInstances(hive))
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                foreach (string item in ListLocalSqlInstances(Registry.LocalMachine))
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<string> ListLocalSqlInstances(RegistryKey hive)
        {
            const string keyName = @"Software\Microsoft\Microsoft SQL Server";
            const string valueName = "InstalledInstances";
            const string defaultName = "MSSQLSERVER";

            using (var key = hive.OpenSubKey(keyName, false))
            {
                if (key == null) return Enumerable.Empty<string>();

                var value = key.GetValue(valueName) as string[];
                if (value == null) return Enumerable.Empty<string>();

                for (int index = 0; index < value.Length; index++)
                {
                    if (string.Equals(value[index], defaultName, StringComparison.OrdinalIgnoreCase))
                    {
                        value[index] = ".";
                    }
                    else
                    {
                        value[index] = @".\" + value[index];
                    }
                }

                return value;
            }
        }

        private void cbcServer_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtUsername_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtPassword_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtDatabase_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
