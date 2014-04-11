using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Actio.Negocio;
using System.IO;

public partial class ActioAdms_equipe_Default : System.Web.UI.Page
{
    #region aparência da página ao carregar
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
        gridList.Visible = true;
    }
    #endregion
    #region padrão do enter
    private void PadraoDoEnter(ImageButton botao)
    {
        this.Page.Form.DefaultButton = botao.UniqueID;
    }
    #endregion
    #region Credencial - checa se o usuário tem permissão para usar o recurso
    public void Credencial()
    {
        try
        {
            Usuario usuarioLogado = new Usuario(int.Parse(Page.User.Identity.Name));
            int Tipo = int.Parse(usuarioLogado.Tipo);

            if (Tipo == 2)
            {
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Equipe leo";
                mvAll.ActiveViewIndex = 0;
                gridList.Visible = true;
                gridList.DataBind();

            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("12", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Equipe";
                    mvAll.ActiveViewIndex = 0;
                    gridList.Visible = true;
                    gridList.DataBind();

                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Equipe - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Equipe  - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
            }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #endregion
    #region novo item
    #region aparencia da página
    protected void ibt_adicionar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 1;
        ibt_editar.Visible = false;
        ibt_salvar.Visible = true;
        Titulo.Text = "";
        Email.Text = "";
        Resumo.Text = "";
        Descricao.Text = "";
        rfvIcone.Enabled = true;
        IconePostado.Visible = false;
        PadraoDoEnter(ibt_salvar);
    }
    #endregion
    #region salvar informações
    protected void ibt_salvar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = Titulo.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        String r = Resumo.Text;
        String r1 = r.Replace("\\", "/");
        String resumo = r1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = Equipe.nextid;
            Session["nextID"] = nextID;
            DateTime dates = DateTime.Now;
            string s1 = Convert.ToString(dates);
            string newresult = "";
            try
            {

                foreach (char c in s1)
                {
                    if (char.IsLetterOrDigit(c) && (!char.IsWhiteSpace(c)))
                    {
                        newresult += c.ToString();
                    }
                }
            }
            catch { }
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioEquipeIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/Equipe/Icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region salva dados
        Equipe.Inserir(titulo, resumo, descricao, HidIcone.Value, Status.SelectedValue, Destaque.SelectedValue.ToString(), "0", Email.Text);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + titulo, "Equipe");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        PadraoDoEnter(ibt_cancelar);
        gridList.DataBind();
        gridList.Visible = true;
        #endregion
    }
    #endregion
    #endregion
    #region comandos da grid de listagem
    public void ComandoDaListagem(object sender, GridViewCommandEventArgs e)
    {
        Session["id"] = e.CommandArgument.ToString();
        try
        {
            if (e.CommandName.ToString() == "Editar")
                Carregar();
            if (e.CommandName.ToString() == "Excluir")
                Excluir();
        }
        catch { }
    }
    #endregion
    #region editar um item
    #region carrega dados
    public void Carregar()
    {
        #region aparencia da página
        mvAll.ActiveViewIndex = 1;
        ibt_editar.Visible = true;
        ibt_salvar.Visible = false;
        PadraoDoEnter(ibt_editar);
        IconePostado.Visible = true;
        #endregion
        #region carrega dados
        DataTable dt = Equipe.SelectByid(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        Destaque.SelectedValue = dr["destaque"].ToString();
        Titulo.Text = dr["titulo"].ToString();
        Email.Text = dr["email"].ToString();
        rfvIcone.Enabled = false;
        HidIcone.Value = dr["icone"].ToString();
        IconePostado.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/Equipe/icones/" + dr["icone"].ToString());
        Resumo.Text = dr["resumo"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        Status.SelectedValue = dr["status"].ToString();
        #endregion
    }
    #endregion
    #region salvar alterações
    protected void ibt_editar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = Titulo.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        String r = Resumo.Text;
        String r1 = r.Replace("\\", "/");
        String resumo = r1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            #region exclui icones
            try
            {
                string arquivo = HidIcone.Value;
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\Equipe\Icones\{0}", arquivo));
                File.Delete(deletefile);
            }
            catch
            {

            }
            #endregion

            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id"].ToString());

            DateTime dates = DateTime.Now;
            string s1 = Convert.ToString(dates);
            string newresult = "";
            try
            {

                foreach (char c in s1)
                {
                    if (char.IsLetterOrDigit(c) && (!char.IsWhiteSpace(c)))
                    {
                        newresult += c.ToString();
                    }
                }
            }
            catch { }
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActiotextoIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/Equipe/Icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion

        #endregion
        #region salva dados
        Equipe.Atualizar(Session["id"].ToString(), titulo, resumo, descricao, HidIcone.Value, Status.SelectedValue, Destaque.SelectedValue.ToString(), "0", Email.Text);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o item " + titulo, "Equipe");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        PadraoDoEnter(ibt_cancelar);
        #endregion
    }
    #endregion
    #endregion
    #region excluir
    public void Excluir()
    {
        #region carrega dados
        DataTable dt = Equipe.SelectByid(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        string arquivo = dr["icone"].ToString();
        string titulo = dr["titulo"].ToString();
        #endregion
        #region apaga arquivo 
        try
        {
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\Equipe\Icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }

        #endregion
        #region apaga registros na base de dados
        Equipe.Delete(int.Parse(Session["id"].ToString()));
        try
        {
            Textos.ExcluirByIdEquipe(Session["id"].ToString());
            #region grava histórico
            DateTime date = DateTime.Now;
            string s = Convert.ToString(date);
            Historico.Inserir(Page.User.Identity.Name, s, "2", "Exclui o coordenador " + titulo + ", por isso o Ministério também foi excluido", "Equipe");
            #endregion
        }
        catch
        { }
        #endregion
        #region grava histórico
        DateTime dia = DateTime.Now;
        string ss = Convert.ToString(dia);
        Historico.Inserir(Page.User.Identity.Name, ss, "2", "Exclui o item " + titulo, "Equipe");
        #endregion
        #region comporatamento da página
        Response.Redirect("~/equipe/");
        #endregion
    }
    #endregion
    #region voltar para o inicio
    protected void ibt_cancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
    }

    #endregion
}