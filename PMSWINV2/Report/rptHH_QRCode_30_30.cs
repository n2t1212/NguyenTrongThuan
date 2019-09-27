using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
namespace MTPMSWIN.Report
{
    public partial class rptHH_QRCode_30_30 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHH_QRCode_30_30()
        {
            InitializeComponent();
        }

  
       public void InitMacode(bool isQrcode=false) {
           if (isQrcode)
           {
               xrMaCode.Symbology = new QRCodeGenerator();
               //xrMacode.Text = BarCodeText;
               xrMaCode.Width = 290;
               xrMaCode.Height = 290;
               xrMaCode.BackColor = System.Drawing.Color.White;
               xrMaCode.ForeColor = System.Drawing.Color.Green;
               
               xrMaCode.AutoModule = true;
               xrMaCode.Alignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
               ((QRCodeGenerator)xrMaCode.Symbology).CompactionMode = QRCodeCompactionMode.AlphaNumeric;
               ((QRCodeGenerator)xrMaCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
               ((QRCodeGenerator)xrMaCode.Symbology).Version = QRCodeVersion.AutoVersion;
               
           }
           else {               
               xrMaCode.Symbology = new Code128Generator();
               xrMaCode.Width =300;
               xrMaCode.Height = 150;
               xrMaCode.BackColor = System.Drawing.Color.White;
               xrMaCode.ForeColor = System.Drawing.Color.Green;
               xrMaCode.AutoModule = true;
               ((Code128Generator)xrMaCode.Symbology).CharacterSet = Code128Charset.CharsetB;
               this.Detail.HeightF = 160;
           }


        }
        public void BindData(){

            xrMaCode.DataBindings.Add(new XRBinding("Text", DataSource, "MaCode"));          
        }

        public XRBarCode CreateQRCodeBarCode(string BarCodeText)
        {
            // Create a bar code control. 
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to QRCode. 
            barCode.Symbology = new QRCodeGenerator();

            //Mavach

            // Adjust the bar code's main properties. 
            barCode.Text = BarCodeText;
            barCode.Width = 400;
            barCode.Height = 150;
            barCode.BackColor = System.Drawing.Color.Green;
            barCode.ForeColor = System.Drawing.Color.White;
            // If the AutoModule property is set to false, uncomment the next line. 
            barCode.AutoModule = true;
            // barcode.Module = 3; 
            // Adjust the properties specific to the bar code type. 
            ((QRCodeGenerator)barCode.Symbology).CompactionMode = QRCodeCompactionMode.AlphaNumeric;
            ((QRCodeGenerator)barCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            ((QRCodeGenerator)barCode.Symbology).Version = QRCodeVersion.AutoVersion;
            return barCode;
        }

        private void xrMaCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           // this.Detail.Controls.Add(
        }
 
    }
}
