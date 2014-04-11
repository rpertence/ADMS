using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Text;
using System.Reflection;

namespace Actio.Negocio
{
    [DataObject(true)]
    public class Busca
    {
        #region páginas públicas
        #region busca notícia
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Noticias(string parametro)
        {
            String p = parametro;
            String p1 = p.Replace("\\", "/");
            String busca = p1.Replace("'", "\\'");

            string SQL = "SELECT " +
                         "n.`id`, " +
                         "n.`titulo`, " +
                         "n.`resumo`, " +
                         "n.`data`, " +
                         "n.`ordem` " +
                         " FROM noticias n WHERE n.`status` = '1'" +
                         " AND n.`resumo` LIKE '%" + busca + "%'" +
                         " OR n.`descricao`  LIKE '%" + busca + "%'" +
                         " OR n.`titulo`  LIKE '%" + busca + "%';";
            return conexao.Dados(SQL);
        }
        #endregion
        #region busca artigos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Artigos(string parametro)
        {
            String p = parametro;
            String p1 = p.Replace("\\", "/");
            String busca = p1.Replace("'", "\\'");

            string SQL = "SELECT" +
                         "(SELECT aa.`nome` FROM artigo_autor aa WHERE aa.`id` = a.`id_autor`) artigo_autor, " +
                         "(a.`id`) codigo, " +
                         "(a.`titulo`) titulo, " +
                         "(a.`resumo`) resumo, " +
                         "a.`data`, " +
                         "a.`id_autor` " +
                         " FROM artigos a WHERE a.`status` = '1'" +
                         " AND a.`resumo` LIKE '%" + busca + "%'" +
                         " OR a.`descricao`  LIKE '%" + busca + "%'" +
                         " OR a.`titulo`  LIKE '%" + busca + "%';";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Agenda
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Agenda(string parametro)
        {
            String p = parametro;
            String p1 = p.Replace("\\", "/");
            String busca = p1.Replace("'", "\\'");

            string SQL = "SELECT " +
                         "a.`id`, " +
                         "a.`titulo`, " +
                         "a.`resumo`, " +
                         "a.`dia`, " +
                         "a.`mes`, " +
                         "a.`ano` " +
                         " FROM agenda a " +
                         " WHERE a.`status` = '1' AND " +
                         " a.`descricao` LIKE '%" + busca + "%'" +
                         " OR a.`titulo`  LIKE '%" + busca + "%'" +
                         " OR a.`resumo`  LIKE '%" + busca + "%'";
            return conexao.Dados(SQL);
        }
        #endregion
        #region busca album
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Album(string parametro)
        {
            String p = parametro;
            String p1 = p.Replace("\\", "/");
            String busca = p1.Replace("'", "\\'");

            string SQL = "SELECT" +
                         "(a.`id`) codigo, " + 
                         "(a.`titulo`) titulo, " + 
                         "(a.`descricao`) resumo, " +
                         "a.`icone` " +
                         " FROM foto_album a WHERE a.`status` = '1'" +
                         " AND a.`resumo` LIKE '%" + busca + "%'" +
                         " OR a.`descricao`  LIKE '%" + busca + "%'" +
                         " OR a.`titulo`  LIKE '%" + busca + "%';";
            return conexao.Dados(SQL);
        }
        #endregion
        #region busca video
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Video(string parametro)
        {
            String p = parametro;
            String p1 = p.Replace("\\", "/");
            String busca = p1.Replace("'", "\\'");

            string SQL = "SELECT " +
                         "a.`id`, " +
                         "a.`titulo`, " +
                         "a.`descricao`, " +
                         "a.`icone` " +
                         " FROM videos a WHERE a.`status` = '1'" +
                         " AND a.`descricao` LIKE '%" + busca + "%'" +
                         " OR a.`titulo`  LIKE '%" + busca + "%';";
            return conexao.Dados(SQL);
        }
        #endregion
        #region capela virtual
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Capela(string parametro)
        {
            String p = parametro;
            String p1 = p.Replace("\\", "/");
            String busca = p1.Replace("'", "\\'");

            string SQL = "SELECT" +
                         " a.`descricao`, " +
                         " a.`nome` " +
                         " FROM oracao a WHERE a.`status` = '1'" +
                         " AND a.`descricao` LIKE '%" + busca + "%'" +
                         " OR a.`nome`  LIKE '%" + busca + "%';";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Testemunhos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Testemunhos(string parametro)
        {
            String p = parametro;
            String p1 = p.Replace("\\", "/");
            String busca = p1.Replace("'", "\\'");

            string SQL = "SELECT " +
                         "a.`depoimento_id`, " +
                         " (a.`depoimento_nome`) nome, " +
                         " a.`resumo`, " +
                         " a.`depoimento_insdata` " +
                         " FROM depoimento a WHERE a.`depoimento_status` = '1'" +
                         " AND a.`depoimento_descricao` LIKE '%" + busca + "%'" +
                         " OR a.`depoimento_nome`  LIKE '%" + busca + "%'" +
                         " OR a.`resumo`  LIKE '%" + busca + "%';";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Santo do dia
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Santos(string parametro)
        {
            String p = parametro;
            String p1 = p.Replace("\\", "/");
            String busca = p1.Replace("'", "\\'");

            string SQL = "SELECT " +
                         "a.`id`, " +
                         "a.`nome`, " +
                         "a.`icone`, " +
                         "a.`dia`, " +
                         "a.`mes` " +
                         " FROM santo a " +
                         " WHERE a.`descricao` LIKE '%" + busca + "%'" +
                         " OR a.`nome`  LIKE '%" + busca + "%'" +
                         " OR a.`mes`  LIKE '%" + busca + "%'" +
                         " OR a.`dia`  LIKE '%" + busca + "%'";
            return conexao.Dados(SQL);
        }
        #endregion
        #endregion
    }
}
