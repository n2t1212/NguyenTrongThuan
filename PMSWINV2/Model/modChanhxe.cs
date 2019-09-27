using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MTPMSWIN.Model
{
    public class modChanhxe{
        public modChanhxe() { }
           
         
        public DataTable dtPhieu()
        {
            DataTable Phieu = new DataTable();
            Phieu.Columns.Add("Chanhxeid", typeof(String));
            Phieu.Columns.Add("Sophieu", typeof(String));
            Phieu.Columns.Add("Loaiphieu", typeof(String));
            Phieu.Columns.Add("Ngayct", typeof(DateTime));
            Phieu.Columns.Add("Ngayps", typeof(DateTime));
            Phieu.Columns.Add("Makh", typeof(String));
            Phieu.Columns.Add("Tenkh", typeof(String));
            Phieu.Columns.Add("Diachi", typeof(String));
            Phieu.Columns.Add("Dienthoai", typeof(String));
            Phieu.Columns.Add("Email", typeof(String));
            Phieu.Columns.Add("Soxe", typeof(String));
            Phieu.Columns.Add("Loaixe", typeof(String));
            Phieu.Columns.Add("Taixe", typeof(String));
            Phieu.Columns.Add("Dienthoaixe", typeof(String));
            Phieu.Columns.Add("Trongluong", typeof(Double));
            Phieu.Columns.Add("Chiphi", typeof(Double));
            Phieu.Columns.Add("HTThanhtoan", typeof(String));
            Phieu.Columns.Add("Trangthai", typeof(int));
            Phieu.Columns.Add("Ghichu", typeof(String));
            return Phieu; 
        }

        public DataTable dtPhieuCT()
        {
            DataTable PhieuCT = new DataTable();
            PhieuCT.Columns.Add("Chanhxectid", typeof(String));
            PhieuCT.Columns.Add("Chanhxeid", typeof(String));
            PhieuCT.Columns.Add("Maspid", typeof(String));
            PhieuCT.Columns.Add("Masp", typeof(String));
            PhieuCT.Columns.Add("SLThung", typeof(Double));
            PhieuCT.Columns.Add("Soluong", typeof(Double));
            PhieuCT.Columns.Add("Chiphi", typeof(Double));
            PhieuCT.Columns.Add("Ghichu", typeof(String));
            return PhieuCT;
        }
         
        public string SavePhieu(System.Data.DataTable tblPCX, System.Data.DataTable tblPCXCT) {
            try
            {
                SqlParameter[] arrPara = new SqlParameter[4];
                arrPara[0] = new SqlParameter("@phieucx", SqlDbType.Structured);
                arrPara[0].Value = tblPCX;
                arrPara[1] = new SqlParameter("@phieucxct", SqlDbType.Structured);
                arrPara[1].Value = tblPCXCT;
            
                arrPara[2] = new SqlParameter("@nguoidung", SqlDbType.NVarChar,50);
                arrPara[2].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[3] = new SqlParameter("@ketqua", SqlDbType.NVarChar, 255);
                arrPara[3].Direction=ParameterDirection.Output;
                int iRs = new MTSQLServer().wExec("spNX_AddChanhXe", arrPara);
                return arrPara[3].Value.ToString();
            }
            catch (Exception ex) {
                return ex.Message.ToString();
            }
        }

        public bool DelPhieu(String mPhieucxid)
        {
            try
            {
                if (mPhieucxid == "") {
                    Utils.showMessage("Bạn chưa chọn phiếu cần xóa..", "Thông báo");
                    return false;
                }
                SqlParameter[] arrPara = new SqlParameter[3];
                arrPara[0] = new SqlParameter("@Phieucxid", SqlDbType.NVarChar,50);
                arrPara[0].Value =mPhieucxid;
                arrPara[1] = new SqlParameter("@nguoidung", SqlDbType.NVarChar, 50);
                arrPara[1].Value = MTGlobal.MT_USER_LOGIN;
                arrPara[2] = new SqlParameter("@ketqua", SqlDbType.NVarChar,100);
                arrPara[2].Direction = ParameterDirection.Output;
                int iRs = new MTSQLServer().wExec("spNX_DelPhieuCX", arrPara);
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
