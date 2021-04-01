using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace project.DAL
{
    class DataProvider
    {
        //khai bao các phần kết nối và xử lý databse
        SqlConnection conn; //kết nối dbi
        SqlDataAdapter da; //xử lý câu lệnh sql: select
        SqlCommand cmd; //thực thi câu lệnh liên quan đến insert, update, delete
        public DataProvider()
        {
            connect();
        }

        private void connect()
        {
            //get text, check authencation
            try
            {
                //sua database thi doi Catalog= 'ten database'
                string strConn = ConfigurationManager.ConnectionStrings["Education"].ConnectionString;
                conn = new SqlConnection(strConn);

                //check xem conn có đang mở hay không, nêu mở thì đóng lại để mở cổng mới
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                //sau khi check xong thì open cổng kết nối mới
                conn.Open();
                Console.WriteLine("Connect successfully!! ");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Connect error: " + ex.Message);
            }
        }

        //hàm excute 1 câu lệnh select nó sẽ trả về 1 dataTable lưu các record lấy được
        public DataTable executeQuery(string strSelect)
        {
            connect();
            DataTable dt = new DataTable();// chứa dữ liệu sau khi select về
            try
            {
                //thuc hien query
                da = new SqlDataAdapter(strSelect, conn);

                //select xong thì fill vào dataTable để chứa dữ liệu
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excute query loi: " + ex.Message);
            }
            return dt;
        }

        public void executeNonQuery(string query)
        {
            try
            {
                connect();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                //executenonquery dành cho các query không trả về dữ liệu còn executequery là dành cho trường hợp có trả về dữ liệu(Select query)
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                Console.WriteLine("Excute non query loi: " + e.Message);
            }

        }
    }

}