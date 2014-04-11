using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Actio.Negocio;
using System.IO;

public partial class ActioAdms_artigos_Default : System.Web.UI.Page
{
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
    }
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
                LabelTituloPagina.Text = "Administração de Artigos e Autores leo";
                mvAll.ActiveViewIndex = 0;
                gridList.Visible = true;
                gridList.DataBind();

            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("17", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Artigos e Autores";
                    mvAll.ActiveViewIndex = 0;
                    gridList.Visible = true;
                    gridList.DataBind();

                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Artigos e Autores - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Artigos e Autores  - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
            }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #endregion
    #region autores
    #region comandos da grid de autores
    protected void ComandoAutores(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditarAutor")
        {
            CarregaAutor(e.CommandArgument.ToString());
        }
        if (e.CommandName == "ExcluirAutor")
        {
            ApagaAutor(e.CommandArgument.ToString());
        }
        if (e.CommandName == "Artigos")
        {
            ArtigosdoAutor(e.CommandArgument.ToString());
        }
    }
    #endregion
    #region artigos do autor
    protected void ArtigosdoAutor(string id)
    {
        #region aparencia da Página
        #region carrega dados
        DataTable dt = Artigo_Autor.SelectByID(int.Parse(id.ToString()));
        DataRow dr = dt.Rows[0];
        string nome = dr["nome"].ToString();
        Session["id_autor_selecionado"] = dr["id"].ToString();
        LabelAutor.Text = nome;
        #endregion
        #region aparencia da página
        mvAll.ActiveViewIndex = 4;
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Autores - Listagem dos Artigos do Autor " + nome + " cadastrados no sistema";
        #endregion
        #endregion
    }
    #endregion
    #region ver autores
    protected void bt_autores_Click(object sender, EventArgs e)
    {
        mvAll.ActiveViewIndex = 1;
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Autores - Autores cadastrados no sistema";
        PadraoDoEnter(ibt_salvar_autor);

    }
    protected void ibt_cancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 1;
        mvAll.ActiveViewIndex = 1;
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Autores - Autores cadastrados no sistema";
    }
    #endregion
    #region novo autor
    #region aparencia da página
    protected void bt_novo_autor_Click(object sender, EventArgs e)
    {
        Nome.Text = "";
        Email.Text = "";
        Descricao.Text = "";
        IconePostado.Visible = false;
        ibt_editar_autor.Visible = false;
        ibt_salvar_autor.Visible = true;
        mvAll.ActiveViewIndex = 3;
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Autores - Cadastrar Novo autor";
        PadraoDoEnter(ibt_salvar_autor);
    }
    #endregion
    #region Salva dados
    protected void ibt_salvar_autor_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = Nome.Text;
        String t1 = t.Replace("\\", "/");
        String nome = t1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = Artigo_Autor.nextID;
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioAutores"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/artigos_autores/Icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region grava dados na base
        Artigo_Autor.Inserir(nome, Email.Text, descricao, HidIcone.Value);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o Autor " + nome, "Autores de Artigos");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 1;
        gridAutores.DataBind();
        #endregion
    }

    #endregion
    #endregion
    #region editar autores
    #region carrega dados
    protected void CarregaAutor(string id)
    {
        DataTable dt = Artigo_Autor.SelectByID(int.Parse(id.ToString()));
        DataRow dr = dt.Rows[0];
        Nome.Text = dr["nome"].ToString();
        Email.Text = dr["email"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        IconePostado.Visible = true;
        IconePostado.ImageUrl = "~/App_Themes/ActioAdms/hd/artigos_autores/icones/" + dr["icone"].ToString();
        ibt_editar_autor.Visible = true;
        ibt_salvar_autor.Visible = false;
        mvAll.ActiveViewIndex = 3;
        HidIcone.Value = dr["icone"].ToString();
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Autores - Atualizar os dados do autor" + dr["nome"].ToString();
        PadraoDoEnter(ibt_editar_autor);
        Session["id_autor"] = dr["id"].ToString();
    }
    #endregion
    #region salva dados na dedição 
    protected void ibt_editar_autor_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = Nome.Text;
        String t1 = t.Replace("\\", "/");
        String nome = t1.Replace("'", "\\'");
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
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\artigos_autores\icones\{0}", arquivo));
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioAutores"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/artigos_autores/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region salva dados na base
        Artigo_Autor.Atualizar(Session["id_autor"].ToString(), nome, Email.Text, descricao, HidIcone.Value);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o Autor " + nome, "Autores de Artigos");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridAutores.DataBind();
        #endregion

    }
    #endregion
    #region apaga autor
    protected void ApagaAutor(string id)
    {
        #region carrega dados do autor
        DataTable dt = Artigo_Autor.SelectByID(int.Parse(id));
        DataRow dr = dt.Rows[0];
        string icone = dr["icone"].ToString();
        string nome = dr["nome"].ToString();
        #endregion
        #region apaga icone do autor
        try
        {
            string arquivo = icone;
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\artigos_autores\icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion
        #region apaga artigos do autor
        Artigos.ExcluirByIdAutor(int.Parse(id));
        #endregion
        #region excluir autor
        Artigo_Autor.Delete(int.Parse(id));
        #endregion
        #region grava histórico
        DateTime dia = DateTime.Now;
        string ss = Convert.ToString(dia);
        Historico.Inserir(Page.User.Identity.Name, ss, "2", "Exclui o autor (e seus artigos) " + nome, "Artigos e Autores");
        #endregion
        #region comporatamento da página
        Response.Redirect("~/artigos/");
        #endregion

    }
    #endregion
    #endregion
    #endregion
    #region artigos
    #region novo artigo
    #region aparencia da página
    protected void bt_novo_artigo_Click(object sender, EventArgs e)
    {
        mvAll.ActiveViewIndex = 2;
        PadraoDoEnter(ibt_salvar_artigo);
        Ordem.Text = "";
        Data.Text = "";
        Destaque.Checked = false;
        Titulo.Text = "";
        Resumo.Text = "";
        DescricaoArtigo.Text = "";
        ibt_salvar_artigo.Visible = true;
        ibt_editar_artigo.Visible = false;
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Artigos - Cadastrar Novo Artigo";
        PadraoDoEnter(ibt_salvar_autor);

    }
    #endregion
    #region salva novo artigo
    protected void ibt_salvar_artigo_Click(object sender, ImageClickEventArgs e)
    {
        #region trata strings importantes
        String t = Titulo.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        String r = Resumo.Text;
        String r1 = r.Replace("\\", "/");
        String resumo = r1.Replace("'", "\\'");
        String d = DescricaoArtigo.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #region salva dados na base
        Artigos.Inserir(titulo, resumo, descricao, Data.Text, Ordem.Text, "0", Destaque.Checked ? "1" : "0", Status.SelectedValue, ddlAutores.SelectedValue);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o Artigo com o Título " + titulo, "Artigos");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region voltar para artigos
    protected void bt_voltar_artigos_Click(object sender, EventArgs e)
    {
        mvAll.ActiveViewIndex = 0;
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Artigos - Artigos cadastrados no sistema";
    }
    protected void ibt_cancelar_artigo_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 0;
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Artigos - Artigos cadastrados no sistema";
    }
    #endregion
    #region comando da grid de artigos
    protected void ComandosArtigos(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditarArtigo")
        {
            CarregaArtigo(e.CommandArgument.ToString());
        }
        if (e.CommandName == "ExcluirArtigo")
        {
            ExcluiArtigo(e.CommandArgument.ToString());
        }
        if (e.CommandName == "EditarAutor")
        {
            CarregaAutor(e.CommandArgument.ToString());
        }
    }
    #endregion
    #region edita artigo
    #region carrega dados
    protected void CarregaArtigo(string id)
    {
        #region aparencia da página
        mvAll.ActiveViewIndex = 2;
        ibt_salvar_artigo.Visible = false;
        ibt_editar_artigo.Visible = true;
        Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
        LabelTituloPagina.Text = "Administração de Artigos - Cadastrar novo Artigo";
        #endregion
        #region carrega dados
        DataTable dt = Artigos.SelectByID(int.Parse(id));
        DataRow dr = dt.Rows[0];
        Status.SelectedValue = dr["status"].ToString();
        ddlAutores.SelectedValue = dr["id_autor"].ToString();
        Ordem.Text = dr["ordem"].ToString();
        Data.Text = dr["data"].ToString();
        if (dr["destaque"].ToString() == "1")
            Destaque.Checked = true;
        Titulo.Text = dr["titulo"].ToString();
        Resumo.Text = dr["resumo"].ToString();
        DescricaoArtigo.Text = dr["descricao"].ToString();
        Session["id_artigo"] = dr["id"].ToString();
        #endregion

    }

    #endregion
    #region salva dados na edição
    protected void ibt_editar_artigo_Click(object sender, ImageClickEventArgs e)
    {
        #region trata strings importantes
        String t = Titulo.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        String r = Resumo.Text;
        String r1 = r.Replace("\\", "/");
        String resumo = r1.Replace("'", "\\'");
        String d = DescricaoArtigo.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #region salva dados na base
        Artigos.Atualizar(Session["id_artigo"].ToString(), titulo, resumo, descricao, Data.Text, Ordem.Text, "0", Destaque.Checked ? "1" : "0", Status.SelectedValue, ddlAutores.SelectedValue);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o Artigo " + titulo, "Artigos");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        #endregion

    }
    #endregion
    #endregion
    #region exclui artigos
    protected void ExcluiArtigo(string id)
    {
        #region carrega dados
        DataTable dt = Artigos.SelectByID(int.Parse(id));
        DataRow dr = dt.Rows[0];
        string titulo = dr["titulo"].ToString();
        #endregion
        #region apaga artigo
        Artigos.Excluir(int.Parse(id));
        #endregion
        #region grava histórico
        DateTime dia = DateTime.Now;
        string ss = Convert.ToString(dia);
        Historico.Inserir(Page.User.Identity.Name, ss, "2", "Exclui o Artigo " + titulo, "Artigos");
        #endregion
        #region comporatamento da página
        Response.Redirect("~/artigos/");
        #endregion
    }
    #endregion
    #endregion
}