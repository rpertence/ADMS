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
    public class Clientes
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string CliNome, string CliEmail, string CliEndereco, string CliNumero, string CliComplemento, string CliBairro, string CliCidade, string CliEstado, string CliCEP, string CliTelefone, string emailmarketing, string status)
        {
            string SQL = @"INSERT INTO `cliente` 
                          (`CliNome`, `CliEmail`, `CliEndereco`, `CliNumero`, `CliComplemento`, `CliBairro`, `CliCidade`, `CliEstado`, `CliCEP`, `CliTelefone`, `emailmarketing`, `status`) 
                          VALUES
                          ('" + CliNome + "','" + CliEmail + "','" + CliEndereco + "','" + CliNumero + "','" + CliComplemento + "','" + CliBairro + "','" + CliCidade + "','" + CliEstado + "','" + CliCEP + "','" + CliTelefone + "','" + emailmarketing + "','" + status + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT c.`cliente_id`, c.`CliNome`, c.`CliEmail`, c.`CliEndereco`, c.`CliNumero`, c.`CliComplemento`, c.`CliBairro`, c.`CliCidade`, c.`CliEstado`, c.`CliCEP`, c.`CliTelefone`, c.`emailmarketing`, c.`status` FROM cliente c ORDER BY c.`CliNome` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int cliente_id)
        {
            string SQL = string.Format("SELECT c.`cliente_id`, c.`CliNome`, c.`CliEmail`, c.`CliEndereco`, c.`CliNumero`, c.`CliComplemento`, c.`CliBairro`, c.`CliCidade`, c.`CliEstado`, c.`CliCEP`, c.`CliTelefone`, c.`emailmarketing`, c.`status` FROM cliente c WHERE c.`cliente_id` = {0}", cliente_id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por email
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByEmail(string CliEmail)
        {
            string SQL = string.Format("SELECT c.`cliente_id`, c.`CliNome`, c.`CliEmail`, c.`CliEndereco`, c.`CliNumero`, c.`CliComplemento`, c.`CliBairro`, c.`CliCidade`, c.`CliEstado`, c.`CliCEP`, c.`CliTelefone`, c.`emailmarketing`, c.`status` FROM cliente c WHERE c.`CliEmail` = '" + CliEmail + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar cliente
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string cliente_id, string CliNome, string CliEmail, string CliEndereco, string CliNumero, string CliComplemento, string CliBairro, string CliCidade, string CliEstado, string CliCEP, string CliTelefone, string emailmarketing, string status)
        {
            string SQL = @"UPDATE cliente SET CliNome = '" + CliNome + "', CliEmail = '" + CliEmail + "', CliEndereco = '" + CliEndereco + "', CliNumero = '" + CliNumero + "', CliComplemento = '" + CliComplemento + "', CliBairro = '" + CliBairro + "', CliCidade = '" + CliCidade + "', CliEstado = '" + CliEstado + "', ClieCEP = '" + CliCEP + "', CliTelefone = '" + CliTelefone + "', emailmarketing = '" + emailmarketing + "', stauts = '" + status + "' WHERE cliente_id = '" + cliente_id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int cliente_id)
        {
            string SQL = string.Format("DELETE FROM cliente WHERE cliente_id = {0}", cliente_id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM cliente";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
    }
}
