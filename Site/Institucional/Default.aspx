<%@ Page Title="Adms | Textos Institucionais" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="adms_Institucional_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:MultiView ID="mvAll" runat="server">
        <asp:View ID="ViewList" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="char" height="30px" valign="middle" colspan="2" 
                        style="padding: 15px">
                        <asp:ImageButton ID="ibt_adicionar" runat="server" CausesValidation="false" 
                            onclick="ibt_adicionar_Click" SkinID="ibt_NovoItem" />
                        &nbsp;<asp:LinkButton ID="lbt_categorias" runat="server" CausesValidation="False" 
                            CssClass="linksPretos" onclick="lbt_categorias_Click">Gerenciar Categorias</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="padding: 10px;" valign="middle" 
                        width="100%">
                            <fieldset style="border: 3px solid #2E477F; padding: 15px; width: auto;">
                            <legend>
                                <asp:DataList ID="dtlCategorias" runat="server" DataSourceID="odsCategorias" 
                                    RepeatDirection="Horizontal" OnItemCommand="ComandoTitulosCategorias">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" width="155px">
                                            <tr>
                                                <td align="center" class="NavegacaoLateral" height="35" valign="middle" 
                                                    width="155px">
                                                    &nbsp;<asp:LinkButton ID="lbt_recurso" runat="server" CausesValidation="False" 
                                                        CommandArgument='<%# Bind("id") %>' CommandName='<%# Bind("titulo") %>' CssClass="links" 
                                                        Text='<%# Bind("titulo") %>'></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            
                            </legend>
                                <br />
                                <asp:Label ID="LabelCategoria" runat="server" CssClass="azul"></asp:Label>
                                <br />
                                <br />
                                <asp:GridView ID="gridList" runat="server" 
                                    DataSourceID="odsList" EnableModelValidation="True" 
                                    OnRowCommand="ComandoDaListagem" Visible="False" 
                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" PageSize="30">
                                    <Columns>
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
                                        <asp:BoundField DataField="destaque" HeaderText="destaque" 
                                            SortExpression="destaque">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ATIVO" HeaderText="status" SortExpression="status">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                    CommandName="Excluir" CommandArgument='<%# BIND("id") %>' 
                                                    onclientclick="if (confirm('você está certo que vai excluir? Se este membro da Equipe for coordenador de ministério este também será excluido!!!! Exclusões não podem ser desfeitas!') == false) return false;" 
                                                    SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Image ID="Image2" runat="server" SkinID="img_Vazio" />
                                    </EmptyDataTemplate>
                                </asp:GridView>                            
                            </fieldset>

                                <asp:ObjectDataSource ID="odsList" runat="server" OldValuesParameterFormatString="original_{0}" 
                                    SelectMethod="selectAllByTipo" TypeName="Actio.Negocio.Textos">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="id_tipo" SessionField="id_categoria" 
                                            Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsCategorias" runat="server" DeleteMethod="Delete" 
                                    InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" 
                                    SelectMethod="SelectAll" TypeName="Actio.Negocio.Textos_Categoria" 
                                    UpdateMethod="Atualizar">
                                    <DeleteParameters>
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="titulo" Type="String" />
                                        <asp:Parameter Name="icone" Type="String" />
                                    </InsertParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="id" Type="String" />
                                        <asp:Parameter Name="titulo" Type="String" />
                                        <asp:Parameter Name="icone" Type="String" />
                                    </UpdateParameters>
                                </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
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
                            onclick="ibt_salvar_Click" />
                        <asp:ImageButton ID="ibt_editar" runat="server" SkinID="ibt_EditarItem" 
                            onclick="ibt_editar_Click" />
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="Titulo" CssClass="avisos" ErrorMessage="qual o título?" 
                            ForeColor=""></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="Descricao" CssClass="avisos" 
                            ErrorMessage=" faltou a descrição"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        Categoria:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:DropDownList ID="Categoria" runat="server" DataSourceID="odsCategorias" 
                            DataTextField="titulo" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="MostraPanelIcone">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:Panel ID="panelEquipe" runat="server" Visible="False">
                            Selecione quem&nbsp;Coordena este Ministério:
                            <asp:DropDownList ID="ddl_id_coordenador" runat="server" DataSourceID="odsEquipe" DataTextField="titulo" DataValueField="id">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="odsEquipe" runat="server" DeleteMethod="Delete" InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectCoordenadorMinisterio" TypeName="Actio.Negocio.Equipe" UpdateMethod="Atualizar">
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
                            <asp:HiddenField ID="HidEquipe" runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">&nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">ativo</asp:ListItem>
                            <asp:ListItem Value="0">inativo</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:CheckBox ID="Destaque" runat="server" Text="é destaque" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        titulo:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Titulo" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        resumo:</td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:TextBox ID="Resumo" runat="server" MaxLength="245" Rows="4" 
                            TextMode="MultiLine" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" style="padding-top: 20px;" valign="top" 
                        width="200px">
                        descrição:</td>
                    <td align="left" style="padding-left: 10px; padding-top: 10px;" valign="middle">
                        <asp:TextBox runat="server" ID="Descricao" CssClass="EditorHtml" />
                        <asp:Panel ID="PanelIcone" runat="server">
                            <br />
                            <asp:Label ID="Labelintrui" runat="server"></asp:Label>
                            <br />
                            <br />
                            <asp:FileUpload ID="Icone" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvIcone" runat="server" ControlToValidate="Icone" CssClass="avisos" ErrorMessage="imagem é obrigatória para esta categoria" ForeColor=""></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="HidIcone" runat="server" />
                            <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" Width="98px" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewCategoriasList" runat="server">
            <br />
            <asp:ImageButton ID="ibt_retornar" runat="server" CausesValidation="False" 
                ImageAlign="Middle" onclick="ibt_retornar_Click" SkinID="ibt_Voltar" />
            <br />
            <br />
            Listagem de Categorias de Textos&nbsp;
            <br />
            <br />
            <br />
            <asp:ImageButton ID="ibt_adicionarCategoria" runat="server" 
                CausesValidation="False" ImageAlign="Middle" SkinID="ibt_NovoItem" 
                onclick="ibt_adicionarCategoria_Click" />
            <br />
            <br />
            <asp:GridView ID="GridCategorias" runat="server" AutoGenerateColumns="False" 
                DataSourceID="odsCategorias" EnableModelValidation="True"
                OnRowCommand="ComandoGridCategoria">
                <Columns>
                    <asp:TemplateField HeaderText="titulo" SortExpression="titulo">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandArgument='<%# BIND("id") %>' CssClass="linksPretos" 
                                Text='<%# BIND("titulo") %>' CommandName="Editar"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                CommandName="Excluir" CommandArgument='<%# BIND("id") %>' 
                                onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas! Todos os textos desta categoria serão excluidos!') == false) return false;" 
                                ToolTip="Excluir este item" SkinID="ibt_ExcluirItem" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:View>
        <asp:View ID="ViewCategoriasEdit" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right" height="30px" valign="top" width="200px">
                        <asp:ImageButton ID="ibt_cancelarCategoria" runat="server" CausesValidation="False" 
                           SkinID="ibt_CancelarItem" onclick="ibt_cancelarCategoria_Click" 
                            Height="28px" />
                    </td>
                    <td align="left" style="padding-left: 100px; padding-bottom: 20px;" 
                        valign="top">
                        <asp:ImageButton ID="ibt_salvarCategoria" runat="server" 
                            SkinID="ibt_SalvarItem" onclick="ibt_salvarCategoria_Click" />
                        <asp:ImageButton ID="ibt_editarCategoria" runat="server" 
                            SkinID="ibt_EditarItem" onclick="ibt_editarCategoria_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        título:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="TituloCategoria" runat="server" MaxLength="90" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTituloCategoria" runat="server" 
                            CssClass="avisos" ErrorMessage="título Obrigatorio" 
                            ControlToValidate="TituloCategoria"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>
</asp:Content>

