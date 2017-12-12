using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using QLCuaHang_TGDD.DAL;

namespace QLCuaHang_TGDD.BUS
{
    public class BUS_Bill
    {
        DAL_Main db = null;
        public BUS_Bill()
        {
            db = new DAL_Main();
        }

        public DataSet Get_Import_Bill()
        {
            return db.ExecuteQueryDataSet("usp_GetHoaDonNhap", CommandType.StoredProcedure);
        }

        public DataSet Get_Import_Detail(string imp_id, string pro_id)
        {
            return db.ExecuteQueryDataSetWithPra("usp_GetChiTietHoaDonNhap", CommandType.StoredProcedure, new SqlParameter("@imp_id",imp_id), new SqlParameter("@pro_id", pro_id));
        }

        public DataSet Get_Export_Bill()
        {
            return db.ExecuteQueryDataSet("usp_GetHoaDonBan", CommandType.StoredProcedure);
        }

        public DataSet Get_Export_Detail(string exp_id, string pro_id)
        {
            return db.ExecuteQueryDataSetWithPra("usp_GetChiTietHoaDonBan", CommandType.StoredProcedure, new SqlParameter("@exp_id", exp_id), new SqlParameter("@pro_id", pro_id));
        }

        /*--------------------------------IMPORT--------------------------------------*/

        public bool Add_Import_Bill(string imp_id, string dis_id, string emp_id, string date, float total,ref string err)
        {
            return db.MyExecuteNonQuery("usp_iImport_Bill", CommandType.StoredProcedure, ref err,
                new SqlParameter("@imp_id", imp_id),
                new SqlParameter("@dis_id", dis_id),
                new SqlParameter("@emp_id", emp_id),
                new SqlParameter("@date", DateTime.Parse(date)),
                new SqlParameter("@total", total));
        }

        public bool Update_Import_Bill(string imp_id, string dis_id, string emp_id, string date, float total, ref string err)
        {
            return db.MyExecuteNonQuery("usp_uImport_Bill", CommandType.StoredProcedure, ref err,
                new SqlParameter("@imp_id", imp_id),
                new SqlParameter("@dis_id", dis_id),
                new SqlParameter("@emp_id", emp_id),
                new SqlParameter("@date", DateTime.Parse(date)),
                new SqlParameter("@total", total));
        }
        public bool Add_Import_Detail(string imp_id, string pro_id,int iquantity, float price, ref string err)
        {
            return db.MyExecuteNonQuery("usp_iChiTietHoaDonNhap", CommandType.StoredProcedure, ref err,
                new SqlParameter("@imp_id", imp_id),
                new SqlParameter("@pro_id", pro_id),
                new SqlParameter("@iquantity",iquantity),
                new SqlParameter("@price",price));
        }


        public bool Update_Import_Detail(string imp_id, string pro_id, int iquantity, float price, ref string err)
        {
            return db.MyExecuteNonQuery("usp_uChiTietHoaDonNhap", CommandType.StoredProcedure, ref err,
                new SqlParameter("@imp_id", imp_id),
                new SqlParameter("@pro_id", pro_id),
                new SqlParameter("@iquantity", iquantity),
                new SqlParameter("@price", price));
        }

        public bool Delete_Import_Detail(string imp_d, string pro_id, ref string err)
        {
            return db.MyExecuteNonQuery("usp_dChiTietHoaDonNhap", CommandType.StoredProcedure, ref err,
                new SqlParameter("@imp_id", imp_d),
                new SqlParameter("@pro_id", pro_id));
        }


        /*--------------------------------EXPORT--------------------------------------*/
        public bool Add_Export_Bill(string exp_id, string cus_id, string emp_id,string ev_id, string date, float total, ref string err)
        {
            return db.MyExecuteNonQuery("usp_iExport_Bill", CommandType.StoredProcedure, ref err,
                new SqlParameter("@exp_id", exp_id),
                new SqlParameter("@cus_id", cus_id),
                new SqlParameter("@emp_id", emp_id),
                new SqlParameter("@ev_id", ev_id),
                new SqlParameter("@date", DateTime.Parse(date)),
                new SqlParameter("@total", total));
        }


        public bool Update_Export_Bill(string exp_id, string cus_id, string emp_id, string ev_id, string date, float total, ref string err)
        {
            return db.MyExecuteNonQuery("usp_uExport_Bill", CommandType.StoredProcedure, ref err,
                new SqlParameter("@exp_id", exp_id),
                new SqlParameter("@cus_id", cus_id),
                new SqlParameter("@emp_id", emp_id),
                new SqlParameter("@ev_id", ev_id),
                new SqlParameter("@date", DateTime.Parse(date)),
                new SqlParameter("@total", total));
        }
        public bool Add_Export_Detail(string exp_id, string pro_id, int equantity, float price, ref string err)
        {
            return db.MyExecuteNonQuery("usp_iChiTietHoaDonBan", CommandType.StoredProcedure, ref err,
                new SqlParameter("@exp_id", exp_id),
                new SqlParameter("@pro_id", pro_id),
                new SqlParameter("@equantity", equantity),
                new SqlParameter("@price", price));
        }

        public bool Update_Export_Detail(string exp_id, string pro_id, int equantity, float price, ref string err)
        {
            return db.MyExecuteNonQuery("usp_iChiTietHoaDonBan", CommandType.StoredProcedure, ref err,
                new SqlParameter("@exp_id", exp_id),
                new SqlParameter("@pro_id", pro_id),
                new SqlParameter("@equantity", equantity),
                new SqlParameter("@price", price));
        }

        public bool Delete_Export_Detail(string exp_id, string pro_id, ref string err)
        {
            return db.MyExecuteNonQuery("usp_dChiTietHoaDonBan", CommandType.StoredProcedure, ref err,
                new SqlParameter("@exp_id", exp_id),
                new SqlParameter("@pro_id", pro_id));
        }

      
    }
}
