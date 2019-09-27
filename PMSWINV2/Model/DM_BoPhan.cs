using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MTPMSWIN.Model
{
    public class DM_BoPhan
    {
        public string mabophan { get; set; }
        public string root { get; set; }
        public string tenbophan { get; set; }
    }

    public class ListDM_BoPhan
    {
        SqlDataAdapter SQLAdaptor = null;
        DataSet DSetMain = null;

        private string SQL_ALL_BOPHAN = "select * from DM_BOPHAN";

        public List<DM_BoPhan> GetListBoPhan()
        {
            List<DM_BoPhan> listBoPhan = new List<DM_BoPhan>();

            SQLAdaptor = MTSQLServer.getMTSQLServer().wAdapter(SQL_ALL_BOPHAN, null, false);
            DSetMain = new DataSet();
            SQLAdaptor.Fill(DSetMain, "DM_BOPHAN");
            DataTable dt = DSetMain.Tables["DM_BOPHAN"];
            int rowNumber = dt.Rows.Count;

            if (rowNumber > 0)
            {
                for (int i = 0; i < rowNumber; i++)
                {
                    string maBoPhan = dt.Rows[i]["mabophan"].ToString();
                    string tenBoPhan = dt.Rows[i]["tenbophan"].ToString();
                    string root = dt.Rows[i]["root"].ToString();

                    listBoPhan.Add(new DM_BoPhan() { mabophan = maBoPhan, tenbophan = tenBoPhan, root = root });
                }
            }

            return listBoPhan;
        }
    }

}
