<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmViewSubjectInfo.aspx.cs" Inherits="AdminTool.frmViewSubjectInfo" %>

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

        .style1 {
            width: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: middle;" class="style1">
                            <asp:ImageButton ID="ImageGoBack" ValidationGroup="text" runat="server" ImageUrl="~/Images/goBack.png"
                                Style="height: 30px; width: 30px; vertical-align: middle; margin: 5px" OnClientClick="if(!ValidateSearch()) return false;"
                                OnClick="ImageGoBack_Click" align="left" />
                        </td>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Manage Subjects Information" Style="font-weight: bold; text-align: left;"
                                Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                        <td style="vertical-align: middle; float: right; margin: 1%;">
                            <asp:TextBox ID="txtSearchBox" placeholder="Search here" AutoPostBack="true" EnableViewState="true" runat="server" Style="width: 200px; height: 25px; vertical-align: middle;"
                                MaxLength="50" CssClass="form-control" OnTextChanged="txtSearchBox_TextChanged"
                                BorderColor="#bbd3e9" />
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:ImageButton ID="btnSearch" ValidationGroup="text" runat="server" ImageUrl="~/Images/search_User.png"
                                Style="height: 30px; width: 30px; vertical-align: middle; display: none;"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hdnSearchTxt" runat="server" />
            </div>
        </div>
        <div style="padding: 20px;">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="2" align="right">
                        <div id="DivExport" runat="server">
                            <asp:Button ID="ImgAddSubject" runat="server" Text=" New Subject" CssClass="btn"
                                EnableViewState="false" Width="140px" CausesValidation="true" ValidationGroup="Group1"
                                OnClick="ImgAddSubject_Click" BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgExportToExcel" Text="Export EXCEL" OnClick="ImgExportToExcel_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgExportToCSV" Text="Export CSV" OnClick="ImgExportToCSV_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgExportToPDF" Text="Export PDF" OnClick="ImgExportToPDF_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <asp:Panel ID="AddNewSubject" runat="server">
                                <asp:TextBox ID="tbNewSubjectLine" placeholder="Add New Subject Info" ValidationGroup="text"
                                    runat="server" tyle="padding-left: 20px;  padding-right: 20px;" Style="width: 30%; height: 25px; background-color: #e5eef6;"
                                    Width="150px" onkeyup="OnsearchtextChanged();"
                                    MaxLength="50" CssClass="txtbox" BorderColor="#bbd3e9" />
                                <asp:Button ID="imgBtnNewSubjectLine" runat="server" Text="Add" CssClass="btn" EnableViewState="false"
                                    Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="imgBtnNewSubjectLine_Click"
                                    BackColor="#3E75CD" ForeColor="White" />
                                <asp:Button ID="imgBtnNewSubjectLineCancel" runat="server" Text="Cancel" CssClass="btn"
                                    EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                    OnClick="imgBtnNewSubjectLineCancel_Click" BackColor="#3E75CD" ForeColor="White" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" width="100%" style="border: 1px solid #4090fd;">
                            <asp:GridView ID="GridSubjectDetail" runat="server" Width="100%" AllowPaging="True"
                                OnPageIndexChanging="GridSubjectDetail_PageIndexChanging" AutoGenerateColumns="False"
                                GridLines="None" DataKeyNames="Id" OnRowEditing="GridSubjectDetail_RowEditing"
                                OnRowDeleting="GridSubjectDetail_RowDeleting" OnRowCancelingEdit="GridSubjectDetail_RowCancelingEdit"
                                OnRowUpdating="GridSubjectDetail_RowUpdating" OnRowDataBound="GridSubjectDetail_RowDataBound"
                                PageSize="20">
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
                                            <asp:HiddenField ID="hiddenSubjectId" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Subject Line
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectLine" runat="server" Text='<%# Eval("subjectLine") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:TextBox ID="tbSubjectLine" runat="server" Text='<%# Eval("subjectLine") %>'
                                                    Width="70%" CssClass="txtbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortbEmailId" runat="server" ControlToValidate="tbSubjectLine"
                                                    ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            IsApproved
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsApproved" runat="server" Text='<%# Eval("isApproved") %>'
                                                Width="70%" CssClass="txtbox"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:CheckBox ID="chkIsApproved" runat="server" Checked="false" Width="70%" CssClass="txtbox"></asp:CheckBox>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/edit.png" CancelImageUrl="~/Images/cancel_new.png"
                                        DeleteImageUrl="~/Images/delete.png" UpdateImageUrl="~/Images/save.png" ShowCancelButton="true"
                                        ShowDeleteButton="true" ShowEditButton="true" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                                    </asp:CommandField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnSubjectDetail" runat="server" OnClick="imgBtnSubjectDetail_Click"
                                                OnClientClick="return ConfirmAction(this);" CommandArgument='<%# Eval("Id") %>'
                                                ImageUrl="~/Images/Essentials_Icon_Set_V2.1_Expanded_Profile-128.png" Width="20px"
                                                ToolTip="User Split Info" />
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
</asp:Content>
