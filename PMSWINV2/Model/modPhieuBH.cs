using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MTPMSWIN.Model
{
    public class modPhieuBH{
        public modPhieuBH() { }
 
        public DataTable dtPhieuBH(){
            DataTable PhieuBH = new DataTable();
            PhieuBH.Columns.Add("Phieubhid", typeof(String));
            PhieuBH.Columns.Add("Sophieu", typeof(String));
            PhieuBH.Columns.Add("Loaiphieu", typeof(String));
            PhieuBH.Columns.Add("Makho", typeof(String));
            PhieuBH.Columns.Add("Mald", typeof(String));
            PhieuBH.Columns.Add("Makh", typeof(String));
            PhieuBH.Columns.Add("Tenkh", typeof(String));
            PhieuBH.Columns.Add("Dienthoai", typeof(String));
            PhieuBH.Columns.Add("Diachi", typeof(String));
            PhieuBH.Columns.Add("LoaiKH", typeof(String));
            PhieuBH.Columns.Add("Khmoi", typeof(Boolean));
            PhieuBH.Columns.Add("Ngayct", typeof(DateTime));
            PhieuBH.Columns.Add("Ngayps", typeof(DateTime));
            PhieuBH.Columns.Add("Quay", typeof(String));
            PhieuBH.Columns.Add("Ca", typeof(String));
            PhieuBH.Columns.Add("Thungan", typeof(String));
            PhieuBH.Columns.Add("Nguyente", typeof(Double));
            PhieuBH.Columns.Add("Vat", typeof(Double));
            PhieuBH.Columns.Add("TTVat", typeof(Double));
            PhieuBH.Columns.Add("Ck", typeof(Double));
            PhieuBH.Columns.Add("TTCk", typeof(Double));
            PhieuBH.Columns.Add("Thanhtien", typeof(Double));
            PhieuBH.Columns.Add("Tientra", typeof(Double));
            PhieuBH.Columns.Add("Tienthoi", typeof(Double));
            PhieuBH.Columns.Add("Ghichu", typeof(String));
            PhieuBH.Columns.Add("Ngaylap", typeof(DateTime));
            PhieuBH.Columns.Add("Nguoilap", typeof(String));
            PhieuBH.Columns.Add("Ngaysua", typeof(DateTime));
            PhieuBH.Columns.Add("Nguoisua", typeof(String));
            PhieuBH.Columns.Add("Dongbo", typeof(Boolean));
            PhieuBH.Columns.Add("TKNo", typeof(String));
            PhieuBH.Columns.Add("TKCo", typeof(String));
            return PhieuBH; 
        }

        public DataTable dtPhieuBHCT()
        {
            DataTable PhieuBHCT = new DataTable();
            PhieuBHCT.Columns.Add("Phieubhctid", typeof(String));
            PhieuBHCT.Columns.Add("Phieubhid", typeof(String));
            PhieuBHCT.Columns.Add("Mavach", typeof(String));
            PhieuBHCT.Columns.Add("Maspid", typeof(String));
            PhieuBHCT.Columns.Add("Masp", typeof(String));
            PhieuBHCT.Columns.Add("Tensp", typeof(String));
            PhieuBHCT.Columns.Add("Quycach", typeof(String));
            PhieuBHCT.Columns.Add("Dvt", typeof(String));
            PhieuBHCT.Columns.Add("SLThung", typeof(Double));
            PhieuBHCT.Columns.Add("Soluong", typeof(Double));           
            PhieuBHCT.Columns.Add("Dongia", typeof(Double));
            PhieuBHCT.Columns.Add("Nguyente", typeof(Double));
            PhieuBHCT.Columns.Add("Vat", typeof(Double));
            PhieuBHCT.Columns.Add("TTVat", typeof(Double));
            PhieuBHCT.Columns.Add("Ck", typeof(Double));
            PhieuBHCT.Columns.Add("TTCk", typeof(Double));
            PhieuBHCT.Columns.Add("thanhtien", typeof(Double));
            PhieuBHCT.Columns.Add("Quatang", typeof(Boolean));
            PhieuBHCT.Columns.Add("Ghiso", typeof(Boolean));
            PhieuBHCT.Columns.Add("ghichu", typeof(String));

            return PhieuBHCT;
        }
        /*
        public DataTable dtPhieuBHKM()
        {
            DataTable PhieuBHKM = new DataTable();
            PhieuBHKM.Columns.Add("Phieubhkmid", typeof(String));
            PhieuBHKM.Columns.Add("Phieubhid", typeof(String));
            PhieuBHKM.Columns.Add("Mavach", typeof(String));
            PhieuBHKM.Columns.Add("Maspid", typeof(String));
            PhieuBHKM.Columns.Add("Masp", typeof(String));
            PhieuBHKM.Columns.Add("Tensp", typeof(String));
            PhieuBHKM.Columns.Add("Quycach", typeof(String));
            PhieuBHKM.Columns.Add("Dvt", typeof(String));
            PhieuBHKM.Columns.Add("Soluong", typeof(Double));
            PhieuBHKM.Columns.Add("Dongia", typeof(Double));
            PhieuBHKM.Columns.Add("Nguyente", typeof(Double)); 
            PhieuBHKM.Columns.Add("thanhtien", typeof(Double));
            PhieuBHKM.Columns.Add("ghichu", typeof(String));
            return PhieuBHKM;
        }*/
        
        public string SavePhieuBH(System.Data.DataTable tblBH, System.Data.DataTable tblBHCT, String mNguoiDung) {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0] = new SqlParameter("@Phieubh", SqlDbType.Structured);
                arrPara[0].Value = tblBH;
                arrPara[1] = new SqlParameter("@Phieubhct", SqlDbType.Structured);
                arrPara[1].Value = tblBHCT; 

                arrPara[2] = new SqlParameter("@nguoidung", SqlDbType.NVarChar,50);
                arrPara[2].Value = mNguoiDung;
                arrPara[3] = new SqlParameter("@ketqua", SqlDbType.NVarChar, 255);
                arrPara[3].Direction=ParameterDirection.Output;
                int iRs = MTSQLServer.getMTSQLServer().wExec("spBH_AddPhieuBH", arrPara);
                return arrPara[3].Value.ToString();
            }
            catch (Exception ex) {
                return ex.Message.ToString();
            }
        }


        public bool DelPhieuBH(String mPhieubhid)
        {
            try
            {
                if (mPhieubhid == "")
                {
                    Utils.showMessage("Bạn chưa chọn phiếu cần xóa..", "Thông báo");
                    return false;
                }

                SqlParameter[] arrPara = new SqlParameter[3];
                arrPara[0] = new SqlParameter("@Phieubhid", SqlDbType.NVarChar, 50);
                arrPara[0].Value = mPhieubhid;
                arrPara[1] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 50);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[2] = new SqlParameter("@ketqua", SqlDbType.NVarChar, 100);
                arrPara[2].Direction = ParameterDirection.Output;
                int iRs = new MTSQLServer().wExec("spBH_DelPhieuBH", arrPara);
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
