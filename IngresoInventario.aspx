<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="IngresoInventario.aspx.vb" Inherits="IngresoInventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="top23">
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

