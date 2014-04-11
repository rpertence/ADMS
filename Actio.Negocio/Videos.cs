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
    public class Videos
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string titulo, string descricao, string codigo, string icone, string status, string destaque)
        {
            if (destaque == "1")
            {
              string SQLU = @"UPDATE videos SET destaque = '0';";
              conexao.ExecuteNonQuery(SQLU);
            }
            string SQL = @"INSERT INTO `videos` 
                          (`titulo`, `descricao`, `codigo`, `icone`, `status`, `destaque`) 
                          VALUES
                          ('" + titulo + "','" + descricao + "','" + codigo + "','" + icone + "','" + status + "','" + destaque + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Atualizar
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(string id, string titulo, string descricao, string codigo, string icone, string status, string destaque)
        {
            if (destaque == "1")
            {
                string SQLU = @"UPDATE videos SET destaque = '0';";
                conexao.ExecuteNonQuery(SQLU);
            }
            string SQL = @"UPDATE videos SET titulo = '" + titulo + "', descricao = '" + descricao + "', codigo = '" + codigo + "', icone = '" + icone + "', status = '" + status + "', destaque = '" + destaque + "' WHERE id = '" + id + "' LIMIT 1;";
            conexao.ExecuteNonQuery(SQL);                         
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT v.`id`, v.`titulo`, v.`descricao`, v.`codigo`, v.`icone`, v.`status`, v.`destaque`, CASE WHEN v.`status` = '1' THEN 'ativo' ELSE 'inativo' END 'ATIVO', CASE WHEN v.`destaque` = '1' THEN 'DESTAQUE' ELSE '' END 'DISTAK' FROM videos v ORDER BY v.`id` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT v.`id`, v.`titulo`, v.`icone`, v.`descricao`, v.`codigo`, v.`status`, v.`destaque` FROM videos v WHERE v.`id` = '" + id
 + "' ORDER BY v.`descricao` DESC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona o Destaque
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectDestaque()
        {
            string SQL = string.Format("SELECT v.`id`, v.`titulo`, v.`icone`, v.`descricao`, v.`codigo`, v.`status`, v.`destaque` FROM videos v WHERE v.`destaque` = '1' AND v.`status` = '1';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos Ativos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllActive()
        {
            string SQL = string.Format("SELECT v.`id`, v.`titulo`, v.`descricao`, v.`codigo`, v.`icone`, v.`status`, v.`destaque` FROM videos v WHERE v.`status` = '1' AND v.`destaque` = '0' ORDER BY v.`id` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void DeleteByIdUsuario(string titulo)
        {
            string SQL = string.Format("DELETE FROM videos WHERE titulo = '" + titulo + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM videos WHERE id = '" + id + "'");
            conexao.ExecuteNonQuery(SQL);
        }

        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM videos";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
    }
}
