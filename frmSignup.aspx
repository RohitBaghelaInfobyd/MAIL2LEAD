<%@ Page Language="C#" MasterPageFile="~/withoutsidepannel.Master" AutoEventWireup="true" CodeBehind="frmSignup.aspx.cs"
    Inherits="AdminTool.frmSignup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link rel="stylesheet" type="text/css" href="css-js/css/slide_login_register.css">
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

        .style2 {
            width: 47px;
        }

        .table td {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; text-align: center; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Signup" Style="font-weight: bold; text-align: left;" Font-Size="25"
                                Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 10px 20px">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblPasswordTitle" runat="server" Text="Please Provide Below Information to complete your signup process" Style="font-size: 25px;"></asp:Label>
                        <p>&nbsp;</p>
                    </td>
                </tr>
                <tr style="vertical-align: middle">
                    <td style="vertical-align: middle">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name" Style="font-size: 22px;"></asp:Label>
                    </td>
                    <td style="vertical-align: middle">
                        <asp:TextBox ID="tbFirstName" placeholder="First Name" ValidationGroup="text"
                            runat="server" MaxLength="30" CssClass="txtfield" BorderColor="#bbd3e9" Style="margin-left: 10%; padding: 5px !important; font-size: 22px;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortbEndText" runat="server" ControlToValidate="tbFirstName"
                            ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr style="vertical-align: middle">
                    <td style="vertical-align: middle">
                        <asp:Label ID="lbllastName" runat="server" Text="Last Name" Style="font-size: 22px;"></asp:Label>
                    </td>
                    <td style="vertical-align: middle">
                        <asp:TextBox ID="tbLastName" placeholder="Last Name" ValidationGroup="text"
                            runat="server" MaxLength="50" CssClass="txtfield" BorderColor="#bbd3e9" Style="margin-left: 10%; padding: 5px !important; font-size: 22px;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbLastName"
                            ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="vertical-align: middle">
                    <td style="vertical-align: middle">
                        <asp:Label ID="lblEmailId" runat="server" Text="EmailId" Style="font-size: 22px;"></asp:Label>
                    </td>
                    <td style="vertical-align: middle">
                        <asp:TextBox ID="tbEmailID" placeholder="Email Id" ValidationGroup="text"
                            runat="server" MaxLength="50" CssClass="txtfield" BorderColor="#bbd3e9" Style="margin-left: 10%; padding: 5px !important; font-size: 22px;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEmailID"
                            ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="vertical-align: middle">
                    <td style="vertical-align: middle">
                        <asp:Label ID="lblPassword" runat="server" Text="Password" Style="font-size: 22px;"></asp:Label>
                    </td>
                    <td style="vertical-align: middle">
                        <asp:TextBox ID="tbPassword" placeholder="Password" ValidationGroup="text"
                            runat="server" MaxLength="50" CssClass="txtfield" BorderColor="#bbd3e9" Style="margin-left: 10%; padding: 5px !important; font-size: 22px;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPassword"
                            ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMsg" runat="server" Visible="false" Text="text" Style="color: red;"></asp:Label>
                        <p>&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="imgbtnSubmitSignup" runat="server" Text="Submit" CssClass="btn"
                            EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1" Style="text-align: center;"
                            OnClick="imgbtnSubmitSignup_Click" BackColor="#3E75CD" ForeColor="White" />
                        <asp:Button ID="imgCancelButton" runat="server" Text="Cancel" CssClass="btn"
                            EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1" Style="text-align: center;"
                            OnClick="imgCancelButton_Click" BackColor="#3E75CD" ForeColor="White" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
</asp:Content>
