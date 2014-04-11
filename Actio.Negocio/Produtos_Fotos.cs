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
    public class Produtos_Fotos
    {
        #region Novo
        public static void Inserir(int id_produto, string titulo, string arquivo, string ordem)
        {
            string SQL = @"INSERT INTO `produtos_fotos`
                          (`id_produto`, `titulo`, `arquivo`, `ordem`) 
                          VALUES
                          ('" + id_produto + "','" + titulo + "','" + arquivo + "','" + ordem + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todas as fotos do produto
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable FotosDoProduto(int id_produto)
        {
            string SQL = string.Format("SELECT p.`id`, p.`id_produto`, p.`titulo`, p.`arquivo`, p.`ordem` FROM produtos_fotos p WHERE p.`id_produto` = {0} ORDER BY p.`ordem` ASC", id_produto.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por id
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable UmaFoto(string id)
        {
            string SQL = string.Format("SELECT p.`id`, p.`id_produto`, p.`titulo`, p.`arquivo`, p.`ordem` FROM produtos_fotos p WHERE p.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void Atualizar(string id, string titulo, string ordem, string arquivo)
        {
            string SQL = @"UPDATE produtos_fotos SET titulo = '" + titulo + "', ordem = '" + ordem + "', arquivo = '" + arquivo + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(string id)
        {
            string SQL = string.Format("DELETE FROM produtos_fotos WHERE id = {0} LIMIT 1", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM produtos_fotos";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}