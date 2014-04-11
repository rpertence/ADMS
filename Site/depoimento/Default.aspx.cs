using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using Actio.Negocio;

public partial class adms_depoimento_Default : System.Web.UI.Page
{
    #region aparência da página ao carregar
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
        gridList.DataBind();
    }
    #endregion
    #region padrão do enter
    private void PadraoDoEnter(ImageButton botao)
    {
        this.Page.Form.DefaultButton = botao.UniqueID;
    }
    #endregion
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
                LabelTituloPagina.Text = "Administração de Testemunhos";
                mvAll.ActiveViewIndex = 0;
            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("7", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Testemunhos";
                    mvAll.ActiveViewIndex = 0;
                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Testemunhos - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Testemunhos - Sua credencial não permite a utilização deste painel!";
            }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #endregion
    #region adicionar novo item
    #region aparencia da página
    protected void ibt_adicionar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 1;
        ibt_salvar.Visible = true;
        ibt_editar.Visible = false;
        PadraoDoEnter(ibt_salvar);
        Nome.Text = "";
        Email.Text = "";
        Resumo.Text = "";
        Descricao.Text = "";
    }
    #endregion
    #region salvar novo item
    protected void ibt_salvar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String n = Nome.Text;
        String n1 = n.Replace("\\", "/");
        String nome = n1.Replace("'", "\\'");
        String r = Resumo.Text;
        String r1 = r.Replace("\\", "/");
        String resumo = r1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        String l = Local.Text;
        String l1 = l.Replace("\\", "/");
        String local = l1.Replace("'", "\\'");

        string s1 = DateTime.Now.ToShortDateString();

        #endregion
        #endregion
        #region salva dados
        Depoimento.Insert(Email.Text, nome, Status.SelectedValue, descricao, resumo, s1, local);
        #endregion
        #region grava histórico
        Historico.Inserir(Page.User.Identity.Name, s1, "0", "Adicionou o item " + nome, "testemunhos");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        gridListBloqueados.DataBind();
        PadraoDoEnter(ibt_cancelar);
        #endregion
    }
    #endregion
    #endregion
    #region comandos da grid de listagem
    public void ComandoDaListagem(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Session["id"] = e.CommandArgument.ToString();
            if (e.CommandName.ToString() == "Editar")
                Carregar();
            if (e.CommandName.ToString() == "Excluir")
                Excluir();
        }
        catch { }
    }
    #endregion
    #region editar
    #region carrega dados
    public void Carregar()
    {
        #region aparencia da página
        mvAll.ActiveViewIndex = 1;
        ibt_editar.Visible = true;
        ibt_salvar.Visible = false;
        PadraoDoEnter(ibt_editar);
        #endregion
        #region carrega dados
        DataTable dt = Depoimento.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        Status.SelectedValue = dr["status"].ToString();
        Nome.Text = dr["nome"].ToString();
        Email.Text = dr["email"].ToString();
        Resumo.Text = dr["resumo"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        Local.Text = dr["local"].ToString();
        #endregion
    }
    #endregion
    #region salva dados
    protected void ibt_editar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String n = Nome.Text;
        String n1 = n.Replace("\\", "/");
        String nome = n1.Replace("'", "\\'");
        String r = Resumo.Text;
        String r1 = r.Replace("\\", "/");
        String resumo = r1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        String l = Local.Text;
        String l1 = l.Replace("\\", "/");
        String local = l1.Replace("'", "\\'");
        DateTime dates = DateTime.Now;
        string s1 = Convert.ToString(dates);
        #endregion
        #endregion
        #region salva dados
        Depoimento.Update(Session["id"].ToString(), Email.Text, nome, Status.SelectedValue, descricao, resumo, local);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Fez uma alteração no item " + nome, "testemunhos");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        gridListBloqueados.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region excluir
    public void Excluir()
    {
        #region dados para histórico
        DataTable dt = Depoimento.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        string nome = dr["nome"].ToString();
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Excluiu o testemunho de " + nome, "testemunhos");
        #endregion
        #region exclui item da base
        Depoimento.Delete(int.Parse(Session["id"].ToString()));
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridListBloqueados.DataBind();
        gridList.DataBind();
        #endregion
    }
    #endregion
    #region cancelar edição
    protected void ibt_cancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        gridListBloqueados.DataBind();
    }
    #endregion
}