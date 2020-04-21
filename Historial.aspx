<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Historial.aspx.vb" Inherits="Historial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #form {
            width: 250px;
            margin: 0 auto;
            height: 50px;
        }

            #form p {
                text-align: center;
            }


        .ancho150 {
            width: 150px;
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

        input[type="radio"]:checked ~ label {
            color: orange;
        }

        .tamano25 {
            font-size: 25px;
            margin-left: 10px;
        }

        .formCont2 {
            position: relative;
        }

        .displayn {
            display: none;
        }

        .calificacion {
            width: 100%;
            text-align: center;
        }

        .labelFormTitle1 {
            font-size: 15px;
            text-decoration: solid;
        }

        .centrar4 {
            margin: 0 auto;
        }

        .sinposition {
            position: relative;
            width: auto;
        }

        .tmn {
            height: 25px;
        }
        .ocultar{
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="top23">
        <div class="centrar4">

            <asp:Panel ID="pnlsolicitante" CssClass="div-contenedor" runat="server" Visible="false">
                <p class="direccion"><b>Solicitante<i class="icon-asterisk" style="color: #C70000;"></i></b></p>
                <asp:DropDownList ID="ddlSolicitante" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList>
                <asp:Button ID="btnBuscar" CssClass="btn tmn" runat="server" Text="" />

            </asp:Panel>

            <h2 class="h2">Historial</h2>
<asp:Panel runat="server" class="formCont2 warnFrm2 sinposition centrar4" ID="pnlWarning" Visible="false">

                <div class="formdirec">
                    <i class="icon-list-alt direccion"></i>
                    <asp:Label ID="lblcategoria" runat="server" CssClass="direccion"></asp:Label>
                    <i class="icon-chevron-right direccion"></i>
                    <asp:Label ID="lblestado" CssClass="direccion" runat="server"></asp:Label>
                    <a href="Historial.aspx?v=2" class="btn derecha1"><i class="icon-undo"></i>Regresar</a>
                </div>
                <div class="formfecha">
                    <i class="icon-hashtag tamano15"></i>
                    <asp:Label ID="lblticket" runat="server" CssClass="tamano15"></asp:Label>
                    <i class="icon-calendar tamano15 izquierda5"></i>
                    <asp:Label ID="lblfecha" CssClass="tamano15" runat="server">       </asp:Label><i class="icon-clock-o tamano15 izquierda5"></i>
                    <asp:Label ID="lblhora" CssClass="tamano15" runat="server"></asp:Label>
                    <i class="icon-wrench tamano15 izquierda5"></i>
                    <asp:Label ID="lblusuarioit" CssClass="tamano15" runat="server"></asp:Label>
                    <i class="icon-user tamano15 izquierda5"></i>
                    <asp:Label ID="lblsolicitante" CssClass="tamano15" runat="server"></asp:Label>
                </div>
                <div class="formContHeader">
                    <asp:Label ID="lblVservicio" CssClass="labelFormTitle1" runat="server"></asp:Label>
                </div>
                <asp:Panel ID="pnldescripcion" runat="server" Visible="false">
                    <table class="todotamano">
                        <tr>
                            <td>
                                <p class="direccion"><b>Mensaje de Usuario</b></p>
                                <asp:TextBox ID="txtdescripcion" CssClass="descripcion" runat="server" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>

                    </table>
                </asp:Panel>

                <asp:Panel ID="pnlbotones" CssClass="formbotones" runat="server" Visible="false">
                    <asp:Button ID="btncancelar" runat="server" CssClass="btndefault " Text="Cancelar" />
                    <asp:Button ID="btnmodificar" runat="server" CssClass="btn" Text="Modificar" Height="30px" />
                </asp:Panel>


                <div class="formContBody">
                    <asp:Table ID="tblWait" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblVdesc" CssClass="labelForm" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="tblcalificacion" runat="server" CssClass="calificacion">
                        <asp:TableRow>
                            <asp:TableCell>
                                <p class="clasificacion detenido">
                                    <asp:Label ID="lblnota" runat="server" Text="3" CssClass="displayn"></asp:Label>
                                    <input name="estrellas" id="radio1" type="radio" value="5">
                                    <label for="radio1" class="icon-star tamano25"></label>
                                    <input name="estrellas" id="radio2" type="radio" value="4">
                                    <label for="radio2" class="icon-star tamano25"></label>
                                    <input name="estrellas" id="radio3" type="radio" value="3">
                                    <label for="radio3" class="icon-star tamano25"></label>
                                    <input name="estrellas" id="radio4" type="radio" value="2">
                                    <label for="radio4" class="icon-star tamano25"></label>
                                    <input name="estrellas" id="radio5" type="radio" value="1">
                                    <label for="radio5" class="icon-star tamano25"></label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblcomentarios" runat="server" CssClass="labelForm"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </asp:Panel>
            <div class="centrar4 fd" style="overflow-y: auto; overflow-x: auto;">
                <asp:GridView ID="gvhistorial" CssClass="tableform " AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="Ticket" />
                        <asp:BoundField DataField="Creacion" />
                        <asp:BoundField DataField="Categoria" />
                        <asp:BoundField DataField="Descripcion" />
                        <asp:BoundField DataField="Solicitante" />
                        <asp:BoundField DataField="UsuarioIT" />
                        <asp:BoundField DataField="Estado" />
                        <asp:BoundField DataField="Fecha Estado" />
                        <asp:TemplateField HeaderStyle-ForeColor="Black" ItemStyle-CssClass="ancho150">
                            <ItemTemplate>
                                <asp:Button ID="btnfinalizado" CommandName="cmdSeleccionar" CssClass="btn tmn" runat="server" Text="Seleccionar" />
                                <asp:Button ID="btnmodificar" CommandName="cmdModificar" CssClass="btndefault tmn" runat="server" Text="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="Black">
                            <ItemTemplate>
                                <asp:Button ID="btnCalificar" CommandName="cmdCalificar" CssClass="btndefault tmn" runat="server" Text="Calificar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <asp:Panel runat="server" class="formCont3 warnFrm3" ID="pnlhaysolicitu" Visible="false">
                <asp:Label ID="lblhistorialV" runat="server" class="labelFormTitle"></asp:Label>
            </asp:Panel>
            

        </div>
    </div>


    <script>
        $(document).ready(function () {
            //alert(Math.round(Math.random()*10));
            if ($('#<%=lblnota.ClientID%>').val() != '') { }
            else {
                switch ($('#<%=lblnota.ClientID%>').html()) {
                    case '1':
                        $('#radio5').prop("checked", true);
                        $("input[type=radio]").attr('disabled', true);
                        break;
                    case '2':
                        $('#radio4').prop("checked", true);
                        $("input[type=radio]").attr('disabled', true);
                        break;
                    case '3':
                        $('#radio3').prop("checked", true);
                        $("input[type=radio]").attr('disabled', true);
                        break;
                    case '4':
                        $('#radio2').prop("checked", true);
                        $("input[type=radio]").attr('disabled', true);
                        break;
                    case '5':
                        $('#radio1').prop("checked", true);
                        $("input[type=radio]").attr('disabled', true);
                        break;
                }
            }



            var ancho2 = $(body).width();
            var ancho4 = ancho2 - 265;
            var ancho2 = $('#drop').height();
            var ancho = ancho2 - 80;
            var ancho6 = $('#MainContent_gvhistorial').width();
            $('.top23').css('height', ancho)
            $('.top23').css('width', ancho4)
                $('.fd').css('height', ancho2 - 300 + 'px')
                $('.fd').css('width', ancho6 + 'px')
            

            $(window).resize(function () {
                var ancho2 = $(body).width();
                var ancho4 = ancho2 - 245;
                var ancho7 = $('#MainContent_gvhistorial').width();
                $('.top23').css('width', ancho4)
                var ancho2 = $('#drop').height();
                var ancho1 = ancho2 - 300;
                var ancho = ancho2 - 80;
                $('.top23').css('height', ancho)
                    $('.fd').css('height', ancho1 + 'px')
                    $('.fd').css('width', ancho7 + 'px')
                
            });

        });
    </script>
</asp:Content>

