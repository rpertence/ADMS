using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace Actio.Negocio
{
    public class ClienteCSV
    {
        public void Inserir(string usuario_nome, string usuario_email, string usuario_senha, string opt_cliente, string usuario_status, string usuario_filiado, string usuario_situacao, string usuario_tipo, string usuario_sexo, string cod_categoria, string cod_categoria_regional)
        {
            MySqlConnection conn = conexao.getConn();

            string sql =
@"INSERT INTO usuario (
usuario_nome, 
usuario_email, 
usuario_senha, 
opt_cliente, 
usuario_status, 
usuario_filiado, 
usuario_situacao, 
usuario_tipo, 
usuario_sexo, 
cod_categoria, 
cod_categoria_regional)
VALUES 
(@usuario_nome, 
@usuario_email, 
@usuario_senha, 
@opt_cliente, 
@usuario_status, 
@usuario_filiado, 
@usuario_situacao, 
@usuario_tipo, 
@usuario_sexo, 
@cod_categoria, 
@cat_regional)";

            sql = sql.Replace("@usuario_nome", string.Format("'{0}'", usuario_nome));
            sql = sql.Replace("@usuario_email", string.Format("'{0}'", usuario_email));
            sql = sql.Replace("@usuario_senha", string.Format("'{0}'", usuario_senha));
            sql = sql.Replace("@opt_cliente", string.Format("'{0}'", opt_cliente));
            sql = sql.Replace("@usuario_status", string.Format("'{0}'", usuario_status));
            sql = sql.Replace("@usuario_filiado", string.Format("'{0}'", usuario_filiado));
            sql = sql.Replace("@usuario_situacao", string.Format("'{0}'", usuario_situacao));
            sql = sql.Replace("@usuario_tipo", string.Format("'{0}'", usuario_tipo));
            sql = sql.Replace("@usuario_sexo", string.Format("'{0}'", usuario_sexo));
            sql = sql.Replace("@cod_categoria", string.Format("'{0}'", cod_categoria));
            sql = sql.Replace("@cat_regional", string.Format("'{0}'", cod_categoria_regional));

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
