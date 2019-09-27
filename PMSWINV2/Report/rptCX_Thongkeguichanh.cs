using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptCX_Thongkeguichanh : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCX_Thongkeguichanh()
        {
            InitializeComponent();
        }

        private Double dTongCP = 0;

        public void BindData(){
            //GroupField grp = new GroupField("manhom", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
            //grpSP.GroupFields.Add(grp); 
            crNguoinhan.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkh"));
            crDiachi.DataBindings.Add(new XRBinding("Text", DataSource, "Diachi"));
            crSoxe.DataBindings.Add(new XRBinding("Text", DataSource, "Soxe"));
            crTrongluong.DataBindings.Add(new XRBinding("Text", DataSource, "Trongluong", "{0:n0}"));
            crChiphi.DataBindings.Add(new XRBinding("Text", DataSource, "Chiphi", "{0:n0}"));


            crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crHanghoa.DataBindings.Add(new XRBinding("Text", DataSource, "Hanghoa"));
            crMahang.DataBindings.Add(new XRBinding("Text", DataSource, "Masp"));
            crDVT.DataBindings.Add(new XRBinding("Text", DataSource, "Dvt"));        
            crThucnhap.DataBindings.Add(new XRBinding("Text", DataSource, "Soluong", "{0:n0}"));


            //crSumChiphi.DataBindings.Add(new XRBinding("Text", DataSource, "Chiphi", "{0:#,#}"));
            //crSumChiphi.Summary.Func = SummaryFunc.Sum;
            //crSumChiphi.Summary.FormatString = "{0:n0}";
            //crSumChiphi.Summary.Running = SummaryRunning.Report;        
        }
          
        
        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }

        private void crNguoinhan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (this.GetCurrentColumnValue("GRP_TAG") != null)
                {
                    (sender as XRLabel).Tag = this.GetCurrentColumnValue("GRP_TAG").ToString();
                }
            }
            catch (Exception ex) { }
        }

        private void crDiachi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (this.GetCurrentColumnValue("GRP_TAG") != null)
                {
                    (sender as XRLabel).Tag = this.GetCurrentColumnValue("GRP_TAG").ToString();
                }
            }
            catch (Exception ex) { }
        }

        private void crSoxe_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (this.GetCurrentColumnValue("GRP_TAG") != null)
                {
                    (sender as XRLabel).Tag = this.GetCurrentColumnValue("GRP_TAG").ToString();
                }
            }
            catch (Exception ex) { }
        }

        private void crTrongluong_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (this.GetCurrentColumnValue("GRP_TAG") != null)
                {
                    (sender as XRLabel).Tag = this.GetCurrentColumnValue("GRP_TAG").ToString();
                }
            }
            catch (Exception ex) { }
        }

        String mTagtmp = "";
        private void crChiphi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (this.GetCurrentColumnValue("GRP_TAG") != null)
                {
                    (sender as XRLabel).Tag = this.GetCurrentColumnValue("GRP_TAG").ToString();

                    if (mTagtmp != this.GetCurrentColumnValue("GRP_TAG").ToString()) {
                        Double dCP = 0;
                        Double.TryParse(this.GetCurrentColumnValue("Chiphi").ToString(), out dCP);
                        mTagtmp=this.GetCurrentColumnValue("GRP_TAG").ToString();
                        dTongCP += dCP;
                    }
                }

              
            }
            catch (Exception ex) { }
        }

        private void crNgayCT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = String.Format("Từ ngày:{0} Đến ngày: {1}", Model.MTGlobal.MT_TUNGAY, Model.MTGlobal.MT_DENNGAY);
        }

        private void crSumChiphi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {            
            (sender as XRLabel).Text = Model.MTGlobal.getVND(dTongCP.ToString()); 
        }        

    }
}
