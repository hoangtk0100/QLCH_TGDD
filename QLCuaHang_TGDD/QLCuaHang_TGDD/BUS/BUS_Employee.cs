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
    public class BUS_Employee
    {
        DAL_Main db = null;
        public BUS_Employee()
        {
            db = new DAL_Main();
        }

        public DataSet Get()
        {
            return db.ExecuteQueryDataSet("usp_getNhanVien", CommandType.Text);
        }

        public bool Add(string emp_id, string name, string mid_name,
            string sur_name, string gender, string id, string address, string tel, 
            string dob, string job, string img_path,string stt, ref string error)
        { 
            //string sql = "INSERT INTO Employee VALUES('"+emp_id+"',N'"+name+ "',N'"+mid_name+ "',N'"+sur_name+ "','"+gender+ "','"+id + "',N'"+address + "','" +tel + "','" +dob+ "','" +job + "',N'" +img_path + "')";
            return db.MyExecuteNonQuery("usp_iNhanVien", CommandType.StoredProcedure, ref error,
                new SqlParameter("@emp_id",emp_id),
                new SqlParameter("@name",name),
                new SqlParameter("@mid_name",mid_name),
                new SqlParameter("@sur_name",sur_name),
                new SqlParameter("@gender", gender),
                new SqlParameter("@id",id),
                new SqlParameter("@add",address),
                new SqlParameter("@tel",tel),
                new SqlParameter("@dob",DateTime.Parse(dob)),
                new SqlParameter("@jobid",job),
                new SqlParameter("@img",img_path),
                new SqlParameter("@status",stt)
                );
        }
        //public bool Delete(string emp_id, ref string error)
        //{
        //    //string sql = "DELETE FROM Employee WHERE Emp_ID='" + emp_id + "'";              
        //    return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        //}
        public bool Update(string emp_id, string name, string mid_name, string sur_name, string gender, string id, string address, string tel, string dob, string job, string img_path, string stt,ref string error)
        {
            //string sql = "UPDATE Employee SET Name=N'"+name+"',Mid_Name=N'" + mid_name + "',Sur_Name=N'" + sur_name + "', gender='" + gender + "',ID='" + id + "',Address=N'" + address + "',Tel='" + tel + "',DOB='" + dob + "',jobition=N'" + job + "',Img_Path=N'" + img_path + "' WHERE Emp_ID='"+emp_id+"'";
            return db.MyExecuteNonQuery("usp_uNhanVien", CommandType.StoredProcedure, ref error,
              new SqlParameter("@emp_id", emp_id),
              new SqlParameter("@name", name),
              new SqlParameter("@mid_name", mid_name),
              new SqlParameter("@sur_name", sur_name),
              new SqlParameter("@gender",gender),
              new SqlParameter("@id", id),
              new SqlParameter("@add", address),
              new SqlParameter("@tel", tel),
              new SqlParameter("@dob", DateTime.Parse(dob)),
              new SqlParameter("@jobid", job),
              new SqlParameter("@img", img_path),
              new SqlParameter("@status", stt)
              );
        }

        public DataSet Search(int choise, string str, ref string error)
        {
            string sql=null;
            switch(choise)
            {
                case 0:
                    sql = "SELECT * FROM dbo.Employee WHERE Emp_ID = (SELECT Emp_ID  FROM dbo.Employee WHERE Lower(Emp_ID)=Lower('" + str + "'))";
                    break;
                case 1:
                    sql = "SELECT * FROM dbo.Employee WHERE Lower(Name)=Lower(N'" + str + "')";
                    break;
                case 2:
                    sql = "SELECT * FROM dbo.Employee WHERE Emp_ID = (SELECT Emp_ID  FROM dbo.Employee WHERE Lower(ID)=Lower('" + str + "'))";
                    break;
            }
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
        }
    }
}
