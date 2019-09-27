using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptHH_Baocaonhapxuatton: DevExpress.XtraReports.UI.XtraReport
    {
        public rptHH_Baocaonhapxuatton()
        {
            InitializeComponent();
        }

        public void BindData(){
            GroupField grp = new GroupField("Manhom", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
            grpSP.GroupFields.Add(grp);

            crgrpNhomHH.DataBindings.Add(new XRBinding("Text", DataSource, "Nhomhh"));

            crStt.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crMasp.DataBindings.Add(new XRBinding("Text", DataSource, "Masp"));
            crTensp.DataBindings.Add(new XRBinding("Text", DataSource, "Tensp"));        
            crDVT.DataBindings.Add(new XRBinding("Text", DataSource, "Dvt"));
            crQuycach.DataBindings.Add(new XRBinding("Text", DataSource, "Quycach"));

            crDG.DataBindings.Add(new XRBinding("Text", DataSource, "Dongia", "{0:n0}"));
            crDK_SL.DataBindings.Add(new XRBinding("Text", DataSource, "Dauky", "{0:n0}"));
            crDK_TT.DataBindings.Add(new XRBinding("Text", DataSource, "TrigiaD", "{0:n0}"));
            crPSN_SL.DataBindings.Add(new XRBinding("Text", DataSource, "PSNhap", "{0:n0}"));
            crPSN_TT.DataBindings.Add(new XRBinding("Text", DataSource, "TrigiaN", "{0:n0}"));
            crPSX_SL.DataBindings.Add(new XRBinding("Text", DataSource, "PSXuat", "{0:n0}"));            
            crPSX_TT.DataBindings.Add(new XRBinding("Text", DataSource, "TrigiaX", "{0:n0}"));
            crCK_SL.DataBindings.Add(new XRBinding("Text", DataSource, "Cuoiky", "{0:n0}"));
            crCK_TT.DataBindings.Add(new XRBinding("Text", DataSource, "TrigiaC", "{0:n0}"));


            crSumTrigiaD.DataBindings.Add(new XRBinding("Text", DataSource, "TrigiaD", "{0:n0}"));
            crSumTrigiaD.Summary.Func = SummaryFunc.Sum;
            crSumTrigiaD.Summary.FormatString = "{0:n0}";
            crSumTrigiaD.Summary.Running = SummaryRunning.Report;

            crSumTrigiaN.DataBindings.Add(new XRBinding("Text", DataSource, "TrigiaN", "{0:n0}"));
            crSumTrigiaN.Summary.Func = SummaryFunc.Sum;
            crSumTrigiaN.Summary.FormatString = "{0:n0}";
            crSumTrigiaN.Summary.Running = SummaryRunning.Report;

            crSumTrigiaX.DataBindings.Add(new XRBinding("Text", DataSource, "TrigiaX", "{0:n0}"));
            crSumTrigiaX.Summary.Func = SummaryFunc.Sum;
            crSumTrigiaX.Summary.FormatString = "{0:n0}";
            crSumTrigiaX.Summary.Running = SummaryRunning.Report;

            crSumTrigiaC.DataBindings.Add(new XRBinding("Text", DataSource, "TrigiaC", "{0:n0}"));
            crSumTrigiaC.Summary.Func = SummaryFunc.Sum;
            crSumTrigiaC.Summary.FormatString = "{0:n0}";
            crSumTrigiaC.Summary.Running = SummaryRunning.Report;
        }
          
        
        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }
         

        private void crNgayCT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = String.Format("Từ ngày:{0} Đến ngày: {1}", Model.MTGlobal.MT_TUNGAY, Model.MTGlobal.MT_DENNGAY);
        }
         

        private void crDG_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        
        private void crDK_SL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crDK_TT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crPSN_SL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crPSN_TT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crPSX_SL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crPSX_TT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crCK_SL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crCK_TT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSumTrigiaD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSumTrigiaN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSumTrigiaX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSumTrigiaC_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }
         

    }
}
