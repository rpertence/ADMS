<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" ValidateRequest="false" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="ActioAdms_LojaVirtual_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" runat="Server">
    <fieldset style="border: 1px solid #000000; width: 800px; padding: 15px">
        <legend>
            <asp:LinkButton ID="lk_List" runat="server" CssClass="linksPretos"
                OnClick="lk_List_Click" CausesValidation="False">Produtos</asp:LinkButton>&nbsp;|
            <asp:LinkButton
                ID="lk_Banner" runat="server" CssClass="linksPretos"
                CausesValidation="False" OnClick="lk_Banner_Click">Banner da Loja</asp:LinkButton>&nbsp;|
            <asp:LinkButton
                ID="lk_Vendas" runat="server" CssClass="linksPretos" Visible="false"
                CausesValidation="False" OnClick="lk_Vendas_Click">Vendas</asp:LinkButton>
            <asp:LinkButton ID="lk_Clientes"
                runat="server" CssClass="linksPretos" CausesValidation="False" Visible="false"
                OnClick="lk_Clientes_Click">Clientes</asp:LinkButton>

            <asp:LinkButton ID="linkMarcas" runat="server" OnClick="linkMarcas_Click"
                CssClass="linksPretos" CausesValidation="False">Marcas</asp:LinkButton>

            <asp:HyperLink ID="hl_PagSeguro" runat="server" CssClass="linksPretos"
                NavigateUrl="https://acesso.uol.com.br/migration.htm?skin=ps&amp;authenticate=1&amp;dest=http%3A%2F%2Fpagseguro.uol.com.br%2Ftransaction%2Fsearch.jhtml"
                Target="_blank" Visible="False">Financeiro</asp:HyperLink>

            &nbsp;<asp:LinkButton ID="lbt_Doacao" runat="server" OnClick="lbt_Doacao_Click"
                CssClass="linksPretos" CausesValidation="False" Visible="False">Bancos</asp:LinkButton>
            <asp:Label ID="LabelTitulo" runat="server" CssClass="strong"></asp:Label>

        </legend>
        <asp:MultiView ID="mvProdutos" runat="server">
            <asp:View ID="vList" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="padding: 5px" align="char">
                            <asp:Image ID="ImageRecurso" runat="server" ImageAlign="Middle"
                                ImageUrl="~/LojaVirtual/imagens/OK.png" Width="35px" />
                            &nbsp;<asp:Label ID="Label3" runat="server" Text="Listagem dos produtos"></asp:Label>
                            <asp:ObjectDataSource ID="odsProdutos" runat="server" DeleteMethod="Excluir"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="selectAll"
                                TypeName="Actio.Negocio.Produtos">
                                <DeleteParameters>
                                    <asp:Parameter Name="id" Type="String" />
                                </DeleteParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td align="char" style="padding: 5px">
                            <asp:Button ID="bt_NovoProduto" runat="server" CausesValidation="False"
                                OnClick="bt_NovoProduto_Click" Text="+ novo produto" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding: 5px" valign="top" width="100%">
                            <asp:GridView ID="GridProdutos" runat="server" AllowPaging="True"
                                AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsProdutos"
                                EnableModelValidation="True" OnRowCommand="ListProdutosRowCommand" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="produto" SortExpression="titulo">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibt_Item0" runat="server"
                                                AlternateText='<%# Bind("ProdDescricao_") %>' CausesValidation="False"
                                                CommandArgument='<%# Bind("id") %>' CommandName="Alterar" ImageAlign="Middle"
                                                ImageUrl='<%# "~/App_Themes/ActioAdms/hd/produtos/icones/" + DataBinder.Eval(Container.DataItem, "icone") %>'
                                                ToolTip="Alterar ou atualizar este produto" Width="68px" />
                                            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server"
                                                CommandArgument='<%# Bind("id") %>' CommandName="Alterar"
                                                CssClass="linksPretos" Text='<%# Bind("ProdDescricao_") %>'
                                                ToolTip="Alterar ou atualizar este produto"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="categoria" HeaderText="categoria"
                                        SortExpression="id_categoria">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="subcategoria" HeaderText="sub-categoria"
                                        SortExpression="id_subcategoria">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="estoque" HeaderText="estoque"
                                        SortExpression="estoque">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server"
                                                AlternateText="Descrever o produto" CommandArgument='<%# Bind("id") %>'
                                                CommandName="Descricao" Height="28px" ImageAlign="Middle"
                                                ImageUrl="~/LojaVirtual/imagens/editartexto.png" ToolTip="descrição do produto" Width="30px" />
                                            &nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton1" runat="server"
                                            CommandArgument='<%# Bind("id") %>' CommandName="Fotos" ImageAlign="Middle"
                                            ImageUrl="~/LojaVirtual/imagens/fotosproduto.png"
                                            ToolTip="Incluir imagens e descrição para este produto" Width="30px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibt_excluir0" runat="server"
                                                AlternateText="Excluir este item" CausesValidation="False"
                                                CommandArgument='<%# Bind("id") %>' CommandName="Excluir" ImageAlign="Middle"
                                                ImageUrl="~/App_Themes/ActioAdms/botoes/excluir.png"
                                                OnClientClick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;"
                                                ToolTip="Ao apagar um produtos todas as imagens serão apagadas também" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Image ID="Image7" runat="server" ImageAlign="Middle"
                                        ImageUrl="~/LojaVirtual/imagens/vazio.png" Width="38px" />
                                    Não há produtos cadastrados em sua loja
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vEdit" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="150px" style="padding: 5px" align="right">
                            <asp:Image ID="Image1" runat="server" ImageAlign="Middle"
                                ImageUrl="~/LojaVirtual/imagens/detalhe.png" Width="35px" />
                            &nbsp;</td>
                        <td style="padding: 5px">Detalhes de produto</td>
                    </tr>
                    <tr>
                        <td align="right" style="padding: 5px" width="150px" valign="top">
                            <asp:ImageButton ID="ibt_listagemProdutos" runat="server"
                                CausesValidation="False" Height="25px" ImageAlign="Middle"
                                ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png"
                                ToolTip="Voltar para listagem de Produtos"
                                OnClick="ibt_listagemProdutos_Click" />
                        </td>
                        <td style="padding: 5px">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">&nbsp;</td>
                                    <td align="center" style="padding: 5px" valign="middle">

                                        <asp:ImageButton ID="Atualizar" runat="server" OnClick="Atualizar_Click" SkinID="ibt_EditarItem" />
                                        <asp:ImageButton ID="Salvar" runat="server" OnClick="Salvar_Click" SkinID="ibt_SalvarItem" />

                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">&nbsp;</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <fieldset style="border: 1px solid #000000; padding: 10px">
                                            <legend>categorias e sub-categorias</legend>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="left" valign="middle" width="250px">
                                                        <asp:Image ID="ImageCategoria" runat="server" ImageAlign="Middle"
                                                            ImageUrl="~/App_Themes/Site/ImagesSuporte/mini_logo.gif" Width="48px" />
                                                        <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="True"
                                                            DataSourceID="odsCanal" DataTextField="titulo" DataValueField="id"
                                                            OnSelectedIndexChanged="MudarDeCategoria">
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td align="left" valign="top" width="200px" rowspan="2"
                                                        style="padding-left: 10px; border-left-style: dotted; border-left-width: 1px; border-left-color: #808000">
                                                        <br />
                                                        sub-cateogiras:<asp:RadioButtonList ID="rdb_SubCategorias" runat="server"
                                                            DataSourceID="ods_Subcategorias" DataTextField="titulo" DataValueField="id">
                                                        </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                                                            ControlToValidate="rdb_SubCategorias" ErrorMessage="selecione sub_categoria"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle" style="padding-right: 10px">
                                                        <asp:ImageButton ID="ibt_NovoCanal" runat="server" CausesValidation="False"
                                                            ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/adicionar.png"
                                                            OnClick="ibt_NovoCanal_Click" />
                                                        <asp:ImageButton ID="ibt_AlterarCanal" runat="server" CausesValidation="False"
                                                            ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/editar.png"
                                                            OnClick="ibt_AlterarCanal_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <asp:ObjectDataSource ID="odsCanal" runat="server" DeleteMethod="Delete"
                                            InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="SelectAll" TypeName="Actio.Negocio.Produtos_Categoria"
                                            UpdateMethod="Atualizar">
                                            <DeleteParameters>
                                                <asp:Parameter Name="id" Type="String" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="titulo" Type="String" />
                                                <asp:Parameter Name="icone" Type="String" />
                                                <asp:Parameter Name="destaque" Type="String" />
                                            </InsertParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="id" Type="String" />
                                                <asp:Parameter Name="titulo" Type="String" />
                                                <asp:Parameter Name="icone" Type="String" />
                                                <asp:Parameter Name="destaque" Type="String" />
                                            </UpdateParameters>
                                        </asp:ObjectDataSource>
                                        <asp:ObjectDataSource ID="ods_Subcategorias" runat="server"
                                            DeleteMethod="Delete" InsertMethod="Inserir"
                                            OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="SelectByIDCategoria"
                                            TypeName="Actio.Negocio.Produtos_Sub_Categoria"
                                            UpdateMethod="Atualizar">
                                            <DeleteParameters>
                                                <asp:Parameter Name="id" Type="String" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="titulo" Type="String" />
                                                <asp:Parameter Name="icone" Type="String" />
                                                <asp:Parameter Name="id_categoria" Type="String" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlCategoria" Name="id_categoria"
                                                    PropertyName="SelectedValue" Type="Int32" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="id" Type="String" />
                                                <asp:Parameter Name="titulo" Type="String" />
                                                <asp:Parameter Name="icone" Type="String" />
                                                <asp:Parameter Name="id_categoria" Type="String" />
                                            </UpdateParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">marca:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:DropDownList runat="server" ID="ddlMarca" DataSourceID="odsMarca" AppendDataBoundItems="true" DataValueField="id" DataTextField="descricao">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ddlMarca" ErrorMessage="marca"></asp:RequiredFieldValidator>
                                        <asp:ObjectDataSource ID="odsMarca" runat="server"
                                            SelectMethod="SelectAll"
                                            TypeName="Actio.Negocio.Marca">
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">titulo:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:TextBox ID="ProdDescricao_" runat="server" MaxLength="100" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="ProdDescricao_" ErrorMessage="nome do produto!"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">resumo:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:TextBox ID="Resumo" runat="server" MaxLength="100" Rows="4"
                                            TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                            ControlToValidate="Resumo" ErrorMessage="breve descrição"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">destaque:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:CheckBox ID="CheckDestaque" runat="server" Text="destaque" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">status:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:CheckBox ID="CheckStatus" runat="server" Checked="True" Text="ativo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">valor:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:TextBox ID="ProdValor_" runat="server" MaxLength="13" Width="100px"
                                            SkinID="Dinheiro"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                                            ControlToValidate="ProdValor_" ErrorMessage="qual o valor?"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">estoque:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:TextBox ID="Estoque" runat="server" Width="60px" SkinID="Inteiros"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
                                            ControlToValidate="Estoque" ErrorMessage="quanto produtos há em estoque?"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">peso:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:TextBox ID="Peso" runat="server" Width="60px" SkinID="Inteiros"></asp:TextBox>
                                        gr ex: 1Kg = 1000 gr
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                                        ControlToValidate="Peso" ErrorMessage="o peso é usado para o frete!"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">extras:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:TextBox ID="Extras" runat="server" Width="100px" SkinID="Dinheiro"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding: 5px" valign="middle" width="100px">icone:</td>
                                    <td align="left" style="padding: 5px" valign="middle">
                                        <asp:FileUpload ID="IconeProduto" runat="server" />
                                        <br />
                                        <asp:Image ID="ImageSelecionadaProduto" runat="server" ImageAlign="Middle"
                                            ImageUrl="~/App_Themes/Site/ImagesSuporte/mini_logo.gif" />
                                        &nbsp;<asp:HiddenField ID="HidIconeProduto" runat="server" />
                                        <br />
                                        esta imagem será usada, caso você não tenha uma imagem personalizada para seu 
                                    produto.</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vCategorias" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="150px" style="padding: 5px" align="right">
                            <asp:Image ID="Image2" runat="server" ImageAlign="Middle"
                                ImageUrl="~/LojaVirtual/imagens/categoria_OK.png" Width="35px" />
                        </td>
                        <td style="padding: 5px" valign="middle">&nbsp;Categorias e Sub-Categorias
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding: 5px" width="150px" valign="top">
                            <asp:ImageButton ID="ibt_VoltaProdutoDetalhe" runat="server"
                                CausesValidation="False" Height="25px" ImageAlign="Middle"
                                ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png"
                                ToolTip="Voltar para detalhes do Produtos"
                                OnClick="ibt_VoltaProdutoDetalhe_Click" />
                            <br />
                            <br />
                        </td>
                        <td style="padding: 5px" valign="top">
                            <table id="tb_Canal" border="1" cellpadding="0" cellspacing="0"
                                style="border-color: #2E477F; border-width: 1px;" width="100%">
                                <tr>
                                    <td align="left" style="padding: 5px" valign="middle" width="100%">
                                        <asp:Panel ID="PanelListaCategorias" runat="server">
                                            <asp:ImageButton ID="ibt_NovoCanal0" runat="server" CausesValidation="False"
                                                ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/adicionar.png"
                                                OnClick="ibt_NovoCanal_Click" />
                                            <br />
                                            <br />
                                            Listagem de Categorias:<br />
                                            <asp:GridView ID="gridListCategorias" runat="server" AllowPaging="True"
                                                AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsCanal"
                                                EnableModelValidation="True" OnRowCommand="CategoriasRowCommand" PageSize="5"
                                                Width="80%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="titulo" SortExpression="titulo">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibt_Item0" runat="server"
                                                                AlternateText='<%# Bind("titulo") %>' CausesValidation="False"
                                                                CommandArgument='<%# Bind("id") %>' CommandName="Alterar"
                                                                ImageAlign="Middle"
                                                                ImageUrl='<%# "~/App_Themes/ActioAdms/hd/produtos/categorias/" + DataBinder.Eval(Container.DataItem, "icone") %>'
                                                                Width="68px" />
                                                            &nbsp;<asp:LinkButton ID="lkNome0" runat="server" CausesValidation="False"
                                                                CommandArgument='<%# Bind("id") %>' CommandName="Alterar"
                                                                CssClass="linksPretos" Text='<%# Bind("titulo") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibt_excluir0" runat="server"
                                                                AlternateText="Excluir este item" CausesValidation="False"
                                                                CommandArgument='<%# Bind("id") %>' CommandName="Excluir"
                                                                ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/excluir.png"
                                                                OnClientClick="if (confirm('você está certo que vai excluir? TODOS OS PRODUTOS desta categoria serão APAGADOS e exclusões não podem ser desfeitas!') == false) return false;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Image ID="Image8" runat="server" ImageAlign="Middle"
                                                        ImageUrl="~/adms/servicos/imagens/categoria_vazio.png" />
                                                    &nbsp;Não há itens cadastrados no sistema!
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelDetalhesCategoria" runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="right" valign="middle" width="100px">&nbsp;</td>
                                                    <td align="left" style="padding: 4px" valign="middle">Detalhes da Categoria</td>
                                                </tr>
                                                <tr>
                                                    <td align="center" valign="middle" width="100px">
                                                        <asp:ImageButton ID="CancelarNovoCategoria" runat="server"
                                                            CausesValidation="False" Height="28px" ImageAlign="Middle"
                                                            ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png" OnClick="CancelarNovoCategoria_Click" />
                                                    </td>
                                                    <td align="right" style="padding: 4px" valign="middle">
                                                        <asp:ImageButton ID="AtualizarCategoria" runat="server" Height="28px"
                                                            ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png"
                                                            ValidationGroup="Canal" OnClick="AtualizarCategoria_Click" />
                                                        <asp:ImageButton ID="SalvarNovaCategoria" runat="server" Height="28px"
                                                            ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png"
                                                            ValidationGroup="Canal" OnClick="SalvarNovaCategoria_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle" width="100px">título:</td>
                                                    <td align="left" style="padding: 4px" valign="middle">
                                                        <asp:TextBox ID="TituloCategoria" runat="server" Width="300px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                            ControlToValidate="TituloCategoria" CssClass="avisos" ErrorMessage="qual o título"
                                                            ValidationGroup="Canal"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle" width="100px">icone:</td>
                                                    <td align="left" style="padding: 4px" valign="middle">
                                                        <asp:FileUpload ID="IconeCategoria" runat="server" />
                                                        <br />
                                                        <asp:Image ID="ImageSelecionadaCategoria" runat="server" ImageAlign="Middle"
                                                            ImageUrl="~/App_Themes/Site/ImagesSuporte/mini_logo.gif" />
                                                        &nbsp;<asp:HiddenField ID="HidIconeCategoria" runat="server" />
                                                        <br />
                                                        esta imagem será usada, caso você não tenha uma imagem personalizada para esta 
                                                    categoria.</td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle" width="100px">&nbsp;</td>
                                                    <td align="left" style="padding: 4px" valign="middle">
                                                        <asp:Panel ID="PanelSubCategorias" runat="server">
                                                            <br />
                                                            <br />
                                                            sub-categorias:<br />
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td valign="top" width="50%">listagem das sub-categorias</td>
                                                                    <td width="50%">detalhes da sub-categoria</td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" width="50%">
                                                                        <asp:GridView ID="Grid_SubCategorias" runat="server" AllowPaging="True"
                                                                            AllowSorting="True" AutoGenerateColumns="False"
                                                                            DataSourceID="odsSubCategoriasCadastro" EnableModelValidation="True"
                                                                            Width="90%"
                                                                            OnRowCommand="SubCategoriasRowCommand">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="titulo" SortExpression="titulo">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lk_subcat" runat="server" CausesValidation="False"
                                                                                            CommandName="EditarSubCategoria" CssClass="linksPretos"
                                                                                            CommandArgument='<%# Bind("id") %>' Text='<%# Bind("titulo") %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ibt_excluir0" runat="server"
                                                                                            AlternateText="Excluir este item" CausesValidation="False"
                                                                                            CommandArgument='<%# Bind("id") %>' CommandName="ExcluirSubCategoria" ImageAlign="Middle"
                                                                                            ImageUrl="~/App_Themes/ActioAdms/acoes/excluir.gif"
                                                                                            OnClientClick="if (confirm('você está certo que vai excluir? TODOS OS PRODUTOS desta sub-categoria serão APAGADOS e exclusões não podem ser desfeitas!') == false) return false;" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                Não há sub-categorias cadastradas
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td width="50%" valign="top">título da sub-categoria<br />
                                                                        <asp:TextBox ID="Titulo_SubCategoria" runat="server" Width="100%"></asp:TextBox>
                                                                        <br />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                                            ControlToValidate="Titulo_SubCategoria" ErrorMessage="faltou o título!"
                                                                            ValidationGroup="SubCategoria"></asp:RequiredFieldValidator>
                                                                        <br />
                                                                        <asp:ImageButton ID="Salvar_SubCategoria" runat="server" Height="25px"
                                                                            ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png"
                                                                            ValidationGroup="SubCategoria" OnClick="Salvar_SubCategoria_Click" />
                                                                        <asp:ImageButton ID="EditarSubCategoria" runat="server" Height="25px"
                                                                            ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png"
                                                                            ValidationGroup="SubCategoria" OnClick="EditarSubCategoria_Click" />
                                                                        <asp:ImageButton ID="CancelarSubCategoria" runat="server"
                                                                            CausesValidation="False" Height="25px" ImageAlign="Middle"
                                                                            ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png" OnClick="CancelarSubCategoria_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:ObjectDataSource ID="odsSubCategoriasCadastro" runat="server"
                                                                DeleteMethod="Delete" InsertMethod="Inserir"
                                                                OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByIDCategoria"
                                                                TypeName="Actio.Negocio.Produtos_Sub_Categoria"
                                                                UpdateMethod="Atualizar">
                                                                <DeleteParameters>
                                                                    <asp:Parameter Name="id" Type="String" />
                                                                </DeleteParameters>
                                                                <InsertParameters>
                                                                    <asp:Parameter Name="titulo" Type="String" />
                                                                    <asp:Parameter Name="icone" Type="String" />
                                                                    <asp:Parameter Name="id_categoria" Type="String" />
                                                                </InsertParameters>
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="id_categoria" SessionField="id_Categoria"
                                                                        Type="Int32" />
                                                                </SelectParameters>
                                                                <UpdateParameters>
                                                                    <asp:Parameter Name="id" Type="String" />
                                                                    <asp:Parameter Name="titulo" Type="String" />
                                                                    <asp:Parameter Name="icone" Type="String" />
                                                                    <asp:Parameter Name="id_categoria" Type="String" />
                                                                </UpdateParameters>
                                                            </asp:ObjectDataSource>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vPedidos" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="100%" style="padding: 5px" align="left">
                            <asp:Image ID="Image3" runat="server" ImageAlign="Middle"
                                ImageUrl="~/LojaVirtual/imagens/OK.png" Width="35px" />
                            &nbsp; Relação das Vendas</td>
                    </tr>
                    <tr>
                        <td align="left" style="padding: 5px" width="100%">
                            <asp:Panel ID="PanelListaPedidos" runat="server">
                                <asp:GridView ID="gridPedidos" runat="server" AllowPaging="True"
                                    OnRowCommand="VendasListaDetalhes"
                                    AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsPedidos"
                                    EnableModelValidation="True" PageSize="100" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id Transação" SortExpression="transacao">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton5" runat="server"
                                                    CommandArgument='<%# Bind("transacao") %>' CssClass="linksPretos"
                                                    Text='<%# Bind("transacao") %>' CommandName="Pedido"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Height="40px" HorizontalAlign="Left" VerticalAlign="Middle"
                                                Width="150px" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status PagSeguro" SortExpression="status_descricao">
                                            <ItemTemplate>
                                                <asp:Label ID="Label19" runat="server" Text='<%# Bind("status_descricao") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="status Loja" SortExpression="status_loja">
                                            <ItemTemplate>
                                                <asp:Label ID="Label20" runat="server" Text='<%# Bind("status_loja") %>'></asp:Label>
                                                <br />
                                                Rastreador:
                                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("Rastreador") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Detalhes" SortExpression="Cliente">
                                            <ItemTemplate>
                                                Cliente:
                                            <asp:LinkButton ID="LinkButton6" runat="server"
                                                CommandArgument='<%# Bind("IdCliente") %>' CssClass="linksPretos"
                                                Text='<%# Bind("Cliente") %>' CommandName="Cliente"></asp:LinkButton>
                                                <br />
                                                <br />
                                                Quantidade de itens:
                                            <asp:Label ID="Label22" runat="server" Text='<%# Bind("itens") %>'
                                                ToolTip='<%# Bind("pedido") %>'></asp:Label>
                                                <br />
                                                <br />
                                                Frete contratado:
                                            <asp:Label ID="Label23" runat="server" Text='<%# Bind("Tipo_Frete") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsPedidos" runat="server" DeleteMethod="Excluir"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="selectAllVendas"
                                    TypeName="Actio.Negocio.Produtos_Vendas">
                                    <DeleteParameters>
                                        <asp:Parameter Name="id" Type="String" />
                                    </DeleteParameters>
                                </asp:ObjectDataSource>
                            </asp:Panel>
                            <asp:Panel ID="PanelDetalhePedidos" runat="server">
                            </asp:Panel>
                            &nbsp;&nbsp; </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vClientes" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="padding: 5px">Meus Clientes</td>
                    </tr>
                    <tr>
                        <td style="padding: 5px">
                            <asp:Panel ID="PanelListCliente" runat="server">
                                <asp:GridView ID="GridClientes" runat="server" AllowPaging="True"
                                    OnRowCommand="DetalhaCliente"
                                    AllowSorting="True" DataSourceID="odsClientes"
                                    EnableModelValidation="True" AutoGenerateColumns="False" PageSize="100">
                                    <Columns>
                                        <asp:TemplateField HeaderText="cliente" SortExpression="CliNome">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Nome" runat="server" CausesValidation="False"
                                                    CommandArgument='<%# Bind("cliente_id") %>' CommandName="Cliente"
                                                    Text='<%# Bind("CliNome") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="contatos" SortExpression="CliEmail">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="email" runat="server" CausesValidation="False"
                                                    CommandArgument='<%# Bind("cliente_id") %>' Text='<%# Bind("CliEmail") %>'
                                                    ToolTip='<%# Bind("CliNome") %>' CommandName="Contato"></asp:LinkButton>
                                                &nbsp;|
                                            <asp:Label ID="telefone" runat="server" Text='<%# Bind("CliTelefone") %>'
                                                ToolTip='<%# Bind("CliNome") %>'></asp:Label>
                                                &nbsp;
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton3" runat="server"
                                                    AlternateText='<%# Bind("CliNome") %>' CausesValidation="False"
                                                    CommandArgument='<%# Bind("cliente_id") %>' ImageAlign="Middle"
                                                    ImageUrl="~/LojaVirtual/imagens/shopping_cart.png"
                                                    CommandName="Vendas" />
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" VerticalAlign="Middle"
                                                Width="30px" />
                                            <ItemStyle Height="30px" HorizontalAlign="Center" VerticalAlign="Middle"
                                                Width="30px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsClientes" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll"
                                    TypeName="Actio.Negocio.Clientes"></asp:ObjectDataSource>
                            </asp:Panel>
                            <asp:Panel ID="PanelDetalhesCliente" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="100px" align="center" style="padding: 5px" valign="middle">
                                            <asp:ImageButton ID="ibt_ListaClientes" runat="server" Height="25px"
                                                ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png"
                                                OnClick="ibt_ListaClientes_Click" />
                                        </td>
                                        <td align="left" style="padding: 5px" valign="middle">&nbsp;Detalhes do Cliente </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding: 5px" valign="middle" width="100px">&nbsp;</td>
                                        <td align="center" style="padding: 5px" valign="middle">
                                            <asp:ImageButton ID="ibt_Compras" runat="server" CausesValidation="False"
                                                ImageAlign="Middle" ImageUrl="~/LojaVirtual/imagens/shopping_cart.png"
                                                OnClick="ibt_Compras_Click" />
                                            &nbsp;<asp:LinkButton ID="lbt_Compras" runat="server" CssClass="linksAzul"
                                                OnClick="lbt_Compras_Click">Compras Deste Cliente</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding: 5px" valign="middle" width="100px"
                                            class="azul">Nome:</td>
                                        <td align="left" style="padding: 5px" valign="middle">
                                            <asp:Label ID="Nome" runat="server" CssClass="titulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="azul" style="padding: 5px" valign="middle"
                                            width="100px">Contatos:</td>
                                        <td align="left" style="padding: 5px" valign="middle">
                                            <asp:Label ID="Email" runat="server" CssClass="avisos"></asp:Label>
                                            &nbsp;|
                                        <asp:Label ID="Telefone" runat="server" CssClass="avisos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="azul" style="padding: 5px" valign="middle"
                                            width="100px">Endereço:</td>
                                        <td align="left" style="padding: 5px" valign="middle">
                                            <asp:Literal ID="Endereco" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelListaVendas" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" width="100px">
                                            <asp:ImageButton ID="ibt_voltarCliente" runat="server" CausesValidation="False"
                                                Height="25px" ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png"
                                                ToolTip="Voltar para os dados do Cliente"
                                                OnClick="ibt_voltarCliente_Click" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbt_todosClientes" runat="server" CausesValidation="False"
                                                CssClass="linksAzul" OnClick="lbt_todosClientes_Click">&gt;&gt; todos os clientes</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" width="100px">&nbsp;</td>
                                        <td style="padding: 20px">
                                            <asp:Label ID="Nome0" runat="server" CssClass="titulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="azul" width="100px">&nbsp;</td>
                                        <td style="padding: 5px">
                                            <asp:GridView ID="gridVendasCliente" runat="server" AllowPaging="True"
                                                OnRowCommand="DetalhaPedido"
                                                DataSourceID="odsComprasCliente" EnableModelValidation="True"
                                                AutoGenerateColumns="False" PageSize="100">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Transação" SortExpression="transacao">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"
                                                                CommandArgument='<%# Bind("transacao") %>' CommandName="Detalhar"
                                                                CssClass="linksPretos" Text='<%# Bind("transacao") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="status PagSeguro"
                                                        SortExpression="status_descricao">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("forma_pagamento") %>'></asp:Label>
                                                            &nbsp;|
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("status_descricao") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="itens" SortExpression="itens">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("itens") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="status Loja" SortExpression="status_loja">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("status_loja") %>'></asp:Label>
                                                            &nbsp;<asp:Label ID="Label9" runat="server" Text='<%# Bind("rastreador") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="azul" width="100px">&nbsp;</td>
                                        <td style="padding: 5px">
                                            <asp:ObjectDataSource ID="odsComprasCliente" runat="server"
                                                DeleteMethod="Excluir" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="SelectByEmailCliente"
                                                TypeName="Actio.Negocio.Produtos_Vendas">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="id" Type="String" />
                                                </DeleteParameters>
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="email" SessionField="emailCliente" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelDetalhaVendas" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="100px">
                                            <asp:ImageButton ID="ibt_Compras0" runat="server" CausesValidation="False"
                                                Height="25px" ImageAlign="Middle"
                                                ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png" OnClick="ibt_Compras_Click"
                                                ToolTip="Voltar para todos os pedidos do cliente" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Nome1" runat="server" CssClass="titulos"></asp:Label>
                                            <br />
                                            código desta transação:
                                        <asp:Label ID="CodigoTransacao" runat="server" CssClass="azul"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gridProdutosPedido" runat="server"
                                                OnRowCommand="informarEnvio"
                                                DataSourceID="odsProdutosPedido" EnableModelValidation="True"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Detalhes do Produto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label10" runat="server" CssClass="titulos"
                                                                Text='<%# Bind("ProdDescricao") %>'></asp:Label>
                                                            <br />
                                                            R$
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("ProdValor") %>'></asp:Label>
                                                            <br />
                                                            <br />
                                                            quantidade:
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("ProdQuantidade") %>'></asp:Label>
                                                            <br />
                                                            frete contratado:
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("ProdFrete") %>'></asp:Label>
                                                            <br />
                                                            <br />
                                                            ID Transação:
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("TransacaoID") %>'></asp:Label>
                                                            <br />
                                                            Data:
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("dataTransacao") %>'></asp:Label>
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="Label16" runat="server" CssClass="avisos"
                                                                Text='<%# Bind("ProdStatus") %>'></asp:Label>
                                                            <br />
                                                            <br />
                                                            Envio do Produto:<br />
                                                            <asp:Label ID="Label17" runat="server" Text='<%# Bind("StatusEnvio") %>'></asp:Label>
                                                            <br />
                                                            Rastreador:
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("Rastreador") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="40px" HorizontalAlign="Left" VerticalAlign="Middle"
                                                            Width="600px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton4" runat="server"
                                                                CommandArgument='<%# Bind("ProdID") %>' CommandName='<%# Bind("TransacaoID") %>'
                                                                CssClass="linksAzul">Informar Envio</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:ObjectDataSource ID="odsProdutosPedido" runat="server"
                                                DeleteMethod="ExcluirTransacaoID" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="selectByTransacaoID"
                                                TypeName="Actio.Negocio.Produtos_Itens_Pedido">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="TransacaoID" Type="String" />
                                                </DeleteParameters>
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="TransacaoID" SessionField="TransacaoID"
                                                        Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100px">&nbsp;</td>
                                        <td>
                                            <asp:Panel ID="PanelStatusProduto" runat="server">
                                                <asp:ImageButton ID="ibt_CancelaStatusProd" runat="server"
                                                    CausesValidation="False" Height="25px" ImageAlign="Middle"
                                                    ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png"
                                                    OnClick="ibt_CancelaStatusProd_Click" />
                                                <br />
                                                <br />
                                                Status deste produto:
                                            <asp:DropDownList ID="ddlStatusEnvio" runat="server">
                                                <asp:ListItem Selected="True">Sem Envio</asp:ListItem>
                                                <asp:ListItem>Enviado</asp:ListItem>
                                            </asp:DropDownList>
                                                <br />
                                                <br />
                                                Código de Rastreamento:
                                            <asp:TextBox ID="CodigoRastreador" runat="server"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:RadioButtonList ID="rblTipoEncomenda" runat="server" CellPadding="5"
                                                    CellSpacing="5">
                                                    <asp:ListItem Selected="True" Value="0">Todos os Produtos irão na mesma encomenda</asp:ListItem>
                                                    <asp:ListItem Value="1">Os Produtos seguirão em encomendas separadas</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <br />
                                                <asp:CheckBox ID="NotificarCliente" runat="server" Checked="True"
                                                    Text="Notificar o Cliente por e-mail" />
                                                <br />
                                                <br />
                                                <asp:ImageButton ID="ibt_SalvarStatus" runat="server" CausesValidation="False"
                                                    Height="30px" ImageAlign="Middle"
                                                    ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png"
                                                    OnClick="ibt_SalvarStatus_Click" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="FotosProduto" runat="server">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: center">&nbsp;Fotos do Produto</td>
                        <td align="left" height="40px" valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:ImageButton ID="ibt_listagemProdutos0" runat="server"
                                CausesValidation="False" Height="25px" ImageAlign="Middle"
                                ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png"
                                OnClick="ibt_listagemProdutos_Click"
                                ToolTip="Voltar para listagem de Produtos" />
                        </td>
                        <td align="left" height="40px" valign="middle">
                            <asp:ImageButton ID="ibtNovoAnexo" runat="server" CausesValidation="False"
                                ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/adicionar.png"
                                OnClick="ibtNovoAnexo_Click" />
                            &nbsp;<asp:LinkButton ID="lbtNovoAnexo" runat="server" CausesValidation="False"
                                CssClass="linksAzul" OnClick="lbtNovoAnexo_Click">Adicionar Nova Foto</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">&nbsp;</td>
                        <td>
                            <asp:GridView ID="GridProdutos_Fotos" runat="server"
                                AutoGenerateColumns="False" CellPadding="4" DataSourceID="odsAnexos"
                                EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                OnRowCommand="GridAnexosRowCommand" ShowHeader="False">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="alterar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibt_Item" runat="server" CausesValidation="False"
                                                CommandArgument='<%# Bind("id") %>' CommandName="Alterar"
                                                ImageAlign="Middle"
                                                ImageUrl='<%# "~/App_Themes/ActioAdms/hd/produtos/album/" + DataBinder.Eval(Container.DataItem, "id_produto") + "/" + DataBinder.Eval(Container.DataItem, "arquivo") %>'
                                                Width="68px" />
                                            &nbsp;<asp:LinkButton ID="lbt_Ordem" runat="server" CausesValidation="False"
                                                CommandArgument='<%# Bind("id") %>' CommandName="Alterar"
                                                CssClass="linksPretos" Text='<%# Bind("ordem") %>'></asp:LinkButton>
                                            &nbsp;-
                                        <asp:LinkButton ID="lbt_Foto" runat="server" CausesValidation="False"
                                            CommandArgument='<%# Bind("id") %>' CommandName="Alterar"
                                            CssClass="linksPretos" Text='<%# Bind("titulo") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="excluir">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibt_excluir" runat="server"
                                                AlternateText="Excluir este item" CausesValidation="False"
                                                CommandArgument='<%# Bind("id") %>' CommandName="Excluir"
                                                ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/excluir.png"
                                                OnClientClick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Image ID="Image9" runat="server" ImageAlign="Middle"
                                        ImageUrl="~/adms/midias/imagens/vazio.png" />
                                    &nbsp;- não há registros no sistema!
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsAnexos" runat="server" DeleteMethod="Excluir"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="FotosDoProduto"
                                TypeName="Actio.Negocio.Produtos_Fotos">
                                <DeleteParameters>
                                    <asp:Parameter Name="id" Type="String" />
                                </DeleteParameters>
                                <SelectParameters>
                                    <asp:SessionParameter Name="id_produto" SessionField="id" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:HiddenField ID="HidDono" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Panel ID="PanelEditAnexo" runat="server" Visible="False">
                                <table cellpadding="0" cellspacing="0" class="style6">
                                    <tr>
                                        <td style="padding-top: 10px; text-align: right;" valign="middle" width="150">&nbsp;</td>
                                        <td style="padding-top: 10px; padding-left: 10px; text-align: center;">
                                            <asp:ImageButton ID="ibtSalvarAnexo" runat="server"
                                                ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png" OnClick="ibtSalvarAnexo_Click"
                                                Visible="False" />
                                            <asp:ImageButton ID="ibtAtualizarAnexo" runat="server"
                                                ImageUrl="~/App_Themes/ActioAdms/botoes/editar.png" OnClick="ibtAtualizarAnexo_Click"
                                                Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px; text-align: right;" valign="middle" width="150">Título:</td>
                                        <td style="padding-top: 10px; padding-left: 10px">
                                            <asp:TextBox ID="TituloAnexo" runat="server" Width="250px"></asp:TextBox>
                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                ControlToValidate="TituloAnexo" ErrorMessage="qual o título do anexo?"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px; text-align: right;" valign="middle" width="150">ordem:</td>
                                        <td style="padding-top: 10px; padding-left: 10px">
                                            <asp:TextBox ID="OrdemFoto" runat="server" MaxLength="3" SkinID="Inteiros"
                                                Width="45px"></asp:TextBox>
                                            &nbsp;ex: 001<asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                                                runat="server" ControlToValidate="OrdemFoto"
                                                ErrorMessage="qual o número da foto"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px; text-align: right;" valign="middle" width="150">Anexo:</td>
                                        <td style="padding-top: 10px; padding-left: 10px">
                                            <asp:FileUpload ID="Anexo" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                ControlToValidate="Anexo" ErrorMessage="faltou a foto"></asp:RequiredFieldValidator>
                                            <br />
                                            &nbsp;<br />
                                            <asp:HiddenField ID="HidAnexo" runat="server" />
                                            <asp:Image ID="FotoSelecionada" runat="server" Width="98px" />
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px; text-align: right;" valign="middle" width="150">&nbsp;</td>
                                        <td align="left" style="padding: 10px;">
                                            <asp:ImageButton ID="ibt_CancelarNovaFoto" runat="server"
                                                CausesValidation="False" ImageAlign="Middle"
                                                ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png"
                                                OnClick="ibt_CancelarNovaFoto_Click" Width="78px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vProdutosDescricao" runat="server">
                <br />
                Descrições do Produto:<asp:Label ID="TituloProduto" runat="server"
                    CssClass="tituloscinza"></asp:Label>
                <br />
                <asp:Panel ID="PanelListDescricao" runat="server">
                    <br />
                    <asp:Button ID="bt_novaDescricao" runat="server" CausesValidation="False"
                        Text="+ nova descrição" OnClick="bt_novaDescricao_Click" />
                    <br />
                    <br />
                    <asp:DataList ID="dtlTituloDescricao"
                        runat="server" CellPadding="5" CellSpacing="5" DataSourceID="ods_Descricoes"
                        OnItemCommand="AtualizaDescricao"
                        RepeatDirection="Horizontal">
                        <HeaderTemplate>
                            Selecione um título para editar<br />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="titulo" runat="server" CausesValidation="False"
                                CommandArgument='<%# Bind("id") %>' CommandName="Editar" CssClass="linksAzul"
                                Text='<%# DataBinder.Eval(Container.DataItem, "titulo") + " | " %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:ObjectDataSource ID="ods_Descricoes" runat="server" DeleteMethod="Delete"
                        InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="SelectByIDProduto"
                        TypeName="Actio.Negocio.Produtos_Descricao" UpdateMethod="Atualizar">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="id_produto" Type="String" />
                            <asp:Parameter Name="titulo" Type="String" />
                            <asp:Parameter Name="descricao" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="id" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="id" Type="String" />
                            <asp:Parameter Name="id_produto" Type="String" />
                            <asp:Parameter Name="titulo" Type="String" />
                            <asp:Parameter Name="descricao" Type="String" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
                <asp:Panel ID="PanelDetalheDescricao" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right" style="padding: 5px" valign="middle" width="150px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server"
                                    ControlToValidate="TituloDescricao" CssClass="avisos"
                                    ErrorMessage="qual o título"></asp:RequiredFieldValidator>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server"
                                    ControlToValidate="DescricaoProduto" CssClass="avisos"
                                    ErrorMessage="faltou a descrição"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="padding: 5px" valign="middle">&nbsp;<asp:ImageButton ID="ibt_SalvarDescricao" runat="server" ImageAlign="Middle"
                                ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png"
                                OnClick="ibt_SalvarDescricao_Click" Width="68px" Visible="False" />
                                <asp:ImageButton ID="ibt_AtualizarDescricao" runat="server" ImageAlign="Middle"
                                    ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png"
                                    Visible="False" Width="68px" OnClick="ibt_AtualizarDescricao_Click" />
                                <asp:ImageButton ID="ibt_CancelarDescricao" runat="server"
                                    CausesValidation="False" ImageAlign="Middle"
                                    ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png"
                                    OnClick="ibt_CancelarDescricao_Click" Width="78px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding: 5px" valign="middle" width="150px">Título da Descrição:</td>
                            <td align="left" style="padding: 5px" valign="middle">
                                <asp:TextBox ID="TituloDescricao" runat="server" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding: 5px" valign="top" width="150px">Descreva o produto:</td>
                            <td align="left" style="padding: 5px" valign="middle">
                                <asp:TextBox runat="server" ID="DescricaoProduto" CssClass="EditorHtml" />
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
            </asp:View>
            <asp:View ID="v10Banner" runat="server">
                <asp:MultiView ID="mvBanner" runat="server">
                    <asp:View ID="v0List" runat="server">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="60">&nbsp;</td>
                                <td>
                                    <asp:Label ID="LabelDetalheRecurso" runat="server" CssClass="titulos"
                                        Text="Agora você está na listagem dos itens cadastrados"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">&nbsp;</td>
                                <td height="40px" valign="middle">
                                    <asp:ImageButton ID="ibt_NovoBanner" runat="server" CausesValidation="False"
                                        ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/adicionar.png" OnClick="ibt_NovoBanner_Click" />
                                    &nbsp;<asp:LinkButton ID="lbt_NovoBanner" runat="server" CausesValidation="False"
                                        CssClass="linksAzul" OnClick="lbt_NovoBanner_Click">Adicione um novo item</asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center">&nbsp;</td>
                                <td>
                                    <asp:GridView ID="gridListBanner" runat="server" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsBanner"
                                        EnableModelValidation="True" OnRowCommand="RowCommandBanner">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibt_Banner" runat="server"
                                                        AlternateText='<%# Bind("banner_alt") %>'
                                                        CommandArgument='<%# Bind("banner_id") %>' CommandName="Alterar"
                                                        ImageAlign="Middle"
                                                        ImageUrl='<%# Bind("banner_arquivo", "~/App_Themes/ActioAdms/hd/banner_loja/{0}") %>' Width="98px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="banner_alt" SortExpression="banner_alt" HeaderText="ordenar">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:TemplateField
                                                SortExpression="ATIVO" HeaderText="ordenar">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ATIVO") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="150px">
                                                        <tr>
                                                            <td align="center" style="padding: 10px" valign="middle" width="150px">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ATIVO") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="excluir">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibt_excluir1" runat="server"
                                                        CommandArgument='<%# Bind("banner_id") %>' CommandName="Excluir"
                                                        ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/excluir.png"
                                                        OnClientClick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Image ID="Image12" runat="server" ImageAlign="Middle"
                                                ImageUrl="~/adms/banner/imagens/vazio.png" />
                                            &nbsp;Não há banner cadastrado no sistema
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:ObjectDataSource ID="odsBanner" runat="server" DeleteMethod="Excluir"
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll"
                                        TypeName="Actio.Negocio.Banner_Loja">
                                        <DeleteParameters>
                                            <asp:Parameter Name="banner_id" Type="Int32" />
                                        </DeleteParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="v1Edit" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="60">&nbsp;</td>
                                <td>
                                    <asp:Label ID="LabelTituloEdit" runat="server" CssClass="titulos"
                                        Text="Agora você está na edição detalhada"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">&nbsp;</td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="center" valign="middle" width="100px">
                                                <asp:ImageButton ID="ibt_cancelarBanner" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png" OnClick="ibt_cancelarBanner_Click" Width="78px" />
                                            </td>
                                            <td align="center" style="padding: 8px" valign="middle">
                                                <asp:ImageButton ID="ibt_AlterarBanner" runat="server" ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/editar.png" OnClick="ibt_AlterarBanner_Click" Visible="False" />
                                                <asp:ImageButton ID="ibt_SalvarBanner" runat="server" ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png" OnClick="ibt_SalvarBanner_Click" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" width="100px">status:</td>
                                            <td align="left" style="padding: 8px" valign="middle">
                                                <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">inativo</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">ativo</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" width="100px">categoria:</td>
                                            <td align="left" style="padding: 8px" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlCategoriaBanner" DataTextField="titulo" DataValueField="id" DataSourceID="odsCanal" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoriaBanner_SelectedIndexChanged">
                                                    <asp:ListItem>Página principal</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" width="100px">tipo:</td>
                                            <td align="left" style="padding: 8px" valign="middle">
                                                <asp:RadioButtonList ID="Tipo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="Tipo_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">topo</asp:ListItem>
                                                    <asp:ListItem Value="1">lateral</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" width="100px">titulo:</td>
                                            <td align="left" style="padding: 8px" valign="middle">
                                                <asp:TextBox ID="TituloBanner" runat="server" Width="300px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TituloBanner" CssClass="avisos" ErrorMessage="Informe o Título da publicidade"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" width="100px">hyperlink:</td>
                                            <td align="left" style="padding: 8px" valign="middle">
                                                <asp:TextBox ID="bannerLink" runat="server" Width="300px"></asp:TextBox>
                                                ex: <a href="http://www.actio.net.br">http://www.actio.net.br</a><br />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="bannerLink" CssClass="avisos" ErrorMessage="isto não se parece com um link" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" width="100px">Banner:</td>
                                            <td align="left" style="padding: 8px" valign="middle">
                                                <asp:Label runat="server" ID="MensagemTamanho" Text="Sua imagem deve ter 280px X 830px, procure fazer imagens apropriadas para internet." />
                                                <br />
                                                <br />
                                                <asp:FileUpload ID="fuBanner" runat="server" />
                                                &nbsp;<asp:RequiredFieldValidator ID="requireImagem" runat="server" ControlToValidate="fuBanner" CssClass="avisos" ErrorMessage="Selecione uma imagem em seu computador"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="hidBanner" runat="server" />
                                                <asp:HiddenField ID="hidBannerXmlAdress" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" width="100px">
                                                <asp:Label ID="LabelImagemAtual" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td align="left" style="padding: 8px" valign="middle">
                                                <asp:Image ID="ImagemAtual" runat="server" Visible="False" Width="160px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </asp:View>
            <asp:View ID="viewMarcas" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" width="150px">
                            <asp:Image ID="Image4" runat="server" ImageAlign="Middle"
                                ImageUrl="~/LojaVirtual/imagens/bancoOK.png" />
                        </td>
                        <td>Cadastro de Marcas</td>
                    </tr>
                    <tr>
                        <td width="150px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="150px">&nbsp;</td>
                        <td>
                            <asp:Panel ID="PanelListaBanco" runat="server">
                                <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False"
                                    ImageAlign="Middle" ImageUrl="~/LojaVirtual/imagens/bancoNovo.png"
                                    OnClick="ibt_NovoBanco_Click" Width="28px" />
                                &nbsp;<asp:LinkButton ID="LinkButton7" runat="server" CssClass="linkcinzaBold"
                                    OnClick="lbt_NovoBanco_Click">Cadastrar Nova Marca</asp:LinkButton>
                                <br />
                                <br />
                                <asp:GridView ID="gridBanco" runat="server"
                                    OnRowCommand="BancoRowCommand"
                                    AllowSorting="True" DataSourceID="odsMarcas" EnableModelValidation="True"
                                    AutoGenerateColumns="False" Width="70%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Marcas Cadastradas" SortExpression="banco">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"
                                                    CommandArgument='<%# Bind("id") %>' CommandName="Alterar"
                                                    CssClass="linksPretos" Text='<%# Bind("descricao") %>'></asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibt_excluir" runat="server"
                                                    AlternateText="Excluir este item" CausesValidation="False"
                                                    CommandArgument='<%# Bind("id") %>' CommandName="Excluir" ImageAlign="Middle"
                                                    ImageUrl="~/App_Themes/ActioAdms/botoes/excluir.png"
                                                    OnClientClick="if (confirm('você está certo que vai excluir? Exclusões não podem ser desfeitas!') == false) return false;" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Image ID="Image11" runat="server" ImageAlign="Middle"
                                            ImageUrl="~/LojaVirtual/imagens/bancovazio.png" Width="38px" />
                                        Não há marcas cadastradas
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsMarcas" runat="server" DeleteMethod="Excluir"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll"
                                    TypeName="Actio.Negocio.Marca">
                                    <DeleteParameters>
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </DeleteParameters>
                                </asp:ObjectDataSource>
                            </asp:Panel>
                            <asp:Panel ID="PanelDetalhaBanco" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="100px" align="center">
                                            <asp:ImageButton ID="ibt_ListBanco" runat="server" CausesValidation="False"
                                                Height="28px" ImageAlign="Middle"
                                                ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png"
                                                OnClick="ibt_ListBanco_Click" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="AlterarBanco" runat="server" ImageAlign="Middle"
                                                ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png" OnClick="AlterarBanco_Click" />
                                            <asp:ImageButton ID="SalvarBanco" runat="server" ImageAlign="Middle"
                                                ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png" OnClick="SalvarBanco_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding: 5px" valign="middle" width="100px">Marca:</td>
                                        <td align="left" style="padding: 5px" valign="middle">
                                            <asp:TextBox ID="Marca" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="Marca" ErrorMessage="nome da marca"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </fieldset>

</asp:Content>

