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
    public partial class HT_TaoKetNoi : Window
    {      
        private SqlConnection testSQLConnection;

        private string FILE_RESTORE = "";
        private string SERVER = "";
        private string USERNAME = "";
        private string PASSWORD = "";
        private string DBNAME = "";

        public bool isConnect = false;

        public HT_TaoKetNoi()
        {
            InitializeComponent();
        }

        private void formLoad(object sender, RoutedEventArgs e)
        {
            //initCSDLCombobox();
            SERVER = MTGlobal.ReadRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBHOST, "");
            DBNAME = MTGlobal.ReadRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBNAME, "");
            USERNAME = MTGlobal.ReadRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBUSER, "");
            PASSWORD = MTGlobal.ReadRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBPASS, "");

            cmdCreateData.Visibility = Visibility.Hidden;
            txtDB.Text = DBNAME;
            txtUsername.Text = USERNAME;
            txtPassword.Text = PASSWORD;
            if (txtDB.Text != ""){
                cmdSelectFile.IsEnabled = false;         
            }
            else {
                cmdSelectFile.IsEnabled = true;
            }

            initServerCombobox();  
        }

        private bool isConnection(string connectionStr){
            try{
                using (SqlConnection conn = new SqlConnection(connectionStr))
                {
                    conn.Open(); 
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void initServerCombobox(){
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

        public class CSDL
        {
            public string name { get; set; }
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

     

        private void GRID_PRINT_TOP_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void cmdConnect_Click(object sender, RoutedEventArgs e)
        {
        
            //SERVER = ((MTPMSWIN.View.HT_TaoKetNoi.ServerSQL)(cbcServer.SelectedItem)).name;           
            DBNAME = txtDB.Text.ToString().Trim();
            USERNAME = txtUsername.Text.ToString().Trim();
            PASSWORD = txtPassword.Text.ToString().Trim();

            if (SERVER.Length == 0)
            {
                Utils.showMessage("Bạn chưa chọn Máy chủ..", "Thông báo");
                cbcServer.Focus();
                return;
            }
            if (DBNAME.Length == 0)
            {
                Utils.showMessage("CSDL không được để trống", "Thông báo");
                txtDB.Focus();
                return;
            }

            if (USERNAME.Length == 0)
            {
                Utils.showMessage("Tài khoản không được bỏ trống", "Thông báo");
                txtUsername.Focus();
                return;
            }

            if (PASSWORD.Length == 0)
            {
                Utils.showMessage("Mật khẩu không được bỏ trống", "Thông báo");
                txtPassword.Focus();
                return;
            }

            try
            {
                DevExpress.Utils.WaitDialogForm Dlg2=null;
                DevExpress.Utils.WaitDialogForm Dlg = new DevExpress.Utils.WaitDialogForm("Đang thiết lập kết nối đến máy chủ...", "KẾT NỐI MÁY CHỦ");
                Dlg.Show();
                string testConnectionStr = string.Format(@"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Max Pool Size=2000", SERVER, DBNAME, USERNAME, PASSWORD);
                if (isConnection(testConnectionStr))
                {
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBHOST, SERVER);
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBNAME, DBNAME);
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBUSER, USERNAME);
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBPASS, PASSWORD);

                    if (MTGlobal.MTAPPSTART() == true)
                    {
                        Dlg.Close();
                        isConnect = true;
                        Utils.showMessage("Tạo kết nối thành công", "Thông báo");
                        this.Close();
                    }
                    else
                    {
                        Dlg.Close();
                        isConnect = false;
                        Utils.showMessage("Không thể tạo kết nối. Vui lòng kiểm tra lại...", "Thông báo");
                    }
                    Dlg.Close();
                    return;
                }
                else
                {
                    Dlg.Close();
                    Dlg2= new DevExpress.Utils.WaitDialogForm("Đang kiểm tra tài khoản máy chủ...", "KẾT NỐI MÁY CHỦ");
                    Dlg2.Show();
                    string testConnectionStr2 = string.Format(@"Data Source={0};Persist Security Info=True;User ID={1};Password={2};Max Pool Size=2000", SERVER, USERNAME, PASSWORD);
                    if (!isConnection(testConnectionStr2))
                    {
                        Dlg2.Close();
                        Utils.showMessage("Tài khoản hoặc mật khẩu Máy chủ không đúng. Vui lòng kiểm tra lại", "Thông báo");
                        isConnect = false;
                        return;
                    }
                    Dlg2.Close();
                }
                Dlg.Close();
                Dlg2.Close();
            }
            catch { }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void cbDB_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void cmdCreateData_Click(object sender, RoutedEventArgs e)
        {
            DBNAME = txtDB.Text.ToString().Trim();
            USERNAME = txtUsername.Text.ToString().Trim();
            PASSWORD = txtPassword.Text.ToString().Trim();

            if (SERVER.Length == 0) {
                Utils.showMessage("CSDL không được để trống", "Thông báo");
                cbcServer.Focus();
                return;
            }
            if (DBNAME.Length == 0){
                Utils.showMessage("Vui lòng nhập tên CSDL cần tạo...", "Thông báo");
                txtDB.Focus();
                return;
            }
            if (FILE_RESTORE.Length == 0){
                Utils.showMessage("Vui lòng chọn tập tin khôi phục", "Thông báo");
                txtDB.Focus();
                return;
            }

            if (!Utils.validateFileType(FILE_RESTORE, Utils.BACKUP_SQL_TYPE))
            {
                Utils.showMessage(Utils.ERR_FILE_FORMAT(Utils.BACKUP_SQL_EXTENSION), "Thông báo");
                txtDB.Text = ""; 
                return;
            }

            string testConnectionStr = string.Format(@"Data Source={0};Persist Security Info=True;User ID={1};Password={2};Max Pool Size=2000", SERVER, USERNAME, PASSWORD);

            if (!isConnection(testConnectionStr))
            {
                Utils.showMessage("Tài khoản hoặc mật khẩu không đúng. Vui lòng kiểm tra lại", "Thông báo");
                isConnect = false;
                return;
            }

            if (Utils.createAndRestoreDB(SERVER, USERNAME, PASSWORD, DBNAME, FILE_RESTORE))
            {
                Utils.showMessage("Tạo CSDL thành công", "Thông báo");                
                MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBHOST, SERVER);
                MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBNAME, DBNAME);
                MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBUSER, USERNAME);
                MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBPASS, PASSWORD);
                MTGlobal.MTAPPSTART();
                isConnect = true;
                this.Close();
                cmdCreateData.Visibility = Visibility.Hidden;
            }
            else
            {
                Utils.showMessage("Tạo CSDL không thành công", "Thông báo");
                isConnect = false;
            }
        }

        private void cmdSelectFile_Click(object sender, RoutedEventArgs e){
            try{ 
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FILE_RESTORE = ofd.FileName;
                    txtDB.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);        
                    cmdCreateData.Visibility = Visibility.Visible;
                }
            }
            catch { }            
        }

        private void cbcServer_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            SERVER = cbcServer.Text.Trim();
        }
    }
}
