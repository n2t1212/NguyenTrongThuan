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

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for dlgChonNgay.xaml
    /// </summary>
    public partial class dlg_ChonNgay : Window
    {
        private bool isLoad = false;
        public bool isChon = false;

        public dlg_ChonNgay()
        {
            InitializeComponent();
            isLoad = true;
            if (MTGlobal.MT_LOAI_TG == "M") {
                rdbThang.IsChecked = true;
            }
            else if (MTGlobal.MT_LOAI_TG == "Y")
            {
                rdbNam.IsChecked = true;
            }
            else {
                rdbNgay.IsChecked = true;
            }
        }
        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        public void setPermit(bool isNgay = true, bool isThang = true, bool isNam = true) {
            if (isNgay)
            {
                rdbNgay.IsEnabled = true;
            }
            else { rdbNgay.IsEnabled = false; rdbNgay.IsChecked = false; }

            if (isThang)
            {
                rdbThang.IsEnabled = true;
            }
            else { rdbThang.IsEnabled = false; rdbThang.IsChecked = false; }

            if (isNam)
            {
                rdbNam.IsEnabled = true;
            }
            else { rdbNam.IsEnabled = false; rdbNam.IsChecked = false; }

            if (isNgay == true && isThang == false && isNam == false) {
                rdbNgay.IsChecked = true;
            }
            else if (isNgay == false && isThang == true && isNam == false) {
                rdbThang.IsChecked = true;
            }
            else if (isNgay == false && isThang == false && isNam == true) {
                rdbNam.IsChecked = true;
            }

            if (MTGlobal.MT_TUNGAY != "" && MTGlobal.MT_DENNGAY != "")
            {
                dtpTuNgay.EditValue = Convert.ToDateTime(MTGlobal.MT_TUNGAY);
                dtpDenNgay.EditValue = Convert.ToDateTime(MTGlobal.MT_DENNGAY);
            }
            else
            {
                dtpTuNgay.EditValue = DateTime.Now.AddDays(-1);
                dtpDenNgay.EditValue = DateTime.Now;
            }
            dtpTuNgay.Focus();
        }
    
        private void rdbNgay_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdbNgay.IsChecked == true && isLoad == true)
                {
                    PnlNgay.Visibility = Visibility.Visible;
                    PnlThang.Visibility = Visibility.Hidden;
                    PnlNam.Visibility = Visibility.Hidden;
                    if (MTGlobal.MT_TUNGAY != "" && MTGlobal.MT_DENNGAY != "")
                    {
                        dtpTuNgay.EditValue = Convert.ToDateTime(MTGlobal.MT_TUNGAY);
                        dtpDenNgay.EditValue = Convert.ToDateTime(MTGlobal.MT_DENNGAY);
                    }
                    else
                    {
                        dtpTuNgay.EditValue = DateTime.Now.AddDays(-1);
                        dtpDenNgay.EditValue = DateTime.Now;
                    }
                    dtpTuNgay.Focus();
                    MTGlobal.MT_LOAI_TG = "D";
                }
            }
            catch { }
      
        }
        private void rdbThang_Checked(object sender, RoutedEventArgs e)
        {
            if (rdbThang.IsChecked == true && isLoad == true)
            {
                PnlNgay.Visibility = Visibility.Hidden;
                PnlThang.Visibility = Visibility.Visible;
                PnlNam.Visibility = Visibility.Hidden; 
                if (MTGlobal.MT_THANG != "")
                {
                    txtThang.Text = MTGlobal.MT_THANG;
                }
                else {
                    txtThang.Text = DateTime.Today.Month.ToString().PadLeft(2,'0') +"/"+DateTime.Today.Year.ToString();
                }
                 txtThang.Focus();
                 MTGlobal.MT_LOAI_TG = "M";
            }
        }
        private void rdbNam_Checked(object sender, RoutedEventArgs e)
        {
            if (rdbNam.IsChecked == true && isLoad == true)
            {
                PnlNgay.Visibility = Visibility.Hidden;
                PnlThang.Visibility = Visibility.Hidden;
                PnlNam.Visibility = Visibility.Visible;

                if (MTGlobal.MT_NAM != "")
                {
                    txtNam.Text = MTGlobal.MT_NAM;
                }
                else {
                    txtNam.Text = DateTime.Today.Year.ToString();
                }
                txtNam.Focus();
                MTGlobal.MT_LOAI_TG = "Y";
            }
        }

        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            try {
                isChon = false;
                if (rdbNgay.IsChecked==true) {
                    if(dtpTuNgay.Text=="" && dtpDenNgay.Text==""){
                        MessageBox.Show("Bạn chưa chọn ngày cần xem.","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        dtpTuNgay.Focus();
                        return;
                    }else if(dtpTuNgay.Text=="" && dtpDenNgay.Text!=""){
                        MessageBox.Show("Bạn chưa chọn ngày bắt đầu cần xem.","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        dtpTuNgay.Focus();
                        return;
                    }else if(dtpTuNgay.Text!="" && dtpDenNgay.Text==""){
                        MessageBox.Show("Bạn chưa chọn đến ngày cần xem.","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        dtpDenNgay.Focus();
                        return;
                    }else if(dtpTuNgay.DateTime>dtpDenNgay.DateTime){
                        MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        dtpTuNgay.Focus();
                        return;
                    }
                    MTGlobal.MT_TUNGAY =Convert.ToDateTime(dtpTuNgay.Text.ToString()).ToShortDateString();
                    MTGlobal.MT_DENNGAY =Convert.ToDateTime(dtpDenNgay.Text.ToString()).ToShortDateString();
                    isChon = true;
                }
                else if (rdbThang.IsChecked == true) {
                    if(txtThang.Text==""){
                        MessageBox.Show("Bạn chưa chọn tháng cần xem.","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        txtThang.Focus();
                        return;
                    }else if(txtThang.Text.Length!=7){
                        MessageBox.Show("Tháng cần xem không hợp lệ (dd/YYYY).","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        txtThang.Focus();
                        return;
                    }else if( int.Parse(txtThang.Text.Trim().Substring(0,2))>12 &&  int.Parse(txtThang.Text.Trim().Substring(0,2))==0){
                         MessageBox.Show("Bạn nhập tháng không hợp lệ (dd/YYYY).","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        txtThang.Focus();
                        return;
                    }else if( int.Parse(txtThang.Text.Trim().Substring(3,txtThang.Text.Length-3))<1900){
                         MessageBox.Show("Bạn nhập tháng không hợp lệ (dd/YYYY).","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        txtThang.Focus();
                        return;
                    }
                    
                    MTGlobal.MT_THANG=txtThang.Text.Trim();
                    MTGlobal.MT_TUNGAY = "01/" + txtThang.Text.ToString();
                    if(txtThang.Text.Trim().Substring(0,2)=="12"){
                       MTGlobal.MT_DENNGAY =Convert.ToDateTime("01/01/"+ (int.Parse(txtThang.Text.Substring(3,txtThang.Text.Length-3))+1).ToString()).AddDays(-1).ToShortDateString();
                    }else{
                      MTGlobal.MT_DENNGAY=Convert.ToDateTime("01/"+ (int.Parse(txtThang.Text.Substring(0,2))+1).ToString() +"/" + txtThang.Text.Substring(3,txtThang.Text.Length-3)).AddDays(-1).ToShortDateString();
                    }                    
                    isChon = true;
                }

                else if (rdbNam.IsChecked == true) {
                    if (txtNam.Text == "") { 
                        MessageBox.Show("Bạn chưa chọn năm cần xem..","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        txtNam.Focus();
                        return;
                        
                    }else if(txtNam.Text.ToString().Length!=4 && int.Parse(txtNam.Text)<1900){
                        MessageBox.Show("Năm cần xem không hợp lệ..","Lưu ý",MessageBoxButton.OK,MessageBoxImage.Information);
                        txtNam.Focus();
                        return;
                    }else if(int.Parse(txtNam.Text)>DateTime.Today.Year){
                        MessageBox.Show("Năm cần xem không thể lớn hơn năm hiện tại..", "Lưu ý", MessageBoxButton.OK, MessageBoxImage.Information);
                        txtNam.Focus();
                        return;
                    } 
                    MTGlobal.MT_TUNGAY="01/01/"+txtNam.Text.Trim();
                    MTGlobal.MT_DENNGAY="31/12/"+txtNam.Text.Trim();
                    MTGlobal.MT_NAM=txtNam.Text.Trim();
                }

                if (isChon)
                {
                    this.Close();
                }
                else {
                    Utils.showMessage("Bạn chưa chọn thời gian cần xem..", "Lưu ý");
                }
            }catch{}
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            isChon = false;
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

        private void dtpTuNgay_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void dtpDenNgay_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cmdAccept.Focus();
            }
        }

        private void cmdAccept_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cmdAccept_Click(sender, e);
            }
        }
  

      
    }
}
