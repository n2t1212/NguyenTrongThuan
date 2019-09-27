using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.ComponentModel;

using DevExpress.Xpf.Docking;
 
using MTPMSWIN.Model;
using MTPMSWIN.View;

namespace MTPMSWIN
{
 
    public partial class MTMain : Window
    {
        private Double iPanelW = 0;
        private Double iPanelH = 0;
        private BackgroundWorker oWorker = new BackgroundWorker();

        public MTMain(){ 
            InitializeComponent();               
        }

        #region "EVENT"
            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                try
                {
                    fSetMenuMain();                     
                    activeMsg.Text = "Bản quyền: " + MTGlobal.HT_POS_ACTIVE;
                    txtServerName.Text = "Máy chủ: " + MTGlobal.MT_DBHost;
                    txtDBName.Text = "CSDL:" + MTGlobal.MT_DBNAME;

                    oWorker.DoWork += new DoWorkEventHandler(this.oWorker_DoWork);
                    oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(oWorker_RunWorkerCompleted);
                    if (oWorker.IsBusy == false)
                    {
                        oWorker.RunWorkerAsync(2000);
                    }

                    if (!MTGlobal.HT_POS_IS_ACTIVE){
                        //Lock Application
                    }
                }
                catch (Exception ex) { Utils.showMessage("Không thể khởi tạo ứng dụng...", "Thông báo"); }
            }

            private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e){
               if (iPanelW == 0 || iPanelH == 0) {
                    iPanelW = this.Width;
                    iPanelH = this.Height + 50 - (this.GRIDTOP.Height + this.GRIDBOTTOM.Height);
                }               
                this.DLayOutMain.Height = iPanelH;
                this.DLayOutMain.Width = iPanelW-2;
            }

            private void cmdMiniApp_Click(object sender, RoutedEventArgs e)
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            }
            private void cmdExitApp_Click(object sender, RoutedEventArgs e)
            {
                App.Current.Shutdown();
            }
          

            private void mnuAccount_Click(object sender, RoutedEventArgs e)
            {
                /*
                var addButton = sender as FrameworkElement;
                if (addButton != null)
                {
                    addButton.ContextMenu.IsOpen = true;
                }*/
            }

            //SET ACTIVE MENU KHI CHỌN PAGE
            private void DocGrpMain_SelectedItemChanged(object sender, DevExpress.Xpf.Docking.Base.SelectedItemChangedEventArgs e)
            {
                try{
                    if (e.Item.Name != null){
                        MTGlobal.MT_ACTIVE_CAPTION= e.Item.Name.ToString();
                    }
                }
                catch { }
            }
            private void fMenuItem_Click(object sender, EventArgs e){
                try{
                    string mMENUID="", mFormName = "", mFormCaption = "";
                    mMENUID = ((System.Windows.Controls.MenuItem)sender).Tag.ToString();
                    mFormName = ((System.Windows.Controls.MenuItem)sender).Name.ToString();
                    mFormCaption = ((System.Windows.Controls.MenuItem)sender).Header.ToString();

                    switch (((System.Windows.Controls.MenuItem)sender).Name.ToString())
                    {
                        case "mnuDM_KHO":
                            fRenderForm(mMENUID, "DM_KhoHang", mFormCaption);
                            break;
                        case "mnuDM_NHOMSANPHAM":
                            fRenderForm(mMENUID,"DM_NhomSanPham", mFormCaption);
                            break;
                        case "mnuDM_SANPHAM":
                            fRenderForm(mMENUID, "DM_SanPham", mFormCaption);
                            break;
                        case "mnuDM_XE":
                            fRenderForm(mMENUID, "DM_Xe", mFormCaption);
                            break;
                        case "mnuDM_DONGIA":
                            fRenderForm(mMENUID, "DM_BangGia", mFormCaption);
                            break;
                        case "mnuDM_LYDO":
                            fRenderForm(mMENUID, "DM_LyDo", mFormCaption);
                            break;

                        case "mnuHH_PHIEUNHAP":
                            MTGlobal.MT_LOAIP = MTGlobal.PN;
                            if (Utils.ChonThoiGian()) {
                                fRenderForm(mMENUID, "NX_NhapXuatList", "DANH SÁCH PHIẾU NHẬP");
                            }                         
                            break;

                        case "mnuHH_PHIEUXUAT":
                            MTGlobal.MT_LOAIP = MTGlobal.PX;
                            if (Utils.ChonThoiGian()) {
                                fRenderForm(mMENUID, "NX_NhapXuatList", "DANH SÁCH PHIẾU XUẤT");
                            }
                            break;
                       
                        case "mnuHH_CHANHXE":
                            MTGlobal.MT_LOAIP = MTGlobal.CX;
                            if (Utils.ChonThoiGian()) {
                                fRenderForm(mMENUID, "CX_ChanhXeList", "DANH SÁCH GỬI CHÀNH");
                            }
                            break; 
                             
                        case "mnuBH":
                            MTGlobal.MT_LOAIP = MTGlobal.BH;
                            if (Utils.ChonThoiGian()) {
                                if (Utils.ChonQuay(MTGlobal.BH))
                                {
                                    fRenderForm(mMENUID, "BH_BanHangList", "Bán Hàng");
                                }
                            }
                            break;

                        case "mnuHT_NGUOIDUNG":
                            fRenderForm(mMENUID, "HT_NguoiDung", mFormCaption);
                            break;
                        case "mnuHT_QUYENHAN":
                            fRenderForm(mMENUID, "HT_QuyenHan", mFormCaption);
                            break;
                        case "mnuHT_SAOLUU":
                            fRenderForm(mMENUID, "HT_SaoLuu", mFormCaption);
                            break;
                        case "mnuHT_PHUCHOI":
                            fRenderForm(mMENUID, "HT_PhucHoi", mFormCaption);
                            break;
                        case "mnuHT_THAMSO":
                            fRenderForm(mMENUID, "HT_ThamSo", mFormCaption);
                            break;
                        case "mnuHT_THONGTIN":
                            fRenderForm(mMENUID, "HT_ThongTin", mFormCaption);
                            break;
                        case "mnuDM_KHACHHANG":
                            fRenderForm(mMENUID, "DM_KhachHang", mFormCaption);
                            break;
                    }
                }
                catch (Exception ex) { MessageBox.Show("ERR:" + ex.Message.ToString(), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }
            }  
        #endregion


        #region "FUNCTION" 
            private void fSetMenuMain(){
                try{
                    string mSQL = string.Format("select * from DM_CHUCNANG order by cap asc, stt asc");
                    DataTable otblMenu = MTSQLServer.getMTSQLServer().wRead(mSQL, null, false);
                    if (otblMenu != null)
                    {
                        DataRow[] MnuRootRow = otblMenu.Select("", "stt asc");
                        foreach (DataRow MnuR in MnuRootRow)
                        {
                            object mnu = this.mnuMain.FindName(MnuR["kyhieu"].ToString());
                            if (mnu != null)
                            {
                                ((System.Windows.Controls.MenuItem)mnu).IsEnabled = true;
                                ((System.Windows.Controls.MenuItem)mnu).Tag = MnuR["macn"].ToString();
                                ((System.Windows.Controls.MenuItem)mnu).Click += new RoutedEventHandler(this.fMenuItem_Click);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("ERROR_LOADMENU: " + ex.Message.ToString(), "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        
            void SetWindowActive(BaseLayoutItem item){
                DLayOutMain.LayoutController.Activate(item);
            }

            /*NOTES: RENDER DOCUMENTGROUP: WINDOWS_LOADED NOTWORK -> USED GRID_LOADED OF WINDOWS/GRID_LOADED USERCONTROL*/
            public void fRenderForm(string oMenuID,string oFormName, string oFormCaption){
                try{ 
                    if (MTGlobal.TAB_ARRAY == null){
                        MTGlobal.TAB_ARRAY = new List<string>();
                    }
                    if (MTGlobal.TAB_ARRAY.Contains(oMenuID))
                    {
                        return;
                    }
                    MTGlobal.TAB_ARRAY.Add(oMenuID);

                    string mFormPath = @"View\" + oFormName + ".xaml";                   
                    DocumentPanel frmPanel = null;                    
                    frmPanel = DLayOutMain.DockController.AddDocumentPanel(DocGrpMain, new Uri(mFormPath, UriKind.Relative));                 
                    frmPanel.Name = oMenuID;
                    frmPanel.Caption = oFormCaption;
                    DocumentPanel.SetMDISize(frmPanel, new Size(400, 300));
                    DocumentPanel.SetMDILocation(frmPanel, new Point(200, 200));                    
                    SetWindowActive(frmPanel);

                    MTGlobal.MT_ACTIVE_MENUID = oMenuID;
                    MTGlobal.MT_ACTIVE_FORM = oFormName;
                    MTGlobal.MT_ACTIVE_CAPTION = oFormCaption;
                }
                catch { }
            }          
        #endregion        

            public void onExitTab() {                
                try
                {
                    DLayOutMain.DockController.Close(DLayOutMain.ActiveDockItem);
                }
                catch { }
            }

            private void onCloseTab(object sender, DevExpress.Xpf.Docking.Base.ItemCancelEventArgs e)
            {
                var closedItem = (((DevExpress.Xpf.Docking.Base.ItemEventArgs)(e)).Item).Name;
                if (MTGlobal.TAB_ARRAY != null && closedItem != null)
                {
                    if (MTGlobal.TAB_ARRAY.Contains(closedItem))
                    {
                        MTGlobal.TAB_ARRAY.Remove(closedItem);
                    }
                }
            }
            private void mnuHT_THONGTIN_Click(object sender, RoutedEventArgs e)
            {
                HT_Register oRegister = new HT_Register();
                oRegister.ShowDialog();
            }
            private void mnuHT_KETCHUYENSODU_Click(object sender, RoutedEventArgs e)
            {
                if (Utils.ChonThoiGian(false, true,false))
                {
                    MTGlobal.Ketchuyensodu();
                     
                }
            }

            private void mnuBC_THEKHO_Click(object sender, RoutedEventArgs e)
            {
                if (Utils.ChonThoiGian(true, true, true))
                {
                    if (Utils.ChonPhieu("SP"))
                    {
                        new MTReport().rptDM_TheKho(Utils.otblChon);
                    }
                }
            }
            private void mnuBC_NHAPXUATTON_Click(object sender, RoutedEventArgs e)
            {
                if (Utils.ChonThoiGian(true, true, true))
                {
                    if (Utils.ChonPhieu("SP"))
                    {
                        new MTReport().rptHH_Baocaonhapxuatton(Utils.otblChon);
                    }
                }
            }

            private void mnuBC_TKGUICHANH_Click(object sender, RoutedEventArgs e)
            {
                if (Utils.ChonThoiGian(true, true, true))
                {
                    if (Utils.ChonPhieu("CX"))
                    {
                        new MTReport().rptNX_Thongkeguichanh(Utils.otblChon, MTGlobal.MT_TUNGAY, MTGlobal.MT_DENNGAY);
                    }

                }
            }
            private void mnuBC_BANHANG_Click(object sender, RoutedEventArgs e)
            {
                if (Utils.ChonThoiGian(true, true, true))
                {
                    //if (Utils.ChonPhieu("CX")){                       
                    //} 
                    DataTable otblChon = new DataTable();
                    otblChon.Columns.Add(new DataColumn("Maspid", typeof(System.String)));
                    otblChon.Columns.Add(new DataColumn("Masp", typeof(System.String)));

                    new MTReport().rptNX_Baocaobanhang(otblChon, MTGlobal.MT_TUNGAY, MTGlobal.MT_DENNGAY);

                }
            }

            private void onReport(String mMenuName) {
                switch (mMenuName) { 
                    case "PN":
                        break;
                    case "PX":
                        break;
                }    
            }



          #region "SYNC_DATA"
            private void oWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                MTSynData.doSyncPost();
            }
            private void oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                /*if (e.Cancelled) MessageBox.Show("Operation was canceled");
                else if (e.Error != null) MessageBox.Show(e.Error.Message);
                else MessageBox.Show(e.Result.ToString());
                 * */
            }
          #endregion
           

          
         
    }
}
