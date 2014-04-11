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
    public class Produtos
    {
        #region comandos Básicos
        #region Novo
        public static void Novo(string id_categoria, string id_subcategoria, string estoque, string status, string destaque, string resumo, string ProdDescricao_, string ProdValor_, string tipo, string email_cobranca, string moeda, string peso, string extras, string icone, int idMarca)
        {
            string SQL = @"INSERT INTO produtos
(id_categoria, id_subcategoria, estoque, status, destaque, resumo, ProdDescricao_, ProdValor_, tipo, email_cobranca, moeda, peso, extras, icone, id_marca)
VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', {14});";

            SQL = string.Format(SQL, id_categoria, id_subcategoria, estoque, status, destaque, resumo, ProdDescricao_, ProdValor_, tipo, email_cobranca, moeda,
                peso, extras, icone, idMarca);

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos os produtos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAll()
        {
            string SQL = "SELECT " +
                            "(SELECT s.`titulo` FROM produtos_subcategoria s WHERE s.`id` = p.`id_subcategoria`) subcategoria, " +
                            "(SELECT c.`titulo` FROM produtos_categoria c WHERE c.`id` = p.`id_categoria`) categoria, " +
                            " p.`id`, p.`id_subcategoria`, p.`estoque`, p.`status`, p.`destaque`, p.`id_categoria`,  (p.`ProdDescricao_`) titulo, p.`ProdDescricao_`, p.`ProdValor_`, p.`tipo`, p.`email_cobranca`, p.`moeda`, p.`peso`, p.`icone` FROM produtos p ORDER BY p.`id`;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectById(int id)
        {
            string SQL = "SELECT " +
                "(SELECT pc.`titulo` FROM produtos_categoria pc WHERE pc.`id` = p.`id_categoria`) categoria, " +
                "(SELECT ps.`titulo` FROM produtos_subcategoria ps WHERE ps.`id` = p.`id_subcategoria`) subcategoria, " +
                "p.`id`, p.`id_categoria`, p.`id_subcategoria`, p.`estoque`, p.`status`, p.`destaque`, p.`resumo`, p.`ProdDescricao_`, p.`ProdValor_`, p.`tipo`, p.`email_cobranca`, p.`moeda`, p.`peso`, p.`extras`, p.`icone` FROM produtos p WHERE p.`id` = '" + id + "';";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID da categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIdCategoria(int id_categoria)
        {
            string SQL = "SELECT p.`id`, p.`id_subcategoria`, p.`estoque`, p.`status`, p.`destaque`, p.`ProdDescricao_`, p.`ProdValor_`, p.`tipo`, p.`email_cobranca`, p.`moeda`, p.`peso`, p.`icone` FROM produtos p WHERE p.`id_categoria` = '" + id_categoria + "' ORDER BY cast(ProdValor_ as decimal(10, 2));";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID da sub-categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIdSubCategoria(int id_subcategoria)
        {
            string SQL = "SELECT p.`id`, p.`id_subcategoria`, p.`estoque`, p.`status`, p.`destaque`, p.`ProdDescricao_`, p.`ProdValor_`, p.`resumo`, p.`tipo`, p.`email_cobranca`, p.`moeda`, p.`peso`, p.`icone` FROM produtos p WHERE p.`id_subcategoria` = '" + id_subcategoria + "' ORDER BY cast(ProdValor_ as decimal(10, 2));";
            return conexao.Dados(SQL);
        }
        #endregion
        public static DataTable SelectByIdMarca(int idMarca, int idSubcategoria)
        {
            string sql = string.Format(@"select *
from produtos
where id_subcategoria = {0}
    and id_marca = {1}
order by cast(ProdValor_ as decimal(10, 2))", idSubcategoria, idMarca);

            return conexao.Dados(sql);
        }
        public static DataTable SelectByPesquisa(string valor)
        {
            string sql = string.Format(@"select distinct p.id, p.proddescricao_, p.prodvalor_, p.icone
from produtos p
  inner join produtos_categoria c on p.id_categoria = c.id
    and p.status = 1
  inner join produtos_subcategoria sc on p.id_subcategoria = sc.id
  inner join marca m on p.id_marca = m.id
where resumo like '%{0}%'
  or proddescricao_ like '%{0}%'
  or c.titulo like '%{0}%'
  or sc.titulo like '%{0}%'
  or m.descricao like '%{0}%'", valor);

            return conexao.Dados(sql);
        }
        #region Atualizar
        public static void Update(string id, string id_categoria, string id_subcategoria, string estoque, string status, string destaque, string resumo, string ProdDescricao_, string ProdValor_, string tipo, string email_cobranca, string moeda, string peso, string extras, string icone, int idMarca)
        {
            string SQL = @"UPDATE produtos SET id_categoria = '" + id_categoria + "', estoque = '" + estoque + "', id_subcategoria = '" + id_subcategoria + "', status = '" + status + "', destaque = '" + destaque + "', ProdDescricao_ = '" + ProdDescricao_ + "', ProdValor_ = '" + ProdValor_ + "', tipo = '" + tipo + "', email_cobranca = '" + email_cobranca + "', moeda = '" + moeda + "', peso = '" + peso + "', extras = '" + extras + "', icone = '" + icone + "', id_marca = " + idMarca + " WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region AtualizarEstoque
        public static void UpdateEstoque(string id, string estoque)
        {
            string SQL = @"UPDATE produtos SET estoque = '" + estoque + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion

        #region Exluir item
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(string id)
        {
            string SQL = string.Format("DELETE FROM produtos WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item por id da categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void ExcluirByIdCategoria(string id_categoria)
        {
            string SQL = string.Format("DELETE FROM produtos WHERE id_categoria = '" + id_categoria + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item por id da sub-categoria
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void ExcluirByIdSubCategoria(string id_subcategoria)
        {
            string SQL = string.Format("DELETE FROM produtos WHERE id_subcategoria = '" + id_subcategoria + "'");
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM produtos";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
        #endregion
        #region páginas públicas
        #region produtos em destaque
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByDestaque()
        {
            string SQL = "SELECT " +
                "(SELECT pc.`titulo` FROM produtos_categoria pc WHERE pc.`id` = p.`id_categoria`) categoria, " +
                "(SELECT ps.`titulo` FROM produtos_subcategoria ps WHERE ps.`id` = p.`id_subcategoria`) subcategoria, " +
                "p.`id`, p.`id_categoria`, p.`id_subcategoria`, p.`estoque`, p.`status`, p.`destaque`, p.`ProdDescricao_`, (p.`ProdValor_`) valor, p.`icone`, p.`resumo` FROM produtos p WHERE p.`destaque` = '1' AND P.`status` = '1' AND p.`estoque` > '0' ORDER BY p.`id` ASC;";
            return conexao.Dados(SQL);
        }
        public static DataTable SelectByDestaque(int categoria, int qtde)
        {
            string SQL = @"SELECT cast(p.ProdValor_ as decimal(10,2)) ValorProduto, p.icone NomeArquivo, p.proddescricao_ Nome
FROM produtos p
WHERE p.id_categoria = " + categoria + @"
  AND p.destaque = '1'
  AND p.status = '1'
  AND p.estoque > '0'
LIMIT 0, " + qtde;

            return conexao.Dados(SQL);
        }
        #endregion
        #region produtos por subcategoria
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectBySubCategoria(string id)
        {
            string SQL = "SELECT " +
                "p.`id`, p.`id_categoria`, p.`id_subcategoria`, p.`estoque`, p.`status`, p.`destaque`, p.`ProdDescricao_`, (p.`ProdValor_`) valor, p.`icone`, p.`resumo` FROM produtos p, produtos_subcategoria ps WHERE p.`status` = '1' AND ps.`id` = '" + id + "' AND p.`id_subcategoria` = '" + id + "' ORDER BY p.`id` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #endregion
    }
}