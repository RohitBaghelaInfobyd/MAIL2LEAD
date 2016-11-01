<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmUserList.aspx.cs" Inherits="AdminTool.frmUserList" %>

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
            height: 55px;
        }

        .style3 {
            height: 55px;
            width: 43px;
        }

        .style4 {
            float: left;
            width: 525px;
        }
    </style>
    <script type="text/javascript">

        function onlyAlphabets(e, t) {
            try {
                var msg = document.getElementById("<%= emptyListMsg.ClientID %>");
                msg.style.color = "Red";
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32)) {
                    $(msg).text("");
                    return true;
                }
                else {
                    $(msg).text("Invalid Name>");
                    return false;
                }
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: middle;" class="style3">
                            <asp:ImageButton ID="ImageGoBack5" runat="server" ImageUrl="~/Images/goBack.png"
                                OnClick="ImageGoBack5_Click" Style="height: 30px; width: 30px; vertical-align: middle; margin: 5px;"
                                ValidationGroup="text" align="left" />
                        </td>
                        <td style="vertical-align: middle; margin: 1%;" class="style4">
                            <asp:Label ID="lblHeader" runat="server" Text="Manage Users" Style="font-weight: bold; text-align: left;" Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                        <td style="vertical-align: middle; float: right; margin: 1%;">
                            <asp:TextBox ID="txtSearchBox" placeholder="Search here" runat="server" EnableViewState="true" AutoPostBack="true" Style="width: 200px; height: 25px; vertical-align: middle;"
                                MaxLength="50" CssClass="form-control" OnTextChanged="txtSearchBox_TextChanged"
                                BorderColor="#bbd3e9" />
                            <asp:ImageButton ID="btnSearch" ValidationGroup="text" runat="server" ImageUrl="~/Images/search_User.png"
                                Style="height: 30px; width: 30px; vertical-align: middle; display: none;" OnClientClick="if(!ValidateSearch()) return false;"
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
                    <td class="style1">
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="2" align="right" class="style1">
                        <div id="DivExport" runat="server">
                            <asp:Button runat="server" ID="ImgAddNewUser" Text="Add New User" OnClick="ImgAddNewUser_Click1"
                                CssClass="btn" EnableViewState="false" Width="139px" CausesValidation="true"
                                ValidationGroup="Group1" BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgExportToExcel" Text="Export EXCEL" OnClick="ImgExportToExcel_Click"
                                CssClass="btn" EnableViewState="false" Width="128px" CausesValidation="true"
                                ValidationGroup="Group1" BackColor="#3E75CD" ForeColor="White" />
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
                    <td colspan="3">
                        <br />
                        <script type="text/javascript">
                            function confirmation() {
                                if (confirm('Are you sure you want to delete ?')) {
                                    return true;
                                } else {
                                    return false;
                                }
                            }
                        </script>
                        <div id="GroupDetails" runat="server" width="100%">
                            <asp:GridView ID="GridUserDetails" runat="server" Width="101%" AllowPaging="True"
                                OnPageIndexChanging="GridUserDetails_PageIndexChanging" AutoGenerateColumns="False"
                                GridLines="None" DataKeyNames="Id" OnRowEditing="GridUserDetails_RowEditing"
                                OnRowDeleting="GridUserDetails_RowDeleting" OnRowCancelingEdit="GridUserDetails_RowCancelingEdit"
                                OnRowUpdating="GridUserDetails_RowUpdating"
                                OnRowDataBound="GridUserDetails_RowDataBound" PageSize="20">
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
                                            <asp:HiddenField ID="hiddenUserId" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                            <asp:HiddenField ID="hiddenPassword" runat="server" Value='<%# Eval("password") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Email Id
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("emailId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:TextBox ID="tbEmailId" runat="server" Text='<%# Eval("emailId") %>' Width="120"
                                                    CssClass="txtbox" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortbEmailId" runat="server" ControlToValidate="tbFirstName"
                                                    ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            First Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:TextBox ID="tbFirstName" runat="server" Text='<%# Eval("FirstName") %>' Width="120"
                                                    CssClass="txtbox" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortbFirstName" runat="server"
                                                    ControlToValidate="tbFirstName" ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Last Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:TextBox ID="tbLastName" runat="server" Text='<%# Eval("LastName") %>' Width="120"
                                                    CssClass="txtbox" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortbLastName" runat="server"
                                                    ControlToValidate="tbLastName" ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/edit.png" CancelImageUrl="~/Images/cancel_new.png"
                                        DeleteImageUrl="~/Images/delete.png" UpdateImageUrl="~/Images/save.png" ShowCancelButton="true"
                                        ShowDeleteButton="false" ShowEditButton="true" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                                    </asp:CommandField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnDelete" runat="server" ToolTip="Delete User Record" OnClick="imgBtnDelete_Click"
                                                OnClientClick="return confirmation();" CommandArgument='<%# Eval("Id") %>' ImageUrl="~/Images/delete.png" />
                                            <asp:ImageButton ID="imgBtnUserDetail" runat="server" ToolTip="User Details" OnClick="imgBtnUserDetail_Click"
                                                OnClientClick="return ConfirmAction(this);" CommandArgument='<%# Eval("Id") %>'
                                                ImageUrl="~/Images/Essentials_Icon_Set_V2.1_Expanded_Profile-128.png" Width="20px" />
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
