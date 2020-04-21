<%--@Copyright ©: 2019
@Company: Familias de Esperanza
@Author: Jonathan Yos
@Update: Jonathan Yos
@Description: Aplicacion para creacion, modificacion y eliminacion de solicitudes--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio de Sesion</title>
    <link rel="shorcut icon" type="image/x-icon" href="Imagenes/favicon.png" sizes="16x16">
    <link rel="stylesheet" type="text/css" href="css/EstilosLogin.css"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
   <link href="https://fonts.googleapis.com/css?family=Pacifico&display=swap" rel="stylesheet"> 
    <link rel="stylesheet" type="text/css" href="css/style.css"/>
    <script>
        function noatras() {
            window.location.hash = "no-back-button";
            window.location.hash = "Again-No-back-button"
            window.onhashchange = function () {
                window.location.hash = "no-back-button";
            }
        }
    </script>
</head>
<body onload="noatras();">
    <form id="form1" runat="server" defaultbutton="Aceptarbtn">
    <div id="fondo">
        <div class="contenedor">
            <table  class="tablac" id="Formulario" runat="server">
                <tr>
                    <td>   
                        <div class="titulo">Solicitudes<span class="titulo2">IT</span></div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Image ID="Logo" runat="server" ImageUrl="~/Imagenes/FamiliasdeEsperanza_Logo_RGB.png" Width="200" CssClass="image"  />
                    </td>
                    
                </tr>
                <tr>
                    <td>

                      <b>  <asp:Label ID="lblUser" runat="server" Text="Usuario"></asp:Label> </b>

                    </td>
                </tr>
                <tr>

                    <td class="auto-style22">
                     <asp:TextBox ID="txtuser" runat="server" class="txtform" ></asp:TextBox></td>
                    
                </tr>
                
                  <tr>
                    <td class="auto-style23">
                      <b><asp:Label ID="lblpass" runat="server" Text="Contraseña"></asp:Label></b>  
                        </td>
                    
                </tr>
                <tr>
                    <td class="auto-style16">
                        <asp:TextBox ID="txtpass" runat="server" class="txtform"  TextMode="Password"></asp:TextBox>   </td>
                 
                </tr>
                <tr>
                    <td class="auto-style24">

                    </td>
                </tr>
                <tr>
                    <td class="auto-style11"> 
                      <b><asp:Button ID="Ingresar" CssClass="buttom" runat="server" Text="Ingresar "   Height="35px" />
                   </b>     </td>
                    
                </tr>
            </table>
            <table id="Advertenciatb" class="mensaje" runat="server">
                <tr>
                    <td class="auto-style12">
                        <b><asp:Label ID="lblad" runat="server" Text="ADVERTENCIA "></asp:Label></b>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblmens" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style12">
                        <asp:Button ID="Aceptarbtn" CssClass="buttom"  runat="server" Text="Aceptar"  Height="35px" />
                    </td>
                </tr>
            </table>
            
             <table id="Cambio_Contraseña" runat="server" class="mensaje tablac">
                 <tr>
                     <td class="auto-style12"><b>
                         <asp:Label ID="lblmensn" runat="server" CssClass="labelFormBig" Text="Ingrese la contraseña actual"></asp:Label>
                         </b>
                         <asp:TextBox ID="txtactualc" runat="server" Height="32px" TextMode="Password" Width="200px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td class="auto-style1"><b>
                         <asp:Label ID="lblmensnc" runat="server" CssClass="labelFormBig" Text="Ingrese la nueva contraseña"></asp:Label>
                         <br />
                         </b>
                         <asp:TextBox ID="txtnuevac" runat="server" Height="32px" TextMode="Password" Width="200px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td class="auto-style1"><b>
                         <asp:Label ID="lblmensa" runat="server" CssClass="labelFormBig" Text="Confirme la nueva contraseña"></asp:Label>
                         </b>
                         <asp:TextBox ID="txtconfirmarc" runat="server" Height="32px" TextMode="Password" Width="200px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td class="auto-style12">
                         <asp:Button ID="btncambio" runat="server" CssClass="buttom" Height="35px" OnClick="btncambio_Click" Text="Aceptar" />
                     </td>
                 </tr>
            </table>
            <table id="Instrucciones" runat="server" visible="false">
                <tr>
                    <td class="auto-style1"><b>
                        <asp:Label ID="lbltitul" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                        </b>
                        <br />
                        <asp:Label ID="lblmen" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lblreg1" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lblreg2" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lblreg3" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lblreg4" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lblreg5" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                    </td>
                </tr>
             </table>
            
             </div>
            </div>
    </form>
    <footer>
        ©Common Hope 2019. Ver.14022020
    </footer>
</body>

</html>
