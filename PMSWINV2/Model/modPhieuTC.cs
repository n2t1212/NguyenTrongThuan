using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MTPMSWIN.Model
{
    public class modPhieuTC
    {
        public modPhieuTC()
        {
        }

        public DataTable dtPhieuTC()
        {
            DataTable PhieuTC = new DataTable();

            PhieuTC.Columns.Add("Phieutcid", typeof(String));
            PhieuTC.Columns.Add("Mant", typeof(String));
            PhieuTC.Columns.Add("Madt", typeof(String));
            PhieuTC.Columns.Add("Loaiphieu", typeof(String));
            PhieuTC.Columns.Add("Sophieu", typeof(String));
            PhieuTC.Columns.Add("Ngayct", typeof(DateTime));
            PhieuTC.Columns.Add("Ngayps", typeof(DateTime));
            PhieuTC.Columns.Add("Tygia", typeof(Double));
            PhieuTC.Columns.Add("Soctgoc", typeof(String));
            PhieuTC.Columns.Add("TKDu", typeof(String));
            PhieuTC.Columns.Add("Nguyente", typeof(Double));
            PhieuTC.Columns.Add("Sotien", typeof(Double));
            PhieuTC.Columns.Add("Hoten", typeof(String));
            PhieuTC.Columns.Add("Nguoilap", typeof(String));
            PhieuTC.Columns.Add("Ngaylap", typeof(DateTime));
            PhieuTC.Columns.Add("Nguoisua", typeof(String));
            PhieuTC.Columns.Add("Ngaysua", typeof(DateTime));
            PhieuTC.Columns.Add("Ghiso", typeof(int));
            PhieuTC.Columns.Add("Mald", typeof(String));
            PhieuTC.Columns.Add("Ghichu", typeof(String));
            PhieuTC.Columns.Add("Loaitien", typeof(String));

            return PhieuTC;
        }

        public DataTable dtPhieuTCCT()
        {
            DataTable PhieuTCCT = new DataTable();

            PhieuTCCT.Columns.Add("Phieutcctid", typeof(String));
            PhieuTCCT.Columns.Add("Phieutcid", typeof(String));
            PhieuTCCT.Columns.Add("Macp", typeof(String));
            PhieuTCCT.Columns.Add("Madt", typeof(String));
            PhieuTCCT.Columns.Add("Matk", typeof(String));
            PhieuTCCT.Columns.Add("TKNo", typeof(String));
            PhieuTCCT.Columns.Add("TKCo", typeof(String));
            PhieuTCCT.Columns.Add("Nguyente", typeof(Double));
            PhieuTCCT.Columns.Add("Thanhtien", typeof(Double));
            PhieuTCCT.Columns.Add("Diengiai", typeof(String));
            PhieuTCCT.Columns.Add("SoHD", typeof(String));
            PhieuTCCT.Columns.Add("NChono", typeof(Double));

            return PhieuTCCT;
        }

        public string SavePhieuTC(System.Data.DataTable tblPTC, System.Data.DataTable tblPTCCT)
        {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0] = new SqlParameter("@tblPhieutc", SqlDbType.Structured);
                arrPara[0].Value = tblPTC;
                arrPara[1] = new SqlParameter("@tblPhieutcct", SqlDbType.Structured);
                arrPara[1].Value = tblPTCCT;

                arrPara[2] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 50);
                arrPara[2].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[3] = new SqlParameter("@ketqua", SqlDbType.NVarChar, 255);
                arrPara[3].Direction = ParameterDirection.Output;
                int iRs = new MTSQLServer().wExec("sp_TC_AddPhieuTC", arrPara);
                return arrPara[3].Value.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public bool DelPhieuTC(String mPhieutcid)
        {
            try
            {
                if (mPhieutcid == "")
                {
                    Utils.showMessage("Bạn chưa chọn phiếu cần xóa..", "Thông báo");
                    return false;
                }

                SqlParameter[] arrPara = new SqlParameter[3];
                arrPara[0] = new SqlParameter("@Phieutcid", SqlDbType.NVarChar, 50);
                arrPara[0].Value = mPhieutcid;
                arrPara[1] = new SqlParameter("@Nguoidung", SqlDbType.NVarChar, 50);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[2] = new SqlParameter("@ketqua", SqlDbType.NVarChar, 100);
                arrPara[2].Direction = ParameterDirection.Output;
                int iRs = new MTSQLServer().wExec("sp_TC_DelPhieuTC", arrPara);
                if (arrPara[2].Value.ToString() == "")
                {
                    return true;
                }
                else
                {
                    Utils.showMessage(arrPara[2].ToString(), "Thông báo", "ERR");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utils.showMessage("Thông tin lỗi: " + ex.Message.ToString(), "Thông báo", "ERR");
                return false;
            }
        }
    }
}
