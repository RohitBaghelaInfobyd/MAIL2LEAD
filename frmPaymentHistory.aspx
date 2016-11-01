<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPaymentHistory.aspx.cs" Inherits="AdminTool.frmPaymentHistory" %>

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
                            <asp:ImageButton ID="ImageGoBack" ValidationGroup="text" runat="server" ImageUrl="~/Images/goBack.png"
                                Style="height: 30px; width: 30px; vertical-align: middle; margin: 5px;" OnClientClick="if(!ValidateSearch()) return false;"
                                align="left" OnClick="ImageGoBack_Click" />
                        </td>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Payment Details" Style="font-weight: bold; text-align: left;"
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
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <tr>
                        <td colspan="3">
                            <br />
                            <div id="GroupDetails" runat="server" width="100%">
                                <asp:GridView ID="GridUserPaymentDetails" runat="server" Width="101%" AllowPaging="True"
                                    AutoGenerateColumns="False" GridLines="None"
                                    OnRowDataBound="GridUserPaymentDetails_RowDataBound"
                                    OnRowEditing="GridUserPaymentDetails_RowEditing"
                                    OnRowCancelingEdit="GridUserPaymentDetails_RowCancelingEdit"
                                    OnRowUpdating="GridUserPaymentDetails_RowUpdating"
                                    EnableViewState="true"
                                    DataKeyNames="Id" OnSelectedIndexChanged="GridUserPaymentDetails_SelectedIndexChanged">
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
                                                <asp:HiddenField ID="hiddenPaymentId" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="minWidth" />
                                            <HeaderTemplate>
                                                EmailId
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblemailId" runat="server" Text='<%# Eval("emailId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="minWidth" />
                                            <HeaderTemplate>
                                                TransactionId
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbltransactionId" runat="server" Text='<%# Eval("transactionId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="minWidth" />
                                            <HeaderTemplate>
                                                Payment Amount
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpaymentAmount" runat="server" Text='<%# Eval("PaymentAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                API Count
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblapiCount" runat="server" Text='<%# Eval("apiCount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Payment Source
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpaymentSource" runat="server" Text='<%# Eval("paymentSource") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                IsActive
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblisActive" runat="server" Text='<%# Eval("isActive") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <div style="display: inline-block;">
                                                    <asp:DropDownList ID="editDropDownActive" runat="server" EnableViewState="true">
                                                        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/edit.png" CancelImageUrl="~/Images/cancel_new.png"
                                            DeleteImageUrl="~/Images/delete.png" UpdateImageUrl="~/Images/save.png" ShowCancelButton="true"
                                            ShowDeleteButton="false" ShowEditButton="true" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="center" Width="100px"></ItemStyle>
                                        </asp:CommandField>
                                    </Columns>
                                </asp:GridView>
            </table>
        </div>
        <br />
        <asp:Label ID="emptyListMsg" runat="server" Text="No Record Found"></asp:Label>
    </div>
</asp:Content>


