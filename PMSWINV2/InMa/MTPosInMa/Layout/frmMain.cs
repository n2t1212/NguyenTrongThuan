using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MTPosInMa.Model;

namespace MTPosInMa
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private DataTable otblSrc = null;
        public DataTable otblChon = null;
        public bool isMaQR = true;
        public bool isDongia = false;

        private int SO_LUONG_IN = 0;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SysPar.SetFormatGridControl(grdSanpham, gvSanpham);
            SysPar.SetFormatGridControl(grdSanphamc, gvSanphamc);

            setDataSource();
            try
            {
                spinSoluong.Text = SysPar.MT_BARCODE_COPYNO.ToString();
                if (SysPar.MT_DEFAULT_TYPE == "QR")
                {
                    chkQR.Checked = true;
                }
                else
                {
                    chkQR.Checked = false;
                }
                if (SysPar.MT_DEFAUL_TEM_ROW == 3)
                {
                    cbMautem.SelectedIndex = 0;
                }
                else
                {
                    cbMautem.SelectedIndex = 1;
                }
            }
            catch { }
        }


        private void setDataSource(){
            try{
                String mSql ="";
                if (chkQR.Checked)
                {
                    mSql = String.Format("select Maspid,Masp,Tensp,Dvt,Quycach,Macode=Maqrcode,cast ({0} as int) as Soluong, cast(0 as numeric(18,3)) as Dongia from DM_SANPHAM order by Masp asc", spinSoluong.Text.Trim());
                }
                else {
                    mSql = String.Format("select Maspid,Masp,Tensp,Dvt,Quycach,Macode=Mabarcode,cast ({0} as int) as Soluong, cast(0 as numeric(18,3)) as Dongia from DM_SANPHAM order by Masp asc", spinSoluong.Text.Trim());
                }
                otblSrc = new DataAccess.SqlServer().wRead(mSql, null, false);
                if (otblSrc == null)
                {
                    MessageBox.Show("Không thể đọc dữ liệu cần chọn...", "Thông báo");
                    return;
                }
                otblChon = otblSrc.Clone();

                grdSanpham.DataSource = this.otblSrc;
                grdSanphamc.DataSource = this.otblChon;

                SysPar.SetGridReadOnly(true, gvSanpham);
                SysPar.SetGridReadOnly(false, gvSanphamc);
                //grdDSPhieuChon.Columns["Soluong"].ReadOnly = false;
                //grdDSPhieuChon.Columns["Soluong"].AllowFocus = true;
            }
            catch (Exception ex) { }
        }
         
        private void cmdAppMini_Click(object sender, EventArgs e){
            this.WindowState = FormWindowState.Minimized;
        }

        private void cmdAppExit_Click(object sender, EventArgs e){
            this.Close();
        }

        bool isMovefrm = false;
        int MouseDownX, MouseDownY;
        private void pnlTitleForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMovefrm == false) return;
            Point Pos = new Point();
            Pos.X = this.Location.X + (e.X - MouseDownX);
            Pos.Y = this.Location.Y + (e.Y - MouseDownY);
            this.Location = Pos;
        }

        private void pnlTitleForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMovefrm = false;
            }
        }
        private void pnlTitleForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMovefrm = true;
                MouseDownX = e.X;
                MouseDownY = e.Y;
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try{
                for (int i = 0; i < otblSrc.Rows.Count; i++){
                    if (gvSanpham.IsRowSelected(i))
                    {
                        DataRow vR = otblChon.NewRow();
                        vR["Maspid"] = gvSanpham.GetRowCellValue(i, colMaspid);
                        vR["Masp"] = gvSanpham.GetRowCellValue(i, colMasp);
                        vR["Tensp"] = gvSanpham.GetRowCellValue(i, colTensp);
                        vR["Dvt"] = gvSanpham.GetRowCellValue(i, colDvt);
                        vR["Quycach"] = gvSanpham.GetRowCellValue(i, colQuycach);
                        vR["Soluong"] = spinSoluong.Text.Trim();
                        vR["Macode"] = gvSanpham.GetRowCellValue(i, colMacode);
                        otblChon.Rows.Add(vR);

                        gvSanpham.DeleteRow(i);
                    }
                }
                if (gvSanpham.RowCount > 0)
                {
                    gvSanpham.SelectRow(0);
                }
                otblChon.AcceptChanges();
                otblSrc.AcceptChanges();
                grdSanphamc.RefreshDataSource();
            }
            catch { } 
        }

        private void cmdAddAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (otblSrc != null && otblSrc.Rows.Count > 0)
                {
                    foreach (DataRow vRow in otblSrc.Rows)
                    {
                        DataRow vR = otblChon.NewRow();
                        vR["Maspid"] = vRow["Maspid"];
                        vR["Masp"] = vRow["Masp"];
                        vR["Tensp"] = vRow["Tensp"];
                        vR["Dvt"] = vRow["Dvt"];
                        vR["Quycach"] = vRow["Quycach"];
                        vR["Soluong"] = spinSoluong.Text;
                        vR["Macode"] = vRow["Macode"];
                        otblChon.Rows.Add(vR);

                    }
                    otblChon.AcceptChanges();
                    otblSrc.Rows.Clear();
                    grdSanpham.RefreshDataSource();
                    grdSanphamc.RefreshDataSource();
                }
            }
            catch (Exception ex) { }
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < otblChon.Rows.Count; i++)
                {
                    if (gvSanphamc.IsRowSelected(i))
                    {
                        DataRow vR = otblSrc.NewRow();
                        vR["Maspid"] = gvSanphamc.GetRowCellValue(i, colMaspidc);
                        vR["Masp"] = gvSanphamc.GetRowCellValue(i, colMaspc);
                        vR["Tensp"] = gvSanphamc.GetRowCellValue(i, colTenspc);
                        vR["Dvt"] = gvSanphamc.GetRowCellValue(i, colDvtc);
                        vR["Quycach"] = gvSanphamc.GetRowCellValue(i, colQuycachc);
                        otblSrc.Rows.Add(vR);
                        gvSanphamc.DeleteRow(i);
                    }
                }
                if (gvSanphamc.RowCount> 0)
                {
                    gvSanphamc.SelectRow(0);
                }
                otblChon.AcceptChanges();
                otblSrc.AcceptChanges();
                grdSanpham.RefreshDataSource();
                grdSanphamc.RefreshDataSource();
            }
            catch (Exception ex) { }
        }

        private void cmdRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (otblChon != null && otblChon.Rows.Count > 0)
                {
                    foreach (DataRow vRow in otblChon.Rows)
                    {
                        DataRow vR = otblSrc.NewRow();
                        vR["Maspid"] = vRow["Maspid"];
                        vR["Masp"] = vRow["Masp"];
                        vR["Tensp"] = vRow["Tensp"];
                        vR["Dvt"] = vRow["Dvt"];
                        vR["Quycach"] = vRow["Quycach"];
                        otblSrc.Rows.Add(vR);

                    }
                    otblSrc.AcceptChanges();

                    otblChon.AcceptChanges();
                    otblChon.Rows.Clear();
                    otblChon.AcceptChanges();
                    grdSanpham.RefreshDataSource();
                }
            }
            catch (Exception ex) { }
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable otblSP = new DataTable();
                otblSP.Columns.Add("Maspid", typeof(String));
                otblSP.Columns.Add("Masp", typeof(String));
                otblSP.Columns.Add("Soluong", typeof(int));
                if (otblChon != null)
                {
                    foreach (DataRow vRow in otblChon.Rows)
                    {
                        DataRow vR = otblSP.NewRow();
                        vR["Maspid"] = vRow["Maspid"];
                        vR["Masp"] = vRow["Masp"];
                        vR["Soluong"] = SO_LUONG_IN;
                        otblSP.Rows.Add(vR);
                    }
                    otblSP.AcceptChanges();
                }
                isDongia = chkGiaban.Checked;

                if (otblSP == null || otblSP.Rows.Count <= 0)
                {
                    MessageBox.Show(String.Format("Bạn chưa chọn sản phẩm cần tạo mã {0}", isMaQR == true ? " QRCode.." : " Vạch.."), "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show(SO_LUONG_IN.ToString());
                    if (cbMautem.Text.Trim().Substring(0, 1).Equals("3"))
                    {
                        new modReport().rptInMaVach(otblSP, isDongia, (chkQR.Checked == true ? true : false), (chkThung.Checked == true ? true : false), "35_22");
                    }
                    else
                    {
                        new modReport().rptInMaVach(otblSP, isDongia, (chkQR.Checked == true ? true : false), (chkThung.Checked == true ? true : false), "25_15");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkThung_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void spinSoluong_EditValueChanged(object sender, EventArgs e)
        {
            SO_LUONG_IN = int.Parse(spinSoluong.Text.ToString().Replace(".", ""));
        }


    }
}
