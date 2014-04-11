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
    public class Noticias_Comentarios
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir
            (
            string id_noticia, 
            string titulo, 
            string descricao, 
            string autor, 
            string email, 
            string data, 
            string status
            )
        {
            string SQL = @"INSERT INTO `noticias_comentarios` 
                          (`id_noticia`, `titulo`, `descricao`, `autor`, `email`, `data`, `status`) 
                          VALUES
                          ('" + id_noticia + "','" + titulo + "','" + descricao + "','" + autor + "','" + email + "','" + data + "','" + status + "');";

            conexao.ExecuteNonQuery(SQL);

        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT n.`id`, n.`id_noticia`, n.`titulo`, n.`descricao`, n.`autor`, n.`email`, n.`data`, n.`status`, CASE WHEN n.`status` = '1' THEN 'ativo' ELSE 'inativo' END status FROM noticias_comentarios n ORDER BY n.`data` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos emails mais o data
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllActiveByIdNoticia(string id_noticia)
        {
            string SQL = string.Format("SELECT n.`id`, n.`id_noticia`, n.`titulo`, n.`descricao`, n.`autor`, n.`email`, n.`data`, n.`status`, CASE WHEN n.`status` = '1' THEN 'ativo' ELSE 'inativo' END status FROM noticias_comentarios n WHERE n.`status` = '1' AND n.`id_noticia` = '" + id_noticia + "' ORDER BY n.`data` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string id_noticia, string titulo, string descricao, string autor, string email, string data, string ordem, string status)
        {
            string SQL = @"UPDATE noticias_comentarios SET titulo = '" + titulo + "', descricao = '" + descricao + "', autor = '" + autor + "', email = '" + email + "', status = '" + status + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM noticias_comentarios WHERE id = '" + id + "' LIMIT 1");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextid
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextid FROM noticias_comentarios";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}