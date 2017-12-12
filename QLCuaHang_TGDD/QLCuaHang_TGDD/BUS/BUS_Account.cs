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
    public class BUS_Account
    {
        DAL_Main db = null;

        public BUS_Account()
        {
            db = new DAL_Main();
        }

        public int Check(string emp_id, ref string err)
        {
            //string sql= "select count(*) from [qlcuahang].[dbo].[employee] where emp_id='" + emp_id + "'";
            //return db.ExecuteScalar(sql, CommandType.Text,ref err);
            return 0;
        }

        public int SignIn(string username, string pass, ref string err)
        {
            //string sql = "SELECT COUNT(*) FROM [QLCuaHang].[dbo].[Account] WHERE Username=N'" + username + "' AND Password=N'" + pass + "'";
            //return db.ExecuteScalar(sql); 
            /*int k=db.ExecuteScalar(sql);
            if( k!=0)
            {
                sql="select "
            }*/

            return db.ExecuteScalar("SELECT dbo.func_Login(@username, @password)", CommandType.Text, ref err, username, pass);
                //new SqlParameter("@username",username),
                //new SqlParameter("@password",pass));
        }

        //public bool SignUp(string emp_id, string cus_id, string user, string pass, ref string error)
        //{
        //    string sql = "INSERT INTO ACCOUNT VALUES(N'" + emp_id +"',N'" + cus_id +"',N'" + user + "',N'" + pass + "')";
        //    return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        //}

        public bool ChangePassword (string username, string oldpass, string newpass, ref string err)
        {
            return db.MyExecuteNonQuery("usp_uMatKhau", CommandType.StoredProcedure, ref err,
                new SqlParameter("@username", username),
                new SqlParameter("@oldpass", oldpass),
                new SqlParameter("@newpass", newpass)
                );
        }
    }
}
