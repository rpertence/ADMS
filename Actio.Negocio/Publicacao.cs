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
    public class Publicacao
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string titulo, string descricao, string anexo, string icone, string data_publicacao, string edicao)
        {
                string SQL = @"INSERT INTO `publicacoes` 
                          (`titulo`, `descricao`, `anexo`, `icone`,`data_publicacao`,`edicao`) 
                          VALUES
                          ('" + titulo + "','" + descricao + "','" + anexo + "','" + icone + "','" + data_publicacao + "','" + edicao + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT p.`id`, p.`titulo`, p.`descricao`, p.`anexo`, p.`icone`, p.`data_publicacao`, p.`edicao` FROM publicacoes p ORDER BY p.`data_publicacao`, p.`edicao` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT p.`id`, p.`titulo`, p.`descricao`, p.`anexo`, p.`icone`, p.`data_publicacao`, p.`edicao` FROM publicacoes p WHERE p.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar publicacoes
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string titulo, string descricao, string anexo, string icone, string data_publicacao, string edicao)
        {
            string SQL = @"UPDATE publicacoes SET titulo = '" + titulo + "', descricao = '" + descricao + "', anexo = '" + anexo + "', icone = '" + icone + "', data_publicacao = '" + data_publicacao + "', edicao = '" + edicao + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM publicacoes WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM publicacoes";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
        #region Seleciona por Edição
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByEdicao()
        {
            string SQL = string.Format("SELECT p.`id`, p.`titulo`, p.`descricao`, p.`anexo`, p.`icone`, p.`data_publicacao`, p.`edicao` FROM publicacoes p ORDER BY p.`edicao` ASC LIMIT 1");
            return conexao.Dados(SQL);
        }
        #endregion
        #region páginas públicas
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable publicacaoDestaque()
        {
            string SQL = "SELECT p.`id`, p.`titulo`, p.`descricao`, p.`icone`, p.`anexo`, p.`edicao`, p.`data_publicacao` FROM publicacoes p ORDER BY p.`data_publicacao` ASC, p.`edicao` DESC LIMIT 1";
            return conexao.Dados(SQL);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable publicacaoAtivas()
        {
            string SQL = "SELECT p.`id`, p.`titulo`, p.`descricao`, p.`anexo`, p.`edicao`, p.`icone`, p.`data_publicacao` FROM publicacoes p ORDER BY p.`data_publicacao` ASC, p.`edicao` DESC";
            return conexao.Dados(SQL);
        }
        #endregion
    }
}