using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO; 
using System.Globalization;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace MTPMSWIN.Model
{
   public static class MTGlobal
   {
       #region "GLOBAL-VARIBLE"
       public static string MT_REGKEY_OWNER = "SOFTWARE\\MiMiGroup";
       public static string MT_REGKEY_SOFT = "MTPos";
       public static string MT_REGKEY_SECTION_SQL = MT_REGKEY_OWNER +"\\"+MT_REGKEY_SOFT+ "\\MTPOS_SQL";
       public static string MT_REGKEY_SECTION_API =  MT_REGKEY_OWNER +"\\"+MT_REGKEY_SOFT+ "\\MTPOS_API";
       public static string MT_REGKEY_DBHOST = "MT_SQL_HOST";
       public static string MT_REGKEY_DBNAME = "MT_SQL_NAME";
       public static string MT_REGKEY_DBUSER = "MT_SQL_USER";
       public static string MT_REGKEY_DBPASS = "MT_SQL_PASS";
       public static string MT_REGKEY_APIHOST = "MT_API_HOST";

       public static string MT_API_HOST = "";
       public static string MT_DBHost = "";
       public static string MT_DBPass = "";
       public static string MT_DBNAME = "";
       public static string MT_SQL_USER = "";
       public static string MT_SQL_PASS = "";
       public static string MT_SQL_DATA_PATH_x64 = @"C:\Program Files\Microsoft SQL Server\MSSQL10.SQL2008\MSSQL\DATA\";
       public static string MT_SQL_DATA_PATH_x86 = @"C:\Program Files (x86)\Microsoft SQL Server\MSSQL10.SQL2008\MSSQL\DATA\";

       public static string MT_SQLCNN_STR = "";
       public static SqlConnection MT_Cnn;

       public static string RPT_COMPANY = "CÔNG TY ABC";
       public static string RPT_ADDRESS = "AAAA";
       public static string RPT_TEL = "084 28434343";
       public static string RPT_TAX = "084 28434343";

       public static string HT_POS_CUSTOMERNAME = "";
       public static string HT_POS_TEL = "";
       public static string HT_POS_ADDRESS = "";
       public static string HT_POS_ACTIVE = "";
       public static bool HT_POS_IS_ACTIVE = false;
       public static string HT_POS_IMEI = "";
       public static string HT_SYS_INIT_DATE = "";


       public static string MT_KYHIEU_CTY = "GTC";
       public static string MT_USER_LOGIN = "";
       public static string MT_USER_LOGIN_FULLNAME = "";
       public static string MT_KYHIEU_USER = "A";
       public static string MT_KYHIEU_CUAHANG = "X";
       public static string MT_USER_PASS="";

       //DÙNG CHO DIALOG CHỌN NGÀY/THANG/NAM
       public static string MT_LOAI_TG = "D";
       // array tab layout
       public static List<String> TAB_ARRAY = null;
        
       //APDUNG GETQUYEN MENU
       public static string MT_ACTIVE_USERID = "";
       public static string MT_ACTIVE_MENUID = "";
       public static string MT_ACTIVE_FORM = "";
       public static string MT_ACTIVE_CAPTION = "";
       public static string MT_ACTIVE_COMPANYID = "03C46C08-3496-4895-9F2E-A51EEE8CBCC0";

       public static bool MT_REMEMBER = false;
       public static int MT_YEAR = 0;

       public static string MT_TUNGAY = "";
       public static string MT_DENNGAY = "";
       public static string MT_THANG = "";
       public static string MT_NAM = "";
       public static String MT_QUAY = "";
       public static String MT_CA = ""; 

       public static string PN = "PN";
       public static string PX = "PX";
       public static string CX = "CX";
       public static string BH = "BH";
       public static string MT_LOAIP = "";

       public static MT_PHIEU MT_LOAIPHIEU; 
       public enum MT_PHIEU {DM=1,PN=2,PX=3,TK=4,CX=5}


       public struct MT_ROLE {
           public bool isAdd;
           public bool isEdit;
           public bool isDel;
           public bool isPrint;
       }

       public struct MT_BUTTONACTION {
           public Button cmdAdd; 
           public Button cmdEdit;
           public Button cmdDel;
           public Button cmdSave;
           public Button cmdAbort;
           public Button cmdPrint;           
       }

       public static String MT_CURRENT_ACTION = "";
    #endregion


    #region "GLOBAL-FUNCTION"
      //[CONFIG FILE]
       public static string GetConfigKey(string mKey){
           try{
               // var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
               var config = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location); 
               var entry = config.AppSettings.Settings[mKey];
               if (entry != null){                  
                   return entry.Value.ToString();
               }
               else { return ""; }
           }
           catch { return ""; }
       }

       public static void SetConfigKey(string mKey, string mVal)
       {
           try{            
               
              // var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
               var config = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
               var entry = config.AppSettings.Settings[mKey];
               if (entry == null)
                   config.AppSettings.Settings.Add(mKey, mVal);
               else
                   config.AppSettings.Settings[mKey].Value = mVal;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
           }
           catch(Exception ex) { }
       }
       
       //[REGISTRY]
       public static String ReadRegistryKey(String mSection, String mKey, String mDefaultVal) {
           try{
               RegistryKey mRootKey = Registry.CurrentUser;
               mRootKey = Registry.CurrentUser;
               RegistryKey oSubKey = mRootKey.CreateSubKey(mSection);
               return oSubKey.GetValue(mKey,mDefaultVal).ToString();
           }catch { }
           return mDefaultVal;
       }
       public static void WriteRegistryKey(String mSection, String mKey,String mVal)
       {
           try
           {
               RegistryKey mRootKey = Registry.CurrentUser;
               RegistryKey oSubKey = mRootKey.CreateSubKey(mSection);
               oSubKey.SetValue(mKey, mVal);
           }
           catch { }
       }
       public static void DeleteRegistryKey(String mSection, String mKey)
       {
           try
           {
               RegistryKey mRootKey = Registry.CurrentUser;
               RegistryKey oSubKey = mRootKey.CreateSubKey(mSection);
               oSubKey.DeleteSubKey(mKey);
           }
           catch { }
       } 

       //END REGISTRY 


       private static int fRandomNumber(int min, int max)
       {
           Random oRand = new Random();
           return oRand.Next(min, max);
       }

       public static string HashMD5(string password = "")
       {
           try
           {
               byte[] bytesofLink = System.Text.Encoding.UTF8.GetBytes(password);
               System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
               string HashParams = BitConverter.ToString(md5.ComputeHash(bytesofLink));
               return HashParams;
           }
           catch { return ""; }
       }

       public static string getMD5(string input)
       {
           try
           {
               System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
               byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
               byte[] hash = md5.ComputeHash(inputBytes); 
               // step 2, convert byte array to hex string
               System.Text.StringBuilder sb = new System.Text.StringBuilder();
               for (int i = 0; i < hash.Length; i++)
               {
                   sb.Append(hash[i].ToString("X2"));
               }
               return sb.ToString();
           }
           catch { return ""; }
       }
    #endregion


    #region "APP-FUNCTION"
       public static String CNN_TYPE = "SQL";
       public static bool MTAPPSTART(){
           try{
               CNN_TYPE = "SQL";
               try {
                   MT_API_HOST = ReadRegistryKey(MT_REGKEY_SECTION_API, MT_REGKEY_APIHOST, "");
               }
               catch { }

               if (CNN_TYPE == "SQL"){ 
                   MT_DBHost = ReadRegistryKey(MT_REGKEY_SECTION_SQL,MT_REGKEY_DBHOST,"");
                   MT_DBNAME = ReadRegistryKey(MT_REGKEY_SECTION_SQL, MT_REGKEY_DBNAME, "MTSMPOS");
                   MT_SQL_USER = ReadRegistryKey(MT_REGKEY_SECTION_SQL, MT_REGKEY_DBUSER, "sa");
                   MT_SQL_PASS = ReadRegistryKey(MT_REGKEY_SECTION_SQL, MT_REGKEY_DBPASS, "123456789");
                                       
                   MT_SQLCNN_STR = string.Format(@"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Max Pool Size=2000", MT_DBHost, MT_DBNAME, MT_SQL_USER, MT_SQL_PASS);
                   

                   MT_USER_LOGIN = GetConfigKey("MT_ACCOUNT");
                   MT_USER_PASS = GetConfigKey("MT_PASSWORD");
                   MT_REMEMBER = GetConfigKey("MT_REMEMBER").ToString().Trim() == "1" ? true : false;
 
                   return fInit_SQLConnect();
               }
               else if (CNN_TYPE == "MDF") { 
                   MT_DBHost = ReadRegistryKey(MT_REGKEY_SECTION_SQL, MT_REGKEY_DBHOST, "");
                   MT_DBNAME = ReadRegistryKey(MT_REGKEY_SECTION_SQL, MT_REGKEY_DBNAME, "MTSMPOS");
                   //MT_SQL_USER = ReadRegistryKey(MT_REGKEY_SECTION_SQL, MT_REGKEY_DBUSER, "sa");
                   //MT_SQL_PASS = ReadRegistryKey(MT_REGKEY_SECTION_SQL, MT_REGKEY_DBPASS, "123456789");

                   String mMDFPath = System.Reflection.Assembly.GetExecutingAssembly().Location + "/POSDB/";
                   MT_SQLCNN_STR = string.Format(@"Data Source={0};Integrated Security=True;AttachDbFilename=", MT_DBHost,  mMDFPath+ MT_DBNAME);
                   
                   MT_USER_LOGIN = GetConfigKey("MT_ACCOUNT");
                   MT_USER_PASS = GetConfigKey("MT_PASSWORD");
                   MT_REMEMBER = GetConfigKey("MT_REMEMBER").ToString().Trim() == "1" ? true : false;

                   return fInit_SQLConnect();
               }
               else
               {
                   MT_DBHost = GetConfigKey("MT_DBHOST");
                   MT_DBPass = GetConfigKey("MT_DBPASS");
                   if (MT_DBPass != "")
                   {
                       MT_SQLCNN_STR = string.Format(@"Data Source={0};Version=3;Pooling=True;Max Pool Size=3000;datetimeformat=CurrentCulture;Password={1}", System.AppDomain.CurrentDomain.BaseDirectory + MT_DBHost, MT_DBPass);
                   }
                   else
                   {
                       MT_SQLCNN_STR = string.Format(@"Data Source={0};Version=3;Pooling=True;Max Pool Size=3000;datetimeformat=CurrentCulture", System.AppDomain.CurrentDomain.BaseDirectory + MT_DBHost);
                   }

                   MT_USER_LOGIN = GetConfigKey("MT_ACCOUNT");
                   MT_USER_PASS = GetConfigKey("MT_PASSWORD");
                   MT_REMEMBER = GetConfigKey("MT_REMEMBER").ToString().Trim() == "1" ? true : false;
                   return true;
               }

           }
           catch { return false; }         
       }

       public static bool fInit_SQLConnect()
       {
           try
           {
               MT_Cnn = new SqlConnection();
               MT_Cnn.ConnectionString = MT_SQLCNN_STR;
               MT_Cnn.Open();
               return (MT_Cnn.State == System.Data.ConnectionState.Open);
           }
           catch{             
               return false;
           }
       }
       
       public static string GetNewID(){
           try{
               string NEWID = "";
               string DD = "", MM = "", YY = "", HH = "", MI = "", SS = "", SSS = "";
               YY = DateTime.Now.Year.ToString().PadRight(2);
               MM = DateTime.Now.Month.ToString().PadLeft(2, '0');
               DD = DateTime.Now.Day.ToString().PadLeft(2, '0');
               HH = DateTime.Now.Hour.ToString().PadLeft(2, '0');
               MI = DateTime.Now.Minute.ToString().PadLeft(2, '0');
               SS = DateTime.Now.Second.ToString().PadLeft(2, '0');
               SSS = DateTime.Now.Millisecond.ToString().PadLeft(3, '0');

               Guid gKEY = new Guid();
               NEWID = gKEY.ToString().PadLeft(40, '0');
               NEWID = MT_KYHIEU_CTY + MT_KYHIEU_USER + YY + MM + DD + MI + SS + SSS + fRandomNumber(0, 9999).ToString().PadRight(5, 'N');
               return NEWID;
           }
           catch { return ""; }
       }
              
       public static void SetFormatGridControl(DevExpress.Xpf.Grid.GridControl oGrid, DevExpress.Xpf.Grid.TableView oTblView,bool isFilterRow =true)
       {
           try{
               oGrid.View.NavigationStyle = DevExpress.Xpf.Grid.GridViewNavigationStyle.Row;
               oGrid.View.EnterMoveNextColumn = true;
               oTblView.ShowAutoFilterRow = isFilterRow;
               oTblView.FixedLineWidth = 3;
               oTblView.HeaderPanelMinHeight = 40;
               oTblView.IndicatorWidth = 35;
               oTblView.RowMinHeight = 30;
               oTblView.ShowGroupPanel = false;
               oTblView.NavigationStyle = DevExpress.Xpf.Grid.GridViewNavigationStyle.Row;
               oTblView.Cursor = System.Windows.Input.Cursors.Hand;               
               oTblView.EnterMoveNextColumn = true; 
               oTblView.HorizontalScrollbarVisibility = ScrollBarVisibility.Auto;
               oTblView.MultiSelectMode = DevExpress.Xpf.Grid.TableViewSelectMode.Row; 
           }
           catch { }
       }

       public static bool SetGridReadOnly(DevExpress.Xpf.Grid.GridControl oGrid, DevExpress.Xpf.Grid.TableView oTblView, bool isReadOnly = false)
       {
           try{
               if (isReadOnly == true){
                   oGrid.View.CloseEditor();
                   oGrid.View.NavigationStyle = DevExpress.Xpf.Grid.GridViewNavigationStyle.Row;
                   oTblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.None;
               }
               else{
                   oGrid.View.NavigationStyle = DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell;
                   oGrid.View.ShowEditor();
                   oTblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
               }               

               foreach (var Col in oGrid.Columns){
                   if (Col.Tag != null && Col.Tag.ToString().Contains("R"))
                   {
                       Col.ReadOnly = true;
                       Col.AllowFocus = false;                       
                   }
                   else
                   {
                       Col.ReadOnly = isReadOnly;
                   }
               }
           }
           catch { return false; }
           return true;
       }

       public static void SetButtonAction(MT_ROLE oROLE,MT_BUTTONACTION oButton, string isAction){
           MT_CURRENT_ACTION = isAction;
           if (oButton.cmdAdd == null) oButton.cmdAdd = new Button();
           if (oButton.cmdEdit == null) oButton.cmdEdit = new Button();
           if (oButton.cmdDel == null) oButton.cmdDel = new Button();
           if (oButton.cmdSave == null) oButton.cmdSave = new Button();
           if (oButton.cmdAbort == null) oButton.cmdAbort = new Button();
           if (oButton.cmdPrint == null) oButton.cmdPrint = new Button();

           switch (isAction)
           {
               case "INIT":
               case "ABORT":
               case "SAVE":
                   oButton.cmdAdd.Visibility = Visibility.Visible;
                   oButton.cmdEdit.Visibility = Visibility.Visible;
                   oButton.cmdDel.IsEnabled = true;
                   oButton.cmdSave.Visibility = Visibility.Hidden;
                   oButton.cmdAbort.Visibility = Visibility.Hidden;

                   oButton.cmdAdd.IsEnabled = oROLE.isAdd;
                   oButton.cmdEdit.IsEnabled = oROLE.isEdit;
                   oButton.cmdDel.IsEnabled = oROLE.isDel;
                   oButton.cmdPrint.IsEnabled = oROLE.isPrint; 
                   break;

               case "ADD":
                   oButton.cmdAdd.IsEnabled = oROLE.isAdd;
                   oButton.cmdEdit.IsEnabled = oROLE.isEdit;
                   oButton.cmdDel.IsEnabled = oROLE.isDel;
                   oButton.cmdPrint.IsEnabled = oROLE.isPrint;

                   oButton.cmdAdd.Visibility = Visibility.Hidden;
                   oButton.cmdEdit.Visibility = Visibility.Hidden;
                   oButton.cmdDel.IsEnabled = false;
                   oButton.cmdSave.Visibility = Visibility.Visible;
                   oButton.cmdAbort.Visibility = Visibility.Visible;                 
                   break;

               case "EDIT":
                   oButton.cmdAdd.IsEnabled = oROLE.isAdd;
                   oButton.cmdEdit.IsEnabled = oROLE.isEdit;
                   oButton.cmdDel.IsEnabled = oROLE.isDel;
                   oButton.cmdPrint.IsEnabled = oROLE.isPrint;

                   oButton.cmdAdd.Visibility = Visibility.Hidden;
                   oButton.cmdEdit.Visibility = Visibility.Hidden;
                   oButton.cmdDel.IsEnabled = false;
                   oButton.cmdSave.Visibility = Visibility.Visible;
                   oButton.cmdAbort.Visibility = Visibility.Visible;                  
                   break;              
               default:
                   break;
           }
       }

       // Set all permision
       public static void SetPermit(ref MT_ROLE mROLE){
           mROLE.isAdd = true;
           mROLE.isEdit = true;
           mROLE.isDel = true;
           mROLE.isPrint = true;
       }
       // Su dung khi phan quyen
       public static void SetPermit_Offical(ref MT_ROLE mROLE){
           try{
               bool isAdd = false, isEdit = false, isDel = false, isPrint = false;
               string mSQL = string.Format(" select b.* from HT_QUYENHAN a inner join HT_NHOMQUYEN_CHUCNANG b on a.soid_nhomquyen=b.manhom " +
                                         " where a.soid_congty='{0}' and a.soid_nguoidung='{1}' and b.macn='{2}'",MT_ACTIVE_COMPANYID,MT_ACTIVE_USERID, MT_ACTIVE_CAPTION);

               System.Data.DataTable oTbl = MTSQLite.SQLiteReadTBL(mSQL, null);
               if (oTbl != null)
               {
                   foreach (System.Data.DataRow vRow in oTbl.Rows)
                   {
                       bool.TryParse(vRow["them"].ToString(), out isAdd);
                       bool.TryParse(vRow["sua"].ToString(), out isEdit);
                       bool.TryParse(vRow["xoa"].ToString(), out isDel);
                       bool.TryParse(vRow["in"].ToString(), out isPrint);
                       break;
                   }
               }
               mROLE.isAdd = isAdd;
               mROLE.isEdit = isEdit;
               mROLE.isDel = isDel;
               mROLE.isPrint = isPrint;
           }
           catch { }
       }


       public static double GetDonGia(String mMasp, String mDenNgay, String mLoai = "LE"){
           try{
               Double mDG = 0;
               String mSql = String.Format("set dateformat dmy SELECT DBO.fn_DonGia ('{0}','{1}','{2}')", mMasp, mDenNgay, mLoai);

               Double.TryParse(new MTSQLServer().wReadDecimal(mSql).ToString(),out mDG);
               if (mDG == null) mDG = 0;
               return mDG;
           }
           catch { return 0; }
       }

       public static string GetSophieu(String mKyhieuCuaHang, String mLoaiPhieu, int mThang, int mNam, String mNguoiDung)
       {
           try
           {
               if (mKyhieuCuaHang.Length > 2) { mKyhieuCuaHang = mKyhieuCuaHang.Substring(0, 1); }
               String mSql = String.Format("select [dbo].[fn_SoPhieu]('{0}','{1}',{2},{3},'{4}')", mKyhieuCuaHang, mLoaiPhieu, mThang, mNam, mNguoiDung);
               return new MTSQLServer().wReadString(mSql);
           }
           catch (Exception ex) { return "N/A"; }
       }

       public static string SothanhChu(Double total){
           try{
               string rs = "";
               total = Math.Round(total, 0);
               string[] ch = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
               string[] rch = { "lẻ", "mốt", "", "", "", "lăm" };
               string[] u = { "", "mươi", "trăm", "ngàn", "", "", "triệu", "", "", "tỷ", "", "", "ngàn", "", "", "triệu" };
               string nstr = total.ToString();

               int[] n = new int[nstr.Length];
               int len = n.Length;
               for (int i = 0; i < len; i++)
               {
                   n[len - 1 - i] = Convert.ToInt32(nstr.Substring(i, 1));
               }

               for (int i = len - 1; i >= 0; i--){
                   if (i % 3 == 2){
                       if (n[i] == 0 && n[i - 1] == 0 && n[i - 2] == 0) 
                           continue;
                   }
                   else if (i % 3 == 1) 
                   {
                       if (n[i] == 0)
                       {
                           if (n[i - 1] == 0) { continue; }
                           else
                           {
                               rs += " " + rch[n[i]]; continue;
                           }
                       }
                       if (n[i] == 1)
                       {
                           rs += " mười"; continue;
                       }
                   }
                   else if (i != len - 1)
                   {
                       if (n[i] == 0)
                       {
                           if (i + 2 <= len - 1 && n[i + 2] == 0 && n[i + 1] == 0) continue;
                           rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                           continue;
                       }
                       if (n[i] == 1)
                       {
                           rs += " " + ((n[i + 1] == 1 || n[i + 1] == 0) ? ch[n[i]] : rch[n[i]]);
                           rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                           continue;
                       }
                       if (n[i] == 5)
                       {
                           if (n[i + 1] != 0) 
                           {
                               rs += " " + rch[n[i]];
                               rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                               continue;
                           }
                       }
                   }

                   rs += (rs == "" ? " " : ", ") + ch[n[i]];
                   rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
               }
               if (rs[rs.Length - 1] != ' ')
                   rs += " đồng";
               else
                   rs += "đồng";

               if (rs.Length > 2)
               {
                   string rs1 = rs.Substring(0, 2);
                   rs1 = rs1.ToUpper();
                   rs = rs.Substring(2);
                   rs = rs1 + rs;
               }
               return rs.Trim().Replace("lẻ,", "lẻ").Replace("mươi,", "mươi").Replace("trăm,", "trăm").Replace("mười,", "mười");
           }
           catch
           {
               return "";
           } 
       }

       public static String getVND(String vVND) {
           try
           {
               CultureInfo cul = CultureInfo.CurrentCulture;
               string decimalSep = cul.NumberFormat.CurrencyDecimalSeparator;//decimalSep ='.'
               string groupSep = cul.NumberFormat.CurrencyGroupSeparator;//groupSep=','
               String sFormat = string.Format("#{0}###", groupSep);
               return Double.Parse(vVND).ToString(sFormat);
           }
           catch (Exception ex) { return vVND; }
        
       }

       public static void BlankZero(Object sender){
         try{
            var mVal=((DevExpress.XtraReports.UI.XRLabel)sender).Text;
             if(mVal!="" && Convert.ToDouble(mVal)==0){
               ((DevExpress.XtraReports.UI.XRLabel)sender).Text="";
             }
         }catch{}
       }


       public static void onSetPara()
       {
           try
           {               
               String mSql = String.Format("select PARA_NAME,PARA_VAL,PARA_DESC from HT_PARA WHERE PARA_TYPE IN('HT','RPT')");
               DataTable oTblSrc = new MTSQLServer().wRead(mSql, null, false);
               if (oTblSrc != null)
               {
                   MTGlobal.HT_POS_IS_ACTIVE = false;
                   foreach (DataRow vR in oTblSrc.Rows)
                   {
                       if (vR["PARA_NAME"].ToString().Equals("PARA_CUS"))
                       {
                           HT_POS_CUSTOMERNAME = vR["PARA_VAL"].ToString();
                       }
                       if (vR["PARA_NAME"].ToString().Equals("PAR_IMEI"))
                       {
                           HT_POS_IMEI = vR["PARA_VAL"].ToString();
                       }
                       if (vR["PARA_NAME"].ToString().Equals("PAR_PHONE"))
                       {
                           HT_POS_TEL = vR["PARA_VAL"].ToString();
                       }
                       if (vR["PARA_NAME"].ToString().Equals("PAR_ADDR"))
                       {
                           HT_POS_ADDRESS = vR["PARA_VAL"].ToString();
                       }
                       if (vR["PARA_NAME"].ToString().Equals("PAR_ACTIVE"))
                       {
                           switch (vR["PARA_VAL"].ToString())
                           {
                               case "SYNC_WAIT":
                                   MTGlobal.HT_POS_ACTIVE = "Phần mềm đã đăng ký. Đang chờ duyệt..";
                                   break;

                               case "SYNC_LOCK":
                                   MTGlobal.HT_POS_ACTIVE = "Phần mềm đã bị khóa. Vui lòng liện hệ Admin..";
                                   break;

                               case "SYNC_NOT_REG":
                                   MTGlobal.HT_POS_ACTIVE = "Phần mềm chưa gửi thông tin đăng ký..";
                                   break;

                               case "SYNC_ACTIVE":
                                   MTGlobal.HT_POS_ACTIVE = "Phần mềm đã kích hoạt..";
                                   MTGlobal.HT_POS_IS_ACTIVE = true;
                                   break;
                               default:
                                   MTGlobal.HT_POS_ACTIVE = "Phần mềm Chưa kích hoạt";
                                   break;
                           }
                               
                       }

                       if (vR["PARA_NAME"].ToString().Equals("SYS_INIT_DATE"))
                       {
                           HT_SYS_INIT_DATE = vR["PARA_VAL"].ToString();
                       }
                       if (vR["PARA_NAME"].ToString().Equals("RPT_COMPANY"))
                       {
                           RPT_COMPANY = vR["PARA_VAL"].ToString();
                       }
                       if (vR["PARA_NAME"].ToString().Equals("RPT_ADDRESS"))
                       {
                           RPT_ADDRESS = vR["PARA_VAL"].ToString();
                       }
                       if (vR["PARA_NAME"].ToString().Equals("RPT_TAX"))
                       {
                           RPT_TAX = vR["PARA_VAL"].ToString();
                       }
                       if (vR["PARA_NAME"].ToString().Equals("RPT_TEL"))
                       {
                           RPT_TEL = vR["PARA_VAL"].ToString();
                       }
                   }
               }
           }
           catch { }
       }

       public static void Ketchuyensodu(){
           String mThangnam = MT_THANG.Trim().Replace("/", "");
           try{             
               if (mThangnam == "") {
                   Utils.showMessage("Bạn chưa chọn tháng kết chuyển số dư", "Thông báo");
                   return;
               }

               if (Utils.ConfirmMessage(String.Format("Bạn có chắc muốn kết số dư tháng {0} không?", mThangnam), "Xác nhận"))
               {
                   SqlParameter[] arrPara = new SqlParameter[3];
                   arrPara[0] = new SqlParameter("@Thangnam", SqlDbType.NVarChar, 6);
                   arrPara[0].Value = mThangnam;
                   arrPara[1] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
                   arrPara[1].Value = MT_USER_LOGIN;
                   arrPara[2] = new SqlParameter("@Output", SqlDbType.TinyInt);
                   arrPara[2].Direction = ParameterDirection.Output;

                   new MTSQLServer().wExec("spHT_Ketsoton", arrPara);
                   if (arrPara[2].Value.ToString() == "1") {
                       Utils.showMessage("Bạn chưa kết số dư tháng trước..", "Thông báo");
                       return;
                   }

                   Utils.showMessage(String.Format("Kết chuyển số dư tháng {0} thành công.", mThangnam), "Thông báo");
               }
           }
           catch (Exception ex) {
               Utils.showMessage(String.Format("Không thể Kết chuyển số dư tháng {0}. Lỗi: "+ ex.Message.ToString(), mThangnam), "Thông báo");
           }

           
       }
        
    #endregion

 
    
  }
}
