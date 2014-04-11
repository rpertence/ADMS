<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="adms_Default" %>

<%@ Register Src="~/Login/ucLoginADMs.ascx" TagName="ucLoginADMs" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actio Comunicação, Sistema ADM´s, Belo Horizonte, Minas Gerais</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta name="robots" content="index, follow" />
    <meta name="author" content="Actio Comunicação Ltda | http://actiocomunicacao.com.br" />
    <meta name="reply-to" content="contato@actiocomunicacao.com.br" />
    <meta name="MSSmartTagsPreventParsing" content="TRUE" />
    <meta name="DC.title" content="Actio Comunicação, textos, site, meus, clientes, usuários, serviços, pordutos, notícias, publicidade, minhas, mídias, links, arquivos " />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="imagetoolbar" content="no" />
    <meta name="language" content="pt-br" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta name="googlebot" content="index, follow" />
    <script language="Javascript1.2"> 
            message = "Actio Comunicação Ltda: [31] 3317-0794";
 
            function NoRightClick(b) {
              if(((navigator.appName=="Microsoft Internet Explorer")&&(event.button > 1))
              ||((navigator.appName=="Netscape")&&(b.which > 1))){
              alert(message);
              return false;
              }
            }
            document.onmousedown = NoRightClick;
            // -->
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <table id="Principal" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="top" width="100%">
                    <table id="Conteudo" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right" valign="top" width="160">
                                <asp:Image ID="Actio" runat="server" AlternateText="Actio Comunicação Ltda."
                                    ImageAlign="Middle" ImageUrl="~/imagens/Actio.jpg" />
                            </td>
                            <td align="left"
                                style="background-image: url('imagens/bg_Adms.jpg'); background-repeat: repeat"
                                valign="top">
                                <asp:Image ID="Adms" runat="server"
                                    AlternateText="Administração de conteúdo web" ImageAlign="Top"
                                    ImageUrl="~/imagens/adms.jpg" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="160">&nbsp;</td>
                            <td align="left" valign="top">

                                <p style="padding: 30px">
                                    Seja bem vindo ao 
        ADM´s, sistema de adminsitração de conteúdo web!
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="160">&nbsp;</td>
                            <td align="left" valign="top">
                                <p>
                                    <uc1:ucLoginADMs ID="ucLoginADMs1" runat="server" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="160">&nbsp;</td>
                            <td align="right" valign="top">[31] 3317-07494 | contato@actio.net.br</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </form>
    <p class="style1">
        &nbsp;
    </p>
</body>
</html>
