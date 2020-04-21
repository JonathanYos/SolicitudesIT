<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="CrearT.aspx.vb" Inherits="CrearT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .sub {
            display: inline-block;
            float: left;
            margin-top: 15px;
            margin-left: 10px;
            background: rgba(155,155,155,.5);
            border-radius: 5px;
            height:156.5px;
            max-width:250px;
        }

        .top23 {
            display: block;
        }

        .prueba3 {
        }

        .titcrear {
            background: #696969;
            color: #fff;
            border-radius: 50px;
            font-size: 13px;
        }

        .prueba3 input[type="checkbox"] {
            -webkit-appearance: none;
            position: relative;
            background: #E81123;
            width: 30px;
            height: 15px;
            outline: none;
            border-radius: 20px;
            box-shadow: inset 0 0 5px rgba(0,0,0,.2);
            /*transition: .5s;*/
        }

        .prueba3 input:checked[type="checkbox"] {
            background: #029800;
        }

        .prueba3 input[type="checkbox"]:before {
            content: '';
            position: absolute;
            width: 15px;
            height: 15px;
            border-radius: 20px;
            top: 0;
            left: 0;
            transform: scale(1.1);
            transition: .5s;
            background: #fff;
            box-shadow: 0 1px 2px rgba(0,0,0,.2);
        }

        .prueba3 input:checked[type="checkbox"]:before {
            left: 15px;
        }
        .centrar4 {
        margin:0 auto;
        }
    </style>
    <asp:Panel runat="server" class="formCont warnFrm izquierda225 " ID="pnlAdv" Visible="False">
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
    <asp:Panel runat="server" CssClass="top23" ID="formularios">
        <div class="centrar">

        
        <div class="sub">
            <table style="width: 100%; text-align: center;" runat="server" id="tblServicios">
                <tr>
                    <td colspan="2">
                        <b>
                            <p class="titcrear">Crear Tipos</p>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td style="min-width: 60px;">
                        <asp:Label ID="lblcategoria" runat="server"></asp:Label>
                        <i class="icon-asterisk" style="color: #C70000;"></i></td>
                    <td>
                        <asp:DropDownList ID="ddlcategoria" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbltipo" runat="server"></asp:Label>
                        <i class="icon-asterisk" style="color: #C70000;"></i></td>
                    <td>
                        <asp:TextBox ID="txbtipo" CssClass="ancho250 txtfila2" runat="server" Height="30" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    
                    <td colspan="2">
                        <asp:Button ID="btnaceptarser" CssClass="btn" runat="server" /><asp:Label ID="lblconteo" runat="server" Visible="false"></asp:Label></td>
                </tr>
            </table>
        </div>
        <div class="sub" >
            <table style="width: 100%; text-align: center;" runat="server" id="tblTareas">
                <tr>
                    <td colspan="2">
                        <b>
                            <p class="titcrear">Crear Tarea</p>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td style="min-width: 60px;">
                        <asp:Label ID="lbltiposol" runat="server"></asp:Label>
                        <i class="icon-asterisk" style="color: #C70000;"></i></td>
                    <td>
                        <asp:DropDownList ID="ddlservicio" CssClass="ancho250 txtfila2" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbltarea" runat="server"></asp:Label>
                        <i class="icon-asterisk" style="color: #C70000;"></i></td>
                    <td>
                        <asp:TextBox ID="txbtarea" CssClass=" ancho250 txtfila2" runat="server" Height="30" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnaceptartar" CssClass="btn" runat="server" /></td>
                </tr>
            </table>
        </div>
        <div class="sub" >
            <table style="width: 100%; text-align: center;" runat="server" id="tblAsignacion">
                <tr>
                    <td colspan="2">
                        <b>
                            <p class="titcrear">Asignar Tipos</p>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblasignacion" runat="server"></asp:Label>
                        <i class="icon-asterisk" style="color: #C70000;"></i>
                    </td>
                    <td>
                        <div class="prueba3">
<asp:CheckBox ID="chkasignacion" CssClass="ckb" runat="server" />
                        </div>
                        </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnguardarasig" CssClass="btn" runat="server" /></td>
                </tr>
            </table></div>
        </div>
    </asp:Panel>
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

