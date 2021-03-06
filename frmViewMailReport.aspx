﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmViewMailReport.aspx.cs" Title="Analytics"
    Inherits="AdminTool.frmViewMailReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css-js/css/calendar.css" rel="stylesheet" />

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
            width: 48px;
        }

        .style2 {
            float: left;
            width: 535px;
        }

        .style3 {
            height: 86px;
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
                        <td style="vertical-align: middle; margin: 1%;" class="style2">
                            <asp:Label ID="lblHeader" runat="server" Text="Analytics " Style="font-weight: bold; text-align: left;" Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                        <td style="vertical-align: middle; float: right; margin: 1%; display: none;">
                            <asp:TextBox ID="txtSearchBox" placeholder="Search here" runat="server" Style="width: 200px; height: 25px; vertical-align: middle;"
                                MaxLength="50" CssClass="form-control" AutoPostBack="true" EnableViewState="true" OnTextChanged="txtSearchBox_TextChanged"
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
            <table style="width: 100%">
                <tr>
                    <td colspan="3" style="text-align: right">
                        <div id="DivExport" runat="server">
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
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Select Subject List : "></asp:Label>
                            <asp:DropDownList ID="dropDpownListOfAllSubjectList" runat="server">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="padding-top: 1%; padding-bottom: 1%;">
                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Select Status : "></asp:Label>
                        <asp:DropDownList ID="dropDownStatusOfReport" runat="server">
                            <asp:ListItem Text="All" Value="all"></asp:ListItem>
                            <asp:ListItem Text="Inserted" Value="ADD"></asp:ListItem>
                            <asp:ListItem Text="Updated" Value="UPDATE"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="pending"></asp:ListItem>
                            <asp:ListItem Text="Error" Value="ERROR"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblstartText" runat="server" Font-Bold="true" Text="Start Date : "></asp:Label>
                        <asp:TextBox runat="server" ID="tbStartDate" Width="15%" />
                        <asp:CalendarExtender ID="calStartDate" runat="server" Format="dd/MM/yyyy" CssClass="Calendar" PopupButtonID="tbStartDate" TargetControlID="tbStartDate"></asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="requiredFieldValidator2" runat="server" ControlToValidate="tbStartDate" ValidationGroup="SaveData" ErrorMessage="*" ForeColor="Red" Font-Bold="true">
                        </asp:RequiredFieldValidator>
                        <asp:Label ID="lblEndDate" runat="server" Font-Bold="true" Text="End Date : "></asp:Label>
                        <asp:TextBox runat="server" ID="tbEndDate" Width="15%" />
                        <asp:CalendarExtender ID="calEndDate" runat="server" Format="dd/MM/yyyy" CssClass="Calendar" PopupButtonID="tbEndDate" TargetControlID="tbEndDate"></asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="requiredFieldValidator1" runat="server" ControlToValidate="tbEndDate" ValidationGroup="SaveData" ErrorMessage="*" ForeColor="Red" Font-Bold="true">
                        </asp:RequiredFieldValidator>
                        <asp:Button runat="server" ID="btnApplyFilter" Text="Apply Filter" OnClick="btnApplyFilter_Click"
                            CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                            BackColor="#3E75CD" ForeColor="White" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" width="100%">
                            <asp:GridView ID="GridViewMailReport" runat="server" Width="101%"
                                AutoGenerateColumns="False" GridLines="None" DataKeyNames="DatabaseId">
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
                                            <asp:HiddenField ID="hiddenDataBaseId" runat="server" Value='<%# Eval("DatabaseId") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            CRM Record Id 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("record_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Subject Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectLine" runat="server" Text='<%# Eval("subjectLine") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            EmailId
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadEmailId" runat="server" Text='<%# Eval("value_from_mail") %>' MaxLength="60"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Submition Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubmitionDate" runat="server" Text='<%# Eval("serviceTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubmitStatus" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgMailDetailInfo" runat="server" ToolTip="Mail Details" OnClick="imgMailDetailInfo_Click"
                                                CommandArgument='<%# Eval("DatabaseId") %>'
                                                ImageUrl="~/Images/detail_screen.png" Width="20px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" Width="100px"></ItemStyle>
                                        <HeaderStyle CssClass="commondClass" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
