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

    public partial class dlg_ChonInmavach : Window
    {
        public bool isChon = false;
        private DataTable otblSrc = null;
        public DataTable otblChon = null;
        public bool isMaQR = true;
        public bool isDongia = false;

        public dlg_ChonInmavach()
        {
            InitializeComponent();                       
           
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
                String mSql=String.Format("select Maspid,Masp,Tensp,Dvt,Quycach,Mabarcode,Maqrcode,cast ({0} as int) as Soluong from DM_SANPHAM order by Masp asc",txtSoluong.Text.Trim());
                otblSrc = new MTSQLServer().wRead(mSql,null,false);
                if (otblSrc == null) {
                    Utils.showMessage("Không thể đọc dữ liệu cần chọn...", "Thông báo");
                    return;
                }
                otblChon = otblSrc.Clone();

                grdDSPhieu.ItemsSource = this.otblSrc;
                grdDSPhieuChon.ItemsSource = this.otblChon;
                
                
                tblDSPhieu.ShowEditor();
                tblDSPhieuChon.ShowEditor();

                grdDSPhieuChon.View.ShowEditor();
                grdDSPhieuChon.View.NavigationStyle = DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell;
                foreach (var Col in grdDSPhieuChon.Columns)
                {
                    Col.ReadOnly = true;
                    Col.AllowFocus = false; 
                }

                grdDSPhieuChon.Columns["Soluong"].ReadOnly = false;
                grdDSPhieuChon.Columns["Soluong"].AllowFocus = true;

            }
            catch (Exception ex) { }
        }
       
        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            try {
                isMaQR = false;
                isDongia = false;  
                if (rdbQRCode.IsChecked == true) {
                    isMaQR = true;
                }
                if (chkInPrice.IsChecked == true) {
                    isDongia = true;
                }
                otblChon.AcceptChanges();
                isChon = true;
                this.Close();
            }catch{}
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
            if (tblDSPhieu.IsRowSelected(tblDSPhieu.FocusedRowHandle) == true) { 
              
            }
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < grdDSPhieu.VisibleRowCount; i++)
                {
                    if (tblDSPhieu.IsRowSelected(i))
                    {
                        DataRow vR = otblChon.NewRow();
                        vR["Maspid"] = grdDSPhieu.GetCellValue(i, colMaspid);
                        vR["Masp"] = grdDSPhieu.GetCellValue(i, colMasp);
                        vR["Tensp"] = grdDSPhieu.GetCellValue(i, colTensp);
                        vR["Dvt"] = grdDSPhieu.GetCellValue(i, colDvt);
                        vR["Quycach"] = grdDSPhieu.GetCellValue(i, colQuycach);
                        vR["Soluong"] = txtSoluong.Text.Trim();
                        otblChon.Rows.Add(vR);

                        tblDSPhieu.DeleteRow(i);
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
            try
            {
                if (otblSrc != null && otblSrc.Rows.Count > 0)
                {
                    foreach (DataRow vRow in otblSrc.Rows)
                    {
                        DataRow vR = otblChon.NewRow();
                        vR["Maspid"] = vRow["Maspid"];
                        vR["Masp"] = vRow["Masp"];
                        vR["Tensp"] = vRow["Tensp"];
                        vR["Dvt"] = vRow["Dvt"];
                        vR["Quycach"] = vRow["Quycach"];
                        vR["Soluong"] = txtSoluong.Text;
                        otblChon.Rows.Add(vR);

                    }
                    otblChon.AcceptChanges();
                    otblSrc.Rows.Clear();
                    grdDSPhieu.ItemsSource = otblSrc;
                    grdDSPhieuChon.ItemsSource = otblChon;
                }
            }
            catch (Exception ex) { }
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            try{
                for (int i = 0; i < grdDSPhieuChon.VisibleRowCount; i++)
                {
                    if (tblDSPhieuChon.IsRowSelected(i))
                    {
                        DataRow vR = otblSrc.NewRow();
                        vR["Maspid"] = grdDSPhieuChon.GetCellValue(i, colMaspidc);
                        vR["Masp"] = grdDSPhieuChon.GetCellValue(i, colMaspc);
                        vR["Tensp"] = grdDSPhieuChon.GetCellValue(i, colTenspc);
                        vR["Dvt"] = grdDSPhieuChon.GetCellValue(i, colDvtc);
                        vR["Quycach"] = grdDSPhieuChon.GetCellValue(i, colQuycachc);
                        otblSrc.Rows.Add(vR);

                        tblDSPhieuChon.DeleteRow(i);
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
                if (otblChon != null && otblChon.Rows.Count > 0)
                {
                    foreach (DataRow vRow in otblChon.Rows)
                    {
                        DataRow vR = otblSrc.NewRow();
                        vR["Maspid"] = vRow["Maspid"];
                        vR["Masp"] = vRow["Masp"];
                        vR["Tensp"] = vRow["Tensp"];
                        vR["Dvt"] = vRow["Dvt"];
                        vR["Quycach"] = vRow["Quycach"];
                        otblSrc.Rows.Add(vR);

                    }
                    otblSrc.AcceptChanges();

                    otblChon.AcceptChanges();
                    otblChon.Rows.Clear();
                    otblChon.AcceptChanges();
                    //grdDSPhieuChon.ItemsSource = otblSrc;
                    grdDSPhieu.ItemsSource = otblSrc;
                }
            }
            catch (Exception ex) { }
        }

        private void txtSoluong_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                if (txtSoluong.Text != "0" && txtSoluong.Text.Length > 0) {
                    grdDSPhieuChon.SetCellValue(tblDSPhieuChon.FocusedRowHandle, colSoluongc, int.Parse(txtSoluong.Text));
                }
            }
        }

        private void tblDSPhieu_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {
            cmdAdd_Click(sender, e);
        }

        private void tblDSPhieuChon_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {
            cmdRemove_Click(sender, e);
        }

      
    }
}
