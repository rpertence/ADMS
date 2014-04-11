using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Actio.Negocio;
using System.IO;

public partial class adms_Institucional_Default : System.Web.UI.Page
{
    #region aparência da página ao carregar
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
        gridList.Visible = false;
        LabelCategoria.Text = "Selecione uma categoria";

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
                LabelTituloPagina.Text = "Administração de Textos Institucionais";
                mvAll.ActiveViewIndex = 0;
                lbt_categorias.Visible = true;
            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("4", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Textos Institucionais";
                    mvAll.ActiveViewIndex = 0;
                    lbt_categorias.Visible = false;
                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Textos Institucionais - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Textos Institucionais  - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
            }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #endregion
    #region itens
    #region mostra e esconde panel de icone
    public void MostraPanelIcone(object sender, EventArgs e)
    {
        int tipo = int.Parse(Categoria.SelectedValue);
        switch (tipo)
        {
            case 4:
                panelEquipe.Visible = true;
                HidEquipe.Value = ddl_id_coordenador.SelectedValue;
                break;
            case 2:
                panelEquipe.Visible = false;
                HidEquipe.Value = "0";
                break;
            case 3:
                panelEquipe.Visible = false;
                HidEquipe.Value = "0";
                break;
            case 5:
                panelEquipe.Visible = false;
                HidEquipe.Value = "0";
                break;
        }
        PanelIcone.Visible = false;

    }
    #endregion
    #region Comando da listagem de categorias
    public void ComandoTitulosCategorias(object sender, DataListCommandEventArgs e)
    {
        Session["id_categoria"] = e.CommandArgument.ToString();
        gridList.Visible = true;
        LabelCategoria.Visible = true;
        LabelCategoria.Text = "Listagem de itens da categoria " + e.CommandName.ToString();
    }
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
        Resumo.Text = "";
        Descricao.Text = "";
        Destaque.Checked = false;
        Status.SelectedValue = "1";
        PanelIcone.Visible = false;
        HidEquipe.Value = "0";
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
        String r = Resumo.Text;
        String r1 = r.Replace("\\", "/");
        String resumo = r1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        int id_coordenador = 0;
        if (Categoria.SelectedValue == "4")
        {
            id_coordenador = int.Parse(ddl_id_coordenador.SelectedValue.ToString());
        }
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = Textos.nextID;
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActiotextoIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/institucional/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region salva dados
        Textos.Inserir(Categoria.SelectedValue.ToString(), resumo, descricao, Status.SelectedValue, Destaque.Checked ? "1" : "0", titulo, HidIcone.Value, id_coordenador);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + titulo, "institucional");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        PadraoDoEnter(ibt_cancelar);
        Session["id_categoria"] = Categoria.SelectedValue.ToString();
        gridList.DataBind();
        gridList.Visible = true;
        PadraoDoEnter(ibt_cancelar);
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
        DataTable dt = Textos.SelectOne(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        Categoria.SelectedValue = dr["id_tipo"].ToString();
        HidEquipe.Value = dr["id_coordenador"].ToString();
        ddl_id_coordenador.SelectedValue = dr["id_coordenador"].ToString();
        Status.SelectedValue = dr["status"].ToString();
        if (dr["destaque"].ToString() == "1")
            Destaque.Checked = true;
        Titulo.Text = dr["titulo"].ToString();
        Resumo.Text = dr["resumo"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        HidIcone.Value = dr["icone"].ToString();
        string icone = dr["icone"].ToString();
        if(icone == string.Empty)
        {
            rfvIcone.Enabled = true;
            IconePostado.Visible = false;
        }
        if(icone != string.Empty)
        {
            rfvIcone.Enabled = false;
            IconePostado.Visible = true;
            IconePostado.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/institucional/icones/" + dr["icone"].ToString());
        }
        int categoria = int.Parse(dr["id_tipo"].ToString());
        switch (categoria)
        {
            case 4:
                panelEquipe.Visible = true;
                break;
            case 2:
                panelEquipe.Visible = false;
                break;
            case 3:
                panelEquipe.Visible = false;
                break;
            case 5:
                panelEquipe.Visible = false;
                break;
        }
        PanelIcone.Visible = false;
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
        String r = Resumo.Text;
        String r1 = r.Replace("\\", "/");
        String resumo = r1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        int id_coordenador = int.Parse(ddl_id_coordenador.SelectedValue.ToString());
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            #region exclui icones
            try
            {
                string arquivo = HidIcone.Value;
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\institucional\icones\{0}", arquivo));
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
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/institucional/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion

        #endregion
        #region salva dados
        Textos.Update(Session["id"].ToString(), Categoria.SelectedValue, resumo, descricao, Status.SelectedValue, Destaque.Checked ? "1" : "0", titulo, HidIcone.Value, id_coordenador); 
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Atualizou o item " + titulo, "institucional");
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
        #region exclui icones
        try
        {
            DataTable dti = Textos.SelectOne(int.Parse(Session["id"].ToString()));
            DataRow dri = dti.Rows[0];
            string arquivo = dri["icone"].ToString();
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\institucional\icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion
        #region grava histórico
        DataTable dt = Textos.SelectOne(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        string item = dr["titulo"].ToString();
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Exluiu o item " + item, "institucional");
        #endregion
        #region exclui item
        Textos.Excluir(Session["id"].ToString());
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
    #endregion
    #region categorias
    #region aparencia do site
    protected void lbt_categorias_Click(object sender, EventArgs e)
    {
        mvAll.ActiveViewIndex = 2;
    }
    #endregion
    #region comandos da grid de categorias
    public void ComandoGridCategoria(object sender, GridViewCommandEventArgs e)
    {
        Session["id_categoria"] = e.CommandArgument.ToString();
        try
        {
            if (e.CommandName == "Editar")
                EditarCategorias();
            if (e.CommandName == "Excluir")
                ExcluirCategorias();
        }
        catch { }
    }
    #endregion
    #region Nova Categoria
    #region aparencia da página
    protected void ibt_adicionarCategoria_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 3;
        TituloCategoria.Text = "";
        ibt_editarCategoria.Visible = false;
        ibt_salvarCategoria.Visible = true;
        PadraoDoEnter(ibt_salvarCategoria);
    }
    #endregion
    #region salvar categoria
    protected void ibt_salvarCategoria_Click(object sender, ImageClickEventArgs e)
    {
    #region salva dados

        Textos_Categoria.Inserir(TituloCategoria.Text, "sem icone");
        mvAll.ActiveViewIndex = 2;
        GridCategorias.DataBind();
        dtlCategorias.DataBind();
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + TituloCategoria.Text, "institucional - categorias");
        #endregion
    }


    #endregion
    #endregion
    #region Editar Categoria
    #region carrega dados
    public void EditarCategorias()
    {
        DataTable dt = Textos_Categoria.SelectByID(int.Parse(Session["id_categoria"].ToString()));
        DataRow dr = dt.Rows[0];
        TituloCategoria.Text = dr["titulo"].ToString();
        ibt_editarCategoria.Visible = true;
        ibt_salvarCategoria.Visible = false;
        PadraoDoEnter(ibt_editarCategoria);
        mvAll.ActiveViewIndex = 3;
    }
    #endregion
    #region Salva alterações
    protected void ibt_editarCategoria_Click(object sender, ImageClickEventArgs e)
    {
        #region grava dados
        Textos_Categoria.Atualizar(Session["id_categoria"].ToString(), TituloCategoria.Text, "sem icone");
        mvAll.ActiveViewIndex = 2;
        GridCategorias.DataBind();
        dtlCategorias.DataBind();
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o item " + TituloCategoria.Text, "institucional - categorias");
        #endregion
    }
    #endregion
    #endregion
    #region Excluir Categoria
    public void ExcluirCategorias()
    {
        #region grava histórico
        DataTable dt = Textos_Categoria.SelectByID(int.Parse(Session["id_categoria"].ToString()));
        DataRow dr = dt.Rows[0];
        string titulo = dr["titulo"].ToString();
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Excluiu todos os itens e a categoria " + titulo, "institucional - categorias");
        #endregion
        #region grava dados
        Textos.ExcluirByIdTipo(Session["id_categoria"].ToString());
        Textos_Categoria.Delete(int.Parse(Session["id_categoria"].ToString()));
        mvAll.ActiveViewIndex = 2;
        GridCategorias.DataBind();
        dtlCategorias.DataBind();
        #endregion

    }
    #endregion
    #region Cancelar Categoria
    protected void ibt_cancelarCategoria_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 2;
    }
    protected void ibt_retornar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 0;
    }
    #endregion
    #endregion
}