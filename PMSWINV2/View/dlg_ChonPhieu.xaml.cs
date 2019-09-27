using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using System.Data;
using System.Data.SqlClient;

using MTPMSWIN.Model;


namespace MTPMSWIN.View
{
 
    public partial class dlg_ChonPhieu : Window
    {
        public bool isChon = false;
        private DataTable otblSrc = null;
        public DataTable otblChon = null;
        private String mIsType = "";
        private String sTuNgay = "";
        private String sDenNgay = "";
        public dlg_ChonPhieu(String mType = "PN", String mTungay = "", String mDenNgay = "")
        {
            InitializeComponent();             
            this.mIsType = mType;
            sTuNgay = mTungay == "" ? MTGlobal.MT_TUNGAY : mTungay;
            sDenNgay = mDenNgay == "" ? MTGlobal.MT_DENNGAY : mDenNgay;

            MTGlobal.SetFormatGridControl(grdDSPhieu, tblDSPhieu);
            MTGlobal.SetFormatGridControl(grdDSPhieuChon, tblDSPhieuChon);
            tblDSPhieu.MultiSelectMode = DevExpress.Xpf.Grid.TableViewSelectMode.Row;
            tblDSPhieuChon.MultiSelectMode = DevExpress.Xpf.Grid.TableViewSelectMode.Row;
            setDataSource();
           
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void setDataSource() {
            try{

                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0] = new SqlParameter("@LOAIP", SqlDbType.NVarChar,5);
                arrPara[0].Value = mIsType;
                arrPara[1] = new SqlParameter("@TUNGAY", SqlDbType.NVarChar, 15);
                arrPara[1].Value =sTuNgay;
                arrPara[2] = new SqlParameter("@DENNGAY", SqlDbType.NVarChar, 15);
                arrPara[2].Value = sDenNgay;
                arrPara[3] = new SqlParameter("@NGUOIDUNG", SqlDbType.NVarChar, 50);
                arrPara[3].Value = MTGlobal.MT_USER_LOGIN;
                otblSrc = new MTSQLServer().wRead("mt_ChonPhieu", arrPara);
                if (otblSrc == null) {
                    Utils.showMessage("Không thể đọc dữ liệu cần chọn...", "Thông báo");
                    return;
                }
                otblChon = otblSrc.Clone();

                grdDSPhieu.ItemsSource = this.otblSrc;
                grdDSPhieuChon.ItemsSource = this.otblChon;

                switch (this.mIsType) { 
                    case "SP":
                        colID.FieldName = "Maspid";
                        colMaso.FieldName = "Masp";
                        colMaso.Header = "Mã SP";
                        colDiengiai.FieldName = "Tensp";
                        colDiengiai.Header = "Sản Phẩm";
                        colHide1.FieldName = "Dvt";
                        colHide1.Header = "ĐVT";
                        colHide2.FieldName = "Quycach";
                        colHide2.Header = "Qui Cách";

                        colIDC.FieldName = "Maspid";
                        colMasoC.FieldName = "Masp";
                        colMasoC.Header = "Mã SP";
                        colDiengiaiC.FieldName = "Tensp";
                        colDiengiaiC.Header = "Sản Phẩm";
                        colHide1C.FieldName = "Dvt";
                        colHide1C.Header = "ĐVT";
                        colHide2C.FieldName = "Quycach";
                        colHide2C.Header = "Qui Cách";

                        colHide1.Visible = true;
                        colHide2.Visible = true;
                        colHide1C.Visible = true;
                        colHide2C.Visible = true;
                        break;

                    case "PN":
                    case "PX":
                        colID.FieldName = "Phieunxid";
                        colMaso.FieldName = "Sophieu";
                        colMaso.Header = "Số Phiếu";
                        colDiengiai.FieldName = "Ngayps";
                        colDiengiai.Header = "Ngày PS";
                        colHide1.FieldName = "Ghichu";
                        colHide1.Header = "Ghi chú";
                        colHide1.Visible = true;
                        colDiengiai.Width = 120;

                        colIDC.FieldName = "Phieunxid";
                        colMasoC.FieldName = "Sophieu";
                        colMasoC.Header = "Số Phiếu";
                        colDiengiaiC.FieldName = "Ngayps";
                        colDiengiaiC.Header = "Ngày PS";
                        colHide1C.FieldName = "Ghichu";
                        colHide1C.Header = "Ghi chú";
                        colHide1C.Visible = true;
                        colDiengiaiC.Width = 120;
                        break;

                    case "CX":
                        colID.FieldName = "Chanhxeid";
                        colMaso.FieldName = "Sophieu";
                        colMaso.Header = "Số Phiếu";
                        colDiengiai.FieldName = "Ngayps";
                        colDiengiai.Header = "Ngày PS";
                        colHide1.FieldName = "Ghichu";
                        colHide1.Header = "Ghi chú";
                        colHide1.Visible = true;
                        colDiengiai.Width = 120;

                        colIDC.FieldName = "Chanhxeid";
                        colMasoC.FieldName = "Sophieu";
                        colMasoC.Header = "Số Phiếu";
                        colDiengiaiC.FieldName = "Ngayps";
                        colDiengiaiC.Header = "Ngày PS";
                        colHide1C.FieldName = "Ghichu";
                        colHide1C.Header = "Ghi chú";
                        colHide1C.Visible = true;
                        colDiengiaiC.Width = 120;
                        break;

                    default:
                        break;               

                } 
            }
            catch (Exception ex) { }
        }
       


        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            try{                 
                otblChon.AcceptChanges();
                isChon = true;
                this.Close();
            }
            catch { }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            isChon = false;
            this.Close();
        }

        private void cmdExitApp_Click(object sender, RoutedEventArgs e)
        {
            isChon = false;
            this.Close();
        }

        private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
 

        private void tblDSPhieuChon_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

        }

        private void tblDSPhieu_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRow vR = null;
                for (int i = 0; i < grdDSPhieu.VisibleRowCount; i++)
                {
                    if (tblDSPhieu.IsRowSelected(i))
                    {
                        switch (this.mIsType)
                        {
                            case "SP":
                                vR = otblChon.NewRow();
                                vR["Maspid"] = grdDSPhieu.GetCellValue(i,colID);
                                vR["Masp"] = grdDSPhieu.GetCellValue(i, colMaso);
                                vR["Tensp"] = grdDSPhieu.GetCellValue(i, colDiengiai);
                                vR["Dvt"] = grdDSPhieu.GetCellValue(i, colHide1);
                                vR["Quycach"] = grdDSPhieu.GetCellValue(i, colHide2); 
                                otblChon.Rows.Add(vR);
                                tblDSPhieu.DeleteRow(i);
                                break;

                            case "PN":
                            case "PX":
                                vR = otblChon.NewRow();
                                vR["Phieunxid"] = grdDSPhieu.GetCellValue(i, colIDC);
                                vR["Sophieu"] = grdDSPhieu.GetCellValue(i, colMasoC);
                                vR["Ngayps"] = grdDSPhieu.GetCellValue(i, colDiengiaiC);
                                vR["Ghichu"] = grdDSPhieu.GetCellValue(i, colHide1C);
                                otblChon.Rows.Add(vR);

                                tblDSPhieu.DeleteRow(i);
                                break;

                            case "CX":
                                vR = otblChon.NewRow();
                                vR["Chanhxeid"] = grdDSPhieu.GetCellValue(i, colIDC);
                                vR["Sophieu"] = grdDSPhieu.GetCellValue(i, colMasoC);
                                vR["Ngayps"] = grdDSPhieu.GetCellValue(i, colDiengiaiC);
                                vR["Ghichu"] = grdDSPhieu.GetCellValue(i, colHide1C);
                                otblChon.Rows.Add(vR);

                                tblDSPhieu.DeleteRow(i);
                                break; 

                        }                       
                    }
                }

                if (grdDSPhieu.VisibleRowCount > 0)
                {
                    tblDSPhieu.SelectRow(0);
                }
                otblChon.AcceptChanges();
                grdDSPhieuChon.ItemsSource = otblChon;
                otblSrc.AcceptChanges();
                grdDSPhieu.RefreshData();
            }
            catch (Exception ex) { }
        }


        private void cmdAddAll_Click(object sender, RoutedEventArgs e)
        {
            try{
                DataRow vR = null;
                if (otblSrc != null && otblSrc.Rows.Count > 0)
                {
                    foreach (DataRow vRow in otblSrc.Rows)
                    {
                        switch (this.mIsType)
                        {
                            case "SP":
                                vR = otblChon.NewRow();
                                vR["Maspid"] = vRow["Maspid"];
                                vR["Masp"] = vRow["Masp"];
                                vR["Tensp"] = vRow["Tensp"];
                                vR["Dvt"] = vRow["Dvt"];
                                vR["Quycach"] = vRow["Quycach"];
                                otblChon.Rows.Add(vR);
                                break;

                            case "PN":
                            case "PX":
                                vR = otblChon.NewRow();
                                vR["Phieunxid"] = vRow["Phieunxid"];
                                vR["Sophieu"] = vRow["Sophieu"];
                                vR["Ngayps"] = vRow["Ngayps"];
                                vR["Ghichu"] = vRow["Ghichu"];
                                otblChon.Rows.Add(vR);
                                break;

                            case "CX":
                                vR = otblChon.NewRow();
                                vR["Chanhxeid"] = vRow["Chanhxeid"];
                                vR["Sophieu"] = vRow["Sophieu"];
                                vR["Ngayps"] = vRow["Ngayps"];
                                vR["Ghichu"] = vRow["Ghichu"];
                                otblChon.Rows.Add(vR);
                                break;

                        } 
                    }
                    otblChon.AcceptChanges();
                    otblSrc.Rows.Clear();
                    grdDSPhieu.RefreshData();
                    grdDSPhieuChon.RefreshData();
                }
            }
            catch { }
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRow vR = null;
                for (int i = 0; i < grdDSPhieuChon.VisibleRowCount; i++)
                {
                    if (tblDSPhieuChon.IsRowSelected(i))
                    {
                        switch (this.mIsType) { 
                            case "SP":
                                    vR = otblSrc.NewRow();
                                    vR["Maspid"] = grdDSPhieuChon.GetCellValue(i,colIDC);
                                    vR["Masp"] = grdDSPhieuChon.GetCellValue(i, colMasoC);
                                    vR["Tensp"] = grdDSPhieuChon.GetCellValue(i, colDiengiaiC);
                                    vR["Dvt"] = grdDSPhieuChon.GetCellValue(i, colHide1C);
                                    vR["Quycach"] = grdDSPhieuChon.GetCellValue(i, colHide2C);
                                    otblSrc.Rows.Add(vR);

                                    tblDSPhieuChon.DeleteRow(i);
                                   break;

                            case "PN":
                            case "PX":
                                    vR = otblSrc.NewRow();
                                    vR["Phieunxid"] = grdDSPhieuChon.GetCellValue(i, colIDC);
                                    vR["Sophieu"] = grdDSPhieuChon.GetCellValue(i, colMasoC);
                                    vR["Ngayps"] = grdDSPhieuChon.GetCellValue(i, colDiengiaiC);
                                    vR["Ghichu"] = grdDSPhieuChon.GetCellValue(i, colHide1C); 
                                    otblSrc.Rows.Add(vR);

                                    tblDSPhieuChon.DeleteRow(i);
                                   break;

                            case "CX":
                                    vR = otblSrc.NewRow();
                                    vR["Chanhxeid"] = grdDSPhieuChon.GetCellValue(i, colIDC);
                                    vR["Sophieu"] = grdDSPhieuChon.GetCellValue(i, colMasoC);
                                    vR["Ngayps"] = grdDSPhieuChon.GetCellValue(i, colDiengiaiC);
                                    vR["Ghichu"] = grdDSPhieuChon.GetCellValue(i, colHide1C); 
                                    otblSrc.Rows.Add(vR);

                                    tblDSPhieuChon.DeleteRow(i);
                                   break;
                        }
                      
                    }
                }
                if (grdDSPhieuChon.VisibleRowCount > 0)
                {
                    tblDSPhieuChon.SelectRow(0);
                }
                otblChon.AcceptChanges();
                otblSrc.AcceptChanges();
                grdDSPhieu.ItemsSource = otblSrc;
            }
            catch (Exception ex) { }
        }

        private void cmdRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRow vR = null;
                if (otblChon != null && otblChon.Rows.Count > 0)
                {
                    foreach (DataRow vRow in otblChon.Rows)
                    {
                        switch (this.mIsType) { 
                            case "SP":
                                    vR = otblSrc.NewRow();
                                    vR["Maspid"] = vRow["Maspid"];
                                    vR["Masp"] = vRow["Masp"];
                                    vR["Tensp"] = vRow["Tensp"];
                                    vR["Dvt"] = vRow["Dvt"];
                                    vR["Quycach"] = vRow["Quycach"];
                                    otblSrc.Rows.Add(vR);
                                break;

                            case "PN":
                            case "PX":
                                    vR = otblSrc.NewRow();
                                    vR["Phieunxid"] = vRow["Phieunxid"];
                                    vR["Sophieu"] = vRow["Sophieu"];
                                    vR["Ngayps"] = vRow["Ngayps"];
                                    vR["Ghichu"] = vRow["Ghichu"]; 
                                    otblSrc.Rows.Add(vR);
                                break;

                            case "CX":
                                    vR = otblSrc.NewRow();
                                    vR["Chanhxeid"] = vRow["Chanhxeid"];
                                    vR["Sophieu"] = vRow["Sophieu"];
                                    vR["Ngayps"] = vRow["Ngayps"];
                                    vR["Ghichu"] = vRow["Ghichu"]; 
                                    otblSrc.Rows.Add(vR);
                                break;
                        }           
                    }
                    otblSrc.AcceptChanges(); 
                    otblChon.AcceptChanges();
                    otblChon.Rows.Clear();
                    otblChon.AcceptChanges(); 
                    grdDSPhieu.ItemsSource = otblSrc;
                }
            }
            catch (Exception ex) { }
        }

      
    }
}
