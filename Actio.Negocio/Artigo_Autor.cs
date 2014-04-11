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
    public class Artigo_Autor
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string nome, string email, string descricao, string icone)
        {
            string SQL = @"INSERT INTO `artigo_autor` 
                          (`nome`, `email`, `descricao`, `icone`) 
                          VALUES
                          ('" + nome + "','" + email + "','" + descricao + "','" + icone + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format(
                "SELECT " +
                "a.`id`, a.`nome`, a.`email`, a.`descricao`, a.`icone` FROM artigo_autor a ORDER BY a.`nome`"
                );
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format(
                "SELECT a.`id`, a.`nome`, a.`descricao`, a.`email`, a.`icone` FROM artigo_autor a WHERE a.`id` = '" + id + "'"
                );
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar 
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string nome, string email, string descricao, string icone)
        {
            string SQL = 
                @"UPDATE artigo_autor SET " +
                "nome = '" + nome + "', " +
                "email = '" + email + "', " +
                "descricao = '" + descricao + "', " +
                "icone = '" + icone + "' " + 
                "WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM artigo_autor WHERE id = '" + id + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM artigo_autor";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
    }
}