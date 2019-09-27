using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptHH_Thekho : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHH_Thekho()
        {
            InitializeComponent();
        }

        public void BindData(){
             GroupField grp = new GroupField("Masp", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
             grpSP.GroupFields.Add(grp);

            crTensp.DataBindings.Add(new XRBinding("Text", DataSource, "Tensp"));
            crMasp.DataBindings.Add(new XRBinding("Text", DataSource, "Masp"));
            crDvt.DataBindings.Add(new XRBinding("Text", DataSource, "Dvt"));
            crQuycach.DataBindings.Add(new XRBinding("Text", DataSource, "Quycach"));

           
            crNgayIn.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPhieu")); 
             
            //crGhichuCTG.DataBindings.Add(new XRBinding("Text", DataSource, "Ghichu"));

            crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crNgayct.DataBindings.Add(new XRBinding("Text", DataSource, "Ngayct","{0:dd/MM/yyyy}"));
            crNgayPS.DataBindings.Add(new XRBinding("Text", DataSource, "Ngayps", "{0:dd/MM/yyyy}"));
            crSophieuN.DataBindings.Add(new XRBinding("Text", DataSource, "SophieuN"));
            crSophieuX.DataBindings.Add(new XRBinding("Text", DataSource, "SophieuX"));
            crDiengiai.DataBindings.Add(new XRBinding("Text", DataSource, "Diengiai"));

            crSLN.DataBindings.Add(new XRBinding("Text", DataSource, "SLNhap", "{0:n0}"));
            crSLX.DataBindings.Add(new XRBinding("Text", DataSource, "SLXuat", "{0:n0}"));
            crCK.DataBindings.Add(new XRBinding("Text", DataSource, "Cuoiky", "{0:n0}")); 
        }



        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }

      
        private void crTheoCT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSTT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSLN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSLX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crCK_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

      
 

    }
}
