<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="MenuLateral.ascx.cs" Inherits="adms_controles_MenuLateral" %>
<style type="text/css">
    .NavegacaoLateral
        {
            background-image: url('../imagens/botao_up.png');
            padding-left: 5px;
            padding-right: 3px;
            background-repeat: no-repeat;
            vertical-align: middle;
            text-align: left;
        }
    .NavegacaoLateral:hover
        {
            background-image: url('../imagens/botao.png');
        }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="left" valign="top">
                <asp:ImageButton ID="ibt_expandir" runat="server" CausesValidation="False" 
                    ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/plus.png" 
                    onclick="ibt_expandir_Click" ToolTip="Expandir Painel para icones e textos" />
                <asp:ImageButton ID="ibt_Reduzir" runat="server" CausesValidation="False" 
                    ImageAlign="Middle" ImageUrl="~/App_Themes/ActioAdms/botoes/minus.png" 
                    onclick="ibt_Reduzir_Click" ToolTip="Reduzir painel para icones" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:MultiView ID="mvNavegacao" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:Repeater ID="rptLinks" runat="server" DataSourceID="odsRecursos" 
                            OnItemCommand="RecursoSelecionado">
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="155px">
                                    <tr>
                                        <td class="NavegacaoLateral" height="35" valign="middle" width="155px">
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                CommandArgument='<%# Eval("url")%>' ImageAlign="Middle" 
                                                ImageUrl='<%# "~/imagens/recursos/" + Eval("icone") %>' />
                                            &nbsp;<asp:LinkButton ID="lbt_recurso" runat="server" CausesValidation="False" 
                                                CommandArgument='<%# Eval("url") %>' CssClass="links"><%# Eval("titulo") %></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Repeater ID="rptIcones" runat="server" DataSourceID="odsRecursos" 
                            OnItemCommand="RecursoSelecionado">
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td class="NavegacaoLateral" height="35px" valign="middle" align="center" width="35px">
                                            <asp:ImageButton ID="ibt_icones" runat="server" CausesValidation="False" 
                                                CommandArgument='<%# Eval("url") %>' ImageAlign="AbsMiddle" 
                                                ImageUrl='<%# "~/imagens/recursos/" + Eval("icone")  %>'
                                                ToolTip='<%# "ir para o painel " + Eval("titulo") %>' />
                                            </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
<asp:ObjectDataSource ID="odsRecursos" runat="server" InsertMethod="Inserir" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="SelectByIdUsuarioTipoUsuario" 
        TypeName="Actio.Negocio.Usuario_Recursos" UpdateMethod="AtualizarPorIdUsuario">
    <InsertParameters>
        <asp:Parameter Name="id_usuario" Type="String" />
        <asp:Parameter Name="id_recurso" Type="String" />
    </InsertParameters>
    <SelectParameters>
        <asp:SessionParameter Name="id" SessionField="Id_Usuario" Type="String" />
        <asp:SessionParameter Name="tipo" SessionField="Tipo_Usuario" Type="String" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="id_usuario" Type="String" />
        <asp:Parameter Name="id_recurso" Type="String" />
    </UpdateParameters>
    </asp:ObjectDataSource>
</ContentTemplate>
</asp:UpdatePanel>