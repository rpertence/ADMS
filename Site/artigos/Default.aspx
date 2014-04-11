<%@ Page Title="Administração de Artigos" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="ActioAdms_artigos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:MultiView ID="mvAll" runat="server">
        <asp:View ID="View1" runat="server">

            <br />
            <br />

            <asp:Button ID="bt_novo_artigo" runat="server" Text="+ Novo" OnClick="bt_novo_artigo_Click" />
            &nbsp;|
            <asp:Button ID="bt_autores" runat="server" Text="Autores" OnClick="bt_autores_Click" />
            <br />
            <br />
            Listagem de todos os artigos cadastrados no sistema:<br />
            <br />
            <asp:GridView ID="gridList" runat="server" DataSourceID="odsTodosArtigos" OnRowCommand="ComandosArtigos" EnableModelValidation="True" AutoGenerateColumns="False" Width="100%" AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="autor" SortExpression="id_autor">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("autor_nome") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="EditarAutor" CommandArgument='<%# Bind("id_autor_id") %>' Text='<%# Bind("autor_nome") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle Height="35px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="título" SortExpression="titulo">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("titulo") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="EditarArtigo" Text='<%# Bind("titulo") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="destaques" SortExpression="destaque" />
                    <asp:BoundField DataField="ATIVO" SortExpression="status" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="editar" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="EditarArtigo" ImageUrl="~/App_Themes/ActioAdms/acoes/editar.gif" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="excluir" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="ExcluirArtigo" ImageUrl="~/App_Themes/ActioAdms/acoes/excluir.gif" OnClientClick="if (confirm('Está certo que vai escluir? Exclusões não podem ser desfeitas!') == false) return false;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsTodosArtigos" runat="server" DeleteMethod="ExcluirByIdAutor" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" TypeName="Actio.Negocio.Artigos">
                <DeleteParameters>
                    <asp:Parameter Name="autor" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>

        </asp:View>
        <asp:View ID="View2" runat="server">
            <br />
            <asp:Button ID="bt_novo_autor" runat="server" Text="+ Novo" OnClick="bt_novo_autor_Click" />
            &nbsp;|
            <asp:Button ID="bt_voltar_artigos" runat="server" Text="Artigos" OnClick="bt_voltar_artigos_Click" />
            <br />
            <br />
            <asp:GridView ID="gridAutores" runat="server" DataSourceID="ods_Autores" OnRowCommand="ComandoAutores" EnableModelValidation="True" AutoGenerateColumns="False" AllowSorting="True" CellPadding="10">
                <Columns>
                    <asp:TemplateField HeaderText="Nome" SortExpression="nome">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="EditarAutor" Text='<%# Bind("nome") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Height="35px" />
                        <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="Artigos">artigos deste autor</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="editar" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="EditarAutor" ImageUrl="~/App_Themes/ActioAdms/acoes/editar.gif" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="excluir" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="ExcluirAutor" ImageUrl="~/App_Themes/ActioAdms/acoes/excluir.gif" OnClientClick="if (confirm('Se este autor todos os artigos dele também serão excluidos! Você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ods_Autores" runat="server" DeleteMethod="Delete" InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" TypeName="Actio.Negocio.Artigo_Autor" UpdateMethod="Atualizar">
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="nome" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                    <asp:Parameter Name="descricao" Type="String" />
                    <asp:Parameter Name="icone" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="id" Type="String" />
                    <asp:Parameter Name="nome" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                    <asp:Parameter Name="descricao" Type="String" />
                    <asp:Parameter Name="icone" Type="String" />
                </UpdateParameters>
            </asp:ObjectDataSource>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right" style="padding-right: 15px;" valign="top" width="250px">
                        <asp:ImageButton ID="ibt_cancelar_artigo" runat="server" CausesValidation="False" SkinID="ibt_CancelarItem" OnClick="ibt_cancelar_artigo_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ibt_salvar_artigo" runat="server" SkinID="ibt_SalvarItem" OnClick="ibt_salvar_artigo_Click" ValidationGroup="valida_artigo" />
                        <asp:ImageButton ID="ibt_editar_artigo" runat="server" SkinID="ibt_EditarItem" OnClick="ibt_editar_artigo_Click" ValidationGroup="valida_artigo" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; padding-top: 10px;" valign="top" width="250px">Status:</td>
                    <td style="padding-top: 10px;">
                        <asp:RadioButtonList ID="Status" runat="server" CellPadding="5" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">Inativo</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; padding-top: 10px;" valign="middle" width="250px">Autor:</td>
                    <td style="padding-top: 10px;">
                        <asp:DropDownList ID="ddlAutores" runat="server" DataSourceID="ods_Autores" DataTextField="nome" DataValueField="id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; padding-top: 10px;" valign="top" width="250px">Ordem:</td>
                    <td style="padding-top: 10px;">
                        <asp:TextBox ID="Ordem" runat="server" MaxLength="3" SkinID="Inteiros" Width="40px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ordem" ErrorMessage="Ordene seu artigo" ValidationGroup="valida_artigo"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; padding-top: 10px;" valign="top" width="250px">Data:</td>
                    <td style="padding-top: 10px;">
                        <asp:TextBox ID="Data" runat="server" MaxLength="10" SkinID="Data"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="data" ErrorMessage="qual a data da publicação?" ValidationGroup="valida_artigo"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; padding-top: 10px;" valign="top" width="250px">Destaque:</td>
                    <td style="padding-top: 10px;">
                        <asp:CheckBox ID="Destaque" runat="server" Text="é Destaque" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; padding-top: 10px;" valign="top" width="250px">Título:</td>
                    <td style="padding-top: 10px;">
                        <asp:TextBox ID="Titulo" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="titulo" ErrorMessage="Título necessário" ValidationGroup="valida_artigo"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; padding-top: 10px;" valign="top" width="250px">Resumo:</td>
                    <td style="padding-top: 10px;">
                        <asp:TextBox ID="Resumo" runat="server" MaxLength="145" Rows="5" TextMode="MultiLine" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="resumo" ErrorMessage="faça um resumo da publicação" ValidationGroup="valida_artigo"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; padding-top: 10px;" valign="top" width="250px">Descrição:<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="DescricaoArtigo" ErrorMessage="Qual o artigo?" ValidationGroup="valida_artigo"></asp:RequiredFieldValidator>
                    </td>
                    <td style="padding-top: 10px;">
                        <asp:TextBox runat="server" ID="DescricaoArtigo" CssClass="EditorHtml" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" Width="78px" />
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" height="30px" style="padding-top: 20px; padding-bottom: 10px;" valign="middle" width="200px">
                        <asp:ImageButton ID="ibt_cancelar" runat="server" CausesValidation="False" SkinID="ibt_CancelarItem" OnClick="ibt_cancelar_Click" />
                    </td>
                    <td align="left" style="padding-left: 100px; padding-top: 20px; padding-bottom: 10px;" valign="middle" width="800px">
                        <asp:ImageButton ID="ibt_salvar_autor" runat="server" SkinID="ibt_SalvarItem" OnClick="ibt_salvar_autor_Click" />
                        <asp:ImageButton ID="ibt_editar_autor" runat="server" SkinID="ibt_EditarItem" OnClick="ibt_editar_autor_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">nome:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Nome" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="Nome" CssClass="avisos" ErrorMessage="preencha o nome"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">email:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Email" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="Email" CssClass="avisos" ErrorMessage="preencha o e-mail"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="Email" CssClass="avisos" ErrorMessage="e-mail inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">imagem:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:FileUpload ID="Icone" runat="server" />
                        <asp:HiddenField ID="HidIcone" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="Icone" ErrorMessage="Foto do autor necessária"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">descrição:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Descricao" ErrorMessage="Preencha uma descrição sobre o autor"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="Descricao" CssClass="EditorHtml" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View5" runat="server">


            <br />
            <asp:Button ID="bt_novo_artigo0" runat="server" OnClick="bt_novo_artigo_Click" Text="+ Novo" />
            &nbsp;|
            <asp:Button ID="bt_autores0" runat="server" OnClick="bt_autores_Click" Text="Autores" />
            <br />
            <br />
            <asp:Label ID="LabelAutor" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:GridView ID="gridList0" runat="server" AutoGenerateColumns="False" DataSourceID="odsArtigosAutor" EnableModelValidation="True" OnRowCommand="ComandosArtigos" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="título" SortExpression="titulo">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("titulo") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="EditarArtigo" Text='<%# Bind("titulo") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="destaques" SortExpression="destaque" />
                    <asp:BoundField DataField="ATIVO" SortExpression="status" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="editar0" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="EditarArtigo" ImageUrl="~/App_Themes/ActioAdms/acoes/editar.gif" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="excluir0" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="ExcluirArtigo" ImageUrl="~/App_Themes/ActioAdms/acoes/excluir.gif" OnClientClick="if (confirm('Está certo que vai escluir? Exclusões não podem ser desfeitas!') == false) return false;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsArtigosAutor" runat="server" DeleteMethod="ExcluirByIdAutor" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByIDAutor" TypeName="Actio.Negocio.Artigos">
                <DeleteParameters>
                    <asp:Parameter Name="autor" Type="Int32" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:SessionParameter Name="autor" SessionField="id_autor_selecionado" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>


        </asp:View>
    </asp:MultiView>
</asp:Content>

