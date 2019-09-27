using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptHH_TheKho_bk : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHH_TheKho_bk()
        {
            InitializeComponent();
        }

        public void BindData(){
            GroupField grp = new GroupField("manhom", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
            grpSP.GroupFields.Add(grp);
            //crGrpNhomSP.DataBindings.Add(new XRBinding("Text", DataSource, "tennhom"));
             crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crNgayPS.DataBindings.Add(new XRBinding("Text", DataSource, "masp"));
            crSoPhieu.DataBindings.Add(new XRBinding("Text", DataSource, "tensp"));
            crMota.DataBindings.Add(new XRBinding("Text", DataSource, "dvt"));
            crPSN.DataBindings.Add(new XRBinding("Text", DataSource, "quycach"));
            crPSX.DataBindings.Add(new XRBinding("Text", DataSource, "quydoikgl", "{0:n3}"));
            crSLTon.DataBindings.Add(new XRBinding("Text", DataSource, "quydoithung", "{0:n1}"));
           // crGhichu.DataBindings.Add(new XRBinding("Text", DataSource, "nhacungcap"));
        }



        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }

    }
}
