using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptTC_Phieuchi : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTC_Phieuchi()
        {
            InitializeComponent();
        }

        public void BindData(){
            crSophieu.DataBindings.Add(new XRBinding("Text", DataSource, "Sophieu"));
            crTKNo.DataBindings.Add(new XRBinding("Text", DataSource, "TKNo"));
            crTKCo.DataBindings.Add(new XRBinding("Text", DataSource, "TKCo"));
            crTenKH.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkh"));
            crDiaChi.DataBindings.Add(new XRBinding("Text", DataSource, "DiaChi"));
            crLyDo.DataBindings.Add(new XRBinding("Text", DataSource, "Lydo"));
            crSoTien.DataBindings.Add(new XRBinding("Text", DataSource, "Sotien", "{0:#,#}"));
            crGhiChu.DataBindings.Add(new XRBinding("Text", DataSource, "Ghichu"));
            crNgayIn.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPhieu"));
        }

        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }

        private void crVietBangChu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                Double dSotien = 0;
                Double.TryParse(crSoTien.Text.ToString(), out dSotien);
                crVietBangChu.Text = Model.MTGlobal.SothanhChu(dSotien);
                crNhanSoTien.Text = Model.MTGlobal.SothanhChu(dSotien);
            }
            catch (Exception ex) { }
        }

      
 

    }
}
