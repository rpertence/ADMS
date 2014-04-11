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
    public class Historico
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string id_usuario, string data, string tipo, string descricao, string painel)
        {
            string SQL = @"INSERT INTO `historico` 
                          (`id_usuario`, `data`, `tipo`, `descricao`, `painel`) 
                          VALUES
                          ('" + id_usuario + "','" + data + "','" + tipo + "','" + descricao + "','" + painel + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT h.`id`, h.`id_usuario`, h.`data`, h.`tipo`, h.`descricao`, h.`painel` FROM historico h ORDER BY h.`data` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIDUsuario(int id_usuario)
        {
            string SQL = string.Format("SELECT h.`id`, h.`id_usuario`, h.`descricao`, h.`data`, h.`tipo`, h.`painel`, CASE WHEN h.`tipo` = '0' THEN 'incluir.gif' WHEN h.`tipo` = '1' THEN 'editar.gif' WHEN h.`tipo` = '2' THEN 'excluir.gif' WHEN h.`tipo` = '8' THEN 'logoff.png' WHEN h.`tipo` = '9' THEN 'login.png' ELSE 'hlp.gif' END icone FROM historico h WHERE h.`id_usuario` = '" + id_usuario + "' ORDER BY h.`data` DESC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void DeleteByIdUsuario(string id_usuario)
        {
            string SQL = string.Format("DELETE FROM historico WHERE id_usuario = '" + id_usuario + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
    }
}
