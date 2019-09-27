using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MTPMSWIN.Model;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace MTPMSWIN.View
{
    /// <summary>
    /// Interaction logic for FormAddProduct.xaml
    /// </summary>
    public partial class FormAddProduct : Window
    {
        SQLiteDataAdapter SQLAdaptor = null;
        DataSet DSetMain = null;

        public FormAddProduct()
        {
            InitializeComponent();
        }

        private void InitProductGroupCbb()
        {
            string mSQL = string.Format("select * from DM_NHOMSP order by manhom asc");
            SQLAdaptor = new SQLiteDataAdapter(mSQL, MTSQLite.SQLITE_CNN);
            DSetMain = new DataSet();
            SQLAdaptor.Fill(DSetMain, "DM_NHOMSP");
            cbbProductGroup.ItemsSource = DSetMain.Tables[0].DefaultView;
            cbbProductGroup.DisplayMemberPath = DSetMain.Tables[0].Columns["tennhom"].ToString();
            cbbProductGroup.SelectedValuePath = DSetMain.Tables[0].Columns["kihieu"].ToString();
            SQLAdaptor.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitProductGroupCbb();
        }

        private void onClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onClickSaveAndAddNew(object sender, RoutedEventArgs e)
        {
            saveProduct();
            txtProductCode.Clear();
            cbbProductGroup.SelectedIndex = -1;
            txtProductName.Clear();
            txtDVT.Clear();
            txtQuyCach.Clear();
            txtQuyDoiLe.Clear();
            txtQuyDoiThung.Clear();
            txtNhaCC.Clear();
        }

        private void onClickSave(object sender, RoutedEventArgs e)
        {
            saveProduct();
            this.Close();
        }

        private void saveProduct()
        {
            try
            {
                if (MTSQLite.OpenSQLite())
                {
                    String mSQL = String.Format("INSERT INTO DM_SANPHAM (masp, nhomsp, tensp, dvt, quycach, quydoile, quydoithung, nhacungcap) " +
                        "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", 
                        txtProductCode.Text,
                        cbbProductGroup.SelectedValue,
                        txtProductName.Text,
                        txtDVT.Text,
                        txtQuyCach.Text,
                        txtQuyDoiLe.Text,
                        txtQuyDoiThung.Text,
                        txtNhaCC.Text);
                    MTSQLite.SQLITE_CMM = new SQLiteCommand();
                    MTSQLite.SQLITE_CMM.CommandText = mSQL;
                    MTSQLite.SQLITE_CMM.Connection = MTSQLite.SQLITE_CNN;

                    var rowInserted = MTSQLite.SQLITE_CMM.ExecuteNonQuery();
                    if (rowInserted > 0)
                    {
                        XtraMessageBox.Show("Lưu dữ liệu thành công");
                    }
                    else
                    {
                        XtraMessageBox.Show("Lỗi. Không lưu được dữ liệu");
                    }
                }
            }
            catch (Exception ex) 
            {  
                ex.Message.ToString();
                XtraMessageBox.Show("Lỗi. Không lưu được dữ liệu");
            }
                     
        }
    }
}
