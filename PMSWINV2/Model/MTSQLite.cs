using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace MTPMSWIN.Model
{

    public static class MTSQLite
    {
       public static SQLiteConnection SQLITE_CNN = null;
       public static SQLiteCommand SQLITE_CMM = null;
        
        //OPEN CONNECT
        public static bool OpenSQLite() {
            try
            {
                if (SQLITE_CNN == null || SQLITE_CNN.State == ConnectionState.Closed)
                {
                    SQLITE_CNN = new SQLiteConnection(MTGlobal.MT_SQLCNN_STR);
                    //SQLITE_CNN.SetPassword("");
                    SQLITE_CNN.Open();
                    if (SQLITE_CNN.State == System.Data.ConnectionState.Open) return true;
                     
                }
                else if (SQLITE_CNN.State == ConnectionState.Open)
                {
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public static void closeSQLite()
        {
            if (SQLITE_CNN != null || SQLITE_CNN.State == ConnectionState.Open)
            {
                SQLITE_CNN.Close();
            }
        }

        public static DataSet SQLiteRead(string mSQL, SQLiteParameter[] arrPara) {
            try
            {
                DataSet oDset = new DataSet();
                if (OpenSQLite()) {
                    SQLITE_CMM = new SQLiteCommand();
                    SQLITE_CMM.CommandText = mSQL;
                    SQLITE_CMM.CommandType = CommandType.Text;
                    SQLITE_CMM.Connection = SQLITE_CNN;
                    if (arrPara != null) {
                        if (SQLITE_CMM.Parameters.Count > 0) SQLITE_CMM.Parameters.Clear();
                        foreach (SQLiteParameter oPara in arrPara) {
                            SQLITE_CMM.Parameters.Add(arrPara);
                        }
                    }

                    SQLiteDataAdapter SQLITE_Adaptor = new SQLiteDataAdapter(SQLITE_CMM);
                    SQLITE_Adaptor.Fill(oDset);
                    if (SQLITE_CMM.Parameters.Count > 0) {
                        SQLITE_CMM.Parameters.Clear();
                        SQLITE_CMM.Dispose();
                        SQLITE_CMM = null;
                    }
                }
                return oDset;
            }
            catch { return null; }
        }

        public static DataTable SQLiteReadTBL(string mSQL, SQLiteParameter[] arrPara){
            try{
                DataTable oTbl = new DataTable();

                if (OpenSQLite())
                {
                    SQLITE_CMM = new SQLiteCommand(mSQL, SQLITE_CNN);
                    //SQLITE_CMM.CommandText = mSQL;
                    //SQLITE_CMM.CommandType = CommandType.Text;
                    //SQLITE_CMM.Connection = SQLITE_CNN;

                    if (arrPara != null)
                    {
                        if (SQLITE_CMM.Parameters.Count > 0) SQLITE_CMM.Parameters.Clear();
                        foreach (SQLiteParameter oPara in arrPara)
                        {
                            SQLITE_CMM.Parameters.Add(arrPara);
                        }
                    }
                    //SQLiteDataReader SQLITE_Reader = SQLITE_CMM.ExecuteReader(CommandBehavior.CloseConnection);
                    SQLiteDataReader SQLITE_Reader = SQLITE_CMM.ExecuteReader();


                    oTbl.Load(SQLITE_Reader); 
                    if (SQLITE_CMM.Parameters.Count > 0)
                    {
                        SQLITE_CMM.Parameters.Clear();
                        SQLITE_CMM.Dispose();
                        SQLITE_CMM = null;
                    }
                }
                return oTbl;
            }
            catch {
                return null; 
            }
        }

        public static DataSet SQLiteReadAdaptor(string mSQL, SQLiteParameter[] arrPara, ref SQLiteDataAdapter SQLAdaper)
        {
            try
            {
                DataSet oDset = new DataSet();
                if (OpenSQLite())
                {
                    SQLITE_CMM = new SQLiteCommand();
                    SQLITE_CMM.CommandText = mSQL;
                    SQLITE_CMM.CommandType = CommandType.Text;
                    SQLITE_CMM.Connection = SQLITE_CNN;
                    if (arrPara != null)
                    {
                        if (SQLITE_CMM.Parameters.Count > 0) SQLITE_CMM.Parameters.Clear();
                        foreach (SQLiteParameter oPara in arrPara)
                        {
                            SQLITE_CMM.Parameters.Add(arrPara);
                        }
                    }

                    SQLAdaper = new SQLiteDataAdapter(SQLITE_CMM);
                    SQLAdaper.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    SQLAdaper.Fill(oDset,"DM_CHUCVU");
                    if (SQLITE_CMM.Parameters.Count > 0)
                    {
                        SQLITE_CMM.Parameters.Clear();
                        SQLITE_CMM.Dispose();
                        SQLITE_CMM = null;
                    }
                }
                return oDset;
            }
            catch { return null; }
        }

        public static string SQLExcute(string mSQL, SQLiteParameter[] arrPara) {
            try
            {
                if (OpenSQLite())
                {
                    SQLITE_CMM = new SQLiteCommand();
                    SQLITE_CMM.CommandText = mSQL;
                    SQLITE_CMM.CommandType = CommandType.Text;
                    SQLITE_CMM.Connection = SQLITE_CNN;
                    if (arrPara != null)
                    {
                        if (SQLITE_CMM.Parameters.Count > 0) SQLITE_CMM.Parameters.Clear();
                        foreach (SQLiteParameter oPara in arrPara)
                        {
                            SQLITE_CMM.Parameters.Add(oPara);
                        }
                    }
                    int iRs = SQLITE_CMM.ExecuteNonQuery(); 
                    if (SQLITE_CMM.Parameters.Count > 0)
                    {
                        SQLITE_CMM.Parameters.Clear();
                        SQLITE_CMM.Dispose();
                        SQLITE_CMM = null;
                    }
                }
                return "";
            }
            catch (Exception ex) { return ex.Message.ToString(); }
        } 

        public static bool SQLiteUpdate(SQLiteDataAdapter SQLAdaptor, DataTable SQLTable){
          try{
            
              SQLiteCommandBuilder cmdBuilder=new SQLiteCommandBuilder(SQLAdaptor);
              cmdBuilder.ConflictOption=ConflictOption.OverwriteChanges;
              SQLAdaptor.UpdateCommand=cmdBuilder.GetUpdateCommand();
              SQLAdaptor.InsertCommand=cmdBuilder.GetInsertCommand();
              SQLAdaptor.DeleteCommand=cmdBuilder.GetDeleteCommand();
              SQLAdaptor.MissingSchemaAction=MissingSchemaAction.AddWithKey;
              SQLAdaptor.Update(SQLTable);
              SQLTable.AcceptChanges();

              return true;
          }catch(Exception ex){
            if(ex.Message.ToString().IndexOf("conflicted with the REFERENCE constraint")!=0){
              SQLTable.RejectChanges();
              System.Windows.MessageBox.Show("Mẫu tin đã được sử dụng. Bạn không thể xóa...","Thông  báo",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Error);             
            }
              return false;
          }
        }

        public static bool SQLiteAbort(SQLiteDataAdapter SQLAdaptor, DataTable SQLTable) {
            try
            {
                SQLTable.RejectChanges();
                return true;
            }
            catch { return false; }
        }

        public static bool SQLiteDelete(SQLiteDataAdapter SQLAdaptor, DataTable SQLTable) {
            return true;
        }


        #region "NOTES"
        /*SQLIAdaptor.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            SQLIAdaptor.InsertCommand = new System.Data.SQLite.SQLiteCommand("INSERT INTO DM_CHUCVU(macv, chucvu) VALUES (:macv, :chucvu)", MTSQLite.SQLITE_CNN);
            SQLIAdaptor.InsertCommand.Parameters.Add("macv", DbType.String, 50, "macv");
            SQLIAdaptor.InsertCommand.Parameters.Add("chucvu", DbType.String, 50, "chucvu");
             

            SQLIAdaptor.UpdateCommand = new System.Data.SQLite.SQLiteCommand("UPDATE DM_CHUCVU SET chucvu=:chucvu where macv=:macv", MTSQLite.SQLITE_CNN);
            SQLIAdaptor.UpdateCommand.Parameters.Add("macv", DbType.String, 50, "macv");
            SQLIAdaptor.UpdateCommand.Parameters.Add("chucvu", DbType.String, 50, "chucvu");

            SQLIAdaptor.DeleteCommand = new System.Data.SQLite.SQLiteCommand("delete from DM_CHUCVU where macv=:macv", MTSQLite.SQLITE_CNN);
            SQLIAdaptor.DeleteCommand.Parameters.Add("macv", DbType.String, 50, "macv");
            */
        #endregion
    }
}
