<%@ Page Title="" Language="C#" MasterPageFile="~/withoutsidepannel.Master" AutoEventWireup="true"
    CodeBehind="frmpassword.aspx.cs" Inherits="AdminTool.frmpassword" %>

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
                            <asp:Label ID="lblHeader" runat="server" Text="Reset Password" Style="font-weight: bold; text-align: left;" Font-Size="25"
                                Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 20px;">
            <table>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblPasswordTitle" runat="server" Text="Please Enter Password Below" Style="font-size: 25px;"></asp:Label>
                        <p>&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblpassword" runat="server" Text="Password" Style="font-size: 20px;"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbPassword" placeholder="Enter Password" ValidationGroup="text" TextMode="Password"
                            runat="server" MaxLength="15" class="form-control" BorderColor="#bbd3e9" Style="margin-left: 10%; padding: 5px !important; font-size: 22px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblconfirmpassword" runat="server" Text="Confirm Password" Style="font-size: 20px;"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbConfirmPassword" placeholder="Enter Confirm Password" ValidationGroup="text" TextMode="Password"
                            runat="server" MaxLength="15" class="form-control" BorderColor="#bbd3e9" Style="margin-left: 10%; padding: 5px !important; font-size: 22px;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMsg" runat="server" Visible="false" Text="text" Style="color: red;"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="imgbtnResetPassword" runat="server" Text="Submit" CssClass="btn"
                            EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1" Style="text-align: center;"
                            OnClick="imgbtnResetPassword_Click" BackColor="#3E75CD" ForeColor="White" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>

</asp:Content>
