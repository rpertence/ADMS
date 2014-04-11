#region referencias
using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net;
using System.Net.Mail;
using System.Text;
using Actio.Negocio;
#endregion
#region página
public partial class ActioAdms_LojaVirtual_Default : System.Web.UI.Page
{
    #region aparencia do site ao carregar
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
                LabelTituloPagina.Text = "Administração da Loja Virtual";
                mvProdutos.ActiveViewIndex = 0;
                GridProdutos.DataBind();
                gridPedidos.DataBind();
                GridClientes.DataBind();
                gridListCategorias.DataBind();

            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("18", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração da Loja Virtual";
                    mvProdutos.ActiveViewIndex = 0;
                    GridProdutos.DataBind();
                    gridPedidos.DataBind();
                    GridClientes.DataBind();
                    gridListCategorias.DataBind();
                }
                catch
                {
                    mvProdutos.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Artigos e Autores - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvProdutos.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração da Loja Virtual  - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
            }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #endregion
    #region Produtos
    #region listagem de produtos
    protected void ibt_listagemProdutos_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 0;
        GridProdutos.DataBind();
        LabelTitulo.Text = "Listagem de Produtos";

    }
    protected void lk_List_Click(object sender, EventArgs e)
    {
        mvProdutos.ActiveViewIndex = 0;
        GridProdutos.DataBind();
        LabelTitulo.Text = "Listagem de Produtos";
    }
    #endregion
    #region mudar de categoria no cadastro de produtos
    public void MudarDeCategoria(object sender, EventArgs e)
    {
        DataTable dt = Produtos_Categoria.SelectByID(int.Parse(ddlCategoria.SelectedValue));
        DataRow dr = dt.Rows[0];
        ImageCategoria.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/produtos/categorias/" + dr["icone"].ToString());
        rdb_SubCategorias.DataBind();
    }
    #endregion
    #region Novo Produto
    #region aparência da página
    protected void bt_NovoProduto_Click(object sender, EventArgs e)
    {
        #region aparencia do site
        mvProdutos.ActiveViewIndex = 1;
        LabelTitulo.Text = " >> Você está em Produtos > Cadastrar Novo Produto";
        ddlCategoria.DataBind();
        rdb_SubCategorias.DataBind();
        Salvar.Visible = true;
        Atualizar.Visible = false;
        #endregion
        #region limpa campos
        Estoque.Text = "";
        ProdDescricao_.Text = "";
        ProdValor_.Text = "";
        Peso.Text = "";
        Extras.Text = "";
        HidIconeProduto.Value = "";
        Resumo.Text = "";
        #endregion
    }
    #endregion
    #region salvar o novo produto
    protected void Salvar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region icone postado
        if (IconeProduto.FileName != string.Empty)
        {
            string extensao = (IconeProduto.PostedFile.FileName.Split('.'))[1];
            int nextID = Produtos.nextID;

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioProdutoIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/produtos/icones/{0}", nomeArquivo));
            IconeProduto.PostedFile.SaveAs(enderecoCompleto);
            HidIconeProduto.Value = nomeArquivo;
        }
        else
        {
            int nextID = Produtos.nextID;
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

            string nomeArquivo = string.Format("{0}_{1}.jpg", nextID.ToString("ActioIconePadrao"), newresult);
            File.Copy(Server.MapPath(@"~\App_Themes\Site\ImagesSuporte\Mini_logo.gif"), Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\icones\{0}", nomeArquivo)));
            HidIconeProduto.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region grava dados
        Produtos.Novo(ddlCategoria.SelectedValue, rdb_SubCategorias.SelectedValue, Estoque.Text, CheckStatus.Checked ? "1" : "0", CheckDestaque.Checked ? "1" : "0", Resumo.Text, ProdDescricao_.Text, ProdValor_.Text, "CBR", "loja@rccbh.com.br", "BRL", Peso.Text, Extras.Text, HidIconeProduto.Value,  int.Parse(ddlMarca.SelectedValue));
        #endregion
        #region comportamento da página
        mvProdutos.ActiveViewIndex = 0;
        GridProdutos.DataBind();
        LabelTitulo.Text = "Listagem de produtos cadastrados";
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item inserido com sucesso!');window.location.src = window.location.src;", true);
        #endregion
    }
    #endregion
    #endregion
    #region comandos da grid de produtos
    public void ListProdutosRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int id = int.Parse(e.CommandArgument.ToString());
            Session["id"] = id;
            if (e.CommandName == "Alterar")
                CarregaProduto();
            if (e.CommandName == "Excluir")
                DeletaProduto();
            if (e.CommandName == "Fotos")
                FotosdoProduto();
            if (e.CommandName == "Descricao")
                Descricao();
        }
        catch { }
    }
    #endregion
    #region alterar um produto
    #region carrega dados e aparencia do site
    public void CarregaProduto()
    {
        #region dados
        Salvar.Visible = false;
        Atualizar.Visible = true;

        DataTable dt = Produtos.SelectById(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        Estoque.Text = dr["estoque"].ToString();
        CheckStatus.Checked = (dr["status"].ToString() == "1");
        CheckDestaque.Checked = (dr["destaque"].ToString() == "1");
        Resumo.Text = dr["resumo"].ToString();
        ProdDescricao_.Text = dr["ProdDescricao_"].ToString();
        ProdValor_.Text = dr["ProdValor_"].ToString();
        Peso.Text = dr["peso"].ToString();
        Extras.Text = dr["extras"].ToString();
        HidIconeProduto.Value = dr["icone"].ToString();
        ImageSelecionadaProduto.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/produtos/icones/" + dr["icone"].ToString());
        #endregion
        #region aparencia do site
        mvProdutos.ActiveViewIndex = 1;
        LabelTitulo.Text = " >> Edição detalhada do produto: " + dr["ProdDescricao_"].ToString();
        ddlCategoria.DataBind();
        ddlCategoria.SelectedValue = dr["id_categoria"].ToString();

        rdb_SubCategorias.DataBind();
        rdb_SubCategorias.SelectedValue = dr["id_subcategoria"].ToString();

        #endregion
    }
    #endregion
    #region salvar alterações do produto
    protected void Atualizar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatena dados
        #region icone postado
        if (IconeProduto.FileName != string.Empty)
        {
            #region exclui icones
            try
            {
                int id = int.Parse(Session["id"].ToString());
                DataTable dt = Produtos.SelectById(id);
                DataRow dr = dt.Rows[0];
                string arquivo = dr["icone"].ToString();
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\icones\{0}", arquivo));
                File.Delete(deletefile);
            }
            catch
            {

            }
            #endregion

            string extensao = (IconeProduto.PostedFile.FileName.Split('.'))[1];
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioProdutoIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/produtos/icones/{0}", nomeArquivo));
            IconeProduto.PostedFile.SaveAs(enderecoCompleto);
            HidIconeProduto.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region grava dados
        Produtos.Update
            (
            Session["id"].ToString(),
            ddlCategoria.SelectedValue,
            rdb_SubCategorias.SelectedValue,
            Estoque.Text,
            CheckStatus.Checked ? "1" : "0",
            CheckDestaque.Checked ? "1" : "0",
            Resumo.Text,
            ProdDescricao_.Text,
            ProdValor_.Text,
            "CBR",
            "loja@rccbh.com.br",
            "BRL",
            Peso.Text,
            Extras.Text,
            HidIconeProduto.Value,
            int.Parse(ddlMarca.SelectedValue)
            );
        #endregion
        #region comportamento da página
        mvProdutos.ActiveViewIndex = 0;
        GridProdutos.DataBind();
        LabelTitulo.Text = "Listagem de produtos cadastrados";
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
        #endregion
    }
    #endregion
    #endregion
    #region excluir um produto
    public void DeletaProduto()
    {
        #region exclui icones
        try
        {
            int id = int.Parse(Session["id"].ToString());
            DataTable dt = Produtos.SelectById(id);
            DataRow dr = dt.Rows[0];
            string arquivo = dr["icone"].ToString();
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion
        #region exclui produto da base
        Produtos.Excluir(Session["id"].ToString());
        #endregion
        #region comportamento da página
        mvProdutos.ActiveViewIndex = 0;
        GridProdutos.DataBind();
        LabelTitulo.Text = "Listagem de produtos cadastrados";
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item excluido com sucesso!');window.location.src = window.location.src;", true);
        #endregion

    }
    #endregion
    #region fotos do produto
    #region Carrega fotos do produto
    public void FotosdoProduto()
    {
        #region aparencia da página
        mvProdutos.ActiveViewIndex = 5;
        TituloAnexo.Text = "";
        OrdemFoto.Text = "";
        #endregion
        #region carrega dados
        DataTable dt = Produtos.SelectById(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        LabelTitulo.Text = "Detalhes e Álbum de fotos do produto: " + dr["ProdDescricao_"].ToString();
        HidDono.Value = dr["id"].ToString();

        #endregion
        #region trata diretório de fotos
        string path = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\album\{0}", Session["id"].ToString()));
        try
        {
            // Determine whether the directory exists.
            if (!Directory.Exists(path))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(path);
            }
        }
        catch
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('OP´S! Não foi possível criar o diretório!');window.location.src = window.location.src;", true);
        }
        finally { }

        #endregion
    }
    #endregion
    #region nova foto
    #region aparencia da página
    protected void ibtNovoAnexo_Click(object sender, ImageClickEventArgs e)
    {
        PanelEditAnexo.Visible = true;
        ibtAtualizarAnexo.Visible = false;
        ibtSalvarAnexo.Visible = true;
        FotoSelecionada.Visible = false;
        TituloAnexo.Text = "";
        OrdemFoto.Text = "";
    }
    protected void lbtNovoAnexo_Click(object sender, EventArgs e)
    {
        PanelEditAnexo.Visible = true;
        ibtAtualizarAnexo.Visible = false;
        ibtSalvarAnexo.Visible = true;
        FotoSelecionada.Visible = false;
        TituloAnexo.Text = "";
        OrdemFoto.Text = "";
    }
    #endregion
    #region salvar novo anexo
    protected void ibtSalvarAnexo_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region anexo postado
        if (Anexo.FileName != string.Empty)
        {
            string extensao = (Anexo.PostedFile.FileName.Split('.'))[1];
            int nextID = Produtos_Fotos.nextID;

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioFotoProduto"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/produtos/album/{0}/{1}", HidDono.Value, nomeArquivo));
            Anexo.PostedFile.SaveAs(enderecoCompleto);
            HidAnexo.Value = nomeArquivo;
        }
        #endregion
        string titulo = TituloAnexo.Text;
        int dono = int.Parse(HidDono.Value);
        string arquivo = HidAnexo.Value;
        string ordem = OrdemFoto.Text;

        TituloAnexo.Text = "";
        #endregion
        #region salva e comportamento da página
        Produtos_Fotos.Inserir(dono, titulo, arquivo, ordem);
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item incluido com sucesso!');window.location.src = window.location.src;", true);
        PanelEditAnexo.Visible = false;
        GridProdutos_Fotos.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region lista de fotos
    protected void ibtListAnexo_Click(object sender, ImageClickEventArgs e)
    {
        PanelEditAnexo.Visible = false;
        GridProdutos_Fotos.DataBind();
    }
    protected void lbtListAnexos_Click(object sender, EventArgs e)
    {
        PanelEditAnexo.Visible = false;
        GridProdutos_Fotos.DataBind();
    }
    #endregion
    #region comandos da grid fotos
    protected void GridAnexosRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int fotos_id = int.Parse(e.CommandArgument.ToString());
        Session["fotos_id"] = fotos_id;

        if (e.CommandName == "Alterar")
            CarregaAnexo();
        if (e.CommandName == "Excluir")
            ExcluirAnexo();
    }

    #endregion
    #region alterar uma foto
    #region carrega foto
    public void CarregaAnexo()
    {
        #region aparencia da página
        PanelEditAnexo.Visible = true;
        ibtSalvarAnexo.Visible = false;
        ibtAtualizarAnexo.Visible = true;
        RequiredFieldValidator11.Enabled = false;
        FotoSelecionada.Visible = true;
        #endregion

        DataTable dt = Produtos_Fotos.UmaFoto(Session["fotos_id"].ToString());
        DataRow dr = dt.Rows[0];

        TituloAnexo.Text = dr["titulo"].ToString();
        HidAnexo.Value = dr["arquivo"].ToString();
        HidDono.Value = dr["id_produto"].ToString();
        OrdemFoto.Text = dr["ordem"].ToString();
        FotoSelecionada.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/produtos/album/{0}/{1}", dr["id_produto"].ToString(), dr["arquivo"].ToString());


    }
    #endregion
    #region atualizar foto
    protected void ibtAtualizarAnexo_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region anexo postado
        if (Anexo.FileName != string.Empty)
        {
            #region exclui anexos
            try
            {
                DataTable dt = Produtos_Fotos.UmaFoto(Session["fotos_id"].ToString());
                DataRow dr = dt.Rows[0];
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\album\{0}\{1}", HidDono.Value, dr["arquivo"].ToString()));
                File.Delete(deletefile);
            }
            catch
            {
            }
            #endregion

            string extensao = (Anexo.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["fotos_id"].ToString());

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioFotoProduto"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/produtos/album/{0}/{1}", HidDono.Value, nomeArquivo));
            Anexo.PostedFile.SaveAs(enderecoCompleto);
            HidAnexo.Value = nomeArquivo;
        }
        #endregion
        string titulo = TituloAnexo.Text;
        int dono = int.Parse(HidDono.Value);
        string arquivo = HidAnexo.Value;
        string ordem = OrdemFoto.Text;

        OrdemFoto.Text = "";
        TituloAnexo.Text = "";
        #endregion
        #region atualiza e comportamento da página
        Produtos_Fotos.Atualizar(Session["fotos_id"].ToString(), titulo, ordem, HidAnexo.Value);
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
        PanelEditAnexo.Visible = false;
        GridProdutos_Fotos.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region excluir foto
    public void ExcluirAnexo()
    {
        #region exclui arquivo
        try
        {
            DataTable dt = Produtos_Fotos.UmaFoto(Session["fotos_id"].ToString());
            DataRow dr = dt.Rows[0];
            string pasta = dr["id_produto"].ToString();
            string arquivo = dr["arquivo"].ToString();
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\album\{0}\{1}", pasta, arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion
        #region excluir registro e aparencia da página
        Produtos_Fotos.Excluir(Session["fotos_id"].ToString());
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item excluido com sucesso!');window.location.src = window.location.src;", true);
        PanelEditAnexo.Visible = false;
        GridProdutos_Fotos.DataBind();
        #endregion
    }

    #endregion
    #region cancelar nova foto
    protected void ibt_CancelarNovaFoto_Click(object sender, ImageClickEventArgs e)
    {
        PanelEditAnexo.Visible = false;
        ibtAtualizarAnexo.Visible = false;
        ibtSalvarAnexo.Visible = true;
        FotoSelecionada.Visible = false;
        TituloAnexo.Text = "";
        OrdemFoto.Text = "";
    }
    #endregion
    #endregion
    #region Descrição do produto
    #region Nova Descrição
    #region aparencia da página
    protected void bt_novaDescricao_Click(object sender, EventArgs e)
    {
        PanelListDescricao.Visible = false;
        PanelDetalheDescricao.Visible = true;
        TituloDescricao.Text = "";
        DescricaoProduto.Text = "";
        ibt_SalvarDescricao.Visible = true;
        ibt_AtualizarDescricao.Visible = false;
    }
    #endregion
    #region salva nova descrição
    protected void ibt_SalvarDescricao_Click(object sender, ImageClickEventArgs e)
    {
        #region trata strings importantes
        String r = DescricaoProduto.Text;
        String r1 = r.Replace("\\", "/");
        String descricao = r1.Replace("'", "\\'");
        String d = TituloDescricao.Text;
        String d1 = d.Replace("\\", "/");
        String titulo = d1.Replace("'", "\\'");
        #endregion
        #region grava dados
        Produtos_Descricao.Inserir(Session["id"].ToString(), titulo, descricao);
        #endregion
        #region comportamento da página
        PanelListDescricao.Visible = true;
        PanelDetalheDescricao.Visible = false;
        dtlTituloDescricao.DataBind();
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item inserido com sucesso!');window.location.src = window.location.src;", true);
        #endregion
    }
    #endregion
    #endregion
    #region aparência da página
    public void Descricao()
    {
        mvProdutos.ActiveViewIndex = 6;
        DataTable pr = Produtos.SelectById(int.Parse(Session["id"].ToString()));
        DataRow pdr = pr.Rows[0];
        LabelTitulo.Text = "descrições do produto: " + pdr["ProdDescricao_"].ToString();
        TituloProduto.Text = pdr["ProdDescricao_"].ToString();
        PanelListDescricao.Visible = true;
        PanelDetalheDescricao.Visible = false;
        dtlTituloDescricao.DataBind();
    }
    #endregion
    #region atualizar descrição
    #region carrega descrição
    public void AtualizaDescricao(object sender, DataListCommandEventArgs e)
    {
        Session["id_descricao"] = e.CommandArgument.ToString();
        PanelListDescricao.Visible = true;
        PanelDetalheDescricao.Visible = false;
        DataTable dt = Produtos_Descricao.SelectByID(int.Parse(e.CommandArgument.ToString()));
        DataRow dr = dt.Rows[0];

        TituloDescricao.Text = dr["titulo"].ToString();
        DescricaoProduto.Text = dr["descricao"].ToString();

        PanelListDescricao.Visible = false;
        PanelDetalheDescricao.Visible = true;

        ibt_AtualizarDescricao.Visible = true;
        ibt_SalvarDescricao.Visible = false;
    }
    #endregion
    #region atualiza descrição
    protected void ibt_AtualizarDescricao_Click(object sender, ImageClickEventArgs e)
    {
        #region trata strings importantes
        String r = DescricaoProduto.Text;
        String r1 = r.Replace("\\", "/");
        String descricao = r1.Replace("'", "\\'");
        String d = TituloDescricao.Text;
        String d1 = d.Replace("\\", "/");
        String titulo = d1.Replace("'", "\\'");
        #endregion
        #region grava dados
        Produtos_Descricao.Atualizar(Session["id_descricao"].ToString(), Session["id"].ToString(), titulo, descricao);
        #endregion
        #region comportamento da página
        PanelListDescricao.Visible = true;
        PanelDetalheDescricao.Visible = false;

        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);

        dtlTituloDescricao.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region cancelar descrição
    protected void ibt_CancelarDescricao_Click(object sender, ImageClickEventArgs e)
    {
        PanelListDescricao.Visible = true;
        PanelDetalheDescricao.Visible = false;
    }
    #endregion
    #endregion
    #region Categorias e sub-categorias
    #region categorias
    #region nova Categoria
    #region aparencia do site
    protected void ibt_NovoCanal_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 2;
        PanelListaCategorias.Visible = false;
        PanelDetalhesCategoria.Visible = true;
        PanelSubCategorias.Visible = false;
        AtualizarCategoria.Visible = false;
        SalvarNovaCategoria.Visible = true;

        TituloCategoria.Text = "";
    }
    #endregion
    #region clique em salvar categoria
    protected void SalvarNovaCategoria_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region icone postado
        if (IconeCategoria.FileName != string.Empty)
        {
            string extensao = (IconeCategoria.PostedFile.FileName.Split('.'))[1];
            int nextID = Produtos_Categoria.nextID;

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioCategoriaIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/produtos/categorias/{0}", nomeArquivo));
            IconeCategoria.PostedFile.SaveAs(enderecoCompleto);
            HidIconeCategoria.Value = nomeArquivo;
        }
        else
        {
            int nextID = Produtos_Categoria.nextID;
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

            string nomeArquivo = string.Format("{0}_{1}.jpg", nextID.ToString("ActioCategoriaPadrao"), newresult);

            if (File.Exists(Server.MapPath(@"~\App_Themes\Site\ImagesSuporte\IconeRCC.jpg")))
            {
                File.Copy(Server.MapPath(@"~\App_Themes\Site\ImagesSuporte\IconeRCC.jpg"), Server.MapPath(string.Format(@"~\App_Themes\ActioAdms\hd\produtos\categorias\{0}", nomeArquivo)));
                HidIconeCategoria.Value = nomeArquivo;
            }
        }
        #endregion
        #endregion
        #region grava dados
        Produtos_Categoria.Inserir(TituloCategoria.Text, HidIconeCategoria.Value, "0");
        #endregion
        #region comportamento da página
        mvProdutos.ActiveViewIndex = 1;
        ddlCategoria.DataBind();

        TituloCategoria.Text = "";

        LabelTitulo.Text = " > Produtos - Detalhes do Produto";
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item inserido com sucesso!');window.location.src = window.location.src;", true);

        #endregion
    }
    #endregion
    #endregion
    #region cancelar listagem de categorias - Voltar para detalhes do produto
    protected void ibt_VoltaProdutoDetalhe_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 1;
        ddlCategoria.DataBind();
    }
    #endregion
    #region Listagem das categorias
    protected void ibt_AlterarCanal_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 2;
        PanelListaCategorias.Visible = true;
        PanelDetalhesCategoria.Visible = false;
        gridListCategorias.DataBind();
    }
    #endregion
    #region cancelar detalhes de categorias - voltar para listagem de categorias
    protected void CancelarNovoCategoria_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 2;
        PanelListaCategorias.Visible = true;
        PanelDetalhesCategoria.Visible = false;
        gridListCategorias.DataBind();
    }
    #endregion
    #region comando da grid de categorias
    public void CategoriasRowCommand(object sender, GridViewCommandEventArgs e)
    {
        Session["id_Categoria"] = e.CommandArgument.ToString();

        if (e.CommandName == "Alterar")
            EditarCategoria();
        if (e.CommandName == "Excluir")
            ExcluirCategoria();
    }
    #endregion
    #region atualizar categoria selecionada
    #region aparencia da página
    public void EditarCategoria()
    {
        #region aparencia da página
        mvProdutos.ActiveViewIndex = 2;
        PanelListaCategorias.Visible = false;
        PanelDetalhesCategoria.Visible = true;
        PanelSubCategorias.Visible = false;

        SalvarNovaCategoria.Visible = false;
        AtualizarCategoria.Visible = true;

        Grid_SubCategorias.DataBind();

        PanelSubCategorias.Visible = true;
        Salvar_SubCategoria.Visible = true;
        EditarSubCategoria.Visible = false;
        Titulo_SubCategoria.Text = "";

        #endregion
        #region carrega dados
        DataTable dt = Produtos_Categoria.SelectByID(int.Parse(Session["id_Categoria"].ToString()));
        DataRow dr = dt.Rows[0];

        TituloCategoria.Text = dr["titulo"].ToString();
        HidIconeCategoria.Value = dr["icone"].ToString();
        ImageSelecionadaCategoria.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/produtos/categorias/" + dr["icone"].ToString());
        Grid_SubCategorias.DataBind();
        #endregion
    }
    #endregion
    #region clique em atualizar categoria
    protected void AtualizarCategoria_Click(object sender, ImageClickEventArgs e)
    {
        #region concatena dados
        #region icone postado
        if (IconeCategoria.FileName != string.Empty)
        {
            #region exclui icones
            try
            {
                int id = int.Parse(Session["id_Categoria"].ToString());
                DataTable dt = Produtos_Categoria.SelectByID(id);
                DataRow dr = dt.Rows[0];
                string arquivo = dr["icone"].ToString();
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\categorias\{0}", arquivo));
                File.Delete(deletefile);
            }
            catch
            {

            }
            #endregion

            string extensao = (IconeCategoria.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id_Categoria"].ToString());

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioCategoriaIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/produtos/categorias/{0}", nomeArquivo));
            IconeCategoria.PostedFile.SaveAs(enderecoCompleto);
            HidIconeCategoria.Value = nomeArquivo;
        }
        #endregion
        #endregion
        #region grava dados
        Produtos_Categoria.Atualizar(Session["id_Categoria"].ToString(), TituloCategoria.Text, HidIconeCategoria.Value, "0");
        #endregion
        #region comportamento da página
        mvProdutos.ActiveViewIndex = 1;
        ddlCategoria.DataBind();

        TituloCategoria.Text = "";

        LabelTitulo.Text = " > Produtos - Detalhes do Produto";
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);

        #endregion

    }
    #endregion
    #endregion
    #region excluir uma categoria (Exclui subcategorias, Registro de Vendas e Produtos também)
    public void ExcluirCategoria()
    {
        #region exclui subcategorias
        Produtos_Sub_Categoria.DeleteByIdCategoria(Session["id_Categoria"].ToString());
        #endregion
        #region exclui imagens
        #region exclui imagens de categorias
        #region exclui icones
        try
        {
            int id = int.Parse(Session["id_Categoria"].ToString());
            DataTable dt = Produtos_Categoria.SelectByID(id);
            DataRow dr = dt.Rows[0];
            string arquivo = dr["icone"].ToString();
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\categorias\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion

        #endregion
        #region exclui imagens de produtos
        #region exclui icones
        try
        {
            int id = int.Parse(Session["id_Categoria"].ToString());
            DataTable dt = Produtos.SelectByIdCategoria(id);
            DataRow dr = dt.Rows[0];
            string arquivo = dr["icone"].ToString();
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion

        #endregion
        #endregion
        #region exclui produtos
        Produtos.ExcluirByIdCategoria(Session["id_Categoria"].ToString());
        #endregion
        #region Exclui categorias
        Produtos_Categoria.Delete(Session["id_Categoria"].ToString());
        gridListCategorias.DataBind();
        ddlCategoria.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region sub-categorias
    #region listar sub-categorias da Categoria
    public void SubCategorias()
    {
        #region aparencia da página
        mvProdutos.ActiveViewIndex = 2;
        PanelDetalhesCategoria.Visible = true;
        PanelListaCategorias.Visible = false;
        #endregion
        #region exibe grid de subcategoria
        DataTable dt = Produtos_Sub_Categoria.SelectByIDCategoria(int.Parse(Session["id_Categoria"].ToString()));
        DataRow dr = dt.Rows[0];

        Titulo_SubCategoria.Text = dr["titulo"].ToString();
        Salvar_SubCategoria.Visible = false;
        EditarSubCategoria.Visible = true;
    }
        #endregion

    #endregion
    #region comandos da grid de sub categorias
    public void SubCategoriasRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int id = int.Parse(e.CommandArgument.ToString());
            Session["id_SubCategoria"] = id;
            if (e.CommandName == "EditarSubCategoria")
                subCategoriaEdit();
            if (e.CommandName == "ExcluirSubCategoria")
                excluiSubCategoria();

        }
        catch
        { }
    }
    #endregion
    #region Salva nova sub-categoria
    protected void Salvar_SubCategoria_Click(object sender, ImageClickEventArgs e)
    {
        Salvar_SubCategoria.Visible = true;
        EditarSubCategoria.Visible = false;

        Produtos_Sub_Categoria.Inserir(Titulo_SubCategoria.Text, "0", Session["id_Categoria"].ToString());
        Grid_SubCategorias.DataBind();
        rdb_SubCategorias.DataBind();
        Titulo_SubCategoria.Text = "";
    }
    #endregion
    #region Alterar Sub-Categoria
    #region aparencia da página
    #endregion
    #region carrega dados
    public void subCategoriaEdit()
    {
        DataTable dt = Produtos_Sub_Categoria.SelectByID(int.Parse(Session["id_SubCategoria"].ToString()));
        DataRow dr = dt.Rows[0];
        Titulo_SubCategoria.Text = dr["titulo"].ToString();
        Salvar_SubCategoria.Visible = false;
        EditarSubCategoria.Visible = true;
    }
    #endregion
    #region clique em alterar
    protected void EditarSubCategoria_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 2;
        TituloCategoria.Text = "";
        Salvar_SubCategoria.Visible = true;
        EditarSubCategoria.Visible = false;

        Produtos_Sub_Categoria.Atualizar(Session["id_SubCategoria"].ToString(), Titulo_SubCategoria.Text, "0", Session["id_Categoria"].ToString());
        Grid_SubCategorias.DataBind();
        rdb_SubCategorias.DataBind();
    }
    #endregion
    #endregion
    #region Cancelar Sub-Categoria
    protected void CancelarSubCategoria_Click(object sender, ImageClickEventArgs e)
    {
        Salvar_SubCategoria.Visible = true;
        EditarSubCategoria.Visible = false;
        Titulo_SubCategoria.Text = "";
    }
    #endregion
    #region excluir uma sub categoria e seus produtos
    public void excluiSubCategoria()
    {
        #region exclui imagens
        #region exclui imagens de produtos
        #region exclui icones
        try
        {
            int id = int.Parse(Session["id_SubCategoria"].ToString());
            DataTable dt = Produtos.SelectByIdSubCategoria(id);
            DataRow dr = dt.Rows[0];
            string arquivo = dr["icone"].ToString();
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\produtos\icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion

        #endregion
        #endregion
        #region exclui produtos
        Produtos.ExcluirByIdSubCategoria(Session["id_SubCategoria"].ToString());
        #endregion
        #region exclui subcategorias
        Produtos_Sub_Categoria.Delete(Session["id_SubCategoria"].ToString());
        Grid_SubCategorias.DataBind();
        rdb_SubCategorias.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #endregion
    #endregion
    #region cadastro de bancos para doação
    #region aparencia do site
    protected void lbt_Doacao_Click(object sender, EventArgs e)
    {
        mvProdutos.ActiveViewIndex = 7;
        PanelDetalhaBanco.Visible = false;
        PanelListaBanco.Visible = true;
        LabelTitulo.Text = "Cadastro de Bancos";
        gridBanco.DataBind();
    }
    #endregion
    #region novo banco
    #region aparencia da página
    protected void ibt_NovoBanco_Click(object sender, ImageClickEventArgs e)
    {
        PanelDetalhaBanco.Visible = true;
        PanelListaBanco.Visible = false;
        Marca.Text = "";
        SalvarBanco.Visible = true;
        AlterarBanco.Visible = false;
    }
    protected void lbt_NovoBanco_Click(object sender, EventArgs e)
    {
        PanelDetalhaBanco.Visible = true;
        PanelListaBanco.Visible = false;
        Marca.Text = "";
        SalvarBanco.Visible = true;
        AlterarBanco.Visible = false;
    }
    #endregion
    #region salva banco
    protected void SalvarBanco_Click(object sender, ImageClickEventArgs e)
    {
        Actio.Negocio.Marca.Inserir(Marca.Text);
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
        gridBanco.DataBind();
        PanelListaBanco.Visible = true;
        PanelDetalhaBanco.Visible = false;
    }
    #endregion
    #endregion
    #region comandos da grid
    public void BancoRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Session["id_Banco"] = e.CommandArgument.ToString();
            if (e.CommandName == "Alterar")
                carregaBanco();
            if (e.CommandName == "Excluir")
                excluiBanco();
        }
        catch { }
    }
    #endregion
    #region alterar banco
    #region carrega dados | aparencia da página
    public void carregaBanco()
    {
        DataTable dt = Actio.Negocio.Marca.SelectOne(int.Parse(Session["id_Banco"].ToString()));
        DataRow dr = dt.Rows[0];

        Marca.Text = dr["descricao"].ToString();

        PanelDetalhaBanco.Visible = true;
        PanelListaBanco.Visible = false;
        SalvarBanco.Visible = false;
        AlterarBanco.Visible = true;
    }
    #endregion
    #region grava alterações
    protected void AlterarBanco_Click(object sender, ImageClickEventArgs e)
    {
        Actio.Negocio.Marca.Update(int.Parse(Session["id_Banco"].ToString()), Marca.Text);
        PanelDetalhaBanco.Visible = false;
        PanelListaBanco.Visible = true;
        gridBanco.DataBind();

        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
    }
    #endregion
    #endregion
    #region exclui banco
    public void excluiBanco()
    {
        Actio.Negocio.Marca.Excluir(int.Parse(Session["id_Banco"].ToString()));
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item excluido com sucesso!');window.location.src = window.location.src;", true);
        gridBanco.DataBind();
    }
    #endregion
    #region cancela edição e listar bancos
    protected void ibt_ListBanco_Click(object sender, ImageClickEventArgs e)
    {
        PanelDetalhaBanco.Visible = false;
        PanelListaBanco.Visible = true;
        gridBanco.DataBind();
    }
    #endregion
    #endregion
    #region Listagem das vendas da loja
    #region aparencia da página
    protected void lk_Vendas_Click(object sender, EventArgs e)
    {
        mvProdutos.ActiveViewIndex = 3;
    }
    #endregion
    #region comando da lista de pedidos
    public void VendasListaDetalhes(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Pedido")
            {
                Session["TransacaoID"] = e.CommandArgument.ToString();
                mvProdutos.ActiveViewIndex = 4;
                PanelListCliente.Visible = false;
                PanelDetalhesCliente.Visible = false;
                PanelListaVendas.Visible = false;
                PanelDetalhaVendas.Visible = true;
                PanelStatusProduto.Visible = false;
                CodigoRastreador.Text = "";
                CodigoTransacao.Text = e.CommandArgument.ToString();

            }
            if (e.CommandName == "Cliente")
            {
                mvProdutos.ActiveViewIndex = 4;
                Session["id_cliente"] = e.CommandArgument.ToString();
                DadosCliente();
            }
        }
        catch { }
    }
    #endregion
    #endregion
    #region clientes e vendas da loja
    #region Lista clientes
    protected void lk_Clientes_Click(object sender, EventArgs e)
    {
        mvProdutos.ActiveViewIndex = 4;
        GridClientes.DataBind();
        PanelListCliente.Visible = true;
        PanelDetalhesCliente.Visible = false;
        PanelListaVendas.Visible = false;
        PanelDetalhaVendas.Visible = false;
    }
    protected void ibt_ListaClientes_Click(object sender, ImageClickEventArgs e)
    {
        PanelListCliente.Visible = true;
        PanelDetalhesCliente.Visible = false;
        PanelListaVendas.Visible = false;
        PanelDetalhaVendas.Visible = false;
        GridClientes.DataBind();
    }
    protected void lbt_todosClientes_Click(object sender, EventArgs e)
    {
        PanelListCliente.Visible = true;
        PanelDetalhesCliente.Visible = false;
        PanelListaVendas.Visible = false;
        PanelDetalhaVendas.Visible = false;
        GridClientes.DataBind();
    }
    #endregion
    #region comando da grid de clientes
    protected void ibt_voltarCliente_Click(object sender, ImageClickEventArgs e)
    {
        PanelListCliente.Visible = false;
        PanelDetalhesCliente.Visible = true;
        PanelListaVendas.Visible = false;
        PanelDetalhaVendas.Visible = false;
    }
    public void DetalhaCliente(object sender, GridViewCommandEventArgs e)
    {
        Session["id_cliente"] = e.CommandArgument.ToString();
        if (e.CommandName == "Cliente")
        {
            DadosCliente();
        }
        if (e.CommandName == "Cotato")
        {

        }
        if (e.CommandName == "Vendas")
        {
            ComprasCliente();
        }
    }

    #endregion
    #region detalha cliente
    public void DadosCliente()
    {
        if (Session["id_cliente"] == null)
            mvProdutos.ActiveViewIndex = 3;
        if (Session["id_cliente"] != null)
        {
            PanelListCliente.Visible = false;
            PanelDetalhesCliente.Visible = true;
            PanelListaVendas.Visible = false;
            PanelDetalhaVendas.Visible = false;
            DataTable dt = Clientes.SelectByID(int.Parse(Session["id_cliente"].ToString()));
            DataRow dr = dt.Rows[0];
            Session.Clear();
            Nome.Text = dr["CliNome"].ToString();
            Nome0.Text = "Listagem dos Pedidos Feitos por: " + dr["CliNome"].ToString();
            Nome1.Text = "Detalhes dos Pedidos Feito por: " + dr["CliNome"].ToString();
            Email.Text = dr["CliEmail"].ToString();
            Session["emailCliente"] = dr["CliEmail"].ToString();
            Session["id_cliente"] = dr["cliente_id"].ToString();
            Telefone.Text = dr["CliTelefone"].ToString();
            Endereco.Text = "Logradouro: " + dr["CliEndereco"].ToString() + ", Número: " + dr["CliNumero"].ToString() + ",                                    Complemento: " + dr["CliComplemento"].ToString() +
                            "<br><br>Bairro: " + dr["CliBairro"].ToString() +
                            "<br><br>Cidade: " + dr["CliCidade"].ToString() +
                            "<br><br>Estado: " + dr["CliEstado"].ToString() + " | CEP: " + dr["CliCEP"].ToString();
        }
    }
    #endregion
    #region compras do cliente
    #region lista compras do Cliente
    protected void ibt_Compras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["id_cliente"] == null)
            mvProdutos.ActiveViewIndex = 3;
        if (Session["id_cliente"] != null)
        {
            PanelListCliente.Visible = false;
            PanelDetalhesCliente.Visible = false;
            PanelListaVendas.Visible = true;
            PanelDetalhaVendas.Visible = false;
            DataTable dt = Clientes.SelectByID(int.Parse(Session["id_cliente"].ToString()));
            DataRow dr = dt.Rows[0];
            Session["emailCliente"] = dr["CliEmail"].ToString();
            Nome0.Text = "Listagem dos Pedidos Feitor por: " + dr["CliNome"].ToString();
            Nome1.Text = "Detalhes dos Pedidos Feito por: " + dr["CliNome"].ToString();
            gridVendasCliente.DataBind();
        }
    }
    protected void lbt_Compras_Click(object sender, EventArgs e)
    {
        PanelListCliente.Visible = false;
        PanelDetalhesCliente.Visible = false;
        PanelListaVendas.Visible = true;
        PanelDetalhaVendas.Visible = false;
        DataTable dt = Clientes.SelectByID(int.Parse(Session["id_cliente"].ToString()));
        DataRow dr = dt.Rows[0];
        Session["emailCliente"] = dr["CliEmail"].ToString();
        Nome0.Text = "Listagem dos Pedidos Feitor por: " + dr["CliNome"].ToString();
        Nome1.Text = "Detalhes dos Pedidos Feito por: " + dr["CliNome"].ToString();
        gridVendasCliente.DataBind();
    }
    public void ComprasCliente()
    {
        PanelListCliente.Visible = false;
        PanelDetalhesCliente.Visible = false;
        PanelListaVendas.Visible = true;
        PanelDetalhaVendas.Visible = false;
        DataTable dt = Clientes.SelectByID(int.Parse(Session["id_cliente"].ToString()));
        DataRow dr = dt.Rows[0];
        Session["emailCliente"] = dr["CliEmail"].ToString();
        Nome0.Text = "Listagem dos Pedidos Feitor por: " + dr["CliNome"].ToString();
        Nome1.Text = "Detalhes dos Pedidos Feito por: " + dr["CliNome"].ToString();
        gridVendasCliente.DataBind();
    }
    #endregion
    #region detalha pedido
    public void DetalhaPedido(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Session["TransacaoID"] = e.CommandArgument.ToString();
            PanelListCliente.Visible = false;
            PanelDetalhesCliente.Visible = false;
            PanelListaVendas.Visible = false;
            PanelDetalhaVendas.Visible = true;
            PanelStatusProduto.Visible = false;
            CodigoRastreador.Text = "";
            DataTable dt = Produtos_Itens_Pedido.selectByTransacaoID(e.CommandArgument.ToString());
            DataRow dr = dt.Rows[0];
            CodigoTransacao.Text = dr["transacao"].ToString();
        }
        catch { }
    }
    #endregion
    #region informar envio de produto
    public void informarEnvio(object sender, GridViewCommandEventArgs e)
    {
        PanelStatusProduto.Visible = true;
        DataTable dt = Produtos_Itens_Pedido.selectByTransacaoIDProdID(e.CommandName.ToString(), e.CommandArgument.ToString());
        DataRow dr = dt.Rows[0];
        Session["IdDoPedido"] = e.CommandArgument.ToString();
        ddlStatusEnvio.SelectedValue = dr["StatusEnvio"].ToString();
        CodigoRastreador.Text = dr["Rastreador"].ToString();
        Session["idItemPedido"] = dr["id"].ToString();
    }
    #region cancela status
    protected void ibt_CancelaStatusProd_Click(object sender, ImageClickEventArgs e)
    {
        PanelStatusProduto.Visible = false;
        CodigoRastreador.Text = "";
    }
    #endregion
    #region salva status e notifica cliente
    protected void ibt_SalvarStatus_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtc = Produtos_Vendas.SelectClienteByIdTransacao(CodigoTransacao.Text);
        DataRow drc = dtc.Rows[0];
        Session["id_cliente"] = drc["IdCliente"].ToString();
        #region atualiza status do pedido
        if (rblTipoEncomenda.SelectedValue == "0")
        {
            Produtos_Itens_Pedido.UpdateByStatusEnvioTodos(CodigoTransacao.Text, ddlStatusEnvio.SelectedValue, CodigoRastreador.Text);
        }
        if (rblTipoEncomenda.SelectedValue == "1")
        {
            Produtos_Itens_Pedido.UpdateByStatusEnvio(Session["idItemPedido"].ToString(), ddlStatusEnvio.SelectedValue, CodigoRastreador.Text, CodigoTransacao.Text);
        }
        Produtos_Vendas.UpdateByStatusLoja(CodigoTransacao.Text, ddlStatusEnvio.SelectedValue, CodigoRastreador.Text);

        #endregion
        #region envia e-mail para o cliente
        if (NotificarCliente.Checked == true)
        {
            DataTable dt = Clientes.SelectByID(int.Parse(Session["id_cliente"].ToString()));
            DataRow dr = dt.Rows[0];
            #region envia mensagem para o cliente
            System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.From = new System.Net.Mail.MailAddress("informatica@rccbh.com.br", "Contato - WebSite");
            objEmail.To.Add(dr["CliEmail"].ToString());
            objEmail.Priority = System.Net.Mail.MailPriority.High;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = "RCCShopping - Notificação de Envio de Produto";
            objEmail.ReplyTo = new System.Net.Mail.MailAddress("rccbh@rccbh.com.br", "RCCShopping");
            //corpo do e-mail 
            objEmail.Body = dr["CliNome"].ToString() + ", A Paz de Jesus, O Amor de Maria! " +
                            "<br><br>Este contato visa informar o envio do(s) produto(s) que você adquiriu em nossa loja virtual." +
                            "<br><br>Usamos os serviços dos Correios para fazer o envio, conforme selecionado no ato da compra." +
                            "<br><br>Você pode acompanhar o deslocamento do produto pelo site dos Correios, para isso informe o seguinte código:" +
                            "<br><br><STRONG><EM><FONT color=#ff0000 size=5>" + CodigoRastreador.Text + "</FONT></EM></STRONG>" +
                            "<br><br>Deus te abençoe grandemente, sua compra ajudou efetivamente em nosso projeto de evangelização!" +
                            "<br><br> Atenciosamente, <br><br>Equipe RCCShopping";
            //cria objeto com os dados do SMTP 
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("informatica@rccbh.com.br", "p@ssw0rd");
            try
            {
                smtp.Send(objEmail);
            }
            catch (Exception ex)
            {
            }
            objEmail.Dispose();

            #endregion
        }
        #endregion
        #region comportamento da página
        PanelStatusProduto.Visible = false;
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('status atualizado com sucesso');window.location.src = window.location.src;", true);
        gridProdutosPedido.DataBind();
        gridVendasCliente.DataBind();
        gridPedidos.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #endregion
    #endregion
    #region banner da loja
    #region lista de banner
    protected void lk_Banner_Click(object sender, EventArgs e)
    {
        mvProdutos.SetActiveView(v10Banner);
        mvBanner.ActiveViewIndex = 0;
    }
    #endregion
    #region cancelar banner
    protected void ibtList_Banner_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 10;
        mvBanner.ActiveViewIndex = 0;
        gridListBanner.DataBind();
    }
    protected void lbtList_Banner_Click(object sender, EventArgs e)
    {
        mvProdutos.ActiveViewIndex = 10;
        mvBanner.ActiveViewIndex = 0;
        gridListBanner.DataBind();
    }
    protected void ibt_cancelarBanner_Click(object sender, ImageClickEventArgs e)
    {
        mvProdutos.ActiveViewIndex = 10;
        mvBanner.ActiveViewIndex = 0;
        gridListBanner.DataBind();
    }
    #endregion
    #region adiciona novo banner
    #region aparencia da página
    protected void ibt_NovoBanner_Click(object sender, ImageClickEventArgs e)
    {
        mvBanner.ActiveViewIndex = 1;
        TituloBanner.Text = "";
        ImagemAtual.Visible = false;
        bannerLink.Text = "";
        Status.SelectedValue = "1";
        ibt_SalvarBanner.Visible = true;
        ibt_AlterarBanner.Visible = false;
    }
    protected void lbt_NovoBanner_Click(object sender, EventArgs e)
    {
        mvBanner.ActiveViewIndex = 1;
        TituloBanner.Text = "";
        ImagemAtual.Visible = false;
        bannerLink.Text = "";
        Status.SelectedValue = "1";
        ibt_SalvarBanner.Visible = true;
        ibt_AlterarBanner.Visible = false;
    }
    #endregion
    #region salvar novo banner
    protected void ibt_SalvarBanner_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region Banner Postado
        string nomeArquivo = "";
        if (fuBanner.FileName != string.Empty)
        {
            string extensao = (fuBanner.PostedFile.FileName.Split('.'))[1];
            int nextID = Banner_Loja.nextID;

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
            nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioBanner"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/banner_loja/" + nomeArquivo));
            fuBanner.PostedFile.SaveAs(enderecoCompleto);
            hidBanner.Value = "~/App_Themes/ActioAdms/hd/banner_loja/" + nomeArquivo;
            hidBannerXmlAdress.Value = "../App_Themes/ActioAdmshd/banner_loja/" + nomeArquivo;
        }
        #endregion
        #endregion
        #region salva dados
        Banner_Loja.Inserir(TituloBanner.Text, bannerLink.Text, Status.SelectedValue, int.Parse(Tipo.SelectedValue), nomeArquivo, ddlCategoriaBanner.SelectedIndex == 0 ? null : (int?)int.Parse(ddlCategoriaBanner.SelectedValue));
        #endregion
        #region comportamenteo da página
        mvBanner.ActiveViewIndex = 0;
        gridListBanner.DataBind();
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item inserido com sucesso!');window.location.src = window.location.src;", true);

        #endregion
    }
    #endregion
    #endregion
    #region atualiza banner
    #region comando da grid
    public void RowCommandBanner(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int id = int.Parse(e.CommandArgument.ToString());
            Session["banner_id"] = id;

            if (e.CommandName == "Alterar")
                CarregarBanner();
            if (e.CommandName == "Excluir")
                ExcluirBanner();
        }
        catch
        { }
    }

    #endregion
    #region carrega dados
    public void CarregarBanner()
    {
        ibt_AlterarBanner.Visible = true;
        ibt_SalvarBanner.Visible = false;
        requireImagem.Enabled = false;
        ImagemAtual.Visible = true;
        LabelImagemAtual.Visible = true;
        LabelImagemAtual.Text = "Imagem Atual";
        mvBanner.ActiveViewIndex = 1;

        DataTable dt = Banner_Loja.SelectOne(int.Parse(Session["banner_id"].ToString()));
        DataRow dr = dt.Rows[0];

        TituloBanner.Text = dr["banner_alt"].ToString();
        Status.SelectedValue = dr["banner_isAtivo"].ToString();
        bannerLink.Text = dr["banner_url"].ToString();
        hidBanner.Value = dr["banner_arquivo"].ToString();
        ImagemAtual.ImageUrl = dr["banner_arquivo"].ToString();
    }
    #endregion
    #region atualiza banner
    protected void ibt_AlterarBanner_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region Banner Postado
        string nomeArquivo = "";
        if (fuBanner.FileName != string.Empty)
        {
            #region apaga icone atual
            try
            {
                string deletefile = Server.MapPath(string.Format(hidBanner.Value));
                File.Delete(deletefile);
            }
            catch { }
            #endregion
            string extensao = (fuBanner.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["banner_id"].ToString());

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
            nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioBanner"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/banner_loja/" + nomeArquivo));

            fuBanner.PostedFile.SaveAs(enderecoCompleto);
            hidBanner.Value = "~/App_Themes/ActioAdms/hd/banner_loja/" + nomeArquivo;
            hidBannerXmlAdress.Value = "../App_Themes/ActioAdms/hd/banner_loja/" + nomeArquivo;
        }
        #endregion
        #endregion
        int umBanner = Banner_Loja.nextIDActive;
        #region Atualização Padrão
        if (umBanner != 2)
        {
            #region salva dados
            Banner_Loja.Update(Session["banner_id"].ToString(), TituloBanner.Text, bannerLink.Text, nomeArquivo, Status.SelectedValue, ddlCategoriaBanner.SelectedIndex == 0 ? null : (int?)int.Parse(ddlCategoriaBanner.SelectedValue));
            #endregion
            #region comportamento da página
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
            gridListBanner.DataBind();
            mvBanner.ActiveViewIndex = 0;
            #endregion
        }
        #endregion
        #region Atualização com 2 banners cadastrados ou 1 banner cadastrado
        if (umBanner == 2)
        {
            #region marcado como item inativo
            if (Status.SelectedValue == "0")
            {

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Como você só tem um banner ativo ele ative este item antes de alterá-lo!');window.location.src = window.location.src;", true);
                mvBanner.ActiveViewIndex = 0;
            }
            #endregion
            #region marcado como ativo
            if (Status.SelectedValue == "1")
            {
                #region Carrega Item XML
                DataTable dt = Banner_Loja.SelectOne(int.Parse(Session["banner_id"].ToString()));
                DataRow dr = dt.Rows[0];
                int ativo = int.Parse(dr["banner_isAtivo"].ToString());
                #endregion
                #region se o item já estiver ativo
                if (ativo == 1)
                {
                    #region salva dados
                    Banner_Loja.Update(Session["banner_id"].ToString(), TituloBanner.Text, bannerLink.Text, nomeArquivo, Status.SelectedValue, ddlCategoriaBanner.SelectedIndex == 0 ? null : (int?)int.Parse(ddlCategoriaBanner.SelectedValue));
                    #endregion
                    #region comportamento da página
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
                    mvBanner.ActiveViewIndex = 0;
                    gridListBanner.DataBind();
                    #endregion

                }
                #endregion
                #region se o item estiver inativo
                if (ativo == 0)
                {
                    #region salva dados
                    Banner_Loja.Update(Session["banner_id"].ToString(), TituloBanner.Text, bannerLink.Text, nomeArquivo, Status.SelectedValue, ddlCategoriaBanner.SelectedIndex == 0 ? null : (int?)int.Parse(ddlCategoriaBanner.SelectedValue));
                    #endregion
                    #region comportamento da página
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item atualizado com sucesso!');window.location.src = window.location.src;", true);
                    mvBanner.ActiveViewIndex = 0;
                    gridListBanner.DataBind();
                    #endregion

                }
                #endregion
            }
            #endregion
        }

        #endregion
        gridListBanner.DataBind();
    }
    #endregion
    #endregion
    #region apaga banner
    public void ExcluirBanner()
    {
        int umBanner = Banner_Loja.nextIDActive;

        if (umBanner != 2)
        {
            #region apaga banner
            DataTable dt = Banner_Loja.SelectOne(int.Parse(Session["banner_id"].ToString()));
            DataRow dr = dt.Rows[0];
            String enderecoXML = dr["banner_arquivo"].ToString();
            String xml = enderecoXML.Replace("~/", "../");
            try
            {
                string deletefile = Server.MapPath(string.Format(dr["banner_arquivo"].ToString()));
                File.Delete(deletefile);

            }
            catch
            { }
            #endregion
            #region excluir registro no banco de dados
            Banner_Loja.Excluir(int.Parse(Session["banner_id"].ToString()));
            #endregion
            #region comportamento da página
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item excluido com sucesso!');window.location.src = window.location.src;", true);
            mvBanner.ActiveViewIndex = 0;
            gridListBanner.DataBind();
            #endregion
        }
        if (umBanner == 2)
        {
            DataTable dt = Banner_Loja.SelectOne(int.Parse(Session["banner_id"].ToString()));
            DataRow dr = dt.Rows[0];
            int inativo = int.Parse(dr["banner_isAtivo"].ToString());
            if (inativo == 0)
            {
                #region apaga banner
                String enderecoXML = dr["banner_arquivo"].ToString();
                String xml = enderecoXML.Replace("~/", "../");
                try
                {
                    string deletefile = Server.MapPath(string.Format(dr["banner_arquivo"].ToString()));
                    File.Delete(deletefile);

                }
                catch
                { }
                #endregion
                #region excluir registro no banco de dados
                Banner_Loja.Excluir(int.Parse(Session["banner_id"].ToString()));
                #endregion
                #region comportamento da página
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Item excluido com sucesso!');window.location.src = window.location.src;", true);
                mvBanner.ActiveViewIndex = 0;
                gridListBanner.DataBind();
                #endregion
            }
            if (inativo == 1)
            {
                #region comportamento da página
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Para não comprometer o layout do site pelo menos um banner deve estar cadastrado e ativo');window.location.src = window.location.src;", true);
                #endregion
            }
        }
    }

    #endregion

    protected void Tipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        MensagemTamanho.Text = int.Parse(Tipo.SelectedValue) == 0 ?
            "Sua imagem deve ter 280px X 830px, procure fazer imagens apropriadas para internet." :
            "Sua imagem deve ter 200px de largura, procure fazer imagens apropriadas para internet.";
    }
    #endregion

    protected void linkMarcas_Click(object sender, EventArgs e)
    {
        mvProdutos.SetActiveView(viewMarcas);
        PanelDetalhaBanco.Visible = false;
        PanelListaBanco.Visible = true;
        LabelTitulo.Text = "Cadastro de Marcas";
        gridBanco.DataBind();
    }

    protected void ddlCategoriaBanner_SelectedIndexChanged(object sender, EventArgs e)
    {
        Tipo.Enabled = ddlCategoriaBanner.SelectedIndex == 0;

        if (ddlCategoriaBanner.SelectedIndex > 0)
            Tipo.SelectedIndex = 0;
    }
}
#endregion