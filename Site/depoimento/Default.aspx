<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="adms_depoimento_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:MultiView ID="mvAll" runat="server">
        <asp:View ID="ViewList" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:ImageButton ID="ibt_adicionar" runat="server" CausesValidation="false"                              
                            onclick="ibt_adicionar_Click" SkinID="ibt_NovoItem" />
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" height="30px" valign="middle">
                        Testemunhos ativos:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" height="30px" valign="middle" width="100%" colspan="2">
                        <asp:GridView ID="gridList" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsList" 
                            EnableModelValidation="True" OnRowCommand="ComandoDaListagem" PageSize="60">
                            <Columns>
                                <asp:TemplateField HeaderText="nome" SortExpression="nome">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" 
                                            CommandArgument='<%# BIND("id") %>' CommandName="Editar" CssClass="linksPretos" 
                                            Text='<%# Bind("nome") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" 
                                        Width="300px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="resumo" HeaderText="resumo">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                            CommandArgument='<%# Bind("id") %>' CommandName="Excluir" 
                                            onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" 
                                            SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Image ID="Image2" runat="server" SkinID="img_Vazio" />
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:ObjectDataSource ID="odsList" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" 
                            SelectMethod="SelectAllActive" TypeName="Actio.Negocio.Depoimento">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsListBloqueados" runat="server" 
                            DeleteMethod="Delete" InsertMethod="InserirPeloSite" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAllInactive" 
                            TypeName="Actio.Negocio.Depoimento" UpdateMethod="Update">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="nome" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="data" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="id" Type="String" />
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="nome" Type="String" />
                                <asp:Parameter Name="status" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="resumo" Type="String" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td align="left" height="30px" valign="middle" colspan="2">
                        Testemunhos aguardando liberação:</td>
                </tr>
                <tr>
                    <td align="left" height="30px" valign="middle" colspan="2">
                        <asp:GridView ID="gridListBloqueados" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" 
                            DataSourceID="odsListBloqueados" EnableModelValidation="True" 
                            OnRowCommand="ComandoDaListagem">
                            <Columns>
                                <asp:TemplateField HeaderText="nome" SortExpression="nome">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" 
                                            CommandArgument='<%# BIND("id") %>' CommandName="Editar" CssClass="linksPretos" 
                                            Text='<%# BIND("nome") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" 
                                        Width="300px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="resumo" HeaderText="resumo">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                            CommandArgument='<%# BIND("id") %>' CommandName="Excluir" 
                                            onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" 
                                            SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Image ID="Image3" runat="server" SkinID="img_Vazio" />
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
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
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="avisos" 
                            ValidationGroup="validacao" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        status:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">Liberado</asp:ListItem>
                            <asp:ListItem Value="0">bloqueado</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="Nome" CssClass="avisos" Display="Dynamic" 
                            ErrorMessage="qual o nome?" ForeColor="" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                        nome:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Nome" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="Email" CssClass="avisos" Display="Dynamic" 
                            ErrorMessage="qual o e-mail?" ForeColor="" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="Email" CssClass="avisos" Display="Dynamic" 
                            ErrorMessage="corrija o e-mail" ForeColor="" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            ValidationGroup="validacao">*</asp:RegularExpressionValidator>
                        e-mail:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Email" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                            ControlToValidate="Local" CssClass="avisos" Display="Dynamic" 
                            ErrorMessage="do que participou no Caná?" ForeColor="" 
                            ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                        do que participou:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Local" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="Resumo" CssClass="avisos" Display="Dynamic" 
                            ErrorMessage="faça um resumo do testemunho" ForeColor="" 
                            ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                        resumo:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Resumo" runat="server" MaxLength="245" Rows="5" 
                            TextMode="MultiLine" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        descrição:<asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                            runat="server" ControlToValidate="Descricao" CssClass="avisos" 
                            Display="Dynamic" ErrorMessage="qual o testemunho?" ForeColor="" 
                            ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox runat="server" ID="Descricao" CssClass="EditorHtml" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

