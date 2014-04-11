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
    public class Agenda
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string titulo, string dia, string mes, string ano, string resumo, string descricao, string icone, string status, string destaque)
        {
            string SQL = @"INSERT INTO `agenda` 
                          (`titulo`, `dia`, `mes`, `ano`, `resumo`, `descricao`, `icone`, `status`, `destaque`) 
                          VALUES
                          ('" + titulo + "','" + dia + "','" + mes + "', '" + ano + "', '" + resumo + "', '" + descricao + "', '" + icone + "', '" + status + "', '" + destaque + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT a.`id`, a.`titulo`, a.`dia`, a.`mes`, a.`ano`, a.`resumo`, `descricao`, `icone`, `status`, `destaque`, " +
                " CASE WHEN a.`status` = '0' THEN 'inativo' ELSE 'ativo' END situacao, " +
                " CASE WHEN a.`destaque` = '0' THEN '' ELSE 'destaque' END dest " +
                " FROM agenda a ORDER BY a.`ano` ASC, a.`mes` ASC, a.`dia` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos ativos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllActive()
        {
            string SQL = string.Format("SELECT a.`id`, a.`titulo`, a.`dia`, a.`mes`, a.`ano`, a.`resumo`, `descricao`, `icone`, `status`, `destaque`, " +
                " CASE " +
                "WHEN a.`mes` = '01' THEN 'JANEIRO' " +
                "WHEN a.`mes` = '02'  THEN 'FEVEREIRO' " +
                "WHEN a.`mes` = '03' THEN 'MARÇO' " +
                "WHEN a.`mes` = '04' THEN 'ABRIL' " +
                "WHEN a.`mes` = '05' THEN 'MAIO' " +
                "WHEN a.`mes` = '06' THEN 'JUNHO' " +
                "WHEN a.`mes` = '07' THEN 'JULHO' " +
                "WHEN a.`mes` = '08' THEN 'AGOSTO' " +
                "WHEN a.`mes` = '09' THEN 'SETEMBRO' " +
                "WHEN a.`mes` = '10' THEN 'OUTUBRO' " +
                "WHEN a.`mes` = '11' THEN 'NOVEMBRO' " +
                "WHEN a.`mes` = '12' THEN 'DEZEMBRO' " +
                "END mesLiteral " +
                " FROM agenda a WHERE a.`status` = '1' GROUP BY a.`mes` ORDER BY a.`ano` ASC, a.`mes` asc");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos os destaques
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllDestaque()
        {
            string SQL = string.Format("SELECT a.`id`, a.`titulo`, a.`dia`, a.`mes`, a.`ano`, a.`resumo`, `descricao`, `icone`, `status`, `destaque`, " +
                " CASE " +
                "WHEN a.`mes` = '01' THEN 'JANEIRO' " +
                "WHEN a.`mes` = '02'  THEN 'FEVEREIRO' " +
                "WHEN a.`mes` = '03' THEN 'MARÇO' " +
                "WHEN a.`mes` = '04' THEN 'ABRIL' " +
                "WHEN a.`mes` = '05' THEN 'MAIO' " +
                "WHEN a.`mes` = '06' THEN 'JUNHO' " +
                "WHEN a.`mes` = '07' THEN 'JULHO' " +
                "WHEN a.`mes` = '08' THEN 'AGOSTO' " +
                "WHEN a.`mes` = '09' THEN 'SETEMBRO' " +
                "WHEN a.`mes` = '10' THEN 'OUTUBRO' " +
                "WHEN a.`mes` = '11' THEN 'NOVEMBRO' " +
                "WHEN a.`mes` = '12' THEN 'DEZEMBRO' " +
                "END mesLiteral " +
                " FROM agenda a WHERE a.`status` = '1' AND a.`destaque` = '1' ORDER BY a.`ano` ASC, a.`mes` asc");
            return conexao.Dados(SQL);
        }
        #endregion


        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT  " +
                                " CASE " +
                "WHEN a.`mes` = '01' THEN 'JANEIRO' " +
                "WHEN a.`mes` = '02'  THEN 'FEVEREIRO' " +
                "WHEN a.`mes` = '03' THEN 'MARÇO' " +
                "WHEN a.`mes` = '04' THEN 'ABRIL' " +
                "WHEN a.`mes` = '05' THEN 'MAIO' " +
                "WHEN a.`mes` = '06' THEN 'JUNHO' " +
                "WHEN a.`mes` = '07' THEN 'JULHO' " +
                "WHEN a.`mes` = '08' THEN 'AGOSTO' " +
                "WHEN a.`mes` = '09' THEN 'SETEMBRO' " +
                "WHEN a.`mes` = '10' THEN 'OUTUBRO' " +
                "WHEN a.`mes` = '11' THEN 'NOVEMBRO' " +
                "WHEN a.`mes` = '12' THEN 'DEZEMBRO' " +
                "END mesLiteral, " +
                "a.`id`, a.`titulo`, a.`dia`, a.`mes`, a.`ano`, a.`resumo`, a.`descricao`, a.`icone`, a.`status`, a.`destaque` FROM agenda a WHERE a.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por mes e ano
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectBymesano(string mes, string ano)
        {
            string SQL = string.Format("SELECT a.`id`, a.`titulo`, a.`dia`, a.`mes`, a.`ano`, a.`resumo`, a.`descricao`, a.`icone`, a.`status`, a.`destaque` FROM agenda a WHERE a.`mes` = '" + mes + "' AND a.`ano` = '" + ano + "' ORDER BY a.`titulo` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por mes
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByMes(string mes)
        {
            string SQL = string.Format("SELECT a.`id`, a.`titulo`, a.`dia`, a.`mes`, a.`ano`, a.`resumo`, a.`descricao`, a.`icone`, a.`status`, a.`destaque`," +
                " CASE " +
                "WHEN a.`mes` = '01' THEN 'JANEIRO' " +
                "WHEN a.`mes` = '02'  THEN 'FEVEREIRO' " +
                "WHEN a.`mes` = '03' THEN 'MARÇO' " +
                "WHEN a.`mes` = '04' THEN 'ABRIL' " +
                "WHEN a.`mes` = '05' THEN 'MAIO' " +
                "WHEN a.`mes` = '06' THEN 'JUNHO' " +
                "WHEN a.`mes` = '07' THEN 'JULHO' " +
                "WHEN a.`mes` = '08' THEN 'AGOSTO' " +
                "WHEN a.`mes` = '09' THEN 'SETEMBRO' " +
                "WHEN a.`mes` = '10' THEN 'OUTUBRO' " +
                "WHEN a.`mes` = '11' THEN 'NOVEMBRO' " +
                "WHEN a.`mes` = '12' THEN 'DEZEMBRO' " +
                "END mesLiteral " +
                "FROM agenda a WHERE a.`mes` = '" + mes + "' ORDER BY a.`ano` ASC, a.`mes` asc;");
            return conexao.Dados(SQL);
        }
        #endregion

        #region Seleciona por ano
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByano(string ano)
        {
            string SQL = string.Format("SELECT a.`id`, a.`titulo`, a.`dia`, a.`mes`, a.`ano`, a.`resumo`, a.`descricao`, a.`icone`, a.`status`, a.`destaque`  FROM agenda a WHERE a.`ano` = '" + ano + "' GROUP BY a.`mes` ORDER BY a.`mes` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar agenda
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string titulo, string dia, string mes, string ano, string resumo, string descricao, string icone, string status, string destaque)
        {
            string SQL = @"UPDATE agenda SET titulo = '" + titulo + "', dia = '" + dia + "', mes = '" + mes + "', ano = '" + ano + "', resumo = '" + resumo + "', descricao = '" + descricao + "', icone = '" + icone + "', status = '" + status + "', destaque = '" + destaque + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM agenda WHERE id = '" + id + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM agenda";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
        #region Busca por parametro
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SearchByParametrosColunaParametro(string coluna, string parametro)
        {
            string SQL = string.Format("SELECT a.`id`, a.`titulo`, a.`dia`, a.`mes`, a.`ano`, a.`resumo` FROM agenda a WHERE a.'" + coluna + "' LIKE '%" + parametro + "%'");
            return conexao.Dados(SQL);
        }
        #endregion
    }
}