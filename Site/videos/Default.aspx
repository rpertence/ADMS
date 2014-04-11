<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="adms_videos_Default" ValidateRequest="false" %>

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
                                            AlternateText='<%# BIND("titulo") %>' CommandArgument='<%# BIND("id") %>' 
                                            CommandName="Alterar" ImageAlign="Middle" 
                                            ImageUrl='<%# "~/App_Themes/ActioAdms/hd/videos/imagens/" + DataBinder.Eval(Container.DataItem, "icone") %>' Width="58px" />
                                        <asp:LinkButton ID="lkNome" runat="server" CommandArgument='<%# BIND("id") %>' 
                                            CommandName="Alterar" CssClass="linksPretos" Text='<%# BIND("titulo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="DISTAK" HeaderText="" SortExpression="destaque">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ATIVO" HeaderText="status" SortExpression="status">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                CommandArgument='<%# BIND("id") %>' CommandName="Excluir" 
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
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:ObjectDataSource ID="odsList" runat="server" OldValuesParameterFormatString="original_{0}" 
                            SelectMethod="SelectAll" TypeName="Actio.Negocio.Videos" 
                            DeleteMethod="DeleteByIdUsuario" InsertMethod="Inserir">
                            <DeleteParameters>
                                <asp:Parameter Name="titulo" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="titulo" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="codigo" Type="String" />
                                <asp:Parameter Name="icone" Type="String" />
                                <asp:Parameter Name="status" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
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
                                    status</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="ativo" />
                                    &nbsp;|
                                    <asp:CheckBox ID="chkDestaque" runat="server" Text="é destaque" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" 
                                        Width="98px" />
                                </td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:FileUpload ID="Icone" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvIcone" runat="server" 
                                        ControlToValidate="Icone" CssClass="avisos" ErrorMessage="imagem é obrigatória" 
                                        ForeColor="" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="HidIcone" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    titulo:</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:TextBox ID="Titulo" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="Titulo" CssClass="avisos" Display="Dynamic" 
                                        ErrorMessage="qual o título do vídeo" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    código:</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:TextBox ID="Codigo" runat="server" Width="300px" Rows="5" 
                                        TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="Codigo" CssClass="avisos" 
                                        ErrorMessage="link do vídeo na internet, coloque o código de incorporação" 
                                        Display="Dynamic" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="150px">
                                    descrição:</td>
                                <td align="left" style="padding: 10px;" valign="top">
                                    <asp:TextBox ID="Descricao" runat="server" Rows="5" TextMode="MultiLine" 
                                        Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="Descricao" CssClass="avisos" Display="Dynamic" 
                                        ErrorMessage="faça uma breve descrição do vídeo" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
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

