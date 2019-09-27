using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Drawing;

namespace MTPosInMa.Model
{
    public static class SysPar
    {
        public static string MT_Cnnstr { get; set; }
        public static SqlConnection MT_Cnn;

        public static int MT_BARCODE_COPYNO = 6;
        public static int MT_QR_MARGIN_LEFT = 30;
        public static int MT_QR_MARGIN_RIGHT = 30;
        public static int MT_QR_MARGIN_TOP = 15;
        public static int MT_QR_MARGIN_BOTTON = 15;

        public static int MT_BC_MARGIN_LEFT = 30;
        public static int MT_BC_MARGIN_RIGHT = 30;
        public static int MT_BC_MARGIN_TOP = 15;
        public static int MT_BC_MARGIN_BOTTON = 15;

        public static String MT_DEFAULT_TYPE = "QR";
        public static int MT_DEFAUL_TEM_ROW = 3;

        public static void Start()
        {
            try{
                System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Application.StartupPath + "/MTPosInMa.exe");
                System.Configuration.AppSettingsSection app = config.AppSettings;

                string mHost = app.Settings["host"].Value.ToString();
                string mDBname = app.Settings["dbname"].Value.ToString();
                string mUser = app.Settings["username"].Value.ToString();
                string mPassword = app.Settings["password"].Value.ToString();
                

                MT_Cnnstr = string.Format(@"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Max Pool Size=2000", mHost, mDBname, mUser, mPassword);
                fInit_SQLConnect();

                int.TryParse(app.Settings["barcode_copy_no"].Value.ToString(), out MT_BARCODE_COPYNO);

                int.TryParse(app.Settings["margin_qr_left"].Value.ToString(), out MT_QR_MARGIN_LEFT);
                int.TryParse(app.Settings["margin_qr_right"].Value.ToString(), out MT_QR_MARGIN_RIGHT);
                int.TryParse(app.Settings["margin_qr_top"].Value.ToString(), out MT_QR_MARGIN_TOP);
                int.TryParse(app.Settings["margin_qr_bottom"].Value.ToString(), out MT_QR_MARGIN_BOTTON);

                int.TryParse(app.Settings["margin_bc_left"].Value.ToString(), out MT_BC_MARGIN_LEFT);
                int.TryParse(app.Settings["margin_bc_right"].Value.ToString(), out MT_BC_MARGIN_RIGHT);
                int.TryParse(app.Settings["margin_bc_top"].Value.ToString(), out MT_BC_MARGIN_TOP);
                int.TryParse(app.Settings["margin_bc_bottom"].Value.ToString(), out MT_BC_MARGIN_BOTTON);

                int.TryParse(app.Settings["default_tem_row"].Value.ToString(), out MT_DEFAUL_TEM_ROW);
                MT_DEFAULT_TYPE = app.Settings["default_type"].Value.ToString();
                
              }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        static bool fInit_SQLConnect()
        {
            try
            {
                MT_Cnn = new SqlConnection();
                MT_Cnn.ConnectionString = MT_Cnnstr;
                MT_Cnn.Open();
                return (MT_Cnn.State == ConnectionState.Open);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public static void SetFormatGridControl(DevExpress.XtraGrid.GridControl oGrid, DevExpress.XtraGrid.Views.Grid.GridView View,bool isFilterRow=true)
        {
            try
            {
                View.OptionsNavigation.EnterMoveNextColumn = true;
                View.OptionsSelection.MultiSelect = true;
                View.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                View.ColumnPanelRowHeight =35;
                View.RowHeight = 30;
                View.ViewCaptionHeight = 32;
                View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                //View.OptionsSelection.InvertSelection=true;
                //View.GridControl.LookAndFeel.UseDefaultLookAndFeel=false;
                View.OptionsNavigation.AutoFocusNewRow = true;
                View.OptionsView.ShowAutoFilterRow = isFilterRow;
                View.OptionsView.ShowGroupPanel = false; 
                View.FixedLineWidth = 3;
                View.OptionsView.ColumnAutoWidth = false;
            }
            catch { }
        }

        public static void SetGridReadOnly(bool isReadOnly, DevExpress.XtraGrid.Views.Grid.GridView View)
        {
            try
            {
                for (int i = 0; i < View.Columns.Count; i++)
                {
                    try
                    {
                        View.Columns[i].OptionsColumn.AllowEdit = !isReadOnly;
                        View.Columns[i].OptionsColumn.ReadOnly = isReadOnly;
                    }
                    catch { }
                }
            }
            catch { }
        }



    }
}
