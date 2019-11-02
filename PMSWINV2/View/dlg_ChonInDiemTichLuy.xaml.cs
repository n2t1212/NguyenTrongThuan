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
    public partial class dlg_ChonInDiemTichLuy : Window
    {
        private DataTable tblDTL2View = null;
        private DataTable tblKH;

        public dlg_ChonInDiemTichLuy(DataTable itblKH)
        {
            InitializeComponent();
            this.tblKH = itblKH;
            getReportDataToView(tblKH);
            grdDTL.ItemsSource = tblDTL2View;
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void getReportDataToView(DataTable tblKH)
        {
            SqlParameter[] arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@isChiTiet", SqlDbType.Int);
            arrPara[0].Value = 0;
            arrPara[1] = new SqlParameter("@tblMADT", SqlDbType.Structured);
            arrPara[1].Value = tblKH;

            tblDTL2View = new MTSQLServer().wRead("rptTL_TongDiemTL", arrPara);
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
            this.WindowState = WindowState.Minimized;
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            new MTReport().rptTL_DiemTichLuy(0, tblKH);
        }
      
    }
}
