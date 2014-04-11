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
    public class Produtos_Categoria
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string titulo, string icone, string destaque)
        {
            string SQL = @"INSERT INTO `produtos_categoria` 
                          (`titulo`, `icone`, `destaque`) 
                          VALUES
                          ('" + titulo + "','" + icone + "','" + destaque + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT r.`id`, r.`titulo`, r.`icone`, r.`destaque` FROM produtos_categoria r ORDER BY r.`destaque` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT r.`id`, r.`titulo`, r.`icone`, r.`destaque` FROM produtos_categoria r WHERE r.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar categoria_categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string titulo, string icone, string destaque)
        {
            string SQL = @"UPDATE produtos_categoria SET titulo = '" + titulo + "', icone = '" + icone + "', destaque = '" + destaque + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(string id)
        {
            string SQL = string.Format("DELETE FROM produtos_categoria WHERE id = '" + id + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM produtos_categoria";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
        #region páginas públicas
        #region lista categorias e subcategorias
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectCategoriasESubcategorias()
        {
            string SQL = string.Format
                ("SELECT " +
                "(SELECT ps.`titulo` FROM produtos_subcategoria ps WHERE ps.`id_categoria` = r.`id`) subcategoria, " +
                "(SELECT ps.`id` FROM produtos_subcategoria ps WHERE ps.`id_categoria` = r.`id`) id_subcategoria, " +
                "r.`id`, r.`titulo`, r.`icone`, r.`destaque` FROM produtos_categoria r ORDER BY r.`titulo` ASC");
            return conexao.Dados(SQL);
        }

        #endregion
        #endregion
    }
}