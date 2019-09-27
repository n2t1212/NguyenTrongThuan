namespace MTPosInMa
{
    partial class rptBarCode2515
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
            DevExpress.XtraPrinting.BarCode.EAN128Generator eaN128Generator1 = new DevExpress.XtraPrinting.BarCode.EAN128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.crBarcodePrice = new DevExpress.XtraReports.UI.XRLabel();
            this.crBarcodeText = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMaCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.crBarcodePrice,
            this.crBarcodeText,
            this.xrMaCode});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 140F;
            this.Detail.MultiColumn.ColumnCount = 4;
            this.Detail.MultiColumn.ColumnSpacing = 5F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // crBarcodePrice
            // 
            this.crBarcodePrice.Dpi = 254F;
            this.crBarcodePrice.Font = new System.Drawing.Font("Tahoma", 5F);
            this.crBarcodePrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.crBarcodePrice.LocationFloat = new DevExpress.Utils.PointFloat(11.30286F, 6F);
            this.crBarcodePrice.Name = "crBarcodePrice";
            this.crBarcodePrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 5, 0, 0, 254F);
            this.crBarcodePrice.SizeF = new System.Drawing.SizeF(218.2615F, 21.00378F);
            this.crBarcodePrice.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.crBarcodePrice.StylePriority.UseFont = false;
            this.crBarcodePrice.StylePriority.UseForeColor = false;
            this.crBarcodePrice.StylePriority.UsePadding = false;
            this.crBarcodePrice.StylePriority.UseTextAlignment = false;
            this.crBarcodePrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.crBarcodePrice.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.crBarcodePrice_BeforePrint);
            // 
            // crBarcodeText
            // 
            this.crBarcodeText.Dpi = 254F;
            this.crBarcodeText.Font = new System.Drawing.Font("Tahoma", 5.5F);
            this.crBarcodeText.LocationFloat = new DevExpress.Utils.PointFloat(12.30286F, 102F);
            this.crBarcodeText.Name = "crBarcodeText";
            this.crBarcodeText.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.crBarcodeText.SizeF = new System.Drawing.SizeF(218F, 19.80113F);
            this.crBarcodeText.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.crBarcodeText.StylePriority.UseFont = false;
            this.crBarcodeText.StylePriority.UsePadding = false;
            this.crBarcodeText.StylePriority.UseTextAlignment = false;
            this.crBarcodeText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.crBarcodeText.Visible = false;
            this.crBarcodeText.WordWrap = false;
            // 
            // xrMaCode
            // 
            this.xrMaCode.AutoModule = true;
            this.xrMaCode.Dpi = 254F;
            this.xrMaCode.LocationFloat = new DevExpress.Utils.PointFloat(12.30287F, 27.00376F);
            this.xrMaCode.Module = 5.08F;
            this.xrMaCode.Name = "xrMaCode";
            this.xrMaCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrMaCode.ShowText = false;
            this.xrMaCode.SizeF = new System.Drawing.SizeF(218.2615F, 94.79735F);
            this.xrMaCode.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.xrMaCode.StylePriority.UsePadding = false;
            this.xrMaCode.StylePriority.UseTextAlignment = false;
            eaN128Generator1.CharacterSet = DevExpress.XtraPrinting.BarCode.Code128Charset.CharsetB;
            this.xrMaCode.Symbology = eaN128Generator1;
            this.xrMaCode.Text = "AAAA";
            this.xrMaCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 5F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 5F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rptBarCode2515
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(30, 30, 5, 5);
            this.PageHeight = 150;
            this.PageWidth = 1050;
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
        private DevExpress.XtraReports.UI.XRBarCode xrMaCode;
        private DevExpress.XtraReports.UI.XRLabel crBarcodeText;
        private DevExpress.XtraReports.UI.XRLabel crBarcodePrice;
    }
}
