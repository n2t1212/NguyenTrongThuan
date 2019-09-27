using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptDM_SanPham : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDM_SanPham()
        {
            InitializeComponent();
        }

        public void BindData(){
            GroupField grp = new GroupField("Manhom", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
            grpNhomSP.GroupFields.Add(grp);
            crGrpNhomSP.DataBindings.Add(new XRBinding("Text", DataSource, "Tennhom"));
             crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crMasp.DataBindings.Add(new XRBinding("Text", DataSource, "Masp"));
            crSanpham.DataBindings.Add(new XRBinding("Text", DataSource, "Tensp"));
            crDVT.DataBindings.Add(new XRBinding("Text", DataSource, "Dvt"));
            crQuicach.DataBindings.Add(new XRBinding("Text", DataSource, "Quycach"));
            crQDKgl.DataBindings.Add(new XRBinding("Text", DataSource, "Quydoikgl", "{0:n3}"));
            crQDThung.DataBindings.Add(new XRBinding("Text", DataSource, "Quydoithung", "{0:n1}"));
            crGhichu.DataBindings.Add(new XRBinding("Text", DataSource, "Nhacungcap"));
        }



        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }

    }
}
