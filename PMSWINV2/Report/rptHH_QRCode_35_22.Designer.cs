namespace MTPMSWIN.Report
{
    partial class rptHH_QRCode_35_22
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator1 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.crPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.crBarcodeText = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMaCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.crPrice,
            this.crBarcodeText,
            this.xrMaCode});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 150F;
            this.Detail.KeepTogetherWithDetailReports = true;
            this.Detail.MultiColumn.ColumnCount = 3;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // crPrice
            // 
            this.crPrice.Angle = 90F;
            this.crPrice.Dpi = 254F;
            this.crPrice.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.crPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.crPrice.LocationFloat = new DevExpress.Utils.PointFloat(129.05F, 18F);
            this.crPrice.Name = "crPrice";
            this.crPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.crPrice.SizeF = new System.Drawing.SizeF(34.8875F, 86F);
            this.crPrice.StylePriority.UseFont = false;
            this.crPrice.StylePriority.UseForeColor = false;
            this.crPrice.StylePriority.UsePadding = false;
            this.crPrice.StylePriority.UseTextAlignment = false;
            this.crPrice.Text = "250.000";
            this.crPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // crBarcodeText
            // 
            this.crBarcodeText.Angle = 90F;
            this.crBarcodeText.Dpi = 254F;
            this.crBarcodeText.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.crBarcodeText.LocationFloat = new DevExpress.Utils.PointFloat(180.8708F, 19.51671F);
            this.crBarcodeText.Name = "crBarcodeText";
            this.crBarcodeText.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.crBarcodeText.SizeF = new System.Drawing.SizeF(40.70828F, 84.4833F);
            this.crBarcodeText.StylePriority.UseFont = false;
            this.crBarcodeText.StylePriority.UsePadding = false;
            this.crBarcodeText.StylePriority.UseTextAlignment = false;
            this.crBarcodeText.Text = "AAAAA";
            this.crBarcodeText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrMaCode
            // 
            this.xrMaCode.AutoModule = true;
            this.xrMaCode.Dpi = 254F;
            this.xrMaCode.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrMaCode.LocationFloat = new DevExpress.Utils.PointFloat(24.99999F, 18F);
            this.xrMaCode.Module = 5.08F;
            this.xrMaCode.Name = "xrMaCode";
            this.xrMaCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 1, 254F);
            this.xrMaCode.ShowText = false;
            this.xrMaCode.SizeF = new System.Drawing.SizeF(89.41666F, 86F);
            this.xrMaCode.StylePriority.UseFont = false;
            this.xrMaCode.StylePriority.UsePadding = false;
            this.xrMaCode.StylePriority.UseTextAlignment = false;
            qrCodeGenerator1.Version = DevExpress.XtraPrinting.BarCode.QRCodeVersion.Version1;
            this.xrMaCode.Symbology = qrCodeGenerator1;
            this.xrMaCode.Text = "AAAAA";
            this.xrMaCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 33F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 25F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 0F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 254F);
            this.ReportHeader.Visible = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 0F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 254F);
            this.ReportFooter.Visible = false;
            // 
            // rptHH_QRCode_35_22
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(36, 38, 33, 25);
            this.PageHeight = 150;
            this.PageWidth = 1001;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRBarCode xrMaCode;
        private DevExpress.XtraReports.UI.XRLabel crBarcodeText;
        private DevExpress.XtraReports.UI.XRLabel crPrice;
    }
}
