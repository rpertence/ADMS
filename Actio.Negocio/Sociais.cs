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
    public class Sociais
    {
        #region Novo
        public static void Inserir(string status, string icone, string link, string titulo)
        {
            string SQL = @"INSERT INTO `sociais` 
                          (`status`, `icone`, `link`, `titulo`) 
                          VALUES
                          ('" + status + "','" + icone + "','" + link + "','" + titulo + "');";

            conexao.ExecuteNonQuery(SQL);            
        }
        #endregion
        #region Seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT s.`id`, s.`status`, s.`icone`, s.`link`, s.`titulo`, CASE WHEN s.`status` = '1' THEN 'ativo' ELSE 'inativo' END ATIVO FROM sociais s ORDER BY s.`id` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectOne(int id)
        {
            string SQL = string.Format("SELECT s.`id`, s.`status`, s.`icone`, s.`link`, `titulo`, CASE WHEN s.`status` = '1' THEN 'ativo' ELSE 'inativo' END ATIVO FROM sociais s WHERE s.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void Update(string id, string status, string icone, string titulo, string link)
        {
            string SQL = @"UPDATE sociais SET status = '" + status + "', icone = '" + icone + "', titulo = '" + titulo+ "', link = '" + link + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(int id)
        {
            string SQL = string.Format("DELETE FROM sociais WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM sociais";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
        #region site
        #region seleciona sociais ativo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectActive()
        {
                string SQL = "SELECT s.`id`, s.`status`, s.`icone`, s.`link`, s.`titulo` FROM sociais s WHERE s.`status` = '1' ORDER BY s.`id` ASC";
                return conexao.Dados(SQL);
        }
                #endregion
        #endregion
    }
}