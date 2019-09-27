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
using MTPMSWIN.Model;

using System.Data;
using System.Data.SqlClient;

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class HT_Register : Window
    {
        public HT_Register()
        {
            InitializeComponent();

            lblAPIServer.Visibility = Visibility.Hidden;
            txtAPIServer.Visibility = Visibility.Hidden;
            onBindData();
        }

        private void onBindData() {
            try
            {
                lblActive.Content = ""; 
                String mSql = String.Format("select PARA_NAME,PARA_VAL,PARA_DESC from HT_PARA WHERE PARA_TYPE IN('HT')");
                DataTable oTblSrc = new MTSQLServer().wRead(mSql, null, false);

                if (oTblSrc != null) {
                    foreach (DataRow vR in oTblSrc.Rows) {
                        if (vR["PARA_NAME"].ToString().Equals("PAR_CUS")) {
                            txtCustomer.Text = vR["PARA_VAL"].ToString();
                        }
                        if (vR["PARA_NAME"].ToString().Equals("PAR_ADDR"))
                        {
                            txtAddress.Text = vR["PARA_VAL"].ToString();
                        }
                        if (vR["PARA_NAME"].ToString().Equals("PAR_PHONE"))
                        {
                            txtPhone.Text = vR["PARA_VAL"].ToString();
                        }
                        if (vR["PARA_NAME"].ToString().Equals("PAR_IMEI"))
                        {
                            txtImei.Text = vR["PARA_VAL"].ToString();
                        }

                      
                        if (vR["PARA_NAME"].ToString().Equals("PAR_ACTIVE"))
                        {
                            switch (vR["PARA_VAL"].ToString()) {
                                case "SYNC_WAIT":
                                   lblActive.Content= "Phần mềm đã đăng ký. Đang chờ duyệt..";
                                    break;

                                case "SYNC_LOCK":
                                    lblActive.Content = "Phần mềm đã bị khóa. Vui lòng liện hệ Admin..";
                                    break;

                                case "SYNC_NOT_REG":
                                    lblActive.Content = "Phần mềm chưa gửi thông tin đăng ký..";
                                    break;

                                case "SYNC_ACTIVE":
                                    lblActive.Content = "Phần mềm đã kích hoạt..";
                                    break;

                                default:
                                    lblActive.Content = "Phần mềm Chưa kích hoạt";
                                    break;
                            }
                        }
                    }
                }

                String APIServer = MTGlobal.ReadRegistryKey(MTGlobal.MT_REGKEY_SECTION_API, MTGlobal.MT_REGKEY_APIHOST, "");
                txtAPIServer.Text = APIServer;
            }
            catch { }
        }

        private void onRegister(object sender, RoutedEventArgs e)
        {
           //txtImei.Text = MTGlobal.GetNewID();
           String Customer = txtCustomer.Text;
           String Address = txtAddress.Text;
           String Phone = txtPhone.Text;
           String PosImei = txtImei.Text;
           try
           {
               if (txtAPIServer.Visibility == Visibility.Visible)
               {
                   MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_API, MTGlobal.MT_REGKEY_APIHOST, txtAPIServer.Text.Trim());
                   MTGlobal.MT_API_HOST = MTGlobal.ReadRegistryKey(MTGlobal.MT_REGKEY_SECTION_API, MTGlobal.MT_REGKEY_APIHOST, "");
                   if (txtAPIServer.Text.Length > 0 && MTGlobal.MT_API_HOST == "")
                   {
                       Utils.showMessage("Không thể ghi thông tin API HOST..", "Thông báo");
                   }
               }
           }
           catch { }

           if (Customer == "") {
               Utils.showMessage("Bạn chưa nhập tên đơn vị sử dụng..", "Thông báo");
               return;
           }
           if (Address == "")
           {
               Utils.showMessage("Bạn chưa nhập địa chỉ..", "Thông báo");
               return;
           }
           if (Phone== "")
           {
               Utils.showMessage("Bạn chưa nhập số điện thoại..", "Thông báo");
               return;
           }
           if (ValidateHelper.fValidPhone(Phone)==false)
           {
               Utils.showMessage("Số điện thoại không hợp lệ.", "Thông báo");
               return;
           }


           DataTable oTblPara = new DataTable();
           oTblPara.Columns.Add("PARA_NAME", typeof(String));
           oTblPara.Columns.Add("PARA_VAL", typeof(String));
           oTblPara.Columns.Add("PARA_DESC", typeof(String));

           DataRow vR = oTblPara.NewRow();
           vR["PARA_NAME"] = "PAR_CUS";
           vR["PARA_VAL"] = txtCustomer.Text.Trim();
           oTblPara.Rows.Add(vR);

           vR = oTblPara.NewRow();
           vR["PARA_NAME"] = "PAR_ADDR";
           vR["PARA_VAL"] = txtAddress.Text.Trim();
           oTblPara.Rows.Add(vR);

           vR = oTblPara.NewRow();
           vR["PARA_NAME"] = "PAR_PHONE";
           vR["PARA_VAL"] = txtPhone.Text.Trim();
           oTblPara.Rows.Add(vR);

           vR = oTblPara.NewRow();
           vR["PARA_NAME"] = "PAR_IMEI";
           vR["PARA_VAL"] = txtImei.Text.Trim();
           oTblPara.Rows.Add(vR);             
           oTblPara.AcceptChanges();

           SqlParameter[] arrPara = new SqlParameter[2];
           arrPara[0] = new SqlParameter("@tblPara", SqlDbType.Structured);
           arrPara[0].Value = oTblPara;
           arrPara[1] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
           arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
           new MTSQLServer().wExec("HT_addPara", arrPara);


           String mPara = String.Format("posimei={0}&customername={1}&tel={2}&address={3}&istype=POS", PosImei, Customer, Phone, Address);
           String mRs = MTSynData.fSyncGet(MTSynData.mGetAction_Register, mPara);
          
           if (mRs != null && mRs != ""){
               String[] aRs = mRs.Split('#');
               if (aRs.Length > 1)
               {
                   String mPAR_ACTIVE = "";
                   txtImei.Text = aRs[0].Trim().Replace("\"", "").Replace("'", "");
                   if (aRs[1].Contains("SYNC_OK"))
                   {
                       mPAR_ACTIVE = "SYNC_OK";
                   }
                   else if (aRs[1].Contains("SYNC_WAIT"))
                   {
                       mPAR_ACTIVE = "SYNC_WAIT";
                   }
                   else if (aRs[1].Contains("SYNC_STOP"))
                   {
                       mPAR_ACTIVE = "SYNC_STOP";
                   }
                   else if (aRs[1].Contains("SYNC_ACTIVE"))
                   {
                       mPAR_ACTIVE = "SYNC_ACTIVE";
                   }
                   else
                   {
                       mPAR_ACTIVE = "";
                   }

                   if (mPAR_ACTIVE != ""){                       
                       oTblPara.Clear(); 
                       vR = oTblPara.NewRow();
                       vR["PARA_NAME"] = "PAR_ACTIVE";
                       vR["PARA_VAL"] = mPAR_ACTIVE;
                       oTblPara.Rows.Add(vR); 
                       oTblPara.AcceptChanges();

                       arrPara = new SqlParameter[2];
                       arrPara[0] = new SqlParameter("@tblPara", SqlDbType.Structured);
                       arrPara[0].Value = oTblPara;
                       arrPara[1] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
                       arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                       new MTSQLServer().wExec("HT_addPara", arrPara);
                   }
                   if (aRs[1].Length > 0)
                   {
                       Utils.showMessage(aRs[1].Replace(mPAR_ACTIVE, "").Replace(":", ""), "Thông báo");
                       return;
                   }
               }
               Utils.showMessage("Đăng ký thông tin thành công.", "Thông báo");
           }
           else {
               Utils.showMessage("Không thể đăng ký. Vui lòng kiểm tra kết nối..", "Thông báo");
           }
           
        }

      
        private void onCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtCustomer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtCustomer.Text.Length<=0){
                    Utils.showMessage("Vui lòng nhập tên đơn vị sử dụng/ Chủ cửa hàng", "Lưu ý");
                    txtCustomer.Focus();
                    return;
                }
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtAddress_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtAddress.Text.Length <= 0)
                {
                    Utils.showMessage("Vui lòng nhập địa chỉ đơn vị sử dụng/ Chủ cửa hàng", "Lưu ý");
                    txtCustomer.Focus();
                    return;
                }
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtPhone_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {               
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void txtImei_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void lblRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Grid_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && Keyboard.Modifiers == ModifierKeys.Control)
            {
                lblAPIServer.Visibility = Visibility.Visible;
                txtAPIServer.Visibility = Visibility.Visible;
            }
        }
         
    }
}
