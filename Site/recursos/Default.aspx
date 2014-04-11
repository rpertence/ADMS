<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True" ValidateRequest="false" CodeBehind="Default.aspx.cs" Inherits="adms_emailmarketing_Default" Title="Actio Comunicação | Administre os Textos do seu Site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:Panel ID="PanelList" runat="server" Visible="False">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="60">
                    <asp:Image ID="ImageDetalhe" runat="server" 
                        ImageUrl="~/imagens/recursos/recursos/detalhe.png" />
                </td>
                <td>
                    <asp:Label ID="LabelDetalheRecurso" runat="server" CssClass="titulos" 
                        Text="Agora você está na listagem dos itens cadastrados"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td valign="middle" height="40px">
                    <asp:ImageButton ID="ibt_NovoList" runat="server" ImageAlign="Middle" 
                        ImageUrl="~/imagens/recursos/recursos/novo.png" onclick="ibt_Novo_Click" 
                        Width="25px" CausesValidation="False" />
                    &nbsp;<asp:LinkButton ID="lbt_Novo0" runat="server" CssClass="linksAzul" 
                        onclick="lbt_Novo_Click" CausesValidation="False">Adicione um novo item</asp:LinkButton>
                    </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td>
                    <asp:GridView ID="gridList" runat="server" AllowPaging="True" 
                    OnRowCommand="RowCommand"
                        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="ods_List" 
                        EnableModelValidation="True" Width="80%">
                        <Columns>
                            <asp:TemplateField HeaderText="recursos" SortExpression="recruso">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibt_Item" runat="server" 
                                        CommandArgument='<%# BIND("id") %>' CommandName="Alterar" 
                                        ImageAlign="Middle" 
                                        ImageUrl='<%# "~/imagens/recursos/" + DataBinder.Eval(Container.DataItem, "icone") %>' 
                                        Width="28px" />
                                    &nbsp;<asp:LinkButton ID="lkNome" runat="server" CommandArgument='<%# BIND("id") %>' 
                                        CommandName="Alterar" CssClass="linksPretos" Text='<%# BIND("titulo") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibt_excluir" runat="server" 
                                        AlternateText="Excluir este item" CommandArgument='<%# BIND("id") %>' 
                                        CommandName="Excluir" ImageAlign="Middle" 
                                        ImageUrl="~/App_Themes/ActioAdms/botoes/excluir.png" 
                                        
                                        onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Image ID="Image2" runat="server" ImageAlign="Middle" 
                                ImageUrl="~/imagens/recursos/recursos/vazio.png" />
                            &nbsp;Não há itens cadastrados no sistema!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ods_List" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="SelectAll" TypeName="Actio.Negocio.Recursos">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="String" />
                        </DeleteParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelEdit" runat="server" Visible="False">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td width="60">
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/imagens/recursos/recursos/detalhe.png" />
                </td>
                <td>
                    <asp:Label ID="LabelTituloEdit" runat="server" CssClass="titulos" 
                        Text="Agora você está na edição detalhada"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td valign="middle" height="40px">
                    &nbsp;
                    <asp:ImageButton ID="ibtList_Edit" runat="server" ImageAlign="Middle" 
                        ImageUrl="~/imagens/recursos/recursos/OK.png" onclick="ibtList_Click" 
                        Width="28px" CausesValidation="False" />
                    &nbsp;<asp:LinkButton ID="lbtList_Edit" runat="server" CssClass="linksAzul" 
                        onclick="lbtList_Click" CausesValidation="False">Visualize os itens cadastrados</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" valign="middle" width="150px">
                                <asp:ImageButton ID="ibt_cancelar" runat="server" CausesValidation="False" 
                                    ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png" onclick="ibt_cancelar_Click" />
                            </td>
                            <td align="center" style="padding: 8px;" valign="middle">
                                <asp:ImageButton ID="ibt_Alterar" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/App_Themes/ActioAdms/botoes/editar.png" onclick="ibt_Alterar_Click" 
                                    Visible="False" />
                                <asp:ImageButton ID="ibt_Salvar" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png" onclick="ibt_Salvar_Click" 
                                    Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="150px">
                                titulo:</td>
                            <td align="left" style="padding: 8px;" valign="top">
                                <asp:TextBox ID="Titulo" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="150px">
                                url:</td>
                            <td align="left" style="padding: 8px;" valign="top">
                                <asp:TextBox ID="Url" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="150px">
                                icone:</td>
                            <td align="left" style="padding-left: 8px" valign="top">
                                <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" 
                                    Width="28px" />
                                <asp:FileUpload ID="Icone" runat="server" />
                                <asp:HiddenField ID="HidIcone" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    </asp:Content>

