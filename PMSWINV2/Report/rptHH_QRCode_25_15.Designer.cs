namespace MTPMSWIN.Report
{
    partial class rptHH_QRCode_25_15
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.crBarcodeText = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMaCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.parThangNam = new DevExpress.XtraReports.Parameters.Parameter();
            this.parTax = new DevExpress.XtraReports.Parameters.Parameter();
            this.parCompany = new DevExpress.XtraReports.Parameters.Parameter();
            this.parAddress = new DevExpress.XtraReports.Parameters.Parameter();
            this.parTel = new DevExpress.XtraReports.Parameters.Parameter();
            this.parNguoiLap = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.crBarcodeText,
            this.xrMaCode});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 133.9375F;
            this.Detail.MultiColumn.ColumnCount = 4;
            this.Detail.MultiColumn.ColumnWidth = 245F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UsePadding = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // crBarcodeText
            // 
            this.crBarcodeText.Dpi = 254F;
            this.crBarcodeText.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.crBarcodeText.LocationFloat = new DevExpress.Utils.PointFloat(11F, 111.9375F);
            this.crBarcodeText.Name = "crBarcodeText";
            this.crBarcodeText.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.crBarcodeText.SizeF = new System.Drawing.SizeF(230F, 20F);
            this.crBarcodeText.StylePriority.UseFont = false;
            this.crBarcodeText.StylePriority.UsePadding = false;
            this.crBarcodeText.StylePriority.UseTextAlignment = false;
            this.crBarcodeText.Text = "AAAA";
            this.crBarcodeText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrMaCode
            // 
            this.xrMaCode.AutoModule = true;
            this.xrMaCode.Dpi = 254F;
            this.xrMaCode.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrMaCode.LocationFloat = new DevExpress.Utils.PointFloat(12F, 6F);
            this.xrMaCode.Module = 5.08F;
            this.xrMaCode.Name = "xrMaCode";
            this.xrMaCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrMaCode.ShowText = false;
            this.xrMaCode.SizeF = new System.Drawing.SizeF(230F, 100F);
            this.xrMaCode.StylePriority.UseFont = false;
            this.xrMaCode.StylePriority.UsePadding = false;
            this.xrMaCode.StylePriority.UseTextAlignment = false;
            code128Generator1.CharacterSet = DevExpress.XtraPrinting.BarCode.Code128Charset.CharsetB;
            this.xrMaCode.Symbology = code128Generator1;
            this.xrMaCode.Text = "AAAAA";
            this.xrMaCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            this.xrMaCode.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrMaCode_BeforePrint);
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // parThangNam
            // 
            this.parThangNam.Description = "Tháng Năm";
            this.parThangNam.Name = "parThangNam";
            // 
            // parTax
            // 
            this.parTax.Description = "Mã Số Thuế";
            this.parTax.Name = "parTax";
            this.parTax.ValueInfo = "0343434343";
            // 
            // parCompany
            // 
            this.parCompany.Description = "Tên Công Ty";
            this.parCompany.Name = "parCompany";
            this.parCompany.ValueInfo = "CÔNG TY TNHH ABC";
            // 
            // parAddress
            // 
            this.parAddress.Description = "Địa chỉ Công Ty";
            this.parAddress.Name = "parAddress";
            this.parAddress.ValueInfo = "Số 1 Đường Cộng Hòa, F.13, Q.Tân Bình, TP.HCM";
            // 
            // parTel
            // 
            this.parTel.Description = "Điện Thoại";
            this.parTel.Name = "parTel";
            this.parTel.ValueInfo = "(028) 03434923";
            // 
            // parNguoiLap
            // 
            this.parNguoiLap.Description = "Người Lập";
            this.parNguoiLap.Name = "parNguoiLap";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 0F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.Visible = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 0F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.Visible = false;
            // 
            // rptHH_QRCode_25_15
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 160;
            this.PageWidth = 1000;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parCompany,
            this.parAddress,
            this.parTax,
            this.parTel,
            this.parNguoiLap,
            this.parThangNam});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.RequestParameters = false;
            this.SnapGridSize = 31.75F;
            this.Version = "12.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rptHH_QRCode_25_15_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.Parameters.Parameter parTax;
        private DevExpress.XtraReports.Parameters.Parameter parCompany;
        private DevExpress.XtraReports.Parameters.Parameter parAddress;
        private DevExpress.XtraReports.Parameters.Parameter parTel;
        private DevExpress.XtraReports.Parameters.Parameter parNguoiLap;
        private DevExpress.XtraReports.Parameters.Parameter parThangNam;
        private DevExpress.XtraReports.UI.XRBarCode xrMaCode;
        private DevExpress.XtraReports.UI.XRLabel crBarcodeText;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
    }
}
