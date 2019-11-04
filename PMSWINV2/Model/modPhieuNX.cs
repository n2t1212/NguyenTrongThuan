using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MTPMSWIN.Model
{
    public class modPhieuNX{       
        public modPhieuNX(){}
 
        public DataTable dtPhieuNX()
        {
            DataTable PhieuNX = new DataTable();
            PhieuNX.Columns.Add("Phieunxid", typeof(String));
            PhieuNX.Columns.Add("Sophieu", typeof(String));
            PhieuNX.Columns.Add("Loaiphieu", typeof(String));
            PhieuNX.Columns.Add("Makho", typeof(String));
            PhieuNX.Columns.Add("Mald", typeof(String));
            PhieuNX.Columns.Add("Makh", typeof(String));
            PhieuNX.Columns.Add("Ngayct", typeof(DateTime));
            PhieuNX.Columns.Add("Ngayps", typeof(DateTime));
            PhieuNX.Columns.Add("Soctgoc", typeof(String));
            PhieuNX.Columns.Add("Loaitien", typeof(String));
            PhieuNX.Columns.Add("Tygia", typeof(Double));
            PhieuNX.Columns.Add("Nguyente", typeof(Double));
            PhieuNX.Columns.Add("Vat", typeof(Double));
            PhieuNX.Columns.Add("TTVat", typeof(Double));
            PhieuNX.Columns.Add("Thanhtien", typeof(Double));
            PhieuNX.Columns.Add("Ghichu", typeof(String));
            PhieuNX.Columns.Add("Giaonhan", typeof(String));
            PhieuNX.Columns.Add("Ngaylap", typeof(DateTime));
            PhieuNX.Columns.Add("Nguoilap", typeof(String));
            PhieuNX.Columns.Add("Ngaysua", typeof(DateTime));
            PhieuNX.Columns.Add("Nguoisua", typeof(String));
            PhieuNX.Columns.Add("Dongbo", typeof(int));
            PhieuNX.Columns.Add("TKNo", typeof(String));
            PhieuNX.Columns.Add("TKCo", typeof(String));
            return PhieuNX; 
        }

        public DataTable dtPhieuNXCT()
        {
            DataTable PhieuNXCT = new DataTable();
            PhieuNXCT.Columns.Add("Phieunxctid", typeof(String));
            PhieuNXCT.Columns.Add("Phieunxid", typeof(String));
            PhieuNXCT.Columns.Add("Maspid", typeof(String));
            PhieuNXCT.Columns.Add("Masp", typeof(String));
            PhieuNXCT.Columns.Add("Tensp", typeof(String));
            PhieuNXCT.Columns.Add("Quycach", typeof(String));
            PhieuNXCT.Columns.Add("Dvt", typeof(String));
            PhieuNXCT.Columns.Add("SLThung", typeof(Double));
            PhieuNXCT.Columns.Add("Soluong", typeof(Double));
            PhieuNXCT.Columns.Add("Dongia", typeof(Double));
            PhieuNXCT.Columns.Add("Nguyente", typeof(Double));
            PhieuNXCT.Columns.Add("Vat", typeof(Double));
            PhieuNXCT.Columns.Add("TTVat", typeof(Double));
            PhieuNXCT.Columns.Add("Thanhtien", typeof(Double));
            PhieuNXCT.Columns.Add("Songaychono", typeof(Double));
            PhieuNXCT.Columns.Add("Ghiso", typeof(Boolean));
            PhieuNXCT.Columns.Add("Ghichu", typeof(String));
            return PhieuNXCT;
        }
         
        public string SavePhieuNX(System.Data.DataTable tblPXN, System.Data.DataTable tblPXNCT) {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0] = new SqlParameter("@tblPhieunx", SqlDbType.Structured);
                arrPara[0].Value = tblPXN;
                arrPara[1] = new SqlParameter("@tblPphieunxct", SqlDbType.Structured);
                arrPara[1].Value = tblPXNCT;
            
                arrPara[2] = new SqlParameter("@nguoidung", SqlDbType.NVarChar,50);
                arrPara[2].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[3] = new SqlParameter("@ketqua", SqlDbType.NVarChar, 255);
                arrPara[3].Direction=ParameterDirection.Output;
                int iRs = new MTSQLServer().wExec("spNX_AddPhieuNX", arrPara);
                return arrPara[3].Value.ToString();
            }
            catch (Exception ex) {
                return ex.Message.ToString();
            }
        }

        public bool DelPhieuNX(String mPhieunxid)
        {
            try
            {
                if (mPhieunxid == "") {
                    Utils.showMessage("Bạn chưa chọn phiếu cần xóa..", "Thông báo");
                    return false;
                }

                SqlParameter[] arrPara = new SqlParameter[3];
                arrPara[0] = new SqlParameter("@Phieunxid", SqlDbType.NVarChar,50);
                arrPara[0].Value =mPhieunxid;
                arrPara[1] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 50);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[2] = new SqlParameter("@ketqua", SqlDbType.NVarChar,100);
                arrPara[2].Direction = ParameterDirection.Output;
                int iRs = new MTSQLServer().wExec("spNX_DelPhieuNX", arrPara);
                if (arrPara[2].Value.ToString()==""){
                  return true;
                }else{
                    Utils.showMessage(arrPara[2].ToString(), "Thông báo", "ERR");
                    return false;
                } 
            }
            catch (Exception ex)
            {
                Utils.showMessage("Thông tin lỗi: "+ex.Message.ToString(), "Thông báo", "ERR");
                return false;
            }
        }
    }
}
