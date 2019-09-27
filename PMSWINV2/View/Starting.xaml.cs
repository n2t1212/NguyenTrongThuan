using System;
using System.Collections.Generic;
using System.Data; 
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
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Editors;
using MTPMSWIN.Model;
using System.Data.SqlClient;

namespace MTPMSWIN.View
{
    
    public partial class Starting : Window{

        DevExpress.Utils.WaitDialogForm oWait;
        public Starting()
        {
            InitializeComponent();          
        }

        private void splashWindow_Loaded(object sender, RoutedEventArgs e){
           oWait= Utils.shwWait("Đang tạo kết nối dữ liệu hệ thống...", "Đang xử lý..");
            try{
                if (MTGlobal.MTAPPSTART())
                {
                    MTGlobal.onSetPara();
                    Utils.closeWait(oWait);
                    if (MTGlobal.HT_POS_IMEI == "")
                    {
                        HT_Register register = new HT_Register();
                        register.Show();
                    }
                    else
                    {
                        HT_Login login = new HT_Login();
                        login.Show();
                    }
                    this.Close();
                }
                else
                {
                    Utils.closeWait(oWait);
                    if (Utils.ConfirmMessage("Thông tin kết nối dữ liệu không đúng. Bạn có muốn tạo kết nối đến CSDL ?", "Xác nhận tạo kết nối"))
                    {
                        //Show Đăng ký kết nối SQL  
                        HT_TaoKetNoi oConnect = new HT_TaoKetNoi();
                        oConnect.ShowDialog();
                        if (oConnect.isConnect)
                        {
                            HT_Login login = new HT_Login();
                            login.Show();
                        }
                        else {
                            this.Close();
                        }

                    }
                    else {
                        this.Close();
                    }
                    
                }
                 Utils.closeWait(oWait);
            }
            catch (Exception ex) {
                MessageBox.Show("ERROR LOAD:" + ex.Message.ToString(), "THÔNG TIN LỖI", MessageBoxButton.OK, MessageBoxImage.Error);
                Utils.closeWait(oWait);
            }
        }

    }
}
