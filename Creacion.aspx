<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Creacion.aspx.vb" Inherits="Creacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .ddlservicio {
            width: 200px;
            height: 20px;
            border-radius: 3px;
        }

        .div-contenedor {
            display: inline;
            float: left;
            width: 250px;
        }
    </style>
    <div class="top23">
        <h2 class="h2">Crear Ticket</h2>


        <asp:Panel runat="server" class="formCont2 warnFrm2" ID="pnlWarning">
            <div class="formdirec">
                <i class="icon-list-alt color direccion"></i>
                <asp:Label ID="lblcategoria" runat="server" CssClass="direccion"></asp:Label><i class="icon-chevron-right izquierda5 direccion"></i>
                <asp:Label ID="lblestado" CssClass="direccion" runat="server"></asp:Label>
            </div>
            <div class="formfecha">
                <i class="icon-hashtag tamano15"></i>
                <asp:Label ID="lblticket" runat="server" CssClass="tamano15"></asp:Label>
                <i class="icon-calendar tamano15"></i>
                <asp:Label ID="lblfecha" CssClass="tamano15" runat="server">       </asp:Label><i class="icon-clock-o tamano15"></i>
                <asp:Label ID="lblhora" CssClass="tamano15" runat="server"></asp:Label>
            </div>
            <div class="formContHeader">
                <p class="direccion"><b>Categoria</b></p>
                <asp:TextBox ID="txttitulo" CssClass="titulosol" runat="server"></asp:TextBox>
            </div>
            <asp:Panel ID="pnlopcionIT" runat="server" CssClass="formContHeader">
               <asp:Panel ID="pnlsolicitante" CssClass="div-contenedor" runat="server" Visible="true">
                    <p class="direccion"><b>Solicitante<i class="icon-asterisk" style="color: #C70000;"></i></b></p>
                    <asp:DropDownList ID="ddlSolicitante" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList>
                </asp:Panel>
                 <asp:Panel ID="pnlservicio" CssClass="div-contenedor" runat="server" Visible="true">
                    <p class="direccion"><b>Servicio<i class="icon-asterisk" style="color: #C70000;"></i></b></p>
                    <asp:DropDownList ID="ddlservicio" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList>
                </asp:Panel>
                <asp:Panel ID="pnlprioridad" CssClass="div-contenedor" runat="server" Visible="true">
                    <p class="direccion"><b>Prioridad<i class="icon-asterisk" style="color: #C70000;"></i></b></p>
                    <asp:DropDownList ID="ddlprioridad" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList>
                </asp:Panel>
                <asp:Panel ID="pnlasignado" CssClass="div-contenedor" runat="server" Visible="true">
                    <p class="direccion"><b>Asignado<i class="icon-asterisk" style="color: #C70000;"></i></b></p>
                    <asp:DropDownList ID="ddlusuarioIT" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList>
                </asp:Panel>
                <asp:Panel ID="pnlparausuario" CssClass="div-contenedor" runat="server" Visible="true">
                    <p class="direccion"><b>Para</b></p>
                    <asp:DropDownList ID="ddlparausuario"  CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList>
                </asp:Panel>
            </asp:Panel>
            <div class="formContBody" style="position: static;">
                <table class="todotamano">
                    <tr>
                        <td>
                            <p class="direccion"><b>Mensaje de Usuario</b></p>
                            <asp:TextBox ID="txtdescripcion" CssClass="descripcion" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:Label ID="lblVdesc" CssClass="labelForm" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>


            </div>
            <div class="formbotones">
                <asp:Button ID="btnAceptar" CssClass="btn derecha15" runat="server" />
            </div>
        </asp:Panel>


        <asp:Panel runat="server" class="formCont warnFrm izquierda225" ID="pnlAdv" Visible="false">
            <div class="formContHeader">
                <asp:Label ID="lblAdv" class="labelFormTitle" runat="server"></asp:Label>
            </div>
            <div class="formContBody">
                <asp:Table ID="tblWait" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblWarning" class="labelForm" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
            <div class="formContBody">
                <asp:Button ID="btnOk" class="btn" runat="server" Text="OK" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>

