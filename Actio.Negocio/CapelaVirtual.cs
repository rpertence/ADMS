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
    public class Capela_Virtual
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string descricao, string nome, string email, string status)
        {
            string SQL = @"INSERT INTO `capela_virtual` 
                          (`descricao`, `nome`, `email`, `status`) 
                          VALUES
                          ('" + descricao + "','" + nome + "','" + email + "','" + status + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format(
                "SELECT " +
                "a.`id`, a.`descricao`, a.`nome`, a.`email`, a.`status` FROM capela_virtual a ORDER BY a.`descricao`"
                );
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format(
                "SELECT a.`id`, a.`descricao`, a.`email`, a.`nome`, a.`status` FROM capela_virtual a WHERE a.`id` = '" + id + "'"
                );
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar 
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string descricao, string nome, string email, string status)
        {
            string SQL = 
                @"UPDATE capela_virtual SET " +
                "descricao = '" + descricao + "', " +
                "nome = '" + nome + "', " +
                "email = '" + email + "', " +
                "status = '" + status + "' " + 
                "WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM capela_virtual WHERE id = '" + id + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM capela_virtual";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
        #region paginas publicas
        #region seleciona todos liberados
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllLiberados()
        {
            string SQL = string.Format(
                "SELECT " +
                "a.`id`, a.`descricao`, a.`nome`, a.`email`, a.`status`, CASE WHEN a.`status` = '1' THEN 'ativo' ELSE 'inativo' END Ativo FROM capela_virtual a WHERE a.`status` = '1' ORDER BY a.`id` ASC"
                );
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos bloqueados
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllBloqueados()
        {
            string SQL = string.Format(
                "SELECT " +
                "a.`id`, a.`descricao`, a.`nome`, a.`email`, a.`status`, CASE WHEN a.`status` = '1' THEN 'ativo' ELSE 'inativo' END Ativo FROM capela_virtual a WHERE a.`status` = '0' ORDER BY a.`id` ASC"
                );
            return conexao.Dados(SQL);
        }
        #endregion
        #endregion
    }
}