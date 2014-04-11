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
    public class Produtos_Comprados
    {
        #region comandos Básicos
        #region Novo
        public static void Novo(string ProdFrete_, string id_cliente, string TransacaoID, string Extras, string TipoFrete, string ValorFrete, string Anotacao, string DataTransacao, string TipoPagamento, string Status_Transacao, string ProdID, string ProdValor_, string ProdDescricao, string ProdQuantidade_, string NumItens, string Parcelas)
        {
            string SQL = @"INSERT INTO `produtos_comprados` 
                          (`ProdFrete_`, `id_cliente`, `TransacaoID`, `Extras`,`TipoFrete`, `ValorFrete`, `Anotacao`, `DataTransacao`, `TipoPagamento`, `Status_Transacao`, `ProdID`, `ProdDescricao`, `ProdValor_`, `ProdQuantidade_`, `NumItens`, `Parcelas`) 
                          VALUES
                          ('" + ProdFrete_ + "','" + id_cliente + "','" + TransacaoID + "','" + Extras + "','" + TipoFrete + "', '" + ValorFrete + "', '" + Anotacao + "', '" + DataTransacao + "', '" + TipoPagamento + "', '" + Status_Transacao + "', '" + ProdID + "', '" + ProdDescricao + "', '" + ProdValor_ + "', '" + ProdQuantidade_ + "', '" + NumItens + "', '" + Parcelas + "' );";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos os produtos_comprados
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAll()
        {

            string SQL = "SELECT p.`id`, p.`id_cliente`, p.`TransacaoID`, p.`Extras`, p.`TipoFrete`, p.`ValorFrete`, p.`Anotacao`, p.`DataTransacao`, p.`TipoPagamento`, p.`Status_Transacao`, p.`ProdID`, p.`ProdDescricao`, p.`ProdValor_`, p.`ProdQuantidade_`, p.`ProdFrete_`, p.`NumItens`, p.`Parcelas` FROM produtos_comprados p;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectById(int id)
        {
            string SQL = "SELECT p.`id`, p.`id_cliente`, p.`TransacaoID`, p.`Extras`, p.`TipoFrete`, p.`ValorFrete`, p.`Anotacao`, p.`DataTransacao`, p.`TipoPagamento`, p.`Status_Transacao`, p.`ProdID`, p.`ProdDescricao`, p.`ProdValor_`, p.`ProdQuantidade_`, p.`ProdFrete_`, p.`NumItens`, p.`Parcelas` FROM produtos_comprados p WHERE p.`id` = '" + id + "';";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void Update(string id, string ProdFrete_, string id_cliente, string TransacaoID, string Extras, string TipoFrete, string ValorFrete, string Anotacao, string DataTransacao, string TipoPagamento, string Status_Transacao, string ProdID, string ProdValor_, string ProdDescricao, string ProdQuantidade_, string NumItens, string Parcelas)
        {
            string SQL = @"UPDATE produtos_comprados SET ProdFrete_ = '" + ProdFrete_ + "', TransacaoID = '" + TransacaoID + "', id_cliente = '" + id_cliente + "', Extras = '" + Extras + "', TipoFrete = '" + TipoFrete + "', ValorFrete = '" + ValorFrete + "', Anotacao = '" + Anotacao + "', DataTransacao = '" + DataTransacao + "', TipoPagamento = '" + TipoPagamento + "', Status_Transacao = '" + Status_Transacao + "', ProdID = '" + ProdID + "', ProdValor_ = '" + ProdValor_ + "', ProdDescricao = '" + ProdDescricao + "', ProdQuantidade_ = '" + ProdQuantidade_ + "', NumItens = '" + NumItens + "', Parcelas = '" + Parcelas + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(string id)
        {
            string SQL = string.Format("DELETE FROM produtos_comprados WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM produtos_comprados";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
        #endregion
    }
}