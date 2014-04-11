using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Actio.Negocio;
using System.IO;

public partial class ActioAdms_noticias_Default : System.Web.UI.Page
{
    #region aparência da página ao carregar
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
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
                LabelTituloPagina.Text = "Administração de Notícias";
                mvAll.ActiveViewIndex = 0;
            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("13", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Notícias";
                    mvAll.ActiveViewIndex = 0;
                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de TNotícias - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração Notícias - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
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
        Destaque.SelectedIndex = 0;
        Status.SelectedValue = "1";
        Ordem.Text = "";
        Data.Text = "";
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
        #endregion
        #region trata tipo de destaque
        int distak = int.Parse(Destaque.SelectedValue);
        string destaque = "0";
        string destaque_b = "0";
        switch (distak)
        {
            case 0:
                destaque = "0";
                destaque_b = "0";
                break;
            case 1:
                destaque = "1";
                destaque_b = "0";
                break;
            case 2:
                destaque = "0";
                destaque_b = "1";
                break;
        }
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = Usuario.nextID;

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioNoticiaIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/noticias/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #region Miniatura postada
        if (Miniatura.FileName != string.Empty)
        {
            string extensao = (Miniatura.PostedFile.FileName.Split('.'))[1];
            int nextID = Usuario.nextID;

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioNoticiaMiniatura"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/noticias/miniaturas/{0}", nomeArquivo));
            Miniatura.PostedFile.SaveAs(enderecoCompleto);
            HidMiniatura.Value = nomeArquivo;
        }
        else 
        {
            HidMiniatura.Value = "miniatura_noticia.jpg";
        }
        #endregion

        #endregion
        #region salva dados
        Noticias.Inserir(titulo, resumo, descricao, Data.Text, Ordem.Text, HidMiniatura.Value, destaque, Status.SelectedValue, HidIcone.Value, destaque_b);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + Titulo.Text, "noticias");
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
        Destaque.SelectedIndex = 0;
        #endregion
        #region carrega dados
        DataTable dt = Noticias.Noticia(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        Status.SelectedValue = dr["status"].ToString();
        if (dr["destaque"].ToString() == "1")
            Destaque.SelectedValue = "1";
        if (dr["destaque_b"].ToString() == "1")
            Destaque.SelectedValue = "2";
        Ordem.Text = dr["ordem"].ToString();
        Data.Text = dr["data"].ToString();
        Titulo.Text = dr["titulo"].ToString();
        Resumo.Text = dr["resumo"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        HidIcone.Value = dr["icone"].ToString();
        HidMiniatura.Value = dr["miniatura"].ToString();
        LabelContagem.Text = "esta notícia recebeu: " + dr["visitas"].ToString();
        if (dr["icone"].ToString() != string.Empty)
        {
            IconePostado.Visible = true;
            IconePostado.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/noticias/icones/" + dr["icone"].ToString());
        }
        MiniaturaPostada.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/noticias/miniaturas/" + dr["miniatura"].ToString());
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
        #endregion
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id"].ToString());
            #region exclui icones
            try
            {
                string arquivo = HidIcone.Value;
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\noticias\icones\{0}", arquivo));
                File.Delete(deletefile);
            }
            catch
            {

            }
            #endregion
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioMiniaturaIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/noticias/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #region Miniatura postada
        if (Miniatura.FileName != string.Empty)
        {
            string extensao = (Miniatura.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id"].ToString());
            #region exclui icones
            try
            {
                if (HidMiniatura.Value != "miniatura_noticia.jpg")
                {
                    string arquivo = HidMiniatura.Value;
                    string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\noticias\miniaturas\{0}", arquivo));
                    File.Delete(deletefile);
                }
            }
            catch
            {

            }
            #endregion
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioNoticiaMiniatura"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/noticias/miniaturas/{0}", nomeArquivo));
            Miniatura.PostedFile.SaveAs(enderecoCompleto);
            HidMiniatura.Value = nomeArquivo;
        }
        #endregion

        #region trata tipo de destaque
        int distak = int.Parse(Destaque.SelectedValue);
        string destaque = "0";
        string destaque_b = "0";
        switch (distak)
        {
            case 0:
                destaque = "0";
                destaque_b = "0";
                break;
            case 1:
                destaque = "1";
                destaque_b = "0";
                break;
            case 2:
                destaque = "0";
                destaque_b = "1";
                break;
        }
        #endregion
        #endregion
        #region salva dados
        Noticias.Atualizar(Session["id"].ToString(), titulo, resumo, descricao, Data.Text, Ordem.Text, HidMiniatura.Value, destaque, Status.SelectedValue, HidIcone.Value, destaque_b);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Atualizou o item " + titulo, "notícias");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        IconePostado.Visible = false;
        PadraoDoEnter(ibt_cancelar);
        #endregion
    }
    #endregion
    #endregion
    #region excluir
    public void Excluir()
    {
        #region grava histórico
        DataTable dt = Noticias.Noticia(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        string item = dr["titulo"].ToString();
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Exluiu o item " + item, "notícias");
        #endregion
        #region exclui icones
        try
        {
            string arquivo = dr["icone"].ToString();
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\noticias\icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion
        #region exclui miniatura
        try
        {
            string arquivo = dr["miniatura"].ToString();
            if (arquivo != "miniatura_noticia.jpg")
            {
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\noticias\miniaturas\{0}", arquivo));
                File.Delete(deletefile);
            }
        }
        catch
        {

        }
        #endregion

        #region exclui item
        Noticias.Excluir(int.Parse(Session["id"].ToString()));
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        Session.Clear();
        gridList.DataBind();
        #endregion
    }
    #endregion
    #region cancelar edição
    protected void ibt_cancelar_Click(object sender, ImageClickEventArgs e)
    {
        IconePostado.Visible = false;
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
    }
    #endregion
    #endregion
    #region trataescolha de noticia em destaque
    protected void Destaque_SelectedIndexChanged(object sender, EventArgs e)
    {
        int valor = int.Parse(Destaque.SelectedValue.ToString());
        if (valor == 1 & HidIcone.Value == null)
        {
            RFVIcone.Enabled = false;
        }
        else { RFVIcone.Enabled = false; }
    }
    #endregion
}