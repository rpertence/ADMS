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
    public class Foto_Album
    {
        #region foto_album
        #region Novo Album
        public static void Inserir(string id_tipo, string resumo, string descricao, string status, string destaque, string titulo, string icone)
        {
            if (destaque.ToString() == "1")
            {
                string SQLU = @"UPDATE foto_album SET destaque = '0';";
                conexao.ExecuteNonQuery(SQLU);


            }
            string SQL = @"INSERT INTO `foto_album` 
                          (`id_tipo`, `resumo`, `descricao`, `status`, `destaque`, `titulo`, `icone`) 
                          VALUES
                          ('" + id_tipo + "','" + resumo + "','" + descricao + "','" + status + "', '" + destaque + "', '" + titulo + "', '" + icone + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos os albuns
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAll()
        {

            string SQL = "SELECT f.`id`, f.`id_tipo`, f.`resumo`, f.`descricao`, f.`status`, f.`titulo`, f.`icone`, CASE f.`destaque` = '1' THEN 'DESTAQUE' ELSE '' END ehDestaque FROM foto_album f ORDER BY f.`id_tipo` ASC;";
                return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona foto_album Por Destaque
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectDestaque(string id_tipo)
        {

            string SQL = "SELECT f.`id`, f.`id_tipo`, f.`resumo`, f.`descricao`, f.`status`, f.`titulo`, f.`icone`, CASE WHEN f.`destaque` = '1' THEN 'DESTAQUE' ELSE '' END destaque, CASE WHEN f.`status` = '1' THEN 'ativo' else 'inativo' END ATIVO FROM foto_album f WHERE f.`destaque` = '1';";
            return conexao.Dados(SQL);
        }
        #endregion

        #region seleciona foto_album Por id tipo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAllByTipo(string id_tipo)
        {

            string SQL = "SELECT f.`id`, f.`id_tipo`, f.`resumo`, f.`descricao`, f.`status`, f.`titulo`, f.`icone`, CASE WHEN f.`destaque` = '1' THEN 'DESTAQUE' ELSE '' END destaque, CASE WHEN f.`status` = '1' THEN 'ativo' else 'inativo' END ATIVO FROM foto_album f WHERE f.`id_tipo` = '" + id_tipo + "' ORDER BY f.`id_tipo` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona foto_album ativos Por id tipo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAllActiveByTipo(string id_tipo)
        {

            string SQL = "SELECT f.`id`, f.`id_tipo`, f.`resumo`, f.`descricao`, f.`status`, f.`titulo`, f.`icone` FROM foto_album f WHERE f.`id_tipo` = '" + id_tipo + "' AND f.`status` = '1' ORDER BY f.`titulo` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona foto_album ativos 
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAllActive()
        {

            string SQL = "SELECT f.`id`, f.`id_tipo`, f.`resumo`, f.`descricao`, f.`status`, f.`titulo`, f.`icone` FROM foto_album f WHERE f.`status` = '1' ORDER BY f.`titulo` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion

        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectOne(int id)
        {
            string SQL = string.Format("SELECT f.`id`, f.`id_tipo`, f.`resumo`, f.`descricao`, f.`status`, f.`destaque`, f.`titulo`, f.`icone` FROM foto_album f WHERE f.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void Update(string id, string id_tipo, string resumo, string descricao, string status, string destaque, string titulo, string icone)
        {
            if (destaque.ToString() == "1")
            {
                string SQLU = @"UPDATE foto_album SET destaque = '0';";
                conexao.ExecuteNonQuery(SQLU);
            }
            string SQL = @"UPDATE foto_album SET resumo = '" + resumo + "', id_tipo = '" + id_tipo + "', descricao = '" + descricao + "', status = '" + status + "', destaque = '" + destaque + "', titulo = '" + titulo + "', icone = '" + icone + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(string id)
        {
            string SQL = string.Format("DELETE FROM foto_album WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item por id do tipo
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void ExcluirByIdTipo(string id_tipo)
        {
            string SQL = string.Format("DELETE FROM foto_album WHERE id_tipo = '" + id_tipo + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion

        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM foto_album";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
        #endregion
    }
}