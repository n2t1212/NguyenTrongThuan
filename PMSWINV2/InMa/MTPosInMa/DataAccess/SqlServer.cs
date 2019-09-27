using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;

using MTPosInMa.Model;

namespace MTPosInMa.DataAccess
{
    class SqlServer
    {
        protected SqlCommand wCmd;
        public DataSet wDset(string spName, object[] arrPara = null, bool isPro = true)
        {
            int intLoop = 0;
            try
            {
                if (SysPar.MT_Cnn.State == ConnectionState.Closed)
                {
                    SysPar.MT_Cnn.Open();
                }

                SqlCommand wCmd = new SqlCommand();
                DataSet wDts = new DataSet();
                wCmd.CommandText = spName;
                wCmd.CommandType = (isPro == true ? CommandType.StoredProcedure : CommandType.Text);
                wCmd.Connection = (SqlConnection)SysPar.MT_Cnn;

                if (arrPara != null)
                {
                    if (wCmd.Parameters.Count > 0)
                        wCmd.Parameters.Clear();
                    for (intLoop = 0; intLoop <= arrPara.Length - 1; intLoop++)
                    {
                        wCmd.Parameters.Add(arrPara[intLoop]);
                    }
                }
                SqlDataAdapter wAdp = new SqlDataAdapter(wCmd);
                wAdp.Fill(wDts);
                if (wCmd.Parameters.Count > 0)
                {
                    wCmd.Parameters.Clear();
                    wCmd.Dispose();
                    wCmd = null;
                }
                return wDts;
            }
            catch (Exception ex) { MessageBox.Show("Err:" + ex.Message.ToString()); return null; }
        }

        public DataTable wRead(string spName, object[] arrPara = null, bool isPro = true)
        {
            int intLoop = 0;
            try
            {
                if (SysPar.MT_Cnn.State == ConnectionState.Closed)
                {
                    SysPar.MT_Cnn.Open();
                }

                SqlCommand wCmd = new SqlCommand();
                DataSet wDts = new DataSet();
                wCmd.CommandText = spName;
                wCmd.CommandType = (isPro == true ? CommandType.StoredProcedure : CommandType.Text);
                wCmd.Connection = (SqlConnection)SysPar.MT_Cnn;

                if (arrPara != null)
                {
                    if (wCmd.Parameters.Count > 0)
                    {
                        wCmd.Parameters.Clear();
                    }
                    for (intLoop = 0; intLoop <= arrPara.Length - 1; intLoop++)
                    {
                        wCmd.Parameters.Add(arrPara[intLoop]);
                    }
                }
                SqlDataReader wReader = wCmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable wTb = new DataTable();
                wTb.Load(wReader);
                if (wCmd.Parameters.Count > 0)
                {
                    wCmd.Parameters.Clear();
                }
                wCmd.Dispose();
                wCmd = null;
                return wTb;
            }
            catch (Exception ex) { MessageBox.Show("Err:" + ex.Message.ToString()); return null; }
        }

        public int wExec(string spName, object[] arrPara = null, bool isPro = true)
        {
            int intLoop = 0;
            try
            {
                if (SysPar.MT_Cnn.State == ConnectionState.Closed)
                {
                    SysPar.MT_Cnn.Open();
                }
                SqlCommand wCmd = new SqlCommand();
                wCmd.CommandText = spName;
                wCmd.CommandType = (isPro == true ? CommandType.StoredProcedure : CommandType.Text);
                wCmd.Connection = (SqlConnection)SysPar.MT_Cnn;

                if (arrPara != null)
                {
                    if (wCmd.Parameters.Count > 0)
                        wCmd.Parameters.Clear();
                    for (intLoop = 0; intLoop <= arrPara.Length - 1; intLoop++)
                    {
                        wCmd.Parameters.Add(arrPara[intLoop]);
                    }
                }
                int wEx = wCmd.ExecuteNonQuery();
                if (wCmd.Parameters.Count > 0)
                {
                    wCmd.Parameters.Clear();

                }
                wCmd.Dispose();
                wCmd = null;
                return wEx;
            }
            catch (Exception ex) { MessageBox.Show("Err:" + ex.Message.ToString()); return -1; }
        }


        public int fMaxID(string oTblName, string oField)
        {
            try
            {
                string Sql = string.Format("select max({0}) from {1}", oField, oTblName);
                SqlCommand wCmd = new SqlCommand(Sql);
                wCmd.CommandType = CommandType.Text;
                wCmd.Connection = (SqlConnection)SysPar.MT_Cnn;

                SqlDataAdapter oAda = new SqlDataAdapter(wCmd);
                return ((int)wCmd.ExecuteScalar());

            }
            catch { return 0; }
        }

        public SqlDataAdapter wAdapter(string spName, object[] arrPara = null, bool isPro = true)
        {
            int intLoop = 0;
            try
            {
                if (SysPar.MT_Cnn.State == ConnectionState.Closed)
                {
                    SysPar.MT_Cnn.Open();
                }

                SqlCommand wCmd = new SqlCommand();
                DataSet wDts = new DataSet();
                wCmd.CommandText = spName;
                wCmd.CommandType = (isPro == true ? CommandType.StoredProcedure : CommandType.Text);
                wCmd.Connection = (SqlConnection)SysPar.MT_Cnn;

                if (arrPara != null)
                {
                    if (wCmd.Parameters.Count > 0)
                        wCmd.Parameters.Clear();
                    for (intLoop = 0; intLoop <= arrPara.Length - 1; intLoop++)
                    {
                        wCmd.Parameters.Add(arrPara[intLoop]);
                    }
                }
                SqlDataAdapter wAdp = new SqlDataAdapter(wCmd);
                return wAdp;
            }
            catch (Exception ex) { MessageBox.Show("Err:" + ex.Message.ToString()); return null; }
        }

        public bool doSaveDetail(GridControl Grid, SqlDataAdapter da, DataTable oTbl)
        {
            ColumnView View = (ColumnView)Grid.FocusedView;
            View.CloseEditor();
            if (View.UpdateCurrentRow() == false)
            {
                if (doSaveTable(da, oTbl))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool doSaveTable(SqlDataAdapter dataAdap, DataTable dTbl)
        {
            try
            {
                SqlCommandBuilder cmb = new SqlCommandBuilder(dataAdap);
                cmb.ConflictOption = ConflictOption.OverwriteChanges;

                dataAdap.UpdateCommand = cmb.GetUpdateCommand();
                dataAdap.InsertCommand = cmb.GetInsertCommand();
                dataAdap.DeleteCommand = cmb.GetDeleteCommand();
                dataAdap.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdap.Update(dTbl);
                dTbl.AcceptChanges();

                return true;

            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().IndexOf("conflicted with the REFERENCE constraint".ToUpper()) != 0)
                {
                   MessageBox.Show("Thông tin dòng này đã được sử dụng nên không cập nhật hay xóa.","Lưu ý",MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(ex.Message.ToString(), "Lưu ý", MessageBoxButtons.OK);
                }
                return false;
            }

        }

    }
}
