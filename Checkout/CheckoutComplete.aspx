<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CheckoutComplete.aspx.cs" Inherits="AdminTool.Checkout.CheckoutComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container">
        <br />      
        <div>
            <h1>Payment Complete</h1>
            <p></p>
            <h3>Payment Transaction ID:</h3>
            <asp:Label ID="TransactionId" runat="server"></asp:Label>
            <p></p>
            <h3>Thank You!</h3>
            <p></p>
            <hr />
            <asp:Button ID="Continue" CssClass="btn" runat="server" Text="Continue" OnClick="Continue_Click" />
        </div>
    </div>
</asp:Content>
