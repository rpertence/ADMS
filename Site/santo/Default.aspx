<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True" CodeBehind="Default.aspx.cs"  ValidateRequest="false" Inherits="ActioAdms_santo_Default" %>
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
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px;" valign="top" width="100%">
                        <asp:GridView ID="gridList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsEquipe" EnableModelValidation="True" OnRowCommand="ComandoDaListagem" Visible="False" PageSize="31">
                            <Columns>
                                <asp:TemplateField HeaderText="nome" SortExpression="titulo"><ItemTemplate><asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="Editar" CssClass="linksPretos" Text='<%# Bind("nome") %>'></asp:LinkButton></ItemTemplate><HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" /><ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" /></asp:TemplateField>
                                <asp:BoundField DataField="mes" HeaderText="mês" SortExpression="mes" />
                                <asp:BoundField DataField="dia" HeaderText="dia" SortExpression="dia"><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" /><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>
                                <asp:TemplateField><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandArgument='<%# Bind("id") %>' CommandName="Excluir" onclientclick="if (confirm('Se este membro da Equipe for coordenador de Minstério este também será exlcuido, se não for coordenador de Ministério somente este item será excluido, você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" /></ItemTemplate></asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Image ID="Image2" runat="server" SkinID="img_Vazio" />
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="odsEquipe" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" TypeName="Actio.Negocio.Santo">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px;" valign="top" width="100%">&nbsp;</td>
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
                        &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="valida" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" Width="98px" />
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        &nbsp;</td>
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
                    <td align="right" height="30px" valign="middle" 
                        width="200px">
                        foto:<asp:RequiredFieldValidator ID="rfvIcone" runat="server" ControlToValidate="Icone" CssClass="avisos" Display="Dynamic" ErrorMessage="Faltou a foto" ForeColor="" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:FileUpload ID="Icone" runat="server" />
                        <asp:HiddenField ID="HidIcone" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">dia:<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Dia" Display="Dynamic" ErrorMessage="qual o dia?" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:TextBox ID="Dia" runat="server" MaxLength="2" Width="25px" SkinID="Inteiros" ValidationGroup="valida"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">mês:<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Mes" Display="Dynamic" ErrorMessage="Mes" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:TextBox ID="Mes" runat="server" MaxLength="2" SkinID="Inteiros" Width="25px"></asp:TextBox>
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

