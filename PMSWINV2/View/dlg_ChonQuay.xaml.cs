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

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for BH_BeginFormBanHang.xaml
    /// </summary>
    public partial class dlg_ChonQuay : Window
    {
     
        public bool isChon = false;
        public dlg_ChonQuay()
        {
            InitializeComponent();
            BindData();
        }

        private void BindData() {
            try
            {
                rdbQ1.IsChecked = true;
                rdbC1.IsChecked = true;
    
            }
            catch { }
        }
       
       
        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
            isChon = false;
            this.Close();

        }
        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            isChon = false;
            this.Close();            
        }    

        private void onAccept_Click(object sender, RoutedEventArgs e)
        {
            isChon = false;
            if (rdbC2.IsChecked == true) {
                MTGlobal.MT_CA = "CA 02";
            }
            else if (rdbC3.IsChecked == true)
            {
                MTGlobal.MT_CA = "CA 03";
            }
            else {
                MTGlobal.MT_CA = "CA 01";
            }

            if (rdbQ2.IsChecked == true) {
                MTGlobal.MT_QUAY = "Quầy số 02";
            }
            else if (rdbQ3.IsChecked == true)
            {
                MTGlobal.MT_QUAY = "Quầy số 03";
            }
            else {
                MTGlobal.MT_QUAY = "Quầy số 01";
            }
            isChon = true;
            this.Close();
        }
    }
}
