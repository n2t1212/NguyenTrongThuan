using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptNX_Baocaobanhang: DevExpress.XtraReports.UI.XtraReport
    {
        public rptNX_Baocaobanhang()
        {
            InitializeComponent();
        }

        private Double dTongCP = 0;

        public void BindData(){
            //GroupField grp = new GroupField("manhom", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
            //grpSP.GroupFields.Add(grp);  
            crStt.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));

            crTensp.DataBindings.Add(new XRBinding("Text", DataSource, "Tensp"));
            crMasp.DataBindings.Add(new XRBinding("Text", DataSource, "Masp"));
            crDVT.DataBindings.Add(new XRBinding("Text", DataSource, "Dvt"));
            crQuycach.DataBindings.Add(new XRBinding("Text", DataSource, "Quycach"));
            crSL.DataBindings.Add(new XRBinding("Text", DataSource, "Soluong", "{0:n0}"));
            crDongia.DataBindings.Add(new XRBinding("Text", DataSource, "Dongia", "{0:n0}"));
            crTienban.DataBindings.Add(new XRBinding("Text", DataSource, "Thanhtien", "{0:n0}"));
            crTientralai.DataBindings.Add(new XRBinding("Text", DataSource, "Tralai", "{0:n0}"));
            crPhidv.DataBindings.Add(new XRBinding("Text", DataSource, "Phidv", "{0:n0}"));
            crTienCK.DataBindings.Add(new XRBinding("Text", DataSource, "TTCK", "{0:n0}"));
            crTienVAT.DataBindings.Add(new XRBinding("Text", DataSource, "TTVat", "{0:n0}"));
            crDoanhthu.DataBindings.Add(new XRBinding("Text", DataSource, "Tongcong", "{0:n0}"));


            crSumTienban.DataBindings.Add(new XRBinding("Text", DataSource, "Thanhtien", "{0:n0}"));
            crSumTienban.Summary.Func = SummaryFunc.Sum;
            crSumTienban.Summary.FormatString = "{0:n0}";
            crSumTienban.Summary.Running = SummaryRunning.Report;

            crSumTientra.DataBindings.Add(new XRBinding("Text", DataSource, "Tralai", "{0:n0}"));
            crSumTientra.Summary.Func = SummaryFunc.Sum;
            crSumTientra.Summary.FormatString = "{0:n0}";
            crSumTientra.Summary.Running = SummaryRunning.Report;

            crSumPhiDV.DataBindings.Add(new XRBinding("Text", DataSource, "Phidv", "{0:n0}"));
            crSumPhiDV.Summary.Func = SummaryFunc.Sum;
            crSumPhiDV.Summary.FormatString = "{0:n0}";
            crSumPhiDV.Summary.Running = SummaryRunning.Report;

            crSumTienCK.DataBindings.Add(new XRBinding("Text", DataSource, "TTCK", "{0:n0}"));
            crSumTienCK.Summary.Func = SummaryFunc.Sum;
            crSumTienCK.Summary.FormatString = "{0:n0}";
            crSumTienCK.Summary.Running = SummaryRunning.Report;

            crSumVAT.DataBindings.Add(new XRBinding("Text", DataSource, "TTVat", "{0:n0}"));
            crSumVAT.Summary.Func = SummaryFunc.Sum;
            crSumVAT.Summary.FormatString = "{0:n0}";
            crSumVAT.Summary.Running = SummaryRunning.Report;

            crSumDoanhthu.DataBindings.Add(new XRBinding("Text", DataSource, "Tongcong", "{0:n0}"));
            crSumDoanhthu.Summary.Func = SummaryFunc.Sum;
            crSumDoanhthu.Summary.FormatString = "{0:n0}";
            crSumDoanhthu.Summary.Running = SummaryRunning.Report;        
        }
          
        
        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }
         

        private void crNgayCT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = String.Format("Từ ngày:{0} Đến ngày: {1}", Model.MTGlobal.MT_TUNGAY, Model.MTGlobal.MT_DENNGAY);
        }

        private void crTientralai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crTienCK_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crPhidv_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crTienVAT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }
         

    }
}
