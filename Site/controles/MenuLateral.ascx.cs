using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Actio.Negocio;


public partial class adms_controles_MenuLateral : System.Web.UI.UserControl
{
    #region aparencia ao carregar
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
        try
        {
            if (Session["PainelLateral"].ToString() == "expandido")
            {
                mvNavegacao.ActiveViewIndex = 0;
                ibt_Reduzir.Visible = true;
                ibt_expandir.Visible = false;
            }
            if (Session["PainelLateral"].ToString() == "reduzido")
            {
                mvNavegacao.ActiveViewIndex = 1;
                ibt_Reduzir.Visible = false;
                ibt_expandir.Visible = true;
            }
        }
        catch
        {
            mvNavegacao.ActiveViewIndex = 0;
            ibt_Reduzir.Visible = true;
            ibt_expandir.Visible = false;
        }
    }
    #endregion
    #region credencial - verifica quem é e o tipo do usuário logado
    public void Credencial()
    {
        try
        {
            Usuario usuarioLogado = new Usuario(int.Parse(Page.User.Identity.Name));
            Session["Tipo_Usuario"] = usuarioLogado.Tipo;
            Session["Id_Usuario"] = usuarioLogado.Id;
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #region estado dos paineis
    protected void ibt_expandir_Click(object sender, ImageClickEventArgs e)
    {
        Session["PainelLateral"] = "expandido";
        mvNavegacao.ActiveViewIndex = 0;
        ibt_Reduzir.Visible = true;
        ibt_expandir.Visible = false;
    }
    protected void ibt_Reduzir_Click(object sender, ImageClickEventArgs e)
    {
        Session["PainelLateral"] = "reduzido";
        mvNavegacao.ActiveViewIndex = 1;
        ibt_Reduzir.Visible = false;
        ibt_expandir.Visible = true;
    }
    #endregion
    #region commando dos repeater
    public void RecursoSelecionado(object sender, RepeaterCommandEventArgs e)
    {
        Response.Redirect(e.CommandArgument.ToString());
    }
    #endregion
}