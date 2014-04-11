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
    public class Depoimento
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Insert(string email, string nome, string status, string descricao, string resumo, string data, string local)
        {
                string SQL = @"INSERT INTO `depoimento` 
                          (`email`, `nome`, `status`, `descricao`, `resumo`, `data`, `local`) 
                          VALUES
                          ('" + email + "','" + nome + "','" + status + "','" + descricao + "', '" + resumo + "','" + data + "','" + local + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos ativos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllActive()
        {
            string SQL = string.Format("SELECT d.`id`, d.`email`, d.`nome`, d.`status`, d.`descricao`, d.`resumo`, d.`data`, d.`data`, d.`local` FROM depoimento d WHERE d.`status` = '1' ORDER BY d.`data` DESC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos bloqueados
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllInactive()
        {
            string SQL = string.Format("SELECT d.`id`, d.`email`, d.`nome`, d.`status`, d.`descricao`, d.`resumo`, d.`data`, d.`local` FROM depoimento d WHERE d.`status` = '0' ORDER BY d.`data` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT d.`id`, d.`email`, d.`nome`, d.`status`, d.`descricao`, d.`resumo`, d.`data`, d.`local` FROM depoimento d WHERE  d.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar 
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Update(string id, string email, string nome, string status, string descricao, string resumo, string local)
        {
            string SQL = @"UPDATE depoimento SET email = '" + email + "', nome = '" + nome + "', status = '" + status + "', descricao = '" + descricao + "', resumo = '" + resumo + "', local = '" + local + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM depoimento WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM depoimento";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
        #region páginas públicas
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void InserirPeloSite(string email, string nome, string descricao, string data, string local)
        {
            string SQL = @"INSERT INTO `depoimento` 
                          (`email`, `nome`, `status`, `descricao`, `data`, `local` ) 
                          VALUES
                          ('" + email + "','" + nome + "','0','" + descricao + "', '" + data + "', '" + local + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos ativos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectUltimosActive()
        {
            string SQL = string.Format("SELECT d.`id`, d.`email`, d.`nome`, d.`status`, d.`resumo`, d.`data`, d.`data`, d.`local` FROM depoimento d WHERE d.`status` = '1' ORDER BY d.`data` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion

        #endregion
    }
}