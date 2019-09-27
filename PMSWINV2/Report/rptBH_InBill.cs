using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptBH_InBill : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBH_InBill()
        {
            InitializeComponent();
        }

        public void BindData(){
            GroupField grp = new GroupField("Loai", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending);
            grpLoai.GroupFields.Add(grp);
            crGrpLoai.DataBindings.Add(new XRBinding("Text", DataSource, "Loai"));
             
            crNgayBan.DataBindings.Add(new XRBinding("Text", DataSource, "Ngayct"));
            crTenKH.DataBindings.Add(new XRBinding("Text", DataSource, "Tenkh"));
            crDiaChi.DataBindings.Add(new XRBinding("Text", DataSource, "Diachi"));
            crDienthoai.DataBindings.Add(new XRBinding("Text", DataSource, "Dienthoai"));
            crThuNgan.DataBindings.Add(new XRBinding("Text", DataSource, "Thungan"));
            crGhichu.DataBindings.Add(new XRBinding("Text", DataSource, "Ghichu"));

            crSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            crTenSP.DataBindings.Add(new XRBinding("Text", DataSource, "Diengiai"));
            crSL.DataBindings.Add(new XRBinding("Text", DataSource, "Soluong", "{0:n0}"));
            crDonGia.DataBindings.Add(new XRBinding("Text", DataSource, "Dongia", "{0:n0}"));          
            crThanhtien.DataBindings.Add(new XRBinding("Text", DataSource, "Nguyente", "{0:n0}"));

            crTienhang.DataBindings.Add(new XRBinding("Text", DataSource, "Tienhang", "{0:n0}"));
            crChietKhau.DataBindings.Add(new XRBinding("Text", DataSource, "TongTTCk", "{0:n0}"));
            crTongCong.DataBindings.Add(new XRBinding("Text", DataSource, "Thanhtoan", "{0:N0}")); 
        }

        private void grpLoai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.GetCurrentColumnValue("Loai").ToString() == "2")
            {
                grpLoai.Visible = true;
            }
            else {
                grpLoai.Visible = false;
            }
        }

        private void crGrpLoai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.GetCurrentColumnValue("Loai").ToString() == "2")
            {
                crGrpLoai.Text = "Sản phẩm tặng kèm";
                grpLoai.Visible = true;
            }
            else {
                grpLoai.Visible = false;
            }
        }
         
    }
}
