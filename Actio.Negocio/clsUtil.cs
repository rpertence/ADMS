using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Actio.Negocio;



namespace PizzaDev.Poll
{
    public class clsUtil
    {
        public static string UserOnLine
        {
            get
            {
                string userName = "";

                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                string id = HttpContext.Current.User.Identity.Name;
                Usuario usuarioLogado = new Usuario(int.Parse(id.ToString()));
                userName = usuarioLogado.Nome;
                }
                return userName;
            }
        }
    }
}

