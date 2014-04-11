using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Actio.Negocio;

public partial class adms_emailmarketing_Default : System.Web.UI.Page
{
    #region aparência da página ao carregar
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
        gridList.DataBind();

    }
    #region Credencial - checa se o usuário tem permissão para usar o recurs
    public void Credencial()
    {
        try
        {
        Usuario usuarioLogado = new Usuario(int.Parse(Page.User.Identity.Name));
        int Tipo = int.Parse(usuarioLogado.Tipo);

        if (Tipo == 2)
        {
            Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
            LabelTituloPagina.Text = "Administração de Recursos";

            PanelList.Visible = true;
            PanelEdit.Visible = false;
        }
        else
        {
            PanelList.Visible = false;
            PanelEdit.Visible = false;
            Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
            LabelTituloPagina.Text = "Administração de Recursos do sistema - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
        }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #endregion
    #region comandos da grid
    public void RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int id = int.Parse(e.CommandArgument.ToString());
            Session["id"] = id;

            if (e.CommandName == "Alterar")
                Carregar();
            if (e.CommandName == "Excluir")
                Excluir();
        }
        catch
        { }
    }
    #endregion
    #region lista de todos os itens
    protected void ibtList_Click(object sender, ImageClickEventArgs e)
    {
        PanelList.Visible = true;
        PanelEdit.Visible = false;
    }
    protected void lbtList_Click(object sender, EventArgs e)
    {
        PanelList.Visible = true;
        PanelEdit.Visible = false;
    }
    #endregion
    #region adicionar novo item
    #region aparencia da página
    protected void ibt_Novo_Click(object sender, ImageClickEventArgs e)
    {
        PanelList.Visible = false;
        PanelEdit.Visible = true;
        ibt_Alterar.Visible = false;
        ibt_Salvar.Visible = true;
        Titulo.Text = "";
        Url.Text = "";
    }
    protected void lbt_Novo_Click(object sender, EventArgs e)
    {
        PanelList.Visible = false;
        PanelEdit.Visible = true;
        ibt_Alterar.Visible = false;
        ibt_Salvar.Visible = true;
        Titulo.Text = "";
        Url.Text = "";
    }
    #endregion
    #region Salvar novo item
    protected void ibt_Salvar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = Recursos.nextID;

            DateTime date = DateTime.Now;
            string s = Convert.ToString(date);
            string newresult = "";
            try
            {

                foreach (char c in s)
                {
                    if (char.IsLetterOrDigit(c) && (!char.IsWhiteSpace(c)))
                    {
                        newresult += c.ToString();
                    }
                }
            }
            catch { }
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("IconeRecurso"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/imagens/recursos/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion

        #endregion
        #region salva dados
        Recursos.Inserir(Titulo.Text, HidIcone.Value, Url.Text);
        #endregion
        #region comportamenteo da página
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item inserido com sucesso!');window.location.src = window.location.src;", true);
        PanelList.Visible = true;
        PanelEdit.Visible = false;
        gridList.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region Atualizar item selecionado
    #region carrega dados
    public void Carregar()
    {
        PanelList.Visible = false;
        PanelEdit.Visible = true;
        ibt_Salvar.Visible = false;
        ibt_Alterar.Visible = true;

        DataTable dt = Recursos.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];

        Titulo.Text = dr["titulo"].ToString();
        Url.Text = dr["url"].ToString();
        IconePostado.ImageUrl = string.Format("~/imagens/recursos/{0}", dr["icone"].ToString());
        HidIcone.Value = dr["icone"].ToString();
        if (dr["noticia_icone"].ToString() != string.Empty)
            IconePostado.Visible = true;
    }
    #endregion
    #region salva dados
    protected void ibt_Alterar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            #region exclui icones
            try
            {
                int id = int.Parse(Session["id"].ToString());
                DataTable dt = Recursos.SelectByID(id);
                DataRow dr = dt.Rows[0];
                string arquivo = dr["icone"].ToString();
                string deletefile = Server.MapPath(string.Format(@"..\..\adms\imagens\recursos\{0}", arquivo));
                File.Delete(deletefile);
            }
            catch
            {

            }
            #endregion

            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id"].ToString());

            DateTime date = DateTime.Now;
            string s = Convert.ToString(date);
            string newresult = "";
            try
            {

                foreach (char c in s)
                {
                    if (char.IsLetterOrDigit(c) && (!char.IsWhiteSpace(c)))
                    {
                        newresult += c.ToString();
                    }
                }
            }
            catch { }
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("IconeRecurso"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/imagens/recursos/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion

        #endregion
        #region salva dados
        Recursos.Atualizar(Session["id"].ToString(), Titulo.Text, HidIcone.Value, Url.Text);
        #endregion
        #region comportamenteo da página
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
        PanelList.Visible = true;
        PanelEdit.Visible = false;
        gridList.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region excluir
    public void Excluir()
    {
        Recursos.Delete(Session["id"].ToString());
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item excluído com sucesso!');window.location.src = window.location.src;", true);
        PanelList.Visible = true;
        PanelEdit.Visible = false;
        gridList.DataBind();

    }
    #endregion
    #region cancelar cadastro
    protected void ibt_cancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/recursos/");
    }
    #endregion

}
