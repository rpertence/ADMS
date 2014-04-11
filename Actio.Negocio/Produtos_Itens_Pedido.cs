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
    public class Produtos_Itens_Pedido
    {
        #region produtos_vendas
        #region Novo Produto Venda
        public static void Inserir(string TransacaoID, string dataTransacao, string Pedido, string ProdID, string ProdDescricao, string ProdValor, string ProdQuantidade, string ProdFrete, string ProdExtras, string ProdStatus)
        {
            string SQL = @"INSERT INTO `produtos_itens_pedido` 
                          (`TransacaoID`, `dataTransacao`, `Pedido`, `ProdID`, `ProdDescricao`, `ProdQuantidade`, `ProdFrete`, `ProdValor`, `ProdExtras`, `ProdStatus`) 
                          VALUES
                          ('" + TransacaoID + "','" + dataTransacao + "','" + Pedido + "','" + ProdID + "', '" + ProdDescricao + "', '" + ProdQuantidade + "', '" + ProdFrete + "', '" + ProdValor + "', '" + ProdExtras + "', '" + ProdStatus + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos os produtos_vendas
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectAll()
        {
            string SQL = "SELECT p.`id`, p.`TransacaoID`, p.`dataTransacao`, p.`Pedido`, p.`ProdID`, p.`ProdDescricao`, p.`ProdQuantidade`, p.`ProdFrete`, p.`ProdValor`, p.`ProdStatus` FROM produtos_itens_pedido p ORDER BY p.`TransacaoID` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona produtos_vendas Por id TransacaoID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectByTransacaoID(string TransacaoID)
        {

            string SQL = "SELECT p.`id`, p.`TransacaoID`, p.`dataTransacao`, p.`Pedido`, p.`ProdID`, p.`ProdDescricao`, p.`ProdQuantidade`, p.`ProdFrete`, p.`ProdValor`, p.`ProdStatus`, p.`StatusEnvio`, p.`Rastreador` FROM produtos_itens_pedido p WHERE p.`TransacaoID` = '" + TransacaoID + "' ORDER BY p.`TransacaoID` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona produtos_vendas Por id do Pedido
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectByIdPedido(string Pedido)
        {

            string SQL = "SELECT p.`id`, p.`TransacaoID`, p.`dataTransacao`, p.`Pedido`, p.`ProdID`, p.`ProdDescricao`, p.`ProdQuantidade`, p.`ProdFrete`, p.`ProdValor`, p.`ProdStatus` FROM produtos_itens_pedido p WHERE p.`Pedido` = '" + Pedido + "' ORDER BY p.`TransacaoID` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona produtos_vendas Por id do produto e IdTransacao
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable selectByTransacaoIDProdID(string TransacaoID, string ProdID)
        {

            string SQL = "SELECT p.`id`, p.`TransacaoID`, p.`dataTransacao`, p.`Pedido`, p.`ProdID`, p.`ProdDescricao`, p.`ProdQuantidade`, p.`ProdFrete`, p.`ProdValor`, p.`ProdStatus`, p.`StatusEnvio`, p.`Rastreador` FROM produtos_itens_pedido p WHERE p.`TransacaoID` = '" + TransacaoID + "' AND ProdID = '" + ProdID + "' ORDER BY p.`TransacaoID` ASC;";
            return conexao.Dados(SQL);
        }
        #endregion

        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectOne(int id)
        {
            string SQL = string.Format("SELECT p.`id`, p.`TransacaoID`, p.`dataTransacao`, p.`Pedido`, p.`ProdID`, p.`ProdDescricao`, p.`ProdQuantidade`, p.`ProdFrete`, p.`ProdValor`, p.`ProdStatus` FROM produtos_itens_pedido p WHERE p.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar por id da transação
        public static void UpdateByTransacaoID(string id, string TransacaoID, string dataTransacao, string Pedido, string ProdID, string ProdDescricao, string ProdValor, string ProdQuantidade, string ProdFrete, string ProdExtras, string ProdStatus)
        {
            string SQL = @"UPDATE produtos_itens_pedido SET dataTransacao = '" + dataTransacao + "', Pedido = '" + Pedido + "', ProdID = '" + ProdID + "', ProdDescricao = '" + ProdDescricao + "', ProdQuantidade = '" + ProdQuantidade + "', ProdFrete = '" + ProdFrete + "', ProdValor = '" + ProdValor + "', ProdExtras = '" + ProdExtras + "', ProdStatus = '" + ProdStatus + "' WHERE TransacaoID = '" + TransacaoID + "' AND id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Atualizar por status e id
        public static void UpdateByTransacaoIDItemID(string id, string TransacaoID, string dataTransacao, string Pedido, string ProdID, string ProdDescricao, string ProdValor, string ProdQuantidade, string ProdFrete, string ProdExtras, string ProdStatus)
        {
            string SQL = @"UPDATE produtos_itens_pedido SET dataTransacao = '" + dataTransacao + "', Pedido = '" + Pedido + "', ProdID = '" + ProdID + "', ProdDescricao = '" + ProdDescricao + "', ProdQuantidade = '" + ProdQuantidade + "', ProdFrete = '" + ProdFrete + "', ProdValor = '" + ProdValor + "', ProdExtras = '" + ProdExtras + "', ProdStatus = '" + ProdStatus + "' WHERE TransacaoID = '" + TransacaoID + "' AND id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Atualizar por Envio
        public static void UpdateByStatusEnvio(string id, string StatusEnvio, string Rastreador, string TransacaoID)
        {
            string SQL = @"UPDATE produtos_itens_pedido SET StatusEnvio = '" + StatusEnvio + "', Rastreador = '" + Rastreador + "' WHERE ProdID = '" + id + "' AND TransacaoID = '" + TransacaoID + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        public static void UpdateByStatusEnvioTodos(string TransacaoID, string StatusEnvio, string Rastreador)
        {
            string SQL = @"UPDATE produtos_itens_pedido SET StatusEnvio = '" + StatusEnvio + "', Rastreador = '" + Rastreador + "' WHERE TransacaoID = '" + TransacaoID + "'";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion

        #region Exluir item por id
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(string id)
        {
            string SQL = string.Format("DELETE FROM produtos_itens_pedido WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item id da transacao
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void ExcluirTransacaoID(string TransacaoID)
        {
            string SQL = string.Format("DELETE FROM produtos_itens_pedido WHERE TransacaoID = {0}", TransacaoID.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #endregion
    }
}