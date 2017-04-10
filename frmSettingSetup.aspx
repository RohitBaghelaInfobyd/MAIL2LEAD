<%@ Page Title="Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSettingSetup.aspx.cs" Inherits="AdminTool.frmSettingSetup" %>

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
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <div style="margin: 2px 5% 5% 5%;">
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; padding-top: 1em; padding-bottom: 1em;">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em;">
                                    <asp:ImageButton ID="btnUserProfileSeeting" runat="server" OnClick="btnUserProfileSeeting_Click" ImageUrl="~/Images/profile_setting.png" Style="outline: none;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em; background-color: black; width: 20%">
                                    <asp:Label ID="lblUserProfileSeeting" Text="Profile Setting" runat="server" Style="color: white;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: center; padding-top: 1em; padding-bottom: 1em">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em;">
                                    <asp:ImageButton ID="btnCrmSetting" runat="server" OnClick="btnCrmSetting_Click" ImageUrl="~/Images/crm_setting.png" Style="outline: none;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em; background-color: black;">
                                    <asp:Label ID="lblCrmSetting" Text="CRM Setting" runat="server" Style="color: white;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; padding-top: 1em; padding-bottom: 1em">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em;">
                                    <asp:ImageButton ID="btnLeadAssignment" runat="server" OnClick="btnLeadAssignment_Click" ImageUrl="~/Images/lead_assignment.png" Style="outline: none;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em; background-color: black;">
                                    <asp:Label ID="lblLeadAssignment" Text="Lead Assignment" runat="server" Style="color: white;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: center; padding-top: 1em; padding-bottom: 1em">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em;">
                                    <asp:ImageButton ID="btnUniqueIdentifier" runat="server" OnClick="btnUniqueIdentifier_Click" ImageUrl="~/Images/unique_identifier.png" Style="outline: none;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em; background-color: black;">
                                    <asp:Label ID="lblUniqueIdentifier" Text="Unique Identifier" runat="server" Style="color: white;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; padding-top: 1em; padding-bottom: 1em">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em;">
                                    <asp:ImageButton ID="btnForceSync" runat="server" OnClick="btnForceSync_Click" ImageUrl="~/Images/force_sync.png" Style="outline: none;" />
                                </td>
                            </tr>
                            <tr style="margin: 1em;">
                                <td style="text-align: center; padding: 1em; padding-bottom: 1em; background-color: black;">
                                    <asp:Label ID="lblForceSync" Text="Force Syncronize" runat="server" Style="color: white;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: center; padding-top: 1em; padding-bottom: 1em">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em;">
                                    <asp:ImageButton ID="btnPaymentModule" runat="server" OnClick="btnPaymentModule_Click" ImageUrl="~/Images/payment.png" Style="outline: none;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 1em; padding-bottom: 1em; background-color: black;">
                                    <asp:Label ID="lblPaymentModule" Text="Payment Subscription" runat="server" Style="color: white;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
