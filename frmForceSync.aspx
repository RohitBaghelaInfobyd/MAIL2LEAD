<%@ Page Title="Force Syncronize" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmForceSync.aspx.cs" Inherits="AdminTool.frmForceSync"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css-js/css/calendar.css" rel="stylesheet" />
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
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Force Sync" Style="font-weight: bold; text-align: left;"
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
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblstartText" runat="server" Text="Start Date : "></asp:Label>
                                    <asp:TextBox runat="server" ID="tbStartDate" Width="15%" />
                                    <asp:CalendarExtender ID="calStartDate" runat="server" Format="dd/MM/yyyy" CssClass="Calendar" PopupButtonID="tbStartDate" TargetControlID="tbStartDate"></asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="requiredFieldValidator2" runat="server" ControlToValidate="tbStartDate" ValidationGroup="SaveData" ErrorMessage="*" ForeColor="Red" Font-Bold="true">
                                    </asp:RequiredFieldValidator>
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date : "></asp:Label>
                                    <asp:TextBox runat="server" ID="tbEndDate" Width="15%" />
                                    <asp:CalendarExtender ID="calEndDate" runat="server" Format="dd/MM/yyyy" CssClass="Calendar" PopupButtonID="tbEndDate" TargetControlID="tbEndDate"></asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="requiredFieldValidator1" runat="server" ControlToValidate="tbEndDate" ValidationGroup="SaveData" ErrorMessage="*" ForeColor="Red" Font-Bold="true">
                                    </asp:RequiredFieldValidator>
                                    <asp:Button runat="server" ID="btnForceSync" Text="Start Sync" OnClick="btnForceSync_Click"
                                        CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                        BackColor="#3E75CD" ForeColor="White" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

