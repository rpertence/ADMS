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
    public class Foto
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string id_Album, string titulo, string arquivo, string miniatura, string ordem, string destaque)
        {
                string SQL = @"INSERT INTO `fotos` 
                          (`id_Album`, `titulo`, `arquivo`, `miniatura`, `ordem`, `destaque`) 
                          VALUES
                          ('" + id_Album + "','" + titulo + "','" + arquivo + "','" + miniatura + "','" + ordem + "','" + destaque + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT fc.`id`, fc.`id_Album`, fc.`titulo`, fc.`arquivo`, fc.`miniatura`, fc.`ordem`, fc.`destaque` FROM fotos fc ORDER BY fc.`ordem` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT fc.`id`, fc.`id_Album`, fc.`titulo`, fc.`arquivo`, fc.`miniatura`, fc.`ordem`, fc.`destaque` FROM fotos fc WHERE fc.`id` = " + id);
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ordem
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByOrdem(int ordem)
        {
            string SQL = string.Format("SELECT fc.`id`, fc.`id_Album`, fc.`titulo`, fc.`arquivo`, fc.`miniatura`, fc.`ordem`, fc.`destaque` FROM fotos fc WHERE fc.`ordem` = " + ordem);
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona todas por ID do album
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIDAlbum(int id_Album)
        {
            string SQL = string.Format("SELECT fc.`id`, fc.`id_Album`, fc.`titulo`, fc.`arquivo`, fc.`miniatura`, fc.`ordem`, fc.`destaque` FROM fotos fc WHERE fc.`id_Album` = '" + id_Album + "' ORDER BY fc.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por Arquivo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByArquivo(string arquivo)
        {
            string SQL = string.Format("SELECT SELECT fc.`id`, fc.`id_Album`, fc.`titulo`, fc.`arquivo`, fc.`miniatura`, fc.`ordem`, fc.`destaque` FROM fotos fc WHERE fc.`arquivo` = '" + arquivo + "';");
            return conexao.Dados(SQL);
        }
        #endregion

        #region Seleciona todas por ID do album e Arquivo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIDAlbumArquivo(int id_Album, string arquivo)
        {
            string SQL = string.Format("SELECT fc.`id`, fc.`id_Album`, fc.`titulo`, fc.`arquivo`, fc.`miniatura`, fc.`ordem`, fc.`destaque` FROM fotos fc WHERE fc.`id_Album` = '" + id_Album + "' AND fc.`arquivo` = '" + arquivo + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar fotos
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string id_Album, string titulo, string arquivo, string miniatura, string ordem, string destaque)
        {
            string SQL = @"UPDATE fotos SET id_Album = '" + id_Album + "', titulo = '" + titulo + "', arquivo = '" + arquivo + "', miniatura = '" + miniatura + "', ordem = '" + ordem + "', destaque = '" + destaque + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Atualizar Titulo fotos
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void AtualizarTitulo(string id, string titulo)
        {
            string SQL = @"UPDATE fotos SET titulo = '" + titulo + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        #region excluir por id
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM fotos WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region excluir por id do album
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void DeleteByIdAlbum(int id_Album)
        {
            string SQL = string.Format("DELETE FROM fotos WHERE id_Album = '" + id_Album + "';");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #endregion
        #region Exluir por nome do arquivo
        #region excluir por id
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void DeleteByArquivo(string arquivo)
        {
            string SQL = string.Format("DELETE FROM fotos WHERE arquivo = '" + arquivo + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM fotos";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}