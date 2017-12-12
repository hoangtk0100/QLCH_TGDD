using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace QLCuaHang_TGDD.DAL
{
    public class DAL_Main:DBConnect
    {
        public static string strConnect = DBConnect.connectStr;

        public DAL_Main()
        {
            //strConnect = @"Server=192.168.0.102;Initial Catalog=QLCuaHang;User id=Admin ; Password=123";//-----//
            conn = new SqlConnection(strConnect);
            cmd = conn.CreateCommand();
        }


        //public bool dbaccess(string ip, string un, string pw)
        //{
        //    strconnect = "server=" + ip + ";database=qlcuahang; user id = " + un + "; password = " + pw + ";";
        //    conn = new sqlconnection(strconnect);
        //    cmd = conn.createcommand();
        //    try
        //    {
        //        if (conn.state == connectionstate.open)
        //        {
        //            conn.close();
        //        }
        //        conn.open();
        //        if (conn.state == connectionstate.open)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (exception)
        //    {
        //        return false;
        //    }
        //}

        public bool ConnectPartner(string sql, ref string error)
        {
            conn = new SqlConnection(sql);
            cmd = conn.CreateCommand();
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();

            bool flag = false;
            if (conn.State == ConnectionState.Open)
                flag = true;
            return flag;
        }
        public int ExecuteScalar(string sql, CommandType cmt, ref string error, string a,string b)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd.Parameters.Clear();
            cmd.CommandText = sql;
            cmd.CommandType=cmt;
            //cmd = new SqlCommand(sql, conn);

            int x = 0;
            cmd.Parameters.AddWithValue("@username", a);
            cmd.Parameters.AddWithValue("@password", b);
            //foreach (SqlParameter i in para)

            //    cmd.Parameters.Add(i);
            try
            {
                x = (int)cmd.ExecuteScalar();

            }
            catch (SqlException e)
            {
                error = e.Message;
            }
            finally
            {
                conn.Close();
            }
            return x;
        }

        public DataSet ExecuteQueryDataSet(string sql, CommandType t)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd.CommandText = sql;
            cmd.CommandType = t;
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet ExecuteQueryDataSetWithPra(string strSQL, CommandType ct, params SqlParameter[] param)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            cmd.Parameters.Clear();
            foreach (SqlParameter p in param)
                cmd.Parameters.Add(p);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet ExecuteScala(string sql, CommandType t, params SqlParameter[] para)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd.CommandType = t;
            cmd.CommandText = sql;
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public bool MyExecuteNonQuery(string sql, CommandType t, ref string error,params SqlParameter[] para)
        {
            bool flag = false;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = t;
            cmd.CommandText = sql;

            foreach(SqlParameter p in para)
            {
                cmd.Parameters.Add(p);
            }

            try
            {
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }

        public SqlDataReader ExecuteReader(string sql, CommandType t, ref string error)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
    }
}
