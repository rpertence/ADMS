<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True"  CodeBehind="Default.aspx.cs" ValidateRequest="false" Inherits="ActioAdms_capela_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:MultiView ID="mvAll" runat="server">
        <asp:View ID="vList" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" style="padding: 10px;" valign="top" width="100%">
                        <asp:ImageButton ID="ibt_adicionar" runat="server" CausesValidation="false" onclick="ibt_adicionar_Click" SkinID="ibt_NovoItem" />
                        &nbsp;<asp:Label ID="LabelAviso" runat="server" Visible="False"></asp:Label>
                        <br />
                        <asp:Button ID="btOK" runat="server" OnClick="btOK_Click" Text="OK" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px;" valign="top" width="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="50%">Pedidos de Oração Liberados</td>
                                <td style="padding-left: 20px;" width="50%">Pedidos de Oração Bloqueados</td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" width="50%">
                                    <asp:GridView ID="gridList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsLiberados" EnableModelValidation="True" OnRowCommand="ComandoDaListagem" PageSize="50" Visible="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="nome" SortExpression="nome">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="Editar" CssClass="linksPretos" Text='<%# Bind("nome") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Ativo" HeaderText="status" SortExpression="status">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandArgument='<%# Bind("id") %>' CommandName="Excluir" onclientclick="if (confirm('Você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Image ID="Image2" runat="server" SkinID="img_Vazio" />
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                                <td align="left" style="padding-left: 20px;" valign="top" width="50%">
                                    <asp:GridView ID="gridListBloqueados" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsBloqueados" EnableModelValidation="True" OnRowCommand="ComandoDaListagem" PageSize="50" Visible="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="nome" SortExpression="nome">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="Editar" CssClass="linksPretos" Text='<%# Bind("nome") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Ativo" HeaderText="status" SortExpression="status">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandArgument='<%# Bind("id") %>' CommandName="Excluir" onclientclick="if (confirm('Você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Image ID="Image3" runat="server" SkinID="img_Vazio" />
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px;" valign="top" width="100%">
                        <asp:ObjectDataSource ID="odsLiberados" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAllLiberados" TypeName="Actio.Negocio.Capela_Virtual">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px;" valign="top" width="100%">
                        <asp:ObjectDataSource ID="odsBloqueados" runat="server" DeleteMethod="Delete" InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAllBloqueados" TypeName="Actio.Negocio.Capela_Virtual" UpdateMethod="Atualizar">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="nome" Type="String" />
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="status" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="id" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="nome" Type="String" />
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="status" Type="String" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vEdit" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:ImageButton ID="ibt_cancelar" runat="server" CausesValidation="False" 
                            SkinID="ibt_CancelarItem" onclick="ibt_cancelar_Click" />
                    </td>
                    <td align="left" style="padding-left: 100px;" valign="middle">
                        <asp:ImageButton ID="ibt_salvar" runat="server" SkinID="ibt_SalvarItem" 
                            onclick="ibt_salvar_Click" ValidationGroup="valida" />
                        <asp:ImageButton ID="ibt_editar" runat="server" SkinID="ibt_EditarItem" 
                            onclick="ibt_editar_Click" ValidationGroup="valida" />
                        &nbsp;<br /> <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="valida" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="Status_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="1">ativo</asp:ListItem>
                            <asp:ListItem Value="0">inativo</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CheckBox ID="checkAvisar" runat="server" Text="Favor notificar Usuário da liberação do Pedido de Oração no site" Visible="False" />
                        </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        nome:<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Titulo" CssClass="avisos" Display="Dynamic" ErrorMessage="Qual o Nome?" ForeColor="" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Titulo" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        email:<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="Corrija o e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="valida">*</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="Insira o endereço de e-mail" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:TextBox ID="Email" runat="server" ValidationGroup="valida" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" style="padding-top: 20px;" valign="top" width="200px">descrição<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Descricao" CssClass="avisos" Display="Dynamic" ErrorMessage=" Faltou a descrição" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                        :</td>
                    <td align="left" style="padding-left: 10px; padding-top: 10px;" valign="middle">
                        <asp:TextBox runat="server" ID="Descricao" CssClass="EditorHtml" />
                        <asp:Panel ID="PanelIcone" runat="server">
                            <br />
                            <br />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

