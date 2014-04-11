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
    public class Recursos
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string titulo, string icone, string url)
        {
            string SQL = @"INSERT INTO `recursos` 
                          (`titulo`, `icone`, `url`) 
                          VALUES
                          ('" + titulo + "','" + icone + "','" + url + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT r.`id`, r.`titulo`, r.`icone`, r.`url` FROM recursos r ORDER BY r.`titulo` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT r.`id`, r.`titulo`, r.`icone`, r.`url` FROM recursos r WHERE r.`id` = '" + id + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar recursos
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string titulo, string icone, string url)
        {
            string SQL = @"UPDATE recursos SET titulo = '" + titulo + "', icone = '" + icone + "', url = '" + url + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(string id)
        {
            string SQL = string.Format("DELETE FROM recursos WHERE id = '" + id + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM recursos";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
    }
}
