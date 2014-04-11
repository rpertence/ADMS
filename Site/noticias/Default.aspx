<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" ValidateRequest="false" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="ActioAdms_noticias_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:MultiView ID="mvAll" runat="server">
        <asp:View ID="ViewList" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="char" height="30px" valign="middle" 
                        style="padding: 15px">
                        <asp:ImageButton ID="ibt_adicionar" runat="server" CausesValidation="false" 
                            onclick="ibt_adicionar_Click" SkinID="ibt_NovoItem" />
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="char" style="padding: 15px;" valign="middle" height="30px">
                            <asp:GridView ID="gridList" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsList" 
                                EnableModelValidation="True" OnRowCommand="ComandoDaListagem" 
                                PageSize="20">
                                <Columns>
                                    <asp:BoundField DataField="ordem" HeaderText="ordem" SortExpression="ordem" />
                                    <asp:TemplateField HeaderText="titulo" SortExpression="titulo">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" 
                                                CommandArgument='<%# BIND("id") %>' CommandName="Editar" CssClass="linksPretos" 
                                                Text='<%# BIND("titulo") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" 
                                            Width="300px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="distak" HeaderText="destaque" 
                                        SortExpression="destaque">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="distak_b" HeaderText="destaque secundário" 
                                        SortExpression="destaque_b" />
                                    <asp:BoundField DataField="ATIVO" HeaderText="status" SortExpression="status">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                CommandArgument='<%# BIND("id") %>' CommandName="Excluir" 
                                                onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" 
                                                SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Image ID="Image2" runat="server" SkinID="img_Vazio" />
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsList" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" 
                                TypeName="Actio.Negocio.Noticias"></asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewEdit" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center" height="30px" valign="middle" width="200px" colspan="2">
                        <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" Width="250px" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:ImageButton ID="ibt_cancelar" runat="server" CausesValidation="False" 
                            onclick="ibt_cancelar_Click" SkinID="ibt_CancelarItem" />
                    </td>
                    <td align="left" style="padding-left: 100px;" valign="middle">
                        <asp:ImageButton ID="ibt_salvar" runat="server" onclick="ibt_salvar_Click" 
                            SkinID="ibt_SalvarItem" ValidationGroup="valida" />
                        <asp:ImageButton ID="ibt_editar" runat="server" onclick="ibt_editar_Click" 
                            SkinID="ibt_EditarItem" ToolTip="valida" />
                        &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="valida" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:Label ID="LabelContagem" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
                            <asp:ListItem Value="0">Inativo</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" height="35px">
                        <asp:RadioButtonList ID="Destaque" runat="server" CellPadding="5" 
                            CellSpacing="5" RepeatDirection="Horizontal" OnSelectedIndexChanged="Destaque_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Não é destaque</asp:ListItem>
                            <asp:ListItem Value="1">Destaque Principal</asp:ListItem>
                            <asp:ListItem Value="2">Destaque Secundário</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        ordem de exibição:<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Ordem" Display="Dynamic" ErrorMessage="Qual a ordem de exibição?" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" height="35px" style="padding-left: 10px;" valign="middle">
                        &nbsp;<asp:TextBox ID="Ordem" runat="server" MaxLength="4" SkinID="Inteiros" 
                            Width="35px"></asp:TextBox>
                        &nbsp;ex.: 001</td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        data da notícia:
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Data" Display="Dynamic" ErrorMessage="Data da Notícia" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Data" runat="server" MaxLength="10" SkinID="Data" Width="70px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="middle" width="200px" class="auto-style1">
                        imagem:<asp:RequiredFieldValidator ID="RFVIcone" runat="server" ControlToValidate="Icone" Display="Dynamic" ErrorMessage="Como sua notícia está em destaque, é necessária uma imagem!" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px" class="auto-style1">
                        <asp:FileUpload ID="Icone" runat="server" />
                        <asp:HiddenField ID="HidIcone" runat="server" />
                        </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        miniatura:<br /> *Caso não tenha uma imagem a miniatura padrão será usada.<br />
                        <asp:Image ID="MiniaturaPostada" runat="server" ImageAlign="Middle" ImageUrl="~/App_Themes/Site/ImagesSuporte/miniatura_noticia.jpg" Width="120px" />
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:FileUpload ID="Miniatura" runat="server" />
                        <asp:HiddenField ID="HidMiniatura" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">titulo:<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Titulo" CssClass="avisos" Display="Dynamic" ErrorMessage="qual o título?" ForeColor="" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Titulo" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        resumo:<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="Resumo" Display="Dynamic" ErrorMessage="Você se esqueceu do resumo da notícia!" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:TextBox ID="Resumo" runat="server" MaxLength="245" Rows="4" 
                            TextMode="MultiLine" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" style="padding-top: 20px;" valign="top" 
                        width="200px">
                        descrição:<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Descricao" CssClass="avisos" Display="Dynamic" ErrorMessage=" faltou a descrição" ValidationGroup="valida">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="padding-left: 10px; padding-top: 10px;" valign="middle">
                        <asp:TextBox runat="server" ID="Descricao" CssClass="EditorHtml" />
                    </td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>

</asp:Content>

