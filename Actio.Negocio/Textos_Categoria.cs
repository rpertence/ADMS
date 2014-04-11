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
    public class Textos_Categoria
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string titulo, string icone)
        {
                string SQL = @"INSERT INTO `textos_categoria` 
                          (`titulo`, `icone`) 
                          VALUES
                          ('" + titulo + "','" + icone + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT t.`id`, t.`titulo`, t.`icone` FROM textos_categoria t ORDER BY t.`id` DESC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT t.`id`, t.`titulo`, t.`icone` FROM textos_categoria t WHERE t.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar textos_categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string titulo, string icone)
        {
            string SQL = @"UPDATE textos_categoria SET titulo = '" + titulo + "', icone = '" + icone + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM textos_categoria WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM textos_categoria";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
        #region páginas públicas
        #region Seleciona TODOS MENOS O "0" e 4
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIDAllForSite()
        {
            string SQL = string.Format("SELECT t.`id`, t.`titulo`, t.`icone` FROM textos_categoria t  WHERE t.`id` != '0' AND t.`id` != '4' ");
            return conexao.Dados(SQL);
        }
        #endregion

        #endregion
    }
}