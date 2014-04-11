<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" ValidateRequest="false" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="adms_fotos_Default" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
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
                        &nbsp;<asp:LinkButton ID="lbt_albuns" runat="server" CausesValidation="False" 
                            CssClass="linksPretos" onclick="lbt_albuns_Click">Gerenciar albuns</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="padding: 10px;" valign="middle" 
                        width="100%">
                            <fieldset style="border: 3px solid #2E477F; padding: 15px; width: auto;">
                            <legend>
                                <asp:DataList ID="dtlalbuns" runat="server" DataSourceID="odsalbuns" 
                                    RepeatDirection="Horizontal" OnItemCommand="ComandoTitulosalbuns">
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
                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="titulo" SortExpression="titulo"><ItemTemplate><asp:ImageButton ID="ImageButtonID" runat="server" 
                                                    CommandArgument='<%# BIND("id") %>' CommandName="Editar" ImageAlign="Middle" 
                                                    ImageUrl='<%#"~/App_Themes/ActioAdms/hd/foto_album/icones/" + DataBinder.Eval(Container.DataItem, "icone") %>' 
                                                    Width="68px" /><asp:LinkButton ID="LinkButton1" runat="server" 
                                                    CommandArgument='<%# BIND("id") %>' CommandName="Editar" CssClass="linksPretos" 
                                                    Text='<%# BIND("titulo") %>'></asp:LinkButton></ItemTemplate><HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" /><ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" 
                                                Width="300px" /></asp:TemplateField>
                                        <asp:BoundField DataField="destaque" HeaderText="destaque" 
                                            SortExpression="destaque"><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" /><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>
                                        <asp:BoundField DataField="ATIVO" HeaderText="status" SortExpression="status"><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" /><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>
                                        <asp:TemplateField HeaderText="fotos" Visible="False"><ItemTemplate><asp:ImageButton ID="ImageButtonResumo" runat="server" 
                                                    CommandArgument='<%# BIND("id") %>' CommandName="Fotos" ImageAlign="Middle" 
                                                    ImageUrl="~/App_Themes/ActioAdms/imagensSuporte/midia.png" 
                                                    ToolTip='<%# BIND("resumo") %>' /></ItemTemplate><EditItemTemplate><asp:TextBox 
                                                ID="TextBox1" runat="server"></asp:TextBox></EditItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" /></asp:TemplateField>
                                        <asp:TemplateField><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                    CommandName="Excluir" CommandArgument='<%# BIND("id") %>' 
                                                    onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" 
                                                    SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" /></ItemTemplate></asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Image ID="Image2" runat="server" SkinID="img_Vazio" />
                                    </EmptyDataTemplate>
                                </asp:GridView>                            
                            </fieldset>

                                <asp:ObjectDataSource ID="odsList" runat="server" OldValuesParameterFormatString="original_{0}" 
                                    SelectMethod="selectAllByTipo" TypeName="Actio.Negocio.Foto_Album" 
                                DeleteMethod="ExcluirByIdTipo">
                                    <DeleteParameters>
                                        <asp:Parameter Name="id_tipo" Type="String" />
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:SessionParameter Name="id_tipo" SessionField="id_categoria" 
                                            Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsalbuns" runat="server" 
                                DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" 
                                    SelectMethod="SelectAll" TypeName="Actio.Negocio.Foto_Categoria">
                                    <DeleteParameters>
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </DeleteParameters>
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
                    <td align="right" height="30px" valign="middle" class="style2">
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
                    <td align="right" height="30px" valign="middle" class="style2">
                        Categoria:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:DropDownList ID="Categoria" runat="server" DataSourceID="odsalbuns" 
                            DataTextField="titulo" DataValueField="id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" class="style2">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:RadioButtonList ID="Status" runat="server" CellPadding="5" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
                            <asp:ListItem Value="0">Inativo</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" class="style2">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:CheckBox ID="Destaque" runat="server" Text="é destaque" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="middle" class="style2">
                        <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" 
                            Width="98px" />
                    </td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:FileUpload ID="Icone" runat="server" />
                        <br />
                        <asp:RequiredFieldValidator ID="rfvIcone" runat="server" 
                            ControlToValidate="Icone" CssClass="avisos" ErrorMessage="imagem é obrigatória" 
                            ForeColor=""></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="HidIcone" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" class="style2">
                        titulo:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="Titulo" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" class="style2">
                        COLE AQUI 0 HTML
                        <br />
                        DO ALBUM DE FOTOS:</td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:TextBox ID="Resumo" runat="server" MaxLength="245" Rows="8" 
                            TextMode="MultiLine" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" style="padding-top: 20px;" valign="middle" 
                        class="style2">
                        Descrição:</td>
                    <td align="left" style="padding-left: 10px; padding-top: 10px;" valign="middle">
                        <asp:TextBox runat="server" ID="Descricao" CssClass="EditorHtml" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewalbunsList" runat="server">
            <br />
            <asp:ImageButton ID="ibt_retornar" runat="server" CausesValidation="False" 
                ImageAlign="Middle" onclick="ibt_retornar_Click" SkinID="ibt_Voltar" />
            <br />
            <br />
            Listagem de albuns de Fotos<br />
            <br />
            <br />
            <asp:ImageButton ID="ibt_adicionarCategoria" runat="server" 
                CausesValidation="False" ImageAlign="Middle" SkinID="ibt_NovoItem" 
                onclick="ibt_adicionarCategoria_Click" />
            <br />
            <br />
            <asp:GridView ID="Gridalbuns" runat="server" AutoGenerateColumns="False" 
                DataSourceID="odsalbuns" EnableModelValidation="True"
                OnRowCommand="ComandoGridCategoria">
                <Columns>
                    <asp:TemplateField HeaderText="titulo" SortExpression="titulo"><ItemTemplate><asp:ImageButton ID="ImageButtonTitulo" runat="server" 
                                CommandArgument='<%# BIND("id") %>' CommandName="Editar" ImageAlign="Middle" 
                                ImageUrl='<%#"~/App_Themes/ActioAdms/hd/foto_album/albuns/" + DataBinder.Eval(Container.DataItem, "icone") %>' 
                                Width="68px" /><asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandArgument='<%# BIND("id") %>' CssClass="linksPretos" 
                                Text='<%# BIND("titulo") %>' CommandName="Editar"></asp:LinkButton></ItemTemplate><HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" /><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" /></asp:TemplateField>
                    <asp:TemplateField><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                CommandName="Excluir" CommandArgument='<%# BIND("id") %>' 
                                onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas! Todos os textos desta categoria serão excluidos!') == false) return false;" 
                                ToolTip="Excluir este item" SkinID="ibt_ExcluirItem" /></ItemTemplate><ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" /></asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:View>
        <asp:View ID="ViewalbunsEdit" runat="server">
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
                    <td align="right" valign="middle" width="150px">
                        <asp:Image ID="IconePostadoCategoria" runat="server" ImageAlign="Middle" 
                            Visible="False" Width="98px" />
                    </td>
                    <td align="left" style="padding: 10px;" valign="middle">
                        <asp:FileUpload ID="IconeCategoria" runat="server" />
                        <br />
                        <asp:RequiredFieldValidator ID="rfvIconeCategoria" runat="server" 
                            ControlToValidate="IconeCategoria" CssClass="avisos" 
                            ErrorMessage="imagem obrigatória!"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="HidIconeCategoria" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        título:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="TituloCategoria" runat="server" MaxLength="90" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTituloCategoria" runat="server" 
                            ControlToValidate="TituloCategoria" CssClass="avisos" 
                            ErrorMessage="título Obrigatorio"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        descricao</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:TextBox ID="DescricaoCategoria" runat="server" MaxLength="245" Rows="5" 
                            TextMode="MultiLine" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ControlToValidate="DescricaoCategoria" CssClass="avisos" 
                            ErrorMessage="o que haverá nesta categoria?"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewFotoList" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" style="padding: 10px" valign="top" width="100%">
                        <asp:ImageButton ID="ibt_retornar0" runat="server" CausesValidation="False" 
                            Height="26px" ImageAlign="Middle" onclick="ibt_retornar_Click" 
                            SkinID="ibt_Voltar" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px" valign="top" width="100%">
                        Fotografias do Álbum:
                        <asp:Label ID="LabelTituloAlbum" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px" valign="top" width="100%">
                        <asp:Panel ID="PanelListFotos" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="style1">
                                        </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 10px;">
                                        <asp:ImageButton ID="ibt_novaFoto" runat="server" ImageAlign="Middle" 
                                            onclick="ibt_novaFoto_Click" SkinID="ibt_NovoItem" 
                                            ToolTip="Adicionar nova foto ao álbum" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridFotos" runat="server" AutoGenerateColumns="False" 
                                            DataSourceID="odsFotosAlbum" EnableModelValidation="True" Width="70%"
                                            OnRowCommand="EditarFoto">
                                            <Columns>
                                                <asp:TemplateField HeaderText="título" SortExpression="titulo"><ItemTemplate>
                                                    <asp:ImageButton ID="ImageButtonID" runat="server" 
                                                            CommandArgument='<%# BIND("id") %>' CommandName="Editar" ImageAlign="Middle" 
                                                            ImageUrl='<%# "~/App_Themes/ActioAdms/hd/albuns/" + DataBinder.Eval(Container.DataItem, "id_album") + "/" + DataBinder.Eval(Container.DataItem, "miniatura") %>' 
                                                            Width="78px" 
                                                        ToolTip='<%# "editar o arquivo" + DataBinder.Eval(Container.DataItem, "titulo") %>' />&nbsp;<asp:LinkButton ID="LinkButton3" runat="server" 
                                                            CommandArgument='<%# BIND("id") %>' CssClass="linksPretos" 
                                                            Text='<%# BIND("titulo") %>' CommandName="Editar"></asp:LinkButton></ItemTemplate><HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" /><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" /></asp:TemplateField>
                                                <asp:TemplateField><ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                            CommandArgument='<%# BIND("id") %>' CommandName="Excluir" 
                                                            onclientclick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" 
                                                            SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" /></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" /></asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ObjectDataSource ID="odsFotosAlbum" runat="server" 
                                            OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByIDAlbum" 
                                            TypeName="Actio.Negocio.Foto" DeleteMethod="DeleteByArquivo" 
                                            InsertMethod="Inserir" UpdateMethod="AtualizarTitulo">
                                            <DeleteParameters>
                                                <asp:Parameter Name="arquivo" Type="String" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="id_Album" Type="String" />
                                                <asp:Parameter Name="titulo" Type="String" />
                                                <asp:Parameter Name="arquivo" Type="String" />
                                                <asp:Parameter Name="miniatura" Type="String" />
                                                <asp:Parameter Name="ordem" Type="String" />
                                                <asp:Parameter Name="destaque" Type="String" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="hidAlbumid" Name="id_Album" 
                                                    PropertyName="Value" Type="Int32" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="id" Type="String" />
                                                <asp:Parameter Name="titulo" Type="String" />
                                            </UpdateParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px" valign="top" width="100%">
                        <asp:Panel ID="PanelEdit" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" height="30px" valign="top" width="200px">
                                        <asp:HiddenField ID="hidAlbumid" runat="server" />
                                    </td>
                                    <td align="left" 
                                        valign="top">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="avisos" 
                                            ValidationGroup="validacao" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" height="30px" valign="top" width="200px">
                                        <asp:ImageButton ID="ibt_cancelarFoto" runat="server" CausesValidation="False" 
                                            Height="28px" onclick="ibt_cancelarFoto_Click" SkinID="ibt_CancelarItem" />
                                    </td>
                                    <td align="left" style="padding-left: 100px; padding-bottom: 20px;" 
                                        valign="top">
                                        <asp:ImageButton ID="ibt_salvarFoto" runat="server" 
                                            onclick="ibt_salvarFoto_Click" SkinID="ibt_SalvarItem"
                                            ValidationGroup="validacao" />
                                        <asp:ImageButton ID="ibt_editarFoto" runat="server" SkinID="ibt_EditarItem" 
                                            ValidationGroup="validacao" onclick="ibt_editarFoto_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">
                                        <asp:Image ID="IconePostadoFotoMiniatura" runat="server" ImageAlign="Middle" 
                                            Visible="False" Width="98px" />
                                    </td>
                                    <td align="left" style="padding: 10px;" valign="middle">
                                        miniatura 148px largura 122px altura:
                                        <asp:RequiredFieldValidator ID="rfvIconeFoto0" runat="server" 
                                            ControlToValidate="fpMiniatura" CssClass="avisos" Display="Dynamic" 
                                            ErrorMessage="imagem obrigatória!" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                        <asp:FileUpload ID="fpMiniatura" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">
                                        <asp:Image ID="IconePostadoFoto" runat="server" ImageAlign="Middle" 
                                            Visible="False" Width="98px" />
                                    </td>
                                    <td align="left" style="padding: 10px;" valign="middle">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; foto com 580 px de largura:
                                        <asp:RequiredFieldValidator ID="rfvIconeFoto" runat="server" 
                                            ControlToValidate="IconeFoto" CssClass="avisos" Display="Dynamic" 
                                            ErrorMessage="imagem obrigatória!" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                        <asp:FileUpload ID="IconeFoto" runat="server" />
                                        <br />
                                        <asp:HiddenField ID="HidIconeFoto" runat="server" />
                                        <asp:HiddenField ID="HidMiniatura" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="200px">
                                        ordem:</td>
                                    <td align="left" style="padding: 10px;" valign="middle">
                                        <asp:TextBox ID="Ordem" runat="server" MaxLength="3" SkinID="Inteiros" 
                                            Width="50px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTituloCategoria1" runat="server" 
                                            ControlToValidate="Ordem" CssClass="avisos" Display="Dynamic" 
                                            ErrorMessage="ordem de exibição Obrigatorio" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" height="30px" valign="middle" width="200px">
                                        título:</td>
                                    <td align="left" style="padding-left: 10px;" valign="middle">
                                        <asp:TextBox ID="TituloFoto" runat="server" MaxLength="90" Width="150px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTituloCategoria0" runat="server" 
                                            ControlToValidate="TituloFoto" CssClass="avisos" Display="Dynamic" 
                                            ErrorMessage="título Obrigatorio" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
