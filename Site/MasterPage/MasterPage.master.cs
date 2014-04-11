using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Actio.Negocio;
using System.Net;
using System.Text;
using System.Net.Mail;

public partial class adms_MasterPage_MasterPage : System.Web.UI.MasterPage
{
    #region aparência do site ao carregar
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.User.Identity.Name == null)
            {
                Response.Redirect("~/");
            }
            else
            {
                Credencial();
            }
            PanelSuporte.Visible = false;
            PanelMaster.Visible = true;
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #region título da página
    public string TituloPagina
    {
        get
        {
            return LabelTituloPagina.Text;
        }
        set
        {
            LabelTituloPagina.Text = value;
        }
    }
    #endregion
    #region credencial - verifica se o usuário está logado e se é administrador do sistema
    public void Credencial()
    {
        Usuario usuarioLogado = new Usuario(int.Parse(Page.User.Identity.Name));
        int tipo = int.Parse(usuarioLogado.Tipo.ToString());
        if (tipo == 0)
        {
         LabelTopo.Text = "Actio Comunicação | ADMs, " + DateTime.Now.ToString("dd 'de' MMMMMM 'de' yyyy HH:mm'h'") + ". Usuário Logado: " +
usuarioLogado.Nome;
        bt_logOff.Visible = true;
        lk_LogOff.Visible = true;
        }
        if (tipo == 1)
        {
            LabelTopo.Text = "Actio Comunicação | ADMs, " + DateTime.Now.ToString("dd 'de' MMMMMM 'de' yyyy HH:mm'h'") + ". Administrador Logado: " +
usuarioLogado.Nome;
            bt_logOff.Visible = true;
            lk_LogOff.Visible = true;
        }
        if (tipo == 2)
        {
            LabelTopo.Text = "Actio Comunicação | ADMs, " + DateTime.Now.ToString("dd 'de' MMMMMM 'de' yyyy HH:mm'h'") + ". Master Logado: " +
usuarioLogado.Nome;
            bt_logOff.Visible = true;
            lk_LogOff.Visible = true;
        }
    }
    #endregion
    #endregion
    #region suporte ao sistema
    protected void lbt_suporte_Click(object sender, EventArgs e)
    {
        PanelMaster.Visible = false;
        PanelSuporte.Visible = true;
    }
    #region Enviando formulário de contato
    protected void lk_contato_Click(object sender, ImageClickEventArgs e)
    {
        System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();
        objEmail.From = new System.Net.Mail.MailAddress("suporte@actiocubic.com.br");
        objEmail.To.Add("<contato@actio.net.br>");
        objEmail.ReplyTo = new System.Net.Mail.MailAddress(txtEmail.Text, txtNome.Text);
        objEmail.Priority = System.Net.Mail.MailPriority.High;
        objEmail.IsBodyHtml = true;
        objEmail.Subject = "Suporte Adm´s";
        objEmail.Body = "<br>Mensagem de suporte<br><br>Usuário: " + this.txtNome.Text + "<br>Email: " + this.txtEmail.Text + "<br><br>Mensagem:<br>" + txtDescricao.Text;
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential("suporte@actiocubic.com.br", "sw0rdfish");
        try
        {
            smtp.Send(objEmail);
            lkbEnviar.Visible = false;
            txtEmail.Visible = false;
            txtNome.Enabled = false;
            txtDescricao.Visible = false;
            LabelEmail.Visible = false;
            LabelGrato.Visible = true;
            LabelMensagem.Visible = false;
            LabelNome.Visible = false;
            ddlPara.Visible = false;
            LabelSelecione.Visible = false;
            this.LabelAviso.Visible = true;
            this.LabelAviso.Text = "Sua mensagem foi enviada com sucesso!";
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('e-mail enviado com sucesso');window.location.src = window.location.src;", true);
        }
        catch (Exception ex)
        {
            this.LabelAviso.Visible = true;
            this.LabelAviso.Text = "Ocorreram problemas no envio do e-mail. Error = " + ex.Message;
        }
        objEmail.Dispose();

        PanelMaster.Visible = false;
        PanelSuporte.Visible = true;
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "7", "solicitou suporte para o sistema da página '" + Request.CurrentExecutionFilePath.ToString() + "'", "suporte");
        #endregion

    }
    #endregion
    #endregion
    #region trata logoff
    protected void bt_logOff_Click1(object sender, ImageClickEventArgs e)
    {
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "8", "Saiu do sistema", "logout");
        #endregion

        FormsAuthentication.SignOut();
        Response.Redirect("~/", true);
    }
    protected void lk_LogOff_Click1(object sender, EventArgs e)
    {
        #region grava histórico
        DateTime date = DateTime.Now;
        string s = Convert.ToString(date);
        Historico.Inserir(Page.User.Identity.Name, s, "8", "Saiu do sistema", "logout");
        #endregion

        FormsAuthentication.SignOut();
        Response.Redirect("~/", true);
    }
    #endregion
    #region navegação do sistema
    #region Meu web site
    protected void ibt_MeuWebSite_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/", true);
    }
    protected void lk_MeuWebSite_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/", true);
    }
    #endregion
    #region Suporte
    protected void ibt_ajuda_Click(object sender, ImageClickEventArgs e)
    {
        PanelMaster.Visible = false;
        PanelSuporte.Visible = true;
    }
    protected void lbt_Ajuda_Click(object sender, EventArgs e)
    {
        PanelMaster.Visible = false;
        PanelSuporte.Visible = true;
    }
    #endregion
    #endregion
}
