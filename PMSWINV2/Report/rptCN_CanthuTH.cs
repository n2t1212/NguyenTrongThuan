using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptCN_CanthuTH : DevExpress.XtraReports.UI.XtraReport
    { 
        int iStt = 1; 
        public rptCN_CanthuTH()
        {
            InitializeComponent();
        }

        public void BindData(){
             GroupField grp = new GroupField("Tinh", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
             grpDP.GroupFields.Add(grp);
            //crNgayCT.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPhieu"));                                      
            crMatk.DataBindings.Add(new XRBinding("Text", DataSource, "Matk"));

            crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT","{0:n0}"));
            crGrpTinh.DataBindings.Add(new XRBinding("Text", DataSource, "Tinh"));
            crMakh.DataBindings.Add(new XRBinding("Text", DataSource, "Madt"));
            crTenkh.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkh"));             
            crDienthoai.DataBindings.Add(new XRBinding("Text", DataSource, "Dienthoai"));             
            crTinh.DataBindings.Add(new XRBinding("Text", DataSource, "Tinh"));

            crQHDauky.DataBindings.Add(new XRBinding("Text", DataSource, "Dudau", "{0:n0}"));
            crCantra.DataBindings.Add(new XRBinding("Text", DataSource, "Phaithu", "{0:n0}"));
            crDatra.DataBindings.Add(new XRBinding("Text", DataSource, "Datra", "{0:n0}"));
            crTongPhaitra.DataBindings.Add(new XRBinding("Text", DataSource, "Ducuoi", "{0:n0}"));

            crSDauky.DataBindings.Add(new XRBinding("Text", DataSource, "Dudau", "{0:#,#}"));
            crSDauky.Summary.Func = SummaryFunc.Sum;
            crSDauky.Summary.FormatString = "{0:n0}";
            crSDauky.Summary.Running = SummaryRunning.Report;
             
            crSCantra.DataBindings.Add(new XRBinding("Text", DataSource, "Phaithu", "{0:#,#}"));
            crSCantra.Summary.Func = SummaryFunc.Sum;
            crSCantra.Summary.FormatString = "{0:n0}";
            crSCantra.Summary.Running = SummaryRunning.Report;

            crSDatra.DataBindings.Add(new XRBinding("Text", DataSource, "Datra", "{0:#,#}"));
            crSDatra.Summary.Func = SummaryFunc.Sum;
            crSDatra.Summary.FormatString = "{0:n0}";
            crSDatra.Summary.Running = SummaryRunning.Report;

            crSTongPT.DataBindings.Add(new XRBinding("Text", DataSource, "Ducuoi", "{0:#,#}"));
            crSTongPT.Summary.Func = SummaryFunc.Sum;
            crSTongPT.Summary.FormatString = "{0:n0}";
            crSTongPT.Summary.Running = SummaryRunning.Report;
        }
         
       
        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }
                 
        private void crQHDauky_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crCantra_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crDatra_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crTongPhaitra_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSDauky_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSQuahan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSCantra_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSDatra_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSTongPT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

     
    }
}
