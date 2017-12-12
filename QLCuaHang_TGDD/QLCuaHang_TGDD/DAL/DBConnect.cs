using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHang_TGDD.DAL
{
    public class DBConnect
    {
        public SqlConnection conn;//= new SqlConnection(@"Server=192.168.0.102;Initial Catalog=QLCuaHang;User id=Admin ; Password=123");
        protected SqlCommand cmd=null;
        protected SqlDataAdapter da=null;

        public static string connectStr;// = new SqlConnection(@"Data Source=KIRITO;Initial Catalog=TGDD;Integrated Security=True;");
        public DBConnect()
        {

        }

        ////kiểm tra chuỗi connection
        public bool ConnectServer(string sqlConnection, ref string error)
        {
            conn = new SqlConnection(sqlConnection);
            cmd = conn.CreateCommand();
            if (conn.State == ConnectionState.Open)
                conn.Close();
            bool flag = false;  //mặc định là false
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                flag = true;    //connect được thì trả về true
                connectStr = sqlConnection;
                conn.Close();
            }
            return flag;
        }


    }
}

