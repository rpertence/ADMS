using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Actio.Negocio;
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class ActioAdms_capela_Default : System.Web.UI.Page
{
    #region aparência da página ao carregar
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Credencial();
        gridList.Visible = true;
        gridListBloqueados.Visible = true;
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
                LabelTituloPagina.Text = "Administração de Capela Virtual";
                mvAll.ActiveViewIndex = 0;
                gridList.Visible = true;
                gridList.DataBind();

            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("14", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Capela Virtual";
                    mvAll.ActiveViewIndex = 0;
                    gridList.Visible = true;
                    gridList.DataBind();

                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Capela Virtual - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Capela Virtual  - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
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
        Titulo.Text = "";
        Email.Text = "";
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
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion
        #endregion
        #region salva dados
        Capela_Virtual.Inserir(descricao, titulo, Email.Text, Status.SelectedValue);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "0", "Incluiu o item " + titulo, "Capela Virtual");
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
        #endregion
        #region carrega dados
        DataTable dt = Capela_Virtual.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        Titulo.Text = dr["nome"].ToString();
        Email.Text = dr["email"].ToString();
        Descricao.Text = dr["descricao"].ToString();
        Status.SelectedValue = dr["status"].ToString();
        if (int.Parse(dr["status"].ToString()) == 1)
            checkAvisar.Visible = false;
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
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        #endregion

        #endregion
        #region salva dados
        Capela_Virtual.Atualizar(Session["id"].ToString(), descricao, titulo, Email.Text, Status.SelectedValue);
        #endregion
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "1", "Atualizou o item " + titulo, "Capela Virtual");
        #endregion
        #region comportamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        PadraoDoEnter(ibt_cancelar);
        #endregion
        #region notifica usuário da liberação
        if (checkAvisar.Checked) 
        {
        System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.From = new System.Net.Mail.MailAddress("rccbh@rccbh.com.br", "Contato - Portal RCCBH");
        objEmail.To.Add(Email.Text);
        objEmail.Priority = System.Net.Mail.MailPriority.High;
        objEmail.IsBodyHtml = true;
        objEmail.Subject = "Capela Virtual Portal RCCBH";
        objEmail.ReplyTo = new System.Net.Mail.MailAddress("rccbh@rccbh.com.br", "Contato - Portal RCCBH");
        objEmail.Body = "<br />Mensagem da equipe de intercessão do Portal RCCBH - Capela Virtual<br /><br />" + titulo + "<br />Email: " + Email.Text + "<br /><br/>A Paz de Jesus e o Amor de Maria, Amém?<br /><br />Seu pedido de oração, encomendado em nossa Capela Virtual, foi lido e está em nossas orações<br />Escrevemos para informar que ele já está disponível também em nosso Portal<br />Contamos com suas orações!<br /><br />Fique na Graça e na Paz da Parte de Nosso Senhor Jesus Cristo<br />";
        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential("suporte@actiocubic.com.br", "sw0rdfish");
        try
        {
            smtp.Send(objEmail);
            this.LabelAviso.Visible = true;
            this.LabelAviso.Text = "<br />O usuário " + titulo + " foi informado por e-mail da publicação do pedido de oração!";
            btOK.Visible = true;
        }
        catch (Exception ex)
        {
            this.LabelAviso.Visible = true;
            this.LabelAviso.Text = "<br /> O pedido de oração está disponível no site! <br />Mas o sistema não conseguiu informar o usuário em função do seguinte erro: Error = <br />" + ex.Message;
            btOK.Visible = true;
        }
        objEmail.Dispose();
        
        }
        #endregion
    }
    #endregion
    #endregion
    #region excluir
    public void Excluir()
    {
        #region carrega dados
        DataTable dt = Capela_Virtual.SelectByID(int.Parse(Session["id"].ToString()));
        DataRow dr = dt.Rows[0];
        string titulo = dr["nome"].ToString();
        #endregion
        #region apaga registros na base de dados
        Capela_Virtual.Delete(int.Parse(Session["id"].ToString()));
        #endregion
        #region grava histórico
        DateTime dia = DateTime.Now;
        string ss = Convert.ToString(dia);
        Historico.Inserir(Page.User.Identity.Name, ss, "2", "Exclui o item " + titulo, "Capela Virtual");
        #endregion
        #region comporatamento da página
        mvAll.ActiveViewIndex = 0;
        gridList.DataBind();
        gridListBloqueados.DataBind();
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
    protected void btOK_Click(object sender, EventArgs e)
    {
        this.LabelAviso.Visible = false;
        btOK.Visible = false;
    }
    protected void Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(Status.SelectedValue.ToString()) == 1)
            checkAvisar.Visible = true;
        else
            checkAvisar.Visible = false;
    }
}