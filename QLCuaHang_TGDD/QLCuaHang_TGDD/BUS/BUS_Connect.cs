using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCuaHang_TGDD.DAL;
namespace QLCuaHang_TGDD.BUS
{
    public class BUS_Connect:DBConnect
    {
        DBConnect db = null;

        public BUS_Connect()
        {
            db = new DBConnect();
        }

        //kiểm tra tên server, username, pass
        public bool SignInPartner(string ip, string userpn, string passpn, ref string error)
        {
            string sql = "Server=" + ip + ";Initial Catalog=QLCuaHang;User Id=" + userpn + ";Password=" + passpn + ";";   //tạo chuỗi kết nối
            return db.ConnectServer(sql, ref error);
        }
    }
}
