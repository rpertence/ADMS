using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Text;
using System.Reflection;

namespace Actio.Negocio
{
    [DataObject(true)]
    public class Links
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string url, string titulo, string alt, string status)
        {
                string SQL = @"INSERT INTO `links` 
                          (`url`, `titulo`, `alt`, `status`) 
                          VALUES
                          ('" + url + "','" + titulo + "','" + alt + "', '" + status + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT l.`id`, l.`url`, l.`titulo`, l.`alt`, l.`status`, CASE WHEN l.`status` = '1' THEN 'ativo' ELSE 'inativo' END 'ATIVO' FROM links l ORDER BY l.`id` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por id
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByid(int id)
        {
            string SQL = string.Format("SELECT l.`id`, l.`url`, l.`titulo`, l.`alt`, l.`status` FROM links l WHERE l.`id` = " + id);
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por status ativo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByStatusAtivo()
        {
            string SQL = string.Format("SELECT l.`id`, l.`url`, l.`titulo`, l.`alt`, l.`status` FROM links l WHERE l.`status` = '1';");
            return conexao.Dados(SQL);
        }
        #endregion

        #region Atualizar banner
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string url, string titulo, string alt, string status)
        {
            string SQL = @"UPDATE links SET url = '" + url + "', titulo = '" + titulo + "', alt = '" + alt + "', status = '" + status + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region excluir por id
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM links WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextid
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextid FROM links";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}