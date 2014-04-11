<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" ValidateRequest="false" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="adms_sociais_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:MultiView ID="mvAll" runat="server">
        <asp:View ID="ViewList" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:ImageButton ID="ibt_adicionar" runat="server" CausesValidation="false" 
                            onclick="ibt_adicionar_Click" SkinID="ibt_NovoItem" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        Itens cadastrados:</td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:GridView ID="gridList" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsList" 
                            EnableModelValidation="True" OnRowCommand="ComandoDaListagem" Width="80%">
                            <Columns>
                                <asp:TemplateField HeaderText="titulo" SortExpression="titulo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibt_Banner" runat="server" 
                                            AlternateText='<%# Bind("titulo") %>' CommandArgument='<%# Bind("id") %>' 
                                            CommandName="Alterar" ImageAlign="Middle" 
                                            
                                            ImageUrl='<%# "~/App_Themes/ActioAdms/hd/publicacoes/icones/" + DataBinder.Eval(Container.DataItem, "icone") %>' 
                                            Width="78px" />
                                        <asp:LinkButton ID="lkNome" runat="server" CommandArgument='<%# Bind("id") %>' 
                                            CommandName="Alterar" CssClass="linksPretos" Text='<%# Bind("titulo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                CommandArgument='<%# Bind("id") %>' CommandName="Excluir" 
                                                onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" 
                                                SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                    <asp:Image ID="Image1" runat="server" SkinID="img_Vazio" />
                                &nbsp;Não há itens cadastrados no sistema!
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="odsList" runat="server" DeleteMethod="Delete" 
                            InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" 
                            SelectMethod="SelectAll" TypeName="Actio.Negocio.Publicacao" 
                            UpdateMethod="Atualizar">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="titulo" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="anexo" Type="String" />
                                <asp:Parameter Name="icone" Type="String" />
                                <asp:Parameter Name="data_publicacao" Type="String" />
                                <asp:Parameter Name="edicao" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="id" Type="String" />
                                <asp:Parameter Name="titulo" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="anexo" Type="String" />
                                <asp:Parameter Name="icone" Type="String" />
                                <asp:Parameter Name="data_publicacao" Type="String" />
                                <asp:Parameter Name="edicao" Type="String" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewEdit" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:ImageButton ID="ibt_cancelar" runat="server" CausesValidation="False" 
                            SkinID="ibt_CancelarItem" onclick="ibt_cancelar_Click" />
                    </td>
                    <td align="left" style="padding-left: 100px;" valign="middle">
                        <asp:ImageButton ID="ibt_salvar" runat="server" SkinID="ibt_SalvarItem" 
                            onclick="ibt_salvar_Click" ValidationGroup="validacao" />
                        <asp:ImageButton ID="ibt_editar" runat="server" SkinID="ibt_EditarItem" 
                            onclick="ibt_editar_Click" ValidationGroup="validacao" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="100%" colspan="2">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    &nbsp;</td>
                                <td align="left" style="padding-left: 8px" valign="top">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="avisos" 
                                        ValidationGroup="validacao" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    titulo:</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:TextBox ID="Titulo" runat="server" Width="300px" MaxLength="145"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="Titulo" CssClass="avisos" ErrorMessage="qual o Título?" 
                                        Display="Dynamic" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" 
                                        Width="98px" />
                                </td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    Selecione uma imagem para representar sua publicação<br />
                                    <asp:FileUpload ID="Icone" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvIcone" runat="server" 
                                        ControlToValidate="Icone" CssClass="avisos" ErrorMessage="imagem é obrigatória" 
                                        ForeColor="" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="HidIcone" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    Código embed da publicação</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:RequiredFieldValidator ID="rfvIcone0" runat="server" 
                                        ControlToValidate="Anexo" CssClass="avisos" 
                                        ErrorMessage="Código da publicação é obrigatório!" ForeColor="" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="Anexo" runat="server" Rows="5" TextMode="MultiLine" Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    Data da publicação:</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:TextBox ID="Data" runat="server" SkinID="Data" MaxLength="15" Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvData" runat="server" 
                                        ControlToValidate="Data" CssClass="avisos" Display="Dynamic" 
                                        ErrorMessage="Qual a data da publicação?" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    Edição:</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:TextBox ID="Edicao" runat="server" MaxLength="145" Width="45px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEdicao" runat="server" 
                                        ControlToValidate="Edicao" CssClass="avisos" Display="Dynamic" 
                                        ErrorMessage="Qual a edição? exemplo: ano 1 número 10" 
                                        ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    Descrição:</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:TextBox ID="Descricao" runat="server" Rows="5" TextMode="MultiLine" 
                                        Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="Descricao" CssClass="avisos" Display="Dynamic" 
                                        ErrorMessage="Faça um resumo da principal matéria" 
                                        ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    &nbsp;</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

