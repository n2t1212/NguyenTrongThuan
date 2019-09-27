using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

using MTPosInMa.DataAccess;

namespace MTPosInMa.Model
{
    class modReport
    {


        private void setFormatReport(DevExpress.XtraReports.UI.XtraReport oReport)
        {
            oReport.RequestParameters = false;
            oReport.CreateDocument(false);
            oReport.PrintingSystem.ShowMarginsWarning = false;
        }

        private void SetCustomPageQR(DevExpress.XtraReports.UI.XtraReport oReport, String isMauIn = "25_15",bool isQR=false)
        {
            switch (isMauIn){
                case "25_15":
                    oReport.ReportUnit = global::DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
                    oReport.PaperKind = System.Drawing.Printing.PaperKind.Custom;
                    oReport.PageSize = new System.Drawing.Size(1050, 150);
                    oReport.DefaultPrinterSettingsUsing.UsePaperKind = false;
                    oReport.DefaultPrinterSettingsUsing.UseLandscape = false;
                    oReport.DefaultPrinterSettingsUsing.UseMargins = false;
                    if (isQR)
                    {
                        oReport.PrintingSystem.PageSettings.LeftMargin = SysPar.MT_QR_MARGIN_LEFT;
                        oReport.PrintingSystem.PageSettings.RightMargin =SysPar.MT_QR_MARGIN_RIGHT;
                        oReport.PrintingSystem.PageSettings.TopMargin = SysPar.MT_QR_MARGIN_TOP;
                        oReport.PrintingSystem.PageSettings.BottomMargin = SysPar.MT_QR_MARGIN_BOTTON;
                    }
                    else {
                        oReport.PrintingSystem.PageSettings.LeftMargin = SysPar.MT_BC_MARGIN_LEFT;
                        oReport.PrintingSystem.PageSettings.RightMargin = SysPar.MT_BC_MARGIN_RIGHT;
                        oReport.PrintingSystem.PageSettings.TopMargin = SysPar.MT_BC_MARGIN_TOP;
                        oReport.PrintingSystem.PageSettings.BottomMargin = SysPar.MT_BC_MARGIN_BOTTON;
                    }
                    break;

                case "35_22":
                    oReport.ReportUnit = global::DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
                    oReport.PaperKind = System.Drawing.Printing.PaperKind.Custom; 
                    oReport.PageSize = new System.Drawing.Size(1050, 220);
                    oReport.DefaultPrinterSettingsUsing.UseLandscape = false;
                    oReport.DefaultPrinterSettingsUsing.UseMargins = false;
                    oReport.DefaultPrinterSettingsUsing.UsePaperKind = false;

                    if (isQR)
                    {
                        oReport.PrintingSystem.PageSettings.LeftMargin = SysPar.MT_QR_MARGIN_LEFT;
                        oReport.PrintingSystem.PageSettings.RightMargin = SysPar.MT_QR_MARGIN_RIGHT;
                        oReport.PrintingSystem.PageSettings.TopMargin = SysPar.MT_QR_MARGIN_TOP;
                        oReport.PrintingSystem.PageSettings.BottomMargin = SysPar.MT_QR_MARGIN_BOTTON;
                    }
                    else
                    {
                        oReport.PrintingSystem.PageSettings.LeftMargin = SysPar.MT_BC_MARGIN_LEFT;
                        oReport.PrintingSystem.PageSettings.RightMargin = SysPar.MT_BC_MARGIN_RIGHT;
                        oReport.PrintingSystem.PageSettings.TopMargin = SysPar.MT_BC_MARGIN_TOP;
                        oReport.PrintingSystem.PageSettings.BottomMargin = SysPar.MT_BC_MARGIN_BOTTON;
                    }
                    oReport.RequestParameters = false;
                    break;

                case "30_30":
                    oReport.ReportUnit = global::DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
                    oReport.PaperKind = System.Drawing.Printing.PaperKind.Custom;
                    oReport.PageSize = new System.Drawing.Size(945, 312);
                    oReport.DefaultPrinterSettingsUsing.UseLandscape = false;
                    oReport.DefaultPrinterSettingsUsing.UseMargins = false;
                    oReport.DefaultPrinterSettingsUsing.UsePaperKind = false;
                    oReport.RequestParameters = false;

                    if (isQR)
                    {
                        oReport.PrintingSystem.PageSettings.LeftMargin = SysPar.MT_QR_MARGIN_LEFT;
                        oReport.PrintingSystem.PageSettings.RightMargin = SysPar.MT_QR_MARGIN_RIGHT;
                        oReport.PrintingSystem.PageSettings.TopMargin = SysPar.MT_QR_MARGIN_TOP;
                        oReport.PrintingSystem.PageSettings.BottomMargin = SysPar.MT_QR_MARGIN_BOTTON;
                    }
                    else
                    {
                        oReport.PrintingSystem.PageSettings.LeftMargin = SysPar.MT_BC_MARGIN_LEFT;
                        oReport.PrintingSystem.PageSettings.RightMargin = SysPar.MT_BC_MARGIN_RIGHT;
                        oReport.PrintingSystem.PageSettings.TopMargin = SysPar.MT_BC_MARGIN_TOP;
                        oReport.PrintingSystem.PageSettings.BottomMargin = SysPar.MT_BC_MARGIN_BOTTON;
                    }
                    break;
            }
        }

        public void rptInMaVach(DataTable oTblSP, Boolean isDG = false, Boolean isMaQR = true,Boolean isThung=false, String isMauIn = "25_15"){
           // DevExpress.Utils.WaitDialogForm oW = new DevExpress.Utils.WaitDialogForm("Đang tạo báo cáo..", "In mã code");
         
            try{
                if (oTblSP == null || oTblSP.Rows.Count <= 0)
                {
                   MessageBox.Show(String.Format("Bạn chưa chọn sản phẩm cần tạo mã {0}", isMaQR == true ? " QRCode.." : " Vạch.."), "Lưu ý",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                //oW.Show();

                SqlParameter[] arrPara = new SqlParameter[6];
                arrPara[0] = new SqlParameter("@tblMasp", SqlDbType.Structured);
                arrPara[0].Value = oTblSP;
                arrPara[1] = new SqlParameter("@Loai", SqlDbType.TinyInt);
                arrPara[1].Value = isMaQR == true ? 0 : 1;
                arrPara[2] = new SqlParameter("@Cogia", SqlDbType.Bit);
                arrPara[2].Value = isDG == true ? 1 : 0;
                arrPara[3] = new SqlParameter("@Thung", SqlDbType.Bit);
                arrPara[3].Value = isThung == true ? 1 : 0;
                arrPara[4] = new SqlParameter("@Denngay", SqlDbType.NVarChar, 15);
                arrPara[4].Value = DateTime.Now.ToShortDateString();
                arrPara[5] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
                arrPara[5].Value ="Admin";

                DataTable otblMaCode = new SqlServer().wRead("rptDM_InMaQRCode", arrPara);
                
                if (otblMaCode != null)
                {
                    switch (isMauIn)
                    {
                        case "25_15":
                            if (isMaQR){
                                rptQRCode2515 oRptQR25 = new rptQRCode2515();
                                oRptQR25.InitMacode(isMaQR,isDG);
                                oRptQR25.DataSource = otblMaCode;
                                oRptQR25.BindData();
                               
                                SetCustomPageQR(oRptQR25, isMauIn);
                                setFormatReport(oRptQR25);
                                frmPrintPreview oPr = new frmPrintPreview();
                                oPr.oReport = oRptQR25;
                                oPr.ShowDialog();
                            }
                            else {
                                rptBarCode2515 oRptBar25 = new rptBarCode2515();
                                oRptBar25.InitMacode(isMaQR,isDG);
                                oRptBar25.DataSource = otblMaCode;
                                oRptBar25.BindData();
                                SetCustomPageQR(oRptBar25, isMauIn);
                                setFormatReport(oRptBar25);
                                frmPrintPreview oPr = new frmPrintPreview();
                                oPr.oReport = oRptBar25;
                                oPr.ShowDialog();
                            }
                            break;

                        //3TEM/ROW
                        case "35_22": 

                            if (isMaQR){
                                rptQRCode3522 oRptQR35 = new rptQRCode3522();
                                oRptQR35.InitMacode(isMaQR,isDG);
                                oRptQR35.DataSource = otblMaCode;
                                oRptQR35.BindData();
                                SetCustomPageQR(oRptQR35, isMauIn);
                                setFormatReport(oRptQR35);
                                frmPrintPreview oPr = new frmPrintPreview();
                                oPr.oReport = oRptQR35;
                                oPr.ShowDialog();
                            }
                            else
                            {
                                rptBarCode3522 oRptBar35 = new rptBarCode3522();
                                oRptBar35.InitMacode(isMaQR,isDG);
                                oRptBar35.DataSource = otblMaCode;
                                oRptBar35.BindData();
                                SetCustomPageQR(oRptBar35, isMauIn);
                                setFormatReport(oRptBar35);
                                frmPrintPreview oPr = new frmPrintPreview();
                                oPr.oReport = oRptBar35;
                                oPr.ShowDialog();
                            }

                            break; 
                    }

                }
                else
                {
                    MessageBox.Show(String.Format("Không thể tạo mã {0}", isMaQR == true ? " QRCode.." : " Vạch.."), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //oW.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString());}
        }

    }
}
