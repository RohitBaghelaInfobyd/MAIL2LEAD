<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmApiReport.aspx.cs"
    Inherits="AdminTool.frmApiReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListRowGrid
        {
            border-bottom: 10px #fff solid;
            background-color: #efefef;
            height: 50px;
            margin-left: 10px;
        }
        
        .ListRowGrid td
        {
            padding-left: 15px;
            vertical-align: middle;
        }
        
        .ListHeaderGrid
        {
            background-color: #BDC9D6;
            height: 50px;
        }
        
        .ListHeaderGrid th
        {
            padding-left: 15px;
            vertical-align: middle;
        }
        .style1
        {
            width: 109px;
        }
        .style2
        {
            width: 47px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: middle;" class="style2">
                            <asp:ImageButton ID="ImageGoBack2" runat="server" ImageUrl="~/Images/goBack.png"
                                OnClick="ImageGoBack2_Click" OnClientClick="if(!ValidateSearch()) return false;"
                                Style="height: 30px; width: 30px; vertical-align: middle; margin: 5px;" ValidationGroup="text"
                                align="left" />
                        </td>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="API Status Report" Style="font-weight: bold; text-align:left;" Font-Size="18"
                                Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 20px;">
            <table width="100%">
                <tr>
                    <td class="style1" align="Left">
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="2" align="right" class="style1">
                        <div id="DivExport" runat="server">
                            <asp:Button runat="server" ID="ImgUpdateInfoNewUser" Text="UPDATE" OnClick="ImgUpdateInfoNewUser_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgReport" Text="REPORT" OnClick="ImgReport_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" width="100%">
                            <asp:GridView ID="GridUserApiDetails" runat="server" Width="101%" AllowPaging="True"
                                AutoGenerateColumns="False" GridLines="None" DataKeyNames="Id">
                                <HeaderStyle CssClass="ListHeaderGrid" HorizontalAlign="Left" BorderColor="#bbd3e9"
                                    BackColor="#e5eef6" />
                                <RowStyle CssClass="ListRowGrid" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" Height="40" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            S No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hiddenUserId" runat="server" Value='<%# Eval("userid") %>'>
                                            </asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            API Description
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            API Call Count
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAPICallCount" runat="server" Text='<%# Eval("APICallCount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Remaning API Count
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemaningAPICount" runat="server" Text='<%# Eval("RemaningAPICount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Submit To CRM Count
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubmitToCRMCount" runat="server" Text='<%# Eval("SubmitToCRMCount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="imgButtonUpgrade" Text="UPGRADE" OnClick="imgBtnUserpayment_Click"
                                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"  BackColor="#3E75CD" ForeColor="White" />
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
    <asp:Label ID="emptyListMsg" runat="server" Text="No Record Found"></asp:Label>
</asp:Content>
