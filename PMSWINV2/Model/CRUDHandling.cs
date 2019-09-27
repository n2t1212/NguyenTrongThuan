using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MTPMSWIN.Model
{
    // Use for all DM
    public class CRUDHandling{
        private const String EMPTY = "";

        SqlDataAdapter SQLAdaptor = null;
        DataSet DSetMain = null;
        private MTGlobal.MT_ROLE MTROLE;
        private MTGlobal.MT_BUTTONACTION MTButton;

        private int totalRow = 0;

        private String TABLE_NAME = "";
        private String ID_NAME = "";
        private String CODE_NAME = "";
        private String CODE_HEADER = "";
        private String SQL_LOAD_ALL = "";
        private String SQL_DELETE = "";
        private DevExpress.Xpf.Grid.GridControl grid = null;
        private DevExpress.Xpf.Grid.TableView tblView = null;
        private DevExpress.Xpf.Grid.GridColumn columnFocus = null;
        private DevExpress.Utils.WaitDialogForm Dlg = null;


        public CRUDHandling(DevExpress.Xpf.Grid.GridControl grid, DevExpress.Xpf.Grid.TableView tblView, 
            DevExpress.Xpf.Grid.GridColumn columnFocus, MTGlobal.MT_ROLE MTROLE, MTGlobal.MT_BUTTONACTION MTButton, String TABLE_NAME, String ID_NAME, String CODE_NAME,
            String CODE_HEADER, String SQL_LOAD_ALL, String SQL_DELETE)
        {
            this.grid = grid;
            this.tblView = tblView;
            this.columnFocus = columnFocus;
            this.MTROLE = MTROLE;
            this.MTButton = MTButton;
            this.TABLE_NAME = TABLE_NAME;
            this.ID_NAME = ID_NAME;
            this.CODE_NAME = CODE_NAME;
            this.CODE_HEADER = CODE_HEADER;
            this.SQL_LOAD_ALL = SQL_LOAD_ALL;
            this.SQL_DELETE = SQL_DELETE;
        }

        public CRUDHandling(){}

        public String GridForm_Loaded(){
            try{
                showWaiting("Đang nạp dữ liệu...");
                string mSQL = string.Format(SQL_LOAD_ALL);
                SQLAdaptor = MTSQLServer.getMTSQLServer().wAdapter(mSQL, null, false);
                DSetMain = new DataSet();
                SQLAdaptor.Fill(DSetMain, TABLE_NAME);
                DataTable dt = DSetMain.Tables[TABLE_NAME];
                totalRow = dt.Rows.Count;
                grid.ItemsSource = dt;
                MTGlobal.SetGridReadOnly(grid,tblView, true);
                MTGlobal.SetFormatGridControl(grid, tblView);
                MTGlobal.SetPermit(ref MTROLE);            
                MTGlobal.SetButtonAction(MTROLE, MTButton, "INIT");                
            }
            catch (Exception ex) {
                closeWaiting();
               return ex.Message.ToString();
            }
            closeWaiting();
            return EMPTY;
        }

        public void cmdAdd_Click()
        {
            if (MTGlobal.SetGridReadOnly(grid,tblView, false))
            {
                tblView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
                tblView.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.NewItemRowHandle;
                tblView.FocusedColumn = columnFocus;
                tblView.Focus();
                tblView.ShowEditor();
                MTGlobal.SetButtonAction(MTROLE, MTButton, "ADD");
            }                
        }

        public void cmdEdit_Click()
        {
            if (MTGlobal.SetGridReadOnly(grid,tblView, false))
            {
                tblView.Focus();
                tblView.FocusedColumn = grid.Columns.First();
                tblView.ShowEditor();
                MTGlobal.SetButtonAction(MTROLE, MTButton, "EDIT");
            }
        }

        public String cmdSave_Click()
        {
            try{           
                if (MTGlobal.MT_CURRENT_ACTION == "ADD" || MTGlobal.MT_CURRENT_ACTION=="EDIT"){
                    if (MTSQLServer.getMTSQLServer().doSaveTable(SQLAdaptor, DSetMain.Tables[TABLE_NAME]))
                    {
                        MTGlobal.SetGridReadOnly(grid,tblView, true);
                        MTGlobal.SetButtonAction(MTROLE, MTButton, "SAVE");
                        this.GridForm_Loaded();
                        return Utils.SAVE_DB_OK;
                    }
                    else{
                        return Utils.ERR_UPDATE_DB;
                    }
                }
                return "";
                 
            }
            catch (Exception ex) {
                return ex.Message.ToString();
            }         
        }

        public String cmdDel_Click()
        {
            try
            {
                if (tblView.GetSelectedRowHandles() == null)
                {
                    return Utils.SELECT_ROW_TO_DELETE;
                }
                var row = grid.GetRow(tblView.GetSelectedRowHandles()[0]);
                if (row == null)
                {
                    return Utils.SELECT_ROW_TO_DELETE;
                }
                var id = ((System.Data.DataRowView)(row)).Row.ItemArray[0];
                String sql = String.Format(SQL_DELETE, id);
                int result = MTSQLServer.getMTSQLServer().wExec(sql, null, false);
                if (result == 1)
                {
                    GridForm_Loaded();
                    return Utils.SAVE_DB_OK;
                }
                else
                {
                    return Utils.ERR_UPDATE_DB;
                }
            }
            catch (Exception ex) { return ex.Message.ToString(); }
        
        }
        public void cmdAbort_Click()
        {
            try
            {
                MTSQLServer.getMTSQLServer().Abort(SQLAdaptor, DSetMain.Tables[TABLE_NAME]);
                MTGlobal.SetGridReadOnly(grid,tblView, true);
                MTGlobal.SetButtonAction(MTROLE, MTButton, "ABORT");
            }
            catch { }
         
        }


        public void showWaiting(String msg) {
            try
            {
                if (Dlg != null)
                {
                    Dlg.Close();
                    Dlg = null;
                }
                Dlg = new DevExpress.Utils.WaitDialogForm("Vui lòng chờ, hệ thống đang xử lý...", "PHẦN MỀM QUẢN LÝ KHO");
                Dlg.Show();
            }
            catch (Exception ex) { }
        }

        public void closeWaiting() {
            if (Dlg != null) {
                Dlg.Close();
                Dlg = null;
            }
        }

    }
}
