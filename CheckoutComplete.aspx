<%@ Page Language="C#" AutoEventWireup="true" Title="Payment Complete" MasterPageFile="~/site.Master" CodeBehind="CheckoutComplete.aspx.cs" Inherits="AdminTool.Checkout.CheckoutComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd; margin-bottom: 5%;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="PAYMENT COMPLETE" Style="font-weight: bold; text-align: left;"
                                Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 5px;">
            <table style="width: 100%">
                <tr>
                    <td style="padding: 20px">
                        <h3 style="color: green;">We have recived your Payment Successfully. Goto Dashboard to see API status.</h3>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%;">
                        <asp:Label ID="lblTransactionId" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="padding: 20px">
                        <h3>Thank You!</h3>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px">
                        <asp:Button ID="Continue" runat="server" Text="Goto DashBoard" CssClass="btn" EnableViewState="false"
                            CausesValidation="true" ValidationGroup="Group1" OnClick="Continue_Click"
                            BackColor="#3E75CD" ForeColor="White" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
