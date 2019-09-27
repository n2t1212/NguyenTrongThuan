using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptCX_Phieuguichanh: DevExpress.XtraReports.UI.XtraReport
    {
        public rptCX_Phieuguichanh()
        {
            InitializeComponent();
        }

        public void BindData(){
            //GroupField grp = new GroupField("manhom", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
            //grpSP.GroupFields.Add(grp);
            crNgayCT.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPhieu"));
            crNgayIn.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPhieu"));

            crSophieu.DataBindings.Add(new XRBinding("Text", DataSource, "Sophieu"));
            crSoxe.DataBindings.Add(new XRBinding("Text", DataSource, "Soxe"));
            crTaixe.DataBindings.Add(new XRBinding("Text", DataSource, "Taixe"));
            crDienthoaiTX.DataBindings.Add(new XRBinding("Text", DataSource, "Dienthoaixe"));

            crKhachhang.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkh")); 
            crDienthoai.DataBindings.Add(new XRBinding("Text", DataSource, "Dienthoai"));
            crDiachiKH.DataBindings.Add(new XRBinding("Text", DataSource, "Diachi"));

            crTrongluong.DataBindings.Add(new XRBinding("Text", DataSource, "Trongluong", "{0:n0}"));
            crChiphi.DataBindings.Add(new XRBinding("Text", DataSource, "Chiphi", "{0:n0}"));
            crHTThanhtoan.DataBindings.Add(new XRBinding("Text", DataSource, "HTThanhtoan"));
            crGhichu.DataBindings.Add(new XRBinding("Text", DataSource, "Ghichu"));

            crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crHanghoa.DataBindings.Add(new XRBinding("Text", DataSource, "Hanghoa"));
            crMahang.DataBindings.Add(new XRBinding("Text", DataSource, "Masp"));
            crDVT.DataBindings.Add(new XRBinding("Text", DataSource, "Dvt"));
            crTheoCT.DataBindings.Add(new XRBinding("Text", DataSource, "SoluongCT", "{0:n0}"));
            crThucnhap.DataBindings.Add(new XRBinding("Text", DataSource, "Soluong", "{0:n0}"));
            crGhichuHH.DataBindings.Add(new XRBinding("Text", DataSource, "GhichuHH"));  
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
                Double.TryParse(this.GetCurrentColumnValue("Chiphi").ToString(), out dSotien);
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
