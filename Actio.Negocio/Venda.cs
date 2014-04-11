using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Actio.Negocio
{
    public class Venda
    {
        private string transacaoID;
        public string TransacaoID
        {
            get { return transacaoID; }
            set { transacaoID = value; }
        }
        private string dataTransacao;
        public string DataTransacao
        {
            get { return dataTransacao; }
            set { dataTransacao = value; }
        }
        private string vendedorEmail;
        public string VendedorEmail
        {
            get { return vendedorEmail; }
            set { vendedorEmail = value; }
        }
        private string referencia;
        public string Referencia
        {
            get { return referencia; }
            set { referencia = value; }
        }
        private string tipoFrete;
        public string TipoFrete
        {
            get { return tipoFrete; }
            set { tipoFrete = value; }
        }
        private string extras;
        public string Extras
        {
            get { return extras; }
            set { extras = value; }
        }
        private string parcelas;
        public string Parcelas
        {
            get { return parcelas; }
            set { parcelas = value; }
        }
        private string valorFrete;
        public string ValorFrete
        {
            get { return valorFrete; }
            set { valorFrete = value; }
        }
        private string anotacao;
        public string Anotacao
        {
            get { return anotacao; }
            set { anotacao = value; }
        }
        private string tipoPagamento;
        public string TipoPagamento
        {
            get { return tipoPagamento; }
            set { tipoPagamento = value; }
        }
        private string statusTransacao;
        public string StatusTransacao
        {
            get { return statusTransacao; }
            set { statusTransacao = value; }
        }
        private string cliNome;
        public string CliNome
        {
            get { return cliNome; }
            set { cliNome = value; }
        }
        private string cliEmail;
        public string CliEmail
        {
            get { return cliEmail; }
            set { cliEmail = value; }
        }
        private string cliEndereco;
        public string CliEndereco
        {
            get { return cliEndereco; }
            set { cliEndereco = value; }
        }
        private string cliNumero;
        public string CliNumero
        {
            get { return cliNumero; }
            set { cliNumero = value; }
        }
        private string cliComplemento;
        public string CliComplemento
        {
            get { return cliComplemento; }
            set { cliComplemento = value; }
        }
        private string cliBairro;
        public string CliBairro
        {
            get { return cliBairro; }
            set { cliBairro = value; }
        }
        private string cliCidade;
        public string CliCidade
        {
            get { return cliCidade; }
            set { cliCidade = value; }
        }
        private string cliEstado;
        public string CliEstado
        {
            get { return cliEstado; }
            set { cliEstado = value; }
        }
        private string cliCEP;
        public string CliCEP
        {
            get { return cliCEP; }
            set { cliCEP = value; }
        }
        private string cliTelefone;
        public string CliTelefone
        {
            get { return cliTelefone; }
            set { cliTelefone = value; }
        }
        private string numItens;
        public string NumItens
        {
            get { return numItens; }
            set { numItens = value; }
        }
    }
    public class Prod
    {
        private string prodID_;
        public string ProdID_
        {
            get { return prodID_; }
            set { prodID_ = value; }
        }
        private string prodDescricao_;
        public string ProdDescricao_
        {
            get { return prodDescricao_; }
            set { prodDescricao_ = value; }
        }
        private string prodValor_;
        public string ProdValor_
        {
            get { return prodValor_; }
            set { prodValor_ = value; }
        }
        private string prodQuantidade_;
        public string ProdQuantidade_
        {
            get { return prodQuantidade_; }
            set { prodQuantidade_ = value; }
        }
        private string prodFrete_;
        public string ProdFrete_
        {
            get { return prodFrete_; }
            set { prodFrete_ = value; }
        }
        private string prodExtras_;
        public string ProdExtras_
        {
            get { return prodExtras_; }
            set { prodExtras_ = value; }
        }
    }
}
