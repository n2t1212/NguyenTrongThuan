using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptTL_DiemTichLuy: DevExpress.XtraReports.UI.XtraReport
    {
        public rptTL_DiemTichLuy()
        {
            InitializeComponent();
        }

        public void BindData(){
            crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crMakh.DataBindings.Add(new XRBinding("Text", DataSource, "Makh"));
            crTenkh.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkh"));
            crDiachi.DataBindings.Add(new XRBinding("Text", DataSource, "Diachi"));

            crDoanhso.DataBindings.Add(new XRBinding("Text", DataSource, "TongDoanhso", "{0:n0}"));
            crDiemds.DataBindings.Add(new XRBinding("Text", DataSource, "TongDiemDs", "{0:n0}"));
            crDiemhientai.DataBindings.Add(new XRBinding("Text", DataSource, "Diemhientai", "{0:n0}"));
            crDiemdatru.DataBindings.Add(new XRBinding("Text", DataSource, "TongDiemdatru", "{0:n0}"));
        }
          
        
        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }
    }
}
