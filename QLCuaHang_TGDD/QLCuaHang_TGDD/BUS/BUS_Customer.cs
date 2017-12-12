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
    public class BUS_Customer
    {
        DAL_Main db = null;
        public BUS_Customer()
        {
            db = new DAL_Main();
        }

        public DataSet Get()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM Customer", CommandType.Text);
        }

        public bool Add(string cus_id, string name, string mid_name, string sur_name, string gender, string id, string address, string tel, string dob, string img_path, ref string error)
        {
            //string sql = "INSERT INTO Customer VALUES('" + cus_id + "',N'" + name + "',N'" + mid_name + "',N'" + sur_name + "','" + gender + "','" + id + "',N'" + address + "','" + tel + "','" + dob + "',N'" + img_path + "')";
            return db.MyExecuteNonQuery("usp_iKhachHang", CommandType.StoredProcedure, ref error,
              new SqlParameter("@cus_id", cus_id),
              new SqlParameter("@name", name),
              new SqlParameter("@mid_name", mid_name),
              new SqlParameter("@sur_name", sur_name),
              new SqlParameter("@gender", gender),
              new SqlParameter("@id", id),
              new SqlParameter("@add", address),
              new SqlParameter("@tel", tel),
              new SqlParameter("@dob", DateTime.Parse(dob)),
              new SqlParameter("@img", img_path)
              );
        }

        //public bool Delete(string cus_id, ref string error)
        //{
        //    string sql = "DELETE FROM Customer WHERE Cus_ID='" + cus_id + "'";
        //    return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        //}

        public bool Update(string cus_id, string name, string mid_name, string sur_name, string gender, string id, string address, string tel, string dob, string img_path, ref string error)
        {
            //string sql = "UPDATE Customer SET Name=N'" + name + "', Mid_Name=N'" + mid_name + "', Sur_Name=N'" + sur_name + "', gender='" + gender + "',ID='" + id + "',Address=N'" + address + "',Tel='" + tel + "',DOB='" + dob + "',Img_Path=N'" + img_path + "' WHERE Cus_ID='"+cus_id+"'";
            //return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
            return db.MyExecuteNonQuery("usp_iNhanVien", CommandType.StoredProcedure, ref error,
              new SqlParameter("@cus_id", cus_id),
              new SqlParameter("@name", name),
              new SqlParameter("@mid_name", mid_name),
              new SqlParameter("@sur_name", sur_name),
              new SqlParameter("@gender", gender),
              new SqlParameter("@id", id),
              new SqlParameter("@add", address),
              new SqlParameter("@tel", tel),
              new SqlParameter("@dob", DateTime.Parse(dob)),
              new SqlParameter("@img", img_path)
              );
        }
        public DataSet Search(int choise, string str, ref string error)
        {
            string sql = null;
            switch (choise)
            {
                case 0:
                    sql = "SELECT * FROM dbo.Customer WHERE Cus_ID = (SELECT Cus_ID  FROM dbo.Customer WHERE Lower(Cus_ID)=Lower('" + str + "'))";
                    break;
                case 1:
                    sql = "SELECT * FROM dbo.Customer WHERE Lower(Name)=Lower(N'" + str + "')";
                    break;
                case 2:
                    sql = "SELECT * FROM dbo.Customer WHERE Cus_ID = (SELECT Cus_ID  FROM dbo.Customer WHERE Lower(ID)=Lower('" + str + "'))";
                    break;
            }
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
        }
    }
}
