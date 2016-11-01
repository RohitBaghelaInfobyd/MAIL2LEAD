<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CheckoutError.aspx.cs" Inherits="AdminTool.Checkout.CheckoutError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container">
        <br />
        <br />
        <div>
            <h1>Payment Error</h1>
            <p></p>
            <table id="ErrorTable">
                <tr>
                    <td class="field"></td>
                    <td><%=Request.QueryString.Get("ErrorCode")%></td>
                </tr>
                <tr>
                    <td class="field"></td>
                    <td><%=Request.QueryString.Get("Desc")%></td>
                </tr>
                <tr>
                    <td class="field"></td>
                    <td><%=Request.QueryString.Get("Desc2")%></td>
                </tr>
            </table>
            <p></p>
        </div>
    </div>
</asp:Content>

