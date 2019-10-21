using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptCN_CanthuCT : DevExpress.XtraReports.UI.XtraReport
    {
        double mSumSotien = 0;
        int iStt = 1;

        public rptCN_CanthuCT()
        {
            InitializeComponent();
        }

        public void BindData(){
             GroupField grp = new GroupField("Madt", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
             grpKH.GroupFields.Add(grp);

            //crNgayCT.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPhieu"));             
             xrTrangSo.DataBindings.Add(new XRBinding("Tag", DataSource, "Madt"));
             crSTT.DataBindings.Add(new XRBinding("Tag", DataSource, "Phieuid"));

            crDonvi.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkh"));
            crDiachi.DataBindings.Add(new XRBinding("Text", DataSource, "Diachi"));
            crDienthoai.DataBindings.Add(new XRBinding("Text", DataSource, "Dienthoai"));
            crDaidien.DataBindings.Add(new XRBinding("Text", DataSource, "Daidien"));
             
            crDiengiai.DataBindings.Add(new XRBinding("Text", DataSource, "Diengiai"));
            crNgay.DataBindings.Add(new XRBinding("Text", DataSource, "Ngayct","{0:dd/MM/yyyy}"));
            crSohoadon.DataBindings.Add(new XRBinding("Text", DataSource, "Soctgoc"));
            crSoPhieu.DataBindings.Add(new XRBinding("Text", DataSource, "Sophieu", "{0:n0}"));
            crSotienDH.DataBindings.Add(new XRBinding("Text", DataSource, "Sotien", "{0:n0}"));
            crNgaychono.DataBindings.Add(new XRBinding("Text", DataSource, "Nchono", "{0:n0}"));
            crNgayDH.DataBindings.Add(new XRBinding("Text", DataSource, "Ngaydh", "{0:dd/MM/yyyy}"));

            crSumTienDH.DataBindings.Add(new XRBinding("Text", DataSource, "Sotien", "{0:#,#}"));
            crSumTienDH.Summary.Func = SummaryFunc.Sum;
            crSumTienDH.Summary.FormatString = "{0:n0}";
            crSumTienDH.Summary.Running = SummaryRunning.Group;

            crSotienQH.DataBindings.Add(new XRBinding("Text", DataSource, "Dudau", "{0:n0}"));
            crSotienDatra.DataBindings.Add(new XRBinding("Text", DataSource, "Datra","{0:n0}"));
            crSotienCantra.DataBindings.Add(new XRBinding("Text", DataSource, "Ducuoi", "{0:n0}"));        

        }



        String lastMadt = "";
        Int32 iPageNumber = 0;
        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            string curMadt= (sender as XRLabel).Tag.ToString();
            if (curMadt == lastMadt)
            {
                iPageNumber += 1;
            }
            else {
                iPageNumber = 1;
            }
            curMadt = lastMadt;

            if (iPageNumber > e.PageIndex + 1)
            {
                (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
            }
            else {
                (sender as XRLabel).Text = "Trang: " + iPageNumber.ToString();
            }           
        }

        private void crTheoCT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void subDatratrongky_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                System.Data.DataTable otblSub = (System.Data.DataTable)this.DataSource;
                if (otblSub != null) {
                    otblSub.DefaultView.RowFilter = String.Format("Madt='{0}' AND Phan=2", this.GetCurrentColumnValue("Madt").ToString());
                    if (otblSub.DefaultView.Count <= 0) {
                        e.Cancel = true;
                    }

                    otblSub.DefaultView.RowFilter = "";
                    rptCN_CanthuCT_Sub oRptSub = new rptCN_CanthuCT_Sub();
                    oRptSub.DataSource = this.DataSource;
                    oRptSub.FilterString = String.Format("Madt='{0}' AND Phan=2", this.GetCurrentColumnValue("Madt").ToString());
                    oRptSub.BindData();
                    subDatratrongky.ReportSource = oRptSub;
                }
            }
            catch(Exception ex) { }
            


        }

        private void crSotienDH_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                Double iSotien = 0;
                Double.TryParse(GetCurrentColumnValue("Sotien").ToString(), out iSotien);
                mSumSotien += iSotien;
            }
            catch { }
        }

        private void crSumTienDH_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //(sender as XRLabel).Text = mSumSotien.ToString();   
            Model.MTGlobal.BlankZero(sender);
        }

        String lastStt = "";
        private void crSTT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string curStt = (sender as XRLabel).Tag.ToString();
            if (curStt != lastStt)
            {
                iStt = iStt + 1;
            }
            else
            {
                iStt = 1;
            }
            lastStt = curStt;
           (sender as XRLabel).Text = iStt.ToString();
        }

        private void grpKH_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            iStt = 0;
            iPageNumber = 0;
        }

        private void crNgaychono_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSotienDatra_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

        private void crSotienCantra_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        } 
    }
}
