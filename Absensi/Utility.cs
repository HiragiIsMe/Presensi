using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Absensi
{
    class Connection
    {
        public static string connection = @"Data Source=DESKTOP-HUJGH1E\SQLEXPRESS;Initial Catalog=PresensiDB;Integrated Security=True";

        public static SqlConnection conn = new SqlConnection(connection);
    }

    class Enc
    {
        public static string getPass(string pass)
        {
            using (SHA256Managed sha = new SHA256Managed())
            {
                byte[] vs = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
                string password = Convert.ToBase64String(vs);

                return password;
            }
        }
    }

    class Model
    {
        public static int id { set; get; }
        public static string name { set; get; }
    }
}
