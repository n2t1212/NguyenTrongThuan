using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using System.Data;
using System.Data.SqlClient;

using MTPMSWIN.Model;


namespace MTPMSWIN.View
{
 
    public partial class dlg_ChonInCongno : Window
    {
        public bool isChon = false;
        private DataTable otblSrc = null;
        public DataTable otblChon = null;
        private String mIsType = "";
        private String mTuNgay = "";
        private String mDenNgay = "";
        private String mMatk = "";
        private String mLoaitien = "";

        private ObservableCollection<KyBC> oKyBC = new ObservableCollection<KyBC>();
        private ObservableCollection<Taikhoan> oTaikhoan = new ObservableCollection<Taikhoan>();
        private class Taikhoan
        {
            public string Matk { get; set; }
            public string Tentk { get; set; }            
        }
        private class KyBC
        {
            public string Maky { get; set; }
            public string Tenky { get; set; }
        }  


        public dlg_ChonInCongno(DataTable oTblKH,String mType = "PTHU")
        {
            InitializeComponent();
            
            this.otblChon = oTblKH;
            this.mIsType = mType;
            mTuNgay =MTGlobal.MT_TUNGAY ;
            mDenNgay =MTGlobal.MT_DENNGAY;
            MTGlobal.SetFormatGridControl(grdCN, tblCN);

            dtpTungay.Text = mTuNgay;
            dtpDenngay.Text = mDenNgay;
            mLoaitien = "VND";

            if (mType == "PTHU") {
                lblTitle.Content = "Tổng hợp công nợ phải thu";
            }
            else if (mType == "PTRA")
            {
                lblTitle.Content = "Tổng hợp công nợ phải trả";
            }
            else {
                lblTitle.Content = "Tổng hợp công nợ";
            }
            setDataCombo();
        }
        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void setDataCombo() {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[2];
                arrPara[0] = new SqlParameter("@LoaiBC", SqlDbType.NVarChar, 5);
                arrPara[0].Value = this.mIsType; 
                arrPara[1] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                DataSet oDset = new MTSQLServer().wDset("spCN_BaocaoList", arrPara);

                if (oDset != null) {
                    if (oDset.Tables[0] != null) {
                      
                        foreach (DataRow vR in oDset.Tables[0].Rows) {
                            oKyBC.Add(new KyBC() { Maky = vR["Maloai"].ToString(),Tenky=vR["Tenloai"].ToString() });
                        }
                        cbKybc.ItemsSource = oKyBC;
                    }

                    if (oDset.Tables[1] != null)
                    {
                        foreach (DataRow vR in oDset.Tables[1].Rows)
                        {
                            oTaikhoan.Add(new Taikhoan() { Matk = vR["Matk"].ToString(), Tentk =vR["Matk"].ToString() +"  "+ vR["Taikhoan"].ToString() });
                        }
                    }
                    cbTaikhoan.ItemsSource =oTaikhoan;
                    cbLoaitien.SelectedIndex = 0;
                }
            }catch { }
        }

        private void setThoigian(String Loai) {
            try{
                if (dtpDenngay.Text == "") {
                    dtpDenngay.Text = DateTime.Now.ToShortDateString();
                }

                switch (cbKybc.SelectedValue.ToString()){
                    case "NGAY":
                        dtpTungay.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                        break;

                    case "THANG":
                        dtpTungay.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                        break;

                    case "QUY":
                        dtpTungay.Text = DateTime.Now.AddMonths(-3).ToShortDateString();
                        break;

                    case "NAM":
                        dtpTungay.Text = DateTime.Now.AddYears(-1).ToShortDateString();
                        break;

                    case "6TDAU":
                        dtpTungay.Text = DateTime.Now.AddMonths(-6).ToShortDateString();
                        break;

                    case "6TCUOI":
                        dtpTungay.Text = DateTime.Now.AddMonths(6).ToShortDateString();
                        break;
                }
            }
            catch { }
        }

        DataTable oTblBC = null;
        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            try{
                mTuNgay = dtpTungay.DateTime.ToShortDateString().Trim();
                mDenNgay = dtpDenngay.DateTime.ToShortDateString().Trim();
                if (cbLoaitien.Text != null)
                {
                    mLoaitien = cbLoaitien.Text;
                }

                if (mTuNgay == "" || mDenNgay == "") {
                    Utils.showMessage("Bạn chưa chọn khoản thời gian cần xem...", "Lưu ý", "ERR");
                    return;
                }
                if (DateTime.Parse(mTuNgay) > DateTime.Parse(mDenNgay)) {
                    Utils.showMessage("Từ ngày không thể lớn hơn đến ngày...", "Lưu ý", "ERR");
                    return;
                }

                if (mMatk == "") {
                    Utils.showMessage("Bạn chưa chọn tài khoản cần xem...", "Lưu ý", "ERR");
                    return;
                }

                SqlParameter[] arrPara = new SqlParameter[5];
                arrPara[0] = new SqlParameter("@Tungay", SqlDbType.NVarChar,10);
                arrPara[0].Value =mTuNgay;
                arrPara[1] = new SqlParameter("@Denngay", SqlDbType.NVarChar,10);
                arrPara[1].Value =mDenNgay;
                arrPara[2] = new SqlParameter("@Matk", SqlDbType.NVarChar, 12);
                arrPara[2].Value = mMatk;
                arrPara[3] = new SqlParameter("@Mant", SqlDbType.NVarChar, 3);
                arrPara[3].Value = mLoaitien;
                arrPara[4] = new SqlParameter("@tblMADT", SqlDbType.Structured);
                arrPara[4].Value = otblChon;
                oTblBC = new MTSQLServer().wRead("rptCN_CanthuCT", arrPara);
                grdCN.ItemsSource = oTblBC;
                //this.Close();
            }
            catch { }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
            isChon = false;
            this.Close();
        }

        private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
      
        //CombobixEdit
        private void cbTaikhoan_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            String mVal = "";
            if (cbTaikhoan.SelectedItem != null)
            {
                mVal = ((Taikhoan)cbTaikhoan.SelectedItem).Matk;
            }
        }

        private void cbTaikhoan_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbTaikhoan.SelectedValue != null) {
                mMatk = cbTaikhoan.SelectedValue.ToString();  
            }            
        }

        private void cbKybc_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbKybc.SelectedValue != null) {
                setThoigian(cbKybc.SelectedValue.ToString());
            }
        }

      

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (oTblBC != null && oTblBC.Rows.Count > 0)
            {
                new MTReport().rptCN_BaocaoCongnophaithu(oTblBC);
            }
            else {
                cmdAccept_Click(sender, e);
                if (oTblBC != null && oTblBC.Rows.Count > 0)
                {
                    new MTReport().rptCN_BaocaoCongnophaithu(oTblBC);
                }
                else
                {
                    Utils.showMessage("Không tìm thấy dữ liệu theo yêu cầu...", "Thông báo");
                }
            }
        }
         
    }
}
