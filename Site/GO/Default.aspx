<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="True"  ValidateRequest="false" CodeBehind="Default.aspx.cs" Inherits="ActioAdms_GO_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdms" Runat="Server">
    <asp:MultiView ID="mvAll" runat="server">
        <asp:View ID="View1" runat="server">
            <asp:Button ID="bt_novo_go" runat="server" Text="+ Novo" OnClick="bt_novo_go_Click" />
            &nbsp;<br />
            <br />
            <br />
            Listagem com todos os Grupos de Oração:<br />
            <asp:GridView ID="gridList" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsGO" EnableModelValidation="True" OnRowCommand="EditaGO">
                <Columns>
                    <asp:TemplateField HeaderText="Grupo de Oração" SortExpression="titulo">
                        <ItemTemplate>
                            <asp:LinkButton ID="lkgo" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="Editar" Text='<%# Bind("titulo") %>' CssClass="linksPretos"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Coordenador" SortExpression="coordenador">
                        <ItemTemplate>
                            <asp:LinkButton ID="lkcordenador" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="Editar" Text='<%# Bind("coordenador") %>' CssClass="linksPretos"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle Height="20px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ATIVO" HeaderText="status" SortExpression="status" />
                    <asp:TemplateField></asp:TemplateField>
                    <asp:TemplateField></asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsGO" runat="server" DeleteMethod="Delete" InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" TypeName="Actio.Negocio.GrupodeOracao" UpdateMethod="Atualizar">
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="id_usuario" Type="String" />
                    <asp:Parameter Name="titulo" Type="String" />
                    <asp:Parameter Name="regiao" Type="String" />
                    <asp:Parameter Name="paroquia" Type="String" />
                    <asp:Parameter Name="bairro" Type="String" />
                    <asp:Parameter Name="cidade" Type="String" />
                    <asp:Parameter Name="endereco" Type="String" />
                    <asp:Parameter Name="telefone" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                    <asp:Parameter Name="site" Type="String" />
                    <asp:Parameter Name="onibus" Type="String" />
                    <asp:Parameter Name="dia" Type="String" />
                    <asp:Parameter Name="hora" Type="String" />
                    <asp:Parameter Name="descricao" Type="String" />
                    <asp:Parameter Name="icone" Type="String" />
                    <asp:Parameter Name="status" Type="String" />
                    <asp:Parameter Name="forania" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="id" Type="String" />
                    <asp:Parameter Name="id_usuario" Type="String" />
                    <asp:Parameter Name="titulo" Type="String" />
                    <asp:Parameter Name="regiao" Type="String" />
                    <asp:Parameter Name="paroquia" Type="String" />
                    <asp:Parameter Name="bairro" Type="String" />
                    <asp:Parameter Name="cidade" Type="String" />
                    <asp:Parameter Name="endereco" Type="String" />
                    <asp:Parameter Name="telefone" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                    <asp:Parameter Name="site" Type="String" />
                    <asp:Parameter Name="onibus" Type="String" />
                    <asp:Parameter Name="dia" Type="String" />
                    <asp:Parameter Name="hora" Type="String" />
                    <asp:Parameter Name="descricao" Type="String" />
                    <asp:Parameter Name="icone" Type="String" />
                    <asp:Parameter Name="status" Type="String" />
                    <asp:Parameter Name="forania" Type="String" />
                </UpdateParameters>
            </asp:ObjectDataSource>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table width="90%">
                <tr>
                    <td style="padding: 15px" width="100%">Prencha os campos e clique em &quot;Enviar&quot; no final do formulário.</td>
                </tr>
                <tr>
                    <td width="100%">
                        <fieldset style="border-style: solid; border-width: 1px; border-color: #FF9900">
                            <legend align="left" style="vertical-align: middle">Dados do Coordenador do Grupo de Oração</legend>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">&nbsp;</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom" align="right">
                                        <asp:ImageButton ID="EditarGO0" runat="server" ImageUrl="~/App_Themes/ActioAdms/botoes/editar.png" TabIndex="3" Visible="False" OnClick="EditarGO_Click" />
                                        <asp:ImageButton ID="Cancelar0" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png" OnClick="Cancelar_Click" />
                                        <asp:ImageButton ID="SalvarGO0" runat="server" ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png" OnClick="SalvarGO_Click" TabIndex="3" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">status do coordenador:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:RadioButtonList ID="StatusCordenador" runat="server" CellPadding="5" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">inativo</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="1">ativo</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">nome:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="Nome" runat="server" Width="300px"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Nome" CssClass="avisos" ErrorMessage="faltou o nome!"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">e-mail:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="Email" runat="server" Width="300px"></asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email" CssClass="avisos" ErrorMessage="isto não se parece com um e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Email" CssClass="avisos" ErrorMessage="qual o e-mail?"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">senha:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="Senha" runat="server" Width="100px"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Senha" CssClass="avisos" ErrorMessage="informe uma senha, ex: trocar"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">nascimento:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="Ano" runat="server" MaxLength="10" SkinID="Data" Width="94px"></asp:TextBox>
                                        &nbsp;ex: 31/12/1970
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Ano" CssClass="avisos" ErrorMessage="corrija a data, ex.: 31/12/1974" ValidationExpression="\d{2}\/\d{2}\/\d{4}"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">telefone:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="Telefone1" runat="server" MaxLength="15" onkeyup="formataTelefone (this,event)" Width="110px" SkinID="Telefone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">celular:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="Celular" runat="server" MaxLength="15" onkeyup="formataTelefone (this,event)" Width="110px" SkinID="Telefone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">endereço:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="EnderecoCoordenador" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">bairro:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="BairroCoordenador" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">cidade:</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">
                                        <asp:TextBox ID="CidadeCoordenador" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom" width="150px" style="text-align: right">cep:</td>
                                    <td style="padding-left: 10px; padding-top: 10px;" valign="bottom">
                                        <asp:TextBox ID="CEP" runat="server" MaxLength="9" onkeyup="formataTelefone (this,event)" SkinID="CEP" Width="110px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" valign="bottom" width="150px">&nbsp;</td>
                                    <td style="padding-left: 10px; padding-top: 10px" valign="bottom">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle" width="150px">Foto coordenador:<br />
                                        <asp:Image ID="ImageSelecionada" runat="server" Visible="False" Width="98px" />
                                    </td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <br />
                                        <asp:FileUpload ID="fuIcone" runat="server" />
                                        <asp:HiddenField ID="hidIcone" runat="server" />
                                        <br />
                                        Caso não tenha uma foto agora, você poderá atualizar depois.</td>
                                </tr>
                            </table>
                        </fieldset> </td>
                </tr>
                <tr>
                    <td width="100%">
                        <fieldset style="border-style: solid; border-width: 1px; border-color: #FF9900">
                            <legend>&nbsp;Dados do Grupo de Oração </legend>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" class="style1" valign="middle" width="150px">status do GO</td>
                                    <td align="left" class="style1" style="padding: 10px" valign="middle">
                                        <asp:RadioButtonList ID="StatusGO" runat="server" CellPadding="5" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">inativo</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="1">ativo</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style1" valign="middle" width="150px">Nome do Grupo:</td>
                                    <td align="left" class="style1" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Titulo" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="lb_Titulo" CssClass="avisos" ErrorMessage="Qual o nome do Grupo de Oração?"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Região Episcopal:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:DropDownList ID="lb_Regiao" runat="server">
                                            <asp:ListItem>RENSA</asp:ListItem>
                                            <asp:ListItem>RENSC</asp:ListItem>
                                            <asp:ListItem>RENSP</asp:ListItem>
                                            <asp:ListItem>RENSE</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Forania:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Forania" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Paróquia:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Paroquia" runat="server" Width="300px"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="lb_Paroquia" CssClass="avisos" ErrorMessage="Qual a paróquia?"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Bairro:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Bairro" runat="server" Width="300px"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="lb_Bairro" CssClass="avisos" ErrorMessage="Informe o Bairro"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Cidade:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Cidade" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Endereço:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Endereco" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="lb_Endereco" CssClass="avisos" ErrorMessage="logradouro e número"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Telefone:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Telefone" runat="server" MaxLength="15" onkeyup="formataTelefone (this,event)" Width="110px" SkinID="Telefone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">e-mail:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Email" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="lb_Email" CssClass="avisos" ErrorMessage="e-mail em formato irregular, corrija" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">site, facebook ou blog:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Site" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="lb_Site" CssClass="avisos" ErrorMessage="corrija o endereço web, ex: http://www.actio.net.br" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Linhas de ônibus:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Onibus" runat="server" Rows="3" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Dia do grupo:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:DropDownList ID="ddl_Dia" runat="server">
                                            <asp:ListItem Value="domingo">domingo</asp:ListItem>
                                            <asp:ListItem Value="segunda-feira">segunda-feira</asp:ListItem>
                                            <asp:ListItem Value="terça-feira">terça-feira</asp:ListItem>
                                            <asp:ListItem Value="quarta-feira">quarta-feira</asp:ListItem>
                                            <asp:ListItem Value="quinta-feira">quinta-feira</asp:ListItem>
                                            <asp:ListItem Value="sexta-feira">sexta-feira</asp:ListItem>
                                            <asp:ListItem Value="sábado">sábado</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Horário de início:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox ID="lb_Hora" runat="server" MaxLength="5" onkeyup="formataHora (this,event)" SkinID="HORA" Width="35px"></asp:TextBox>
                                        h </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="150px">Descrição:</td>
                                    <td align="left" style="padding: 10px" valign="middle">
                                        <asp:TextBox runat="server" ID="Descricao" CssClass="EditorHtml" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">&nbsp;</td>
                                    <td style="padding: 15px">&nbsp;</td>
                                </tr>
                            </table>
                        </fieldset> </td>
                </tr>
                <tr>
                    <td align="center" width="100%">
                        <asp:ImageButton ID="EditarGO" runat="server" ImageUrl="~/App_Themes/ActioAdms/botoes/editar.png" TabIndex="3" Visible="False" OnClick="EditarGO_Click" />
                        <asp:ImageButton ID="Cancelar" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/ActioAdms/botoes/cancelar.png" OnClick="Cancelar_Click" />
                        <asp:ImageButton ID="SalvarGO" runat="server" ImageUrl="~/App_Themes/ActioAdms/botoes/salvar.png" TabIndex="3" Visible="False" OnClick="SalvarGO_Click" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server"></asp:View>
        <asp:View ID="View4" runat="server"></asp:View>
        <asp:View ID="View5" runat="server"></asp:View>
        <asp:View ID="View6" runat="server"></asp:View>

    </asp:MultiView>
</asp:Content>

