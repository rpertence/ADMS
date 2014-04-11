using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Text;
using System.Reflection;


namespace Actio.Negocio
{
    [DataObject(true)]
    public class Produtos_Estoque
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string id_produto, string quantidade)
        {
            string SQL = @"INSERT INTO `produtos_estoque` 
                          (`id_produto`, `quantidade`) 
                          VALUES
                          ('" + id_produto + "','" + quantidade + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT p.`id`, p.`id_produto`, p.`quantidade` FROM produtos_estoque p ORDER BY p.`id_produto` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT p.`id`, p.`id_produto`, p.`quantidade` FROM produtos_estoque p WHERE p.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar categoria_categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string id_produto, string quantidade)
        {
            string SQL = @"UPDATE produtos_estoque SET id_produto = '" + id_produto + "', quantidade = '" + quantidade + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(string id)
        {
            string SQL = string.Format("DELETE FROM produtos_estoque WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
            string SQLP = string.Format("DELETE FROM produtos WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM produtos_estoque";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}