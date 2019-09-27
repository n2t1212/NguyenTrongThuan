using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO; 
using System.Globalization;
using System.Xml;

using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;

using Newtonsoft.Json;

namespace MTPMSWIN.Model
{
    public static class MTSynData
    {

        private static string mMAP_API_ACTION = "MTAPI2018/fSyncDM";
        private static string[] mTBL_LIST = new string[] { "HT_CATEGORIE", "HT_MEMBER", "HT_CATEGORIE_GROUPROLE", "HT_COMPANY" };


        public static string mGetAction_Register = "MIMIAPI/fPosSyncRegister";
        public static string mGetAction_Para = "MIMIAPI/fPosSyncPara";
        public static string mPostAction = "MIMIAPI/fPosSyncPost";

        
        public static bool doSyncPost(){
            try{
                if (ValidateHelper.IsNetworkActive()){
                    String mSql = String.Format("exec spNX_SyncPost '{0}'", MTGlobal.MT_USER_LOGIN);
                    DataSet oDset = new MTSQLServer().wDset(mSql, null, false);
                    if (oDset != null){
                        String mPostResult = "";

                        if (oDset.Tables.Count > 0){
                            if (oDset.Tables[0].Rows.Count > 0)
                            {
                                String mDataNX = GetJsonDataPost(oDset.Tables[0], "tblNX");
                                if (mDataNX != "")
                                {
                                    mPostResult = fSyncPost(mPostAction, MTGlobal.HT_POS_IMEI, "NX", mDataNX);
                                    if (mPostResult!=null && mPostResult.Contains("SYNC_OK") && mPostResult.Length > 20)
                                    {
                                        fPostConfirm(mPostResult, "NX");
                                    }
                                }
                            }
                        }
                        if (oDset.Tables.Count > 1)
                        {
                            if (oDset.Tables[1].Rows.Count > 0)
                            {
                                String mDataBH = GetJsonDataPost(oDset.Tables[1],"tblBH");
                                if (mDataBH != "")
                                {
                                    mPostResult = fSyncPost(mPostAction, MTGlobal.HT_POS_IMEI, "BH", mDataBH);
                                    if (mPostResult != null && mPostResult.Contains("SYNC_OK") && mPostResult.Length > 20)
                                    {
                                        fPostConfirm(mPostResult, "BH");
                                    }
                                }
                            }
                        }
                        if (oDset.Tables.Count > 2)
                        {
                            if (oDset.Tables[2].Rows.Count > 0)
                            {
                                String mDataCX = GetJsonDataPost(oDset.Tables[2], "tblCX");
                                if (mDataCX != "")
                                {
                                    mPostResult = fSyncPost(mPostAction, MTGlobal.HT_POS_IMEI, "CX", mDataCX);
                                    if (mPostResult != null && mPostResult.Contains("SYNC_OK") && mPostResult.Length > 20)
                                    {
                                        fPostConfirm(mPostResult, "CX");
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch { return false; }
            return true;
        }

        private static void fPostConfirm(String mRs, String mPosType) {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrPara = new System.Data.SqlClient.SqlParameter[2];
                arrPara[0] = new System.Data.SqlClient.SqlParameter("@PostResult", SqlDbType.NVarChar, 4000);
                arrPara[0].Value = mRs;
                arrPara[1] = new System.Data.SqlClient.SqlParameter("@PosType", SqlDbType.NVarChar,10);
                arrPara[1].Value = mPosType;
                new MTSQLServer().wExec("spNX_SyncPostConfirm", arrPara);
            }
            catch { }
        }


        public static string doSyncCheckActive() {
            String mReturn = "";
            try{
                String mRs = fSyncGet(mGetAction_Para, "posimei=" + MTGlobal.HT_POS_IMEI);
                if (mRs != "") {
                   DataTable oTble = JsonConvert.DeserializeObject<System.Data.DataTable>(mRs);
                   if (oTble != null) {
                       foreach (DataRow vR in oTble.Rows) {
                           string PARAVAL = "";
                           if (vR["PARA_NAME"].ToString().Equals("PAR_ACTIVE"))
                           {
                               PARAVAL = vR["PARA_VAL"].ToString();
                               String mSql = String.Format("UPDATE HT_PARA SET PARA_VAL='{0}' WHERE PARA_NAME='{1}'", PARAVAL, vR["PARA_NAME"].ToString());
                               new MTSQLServer().wExec(mSql, null, false); 

                               switch(PARAVAL){
                                   case "SYNC_WAIT":
                                       mReturn = "Phần mềm đã đăng ký. Đang chờ duyệt..";
                                       break;

                                   case "SYNC_LOCK":
                                       mReturn = "Phần mềm đã bị khóa. Vui lòng liện hệ Admin..";
                                       break;

                                   case "SYNC_NOT_REG":
                                       mReturn = "Phần mềm chưa gửi thông tin đăng ký..";
                                       break;

                                   case "SYNC_ACTIVE":
                                       mReturn = "Phần mềm đã kích hoạt..";
                                       MTGlobal.HT_POS_IS_ACTIVE = true;
                                       break;

                                   default:
                                       mReturn = "Phần mềm Chưa kích hoạt";
                                       break;
                               }
                           }
                       }
                   }
                }
            }
            catch { return ""; }
            return mReturn;
        }

        public static String fSyncGet(String mPosAction="",String mUrlPara=""){
            //string HTTP_HOST = MTGlobal.GetConfigKey("MT_HTTP_SERVER");
            string HTTP_HOST = MTGlobal.MT_API_HOST;
            string HTTP_URL = ""; 
            var DataRs = "";
            try
            {
                if (HTTP_HOST != "")
                {
                    HTTP_URL = string.Format(HTTP_HOST + "/" + mPosAction+ "?"+ mUrlPara);
                    using (HttpClient oHttp = new HttpClient())
                    {
                        oHttp.BaseAddress = new Uri(HTTP_HOST);
                        oHttp.DefaultRequestHeaders.Accept.Clear();
                        oHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage Resp = oHttp.GetAsync(HTTP_URL).Result;
                        Resp.EnsureSuccessStatusCode();
                        //var DataRs = Resp.Result.Content.ReadAsStringAsync(); 
                        if (Resp.IsSuccessStatusCode)
                        {
                             DataRs = Resp.Content.ReadAsStringAsync().Result.ToString();
                            //myDataSet = JsonConvert.DeserializeObject<System.Data.DataSet>(DataRs.Result.ToString());
                        }
                    }
                }
                return DataRs;
            }
            catch { return null; }
        }
        
        public static String fSyncPost(String mPosAction="",string mPosImei = "POS_IMEI",String mPostType="",String mPostJsonData="") // As Response
        {
            //string HTTP_HOST = MTGlobal.GetConfigKey("MT_HTTP_SERVER");
            string HTTP_HOST = MTGlobal.MT_API_HOST;
            String mPostUrl = HTTP_HOST + "/" + mPosAction;
            HttpWebRequest oWebRequest = (HttpWebRequest)WebRequest.Create(mPostUrl);
            oWebRequest.ContentType = "application/json";
            oWebRequest.Method = "POST";

            oWebRequest.Headers.Add("POS_IMEI", mPosImei);
            oWebRequest.Headers.Add("POS_TYPE", mPostType);
            using (StreamWriter streamWriter = new StreamWriter(oWebRequest.GetRequestStream()))
            {
                streamWriter.Write(mPostJsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            HttpWebResponse oWebResponse = (HttpWebResponse)oWebRequest.GetResponse();

            var result="";
            using (var streamReader = new StreamReader(oWebResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result.ToString();
        }
        
        public static string GetJsonDataPost(System.Data.DataTable dt,String dtName){           
            dt.TableName =dtName;
            string rowsep = "{";
            StringBuilder sb = new StringBuilder(string.Format("["));
            foreach (System.Data.DataRow row in dt.Rows)
            {
                string fldsep = string.Empty;
                sb.Append(rowsep);
                foreach (System.Data.DataColumn col in dt.Columns)
                {
                    sb.AppendFormat("{0}\"{1}\":\"{2}\"", fldsep, col.ColumnName, row[col].ToString());
                    fldsep = ",";
                }
                sb.Append("}");
                rowsep = ",{";
            }
            sb.Append("]");

            return sb.ToString();
        }



    }
}
