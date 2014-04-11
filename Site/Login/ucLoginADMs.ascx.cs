using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using Actio.Negocio;

public partial class controles_ucLoginADMs : System.Web.UI.UserControl
{
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            PadraoDoEnter(bt_Logar);
        mvLogin.ActiveViewIndex = 0;
    }
    private void PadraoDoEnter(ImageButton botao)
    {
        this.Page.Form.DefaultButton = botao.UniqueID;
    }
    #endregion
    #region cancelar
    protected void bt_Cancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvLogin.ActiveViewIndex = 0;
        PadraoDoEnter(bt_Salvar);
    }
    #endregion
    #region logar
    protected void bt_Logar_Click(object sender, ImageClickEventArgs e)
    {

        Usuario usuario = validaUsuario(Login.Text, Senha.Text);

        if (usuario == null)
        {
            LabelAviso.Text = ("Dados informados não conferem ou usuário não é Administrador ou Master do sistema!");
        }
        else
        {
            FormsAuthentication.SetAuthCookie(usuario.Id.ToString(), false);

            #region grava histórico
            DateTime date = DateTime.Now;
            string s = Convert.ToString(date);
            Historico.Inserir(usuario.Id.ToString(), s, "9", "Entrou no sistema", "login");
            #endregion
            Response.Redirect("BemVindo");
        }
    }
    #endregion
    #region alterar senha
    protected void bt_AlterarSenha_Click(object sender, ImageClickEventArgs e)
    {
        mvLogin.ActiveViewIndex = 1;
        PadraoDoEnter(bt_AlterarSenha);
    }
    protected void bt_Salvar_Click(object sender, ImageClickEventArgs e)
    {
        Usuario usuario = validaUsuario(LoginAlteraSenha.Text, SenhaAtual.Text);

        if (usuario == null) return;

        string usuario_email = LoginAlteraSenha.Text;
        string usuario_senha = SenhaConfirmada.Text;

        LoginAlteraSenha.Text = "";
        SenhaConfirmada.Text = "";

        Usuario.AtualizarLoginSenha(usuario_email, usuario_senha);
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Usuário atualizado com sucesso!');window.location.src = window.location.src;", true);
        FormsAuthentication.SignOut();
        mvLogin.ActiveViewIndex = 0;
        PadraoDoEnter(bt_Salvar);
        LabelAviso.Text = ("Informe sua credencial para continuar!");
    }
    #endregion
    #region recupera senha
    protected void lk_RecuperaSenha_Click(object sender, EventArgs e)
    {
        mvLogin.ActiveViewIndex = 2;
        PadraoDoEnter(bt_RecuperaSenha);
    }

    private Usuario validaUsuario(string login, string senha)
    {
        Usuario usuario = new Usuario().BuscaAdministradorPorLogin(login, senha);
        if (usuario == null)
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Dados informados não conferem ou usuário não é administrador do sistema!');window.location.src = window.location.src;", true);

        return usuario;
    }
    #region recupera senha
    private Usuario validaUsuarioRecuperaSenha(string login)
    {
        Usuario usuario = new Usuario().BuscaUsuarioPorEmail(login);
        hidEmail.Value = usuario.Email;
        hidSenha.Value = usuario.Senha;
        hidNome.Value = usuario.Nome;

        if (usuario == null)
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('Este e-mail não está cadastrado no sistema ou usuário bloqueado!');window.location.src = window.location.src;", true);

        return usuario;
    }


    protected void bt_RecuperaSenha_Click(object sender, ImageClickEventArgs e)
    {
        Usuario usuario = validaUsuarioRecuperaSenha(LoginRecuperaSenha.Text);

        if (usuario == null)
        {
            LabelAviso.Text = ("e-mail informado não confere ou usuário bloqueado!");
        }
        else
        {
    #region Enviando formulário de contato
        //cria objeto com dados do e-mail 
        System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();
        //responder para:
        objEmail.ReplyTo = new System.Net.Mail.MailAddress("RCCBH<rccbh@rccbh.com.br>");
        //remetente do e-mail 
        objEmail.From = new System.Net.Mail.MailAddress("Recupera Senha RCCBH<suporte@actiocubic.com.br>");
        //destinatários do e-mail 
        objEmail.To.Add(hidEmail.Value);
        //objEmail.To.Add(this.ddlPara.SelectedItem.Text + " <" + this.ddlPara.SelectedValue + ">");
        //enviar cópia oculta para 
        //objEmail.Bcc.Add("Nome <suportebpf@hotmail.com>") 
        //prioridade do e-mail 
        objEmail.Priority = System.Net.Mail.MailPriority.High;
        //formato do e-mail HTML (caso não queira HTML alocar valor false) 
        objEmail.IsBodyHtml = true;
        //título do e-mail 
        objEmail.Subject = "Suporte portal Portal RCCBH | recuperação de senha";
        //corpo do e-mail 
        objEmail.Body = "<br>Recuperação de senha no site RCCBH | sistema actiocomunicacao.com.br<br><br>login: " + this.hidEmail.Value + "<br>senha: " + this.hidSenha.Value + "<br><br> Ola "+ this.hidNome + " <br><br><H3 align=justify>Recuperação de senha!</H3><P align=justify>Esta mensagem foi gerada pelo sistema de recuperação de senha do portal SINDEPOMINAS.</P><P align=justify>Recomendamos que você altere sua senha!</P><P align=justify>Se você não solicitou a recuperação de senha no portal SINDEPOMINAS,&nbsp;alguém pode estar tentando usar seu login para acessar nosso portal.&nbsp;Recomendamos que você troque a senha o mais breve possível.</P><P align=justify>Para maiores informações entre em contato com o SINDEPOMINAS.</P>";
        // Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1" 
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        //cria objeto com os dados do SMTP 
        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        // descomentar para usar o gmail
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential("suporte@actiocubic.com.br", "sw0rdfish");
        //System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient(); 
        //alocamos o endereço do host para enviar os e-mails, localhost(recomendado) ou smtp2.locaweb.com.br 
        //objSmtp.Host = "webmail.actiocomunicacao.com.br";
        //objSmtp.Credentials = new NetworkCredential("automatico@actiocomunicacao.com.br", "1234");
        //enviamos o e-mail através do método .Send() 
        try
        {
            smtp.Send(objEmail);
            //tratamento de campos se enviado com sucesso
            
            this.LabelAviso.Visible = true;
            this.LabelAviso.Text = "Sua senha foi enviada com sucesso para o e-mail informado!";
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "sucesso", "alert('e-mail enviado com sucesso');window.location.src = window.location.src;", true);
        }
        catch (Exception ex)
        {
            this.LabelAviso.Visible = true;
            // this.LabelAviso.ForeColor = Drawing.Color.Red; 
            this.LabelAviso.Text = "Ocorreram problemas no envio do e-mail. Error = " + ex.Message;
        }
        //excluímos o objeto de e-mail da memória 
        objEmail.Dispose();
        mvLogin.ActiveViewIndex = 0;
        }
    #endregion

    }
    #endregion
    #endregion
}
