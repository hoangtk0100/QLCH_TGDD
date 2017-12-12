using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCuaHang_TGDD.DAL;
using System.Data;

namespace QLCuaHang_TGDD.BUS
{
    public class BUS_Store
    {
        DAL_Main db = null;
        public BUS_Store()
        {
            db = new DAL_Main();
        }
        public static string sql0 = "SELECT DISTINCT id.Imp_ID,s.Pro_ID,p.Name,p.Dis_ID, d.Dis_Name,s.Quantity, s.Imp_Price, ib.Date,ib.Emp_ID, p.Img_Path " +
                                    "FROM dbo.Store s, dbo.Product p, dbo.Import_Bill ib, dbo.Import_Detail id, dbo.Distributor d " +
                                    "WHERE s.Pro_ID = p.Pro_ID AND s.Pro_ID = id.Pro_ID AND id.Imp_ID = ib.Imp_ID AND d.Dis_ID = p.Dis_ID ";

        public static string sql1 = "SELECT DISTINCT id.Imp_ID, s.Pro_ID,p.Name,p.Dis_ID, d.Dis_Name,s.Quantity, s.Imp_Price, ib.Date,ib.Emp_ID, p.Img_Path INTO Store_Imp_Bill " +
                                    "FROM dbo.Store s, dbo.Product p, dbo.Import_Bill ib, dbo.Import_Detail id, dbo.Distributor d " +
                                    "WHERE s.Pro_ID = p.Pro_ID AND s.Pro_ID = id.Pro_ID AND id.Imp_ID = ib.Imp_ID AND d.Dis_ID = p.Dis_ID ";
        public static string sql2= "WITH tem AS(SELECT ROW_NUMBER() OVER(PARTITION BY s.Imp_ID,s.Pro_ID, s.Name, s.Dis_ID, s.Dis_Name, s.Quantity, s.Imp_Price, s.Date,s.Emp_ID,s.Img_Path " +
                                    "ORDER BY s.Imp_ID) AS rownumber, *FROM Store_Imp_Bill s) DELETE FROM tem WHERE rownumber>0 ";

        public static int d = 0;
        public DataSet Get()
        {
            return db.ExecuteQueryDataSet(sql0, CommandType.Text);
        }
        public DataSet Get_Imp_Bill(ref string error)
        {
            string sql = null;
            try
            {
                if (d == 0)
                {
                    d = 1;
                    db.MyExecuteNonQuery(sql1, CommandType.Text, ref error);
                    db.MyExecuteNonQuery(sql2, CommandType.Text, ref error);
                }
                sql = " Select *From Store_Imp_Bill";
            }catch { }
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
        }
        public bool Import(int k,string imp_id,string pro_id,string pro_name, string dis_id,string dis_name, int quantity,float imp_price, string imp_date,string emp_id ,string img_path,ref string error)
        {
            string sql = null;
            if (k == 0)
            {
                    sql =" insert into Import_Bill values('" + imp_id + "','" + dis_id + "','" + emp_id + "','" + imp_date + "') " +
                    " Insert into Import_Detail values('" + imp_id + "','" + pro_id + "'," + quantity + "," + imp_price + ") " +
                    " Insert into Store_Imp_Bill values('" + imp_id + "','" + pro_id + "',N'" + pro_name + "','" + dis_id + "',N'" + dis_name + "'," + quantity + "," + imp_price + ",'" + imp_date + "','" + emp_id + "','" + img_path + "')";
            }
            else
            {
                sql = " Insert into Store_Imp_Bill values('" + imp_id + "','" + pro_id + "',N'" + pro_name + "','" + dis_id + "',N'" + dis_name + "'," + quantity + "," + imp_price + ",'" + imp_date + "','" + emp_id + "','" + img_path + "')" +
                    " update Import_Detail Set Iquantity=" + quantity + ",Price=" + imp_price + " Where Imp_ID='" + imp_id + "' and Pro_ID='" + pro_id + "'" +
                    " Update Import_Bill Set Dis_ID='" + dis_id + "',Emp_ID='" + emp_id + "', Date='" + imp_date + "' where Imp_ID='" + imp_id + "'";
                     //"Update Store Set Quantity+=" + quantity + " and Imp_Price=" + imp_price + " where Pro_ID='" + pro_id + "'";
            }
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }
        public bool Delete(string imp_id,string pro_id, ref string error)
        {
            string sql = " DELETE FROM dbo.Import_Detail WHERE Imp_ID='"+imp_id+"' and Pro_ID='"+pro_id+"'"+
                          " DELETE FROM dbo.Import_Bill WHERE Imp_ID='"+imp_id+"'";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }
        public bool Update(int k, string imp_id, string pro_id, string pro_name, string dis_id, string dis_name, int quantity, float imp_price, string imp_date, string emp_id ,ref string error)
        {
            string sql = " update Import_Detail Set Iquantity=" + quantity + ",Price=" + imp_price + ", Pro_ID='" + pro_id + "' Where Imp_ID='" + imp_id + "'"+
                " Update Import_Bill Set Dis_ID='" + dis_id + "',Emp_ID='" + emp_id + "', Date='" + imp_date + "' where Imp_ID='" + imp_id + "'"; 
            return db.MyExecuteNonQuery(sql,CommandType.Text, ref error);
        }
        public DataSet Search(int choise, string str, ref string error)
        {
            string sql = null;
            switch (choise)
            {
                case 0://imp_id
                    sql =sql0+"and Lower(id.Imp_ID)=Lower('" + str + "')";
                    break;
                case 1://pro_id
                    sql = sql0 + "and Lower(id.Pro_ID)=Lower('" + str + "')";
                    break;
                case 2://Pro_name
                    sql= sql0 + "and Lower(p.Name)=Lower(N'" + str + "')";
                    break;
                case 3://Dis_Id
                    sql = sql0 + "and Lower(ib.Dis_ID)=Lower('" + str + "')";
                    break;
                case 4://Dis_name
                    sql = sql0 + "and Lower(d.Dis_Name)=Lower(N'" + str + "')";
                    break;
            }
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
        }
    }
}
