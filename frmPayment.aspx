<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPayment.aspx.cs" Inherits="AdminTool.frmPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListRowGrid {
            border-bottom: 10px #fff solid;
            background-color: #efefef;
            height: 50px;
            margin-left: 10px;
        }

            .ListRowGrid td {
                padding-left: 15px;
                vertical-align: middle;
            }

        .ListHeaderGrid {
            background-color: #BDC9D6;
            height: 50px;
        }

            .ListHeaderGrid th {
                padding-left: 15px;
                vertical-align: middle;
            }

        .style2 {
            width: 47px;
        }

        .table td {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: middle;" class="style2">
                            <asp:ImageButton ID="ImageGoBack3" ValidationGroup="text" runat="server" ImageUrl="~/Images/goBack.png"
                                Style="height: 30px; width: 30px; vertical-align: middle; margin: 5px;" OnClientClick="if(!ValidateSearch()) return false;"
                                OnClick="ImageGoBack3_Click" align="left" />
                        </td>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Payment" Style="font-weight: bold; text-align: left;"
                                Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 20px;">
            <table width="100%">
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblMsg" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="2" align="right" class="style1">
                        <div id="DivExport" runat="server">
                            <asp:Button runat="server" ID="ImgPaymentHistory" Text="Payment History" OnClick="ImgPaymentHistory_Click"
                                CssClass="btn" EnableViewState="false" Width="151px" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" width="100%">
                            <asp:GridView ID="GridPaymentDetails" runat="server" Width="101%" AllowPaging="True"
                                AutoGenerateColumns="False" GridLines="None" DataKeyNames="Id">
                                <HeaderStyle CssClass="ListHeaderGrid" HorizontalAlign="Left" BorderColor="#bbd3e9"
                                    BackColor="#e5eef6" />
                                <RowStyle CssClass="ListRowGrid" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" Height="40" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            S No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hiddenUserId" runat="server" Value='<%# Eval("id") %>' />
                                            <asp:HiddenField ID="hiddenPaymentAmout" runat="server" Value='<%# Eval("paymentAmount") %>' />
                                            <asp:HiddenField ID="hiddenApiCount" runat="server" Value='<%# Eval("APICallCount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            API Description
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Buy
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="imgButtonbuy" runat="server" ToolTip="But" Text="Buy" CssClass="btn"
                                                OnClientClick="return confirmation();" Style="color: White; background-color: #3E75CD; border: none; width: 60%;"
                                                OnCommand="imgButtonbuy_Command"
                                                CommandArgument='<%#Container.DataItemIndex+1 %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
    <asp:Label ID="emptyListMsg" runat="server" Text="NA"></asp:Label>
</asp:Content>
