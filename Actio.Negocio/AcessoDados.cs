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
using UOL.PagSeguro;

namespace Actio.Negocio
{
    [DataObject(true)]
    public class AcessoDados
    {
        private static AcessoDados _instancia;
        public static AcessoDados Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new AcessoDados();
                }
                return _instancia;
            }
        }

        private AcessoDados()
        {

        }

        //Adicionar o pedido na sua base de dados
        public string GravarPedido(Carrinho carrinho)
        {
            return Convert.ToString(new Random().Next());
        }
        //Aqui estou inserindo nova venda
        public void IncluirVenda(string pedido, string transacao, string Tipo_Frete, string status_descricao, string forma_pagamento, string frete, string anotacao, string email, string itens)
        {
            string SQL = @"INSERT INTO produtos_vendas" +
                "(`pedido`, `transacao`, `Tipo_Frete`, `status_descricao`, `forma_pagamento`, `frete`, `anotacao`, `email`, `itens`)" +
                " VALUES " +
                "('" + pedido + "','" + transacao + "','" + Tipo_Frete + "','" + status_descricao + "','" + forma_pagamento + "','" + frete + "','" + anotacao + "','" + email + "', '" + itens + "');";
            conexao.ExecuteNonQuery(SQL);
        }
        //Aqui você deve atualizar o pedido na sua base de dados
        public void AtualizarVendaTransacaoId(string pedido, string transacao, string Tipo_Frete, string status_descricao, string forma_pagamento, string frete, string anotacao, string email, string itens)
        {
            string SQL = @"UPDATE produtos_vendas SET transacao = '" + transacao + "', Tipo_Frete = '" + Tipo_Frete + "', status_descricao = '" + status_descricao + "', forma_pagamento  = '" + forma_pagamento + "', frete = '" + frete + "', anotacao  = '" + anotacao + "', email  = '" + email + "',  itens = '" + itens + "' WHERE transacao = '" + transacao + "';";
            conexao.ExecuteNonQuery(SQL);
        }
        // insere cliente quando não existe
        public void InserirCliente(string CliNome, string CliEmail, string CliEndereco, string CliNumero, string CliComplemento, string CliBairro, string CliCidade, string CliEstado, string CliCEP, string CliTelefone, string emailmarketing, string status)
        {
            string SQL = @"INSERT INTO `cliente` 
                          (`CliNome`, `CliEmail`, `CliEndereco`, `CliNumero`, `CliComplemento`, `CliBairro`, `CliCidade`, `CliEstado`, `CliCEP`, `CliTelefone`, `emailmarketing`, `status`) 
                          VALUES
                          ('" + CliNome + "','" + CliEmail + "','" + CliEndereco + "','" + CliNumero + "','" + CliComplemento + "','" + CliBairro + "','" + CliCidade + "','" + CliEstado + "','" + CliCEP + "','" + CliTelefone + "','" + emailmarketing + "','" + status + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        // atualiza cliente quando existe
        public void AtualizarCliente(string CliNome, string CliEmail, string CliEndereco, string CliNumero, string CliComplemento, string CliBairro, string CliCidade, string CliEstado, string CliCEP, string CliTelefone, string emailmarketing, string status)
        {
            string SQL = @"UPDATE cliente SET CliNome = '" + CliNome + "', CliEmail = '" + CliEmail + "', CliEndereco = '" + CliEndereco + "', CliNumero = '" + CliNumero + "', CliComplemento = '" + CliComplemento + "', CliBairro = '" + CliBairro + "', CliCidade = '" + CliCidade + "', CliEstado = '" + CliEstado + "', CliCEP = '" + CliCEP + "', CliTelefone = '" + CliTelefone + "', emailmarketing = '" + emailmarketing + "', status = '" + status + "' WHERE CliEmail = '" + CliEmail + "'";
            conexao.ExecuteNonQuery(SQL);
        }
    }
}
