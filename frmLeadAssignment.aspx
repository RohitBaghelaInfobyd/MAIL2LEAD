<%@ Page Title="Lead Assignment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmLeadAssignment.aspx.cs" Inherits="AdminTool.frmLeadAssignment" %>

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
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Lead Owner Assignment" Style="font-weight: bold; text-align: left;"
                                Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 10px;">
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
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:RadioButton ID="radioTypeNormal" runat="server" Text="Normal" GroupName="leadAssignmentType" OnCheckedChanged="radioTypeNormal_CheckedChanged" AutoPostBack="true" />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:RadioButton ID="radioTypeRoundRobin" runat="server" Text="Round Robin" GroupName="leadAssignmentType" OnCheckedChanged="radioTypeRoundRobin_CheckedChanged" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 2em; padding-bottom: 1em;">
                                    <div>
                                        <asp:Panel ID="AddNewLeadAssignmentInfo" runat="server">
                                            <table style="width: 65%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Lead Owner Name : " Style="vertical-align: middle;" CssClass="control-label "></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; padding-right: 1em;">
                                                        <asp:TextBox ID="tbAddNewLeadAssignmentInfo" Width="100%" placeholder="Enter Owner Name" runat="server"
                                                            MaxLength="80" class="form-control" />
                                                    </td>

                                                    <td style="vertical-align: middle;">
                                                        <asp:Button ID="btnAddNewLeadAssignmentInfo" runat="server" Text="Add" CssClass="btn" EnableViewState="false"
                                                            Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="btnAddNewLeadAssignmentInfo_Click"
                                                            BackColor="#3E75CD" ForeColor="White" />
                                                        <asp:Button ID="btnCancelNewLeadAssignmentInfo" runat="server" Text="Reset" CssClass="btn"
                                                            EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                                            OnClick="btnCancelNewLeadAssignmentInfo_Click" BackColor="#3E75CD" ForeColor="White" />
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
                                        <asp:GridView ID="GridLeadAssignmentDetail" runat="server" Width="100%"
                                            AutoGenerateColumns="False"
                                            GridLines="None" DataKeyNames="Id"
                                            OnRowEditing="GridLeadAssignmentDetail_RowEditing" OnRowDeleting="GridLeadAssignmentDetail_RowDeleting"
                                            OnRowCancelingEdit="GridLeadAssignmentDetail_RowCancelingEdit" OnRowUpdating="GridLeadAssignmentDetail_RowUpdating">
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
                                                        <asp:HiddenField ID="hiddenAssignmentId" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemStyle CssClass="minWidth" />
                                                    <HeaderTemplate>
                                                        Lead Owner Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="display: inline-block; width: 100%;">
                                                            <asp:Label ID="lblAssignmentTitle" runat="server" Text='<%# Eval("assignmentName") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <div style="display: inline-block; width: 100%;">
                                                            <asp:TextBox ID="tbAssignmentTitle" runat="server" Text='<%# Eval("assignmentName") %>'
                                                                Width="70%" class="form-control" Style="display: inline-block;"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortbLeadColumnHeader" runat="server"
                                                                ControlToValidate="tbAssignmentTitle" ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemStyle CssClass="minWidth" />
                                                    <HeaderTemplate>
                                                        Status
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="display: inline-block;">
                                                            <div style="display: inline-block;">
                                                                <asp:CheckBox ID="lblAssignmentStatus" runat="server" Enabled="false" Checked='<%# Eval("assignmentStatus").ToString().Equals("1") %>' Width="70%" CssClass="txtbox"></asp:CheckBox>
                                                            </div>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <div style="display: inline-block;">
                                                            <asp:CheckBox ID="chkAssignmentStatus" runat="server" Checked='<%# Eval("assignmentStatus").ToString().Equals("1") %>' Width="70%" CssClass="txtbox"></asp:CheckBox>
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
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

