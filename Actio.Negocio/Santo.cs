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
    public class Santo
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string nome, string descricao, string dia, string mes, string icone)
        {
            string SQL = @"INSERT INTO `santo` 
                          (`nome`, `descricao`, `dia`, `mes`, `icone`) 
                          VALUES
                          ('" + nome + "','" + descricao + "','" + dia + "', '" + mes + "', '" + icone + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT s.`id`, s.`nome`, s.`descricao`, s.`dia`, s.`mes`, s.`icone` FROM santo s ORDER BY s.`nome` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT s.`id`, s.`nome`, s.`descricao`, s.`dia`, s.`mes`, s.`icone` FROM santo s WHERE s.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por Dia e Mes
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByDiaMes(string dia, string mes)
        {
            string SQL = string.Format("SELECT s.`id`, s.`nome`, s.`descricao`, s.`dia`, s.`mes`, s.`icone` FROM santo s WHERE s.`dia` = '" + dia + "' AND s.`mes` = '" + mes + "' ORDER BY s.`nome` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por Mes
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByMes(string mes)
        {
            string SQL = string.Format("SELECT s.`id`, s.`nome`, s.`descricao`, s.`dia`, s.`mes`, s.`icone`  FROM santo s WHERE s.`mes` = '" + mes + "' GROUP BY s.`dia` ORDER BY s.`dia` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar santo
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string nome, string descricao, string dia, string mes, string icone)
        {
            string SQL = @"UPDATE santo SET nome = '" + nome + "', descricao = '" + descricao + "', dia = '" + dia + "', mes = '" + mes + "', icone = '" + icone + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM santo WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM santo";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
        #region Busca por parametro
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SearchByNome(string parametro)
        {
            string SQL = string.Format("SELECT s.`id`, s.`nome`, s.`descricao`, s.`dia`, s.`mes`, s.`icone` FROM santo s WHERE s.`nome` LIKE '%" + parametro + "%'");
            return conexao.Dados(SQL);
        }
        #endregion
    }
}