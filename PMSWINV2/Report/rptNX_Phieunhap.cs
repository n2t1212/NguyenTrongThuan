using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptNX_Phieunhap : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNX_Phieunhap()
        {
            InitializeComponent();
        }

        public void BindData(){
            //GroupField grp = new GroupField("manhom", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
            //grpSP.GroupFields.Add(grp);
            crNgayCT.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPhieu"));
            crNgayIn.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPhieu"));

            crSophieu.DataBindings.Add(new XRBinding("Text", DataSource, "Sophieu"));
            crNguoigiao.DataBindings.Add(new XRBinding("Text", DataSource, "Giaonhan"));
            crSoHD.DataBindings.Add(new XRBinding("Text", DataSource, "Soctgoc"));
            crDonvi.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkh"));
            crTenKho.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkho"));
            crDiachi.DataBindings.Add(new XRBinding("Text", DataSource, "Diachi"));
            crGhichuCTG.DataBindings.Add(new XRBinding("Text", DataSource, "Ghichu"));

            crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crHanghoa.DataBindings.Add(new XRBinding("Text", DataSource, "Hanghoa"));
            crMahang.DataBindings.Add(new XRBinding("Text", DataSource, "Masp"));
            crDVT.DataBindings.Add(new XRBinding("Text", DataSource, "Dvt"));
            crTheoCT.DataBindings.Add(new XRBinding("Text", DataSource, "SoluongCT", "{0:n0}"));
            crThucnhap.DataBindings.Add(new XRBinding("Text", DataSource, "Soluong", "{0:n0}"));
            crDongia.DataBindings.Add(new XRBinding("Text", DataSource, "Dongia", "{0:n0}"));
            crThanhtien.DataBindings.Add(new XRBinding("Text", DataSource, "Thanhtien", "{0:n0}"));


            crSumThanhtien.DataBindings.Add(new XRBinding("Text", DataSource, "Thanhtien", "{0:#,#}"));
            crSumThanhtien.Summary.Func=SummaryFunc.Sum;
            crSumThanhtien.Summary.FormatString = "{0:n0}";
            crSumThanhtien.Summary.Running = SummaryRunning.Report;        
        }



        private void xrPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            (sender as XRLabel).Text = "Trang: " + (e.PageIndex + 1).ToString();
        }

        private void crBangchu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                Double dSotien=0;
                Double.TryParse(crSumThanhtien.Summary.GetResult().ToString(), out dSotien);
                crBangchu.Text = Model.MTGlobal.SothanhChu(dSotien);
            }
            catch (Exception ex) { }
            
        }

        private void crTheoCT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }

      
 

    }
}
