using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MTPMSWIN.View;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace MTPMSWIN.Model
{
    public class Utils
    {
        public static System.Data.DataTable otblChon;
        public static String SELECT_ROW_TO_DELETE = "Vui lòng chọn dòng để xoá.";

        public static String SAVE_DB_OK = "Cập nhật dữ liệu thành công.";

        public static String ERR_UPDATE_DB = "Lỗi. Không thể cập nhật dữ liệu.";
         
        public static String DATA_CAN_SYNC = "Dữ liệu đang được đồng bộ với server";

        public static String REGISTER_OK = "Đăng kí thành công. Thông tin đang chờ server xử lý.";

        public static String REGISTER_NOK = "Lỗi. Đăng kí không thành công.";

        public static String ERR_CONNECT_DB = "Lỗi. Không thể kết nối CSDL.";

        public static string EXCEL_TYPE = "EXCEL";

        public static string EXCEL_EXTENSION_1 = ".xlsx";

        public static string EXCEL_EXTENSION_2 = ".xls";

        public static string BACKUP_SQL_TYPE = "BAK";

        public static string BACKUP_SQL_EXTENSION = ".bak";

        public static String ERR_DUPLICATE_VALUE_OF_FIELD(String field)
        {
            return String.Format("Trường {0} đã tồn tại trong hệ thống.", field);
        }

        public static String ERR_NOT_ALLOW_EMPTY_FIELD(String field)
        {
            return String.Format("Trường {0} không được để trống", field);
        }

        public static String ERR_FIELD_IS_NOT_NUMBER(String field)
        {
            return String.Format("Trường {0} phải là dạng số", field);
        }

        public static string ERR_FILE_FORMAT(string extension)
        {
            return string.Format("Định dạng file không hợp lệ. Định dạng đúng là {0}", extension);
        }

        public static void showMessage(String mMsg="", String mTitle="",String mType = "MSG" ) {
            switch (mType) { 
                case "MSG":
                   //MessageBox.Show(mMsg, mTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                   DevExpress.Xpf.Core.DXMessageBox.Show(mMsg, mTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                   break;
                case "ERR":
                    //MessageBox.Show(mMsg, mTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                   DevExpress.Xpf.Core.DXMessageBox.Show(mMsg, mTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                   break;
                default:
                   break;
            }
        }
        public static bool ConfirmMessage(String mMsg = "", String mTitle = "", String mType = "N")
        {
            switch (mType)
            {
                case "N":
                    /*
                    if (MessageBox.Show(mMsg, mTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }*/
                    if (DevExpress.Xpf.Core.DXMessageBox.Show(mMsg, mTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    break;
            }
            return false;
        }

        public static DevExpress.Utils.WaitDialogForm shwWait(String mMsg = "Đang nạp dữ liệu, vui lòng đợi...", String mTitle = "Đang xử lý...") {
            DevExpress.Utils.WaitDialogForm Dlg = null;
            try{
                Dlg = new DevExpress.Utils.WaitDialogForm(mMsg, mTitle);
                Dlg.ShowDialog();
            }
            catch (Exception ex) { };
            return Dlg;
        }
        public static void closeWait(DevExpress.Utils.WaitDialogForm Dlg)
        {
            try
            {
                if (Dlg != null) {
                    Dlg.Close();
                } 
            }
            catch (Exception ex) { };
        }

        public static bool ChonThoiGian(bool isNgay = true, bool isThang = true, bool isNam = true) {
            try
            {
                dlg_ChonNgay dlgChon = new dlg_ChonNgay();
                dlgChon.setPermit(isNgay, isThang, isNam);
                dlgChon.ShowDialog();
                return dlgChon.isChon;
            }
            catch { return false; }
        }

        public static bool ChonPhieu(String mType){
            try{
              dlg_ChonPhieu oChonPhieu;
                switch (mType) {
                    case "PX":
                    case "PN":
                    case "CX":
                        oChonPhieu = new dlg_ChonPhieu(mType);
                        oChonPhieu.ShowDialog();
                        otblChon = oChonPhieu.otblChon;
                        return oChonPhieu.isChon; 
                    case "SP":
                        oChonPhieu = new dlg_ChonPhieu(mType);
                        oChonPhieu.ShowDialog();
                        otblChon = oChonPhieu.otblChon;
                        return oChonPhieu.isChon; 
                    default:
                        break;

                }                
                return true; 
            }
            catch { return false; }
        }

        public static bool ChonQuay(String mLoaiPhieu)
        {
            try
            {
                if (mLoaiPhieu==MTGlobal.BH){
                          dlg_ChonQuay dlgChon = new dlg_ChonQuay(); 
                          dlgChon.ShowDialog();
                        return dlgChon.isChon;

                }
                return false;
              
            }
            catch { return false; }
        }
        
        public static bool validateFileType(string filename, string FILE_TYPE)
        {
            string ext = Path.GetExtension(filename);

            if (FILE_TYPE == BACKUP_SQL_TYPE)
            {
                if (ext.ToLower() == BACKUP_SQL_EXTENSION)
                {
                    return true;
                }
            }

            if(FILE_TYPE == EXCEL_TYPE)
            {
                if (ext.ToLower() == EXCEL_EXTENSION_1 || ext.ToLower() == EXCEL_EXTENSION_2)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ChonInmavach()
        {
            try
            {
                dlg_ChonInmavach dlgChon = new dlg_ChonInmavach(); 
                dlgChon.ShowDialog();
                return dlgChon.isChon;
            }
            catch { return false; }
        }

        public static string convertToUnsignedString(string s)
        {
            if (s == null || s.Length == 0)
            {
                return "";
            }
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static void fExit()
        {
            try
            {
                MTMain oMain = System.Windows.Application.Current.Windows.OfType<MTMain>().FirstOrDefault();
                if (oMain != null)
                {
                    oMain.onExitTab();
                }
            }
            catch { }
        }

        public static bool isDatabaseExisted(string databaseName)
        {
            string sqlCheck = "SELECT * FROM sys.databases WHERE name = N'" + databaseName + "'";
            SqlCommand cmdCheck = new SqlCommand(sqlCheck, MTGlobal.MT_Cnn);

            try
            {
                object rs = cmdCheck.ExecuteScalar();
                return rs != null && rs.ToString().Length > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool createAndRestoreDB(string server, string username, string password, string databaseName, string restoreFile)
        {
            try
            {
                string sqlCreateDB = "IF  NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'" + databaseName + "') BEGIN CREATE DATABASE [" + databaseName + "] END";

                string sqlRestore_x64 = @"USE master RESTORE DATABASE [" + databaseName + "] FROM  DISK = N'" + @restoreFile +
                    "' WITH  FILE = 1, MOVE N'MTPOS' TO N'" + MTGlobal.MT_SQL_DATA_PATH_x64 + databaseName +
                    ".mdf', MOVE N'MTPOS_log' TO N'" + MTGlobal.MT_SQL_DATA_PATH_x64 + databaseName + ".ldf',NOUNLOAD, REPLACE, STATS = 10";

                string sqlRestore_x86 = @"USE master RESTORE DATABASE [" + databaseName + "] FROM  DISK = N'" + @restoreFile +
                    "' WITH  FILE = 1, MOVE N'MTPOS' TO N'" + MTGlobal.MT_SQL_DATA_PATH_x86 + databaseName +
                    ".mdf', MOVE N'MTPOS_log' TO N'" + MTGlobal.MT_SQL_DATA_PATH_x86 + databaseName + ".ldf',NOUNLOAD, REPLACE, STATS = 10";

                string connectionString = string.Format(@"Data Source={0};Persist Security Info=True;User ID={1};Password={2};Max Pool Size=2000", server, username, password);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Create DB if any
                    SqlCommand sqlCmdCreateDB = new SqlCommand(sqlCreateDB, con);
                    sqlCmdCreateDB.ExecuteNonQuery();

                    // Restore DB
                    try
                    {
                        SqlCommand sqlCmdRestore_x64 = new SqlCommand(sqlRestore_x64, con);
                        sqlCmdRestore_x64.ExecuteNonQuery();
                        MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBNAME, databaseName);
                        return true;
                    }
                    catch
                    {
                        SqlCommand sqlCmdRestore_x86 = new SqlCommand(sqlRestore_x86, con);
                        sqlCmdRestore_x86.ExecuteNonQuery();
                        MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBNAME, databaseName);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Không thể tạo CSDL. Vui lòng kiểm tra lại", "Thông báo");
                return false;
            }
        }

        public static bool createSQLServerUser(string username, string password)
        {
            try
            {
                string connectionString = string.Format(@"Data Source={0};Persist Security Info=True;Max Pool Size=2000;Trusted_Connection=True;", ".\\SQL2008");
                string sqlCreateUser = "CREATE LOGIN " + username + " WITH PASSWORD = '" + password + "', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF";
                string sqlGrantPrivilege = "USE master GRANT CREATE ANY DATABASE TO " + username;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand sqlCmdCreateUser = new SqlCommand(sqlCreateUser, con);
                    sqlCmdCreateUser.ExecuteNonQuery();

                    SqlCommand sqlCmdGrantPrivilege = new SqlCommand(sqlGrantPrivilege, con);
                    sqlCmdGrantPrivilege.ExecuteNonQuery();

                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBUSER, username);
                    MTGlobal.WriteRegistryKey(MTGlobal.MT_REGKEY_SECTION_SQL, MTGlobal.MT_REGKEY_DBPASS, password);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }   
}
