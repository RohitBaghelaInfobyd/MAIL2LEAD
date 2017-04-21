<%@ Page Title="Profile Setting" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSettingUser.aspx.cs" Inherits="AdminTool.frmSettingUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1 {
            height: 18px;
        }

        .style7 {
            width: 474px;
        }

        .style8 {
            width: 47px;
        }
    </style>

    <script type="text/javascript">

        function ShowPortalPasswordClick() {
            if (document.getElementById("<%= tbPassword.ClientID %>").type == "password") {
                document.getElementById("<%= tbPassword.ClientID %>").type = "Text";
                document.getElementById("<%=lnkPortalPassword.ClientID %>").text = "Hide Password";
            }
            else {
                document.getElementById("<%= tbPassword.ClientID %>").type = "Password";
                document.getElementById("<%=lnkPortalPassword.ClientID %>").text = "Show Password";
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Profile Setting" Style="font-weight: bold; text-align: left;"
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
                        <table style="width: 65%">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblFirstName" AssociatedControlID="tbFirstName" Style="vertical-align: middle;" CssClass="control-label ">First Name : </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbFirstName" CssClass="form-control"
                                        BorderColor="#bbd3e9" Style="background-color: #e5eef6; display: inline; margin: 2%;" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbFirstName"
                                        ErrorMessage="The field is required." ForeColor="Red" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblLastName" AssociatedControlID="tbLastName" Style="vertical-align: middle;" CssClass="control-label">Last Name : </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbLastName" CssClass="form-control"
                                        BorderColor="#bbd3e9" Style="background-color: #e5eef6; display: inline; margin: 2%;" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbLastName"
                                        ErrorMessage="The field is required." ForeColor="Red" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblEmail" AssociatedControlID="tbEmail" Style="vertical-align: middle;" CssClass="control-label">Email : </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control"
                                        BorderColor="#bbd3e9" Style="background-color: #e5eef6; display: inline; margin: 2%;" autocomplete="off" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEmail"
                                        ErrorMessage="The field is required." ForeColor="Red" />

                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="tbEmail" ErrorMessage="Invalid Email Format" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblChangePassword" AssociatedControlID="tbPassword" Style="vertical-align: middle;" CssClass="control-label">Password : </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbPassword" CssClass="form-control" TextMode="Password"
                                        BorderColor="#bbd3e9" Style="background-color: #e5eef6; display: inline; margin: 2%;" autocomplete="off" />
                                    <asp:LinkButton ID="lnkPortalPassword" runat="server" OnClientClick="ShowPortalPasswordClick();return false;">Show Password</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPassword"
                                        ErrorMessage="The field is required." ForeColor="Red" />

                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn"
                                        Width="125" BackColor="#3E75CD" OnClick="btnSave_Click"
                                        ForeColor="White" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" Width="125" Style="margin-left: 2%"
                                        CausesValidation="true" OnClick="btnCancel_Click" BackColor="#3E75CD" ForeColor="White" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
