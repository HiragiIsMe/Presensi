using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Absensi
{
    class Connection
    {
        public static string connection = @"Data Source=LAPTOP-S8UCE514;Initial Catalog=Presensi;Integrated Security=True";

        public static SqlConnection conn = new SqlConnection(connection);

        public static DataTable getdata(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();

            return dt;
        }
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

        public static byte[] Encode(Image img)
        {
            ImageConverter convert = new ImageConverter();
            byte[] image = (byte[])convert.ConvertTo(img, typeof(byte[]));
            return image;
        }
    }

    class Model
    {
        public static int id { set; get; }
        public static string name { set; get; }
    }
}
