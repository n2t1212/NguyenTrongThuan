using System;
using System.Collections.Generic;
using System.Data; 
using System.Linq;
using System.Text;
using System.Windows; 
using System.Windows.Shapes;
using MTPMSWIN.Model;

using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;

namespace MTPMSWIN.View
{
 
    public partial class PrintPreview : Window
    {
        public DevExpress.XtraReports.UI.XtraReport report;

        public PrintPreview()
        {
           // DevExpress.Xpf.Core.ThemeManager.SetThemeName(this, "Office2007Blue");
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            XtraReportPreviewModel model = new XtraReportPreviewModel(report);         
            model.AutoShowParametersPanel = false;
            oDocPreview.Model = model;

            //XtraPageSettingsBase oPage = report.PrintingSystem.PageSettings;            
            //oPage.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            //oPage = report.PrintingSystem.PageSettings;             
            //model.Report.ApplyPageSettings(oPage); 
            //report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.SendFile, CommandVisibility.None); 
            //report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Watermark, CommandVisibility.None); 
            //report.CreateDocument();
 

            //report.CreateDocument(false); 
            //DocumentPreviewWindow oPrev = new DocumentPreviewWindow() { Model = model };            
            //report.CreateDocument(false);            
            //oPrev.ShowDialog(); 
            //report.CreateDocument(true); 
        }

        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
       
    }
}
