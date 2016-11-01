<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmUserMailReport.aspx.cs" Inherits="AdminTool.frmUserMailReport" %>

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
            float: left;
            width: 49px;
        }
        .style2
        {
            float: left;
            width: 526px;
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
                            <asp:ImageButton ID="ImageGoBack5" ValidationGroup="text" runat="server" ImageUrl="~/Images/goBack.png"
                            Style="height: 30px; width: 30px; vertical-align: middle; margin:5px;" OnClientClick="if(!ValidateSearch()) return false;"
                            OnClick="ImageGoBack5_Click" align="left" />
                        </td>
                         <td style="vertical-align: middle; margin:1%;" class="style2">
                           <asp:Label ID="lblHeader" runat="server" Text="Manage Users" Style="font-weight: bold; text-align:left;" Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                         <td style="vertical-align: middle; float: right; margin:1%;">
                           <asp:TextBox ID="TextBox1" placeholder="Search here" runat="server" Style="width: 200px;
                                height: 25px; vertical-align: middle;" MaxLength="50" CssClass="form-control"
                                BorderColor="#bbd3e9" />
                            <asp:ImageButton ID="ImageButton1" ValidationGroup="text" runat="server" ImageUrl="~/Images/search_User.png"
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
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="2" align="right">
                        <div id="DivExport" runat="server">
                            <asp:Button runat="server" ID="ImgExportToExcel" Text="Export EXCEL" OnClick="ImgExportToExcel_Click"
                                CssClass="btn" EnableViewState="false" Width="132px" CausesValidation="true" 
                                ValidationGroup="Group1" BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgExportToCSV" Text="Export CSV" OnClick="ImgExportToCSV_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" 
                                ValidationGroup="Group1" BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgExportToPDF" Text="Export PDF" OnClick="ImgExportToPDF_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" 
                                ValidationGroup="Group1" BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" width="100%">
                            <asp:GridView ID="GridUserMailReport" runat="server" Width="100%" AllowPaging="True"
                                AutoGenerateColumns="true" GridLines="None" DataKeyNames="Id" 
                                PageSize="20">
                                <HeaderStyle CssClass="ListHeaderGrid" HorizontalAlign="Left" BorderColor="#bbd3e9"  BackColor="#e5eef6" />
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
