<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master"  ValidateRequest="false" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="adms_usuarios_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:MultiView ID="mvAll" runat="server">
        <asp:View ID="ViewList" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" height="30px" valign="middle" width="200px" 
                        style="padding: 10px;">
                        <asp:ImageButton ID="ibt_adicionar" runat="server" CausesValidation="false" 
                            onclick="ibt_adicionar_Click" SkinID="ibt_NovoItem" />
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" valign="middle" colspan="2" style="padding-left: 20px">
                        Itens cadastrados:<asp:GridView ID="gridList" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsList" 
                            EnableModelValidation="True" OnRowCommand="ComandoDaListagem" 
                            PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderText="nome" SortExpression="nome">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" 
                                            CommandArgument='<%# BIND("id") %>' CommandName="Editar" CssClass="linksPretos" 
                                            Text='<%# BIND("nome") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Height="35px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" 
                                        Width="250px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ATIVO" HeaderText="status" SortExpression="status">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPOLOGIA" HeaderText="tipo" SortExpression="tipo">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="histótico do usuário">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" 
                                            CommandArgument='<%# BIND("id") %>' CssClass="linksPretos" 
                                            ToolTip="Vizualizar o historico de utulização do sistema do usuário" 
                                            CommandName="Historico">ver histórico</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                            CommandArgument='<%# BIND("id") %>' CommandName="Excluir" 
                                            SkinID="ibt_ExcluirItem" ToolTip="Excluir este item" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Image ID="Image2" runat="server" SkinID="img_Vazio" />
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp;</td>
                    <td align="left" style="padding-left: 10px;" valign="middle">
                        <asp:ObjectDataSource ID="odsList" runat="server" DeleteMethod="Delete" 
                            InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" 
                            SelectMethod="SelectAll" TypeName="Actio.Negocio.Usuario" 
                            UpdateMethod="AtualizarNoSite">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="nome" Type="String" />
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="senha" Type="String" />
                                <asp:Parameter Name="telefone" Type="String" />
                                <asp:Parameter Name="celular" Type="String" />
                                <asp:Parameter Name="nascimento" Type="String" />
                                <asp:Parameter Name="endereco" Type="String" />
                                <asp:Parameter Name="bairro" Type="String" />
                                <asp:Parameter Name="cidade" Type="String" />
                                <asp:Parameter Name="estado" Type="String" />
                                <asp:Parameter Name="cep" Type="String" />
                                <asp:Parameter Name="tipo" Type="String" />
                                <asp:Parameter Name="status" Type="String" />
                                <asp:Parameter Name="icone" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="id" Type="String" />
                                <asp:Parameter Name="nome" Type="String" />
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="endereco" Type="String" />
                                <asp:Parameter Name="bairro" Type="String" />
                                <asp:Parameter Name="cidade" Type="String" />
                                <asp:Parameter Name="estado" Type="String" />
                                <asp:Parameter Name="cep" Type="String" />
                                <asp:Parameter Name="celular" Type="String" />
                                <asp:Parameter Name="telefone" Type="String" />
                                <asp:Parameter Name="nascimento" Type="String" />
                                <asp:Parameter Name="sexo" Type="String" />
                                <asp:Parameter Name="opt_cliente" Type="String" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewEdit" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        <asp:Image ID="IconePostado" runat="server" ImageAlign="Middle" Visible="False" 
                            Width="78px" />
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:Label ID="LabelNome" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px" 
                        style="padding-top: 20px; padding-bottom: 10px;">
                        <asp:ImageButton ID="ibt_cancelar" runat="server" CausesValidation="False" 
                            onclick="ibt_cancelar_Click" SkinID="ibt_CancelarItem" />
                    </td>
                    <td align="left" 
                        style="padding-left: 100px; padding-top: 20px; padding-bottom: 10px;" 
                        valign="middle" width="800px">
                        <asp:ImageButton ID="ibt_salvar" runat="server" onclick="ibt_salvar_Click" 
                            SkinID="ibt_SalvarItem" />
                        <asp:ImageButton ID="ibt_editar" runat="server" onclick="ibt_editar_Click" 
                            SkinID="ibt_EditarItem" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        status:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">ativo</asp:ListItem>
                            <asp:ListItem Value="0">inativo</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        &nbsp; tipo:
                    </td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:RadioButtonList ID="Tipo" runat="server" RepeatDirection="Horizontal" 
                            AutoPostBack="True" OnSelectedIndexChanged="UsuarioTipo">
                            <asp:ListItem Selected="True" Value="0">usuário simples</asp:ListItem>
                            <asp:ListItem Value="1">Administrador do Sistema</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        nome:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Nome" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNome" runat="server" 
                            ControlToValidate="Nome" CssClass="avisos" ErrorMessage="preencha o nome"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        email:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Email" runat="server" MaxLength="145" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                            ControlToValidate="Email" CssClass="avisos" ErrorMessage="preencha o nome"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                            ControlToValidate="Email" CssClass="avisos" ErrorMessage="e-mail inválido" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        senha:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Senha" runat="server" MaxLength="12" Width="110px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSenha" runat="server" 
                            ControlToValidate="Senha" CssClass="avisos" ErrorMessage="preencha a senha"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        telefone:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Telefone" runat="server" MaxLength="14" SkinID="Telefone" 
                            Width="90px"></asp:TextBox>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        celular:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Celular" runat="server" MaxLength="14" SkinID="Telefone" 
                            Width="90px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCelular" runat="server" 
                            ControlToValidate="Nome" CssClass="avisos" 
                            ErrorMessage="todo mundo têm celular"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        nascimento:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Nascimento" runat="server" MaxLength="10" SkinID="Data" 
                            Width="70px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        endereço completo:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Endereco" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        bairro:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Bairro" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        cidade:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:TextBox ID="Cidade" runat="server" Width="200px"></asp:TextBox>
                        &nbsp;estado:
                        <asp:TextBox ID="Estado" runat="server" MaxLength="2" Width="30px"></asp:TextBox>
                        &nbsp;cep:<asp:TextBox ID="CEP" runat="server" MaxLength="7" SkinID="CEP" 
                            Width="90px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        imagem:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:FileUpload ID="Icone" runat="server" />
                        <asp:HiddenField ID="HidIcone" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="30px" valign="middle" width="200px">
                        recursos de administrador:</td>
                    <td align="left" style="padding-left: 10px;" valign="middle" width="800px">
                        <asp:CheckBoxList ID="CheckRecursos" runat="server" DataSourceID="odsRecursos" 
                            DataTextField="titulo" DataValueField="id" RepeatColumns="4" 
                            RepeatDirection="Horizontal" CellPadding="5" CellSpacing="5" 
                            Enabled="False">
                        </asp:CheckBoxList>
                        <asp:ObjectDataSource ID="odsRecursos" runat="server" DeleteMethod="Delete" 
                            InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" 
                            SelectMethod="SelectAll" TypeName="Actio.Negocio.Recursos" 
                            UpdateMethod="Atualizar">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="titulo" Type="String" />
                                <asp:Parameter Name="icone" Type="String" />
                                <asp:Parameter Name="url" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="id" Type="String" />
                                <asp:Parameter Name="titulo" Type="String" />
                                <asp:Parameter Name="icone" Type="String" />
                                <asp:Parameter Name="url" Type="String" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewHistorico" runat="server">

            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="top" width="100%" style="padding: 10px">
                        <asp:ImageButton ID="ibt_voltar" runat="server" CausesValidation="False" 
                            ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/retornar.png" 
                            onclick="ibt_voltar_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px" valign="middle" width="100%">
                        Legenda:
                        <asp:Image ID="Image5" runat="server" ImageAlign="Middle" 
                            ImageUrl="~/App_Themes/ActioAdms/acoes/editar.gif" />
                        &nbsp;edição |
                        <asp:Image ID="Image6" runat="server" ImageAlign="Middle" 
                            ImageUrl="~/App_Themes/ActioAdms/acoes/excluir.gif" />
                        &nbsp;exclusão |
                        <asp:Image ID="Image7" runat="server" ImageAlign="Middle" 
                            ImageUrl="~/App_Themes/ActioAdms/acoes/incluir.gif" />
                        &nbsp;inclusão |
                        <asp:Image ID="Image8" runat="server" ImageAlign="Middle" 
                            ImageUrl="~/App_Themes/ActioAdms/acoes/login.png" />
                        &nbsp;login |
                        <asp:Image ID="Image9" runat="server" ImageAlign="Middle" 
                            ImageUrl="~/App_Themes/ActioAdms/acoes/logoff.png" />
                        &nbsp;logoff
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" width="100%">
                        <asp:GridView ID="GridHistorico" runat="server" DataSourceID="odsHistorico" 
                            EnableModelValidation="True" AutoGenerateColumns="False" 
                             AllowSorting="True" AllowPaging="True" PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderText="data" SortExpression="data">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# BIND("data") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="descrição" SortExpression="descricao">
                                    <ItemTemplate>
                                        <asp:Literal ID="Literal1" runat="server" Text='<%# BIND("descricao") %>'></asp:Literal>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ação" SortExpression="tipo">
                                    <ItemTemplate>
                                        <asp:Image ID="Image3" runat="server" ImageAlign="Middle"
                                        ImageUrl='<%# "~/App_Themes/ActioAdms/acoes/" + DataBinder.Eval(Container.DataItem, "icone") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Width="60px" />
                                    <ItemStyle Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Width="60px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="painel" HeaderText="painel" SortExpression="painel">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                &nbsp;<asp:Image ID="Image9" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/App_Themes/ActioAdms/acoes/logoff.png" />
                                &nbsp;este usuário nuca acessou o sistema
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" width="100%">
                        <asp:ObjectDataSource ID="odsHistorico" runat="server" 
                            DeleteMethod="DeleteByIdUsuario" InsertMethod="Inserir" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByIDUsuario" 
                            TypeName="Actio.Negocio.Historico">
                            <DeleteParameters>
                                <asp:Parameter Name="id_usuario" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="id_usuario" Type="String" />
                                <asp:Parameter Name="data" Type="String" />
                                <asp:Parameter Name="tipo" Type="String" />
                                <asp:Parameter Name="descricao" Type="String" />
                                <asp:Parameter Name="painel" Type="String" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:SessionParameter Name="id_usuario" SessionField="id" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>

        </asp:View>
    </asp:MultiView>
</asp:Content>

