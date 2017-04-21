<%@ Page Title="Template Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmTemplate.aspx.cs" Inherits="AdminTool.frmTemplate" %>

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
                            <asp:Label ID="lblHeader" runat="server" Text="Subject Configuration" Style="font-weight: bold; text-align: left;"
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
                            <asp:Button runat="server" ID="btnAddNewTemplate" Text="Add Subject" OnClick="btnAddNewTemplate_Click"
                                CssClass="btn" EnableViewState="false" Width="160px" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <asp:Panel ID="AddNewTemplateInfo" runat="server">
                                <table style="width: 80%;">
                                    <tr>
                                        <td style="vertical-align: middle; padding-right: 1em;">
                                            <asp:TextBox ID="tbNewTemplateInfo" Width="100%" placeholder="Enter Subject title" runat="server"
                                                MaxLength="80" class="form-control" />
                                        </td>
                                         <td style="vertical-align: middle; padding-right: 1em;">
                                            <asp:Label ID="TextBox1" Width="100%" Text="Subject Type : " runat="server"
                                                MaxLength="80" Font-Bold="true" />
                                        </td>
                                        <td style="vertical-align: middle; padding-right: 1em;">
                                            <asp:DropDownList ID="dropDownTemplateType" runat="server">
                                                <asp:ListItem Text="LEAD" Value="LEAD"></asp:ListItem>
                                                <asp:ListItem Text="CONTACT" Value="CONTACT"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td style="vertical-align: middle; padding-left: 1em;">
                                            <asp:Button ID="btnAddNewTemplateInfoAdd" runat="server" Text="Add" CssClass="btn" EnableViewState="false"
                                                Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="btnAddNewTemplateInfoAdd_Click"
                                                BackColor="#3E75CD" ForeColor="White" />
                                            <asp:Button ID="btnAddNewTemplateInfoCancel" runat="server" Text="Cancel" CssClass="btn"
                                                EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                                OnClick="btnAddNewTemplateInfoCancel_Click" BackColor="#3E75CD" ForeColor="White" />
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
                            <asp:GridView ID="GridTemplateDetail" runat="server" Width="100%"
                                AutoGenerateColumns="False"
                                GridLines="None" DataKeyNames="Id"
                                OnRowEditing="GridTemplateDetail_RowEditing" OnRowDeleting="GridTemplateDetail_RowDeleting"
                                OnRowCancelingEdit="GridTemplateDetail_RowCancelingEdit" OnRowUpdating="GridTemplateDetail_RowUpdating">
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
                                        <ItemStyle Width="1px" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hiddenSubjectId" Visible="false" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Subject Title
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display: inline-block; width: 100%;">
                                                <asp:Label ID="lblSubjectTitle" runat="server" Text='<%# Eval("subjectLine") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block; width: 100%;">
                                                <asp:TextBox ID="tbSubjectTitleHeader" runat="server" Text='<%# Eval("subjectLine") %>'
                                                    Width="70%" class="form-control" Style="display: inline-block;"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortbLeadColumnHeader" runat="server"
                                                    ControlToValidate="tbSubjectTitleHeader" ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Module
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display: inline-block; width: 100%;">
                                                <asp:Label ID="lblSubjectType" runat="server" Text='<%# Eval("subjectType") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block; width: 100%;">
                                                <asp:DropDownList ID="gridDropDownTemplateType" runat="server">
                                                    <asp:ListItem Text="LEAD" Value="LEAD"></asp:ListItem>
                                                    <asp:ListItem Text="CONTACT" Value="CONTACT"></asp:ListItem>
                                                </asp:DropDownList>
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
                                                <asp:CheckBox ID="lblIsApproved" runat="server" Checked='<%# Eval("isApproved").ToString().Equals("1") %>' Width="70%" Enabled="false" CssClass="txtbox"></asp:CheckBox>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:CheckBox ID="chkIsApproved" runat="server" Checked='<%# Eval("isApproved").ToString().Equals("1") %>' Width="70%" CssClass="txtbox"></asp:CheckBox>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/edit.png" CancelImageUrl="~/Images/cancel_new.png"
                                        DeleteImageUrl="~/Images/delete.png" UpdateImageUrl="~/Images/save.png" ShowCancelButton="true"
                                        ShowDeleteButton="true" ShowEditButton="true" ItemStyle-Width="50"
                                        ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Right"
                                        HeaderText="Action">
                                        <ItemStyle HorizontalAlign="center" Width="100px"></ItemStyle>
                                        <HeaderStyle CssClass="commondClass" />
                                    </asp:CommandField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Split Info
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnSubjectDetail" runat="server" OnClick="imgBtnSubjectDetail_Click"
                                                OnClientClick="return ConfirmAction(this);" CommandArgument='<%# Eval("Id") %>'
                                                ImageUrl="~/Images/detail_screen.png" Width="20px"
                                                ToolTip="User Split Info" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" Width="100px"></ItemStyle>
                                        <HeaderStyle CssClass="commondClass" />
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
</asp:Content>
