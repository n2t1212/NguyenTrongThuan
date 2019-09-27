using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTPosInMa
{
    public partial class frmPrintPreview : DevExpress.XtraEditors.XtraForm
    {
        public DevExpress.XtraReports.UI.XtraReport oReport;
        public frmPrintPreview()
        {
            InitializeComponent(); 
        }

        private void frmPrintPreview_Load(object sender, EventArgs e)
        {
            this.Text = "IN MÃ CODE";
            oPrintPreview.PrintingSystem = oReport.PrintingSystem;
            oPrintPreview.UpdatePageView();
        }


    }
}
