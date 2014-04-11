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
    public class Banner
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string banner_alt, string banner_url, string banner_isAtivo, string banner_arquivo)
        {
                string SQL = @"INSERT INTO `banner` 
                          (`banner_alt`, `banner_url`, `banner_isAtivo`, `banner_arquivo`) 
                          VALUES
                          ('" + banner_alt + "','" + banner_url + "','" + banner_isAtivo + "', '" + banner_arquivo + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT b.`banner_id`, b.`banner_alt`, b.`banner_url`, b.`banner_isAtivo`, b.`banner_arquivo`, CASE WHEN b.`banner_isAtivo` = '1' THEN 'ativo' ELSE 'inativo' END 'ATIVO' FROM banner b ORDER BY b.`banner_id` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por banner_id
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectBybanner_id(int banner_id)
        {
            string SQL = string.Format("SELECT b.`banner_id`, b.`banner_alt`, b.`banner_url`, b.`banner_isAtivo`, b.`banner_arquivo` FROM banner b WHERE b.`banner_id` = " + banner_id);
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por banner_isAtivo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectBybanner_isAtivo()
        {
            string SQL = string.Format("SELECT b.`banner_id`, b.`banner_alt`, b.`banner_url`, b.`banner_isAtivo`, b.`banner_arquivo` FROM banner b WHERE b.`banner_isAtivo` = '1';");
            return conexao.Dados(SQL);
        }
        #endregion

        #region Atualizar banner
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string banner_id, string banner_alt, string banner_url, string banner_isAtivo, string banner_arquivo)
        {
            string SQL = @"UPDATE banner SET banner_alt = '" + banner_alt + "', banner_url = '" + banner_url + "', banner_isAtivo = '" + banner_isAtivo + "', banner_arquivo = '" + banner_arquivo + "' WHERE banner_id = '" + banner_id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        #region excluir por banner_id
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int banner_id)
        {
            string SQL = string.Format("DELETE FROM banner WHERE banner_id = {0}", banner_id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #endregion
        #region proximo banner_id
        public static int nextbanner_id
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextbanner_id FROM banner";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}