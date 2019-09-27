using MTPMSWIN.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for HT_SaoLuu.xaml
    /// </summary>
    public partial class HT_SaoLuu : Window
    {
        string backUpFile = "";
        private MTGlobal.MT_ROLE MTROLE;
        public HT_SaoLuu()
        {
            InitializeComponent();
            btnBackup.IsEnabled = false;
        }

        private void loadForm(object sender, RoutedEventArgs e)
        {
        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cmd = "BACKUP DATABASE [" + MTGlobal.MT_DBNAME + "] TO DISK='" + @backUpFile + "'";

              
                if (MTGlobal.MT_Cnn.State == ConnectionState.Closed)
                {
                    MTGlobal.MT_Cnn.Open();
                }

                SqlCommand command = new SqlCommand(cmd, MTGlobal.MT_Cnn);
                int rs = command.ExecuteNonQuery();

                Utils.showMessage("Sao lưu dữ liệu thành công", "Thông báo", "MSG");

                btnBackup.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Utils.showMessage("Lỗi. Không thể sao lưu dữ liệu", "", "Thông báo");
            }
        }

        private void btnSelectPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Chọn thư mục";

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                backUpFile = fbd.SelectedPath + "\\" + MTGlobal.MT_DBNAME + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                txtPath.Text = backUpFile;
                btnBackup.IsEnabled = true;
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            Utils.fExit();
        }
     
    }
}
