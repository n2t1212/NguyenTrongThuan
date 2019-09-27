using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;

using MTPMSWIN.Report;
using MTPMSWIN.View;


namespace MTPMSWIN.Model
{
    public class MTBarCode
    {
        public MTBarCode() { }


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

        public void rptInMaVach(DataTable oTblSP,Boolean isDG=false,Boolean isMaQR=true) {
            try
            {
                if (oTblSP == null || oTblSP.Rows.Count <= 0) {
                    Utils.showMessage(String.Format("Bạn chưa chọn sản phẩm cần tạo mã {0}", isMaQR == true ? " QRCode.." : " Vạch.."), "Lưu ý");
                    return;
                }

                SqlParameter[] arrPara = new SqlParameter[5];
                arrPara[0] = new SqlParameter("@tblMasp", SqlDbType.Structured);
                arrPara[0].Value = oTblSP;
                arrPara[1]=new SqlParameter("@Loai",SqlDbType.TinyInt);
                arrPara[1].Value = isMaQR == true ? 0 : 1;
                arrPara[2] = new SqlParameter("@Cogia", SqlDbType.Bit);
                arrPara[2].Value = isDG == true ? 1 :0;
                arrPara[3] = new SqlParameter("@Denngay", SqlDbType.NVarChar,15);
                arrPara[3].Value = MTGlobal.MT_DENNGAY;
                arrPara[4] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
                arrPara[4].Value = MTGlobal.MT_USER_LOGIN;

                DataTable otblMaCode = new MTSQLServer().wRead("rptDM_InMaQRCode", arrPara);
                if (otblMaCode != null) {
                    rptHH_QRCode_30_30 oReport = new rptHH_QRCode_30_30();
                    oReport.DataSource = otblMaCode;
                    oReport.BindData();

                    //setParameterInfo(oReport);
                    //setFormatReport(oReport);
                    //SetMarginReport(oReport, false, 30, 30, 30, 25);

                    PrintPreview oPreview = new PrintPreview();
                    oPreview.report = oReport; 
                    oPreview.ShowDialog();
                }
            }
            catch { }
        }
    }
}
