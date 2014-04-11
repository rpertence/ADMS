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
    public class Produtos_Sub_Categoria
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string titulo, string icone, string id_categoria)
        {
            string SQL = @"INSERT INTO `produtos_subcategoria` 
                          (`titulo`, `icone`, `id_categoria`) 
                          VALUES
                          ('" + titulo + "','" + icone + "', '" + id_categoria + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT p.`id`, p.`titulo`, p.`icone`, p.`id_categoria` FROM produtos_subcategoria p ORDER BY p.`titulo` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT p.`id`, p.`titulo`, p.`icone`, p.`id_categoria` FROM produtos_subcategoria p WHERE p.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID Categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIDCategoria(int id_categoria)
        {
            string SQL = string.Format("SELECT p.`id`, p.`titulo`, p.`icone`, p.`id_categoria` FROM produtos_subcategoria p WHERE p.`id_categoria` = '" + id_categoria + "' order by titulo");
            return conexao.Dados(SQL);
        }
        #endregion

        #region Atualizar categoria_categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string titulo, string icone, string id_categoria)
        {
            if (id_categoria == "1")
            {
                string SQLu = @"UPDATE produtos_subcategoria SET id_categoria = '0'";
                conexao.ExecuteNonQuery(SQLu);
            }
            string SQL = @"UPDATE produtos_subcategoria SET titulo = '" + titulo + "', icone = '" + icone + "', id_categoria = '" + id_categoria + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(string id)
        {
            string SQL = string.Format("DELETE FROM produtos_subcategoria WHERE id = '" + id + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir por id da categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void DeleteByIdCategoria(string id_categoria)
        {
            string SQL = string.Format("DELETE FROM produtos_subcategoria WHERE id_categoria = '" + id_categoria + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion

        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM produtos_subcategoria";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}