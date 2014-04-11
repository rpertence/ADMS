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
    public class Foto_Categoria
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string titulo, string icone, string descricao)
        {
                string SQL = @"INSERT INTO `foto_categoria` 
                          (`titulo`, `icone`, `descricao`) 
                          VALUES
                          ('" + titulo + "','" + icone + "','" + descricao + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT fc.`id`, fc.`titulo`, fc.`icone`, fc.`descricao` FROM foto_categoria fc ORDER BY fc.`titulo` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT fc.`id`, fc.`titulo`, fc.`icone`, fc.`descricao` FROM foto_categoria fc WHERE fc.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar foto_categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string titulo, string icone, string descricao)
        {
            string SQL = @"UPDATE foto_categoria SET titulo = '" + titulo + "', icone = '" + icone + "', descricao = '" + descricao + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM foto_categoria WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM foto_categoria";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}