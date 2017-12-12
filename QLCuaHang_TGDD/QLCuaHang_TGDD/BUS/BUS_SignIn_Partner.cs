using QLCuaHang_TGDD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHang_TGDD.BUS
{
    class BUS_SignIn_Partner
    {
        DAL_Main db = null;

        public BUS_SignIn_Partner()
        {
            db = new DAL_Main();
        }


        public bool DBAcess(string ip, string un, string pw)
        {
            return DBAcess(ip, un, pw);
        }

        public bool SignInPartner(string ip, string dbname, string userpn, string passpn, ref string error)
        {
            string sql = @"Server="+ip+";Initial Catalog="+dbname+";Integrated Security=True; User id="+userpn+"; Password=" + passpn;
            return db.ConnectPartner(sql, ref error);
        }
    }
}
