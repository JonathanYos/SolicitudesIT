<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Recordatorio.aspx.vb" Inherits="Recordatorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div top="top23" style="margin-left:225px; margin-top:55px;">
        <h2 class="h2">Recordatorio</h2>
<div class="sub" style="width: 280px; margin:0 auto;">
            <table style="width: 100%; text-align: center;">
                <tr>
                    <td colspan="2" ">
                        <b>
                            <p class="titcrear">Enviar Recordatorio</p>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td style="min-width:80px;">
                        <asp:Label ID="lblSolicitante" runat="server"></asp:Label>
                        <i class="icon-asterisk" style="color: #C70000;"></i>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSolicitante" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblservicio" runat="server"></asp:Label>
                        <i class="icon-asterisk" style="color: #C70000;"></i>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlservicior" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnguardar" CssClass="btn" runat="server" /></td>
                </tr>
            </table>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var ancho2 = $(body).width();
            var ancho4 = ancho2 - 265;
            var ancho2 = $('#drop').height();
            var ancho = ancho2 - 80;
            $('.top23').css('height', ancho)
            $('.top23').css('width', ancho4)
            $(window).resize(function () {
                var ancho2 = $(body).width();
                var ancho4 = ancho2 - 245;
                $('.top23').css('width', ancho4)
                var ancho2 = $('#drop').height();
                var ancho = ancho2 - 80;
                $('.top23').css('height', ancho)
            });
        });
    </script>
</asp:Content>

