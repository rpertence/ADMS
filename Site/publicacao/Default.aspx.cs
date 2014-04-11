using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Actio.Negocio;

public partial class adms_sociais_Default : System.Web.UI.Page
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
                LabelTituloPagina.Text = "Administração de publicações";
                mvAll.ActiveViewIndex = 0;
            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("20", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de publicações";
                    mvAll.ActiveViewIndex = 0;
                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de publicaçõess - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de publicações - Sua credencial não permite a utilização deste painel!";
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
        Titulo.Text = "";
        Data.Text = "";
        Edicao.Text = "";
        Descricao.Text = "";
        Anexo.Text = "";
        IconePostado.Visible = false;
    }
    #endregion
    #region salvar novo item
    protected void ibt_salvar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = Titulo.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        String ed = Edicao.Text;
        String e1 = ed.Replace("\\", "/");
        String edicao = e1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = Publicacao.nextID;
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioPublicacaoIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/publicacoes/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region salva dados
        Publicacao.Inserir(titulo, descricao, Anexo.Text, HidIcone.Value, Data.Text, edicao);
        
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + titulo, "publicações");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
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
        DataTable dt = Publicacao.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];

        Titulo.Text = dr["titulo"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        Data.Text = dr["data_publicacao"].ToString();
        Edicao.Text = dr["edicao"].ToString();
        Anexo.Text = dr["anexo"].ToString();
        IconePostado.Visible = true;
        IconePostado.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/publicacoes/icones/" + dr["icone"].ToString());
        HidIcone.Value = dr["icone"].ToString();
        rfvIcone.Enabled = false;
        #endregion
    }
    #endregion
    #region salva dados
    protected void ibt_editar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = Titulo.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        String ed = Edicao.Text;
        String e1 = ed.Replace("\\", "/");
        String edicao = e1.Replace("'", "\\'");
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
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\publicacoes\icones\{0}", arquivo));
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioPublicacaoIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/publicacoes/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion


        #endregion
        #region salva dados
        Publicacao.Atualizar(Session["id"].ToString(), titulo, descricao, Anexo.Text, HidIcone.Value, Data.Text, edicao);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o item " + titulo, "Publicação");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region excluir
    public void Excluir()
    {
        #region carrega dados
        DataTable dt = Publicacao.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        string titulo = dr["titulo"].ToString();
        string icone = dr["icone"].ToString();
        string anexo = dr["anexo"].ToString();
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Excluiu o item " + titulo, "Publicações");
        #endregion
        #region exclui item
        Publicacao.Delete(int.Parse(Session["id"].ToString()));
        #endregion
        #region exclui icones
        try
        {
            string arquivo = icone;
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\publicacoes\icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        #endregion
    }
    #endregion
    #region cancelar edição
    protected void ibt_cancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
    }
    #endregion
}