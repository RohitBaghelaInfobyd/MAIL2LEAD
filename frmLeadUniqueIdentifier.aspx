<%@ Page Title="Unique Identifier" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmLeadUniqueIdentifier.aspx.cs" Inherits="AdminTool.frmLeadUniqueIdentifier" %>

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

        .chkChoice input {
            margin-left: 20%;
        }

        .chkChoice td {
            padding-left: 10px;
            padding-right: 50px;
        }
         .commondClass {
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd; margin-bottom: 5%;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Lead Unique Identifier Setting" Style="font-weight: bold; text-align: left;"
                                Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 5px;">
            <table style="width: 100%">
                <tr>
                    <td>&nbsp;
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;                       
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center; padding-bottom: 1em;">
                            <asp:Panel ID="AddNewUniqueIdentifier" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label runat="server" Text="Select Lead Column : " Style="font-weight: bold; text-align: left;"></asp:Label>
                                            <asp:DropDownList ID="dropDownMailToLeadColumn" runat="server" OnSelectedIndexChanged="dropDownMailToLeadColumn_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>

                                        <td style="width: 20%;">
                                            <asp:Panel ID="actionPanel" runat="server">
                                                <asp:Label runat="server" Text="Select Action Type : " Style="font-weight: bold; text-align: left;"></asp:Label>
                                                <asp:DropDownList ID="dropDownActionType" runat="server" OnSelectedIndexChanged="dropDownMailToLeadColumn_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="And" Value="And"></asp:ListItem>
                                                    <asp:ListItem Text="OR" Value="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>

                                        <td style="width: 30%;">
                                            <asp:Button ID="btnAddNewEvent" runat="server" Text="Add New Rule" CssClass="btn" EnableViewState="false"
                                                Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="btnAddNewEvent_Click"
                                                BackColor="#3E75CD" ForeColor="White" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td style="vertical-align: middle; float: left; margin-bottom: 1em; ">
                        <asp:Panel ID="pnlRuleExpresstion" runat="server">
                            <asp:Label runat="server" Text="New Rule Expresstion : " Style="font-weight: bold; text-align: left;"></asp:Label>
                            <asp:Label ID="lblExpression" runat="server" Text=""
                                Font-Size="18px" Font-Names="Forum"></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>

                <tr style="border-bottom: 1px solid #4090fd;">
                    <td style="vertical-align: middle; float: left; margin-bottom: 0.5em; margin-top: 2em;">
                        <asp:Label ID="lblExistingLableRule" runat="server" Text="Existing Unique Identifier Rule" Style="font-weight: bold; text-align: left;"
                            Font-Size="18px" Font-Names="Forum"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; float: left; margin-bottom: 0.5em; margin-top: 1em;">
                        <asp:Panel ID="pnlCurrentExpresstion" runat="server">
                            <asp:Label runat="server" Text="Current Rule Expresstion : " Style="font-weight: bold; text-align: left;"></asp:Label>
                            <asp:Label ID="lblCurrentExpression" runat="server" Text=""
                                Font-Size="18px" Font-Names="Forum"></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" style="width: 100%; border: 1px solid #4090fd;">
                            <asp:GridView ID="GridUniqueIdentifierDetail" runat="server" Width="100%"
                                AutoGenerateColumns="False"
                                GridLines="None" DataKeyNames="Id"
                                OnRowDataBound="GridUniqueIdentifierDetail_RowDataBound"
                                OnRowEditing="GridUniqueIdentifierDetail_RowEditing" OnRowDeleting="GridUniqueIdentifierDetail_RowDeleting"
                                OnRowCancelingEdit="GridUniqueIdentifierDetail_RowCancelingEdit" OnRowUpdating="GridUniqueIdentifierDetail_RowUpdating">
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
                                            <asp:HiddenField ID="hiddenUniqueIdentifierId" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Subject
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display: inline-block; width: 100%;">
                                                <asp:Label ID="lblUniqueIdentifierName" runat="server" Text='<%# Eval("leadColumnHeader") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            ActionType
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("actionType") %>'
                                                    Width="70%"></asp:Label>
                                                <div style="display: inline-block;">
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:DropDownList ID="dropDownActionTypeEdit" runat="server">
                                                    <asp:ListItem Text="And" Value="And"></asp:ListItem>
                                                    <asp:ListItem Text="OR" Value="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </EditItemTemplate>
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

                <tr>
                    <td>
                        <asp:Panel ID="actionRulePanel" Style="margin-top: 1em;" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Select Action : " Style="font-weight: bold; margin-right: 5%;"></asp:Label>
                                        <asp:DropDownList ID="dropdownAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropdownAction_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center; padding-top: 2em; padding-bottom: 1em;">
                                        <asp:Panel ID="PnlupdateLeadColumn" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" Text="Select Column to be Update : " Style="font-weight: bold; margin-right: 5%;"></asp:Label>
                                                        <asp:CheckBoxList ID="chkLeadColumnToUpdate" CssClass="chkChoice" RepeatDirection="Vertical" runat="server"></asp:CheckBoxList>
                                                        <asp:Button ID="btnSaveUpdateLeadColumnList" runat="server" Text="Save" CssClass="btn" EnableViewState="false"
                                                            Width="125" CausesValidation="true" Style="margin: 1em;" ValidationGroup="Group1" OnClick="btnSaveUpdateLeadColumnList_Click"
                                                            BackColor="#3E75CD" ForeColor="White" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

