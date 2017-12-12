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
    public class BUS_Product
    {
        DAL_Main db = null;
        public BUS_Product()
        {
            db = new DAL_Main();
        }

        public DataSet Get()
        {
            // return db.ExecuteQueryDataSet("SELECT * FROM Product", CommandType.Text);
            return db.ExecuteQueryDataSet("usp_GetSanPham", CommandType.StoredProcedure);
        }

        public bool Add_Category(string id, string name, ref string err)
        {
            return db.MyExecuteNonQuery("usp_iLoaiSanPham",CommandType.StoredProcedure,ref err,
                new SqlParameter("@category_id",id),
                new SqlParameter("@name", name));
        }


        public bool Update_Category(string id, string name, ref string err)
        {
            return db.MyExecuteNonQuery("usp_uLoaiSanPham", CommandType.StoredProcedure, ref err,
                new SqlParameter("@category_id", id),
                new SqlParameter("@name", name));
        }

        public bool Delete_Category(string id,ref string err)
        {
            return db.MyExecuteNonQuery("usp_dLoaiSanPham", CommandType.StoredProcedure, ref err,
                new SqlParameter("@category_id", id));
        }
        public bool Add(string pro_id, string name, string dis_id, string category_id, string stt, float exp_price, string filepath,ref string error )
        {
            //string sql = "Insert into Product values('" + pro_id+"',N'"+name+"','" +dis_id + "','" +category_id + "'," +quantity+ "," +exp_price + ",N'" + filepath+"')";
            //return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);

            return db.MyExecuteNonQuery("usp_iSanPham", CommandType.StoredProcedure, ref error,
                new SqlParameter("@pro_id",pro_id),
                 new SqlParameter("@name",name),
                 new SqlParameter("@dis_id",dis_id),
                new SqlParameter("@category_id",category_id),
                new SqlParameter("@status",stt),
                new SqlParameter("@exp_price",exp_price),
                new SqlParameter("@img",filepath)
               );
        }
        public bool Delete(string pro_id, ref string error)
        {
            //string sql = "DELETE FROM Product WHERE Pro_ID='" + pro_id + "'";
            return db.MyExecuteNonQuery("usp_dSanPham", CommandType.StoredProcedure, ref error,new SqlParameter("@pro_id",pro_id));
        }
        public bool Update(string pro_id, string name, string dis_id, string category_id, string stt, float exp_price, string filepath, ref string error)
        {
            //string sql = "Update Product set Name=N'" + name + "',Dis_ID='" + dis_id + "',Category_ID='" + category_id + "',Quantity=" + quantity + ",Exp_Price=" + exp_price + ",Img_Path=N'" + filepath + "' where Pro_ID='"+pro_id+"'";
            //return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);

            return db.MyExecuteNonQuery("usp_uSanPham", CommandType.StoredProcedure, ref error,
                new SqlParameter("@pro_id", pro_id),
                 new SqlParameter("@name", name),
                 new SqlParameter("@dis_id", dis_id),
                new SqlParameter("@category_id", category_id),
                new SqlParameter("@status", stt),
                new SqlParameter("@exp_price", exp_price),
                new SqlParameter("@img", filepath)
               );
        }
        public DataSet Search(int choise, string str, ref string error)
        {
            string sql = null;
            switch (choise)
            {
                case 0:
                    sql = "SELECT * FROM dbo.Product WHERE Lower(Pro_ID)=Lower('" + str + "')";
                    break;
                case 1:
                    sql = "SELECT * FROM dbo.Product WHERE Lower(Name)=Lower(N'" + str + "')";
                    break;
                case 2:
                    sql = "SELECT * FROM dbo.Product WHERE Lower(Dis_ID)=Lower(N'" + str + "')";
                    break;
                case 3:
                    sql = "SELECT * FROM dbo.Product WHERE Lower(Category_ID)=Lower(N'" + str + "')";
                    break;
            }
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
        }
    }
}
