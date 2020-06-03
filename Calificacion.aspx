<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Calificacion.aspx.vb" Inherits="Calificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        #form {
            width: 250px;
            margin: 0 auto;
            height: 50px;
        }
            #form p {
                text-align: center;
            }
        input[type="radio"] {
            display: none;
        }

        label {
            color: grey;
        }

        .clasificacion {
            direction: rtl;
            unicode-bidi: bidi-override;
        }

        label:hover,
        label:hover ~ label {
            color: orange;
        }

        input[type="radio"]:checked ~ label {
            color: orange;
        }

        .tamano25 {
            font-size: 25px;
            margin-left: 10px;
        }
        .formCont2 {
        position:relative;
        }
        .centrar4 {
        margin:0 auto;
        }
    </style>
    <div class="top23">
        <div class="centrar4">
            <h2 class="h2">Calificar Servicio</h2>
         <asp:Panel runat="server" class="formCont warnFrm centrar4" ID="pnlAdv" Visible="False">
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
         <asp:Panel runat="server" Style="max-height:600px; overflow-y:auto;" class="formCont2 warnFrm2 centrar4" ID="pnlhistorial" Visible="true">
            <asp:Label ID="lblavisono"  runat="server" CssClass="labelFormTitle"></asp:Label> 
             <asp:GridView CssClass="tableform centrar4" ID="gvhistorial" runat="server">
                 <Columns>
                <asp:TemplateField HeaderStyle-ForeColor="Black">
                    <ItemTemplate>
                        <asp:Button ID="btnselec" CssClass="btndefault" runat="server" Text="Seleccionar" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
             </asp:GridView>
         </asp:Panel>
        <asp:Panel runat="server" class="formCont2 warnFrm2 centrar4" ID="pnlWarning" Visible="true">
             <div class="formdirec">
                <i class="icon-list-alt color direccion"></i>
                <asp:Label ID="lblcategoria" runat="server" CssClass="direccion"></asp:Label>
                 <i class="icon-chevron-right izquierda5 direccion"></i>
                <asp:Label ID="lblestado" CssClass="direccion" runat="server"></asp:Label>
                <a href="Calificacion.aspx?v=2"class="btn derecha1"><i class="icon-undo"></i> Regresar</a>
            </div>
            <div class="formfecha">
                <i class="icon-hashtag tamano15 izquierda5"></i>
                <asp:Label ID="lblticket" runat="server" CssClass="tamano15"></asp:Label>
                <i class="icon-calendar tamano15 izquierda5"></i>
                <asp:Label ID="lblfecha" CssClass="tamano15" runat="server"> 
                </asp:Label><i class="icon-clock-o tamano15 izquierda5"></i>
                <asp:Label ID="lblhora" CssClass="tamano15" runat="server"></asp:Label>
                <i class="icon-user tamano15 izquierda5"></i>
                <asp:Label ID="lblsolicitante" CssClass="tamano15" runat="server"></asp:Label>
                <i class="icon-wrench tamano15 izquierda5"></i>
                <asp:Label ID="lblusuarioit" CssClass="tamano15" runat="server"></asp:Label>
            </div>
            <table style="width:100%; text-align:center;">
                <tr>
                    <td>
                       <b> <asp:Label ID="lblcalificacion" runat="server"></asp:Label></b>
                    </td>
                    </tr>
                <tr>
                    <td>
                        <form>
                        <p class="clasificacion">
                            <asp:HiddenField ID="hdfvalor" runat="server" />
                            <input name="estrellas" id="radio1" type="radio" value="5"><!--
          --><label for="radio1" class="icon-star tamano25"></label><!--
          --><input name="estrellas" id="radio2" type="radio" value="4"><!--
          --><label for="radio2" class="icon-star tamano25"></label><!--
          --><input name="estrellas" id="radio3" type="radio" value="3"><!--
          --><label for="radio3" class="icon-star tamano25"></label><!--
          --><input name="estrellas" id="radio4" type="radio" value="2"><!--
          --><label for="radio4" class="icon-star tamano25"></label><!--
          --><input name="estrellas" id="radio5" type="radio" value="1"><!--
          --><label for="radio5" class="icon-star tamano25"></label>
                        </p></form>
                    </td>
                </tr>
                <tr>
                    <td>
                       <b> <asp:Label ID="lblcomentarios" runat="server"></asp:Label></b>
                    </td>
                    </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtcomentarios" runat="server" CssClass="descripcion" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnaceptar" runat="server" CssClass="btn" /></td>
                </tr>
            </table>
            </asp:Panel></div>
    </div>
    <script>
        $(document).ready(function () {
            $('#radio1').click(function () {
                if ($('#radio1').is(':checked')) { $("#MainContent_hdfvalor").val("5");}
            });
            $('#radio2').click(function () {
                if ($('#radio2').is(':checked')) { $("#MainContent_hdfvalor").val("4");}
            });
            $('#radio3').click(function () {
                if ($('#radio3').is(':checked')) { $("#MainContent_hdfvalor").val("3");}
            });
            $('#radio4').click(function () {
                if ($('#radio4').is(':checked')) { $("#MainContent_hdfvalor").val("2"); }
            });
            $('#radio5').click(function () {
                if ($('#radio5').is(':checked')) { $("#MainContent_hdfvalor").val("1");}
            });
           
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

