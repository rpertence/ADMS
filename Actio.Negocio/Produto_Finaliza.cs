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
    public class Protudo_Finaliza
    {
        #region botão de compra
        public static void comprar(string item_id, string item_descr, string item_quant, decimal item_valor, string peso)
        {
            string ValorFrete = "";
            string valor = item_valor.ToString();
            string valorfinal = valor.Replace(",", ".");


            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Write("<html><head>");
            context.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", "pagseguro"));
            context.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", "pagseguro", "post", "https://pagseguro.uol.com.br/v2/checkout/cart.html?action=add"));
            context.Response.Write(string.Format("<input type=\"hidden\" name=\"encoding\" value=\"utf-8\">"));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("receiverEmail"), HttpUtility.HtmlEncode("rccshopping@rccbh.com.br")));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("currency"), HttpUtility.HtmlEncode("BRL")));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("itemId"), HttpUtility.HtmlEncode(item_id)));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("itemDescription"), HttpUtility.HtmlEncode(item_descr)));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("itemQuantity"), HttpUtility.HtmlEncode(item_quant)));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("itemAmount"), HttpUtility.HtmlEncode(valorfinal)));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("itemWeight"), HttpUtility.HtmlEncode(peso)));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("itemShippingCost"), HttpUtility.HtmlEncode(ValorFrete)));

            context.Response.Write("</form>");
            context.Response.Write("</body></html>");
            context.Response.End();

        }
        #endregion
        #region ver carrinho
        public static void vercarrinho()
        {
            string email_cobranca = "rccshopping@rccbh.com.br";
            string Url = "https://pagseguro.uol.com.br/security/webpagamentos/webpagto.aspx";
            string Method = "post";
            string FormName = "nome";

            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Write("<html><head>");
            context.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            context.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("email_cobranca"), HttpUtility.HtmlEncode(email_cobranca)));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("tipo"), HttpUtility.HtmlEncode("CBR")));
            context.Response.Write("</form>");
            context.Response.Write("</body></html>");
            context.Response.End();

        }
        #endregion
        #region doação
        public static void Doacao()
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Write("<html><head>");
            context.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", "doacao"));
            context.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", "doacao", "post", "https://pagseguro.uol.com.br/checkout/doacao.jhtml"));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("email_cobranca"), HttpUtility.HtmlEncode("rccshopping@rccbh.com.br")));
            context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode("moeda"), HttpUtility.HtmlEncode("BRL")));
            context.Response.Write("</form>");
            context.Response.Write("</body></html>");
            context.Response.End();

        }
        #endregion
    }
}