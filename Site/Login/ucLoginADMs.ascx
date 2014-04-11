<%@ Control Language="C#" AutoEventWireup="true" Inherits="controles_ucLoginADMs" Codebehind="ucLoginADMs.ascx.cs" %>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td align="left" style="width: 100%" valign="top">
                            <asp:Label ID="LabelAviso" runat="server" CssClass="assinaturas"></asp:Label><br />
                    <asp:MultiView ID="mvLogin" runat="server">
                        <asp:View ID="viewLogin" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 350px">
                                <tr>
                                    <td colspan="2" style="width: 350px; text-align: justify">
                                        </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                        e-mail:</td>
                                    <td style="width: 250px; padding-left: 5px;">
                                        <asp:TextBox ID="Login" runat="server" Width="240px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                        senha:</td>
                                    <td style="width: 250px; padding-left: 5px;">
                                        <asp:TextBox ID="Senha" runat="server" TextMode="Password" Width="240px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                    </td>
                                    <td style="width: 250px; text-align: right">
                                        <asp:LinkButton ID="lk_RecuperaSenha" runat="server" CssClass="noticiaLeiaMais" OnClick="lk_RecuperaSenha_Click">esqueceu a senha?</asp:LinkButton>&nbsp;
                                        &nbsp;<asp:ImageButton ID="bt_Logar" runat="server" 
                                            ImageUrl="~/App_Themes/ActioAdms/botoes/entrar.png" OnClick="bt_Logar_Click" 
                                            ValidationGroup="Login" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                    </td>
                                    <td style="width: 250px; text-align: right">
                                        <asp:ImageButton ID="bt_AlterarSenha" runat="server" 
                                            ImageUrl="~/App_Themes/ActioAdms/botoes/alterarsenha.png" CausesValidation="False" 
                                            OnClick="bt_AlterarSenha_Click" Height="28px" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: right">
                                        <asp:RegularExpressionValidator ID="ValidaEmail" runat="server" 
                                            ControlToValidate="Login" ErrorMessage="isto não se parece com um e-mail!" 
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                            ValidationGroup="Login"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequerSenha" runat="server" 
                                            ControlToValidate="Senha" ErrorMessage="qual sua senha?" 
                                            ValidationGroup="Login"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequerLogin" runat="server" 
                                            ControlToValidate="Login" ErrorMessage="informe seu e-mail" 
                                            ValidationGroup="Login"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            </asp:View>
                        <asp:View ID="viewAlterarSenha" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 350px">
                                <tr>
                                    <td colspan="2" style="width: 350px; text-align: justify; height: 132px;">
                                        <asp:RequiredFieldValidator ID="requerLoginAlt" runat="server" ControlToValidate="LoginAlteraSenha"
                                            ErrorMessage="qual seu e-mail?" ValidationGroup="AlterarSenha"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="validaLoginAlt" runat="server" ControlToValidate="LoginAlteraSenha"
                                            ErrorMessage="isto não se parece com um e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="AlterarSenha"></asp:RegularExpressionValidator><br />
                                        <asp:RequiredFieldValidator ID="requerSenhaAtual" runat="server" ControlToValidate="SenhaAtual"
                                            ErrorMessage="informe sua senha atual." ValidationGroup="AlterarSenha"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="requerNovaSenha" runat="server" ControlToValidate="SenhaNova"
                                            ErrorMessage="informe sua nova senha" ValidationGroup="AlterarSenha"></asp:RequiredFieldValidator><br />
                                        <asp:RequiredFieldValidator ID="requerConfirmacao" runat="server" ControlToValidate="SenhaConfirmada"
                                            ErrorMessage="repita sua nova senha" ValidationGroup="AlterarSenha"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="comparaSenha" runat="server" ControlToCompare="SenhaNova"
                                            ControlToValidate="SenhaConfirmada" ErrorMessage="nova senha não está igual, repita!"
                                            ValidationGroup="AlterarSenha"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                        e-mail:</td>
                                    <td style="width: 250px; padding-left: 5px;">
                                        <asp:TextBox ID="LoginAlteraSenha" runat="server" Width="240px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                        senha atual:</td>
                                    <td style="width: 250px; padding-left: 5px;">
                                        <asp:TextBox ID="SenhaAtual" runat="server" TextMode="Password" Width="240px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; height: 22px; text-align: right">
                                        nova senha:</td>
                                    <td style="padding-left: 5px; width: 250px; height: 22px">
                                        <asp:TextBox ID="SenhaNova" runat="server" TextMode="Password" Width="240px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                        confirme:</td>
                                    <td style="padding-left: 5px; width: 250px">
                                        <asp:TextBox ID="SenhaConfirmada" runat="server" TextMode="Password" Width="240px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                    </td>
                                    <td style="padding-left: 5px; width: 250px; text-align: right">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="noticiaLeiaMais" OnClick="lk_RecuperaSenha_Click">esqueceu a senha?</asp:LinkButton>
                                        &nbsp; &nbsp;<asp:ImageButton ID="bt_Salvar" runat="server" ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png"
                                            OnClick="bt_Salvar_Click" ValidationGroup="AlterarSenha" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                    </td>
                                    <td style="padding-left: 5px; width: 250px; text-align: right">
                                        <asp:ImageButton ID="bt_Cancelar" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png"
                                            OnClick="bt_Cancelar_Click" /></td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="viewRecuperaSenha" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 350px">
                                <tr>
                                    <td colspan="2" style="width: 350px; text-align: justify; padding-bottom: 10px;">
                                        <strong>Recuperar a senha</strong> é muito simples:<br />
                                        Basta informar o e-mail que você cadastrou, clicar em postar e nosso sistema enviará
                                        uma mensagem para este e-mail, informando qual é a sua senha.</td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right" valign="top">
                                        e-mail:</td>
                                    <td style="width: 250px; padding-left: 5px;">
                                        <asp:TextBox ID="LoginRecuperaSenha" runat="server" Width="240px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RecuperaSenhaRequer" runat="server" ControlToValidate="LoginRecuperaSenha"
                                            ErrorMessage="Qual e-mail você cadastrou em nosso sistema?"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RecuperaValidar" runat="server" ControlToValidate="LoginRecuperaSenha"
                                            ErrorMessage="Isto não se parece com um e-mail." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                        <asp:HiddenField ID="hidEmail" runat="server" />
                                        <asp:HiddenField ID="hidSenha" runat="server" />
                                    </td>
                                    <td style="padding-left: 5px; width: 250px; text-align: right">
                                        <asp:HiddenField ID="hidNome" runat="server" />
                                        <asp:ImageButton ID="bt_RecuperaSenha" runat="server" ImageUrl="~/App_Themes/ActioAdms/botoes/postar.png"
                                            OnClick="bt_RecuperaSenha_Click" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                    </td>
                                    <td style="padding-left: 5px; width: 250px; text-align: right">
                                        <asp:ImageButton ID="bt_CancelaRecupera" runat="server" 
                                            CausesValidation="False" ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png"
                                            OnClick="bt_Cancelar_Click" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 350px; text-align: justify">
                                        Caso encontre dificuldades, entre em contato com o SINDEPOMINAS [31] 3262-0780</td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
        </td>
    </tr>
</table>
