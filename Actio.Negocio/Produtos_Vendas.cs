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
    public class Produtos_Vendas
    {
        #region produtos_vendas
        #region Novo Produto Venda
        public static void Inserir(string pedido, string transacao, string Tipo_Frete, string status_descricao, string forma_pagamento, string frete, string anotacao, string email)
        {
            string SQL = @"INSERT INTO `produtos_vendas` 
                          (`pedido`, `transacao`, `Tipo_Frete`,`status_descricao`, `forma_pagamento`, `frete`, `anotacao`, `email`) 
                          VALUES
                          ('" + pedido + "','" + transacao + "','" + Tipo_Frete + "','" + status_descricao + "', '" + forma_pagamento + "', '" + frete + "', '" + anotacao + "', '" + email + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos os produtos_vendas
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAll()
        {
            string SQL = "SELECT p.`id`, p.`pedido`, p.`transacao`, p.`Tipo_Frete`, p.`status_descricao`, p.`forma_pagamento`, p.`frete`, p.`anotacao`, p.`email` FROM produtos_vendas p ORDER BY p.`pedido` ASC;";
                return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos as vendas da loja
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAllVendas()
        {
            string SQL = "SELECT " +
                "(SELECT c.`CliNome` FROM cliente c WHERE c.`CliEmail` = p.`email`) Cliente, " +
                "(SELECT c.`cliente_id` FROM cliente c WHERE c.`CliEmail` = p.`email`) IdCliente, " +
                "p.`id`, p.`pedido`, p.`transacao`, p.`Tipo_Frete`, p.`status_descricao`, p.`forma_pagamento`, p.`frete`, p.`anotacao`, p.`email`, p.`itens`, p.`status_loja`, p.`Rastreador` FROM produtos_vendas p ORDER BY p.`pedido` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion

        #region seleciona produtos_vendas Por id pedido
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectByIdPedido(string pedido)
        {

            string SQL = "SELECT p.`id`, p.`pedido`, p.`transacao`, p.`Tipo_Frete`, p.`status_descricao`, p.`forma_pagamento`, p.`frete`, p.`anotacao`, p.`email` FROM produtos_vendas p WHERE p.`pedido` = '" + pedido + "' ORDER BY p.`pedido` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona produtos_vendas Por id transacao
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectByIdTransacao(string transacao)
        {

            string SQL = "SELECT p.`id`, p.`pedido`, p.`transacao`, p.`Tipo_Frete`, p.`status_descricao`, p.`forma_pagamento`, p.`frete`, p.`anotacao`, p.`email` FROM produtos_vendas p WHERE p.`transacao` = '" + transacao + "' ORDER BY p.`pedido` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por email cliente
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByEmailCliente(string email)
        {
            string SQL = string.Format("SELECT p.`id`, p.`pedido`, p.`transacao`, p.`Tipo_Frete`, p.`status_descricao`, p.`forma_pagamento`, p.`frete`, p.`anotacao`, p.`email`, p.`itens`, p.`status_loja`, p.`rastreador` FROM produtos_vendas p WHERE p.`email` = '" + email + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectOne(int id)
        {
            string SQL = string.Format("SELECT p.`id`, p.`pedido`, p.`transacao`, p.`Tipo_Frete`, p.`status_descricao`, p.`forma_pagamento`, p.`frete`, p.`anotacao`, p.`email` FROM produtos_vendas p WHERE p.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona Usuário por cod Pedido
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectClienteByIdPedido(string pedido)
        {
            string SQL = string.Format("SELECT " +
                            "(SELECT c.`cliente_id` FROM cliente c WHERE c.`CliEmail` = p.`email`) IdCliente, " +
                            "p.`pedido`, p.`email` FROM produtos_vendas p WHERE p.`pedido` = '" + pedido + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona Usuário por cod Transacao
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectClienteByIdTransacao(string transacao)
        {
            string SQL = string.Format("SELECT " +
                            "(SELECT c.`cliente_id` FROM cliente c WHERE c.`CliEmail` = p.`email`) IdCliente, " +
                            "p.`pedido`, p.`email` FROM produtos_vendas p WHERE p.`transacao` = '" + transacao + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void UpdateById(string id, string pedido, string transacao, string status, string status_descricao, string forma_pagamento, string frete, string anotacao, string email)
        {
            string SQL = @"UPDATE produtos_vendas SET transacao = '" + transacao + "', pedido = '" + pedido + "', status = '" + status + "', status_descricao = '" + status_descricao + "', forma_pagamento = '" + forma_pagamento + "', frete = '" + frete + "', anotacao = '" + anotacao + "', email = '" + email + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Atualizar status do pedido
        public static void UpdateByStatusLoja(string transacao, string status_loja, string rastreador)
        {
            string SQL = @"UPDATE produtos_vendas SET status_loja = '" + status_loja + "', rastreador = '" + rastreador + "' WHERE transacao = '" + transacao + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion

        #region Exluir item
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(string id)
        {
            string SQL = string.Format("DELETE FROM produtos_vendas WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #endregion
    }
}