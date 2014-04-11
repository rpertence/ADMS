using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Actio.Negocio;

public partial class adms_videos_Default : System.Web.UI.Page
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
                LabelTituloPagina.Text = "Administração de Videos";
                mvAll.ActiveViewIndex = 0;
            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("11", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Videos";
                    mvAll.ActiveViewIndex = 0;
                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Videos - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3510-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Redes Sociais - Sua credencial não permite a utilização deste painel!";
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
        Codigo.Text = "";
        Descricao.Text = "";

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
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        string descricao = d1.Replace("'", "\\'");
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = Videos.nextID;
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioVideoIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/videos/imagens/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        //#region trata codigo quando destaque
        //string codigo = "";
        //if (chkDestaque.Checked)
        //{
        //    String embed = Codigo.Text;
        //    String e1a = embed.Replace("420", "270");
        //    String e1b = e1a.Replace("480", "270");
        //    String e1c = e1b.Replace("640", "270");
        //    String e1d = e1c.Replace("960", "270");
        //    String e1e = e1d.Replace("560", "270");
        //    String e1f = e1e.Replace("640", "270");
        //    String e1g = e1f.Replace("853", "270");
        //    String e1h = e1g.Replace("1280", "270");
        //    String e2a = e1h.Replace("315", "376");
        //    String e2b = e2a.Replace("360", "377");
        //    String e2c = e2b.Replace("480", "376");
        //    String e2d = e2c.Replace("720", "376");
        //    String e2e = e2d.Replace("315", "205");
        //    String e2f = e2e.Replace("376", "205");
        //    String e2g = e2f.Replace("480", "205");
        //    String e2h = e2g.Replace("720", "205");
        //    codigo = e2h;
        //}
        //if (chkDestaque.Checked == false)
        //{
        //    String embed = Codigo.Text;
        //    String e1a = embed.Replace("420", "270");
        //    String e1b = e1a.Replace("480", "270");
        //    String e1c = e1b.Replace("640", "270");
        //    String e1d = e1c.Replace("960", "270");
        //    String e1e = e1d.Replace("560", "270");
        //    String e1f = e1e.Replace("640", "270");
        //    String e1g = e1f.Replace("853", "270");
        //    String e1h = e1g.Replace("1280", "270");
        //    String e2a = e1h.Replace("315", "376");
        //    String e2b = e2a.Replace("360", "377");
        //    String e2c = e2b.Replace("480", "376");
        //    String e2d = e2c.Replace("720", "376");
        //    String e2e = e2d.Replace("315", "205");
        //    String e2f = e2e.Replace("376", "205");
        //    String e2g = e2f.Replace("480", "205");
        //    String e2h = e2g.Replace("720", "205");
        //    codigo = e2h;
        //}
        //#endregion
        #endregion
        #region salva dados
        Videos.Inserir(titulo, descricao, Codigo.Text, HidIcone.Value, (chkStatus.Checked ? "1" : "0"), (chkDestaque.Checked ? "1" : "0"));

        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + Titulo.Text, "Vídeos");
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
        rfvIcone.Enabled = false;
        #endregion
        #region carrega dados
        DataTable dt = Videos.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        Titulo.Text = dr["titulo"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        Codigo.Text = dr["codigo"].ToString();
        if (dr["status"].ToString() == "1")
            chkStatus.Checked = true;
        if (dr["status"].ToString() == "0")
            chkStatus.Checked = false;
        if (dr["destaque"].ToString() == "1")

            chkDestaque.Checked = true;
        if (dr["destaque"].ToString() == "0")
            chkDestaque.Checked = false;
        HidIcone.Value = dr["icone"].ToString();
        IconePostado.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/videos/imagens/" + HidIcone.Value);
        IconePostado.Visible = true;
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
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        string descricao = d1.Replace("'", "\\'");
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            #region exclui icones
            try
            {
                string arquivo = HidIcone.Value;
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\videos\imagens\{0}", arquivo));
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioVideosIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/videos/imagens/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        //#region trata codigo quando destaque
        //string codigo = "";
        //if (chkDestaque.Checked)
        //{
        //    String embed = Codigo.Text;
        //    String e1a = embed.Replace("420", "270");
        //    String e1b = e1a.Replace("480", "270");
        //    String e1c = e1b.Replace("640", "270");
        //    String e1d = e1c.Replace("960", "270");
        //    String e1e = e1d.Replace("560", "270");
        //    String e1f = e1e.Replace("640", "270");
        //    String e1g = e1f.Replace("853", "270");
        //    String e1h = e1g.Replace("1280", "270");
        //    String e2a = e1h.Replace("315", "376");
        //    String e2b = e2a.Replace("360", "377");
        //    String e2c = e2b.Replace("480", "376");
        //    String e2d = e2c.Replace("720", "376");
        //    String e2e = e2d.Replace("315", "205");
        //    String e2f = e2e.Replace("376", "205");
        //    String e2g = e2f.Replace("480", "205");
        //    String e2h = e2g.Replace("720", "205");
        //    codigo = e2h;
        //}
        //if (chkDestaque.Checked == false)
        //{
        //    String embed = Codigo.Text;
        //    String e1a = embed.Replace("420", "640");
        //    String e1b = e1a.Replace("480", "640");
        //    String e1c = e1b.Replace("290", "640");
        //    String e1d = e1c.Replace("960", "640");
        //    String e2a = e1d.Replace("345", "510");
        //    String e2b = e2a.Replace("390", "510");
        //    String e2c = e2b.Replace("242", "510");
        //    String e2d = e2c.Replace("750", "510");
        //    String e2e = e2d.Replace("510", "640");
        //    String e2f = e2e.Replace("268", "510");
        //    codigo = e2f;
        //}
        //#endregion
        #endregion
        #region salva dados
        Videos.Update(Session["id"].ToString(), titulo, descricao, Codigo.Text, HidIcone.Value, (chkStatus.Checked ? "1" : "0"), (chkDestaque.Checked ? "1" : "0"));
        
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o item " + Titulo.Text, "Vídeos");
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
        DataTable dt = Videos.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        string titulo = dr["titulo"].ToString();
        string imagem = dr["icone"].ToString();
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Excluiu o item " + titulo, "Videos");
        #endregion
        #region exclui item
        Videos.Delete(int.Parse(Session["id"].ToString()));
        #endregion
        #region exclui icones
        try
        {
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\videos\imagens\{0}", imagem));
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