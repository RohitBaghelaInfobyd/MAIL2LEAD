<%@ Page Title="CRM Configuration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSettingCRM.aspx.cs" Inherits="AdminTool.frmCrmSetting" %>

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

        function ShowCrmPasswordClick() {
            if (document.getElementById("<%= tbGmailPassword.ClientID %>").type == "password") {
                document.getElementById("<%= tbGmailPassword.ClientID %>").type = "Text";
                document.getElementById("<%=lnlShowPortalPassword.ClientID %>").text = "Hide Password";
            }
            else {
                document.getElementById("<%= tbGmailPassword.ClientID %>").type = "Password";
                document.getElementById("<%=lnlShowPortalPassword.ClientID %>").text = "Show Password";
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
                            <asp:Label ID="lblHeader" runat="server" Text="CRM Setting" Style="font-weight: bold; text-align: left;"
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
                        <table style="width: 70%;">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblGmailId" AssociatedControlID="tbGmailId" Style="vertical-align: middle;" CssClass="control-label ">Gmail EmailId : </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbGmailId" CssClass="form-control"
                                        BorderColor="#bbd3e9" Style="background-color: #e5eef6; display: inline; margin: 2%;" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbGmailId"
                                        ErrorMessage="The field is required." ForeColor="Red" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblGmailPassword" AssociatedControlID="tbGmailPassword" Style="vertical-align: middle;" CssClass="control-label">Gmail Password : </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbGmailPassword" CssClass="form-control" TextMode="Password"
                                        BorderColor="#bbd3e9" Style="background-color: #e5eef6; display: inline; margin: 2%;" />
                                    <asp:LinkButton ID="lnlShowPortalPassword" runat="server" OnClientClick="ShowCrmPasswordClick();return false;">Show Password</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbGmailPassword"
                                        ErrorMessage="The field is required." ForeColor="Red" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblConfigurationToken" AssociatedControlID="tbConfigurationToken" Style="vertical-align: middle;" CssClass="control-label">ZohoCRM Auth token : </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbConfigurationToken" CssClass="form-control"
                                        BorderColor="#bbd3e9" Style="background-color: #e5eef6; display: inline; margin-left: 2%; margin-bottom: 2%; margin-top: 2%" autocomplete="off" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbConfigurationToken"
                                        ErrorMessage="The field is required." ForeColor="Red" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn" EnableViewState="false" 
                                        Width="125" CausesValidation="false" OnClick="btnUpdate_Click" BackColor="#3E75CD"
                                        ForeColor="White" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" Width="125"
                                        EnableViewState="false"  OnClick="btnUpdateCancel_Click" BackColor="#3E75CD" ForeColor="White" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

