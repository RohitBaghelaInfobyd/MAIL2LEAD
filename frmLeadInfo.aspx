<%@ Page Title="Lead Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmLeadInfo.aspx.cs" Inherits="AdminTool.frmLeadInfo" %>

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
             .commondClass {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="CRM Configuration" Style="font-weight: bold; text-align: left;"
                                Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 20px;">
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="2" style="text-align: right">
                        <div id="DivExport" runat="server">
                            <asp:Button runat="server" ID="btnAddNewLeadInfo" Text="Add Field" OnClick="ImgAddNewLeadColumn_Click1"
                                CssClass="btn" EnableViewState="false" Width="160" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <asp:Panel ID="AddNewLeadInfo" runat="server">
                                <table>
                                    <tr>
                                        <td style="vertical-align: middle; padding-right: 1em;">
                                            <asp:TextBox ID="tbNewLeadInfo" placeholder="Enter Field Name" runat="server"
                                                MaxLength="80" class="form-control" />
                                        </td>

                                        <td style="vertical-align: middle; padding-left: 1em;">
                                            <asp:Button ID="imgBtnNewLeadInfo" runat="server" Text="Add" CssClass="btn" EnableViewState="false"
                                                Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="imgBtnNewLeadInfo_Click"
                                                BackColor="#3E75CD" ForeColor="White" />
                                            <asp:Button ID="imgBtnNewLeadInfoCancel" runat="server" Text="Cancel" CssClass="btn"
                                                EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                                OnClick="imgBtnNewLeadInfoCancel_Click" BackColor="#3E75CD" ForeColor="White" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" width="100%" style="border: 1px solid #4090fd;">
                            <asp:GridView ID="GridLeadDetail" runat="server" Width="100%"
                                AutoGenerateColumns="False"
                                GridLines="None" DataKeyNames="Id"
                                OnRowDataBound="GridTemplateDetail_RowDataBound"
                                OnRowEditing="GridLeadDetail_RowEditing" OnRowDeleting="GridLeadDetail_RowDeleting"
                                OnRowCancelingEdit="GridLeadDetail_RowCancelingEdit" OnRowUpdating="GridLeadDetail_RowUpdating">
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
                                            <asp:HiddenField ID="hiddenLeadId" Visible="false" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                            <asp:HiddenField ID="hiddenColumnType" Visible="false" runat="server" Value='<%# Eval("columnType") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Field Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadColumnHeader" runat="server" Text='<%# Eval("leadColumnHeader") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block; vertical-align: middle;">
                                                <asp:TextBox ID="tbLeadColumnHeader" runat="server" Text='<%# Eval("leadColumnHeader") %>'
                                                    Width="60%" class="form-control" Style="display: inline-block;"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortbLeadColumnHeader" runat="server"
                                                    ControlToValidate="tbLeadColumnHeader" ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:CheckBox ID="chkItemIsSubscribe" runat="server" Checked='<%# Eval("isSubscribe").ToString().Equals("1") %>' Enabled="false" Width="70%" CssClass="txtbox"></asp:CheckBox>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:CheckBox ID="chkIsSubscribe" runat="server" Checked='<%# Eval("isSubscribe").ToString().Equals("1") %>' Width="70%" CssClass="txtbox"></asp:CheckBox>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle />
                                        <HeaderTemplate>
                                            Field Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:Label ID="lblColumnTypeValue" runat="server" Text='<%# Eval("columnTypeValue") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/edit.png" CancelImageUrl="~/Images/cancel_new.png"
                                        DeleteImageUrl="~/Images/delete.png" UpdateImageUrl="~/Images/save.png" ShowCancelButton="true"
                                        ShowDeleteButton="true" ShowEditButton="true" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle"
                                        HeaderText="Action">
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        <HeaderStyle CssClass="commondClass" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
</asp:Content>
