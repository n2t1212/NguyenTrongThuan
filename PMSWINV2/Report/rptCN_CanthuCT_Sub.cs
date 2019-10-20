using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MTPMSWIN.Report
{
    public partial class rptCN_CanthuCT_Sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCN_CanthuCT_Sub()
        {
            InitializeComponent();
        }
        public void BindData(){           
            crNgay.DataBindings.Add(new XRBinding("Text", DataSource, "NgayPS","{0:dd/MM/yyyy}"));
            crDiengiai.DataBindings.Add(new XRBinding("Text", DataSource, "Diengiai"));
            crSotien.DataBindings.Add(new XRBinding("Text", DataSource, "Sotien", "{0:n0}")); 
        }         
        private void crTheoCT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.MTGlobal.BlankZero(sender);
        }   
    }
}
