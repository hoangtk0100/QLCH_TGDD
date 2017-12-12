using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QLCuaHang_TGDD.DAL;
using System.Data.SqlClient;

namespace QLCuaHang_TGDD.BUS
{
    public class BUS_Event
    {
        DAL_Main db = null;
        public BUS_Event()
        {
            db = new DAL_Main();
        }
        public static int d = 0;
        public static string sql0 = "SELECT Distinct e.Ev_ID,e.Name,e.Start_Date,e.End_Date,ed.Pro_ID,p.Name AS Product_Name,ed.Saleoff " +
                    "FROM dbo.Event e, dbo.Event_Detail ed, dbo.Product p " +
                    "WHERE e.Ev_ID = ed.Ev_ID AND ed.Pro_ID = p.Pro_ID";
        public DataSet Get()
        {
            return db.ExecuteQueryDataSet(sql0,CommandType.Text);
        }

        public bool Add(string ev_id, string pro_id, float saleoff, string name, string start_date, string end_date,ref string error)
        {
            string sql = "Insert into Event values('" + ev_id + "',N'" + name + "','" + start_date + "','" + end_date + "')" +
                "Insert into Event_Detail values('" + ev_id + "','" + pro_id + "'," + saleoff + ")";               
            return db.MyExecuteNonQuery(sql,CommandType.Text, ref error);
        }

        public bool Delete(string ev_id,string pro_id,ref string error)
        {
            string sql = "DELETE FROM Event_Detail WHERE Ev_ID='" + ev_id + "'" +
                 "DELETE FROM Event WHERE Ev_ID='" + ev_id + "'";
                
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        public bool Update(string ev_id, string pro_id, float saleoff, string name, string start_date, string end_date, ref string error)
        {
            string sql = "Update Event set Pro_ID='"+pro_id+"',Saleoff="+saleoff+ " where Ev_ID='" + ev_id + "'"+
                "Update Event_Detail set Name=N'"+name+"', Start_Date='"+start_date+"', End_Date='"+end_date+"' Where Ev_ID='"+ev_id+"'";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }
       
        public DataSet Search(int choise, string str, ref string error)
        {
            string sql=null;
            
            switch (choise)
            {
                case 0://ev_id
                    sql = sql0+" and Lower(e.Ev_ID)=Lower('" + str + "')";
                    break;
                case 1://ev_name
                    sql = sql0+" and Lower(e.Name)=Lower(N'" + str + "')";
                    break;
                case 2://pro_ID
                    sql = sql0+" and Lower(ed.Pro_ID)=Lower('" + str + "')";
                    break;
                case 3://pro_name
                    sql = sql0+" and Lower(p.Name)=Lower(N'" + str + "')";
                    break;
            }
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
        }
    }
}
