<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mailChart.aspx.cs" Inherits="AdminTool.mailChart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Chart ID="chartFunnel" runat="server" Width="550" Height="350" AntiAliasing="All"
        AlternateText="Image not available" EnableViewState="true">
        <Series>
            <asp:Series Name="chartSeries" ChartType="Bar" PostBackValue="#INDEX" IsValueShownAsLabel="true" Font="Segoe UI,15pt, style=Bold" LabelForeColor="White"></asp:Series>
        </Series>
        <Legends>
            <asp:Legend Name="chartLegend" Alignment="Near" Docking="Left" IsTextAutoFit="false" LegendStyle="Column" Font="Segoe UI"></asp:Legend>
        </Legends>
        <ChartAreas>
            <asp:ChartArea Name="chartAreaTrainingFunnel">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
</asp:Content>
