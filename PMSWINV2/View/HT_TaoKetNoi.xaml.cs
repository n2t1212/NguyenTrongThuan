﻿using System;
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
        private string restoreFile = "";
        private SqlConnection testSQLConnection;

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
            initServerCombobox();
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

        //private void initCSDLCombobox()
        //{
        //    List<string> lstCSDL = new List<string>();
        //    try
        //    {
        //        string connectionString = string.Format(@"Data Source={0};Persist Security Info=True;User ID={1};Password={2};Max Pool Size=2000", ".\\SQL2008", "sa", "123456789");
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            con.Open();
        //            using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
        //            {
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        lstCSDL.Add(dr[0].ToString());
        //                    }
        //                }
        //            }
        //        }  
        //    }
        //    catch(Exception ex)
        //    {
        //        var a = ex.ToString();
        //    }
        //    int count = lstCSDL.Count;
        //    List<CSDL> dsCSDL = new List<CSDL>();
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            dsCSDL.Add(new CSDL() { name = lstCSDL[i] });
        //        }
        //    }

        //    cbDB.ItemsSource = dsCSDL;
        //}

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

        private void txtDatabase_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
            if (((MTPMSWIN.View.HT_TaoKetNoi.ServerSQL)(cbcServer.SelectedItem)) == null)
            {
                Utils.showMessage("Vui lòng chọn server", "Thông báo");
                cbcServer.Focus();
                return;
            }

            //if (((MTPMSWIN.View.HT_TaoKetNoi.CSDL)(cbDB.SelectedItem)) == null)
            //{
            //    Utils.showMessage("Vui lòng chọn CSDL", "Thông báo");
            //    cbDB.Focus();
            //    return;
            //}

            SERVER = ((MTPMSWIN.View.HT_TaoKetNoi.ServerSQL)(cbcServer.SelectedItem)).name;
            //DBNAME = ((MTPMSWIN.View.HT_TaoKetNoi.CSDL)(cbDB.SelectedItem)).name;
            DBNAME = txtDB.Text.ToString().Trim();
            USERNAME = txtUsername.Text.ToString().Trim();
            PASSWORD = txtPassword.Text.ToString().Trim();

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

            string testConnectionStr = string.Format(@"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Max Pool Size=2000", SERVER, DBNAME, USERNAME, PASSWORD);

            if (isConnection(testConnectionStr))
            {
                Utils.showMessage("Tạo kết nối thành công", "Thông báo");
                isConnect = true;

                /*MTGlobal.SetConfigKey("MT_SQL_HOST", SERVER);
                MTGlobal.SetConfigKey("MT_SQL_DBNAME",DBNAME );
                MTGlobal.SetConfigKey("MT_SQL_USER", USERNAME);
                MTGlobal.SetConfigKey("MT_SQL_PASS", PASSWORD);
                */
                MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBHOST, SERVER);
                MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBNAME, DBNAME);
                MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBUSER, USERNAME);
                MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBPASS, PASSWORD);
                if (MTGlobal.MTAPPSTART() == true)
                {
                    isConnect = true;
                    this.Close();
                }
                else
                {
                    isConnect = false;
                }
                return;
            }
            else
            {
                string testConnectionStr2 = string.Format(@"Data Source={0};Persist Security Info=True;User ID={1};Password={2};Max Pool Size=2000", SERVER, USERNAME, PASSWORD);

                if (isConnection(testConnectionStr2))
                {
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBHOST, SERVER);
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBNAME, DBNAME);
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBUSER, USERNAME);
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBPASS, PASSWORD);

                    if (Utils.ConfirmMessage("Không thể thiết lập kết nối CSDL. Bạn có muốn tạo kết nối đến CSDL ?", "Xác nhận tạo kết nối"))
                    {
                        // Utils.createSQLServerUser(USERNAME, PASSWORD);
                        lblFile.Visibility = Visibility.Visible;
                        txtFileRestore.Visibility = Visibility.Visible;
                        cmdSelectFile.Visibility = Visibility.Visible;
                        cmdCreateData.Visibility = Visibility.Visible;
                        cmdConnect.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    Utils.showMessage("Tài khoản hoặc mật khẩu không đúng. Vui lòng kiểm tra lại", "Thông báo");
                    isConnect = false;
                    return;
                }
            }
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

            if (DBNAME.Length == 0)
            {
                Utils.showMessage("CSDL không được để trống", "Thông báo");
                txtDB.Focus();
                return;
            }

            if (restoreFile.Length == 0)
            {
                Utils.showMessage("Vui lòng chọn tập tin khôi phục", "Thông báo");
                txtFileRestore.Focus();
                return;
            }

            if (!Utils.validateFileType(restoreFile, Utils.BACKUP_SQL_TYPE))
            {
                Utils.showMessage(Utils.ERR_FILE_FORMAT(Utils.BACKUP_SQL_EXTENSION), "Thông báo");
                txtFileRestore.Text = "";
                cmdCreateData.IsEnabled = false;
                return;
            }

            string testConnectionStr = string.Format(@"Data Source={0};Persist Security Info=True;User ID={1};Password={2};Max Pool Size=2000", SERVER, USERNAME, PASSWORD);

            if (!isConnection(testConnectionStr))
            {
                Utils.showMessage("Tài khoản hoặc mật khẩu không đúng. Vui lòng kiểm tra lại", "Thông báo");
                isConnect = false;
                return;
            }

            if (Utils.createAndRestoreDB(SERVER, USERNAME, PASSWORD, DBNAME, restoreFile))
            {
                Utils.showMessage("Tạo CSDL thành công", "Thông báo");
                MTGlobal.MTAPPSTART();
                isConnect = true;
                this.Close();
            }
            else
            {
                Utils.showMessage("Tạo CSDL không thành công", "Thông báo");
                isConnect = false;
            }
        }

        private void cmdSelectFile_Click(object sender, RoutedEventArgs e)
        {
            cmdCreateData.IsEnabled = true;
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                restoreFile = ofd.FileName;
                txtFileRestore.Text = restoreFile;
            }
        }
    }
}
