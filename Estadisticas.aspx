<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Estadisticas.aspx.vb" Inherits="Estadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="top23"> 
       <asp:Panel runat="server" class="formCont3 warnFrm3" ID="Panel1">
         <asp:GridView ID="gvtop10" CssClass="tableform" runat="server"></asp:GridView>       
   </asp:Panel>
    
   <asp:Panel runat="server" class="formCont3 warnFrm3" ID="pnlAdv">
      <div id="donutchart" style="width: 100%;"></div>  
   </asp:Panel> 
   </div>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
              ['Categoria', 'Cantidad de Solicitudes Resueltas'],
              ['Work', 11],
              ['Eat', 2],
              ['Commute', 2],
              ['Watch TV', 2],
              ['Sleep', 30]
            ]);

            var options = {
                title: 'Solicitudes por Categoria',
                pieHole: 0.4,
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
            chart.draw(data, options);
        }
    </script>
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