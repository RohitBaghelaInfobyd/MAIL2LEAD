<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CheckoutReview.aspx.cs" Inherits="AdminTool.Checkout.CheckoutReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container">       
        <br />
        <div>
            <h1>Payment Review</h1>
            <p></p>
            <h3 >Payment Details:</h3>
            <asp:GridView ID="OrderItemList" runat="server" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Left" GridLines="Both" CellPadding="10" Width="500" BorderColor="#808080" BorderWidth="5">
                <Columns>
                    <asp:BoundField DataField="AppId" HeaderText="S.No." />
                    <asp:BoundField DataField="ProductName" HeaderText="Name" />
                    <asp:BoundField DataField="Total" HeaderText="Price" />
                </Columns>
            </asp:GridView>
            <asp:DetailsView ID="ShipInfo" runat="server" AutoGenerateRows="false" GridLines="None" CellPadding="0" BorderStyle="None" CommandRowStyle-BorderStyle="None">
                <Fields>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <h3>Address:</h3>                            
                            <asp:Label ID="FirstName" runat="server" Text='<%#: Eval("FirstName") %>'></asp:Label>
                            <asp:Label ID="LastName" runat="server" Text='<%#: Eval("LastName") %>'></asp:Label>
                            <br />
                            <asp:Label ID="Address" runat="server" Text='<%#: Eval("Address") %>'></asp:Label>
                            <br />
                            <asp:Label ID="City" runat="server" Text='<%#: Eval("City") %>'></asp:Label>
                            <asp:Label ID="State" runat="server" Text='<%#: Eval("State") %>'></asp:Label>
                            <asp:Label ID="PostalCode" runat="server" Text='<%#: Eval("PostalCode") %>'></asp:Label>
                            
                            <h3>Payment Total:</h3>
                            
                            <asp:Label ID="Total" runat="server" Text='<%#: Eval("Total", "{0:C}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
            <p></p>
            <hr />

            <asp:Button ID="CheckoutConfirm" Width="160px" CssClass="btn" runat="server" Text="Complete Payment" OnClick="CheckoutConfirm_Click" />
            <br />
        </div>
    </div>
</asp:Content>
