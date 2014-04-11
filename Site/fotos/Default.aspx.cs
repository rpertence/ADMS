using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Actio.Negocio;

public partial class adms_fotos_Default : System.Web.UI.Page
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
                LabelTituloPagina.Text = "Administração de Fotos";
                mvAll.ActiveViewIndex = 0;
                lbt_albuns.Visible = true;
            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("10", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Fotos";
                    mvAll.ActiveViewIndex = 0;
                    lbt_albuns.Visible = false;
                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Fotos - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Fotos - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
            }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #endregion
    #region Albuns de Fotos
    #region Comando da listagem de Categorias
    public void ComandoTitulosalbuns(object sender, DataListCommandEventArgs e)
    {
        try
        {
            Session["id_categoria"] = e.CommandArgument.ToString();
            gridList.Visible = true;
            LabelCategoria.Visible = true;
            LabelCategoria.Text = "Listagem de itens da categoria " + e.CommandName.ToString();
        }
        catch { }
    }
    #endregion
    #region adicionar novo item
    #region aparencia da página
    protected void ibt_adicionar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 1;
        ibt_salvar.Visible = true;
        ibt_editar.Visible = false;
        rfvIcone.Enabled = true;
        PadraoDoEnter(ibt_salvar);
        Titulo.Text = "";
        Resumo.Text = "";
        Descricao.Text = "";
        Destaque.Checked = false;
        Status.SelectedValue = "1";
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
            int nextID = Foto_Album.nextID;
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioAlbumIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/foto_album/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #region cria diretório para as fotos
        string path = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\foto_album\{0}", Session["nextID"].ToString()));
        try
        {
            // Determine whether the directory exists.
            if (!Directory.Exists(path))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(path);
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('OP´S! Não foi possível criar o diretório!');window.location.src = window.location.src;", true);
        }
        finally { }
        #endregion
        #endregion
        #region salva dados
        Foto_Album.Inserir(Categoria.SelectedValue.ToString(), resumo, descricao, Status.SelectedValue, Destaque.Checked ? "1" : "0", titulo, HidIcone.Value);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + titulo, "fotos - álbum");
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
        hidAlbumid.Value = e.CommandArgument.ToString();
        try
        {
            if (e.CommandName.ToString() == "Editar")
                Carregar();
            if (e.CommandName.ToString() == "Excluir")
                Excluir();
            if (e.CommandName.ToString() == "Fotos")
                Fotos();
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
        IconePostado.Visible = true;
        #endregion
        #region carrega dados
        DataTable dt = Foto_Album.SelectOne(int.Parse(hidAlbumid.Value));
        DataRow dr = dt.Rows[0];
        Categoria.SelectedValue = dr["id_tipo"].ToString();
        Status.SelectedValue = dr["status"].ToString();
        if (dr["destaque"].ToString() == "1")
            Destaque.Checked = true;
        Titulo.Text = dr["titulo"].ToString();
        Resumo.Text = dr["resumo"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        IconePostado.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/foto_album/icones/" + dr["icone"].ToString());
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
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\foto_album\icones\{0}", arquivo));
                File.Delete(deletefile);
            }
            catch
            {

            }
            #endregion

            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(hidAlbumid.Value);

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioAlbumIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/foto_album/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region salva dados
        Foto_Album.Update(hidAlbumid.Value, Categoria.SelectedValue, resumo, descricao, Status.SelectedValue, Destaque.Checked ? "1" : "0", titulo, HidIcone.Value);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o item " + titulo, "fotos - álbum");
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
        #region grava histórico
        DataTable dt = Foto_Album.SelectOne(int.Parse(hidAlbumid.Value));
        DataRow dr = dt.Rows[0];
        string item = dr["titulo"].ToString();
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Exluiu o item " + item, "fotos - álbum");
        #endregion
        #region apaga diretorio de fotos
        string target = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\foto_album\" + hidAlbumid.Value));
        if (Directory.Exists(target))
        {
            // Delete the target to ensure it is not there.
            Directory.Delete(target, true);
        }
        #endregion
        #region exclui item
        Foto_Album.Excluir(hidAlbumid.Value);
        #endregion
        #region apaga fotos da base
        Foto.DeleteByIdAlbum(int.Parse(hidAlbumid.Value));
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
    #region fotos do álbum
    #region listagem das fotos
    public void Fotos()
    {
        DataTable dt = Foto_Album.SelectOne(int.Parse(hidAlbumid.Value));
        DataRow dr = dt.Rows[0];
        Session["NomedoAlbum"] = dr["titulo"].ToString();
        LabelTituloAlbum.Text = Session["NomedoAlbum"].ToString();
        hidAlbumid.Value = hidAlbumid.Value;

        string path = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\albuns\albuns\{0}", hidAlbumid.Value));
        string caminho = string.Format("~/App_Themes/ActioAdms/hd/albuns/" + hidAlbumid.Value);
        try
        {
            // Determine whether the directory exists.
            if (!Directory.Exists(path))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(path);
            }
        }
        catch (Exception e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('OP´S! Não foi possível criar o diretório!');window.location.src = window.location.src;", true);
        }
        mvAll.ActiveViewIndex = 4;
        PanelEdit.Visible = false;
        PanelListFotos.Visible = true;
        GridFotos.DataBind();
    }
    #endregion
    #region nova foto
    #region aparencia da página
    protected void ibt_novaFoto_Click(object sender, ImageClickEventArgs e)
    {
        PanelEdit.Visible = true;
        PanelListFotos.Visible = false;
        TituloFoto.Text = "";
        Ordem.Text = "";
        IconePostadoFotoMiniatura.Visible = false;
        IconePostadoFoto.Visible = false;
        rfvIconeFoto.Enabled = true;
        rfvIconeFoto0.Enabled = true;
        ibt_editarFoto.Visible = false;
        ibt_salvarFoto.Visible = true;
        PadraoDoEnter(ibt_salvarFoto);
    }
    #endregion
    #region adicionar foto
    protected void ibt_salvarFoto_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = TituloFoto.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        #endregion
        #region Miniatura postada
        if (fpMiniatura.FileName != string.Empty)
        {
            string extensao = (fpMiniatura.PostedFile.FileName.Split('.'))[1];
            int nextID = Foto.nextID;
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioFotoMin"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/albuns/" + hidAlbumid.Value + "/" + nomeArquivo));
            fpMiniatura.PostedFile.SaveAs(enderecoCompleto);
            HidMiniatura.Value = nomeArquivo;
        }
        #endregion

        #region Foto postada
        if (IconeFoto.FileName != string.Empty)
        {
            string extensao = (IconeFoto.PostedFile.FileName.Split('.'))[1];
            int nextID = Foto.nextID;
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioFoto"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/albuns/" + hidAlbumid.Value + "/" + nomeArquivo));
            IconeFoto.PostedFile.SaveAs(enderecoCompleto);
            HidIconeFoto.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region salva dados
        Foto.Inserir(hidAlbumid.Value, titulo, HidIconeFoto.Value, HidMiniatura.Value, Ordem.Text, "0");
        PanelEdit.Visible = false;
        PanelListFotos.Visible = true;
        GridFotos.DataBind();
        PadraoDoEnter(ibt_cancelarFoto);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu a foto " + titulo, "fotos");
        #endregion
    }
    #endregion
    #endregion
    #region Comandos da Grid de foto
    public void EditarFoto(Object sender, GridViewCommandEventArgs e)
    {
        try
        {        
            Session["id_foto"] = e.CommandArgument.ToString();
            if (e.CommandName == "Editar")
            {
                CarregaFoto();
            }
            if (e.CommandName == "Excluir")
            {
                ExcluiFoto();
            }
        }
        catch { }
    }
    #endregion
    #region editar uma foto
    #region carrega dados
    public void CarregaFoto()
    {
        #region dados
        DataTable dt = Foto.SelectByID(int.Parse(Session["id_foto"].ToString()));
        DataRow dr = dt.Rows[0];
        TituloFoto.Text = dr["titulo"].ToString();
        Ordem.Text = dr["ordem"].ToString();
        HidMiniatura.Value = dr["miniatura"].ToString();
        HidIconeFoto.Value = dr["arquivo"].ToString();
        IconePostadoFoto.Visible = true;
        IconePostadoFoto.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/albuns/" + hidAlbumid.Value + "/" + dr["arquivo"].ToString());
        IconePostadoFotoMiniatura.Visible = true;
        IconePostadoFotoMiniatura.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/albuns/" + hidAlbumid.Value + "/" + dr["miniatura"].ToString());
        rfvIconeFoto.Enabled = false;
        rfvIconeFoto0.Enabled = false;
        #endregion
        #region aparencia da página
        PanelEdit.Visible = true;
        PanelListFotos.Visible = false;
        ibt_editarFoto.Visible = true;
        ibt_salvarFoto.Visible = false;
        PadraoDoEnter(ibt_editarFoto);
        #endregion
    }
    #endregion
    #region salva alterações
    protected void ibt_editarFoto_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = TituloFoto.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        #endregion
        #region Miniatura postada
        if (fpMiniatura.FileName != string.Empty)
        {
            #region exclui Miniatura
            try
            {
                string file = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\albuns\" + hidAlbumid.Value + "\\" + HidMiniatura.Value));
                File.Delete(file);
            }
            catch { }
            #endregion

            string extensao = (fpMiniatura.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id_foto"].ToString());
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioFotoMin"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/albuns/" + hidAlbumid.Value + "/" + nomeArquivo));
            fpMiniatura.PostedFile.SaveAs(enderecoCompleto);
            HidMiniatura.Value = nomeArquivo;
        }
        #endregion
        #region Foto postada
        if (IconeFoto.FileName != string.Empty)
        {
            #region exclui foto atual
            string file = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\albuns\" + hidAlbumid.Value + "\\" + HidIconeFoto.Value));
            File.Delete(file);
            #endregion

            string extensao = (IconeFoto.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id_foto"].ToString());
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioFoto"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/albuns/" + hidAlbumid.Value + "/" + nomeArquivo));
            IconeFoto.PostedFile.SaveAs(enderecoCompleto);
            HidIconeFoto.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region grava dados
        Foto.Atualizar(Session["id_foto"].ToString(), hidAlbumid.Value, titulo, HidIconeFoto.Value, HidMiniatura.Value, Ordem.Text, "0");
        PanelEdit.Visible = false;
        PanelListFotos.Visible = true;
        GridFotos.DataBind();
        PadraoDoEnter(ibt_cancelarFoto);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou a foto " + titulo, "foto");
        #endregion
    }
    #endregion
    #endregion
    #region excluir foto
    public void ExcluiFoto()
    {
        #region exclui Arquivos físicos
        #region seleciona dados dos arquivos para exclusão
        string t = Session["id_foto"].ToString();
        DataTable dt = Foto.SelectByID(int.Parse(Session["id_foto"].ToString()));
        DataRow dr = dt.Rows[0];
        string miniatura = dr["miniatura"].ToString();
        string foto = dr["arquivo"].ToString();
        #endregion
        #region exlui miniatura
        try
        {
            string file = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\albuns\" + hidAlbumid.Value + "\\" + miniatura));
            File.Delete(file);
        }
        catch
        { }
        #endregion
        #region exlui foto
        try
        {
            string file = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\albuns\" + hidAlbumid.Value + "\\" + foto));
            File.Delete(file);
        }
        catch
        { }
        #endregion
        #endregion
        #region exclui registro
        Foto.Delete(int.Parse(Session["id_foto"].ToString()));
        #endregion
        #region aparencia da pagina
        GridFotos.DataBind();
        PadraoDoEnter(ibt_cancelarFoto);
        #endregion

    }
    #endregion
    #region cancelar foto
    protected void ibt_cancelarFoto_Click(object sender, ImageClickEventArgs e)
    {
        PanelEdit.Visible = false;
        PanelListFotos.Visible = true;
        GridFotos.DataBind();
        PadraoDoEnter(ibt_cancelarFoto);
    }
    #endregion
    #endregion
    #region Categorias
    #region aparencia do site
    protected void lbt_albuns_Click(object sender, EventArgs e)
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
                Editaralbuns();
            if (e.CommandName == "Excluir")
                Excluiralbuns();
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
        DescricaoCategoria.Text = "";
        rfvIconeCategoria.Enabled = true;
        IconePostadoCategoria.Visible = false;
        ibt_editarCategoria.Visible = false;
        ibt_salvarCategoria.Visible = true;
        PadraoDoEnter(ibt_salvarCategoria);
    }
    #endregion
    #region salvar categoria
    protected void ibt_salvarCategoria_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = TituloCategoria.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        String d = DescricaoCategoria.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #region icone postado
        if (IconeCategoria.FileName != string.Empty)
        {
            string extensao = (IconeCategoria.PostedFile.FileName.Split('.'))[1];
            int nextID = Foto_Categoria.nextID;
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioCatIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/foto_album/albuns/{0}", nomeArquivo));
            IconeCategoria.PostedFile.SaveAs(enderecoCompleto);
            HidIconeCategoria.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region salva dados
        Foto_Categoria.Inserir(titulo, HidIconeCategoria.Value, descricao);
        mvAll.ActiveViewIndex = 2;
        Gridalbuns.DataBind();
        dtlalbuns.DataBind();
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + titulo, "fotos - albuns");
        #endregion
    }
    #endregion
    #endregion
    #region Editar Categoria
    #region carrega dados
    public void Editaralbuns()
    {
        DataTable dt = Foto_Categoria.SelectByID(int.Parse(Session["id_categoria"].ToString()));
        DataRow dr = dt.Rows[0];
        TituloCategoria.Text = dr["titulo"].ToString();
        DescricaoCategoria.Text = dr["descricao"].ToString();
        IconePostadoCategoria.Visible = true;
        IconePostadoCategoria.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/foto_album/albuns/" + dr["icone"].ToString());
        HidIconeCategoria.Value = dr["icone"].ToString();
        rfvIconeCategoria.Enabled = false;
        ibt_editarCategoria.Visible = true;
        ibt_salvarCategoria.Visible = false;
        PadraoDoEnter(ibt_editarCategoria);
        mvAll.ActiveViewIndex = 3;
    }
    #endregion
    #region Salva alterações
    protected void ibt_editarCategoria_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region trata strings importantes
        String t = TituloCategoria.Text;
        String t1 = t.Replace("\\", "/");
        String titulo = t1.Replace("'", "\\'");
        String d = DescricaoCategoria.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #region icone postado
        if (IconeCategoria.FileName != string.Empty)
        {
            #region exclui icones
            string file = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\foto_album\albuns\{0}", HidIconeCategoria.Value));
            File.Delete(file);
            #endregion

            string extensao = (IconeCategoria.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id_categoria"].ToString());
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioCatIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/foto_album/albuns/{0}", nomeArquivo));
            IconeCategoria.PostedFile.SaveAs(enderecoCompleto);
            HidIconeCategoria.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region grava dados
        Foto_Categoria.Atualizar(Session["id_categoria"].ToString(), titulo, HidIconeCategoria.Value, descricao);
        mvAll.ActiveViewIndex = 2;
        Gridalbuns.DataBind();
        dtlalbuns.DataBind();
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o item " + titulo, "fotos - albuns");
        #endregion
    }
    #endregion
    #endregion
    #region Excluir Categoria
    public void Excluiralbuns()
    {
        #region concatena dados para exclusões
        int id_categoria = int.Parse(Session["id_categoria"].ToString());
        DataTable dt = Foto_Categoria.SelectByID(id_categoria);
        DataRow dr = dt.Rows[0];
        string titulo = dr["titulo"].ToString();

        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        #region apaga icones dos albusn e diretorios de fotos
        DataTable dta = Foto_Album.selectAllByTipo(id_categoria.ToString());
        foreach (DataRow dra in dta.Rows)
        {
            #region exclui icones dos albuns
            try
            {
                string arquivo = dra["icone"].ToString();
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\foto_album\icones\{0}", arquivo));
                File.Delete(deletefile);
            }
            catch
            {

            }
            #endregion
            #region apaga diretorio de fotos
            string target = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\foto_album\" + dra["id"].ToString()));
            if (Directory.Exists(target))
            {
                // Delete the target to ensure it is not there.
                Directory.Delete(target, true);
            }
            #endregion
        }
        #endregion
        #endregion
        #region grava histórico
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Excluiu todos os itens e a categoria " + titulo, "fotos - albuns");
        #endregion
        #region apaga foto_albuns
        Foto_Album.ExcluirByIdTipo(id_categoria.ToString());
        #endregion
        #region apaga categoria
        Foto_Categoria.Delete(id_categoria);
#endregion
        #region aparencia do site
        mvAll.ActiveViewIndex = 2;
        Gridalbuns.DataBind();
        dtlalbuns.DataBind();
        gridList.DataBind();
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