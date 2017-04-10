<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDashBoard.aspx.cs" Title="DashBoard"
    Inherits="AdminTool.frmDashBoard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
            width: 109px;
        }

        .style2 {
            width: 47px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd;">

        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr style="border-bottom: 1px solid #4090fd;">
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblUserTemplateInfo" runat="server" Text="<%$ Resources:AllString,dashboard_lblUserTemplateInfo %>" Style="font-weight: bold; text-align: left;" Font-Size="18"
                                Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <div id="Div1" runat="server" style="width: 100%; padding: 20px;">
                                <asp:GridView ID="GridTemplateStatus" runat="server" Width="101%"
                                    AutoGenerateColumns="False" GridLines="None" DataKeyNames="count">
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
                                            <ItemStyle CssClass="minWidth" />
                                            <HeaderTemplate>
                                                Template Count
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTemplateCount" runat="server" Text='<%# Eval("count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="minWidth" />
                                            <HeaderTemplate>
                                                Status
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTemplateStatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
        </div>

        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Lead API Usage Status" Style="font-weight: bold; text-align: left;" Font-Size="18"
                                Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 20px;">
            <table style="width: 100%">
                <tr>
                    <td class="style1" style="text-align: Left">
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" style="width: 100%;">
                            <asp:GridView ID="GridUserApiDetails" runat="server" Width="101%"
                                AutoGenerateColumns="False" GridLines="None" DataKeyNames="Id">
                                <HeaderStyle CssClass="ListHeaderGrid" HorizontalAlign="Left" BorderColor="#bbd3e9"
                                    BackColor="#e5eef6" />
                                <RowStyle CssClass="ListRowGrid" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" Height="40" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hiddenUserId" runat="server" Value='<%# Eval("userid") %>'></asp:HiddenField>
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
                                   <%-- <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Total API
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAPICallCount" runat="server" Text='<%# Eval("APICallCount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Remaning API
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemaningAPICount" runat="server" Text='<%# Eval("RemaningAPICount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Lead Info Submited 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubmitToCRMCount" runat="server" Text='<%# Eval("SubmitToCRMCount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>

        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr style="border: 1px solid #4090fd;">
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="Label1" runat="server" Text="Last 7 Day Usage" Style="font-weight: bold; text-align: left;" Font-Size="18"
                                Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <div id="Div2" runat="server" style="width: 100%; padding: 20px;">
                                <asp:Chart ID="chartAPIStatusReport" Width="900px" Height="300px" runat="server" ImageLocation="~/Charts/ChartPic_#SEQ(300,3)">
                                    <Series>
                                        <asp:Series Name="Series1"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </td>
                    </tr>
                    <tr style="border: 1px solid #4090fd;">
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="Label2" runat="server" Text="Overall Usage Status" Style="font-weight: bold; text-align: left;" Font-Size="18"
                                Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 1%; text-align: center;">
                            <asp:Chart ID="chartApiCountOverView" runat="server" BackColor="0, 0, 64" BackGradientStyle="LeftRight"
                                BorderlineWidth="0" Height="360px" Palette="None" PaletteCustomColors="Maroon" ImageLocation="~/Charts/ChartPic_#SEQ(300,3)"
                                Width="700px" BorderlineColor="64, 0, 64">
                                <Titles>
                                    <asp:Title ShadowOffset="10" Name="Items" />
                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                        LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                    <tr style="border: 1px solid #4090fd;">
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="Label3" runat="server" Text="Yearly Usage Status" Style="font-weight: bold; text-align: left;" Font-Size="18"
                                Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 1%; text-align: center;">
                            <asp:Label runat="server" Text="Year : " Style="vertical-align: top;"></asp:Label>
                            <asp:DropDownList ID="dropdowyear" runat="server" Style="vertical-align: top;" OnSelectedIndexChanged="dropdowyear_SelectedIndexChanged" AutoPostBack="true">
                                
                            </asp:DropDownList>
                            <asp:Chart ID="chartApiYearlyStatus" runat="server" BackColor="Cyan" BackGradientStyle="None" ImageLocation="~/Charts/ChartPic_#SEQ(300,3)"
                                BorderlineWidth="0" Height="360px" Palette="None" PaletteCustomColors="Maroon"
                                Width="700px" BorderlineColor="64, 0, 64">
                                <Titles>
                                    <asp:Title ShadowOffset="10" Name="Items" />
                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                        LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
