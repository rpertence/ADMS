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
    public class Usuario_Recursos
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string id_usuario, string id_recurso)
        {
            string SQL = @"INSERT INTO `usuario_recursos` 
                          (`id_usuario`, `id_recurso`) 
                          VALUES
                          ('" + id_usuario + "','" + id_recurso + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos recursos do usuário
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIdUsuarioTipoUsuario(string id, string tipo)
        {
            string SQL = "";
            if (tipo == "0")
            {
               SQL = string.Format("SELECT " + 
                                    "r.`titulo`, r.`icone`, r.`url` FROM recursos r, usuario_recursos rr " +
                                    "WHERE r.`id` = rr.`id_recurso` " +
                                    "AND rr.`id_usuario` = '" + id + "' ORDER BY r.`titulo` ASC;");
            }
            if (tipo == "1")
            {
                SQL = string.Format("SELECT " +
                                     "r.`titulo`, r.`icone`, r.`url` FROM recursos r, usuario_recursos rr " +
                                     "WHERE r.`id` = rr.`id_recurso` " +
                                     "AND rr.`id_usuario` = '" + id + "' ORDER BY r.`titulo` ASC;");
            }
            if (tipo == "2")
            {
                SQL = string.Format("SELECT r.`titulo`, r.`icone`, r.`url` FROM recursos r ORDER BY r.`titulo` ASC;");
            }
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona recursos do usuário por id do usuario e do recurso
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIdRecursoIdUsuario(string id_recurso, string id_usuario)
        {

            string SQL = string.Format("SELECT rr.`id_recurso` FROM usuario_recursos rr WHERE rr.`id_usuario` = '" + id_usuario + "' AND rr.`id_recurso` = '" + id_recurso + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona recursos do usuário por id do usuário
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIdUsuario(string id_usuario)
        {

            string SQL = string.Format("SELECT rr.`id_recurso` FROM usuario_recursos rr WHERE rr.`id_usuario` = '" + id_usuario + "' ORDER BY rr.`id_recurso` DESC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar usuario_recursos por id usuario
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void AtualizarPorIdUsuario(string id_usuario, string id_recurso)
        {
            string SQL = @"UPDATE usuario_recursos SET id_recurso = '" + id_recurso + "' WHERE id_usuario = '" + id_usuario + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void DeleteByIdUsuarioIdRecurso(string id_usuario, string id_recurso)
        {
            string SQL = string.Format("DELETE FROM usuario_recursos WHERE id_usuario = '" + id_usuario + "' AND id_recurso = '" + id_recurso + "';");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void DeleteByIdUsuario(string id_usuario)
        {
            string SQL = string.Format("DELETE FROM usuario_recursos WHERE id_usuario = '" + id_usuario + "';");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM usuario_recursos";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
    }
}
