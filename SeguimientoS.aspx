<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="SeguimientoS.aspx.vb" Inherits="SeguimientoS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .centrar4 {
            margin: 0 auto;
        }

        .gray th {
            background: #D9E1E8;
        }

        .mitad {
            overflow-y: auto;
            overflow-x: auto;
            max-height: 300px;
            border: 1px dotted #696969;
            margin-top: 10px;
            border-radius: 3px;
        }
        .qborde {
        border:none;
        }
        .button20 {
        margin-bottom:50px;
        }
    </style>
    <div class="top23 button20">
        <div class="centrar4">
            <h2 class="h2">Seguimiento de una Solicitud</h2>
            <asp:Panel runat="server" class="formCont3 warnFrm3" ID="pnlhaysolicitu">
                <asp:Label ID="lblhistorialV" runat="server" CssClass="labelFormTitle"></asp:Label>
            </asp:Panel>
            <asp:Button ID="btnVerInactivos" CssClass="btn" runat="server" />
            <asp:Panel runat="server" class="formCont warnFrm" ID="pnlAdv" Visible="False">
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
            <asp:Panel runat="server" class="formCont3 warnFrm3" ID="pnlWarning">
                <div class="formdirec">
                    <i class="icon-list-alt color direccion"></i>
                    <asp:Label ID="lblcategoria" runat="server" CssClass="direccion"></asp:Label><i class="icon-chevron-right izquierda5 direccion"></i>

                    <asp:Label ID="lblestado" CssClass="direccion" runat="server"></asp:Label>
                    <a href="SeguimientoS.aspx" class="btn derecha1"><i class="icon-undo"></i>Regresar</a>
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
                    <i class="icon-send tamano15 izquierda5"></i>
                    <asp:Label ID="lblparausuario" CssClass="tamano15" runat="server"></asp:Label>
                </div>
                <div class="formContHeader">
                    <p class="direccion"><b>Tipo de Servicio</b></p>
                    <asp:TextBox ID="txttitulo" CssClass="titulosol" runat="server"></asp:TextBox>
                    <div class="solicitud2">
                        <i class="icon-plus-circle"></i>Seleccionar Servicio
                    </div>
                    <asp:Button ID="btnguardarser" CssClass="btn" runat="server" Text="Guardar Servicio" />
                    <br />
                    <p id="mensajeguardado"></p>
                </div>
                <div class="formContBody">
                    <table class="todotamano">
                        <tr>
                            <td>
                                <p class="direccion" style="float: left;"><b>Descripcion</b></p>
                                <asp:TextBox ID="txbdescripcion" CssClass="descripcion2" runat="server" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                    </table>
                </div>
                <div class="formContBody">
                    <asp:HiddenField ID="hdfvalor2" runat="server" />
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <i class="icon-asterisk" style="color: #C70000;"></i>
                                <asp:Label ID="lblasignado" CssClass="lblfila2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblestadosol" CssClass="lblfila2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblprioridad" CssClass="lblfila2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlasignado" CssClass="txtfila2" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlestado" CssClass="txtfila2" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlprioridad" CssClass="txtfila2" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="formContBody">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <i class="icon-asterisk" style="color: #C70000;"></i>
                                <asp:Label ID="lblsitio" CssClass="lblfila2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblmeta" CssClass="lblfila2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlsitio" CssClass="txtfila2" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txbmeta" CssClass="txtfila2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="formContBody">
                    <table class="todotamano">
                        <tr>
                            <td>
                                <p class="direccion" style="float: left;"><b>Solucion</b></p>
                                <asp:TextBox ID="txtsolucion" CssClass="descripcion2" runat="server" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                    </table>
                </div>
                <div class="formbotones">
                    <asp:Button ID="btnguardarsol" CssClass="btn derecha15" runat="server" />
                </div>

            </asp:Panel>

            <asp:Panel runat="server" class="formCont3 warnFrm3" ID="pnltarea">
                <div class="formdirec">
                    <i class="icon-check-square-o color direccion"></i>
                    <asp:Label ID="lbltarea" runat="server" CssClass="direccion"></asp:Label>
                </div>
                <div class="formContHeader">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <p class="direccion"><b>Tipo de Tarea<i class="icon-asterisk" style="color: #C70000;"></i></b></p>
                            </td>
                            <td>
                                <p class="direccion"><b>No presencial</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddltipotarea" Style="min-width: 250px;" CssClass="txtfila2" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:CheckBox ID="cknopresente" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="formContBody">
                    <table class="todotamano">
                        <tr>
                            <td>
                                <p class="direccion" style="float: left;"><b>Descripcion</b></p>
                                <asp:TextBox ID="txbdescriptarea" CssClass="descripcion2" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:Label ID="Label8" CssClass="labelForm" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblVdesc" CssClass="labelForm" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="formbotones">
                    <asp:Label ID="lblguardarvalor" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btncancelar" runat="server" CssClass="btndefault " Text="Cancelar" Visible="false"/>
                    <asp:Button ID="btneliminar" runat="server" CssClass="btndefault " Text="Eliminar" Visible="false"/>
                    <asp:Button ID="btnmodificar" runat="server" CssClass="btndefault " Text="Modificar" Visible="false"/>
                    <asp:Button ID="btnguardartarea" CssClass="btn derecha15" runat="server" />
                </div>
                <div class="formContBody">
                    <asp:GridView ID="gvhistorialtarea" CssClass="tableform centrar hovert" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Button ID="btnfinalizado" CommandName="cmdfinalizado" CssClass="btndefault" runat="server" Text="Finalizado" />
                                    <asp:Button ID="btnselect" CommandName="cmdselect" CssClass="btn" runat="server" Text="Seleccionar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="campo1" CssClass="centrar4 mitad" runat="server">
                <asp:GridView ID="gvhistorial" CssClass="tableform centrar4" runat="server" Visible="false" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="NoSolicitud" HeaderText="NoSolicitud"/>
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria"/>
                        <asp:BoundField DataField="Tipo" HeaderText="Servicio"/>
                        <asp:BoundField DataField="Estado" HeaderText="Estado"/>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ItemStyle-CssClass="ancho250"/>
                        <asp:BoundField DataField="Fecha de Solicitud" HeaderText="Fecha de Solicitud"/>
                        <asp:BoundField DataField="Solicitante" HeaderText="Solicitante"/>
                        <asp:TemplateField HeaderStyle-ForeColor="Black">
                            <ItemTemplate>
                                <asp:Button ID="btnselec" CssClass="btndefault" runat="server" Text="Seleccionar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
             <asp:Panel ID="pnlfiltros" CssClass="centrar4 mitad qborde" runat="server">
                <table style="margin:0 auto;" class="tableform">
                    <tr>
                        <th>Categoria</th>
                        <th>Estado</th>
                        <th>UsuarioIT</th>
                        <th>Prioridad</th>
                        <td rowspan="2"><asp:Button ID="btnBuscar" CssClass="btn" runat="server" Text="Buscar" /></td>
                    </tr>
                    <tr>
                        <td><asp:DropDownList ID="ddlcategoriafil" runat="server" CssClass="ddlform"></asp:DropDownList></td>
                        <td><asp:DropDownList ID="ddlestadofil" runat="server" CssClass="ddlform"></asp:DropDownList></td>
                        <td><asp:DropDownList ID="ddlusuarioitfil" runat="server" CssClass="ddlform"></asp:DropDownList></td>
                        <td><asp:DropDownList ID="ddlprioridadfil" runat="server" CssClass="ddlform"></asp:DropDownList></td>
                    </tr>
                </table>
             </asp:Panel>
            <asp:Panel ID="campo2" CssClass="centrar4 mitad button20" runat="server">
                <asp:GridView ID="gvhistorialinac" CssClass="tableform centrar4 gray" runat="server" Visible="false">
                    <Columns>
                        <asp:TemplateField HeaderStyle-ForeColor="Black">
                            <ItemTemplate>
                                <asp:Button ID="btnselecinc" CssClass="btndefault" runat="server" Text="Seleccionar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var historial1 = $('#MainContent_gvhistorial').width();
            var historial2 = $('#MainContent_gvhistorialinac').width();
            var altura = $('#MainContent_gvhistorial').height();
            var alturamenu = $('#drop').height();
           

            $('#MainContent_campo1').css('max-height', alturamenu-50+'px')
            if (historial1 > historial2 || historial2 == undefined) {
                $('#MainContent_campo1').css('width', historial1)
                $('#MainContent_campo2').css('width', historial1)
            } else {
                $('#MainContent_campo1').css('width', historial2)
                $('#MainContent_campo2').css('width', historial2)
            }
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
                var alturamenu = $('#drop').height();
                $('#MainContent_campo1').css('max-height', alturamenu - 50 + 'px')
            
                var historial2 = $(".top23").width();
                var altura = $('#MainContent_gvhistorial').height();
                
                
                    $('#MainContent_campo1').css('width', historial2-80+'px')
                    $('#MainContent_campo2').css('width', historial2-80+'px')
               
                
            });
        });
    </script>
</asp:Content>

