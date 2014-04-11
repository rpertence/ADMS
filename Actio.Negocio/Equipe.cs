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
    public class Equipe
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir
            (
            string titulo, 
            string resumo, 
            string descricao, 
            string icone, 
            string ativo, 
            string destaque, 
            string ordem, 
            string email
            )
        {

            string SQL = @"INSERT INTO `equipe` 
                            (`titulo`, `resumo`, `descricao`, `icone`, `ativo`, `destaque`, `ordem`, `email`) 
                            VALUES
                            ('" + titulo + "','" + resumo + "','" + descricao + "','" + icone + "','" + ativo + "','" + destaque + "','" + ordem + "','" + email + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email`, CASE WHEN e.`ativo` = '1' THEN 'ativo' ELSE 'inativo' END status, CASE WHEN e.`destaque` = '0' THEN 'Coord.Ministério' WHEN e.`destaque` = '1' THEN 'Coord.Forania' ELSE 'Eq.Arquidiocesana' END destaques FROM equipe e ORDER BY e.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos ativos sem o destaque
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllActive()
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email` FROM equipe e WHERE e.`ativo` = '1' AND e.`destaque` = '0' ORDER BY e.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos ativos mais o destaque
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllActiveMaisOdestaque()
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email` FROM equipe e WHERE e.`ativo` = '1' ORDER BY e.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona Coordenador de Ministério
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectCoordenadorMinisterio()
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email` FROM equipe e WHERE e.`ativo` = '1' AND e.`destaque` = '0';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona Coordenador de Fornania
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectCoordenadorForania()
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email` FROM equipe e WHERE e.`ativo` = '1' AND e.`destaque` = '1';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona Coordenador de Ministério
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectEquipeArquidiocesana()
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email` FROM equipe e WHERE e.`ativo` = '1' AND e.`destaque` = '2';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos ativos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectActives()
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email` FROM equipe e WHERE e.`ativo` = '1' ORDER BY e.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por id
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByid(int id)
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email` FROM equipe e WHERE e.`id` = '" + id + "'");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por id ativo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByidActive(int id)
        {
            string SQL = string.Format("SELECT e.`id`, e.`titulo`, e.`resumo`, e.`descricao`, e.`icone`, e.`ativo`, e.`destaque`, e.`ordem`, e.`email` FROM equipe e WHERE e.`id` = '" + id + "' AND e.`ativo` = '1';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string titulo, string resumo, string descricao, string icone, string ativo, string destaque, string ordem, string email)
        {
            string SQL = @"UPDATE equipe SET titulo = '" + titulo + "', resumo = '" + resumo + "', descricao = '" + descricao + "', icone = '" + icone + "', ativo = '" + ativo + "', destaque = '" + destaque + "', ordem = '" + ordem + "', email = '" + email + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM equipe WHERE id = '" + id + "' LIMIT 1");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextid
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextid FROM equipe";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}