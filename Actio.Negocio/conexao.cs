using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Configuration;

namespace Actio.Negocio
{
    public class conexao
    {
        public static MySqlConnection getConn()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ConnectionString"];
            MySqlConnection objConn = new MySqlConnection(settings.ConnectionString);
            return objConn;
        }
        public static void ExecuteNonQuery(string SQL)
        {
            using (MySqlConnection conn = getConn())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SQL, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
        public static string ExecuteScalar(string SQL)
        {
            using (MySqlConnection conn = getConn())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SQL, conn);
                return cmd.ExecuteScalar().ToString();
            }
        }
        public static DataTable Dados(string SQL)
        {
            DataTable dtResult = null;

            using (MySqlConnection conn = getConn())
            {
                dtResult = new DataTable("DTResult");

                MySqlCommand cmd = new MySqlCommand(SQL, conn);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                conn.Open();
                adap.Fill(dtResult);

            }

            return dtResult;
        }
}
}
