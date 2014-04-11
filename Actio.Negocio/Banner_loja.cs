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
    public class Banner_Loja
    {
        #region Novo Banner
        public static void Inserir(string banner_alt, string banner_url, string banner_isAtivo, int banner_tipo, string banner_arquivo, int? banner_categoria)
        {
            string SQL = string.Format(@"INSERT INTO banner_loja (banner_alt, banner_url, banner_isAtivo, banner_arquivo, banner_tipo, banner_categoria)
VALUES('{0}', '{1}', '{2}', '{3}', '{4}', {5});", banner_alt, banner_url, banner_isAtivo, banner_arquivo, banner_tipo, banner_categoria.HasValue ? banner_categoria.Value.ToString() : "NULL");

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT b.`banner_id`, b.`banner_alt`, b.`banner_url`, b.`banner_isAtivo`, b.`banner_arquivo`, CASE WHEN b.`banner_isAtivo` = '1' THEN 'ativo' ELSE 'inativo' END ATIVO FROM banner_loja b ORDER BY b.`banner_id` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectOne(int banner_id)
        {
            string SQL = string.Format("SELECT b.`banner_id`, b.`banner_alt`, b.`banner_url`, b.`banner_isAtivo`, `banner_arquivo`, CASE WHEN b.`banner_isAtivo` = '1' THEN 'ativo' ELSE 'inativo' END ATIVO FROM banner_loja b WHERE b.`banner_id` = {0}", banner_id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void Update(string banner_id, string banner_alt, string banner_url, string banner_arquivo, string banner_isAtivo, int? banner_categoria)
        {
            string SQL = string.Format(@"UPDATE banner_loja
SET banner_alt = '{0}',
    banner_url = '{1}',
    banner_arquivo = '{2}',
    banner_isAtivo = '{3}',
    banner_categoria = {4}
WHERE banner_id = '{5}'
LIMIT 1", banner_alt, banner_url, banner_arquivo, banner_isAtivo, banner_categoria.HasValue ? banner_categoria.Value.ToString() : "NULL", banner_id);
            //string SQL = @"UPDATE banner_loja SET banner_alt = '" + banner_alt + "', banner_url = '" + banner_url + "', banner_arquivo = '" + banner_arquivo + "', banner_isAtivo = '" + banner_isAtivo + "' WHERE banner_id = '" + banner_id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(int banner_id)
        {
            string SQL = string.Format("DELETE FROM banner_loja WHERE banner_id = {0}", banner_id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM banner_loja";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
        #region Seleciona todos ativos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllActive(int tipo, int? categoria)
        {
            string SQL = string.Format(@"SELECT banner_id, banner_alt, banner_url, banner_isAtivo, banner_arquivo
FROM banner_loja
WHERE banner_isAtivo = '1'
    and banner_tipo = {0}
    and banner_categoria {1}
ORDER BY banner_id ASC", tipo, categoria.HasValue ? "= " + categoria.Value.ToString() : "IS NULL");

            return conexao.Dados(SQL);
        }
        #endregion

        #region conta quantos itens ativos nós temos
        public static int nextIDActive
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM banner_loja WHERE banner_isAtivo = '1' ";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}