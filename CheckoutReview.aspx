<%@ Page Language="C#" AutoEventWireup="true" Title="Payment Review" MasterPageFile="~/site.Master" CodeBehind="CheckoutReview.aspx.cs" Inherits="AdminTool.Checkout.CheckoutReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd; margin-bottom: 5%;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="PAYMENT REVIEW" Style="font-weight: bold; text-align: left;"
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
                        <asp:Label ID="lblAPICount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px">
                        <asp:Label ID="lblTotalPayment" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px">
                        <asp:Button ID="CheckoutConfirm" runat="server" Text="Complete Payment" CssClass="btn" EnableViewState="false"
                            CausesValidation="true" ValidationGroup="Group1" OnClick="CheckoutConfirm_Click"
                            BackColor="#3E75CD" ForeColor="White" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
