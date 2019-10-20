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
 
    public partial class dlg_ChonKhachHang : Window
    {
        public bool isChon = false;
        private DataTable otblSrc = null;
        public DataTable otblChon = null;
        private String mIsType = "";
        private String sTuNgay = "";
        private String sDenNgay = "";
        public dlg_ChonKhachHang(String mType = "PN", String mTungay = "", String mDenNgay = "")
        {
            InitializeComponent();             
            this.mIsType = mType;
            sTuNgay = mTungay == "" ? MTGlobal.MT_TUNGAY : mTungay;
            sDenNgay = mDenNgay == "" ? MTGlobal.MT_DENNGAY : mDenNgay;

            MTGlobal.SetFormatGridControl(grdDS, tblDS);
            MTGlobal.SetFormatGridControl(grdDSChon, tblDSChon);
            tblDS.MultiSelectMode = DevExpress.Xpf.Grid.TableViewSelectMode.Row;
            tblDSChon.MultiSelectMode = DevExpress.Xpf.Grid.TableViewSelectMode.Row;
            setDataSource();
           
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void setDataSource() {
            try{                
                String mSql = String.Format("SELECT Makh,Tenkh,Dienthoai,Diachi FROM DM_KHACHHANG ORDER BY Makh ASC");
                otblSrc = new MTSQLServer().wRead(mSql,null,false);
                if (otblSrc == null)
                {
                    Utils.showMessage("Không thể đọc dữ liệu cần chọn...", "Thông báo");
                    return;
                }
                otblChon = otblSrc.Clone();

                grdDS.ItemsSource = this.otblSrc;
                grdDSChon.ItemsSource = this.otblChon;                  
            }
            catch (Exception ex) { Utils.showMessage("Không thể đọc danh mục. " + ex.Message.ToString(), "Thông báo","ERR"); }
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

        private void tblDS_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e){
            try{
                DataRow vR = null;
                for (int i = 0; i < grdDS.VisibleRowCount; i++){
                    if (tblDS.IsRowSelected(i)){
                        vR = otblChon.NewRow();
                        vR["Makh"] = grdDS.GetCellValue(i, colMakh);
                        vR["Tenkh"] = grdDS.GetCellValue(i, colTenKH);
                        vR["Dienthoai"] = grdDS.GetCellValue(i, colDienthoai);
                        vR["Diachi"] = grdDS.GetCellValue(i, colDiachi);
                         
                        otblChon.Rows.Add(vR);
                        tblDS.DeleteRow(i);
                        break;
                    }
                }

                if (grdDS.VisibleRowCount > 0)
                {
                    tblDS.SelectRow(0);
                }
                otblChon.AcceptChanges();
                grdDSChon.ItemsSource = otblChon;
                otblSrc.AcceptChanges();
                grdDS.RefreshData();
            }
            catch (Exception ex) { }
        }


        private void cmdAddAll_Click(object sender, RoutedEventArgs e){
            try{
                DataRow vR = null;
                if (otblSrc != null && otblSrc.Rows.Count > 0){
                    foreach (DataRow vRow in otblSrc.Rows){
                        vR = otblChon.NewRow();                       
                        vR["Makh"] = vRow["Makh"];
                        vR["Tenkh"] = vRow["Tenkh"];
                        vR["Dienthoai"] = vRow["Dienthoai"];
                        vR["Diachi"] = vRow["Diachi"];
                        otblChon.Rows.Add(vR);
                    }
                    otblChon.AcceptChanges();
                    otblSrc.Rows.Clear();
                    grdDS.RefreshData();
                    grdDSChon.RefreshData();
                }
            }
            catch { }
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRow vR = null;
                for (int i = 0; i < grdDSChon.VisibleRowCount; i++)
                {
                    if (tblDSChon.IsRowSelected(i))
                    {
                       vR = otblSrc.NewRow();
                    
                       vR["Makh"] = grdDSChon.GetCellValue(i, colMakhC);
                       vR["Tenkh"] = grdDSChon.GetCellValue(i, colTenKHC);
                       vR["Dienthoai"] = grdDSChon.GetCellValue(i, colDienthoaiC);
                       vR["Diachi"] = grdDSChon.GetCellValue(i, colDiachiC);
                       otblSrc.Rows.Add(vR);
                      tblDSChon.DeleteRow(i);
                    }
                }
                if (grdDSChon.VisibleRowCount > 0)
                {
                    tblDSChon.SelectRow(0);
                }
                otblChon.AcceptChanges();
                otblSrc.AcceptChanges();
                grdDS.ItemsSource = otblSrc;
            }
            catch (Exception ex) { }
        }

        private void cmdRemoveAll_Click(object sender, RoutedEventArgs e){
            try{
                DataRow vR = null;
                if (otblChon != null && otblChon.Rows.Count > 0){
                    foreach (DataRow vRow in otblChon.Rows)
                    {
                       vR = otblSrc.NewRow();                       
                       vR["Makh"] = vRow["Makh"];
                       vR["Tenkh"] = vRow["Tenkh"];
                       vR["Dienthoai"] = vRow["Dienthoai"];
                       vR["Diachi"] = vRow["Diachi"];
                       otblSrc.Rows.Add(vR);                           
                    }
                    otblSrc.AcceptChanges(); 
                    otblChon.AcceptChanges();
                    otblChon.Rows.Clear();
                    otblChon.AcceptChanges(); 
                    grdDS.ItemsSource = otblSrc;
                }
            }
            catch (Exception ex) { }
        }

      
    }
}
