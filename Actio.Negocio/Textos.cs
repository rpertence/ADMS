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
    public class Textos
    {
        #region textos
        #region Novo Texto
        public static void Inserir(string id_tipo, string resumo, string descricao, string status, string destaque, string titulo, string icone, int id_coordenador)
        {
            if (destaque.ToString() == "1")
            {
                string SQLU = @"UPDATE textos SET destaque = '0' WHERE id_tipo = '" + id_tipo + "'";
                conexao.ExecuteNonQuery(SQLU);

                string SQL = @"INSERT INTO `textos` 
                          (`id_tipo`, `resumo`, `descricao`, `status`, `destaque`, `titulo`, `icone`, `id_coordenador`) 
                          VALUES
                          ('" + id_tipo + "','" + resumo + "','" + descricao + "','" + status + "', '" + destaque + "', '" + titulo + "', '" + icone + "', '" + id_coordenador + "');";

                conexao.ExecuteNonQuery(SQL);
            }
            else
            {
                string SQL = @"INSERT INTO `textos` 
                          (`id_tipo`, `resumo`, `descricao`,`status`, `destaque`, `titulo`, `icone`, `id_coordenador`) 
                          VALUES
                          ('" + id_tipo + "','" + resumo + "','" + descricao + "','" + status + "', '" + destaque + "', '" + titulo + "', '" + icone + "', '" + id_coordenador + "');";

                conexao.ExecuteNonQuery(SQL);
            }
        }
        #endregion
        #region seleciona todos os textos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAll()
        {

            string SQL = "SELECT t.`id`, t.`id_tipo`, t.`resumo`, t.`descricao`, t.`status`, t.`titulo`, t.`icone`, t.`id_coordenador`, CASE t.`destaque` = '1' THEN 'DESTAQUE' ELSE '' END destaque FROM textos t ORDER BY t.`id_tipo` ASC;";
                return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona textos Por id tipo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAllByTipo(string id_tipo)
        {

            string SQL = "SELECT t.`id`, t.`id_tipo`, t.`resumo`, t.`descricao`, t.`status`, t.`titulo`, t.`icone`, t.`id_coordenador`, CASE WHEN t.`destaque` = '1' THEN 'DESTAQUE' ELSE '' END destaque, CASE WHEN t.`status` = '1' THEN 'ativo' else 'inativo' END ATIVO FROM textos t WHERE t.`id_tipo` = '" + id_tipo + "' ORDER BY t.`id_tipo` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectOne(int id)
        {
            string SQL = string.Format("SELECT t.`id`, t.`id_tipo`, t.`resumo`, t.`descricao`, t.`status`, t.`destaque`, t.`titulo`, t.`icone`, t.`id_coordenador` FROM textos t WHERE t.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void Update(string id, string id_tipo, string resumo, string descricao, string status, string destaque, string titulo, string icone, int id_coordenardor)
        {
            if (destaque.ToString() == "1")
            {
                string SQLU = @"UPDATE textos SET destaque = '0' WHERE id_tipo = '" + id_tipo + "'";
                conexao.ExecuteNonQuery(SQLU);

                string SQL = @"UPDATE textos SET resumo = '" + resumo + "', id_tipo = '" + id_tipo + "', descricao = '" + descricao + "', status = '" + status + "', destaque = '" + destaque + "', titulo = '" + titulo + "', icone = '" + icone + "', id_coordenador = '" + id_coordenardor + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
            }
            else
            {
                string SQL = @"UPDATE textos SET resumo = '" + resumo + "', id_tipo = '" + id_tipo + "', descricao = '" + descricao + "', status = '" + status + "', destaque = '" + destaque + "', titulo = '" + titulo + "', icone = '" + icone + "', id_coordenador = '" + id_coordenardor + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
            }
        }
        #endregion
        #region Exluir item
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(string id)
        {
            string SQL = string.Format("DELETE FROM textos WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item por id do tipo
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void ExcluirByIdTipo(string id_tipo)
        {
            string SQL = string.Format("DELETE FROM textos WHERE id_tipo = '" + id_tipo + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item por id Equipe
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void ExcluirByIdEquipe(string id)
        {
            string SQL = string.Format("DELETE FROM textos WHERE id_coordenador = '" + id + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM textos";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
        #endregion
        #region páginas públicas
        #region seleciona textos em destaque por categoria de texto
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectDestaqueByTipo(string id_tipo)
        {

            string SQL = "SELECT t.`id`, t.`id_tipo`, t.`resumo`, t.`status`, t.`titulo`, t.`icone`, t.`descricao`, t.`id_coordenador` FROM textos t WHERE t.`id_tipo` = '" + id_tipo + "' AND t.`destaque` = '1'  AND t.`status` = '1' ORDER BY t.`titulo` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona textos por id do tipo
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAllActiveByTipo(string id_tipo)
        {

            string SQL = " SELECT " +
                " (SELECT tt.`titulo` FROM textos_categoria tt WHERE tt.`id` = '" + id_tipo + "') tipo, " +
                " t.`id`, t.`id_tipo`, t.`resumo`, t.`descricao`, t.`status`, t.`titulo`, t.`icone`, t.`id_coordenador` FROM textos t WHERE t.`id_tipo` = '" + id_tipo + "' AND t.`status` = '1' ORDER BY t.`titulo` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #endregion
    }
}