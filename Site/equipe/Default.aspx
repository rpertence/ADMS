<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" ValidateRequest="false" Inherits="ActioAdms_equipe_Default" %>
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
                        <asp:GridView ID="gridList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsEquipe" EnableModelValidation="True" OnRowCommand="ComandoDaListagem" Visible="False" PageSize="50">
                            <Columns>
                                <asp:TemplateField HeaderText="nome" SortExpression="titulo"><ItemTemplate><asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="Editar" CssClass="linksPretos" Text='<%# Bind("titulo") %>'></asp:LinkButton></ItemTemplate><HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" /><ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" /></asp:TemplateField>
                                <asp:BoundField DataField="destaques" HeaderText="Tipo" SortExpression="destaques" />
                                <asp:BoundField DataField="status" HeaderText="status" SortExpression="status"><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" /><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>
                                <asp:TemplateField><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandArgument='<%# Bind("id") %>' CommandName="Excluir" onclientclick="if (confirm('Se este membro da Equipe for coordenador de Minstério este também será exlcuido, se não for coordenador de Ministério somente este item será excluido, você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" /></ItemTemplate></asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Image ID="Image2" runat="server" SkinID="img_Vazio" />
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="odsEquipe" runat="server" DeleteMethod="Delete" InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" TypeName="Actio.Negocio.Equipe" UpdateMethod="Atualizar">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="titulo" Type="String" />
                                <asp:Parameter Name="resumo" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="icone" Type="String" />
                                <asp:Parameter Name="ativo" Type="String" />
                                <asp:Parameter Name="destaque" Type="String" />
                                <asp:Parameter Name="ordem" Type="String" />
                                <asp:Parameter Name="email" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="id" Type="String" />
                                <asp:Parameter Name="titulo" Type="String" />
                                <asp:Parameter Name="resumo" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="icone" Type="String" />
                                <asp:Parameter Name="ativo" Type="String" />
                                <asp:Parameter Name="destaque" Type="String" />
                                <asp:Parameter Name="ordem" Type="String" />
                                <asp:Parameter Name="email" Type="String" />
                            </UpdateParameters>
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
                        <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">ativo</asp:ListItem>
                            <asp:ListItem Value="0">inativo</asp:ListItem>
                        </asp:RadioButtonList>
                        </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:RadioButtonList ID="Destaque" runat="server" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">Coordenador de Ministério</asp:ListItem>
                            <asp:ListItem Value="1">Coordenador de Forania</asp:ListItem>
                            <asp:ListItem Value="2">Equipe Arquidiocesana</asp:ListItem>
                        </asp:RadioButtonList>
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
                    <td align="right" height="30px" valign="middle" width="200px">Resumo:<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Resumo" Display="Dynamic" ErrorMessage="Faças um resumo" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:TextBox ID="Resumo" runat="server" MaxLength="245" Rows="4" TextMode="MultiLine" Width="300px"></asp:TextBox>
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

