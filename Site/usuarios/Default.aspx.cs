using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Actio.Negocio;

public partial class adms_usuarios_Default : System.Web.UI.Page
{
    #region aparência da página ao carregar
    #region pageload
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
                LabelTituloPagina.Text = "Administração de Usuários";
                mvAll.ActiveViewIndex = 0;
            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("3", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Usuários";
                    mvAll.ActiveViewIndex = 0;
                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Usuários - Sua credencial não permite a utilização deste painel!";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Usuários - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
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
        PadraoDoEnter(ibt_salvar);
        ibt_salvar.Visible = true;
        ibt_editar.Visible = false;
        LabelNome.Text = "Adicionar novo usuário ou administrador";
        IconePostado.Visible = false;
        Nome.Text = ""; 
        Email.Text= ""; 
        Senha.Text= ""; 
        Telefone.Text= ""; 
        Celular.Text= ""; 
        Nascimento.Text= ""; 
        Endereco.Text= ""; 
        Bairro.Text= ""; 
        Cidade.Text= ""; 
        Estado.Text= ""; 
        CEP.Text= ""; 
        Tipo.SelectedValue= "0"; 
        Status.SelectedValue= "1"; 
        HidIcone.Value= "";
    }
    #endregion
    #region salvar novo item
    protected void ibt_salvar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioUsuarioIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/usuarios/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #region trata strings importantes
        String n = Nome.Text;
        String n1 = n.Replace("\\", "/");
        String nome = n1.Replace("'", "\\'");
        #endregion
        #endregion
        #region salva dados       
        #region Grava permissões do usuário
        if (Tipo.SelectedValue == "1")
        {
            string id = Usuario.nextID.ToString();
            foreach (ListItem li in CheckRecursos.Items)
            {
                if (li.Selected)
                    Usuario_Recursos.Inserir(id, li.Value.ToString());
            }
        }
        #endregion
        #region salva usuário
        Usuario.Inserir(nome, Email.Text, Senha.Text, Telefone.Text, Celular.Text, Nascimento.Text, Endereco.Text, Bairro.Text, Cidade.Text, Estado.Text, CEP.Text, Tipo.SelectedValue, Status.SelectedValue, HidIcone.Value);
        #endregion
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Adicionou o item " + Nome.Text, "usuários");
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
            Session["id"] = e.CommandArgument.ToString();
            if (e.CommandName.ToString() == "Editar")
                Carregar();
            if (e.CommandName.ToString() == "Excluir")
                Excluir();
            if (e.CommandName.ToString() == "Historico")
                HistoricoUsuario();
        }
        catch
        { }
    }
    #endregion
    #region comando da seleção de tipo de usuário
    public void UsuarioTipo(object sender, EventArgs e)
    {
        if (Tipo.SelectedValue == "0")
            CheckRecursos.Enabled = false;
        if (Tipo.SelectedValue == "1")
            CheckRecursos.Enabled = true;
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
        DataTable dt = Usuario.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        Status.SelectedValue = dr["status"].ToString();
        Tipo.SelectedValue = dr["tipo"].ToString();
        Nome.Text = dr["nome"].ToString();
        LabelNome.Text = "Edição do usuário: " + dr["nome"].ToString();
        Email.Text = dr["email"].ToString();
        Senha.Text = dr["senha"].ToString();
        Telefone.Text = dr["telefone"].ToString();
        Celular.Text = dr["celular"].ToString();
        Nascimento.Text = dr["nascimento"].ToString();
        Endereco.Text = dr["endereco"].ToString();
        Bairro.Text = dr["bairro"].ToString();
        Cidade.Text = dr["cidade"].ToString();
        Estado.Text = dr["estado"].ToString();
        CEP.Text = dr["cep"].ToString();
        if (dr["icone"].ToString() != string.Empty)
        {
            IconePostado.Visible = true;
            IconePostado.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/usuarios/icones/" + dr["icone"].ToString());
            HidIcone.Value = dr["icone"].ToString();
        }

        #endregion
        #region carrega recursos
        #region aparencia do checkboxlist
        if (dr["tipo"].ToString() == "0")
            CheckRecursos.Enabled = false;
        if (dr["tipo"].ToString() == "1")
            CheckRecursos.Enabled = true;
        CheckRecursos.DataBind();

        #endregion
        #region carrega permissões                
        DataTable dtt = Usuario_Recursos.SelectByIdUsuario(dr["id"].ToString());
        foreach (DataRow drt in dtt.Rows)
        {
            string ids = drt["id_recurso"].ToString();
            foreach (ListItem li in CheckRecursos.Items)
            {
                if (li.Value == ids)
                    li.Selected = true;
            }
        }
        #endregion
        #endregion
    }
    #endregion
    #region salva dados
    protected void ibt_editar_Click(object sender, ImageClickEventArgs e)
    {
        #region concatenando dados
        #region icone postado
        if (Icone.FileName != string.Empty)
        {
            string extensao = (Icone.PostedFile.FileName.Split('.'))[1];
            int nextID = int.Parse(Session["id"].ToString());
            #region exclui icones
            try
            {
                int id = int.Parse(Session["id"].ToString());
                string arquivo = HidIcone.Value;
                string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\usuarios\icones\{0}", arquivo));
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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("ActioUsuarioIcone"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/usuarios/icones/{0}", nomeArquivo));
            Icone.PostedFile.SaveAs(enderecoCompleto);
            HidIcone.Value = nomeArquivo;
        }
        #endregion
        #region trata strings importantes
        String n = Nome.Text;
        String n1 = n.Replace("\\", "/");
        String nome = n1.Replace("'", "\\'");
        #endregion
        #endregion
        #region Grava permissões do usuário
        Usuario_Recursos.DeleteByIdUsuario(Session["id"].ToString());
        if (Tipo.SelectedValue == "1")
        {
            string id = Session["id"].ToString();
            foreach (ListItem li in CheckRecursos.Items)
            {
                if (li.Selected)
                    Usuario_Recursos.Inserir(id, li.Value.ToString());
            }
        }
        #endregion
        #region salva dados
        Usuario.Atualizar(Session["id"].ToString(), nome, Email.Text, Senha.Text, Telefone.Text, Celular.Text, Nascimento.Text, Endereco.Text, Bairro.Text, Cidade.Text, Estado.Text, CEP.Text, Tipo.SelectedValue, Status.SelectedValue, HidIcone.Value);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Fez uma atualização no item " + Nome.Text, "usuários");
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
        #region dados do usuário a ser excluido
        int id = int.Parse(Session["id"].ToString());
        DataTable dt = Usuario.SelectByID(id);
        DataRow dr = dt.Rows[0];
        string arquivo = dr["icone"].ToString();
        string nome = dr["nome"].ToString();
        #endregion
        #region exclui permissões do usuário
        Usuario_Recursos.DeleteByIdUsuario(Session["id"].ToString());
        #endregion
        #region excluir histórico
        Historico.DeleteByIdUsuario(Session["id"].ToString());
        #endregion
        #region exclui icone do usuário
        try
        {            
            string deletefile = Server.MapPath(string.Format(@"..\..\App_Themes\ActioAdms\hd\usuarios\icones\{0}", arquivo));
            File.Delete(deletefile);
        }
        catch
        {

        }
        #endregion
        #region exclui item
        Usuario.Delete(int.Parse(Session["id"].ToString()));
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "2", "Excluiu o item " + nome, "usuários");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        #endregion
    }
    #endregion
    #region exibir historico do usuario
    public void HistoricoUsuario()
    {
        #region aparencia da página
        mvAll.ActiveViewIndex = 2;
        GridHistorico.DataBind();
        #endregion
    }
    protected void ibt_voltar_Click(object sender, ImageClickEventArgs e)
    {
        mvAll.ActiveViewIndex = 0;
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