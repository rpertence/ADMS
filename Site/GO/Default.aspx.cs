using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Actio.Negocio;
using System.IO;

public partial class ActioAdms_GO_Default : System.Web.UI.Page
{
    #region page load
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
                LabelTituloPagina.Text = "Administração de Grupo de Oraçã leo";
                mvAll.ActiveViewIndex = 0;
                gridList.Visible = true;
                gridList.DataBind();

            }
            if (Tipo == 1)
            {
                try
                {
                    DataTable dt = Usuario_Recursos.SelectByIdRecursoIdUsuario("16", Page.User.Identity.Name);
                    DataRow dr = dt.Rows[0];
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Grupos de Oração";
                    mvAll.ActiveViewIndex = 0;
                    gridList.Visible = true;
                    gridList.DataBind();

                }
                catch
                {
                    mvAll.ActiveViewIndex = -1;
                    Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                    LabelTituloPagina.Text = "Administração de Artigos e Autores - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
                }
            }
            if (Tipo == 0)
            {
                mvAll.ActiveViewIndex = -1;
                Label LabelTituloPagina = (Label)Master.FindControl("LabelTituloPagina");
                LabelTituloPagina.Text = "Administração de Artigos e Autores  - <STRONG>Sua credencial não permite a utilização deste painel!</STRONG><br /> Caso tenha interesse em adquir esta ferramenta para seu site<br />Entre em contato com a Actio Comunicação:<br />3317-0794 | 88226710 - contato@actio.net.br";
            }
        }
        catch
        {
            Response.Redirect("~/");
        }
    }
    #endregion
    #endregion
    #region novo GO
    #region aparencia da página
    protected void bt_novo_go_Click(object sender, EventArgs e)
    {
        //coordenador
        Nome.Text = "";
        Email.Text = "";
        Senha.Text = "";
        Ano.Text = "";
        Telefone1.Text = "";
        Celular.Text = "";
        EnderecoCoordenador.Text = "";
        BairroCoordenador.Text = "";
        CidadeCoordenador.Text = "";
        CEP.Text = "";
        //GO
        lb_Titulo.Text = "";
        lb_Forania.Text = "";
        lb_Paroquia.Text = "";
        lb_Bairro.Text = "";
        lb_Cidade.Text = "";
        lb_Endereco.Text = "";
        lb_Telefone.Text = "";
        lb_Email.Text = "";
        lb_Site.Text = "";
        lb_Onibus.Text = "";
        lb_Hora.Text = "";
        Descricao.Text = "";
        ImageSelecionada.Visible = false;
        // botões
        SalvarGO.Visible = true;
        EditarGO.Visible = false;
        SalvarGO0.Visible = true;
        EditarGO0.Visible = false;
        //
        mvAll.ActiveViewIndex = 1;
    }
    #endregion
    #region cadastra novo grupo e coordenador
    protected void SalvarGO_Click(object sender, ImageClickEventArgs e)
    {
        #region concatena dados
        //coordenador
        String n = Nome.Text;
        String n1 = n.Replace("\\", "/");
        String nome = n1.Replace("'", "\\'");
        String em = Email.Text;
        String em1 = em.Replace("\\", "/");
        String email = em1.Replace("'", "\\'");
        String se = Senha.Text;
        String se1 = se.Replace("\\", "/");
        String senha = se1.Replace("'", "\\'");
        String an = Ano.Text;
        String an1 = an.Replace("\\", "/");
        String ano = an1.Replace("'", "\\'");
        String te = Telefone1.Text;
        String te1 = te.Replace("\\", "/");
        String telefone1 = te1.Replace("'", "\\'");
        String ce = Celular.Text;
        String ce1 = ce.Replace("\\", "/");
        String celular = ce1.Replace("'", "\\'");
        String enco = EnderecoCoordenador.Text;
        String enco1 = enco.Replace("\\", "/");
        String enderecocoordenador = enco1.Replace("'", "\\'");
        String baco = BairroCoordenador.Text;
        String baco1 = baco.Replace("\\", "/");
        String bairrocoordenador = baco1.Replace("'", "\\'");
        String cico = CidadeCoordenador.Text;
        String cico1 = cico.Replace("\\", "/");
        String cidadecoordenador = cico1.Replace("'", "\\'");
        String statuscoordenador = StatusCordenador.SelectedValue;
        //GO
        String ti = lb_Titulo.Text;
        String ti1 = ti.Replace("\\", "/");
        String titulo = ti1.Replace("'", "\\'");
        String fo = lb_Forania.Text;
        String fo1 = fo.Replace("\\", "/");
        String forania = fo1.Replace("'", "\\'");
        String pa = lb_Paroquia.Text;
        String pa1 = pa.Replace("\\", "/");
        String paroquia = pa1.Replace("'", "\\'");
        String ba = lb_Bairro.Text;
        String ba1 = ba.Replace("\\", "/");
        String bairro = ba1.Replace("'", "\\'");
        String ci = lb_Cidade.Text;
        String ci1 = ci.Replace("\\", "/");
        String cidade = ci1.Replace("'", "\\'");
        String en = lb_Endereco.Text;
        String en1 = en.Replace("\\", "/");
        String endereco = en1.Replace("'", "\\'");
        String tego = lb_Telefone.Text;
        String tego1 = tego.Replace("\\", "/");
        String telefonego = tego1.Replace("'", "\\'");
        String emgo = lb_Email.Text;
        String emgo1 = emgo.Replace("\\", "/");
        String emailgo = emgo1.Replace("'", "\\'");
        String si = lb_Site.Text;
        String si1 = si.Replace("\\", "/");
        String site = si1.Replace("'", "\\'");
        String on = lb_Onibus.Text;
        String on1 = on.Replace("\\", "/");
        String onibus = on1.Replace("'", "\\'");
        String h = lb_Hora.Text;
        String h1 = h.Replace("\\", "/");
        String hora = h1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        String regiao = lb_Regiao.SelectedValue;
        #region Icone Postado
        if (fuIcone.FileName != string.Empty)
        {
            string extensao = (fuIcone.PostedFile.FileName.Split('.'))[1];
            int nextID = Coordenador_GO.nextID;

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("Coordenador"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/coordenador_GO/" + nomeArquivo));
            fuIcone.PostedFile.SaveAs(enderecoCompleto);
            hidIcone.Value = nomeArquivo;
        }
        else
        {
            int nextID = Coordenador_GO.nextID;
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
            File.Copy(Server.MapPath(@"..\App_Themes\Site\ImagensSuporte\CoordenadorGO.png"), Server.MapPath(string.Format(@"..\App_Themes\ActioAdms\hd\coordenador_GO\{0}", nomeArquivo)));
            hidIcone.Value = nomeArquivo;
        }

        #endregion
        #endregion
        #region Grava Coordenador
        Coordenador_GO.Inserir(nome, email, senha, telefone1, celular, ano, enderecocoordenador, bairrocoordenador, cidadecoordenador, "MG", CEP.Text, "3", statuscoordenador, hidIcone.Value);
        #endregion
        #region Grava GO
        DataTable dt = Coordenador_GO.SearchByName(nome);
        DataRow dr = dt.Rows[0];
        GrupodeOracao.Inserir(dr["id"].ToString(), titulo, regiao, paroquia, bairro, cidade, endereco, telefonego, emailgo, site, onibus, ddl_Dia.SelectedValue, hora, descricao, "", StatusGO.SelectedValue, forania);
        #endregion
        #region aparencia da página
        mvAll.ActiveViewIndex = 0;
        gridList.Visible = true;
        gridList.DataBind();
        #endregion
    }
    #endregion
    #endregion
    #region edita Grupo de Oração
    protected void EditaGO(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            #region aparencia
            // botões
            SalvarGO.Visible = false;
            EditarGO.Visible = true;
            SalvarGO0.Visible = false;
            EditarGO0.Visible = true;
            mvAll.ActiveViewIndex = 1;
            #endregion
            #region carrega GO
            DataTable dtgo = GrupodeOracao.SelectByID(int.Parse(e.CommandArgument.ToString()));
            DataRow drgo = dtgo.Rows[0];
            lb_Titulo.Text = drgo["titulo"].ToString();
            lb_Forania.Text = drgo["forania"].ToString();
            lb_Paroquia.Text = drgo["paroquia"].ToString();
            lb_Bairro.Text = drgo["bairro"].ToString();
            lb_Cidade.Text = drgo["cidade"].ToString();
            lb_Endereco.Text = drgo["endereco"].ToString();
            lb_Telefone.Text = drgo["telefone"].ToString();
            lb_Email.Text = drgo["email"].ToString();
            lb_Site.Text = drgo["site"].ToString();
            lb_Onibus.Text = drgo["onibus"].ToString();
            lb_Hora.Text = drgo["hora"].ToString();
            Descricao.Text = drgo["descricao"].ToString();
            ddl_Dia.SelectedValue = drgo["dia"].ToString();
            lb_Regiao.SelectedValue = drgo["regiao"].ToString();
            Session["id"] = drgo["id"].ToString();
            //coordenador
            try
            {
                string id_usuario = drgo["id_usuario"].ToString();
                DataTable dt = Coordenador_GO.SelectByID(int.Parse(id_usuario));
                DataRow dr = dt.Rows[0];
                Nome.Text = dr["nome"].ToString();
                Email.Text = dr["email"].ToString();
                Senha.Text = dr["senha"].ToString();
                Ano.Text = dr["nascimento"].ToString();
                Telefone1.Text = dr["telefone"].ToString();
                Celular.Text = dr["celular"].ToString();
                Session["id_coordenador"] = dr["id"].ToString();
                //imagem
                try
                {
                    if (dr["icone"].ToString() != string.Empty)
                    {
                        ImageSelecionada.Visible = true;
                        ImageSelecionada.ImageUrl = string.Format("~/App_Themes/ActioAdms/hd/coordenador_GO/" + dr["icone"].ToString());
                        ImageSelecionada.Visible = true;
                        hidIcone.Value = dr["icone"].ToString();
                    }
                }
                catch
                {
                    ImageSelecionada.Visible = false;
                    hidIcone.Value = "";
                }
            }
            catch
            {

            }

            #endregion
        }
    }
    protected void EditarGO_Click(object sender, ImageClickEventArgs e)
    {
        #region concatena dados
        //coordenador
        String n = Nome.Text;
        String n1 = n.Replace("\\", "/");
        String nome = n1.Replace("'", "\\'");
        String em = Email.Text;
        String em1 = em.Replace("\\", "/");
        String email = em1.Replace("'", "\\'");
        String se = Senha.Text;
        String se1 = se.Replace("\\", "/");
        String senha = se1.Replace("'", "\\'");
        String an = Ano.Text;
        String an1 = an.Replace("\\", "/");
        String ano = an1.Replace("'", "\\'");
        String te = Telefone1.Text;
        String te1 = te.Replace("\\", "/");
        String telefone1 = te1.Replace("'", "\\'");
        String ce = Celular.Text;
        String ce1 = ce.Replace("\\", "/");
        String celular = ce1.Replace("'", "\\'");
        String enco = EnderecoCoordenador.Text;
        String enco1 = enco.Replace("\\", "/");
        String enderecocoordenador = enco1.Replace("'", "\\'");
        String baco = BairroCoordenador.Text;
        String baco1 = baco.Replace("\\", "/");
        String bairrocoordenador = baco1.Replace("'", "\\'");
        String cico = CidadeCoordenador.Text;
        String cico1 = cico.Replace("\\", "/");
        String cidadecoordenador = cico1.Replace("'", "\\'");
        String statuscoordenador = StatusCordenador.SelectedValue;
        //GO
        String ti = lb_Titulo.Text;
        String ti1 = ti.Replace("\\", "/");
        String titulo = ti1.Replace("'", "\\'");
        String fo = lb_Forania.Text;
        String fo1 = fo.Replace("\\", "/");
        String forania = fo1.Replace("'", "\\'");
        String pa = lb_Paroquia.Text;
        String pa1 = pa.Replace("\\", "/");
        String paroquia = pa1.Replace("'", "\\'");
        String ba = lb_Bairro.Text;
        String ba1 = ba.Replace("\\", "/");
        String bairro = ba1.Replace("'", "\\'");
        String ci = lb_Cidade.Text;
        String ci1 = ci.Replace("\\", "/");
        String cidade = ci1.Replace("'", "\\'");
        String en = lb_Endereco.Text;
        String en1 = en.Replace("\\", "/");
        String endereco = en1.Replace("'", "\\'");
        String tego = lb_Telefone.Text;
        String tego1 = tego.Replace("\\", "/");
        String telefonego = tego1.Replace("'", "\\'");
        String emgo = lb_Email.Text;
        String emgo1 = emgo.Replace("\\", "/");
        String emailgo = emgo1.Replace("'", "\\'");
        String si = lb_Site.Text;
        String si1 = si.Replace("\\", "/");
        String site = si1.Replace("'", "\\'");
        String on = lb_Onibus.Text;
        String on1 = on.Replace("\\", "/");
        String onibus = on1.Replace("'", "\\'");
        String h = lb_Hora.Text;
        String h1 = h.Replace("\\", "/");
        String hora = h1.Replace("'", "\\'");
        String d = Descricao.Text;
        String d1 = d.Replace("\\", "/");
        String descricao = d1.Replace("'", "\\'");
        String regiao = lb_Regiao.SelectedValue;
        #region Icone Postado
        try
        {
            string extensao = (fuIcone.PostedFile.FileName.Split('.'))[1];
            int nextID = Coordenador_GO.nextID;

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
            string nomeArquivo = string.Format("{0}_{1}.{2}", nextID.ToString("Coordenador"), newresult, extensao);
            string enderecoCompleto = Server.MapPath(string.Format("~/App_Themes/ActioAdms/hd/coordenador_GO/" + nomeArquivo));
            fuIcone.PostedFile.SaveAs(enderecoCompleto);
            hidIcone.Value = nomeArquivo;
        }
        catch
        {

        }

        #endregion
        #endregion
        #region atualiza coordenador
        Coordenador_GO.Atualizar(Session["id_coordenador"].ToString(), nome, email, senha, telefone1, celular, ano, enderecocoordenador, bairrocoordenador, cidadecoordenador, "MG", CEP.Text, "3", statuscoordenador, hidIcone.Value);
        #endregion
        #region atualiza GO
        GrupodeOracao.Atualizar(Session["id"].ToString(), Session["id_coordenador"].ToString(), titulo, regiao, paroquia, bairro, cidade, endereco, telefonego, email, site, onibus, ddl_Dia.SelectedValue, hora, descricao, "", StatusGO.SelectedValue, forania); 
        #endregion
        #region aparencia da página
        mvAll.ActiveViewIndex = 0;
        gridList.Visible = true;
        gridList.DataBind();
        #endregion
    }
    #endregion
    #region cancela tudo
    protected void Cancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/GO/");
    }
    #endregion
} 